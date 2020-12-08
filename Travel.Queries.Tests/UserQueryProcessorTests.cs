using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Travel.Api.Common.Exceptions;
using Travel.Api.Models.Users;
using Travel.Data.Access.DAL;
using Travel.Data.Model.Entities;
using Travel.Queries.Queries;
using Travel.Security;
using Xunit;

namespace Travel.Queries.Tests
{
    public class UserQueryProcessorTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ISecurityContext> _securityContextMock;

        private readonly IUserQueryProcessor _userQueryProcessor;
        private readonly List<UserEntity> _userList;
        private readonly List<RoleEntity> _roleList;

        private readonly UserEntity _currentUserEntity;
        private readonly Random _random;

        public UserQueryProcessorTests()
        {
            _random = new Random();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _userList = new List<UserEntity>();
            _unitOfWorkMock.Setup(x => x.Query<UserEntity>())
                .Returns(() => _userList.AsQueryable());

            _roleList = new List<RoleEntity>();
            _unitOfWorkMock.Setup(x => x.Query<RoleEntity>())
                .Returns(() => _roleList.AsQueryable());

            _currentUserEntity = new UserEntity { Id = Guid.NewGuid() };
            _securityContextMock = new Mock<ISecurityContext>(MockBehavior.Strict);
            _securityContextMock.Setup(x => x.UserEntity).Returns(_currentUserEntity);
            _securityContextMock.Setup(x => x.IsAdministrator).Returns(false);

            _userQueryProcessor = new UserQueryProcessor(_unitOfWorkMock.Object, _securityContextMock.Object);
        }

        [Fact]
        public void GetMustReturnAllUsers()
        {
            // arrange
            _userList.Add(new UserEntity { Id = _currentUserEntity.Id});

            // act
            var result = _userQueryProcessor.Get().ToList();

            // assert
            result.Count().Should().Be(1);
            result[0].Id.Should().Be(_currentUserEntity.Id);
        }

        [Fact]
        public void GetMustReturnAllUsersExceptDeleted()
        {
            // arrange
            _userList.Add(new UserEntity { Id = _currentUserEntity.Id });
            _userList.Add(new UserEntity { IsDeleted = true});
            _userList.Add(new UserEntity { IsDeleted = true});

            // act
            var result = _userQueryProcessor.Get().ToList();

            // assert
            result.Count().Should().Be(1);
            result[0].Id.Should().Be(_currentUserEntity.Id);
        }

        [Fact]
        public void GetMustReturnUserById()
        {
            // arrange
            var user = new UserEntity {Id = Guid.NewGuid()};
            _userList.Add(user);

            // act
            var result = _userQueryProcessor.Get(user.Id);

            // assert
            result.Should().Be(user);
        }

        [Fact]
        public void GetShouldThrowNotFoundExceptionIfUserIsNotFoundById()
        {
            // arrange
            var user = new UserEntity {Id = Guid.NewGuid()};
            _userList.Add(user);

            // act
            Action get = () => { _userQueryProcessor.Get(Guid.NewGuid()); };

            // assert
            get.Should().Throw<NotFoundException>();
        }

        [Fact]
        public void GetMustThrowNotFoundExceptionIfUserIsDeleted()
        {
            // arrange
            var user = new UserEntity{Id = Guid.NewGuid(), IsDeleted = true};
            _userList.Add(user);

            // act
            Action get = () => { _userQueryProcessor.Get(user.Id); };

            // assert
            get.Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task CreateMustSaveNewUser()
        {
            // arrange
            var model = new CreateUserModel
            {
                FirstName = "testFirstName",
                Surname = "testSurname",
                PhoneNumber = "000000000",
                Email = "test@mail.com",
                Password = _random.Next().ToString()
            };
            
            // act
            var result = await _userQueryProcessor.Create(model);

            // assert
            result.Email.Should().Be(model.Email);
            result.Password.Should().NotBeEmpty();
            result.FirstName.Should().Be(model.FirstName);
            result.Surname.Should().Be(model.Surname);
            result.PhoneNumber.Should().Be(model.PhoneNumber);

            _unitOfWorkMock.Verify(x => x.Add(result));
            _unitOfWorkMock.Verify(x => x.CommitAsync());
        }

        [Fact]
        public async Task CreateShouldAddUserWithRoles()
        {
            // arrange
            var roleModel = new RoleEntity { RoleName = "testAdminRole"};
            _roleList.Add(roleModel);

            var model = new CreateUserModel
            {
                FirstName = "testFirstName",
                Surname = "testSurname",
                PhoneNumber = "000000000",
                Email = "test@mail.com",
                Password = _random.Next().ToString(),
                Roles = new []{roleModel.RoleName}
            };

            // act
            var result = await _userQueryProcessor.Create(model);

            // assert
            result.Roles.Should().HaveCount(1);
            result.Roles.Should().Contain(x => x.UserEntity == result && x.Role == roleModel);
        }

        [Fact]
        public void CreateMustThrowBadRequestExceptionIfEmailIsNotUnique()
        {
            // arrange
            var model = new CreateUserModel
            {
                Email = "test@mail.com"
            };
            _userList.Add(new UserEntity {Email = model.Email});

            // act
            Action createExecution = () =>
            {
                var item = _userQueryProcessor.Create(model).Result;
            };

            // assert
            createExecution.Should().Throw<BadRequestException>();
        }

        [Fact]
        public async Task UpdateMustUpdateUserAsync()
        {
            // arrange
            var user = new UserEntity {Id = Guid.NewGuid()};
            _userList.Add(user);

            var model = new UpdateUserModel
            {
                FirstName = "testFirstName",
                Surname = "testSurname",
                PhoneNumber = "000000000",
                Email = "test@mail.com"
            };

            // act
            var result = await _userQueryProcessor.Update(user.Id, model);

            // assert
            result.Should().Be(user);
            result.FirstName.Should().Be(model.FirstName);
            result.Surname.Should().Be(model.Surname);
            result.PhoneNumber.Should().Be(model.PhoneNumber);

            _unitOfWorkMock.Verify(x => x.CommitAsync());
        }

        [Fact]
        public async Task UpdateMustUpdateUserRoles()
        {
            // arrange
            var roleModel = new RoleEntity { RoleName = "testAdminRole"};
            _roleList.Add(roleModel);

            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                Roles = new List<UserRoleEntity> {new UserRoleEntity()}
            };
            _userList.Add(user);

            var model = new UpdateUserModel
            {
                FirstName = "testFirstName",
                Surname = "testSurname",
                PhoneNumber = "000000000",
                Email = "test@mail.com",
                Roles = new[] {roleModel.RoleName}
            };

            // act
            var result = await _userQueryProcessor.Update(user.Id, model);

            // arrange
            result.Roles.Should().HaveCount(1);
            result.Roles.Should().Contain(x => x.UserEntity == result && x.Role == roleModel);
        }

        [Fact]
        public void UpdateMustThrowNotFoundExceptionIfUserIsNotFound()
        {
            // act
            Action execute = () =>
            {
                var result = _userQueryProcessor.Update(Guid.NewGuid(), new UpdateUserModel()).Result;
            };

            // assert
            execute.Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task DeleteMustMarkUserAsDeleted()
        {
            // arrange
            var user = new UserEntity {Id = Guid.NewGuid()};
            _userList.Add(user);

            // act
            await _userQueryProcessor.Delete(user.Id);

            // assert
            user.IsDeleted.Should().BeTrue();
            _unitOfWorkMock.Verify(x => x.CommitAsync());
        }

        [Fact]
        public void DeleteMustThrowExceptionIfUserIsNotFound()
        {
            // act
            Action execute = () => { _userQueryProcessor.Delete(Guid.NewGuid()).Wait(); };

            // assert
            execute.Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task ChangePasswordMustChangeTheUsersPassword()
        {
            // arrange
            var user = new UserEntity {Id = Guid.NewGuid()};
            _userList.Add(user);

            var newPassword = "newPassword";

            // act
            await _userQueryProcessor.ChangePassword(user.Id, new ChangeUserPasswordModel {Password = newPassword});

            // assert
            user.Password.Should().NotBeEmpty();

            _unitOfWorkMock.Verify(x=> x.CommitAsync());

        }

        [Fact]
        public void GetByEmailShouldReturnValidUser()
        {
            // arrange 
            var user = new UserEntity { Id = Guid.NewGuid(), Email = "test@mail.com" };
            _userList.Add(user);
            _userList.Add(new UserEntity { Id = Guid.NewGuid(), Email = "test123@mail.com" });
            _userList.Add(new UserEntity { Id = Guid.NewGuid(), Email = "test453@mail.com" });

            // act
           var result = _userQueryProcessor.GetByEmail("test@mail.com");

            // assert
            result.Id.Should().Be(user.Id);
        }

        [Fact]
        public void GetByTokenShouldReturnValidUser()
        {
            // arrange
             var user = new UserEntity { Id = Guid.NewGuid(), Email = "test@mail.com", UserToken = _random.Next().ToString() };
            _userList.Add(user);
            _userList.Add(new UserEntity { Id = Guid.NewGuid(), UserToken = _random.Next().ToString() });
            _userList.Add(new UserEntity { Id = Guid.NewGuid(), UserToken = _random.Next().ToString() });

            // act
            var result = _userQueryProcessor.GetByToken(user.UserToken);

            // assert
            result.Id.Should().Be(user.Id);
            result.UserToken.Should().Be(user.UserToken);

        }


    }
}
