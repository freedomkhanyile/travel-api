using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Travel.Api.Common.Exceptions;
using Travel.Api.Models.Accounts;
using Travel.Api.Models.Users;
using Travel.Data.Access.DAL;
using Travel.Data.Access.Helpers;
using Travel.Data.Model.Entities;
using Travel.Queries.Queries;
using Travel.Security;
using Travel.Security.Auth;
using Xunit;

namespace Travel.Queries.Tests
{
    public class AccountQueryProcessorTests
    {
        private readonly IAccountQueryProcessor _accountQueryProcessor;
        private readonly Mock<IUserQueryProcessor> _userQueryProcessorMock;

        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly Mock<ITokenBuilder> _tokenBuilderMock;
        private readonly Mock<ISecurityContext> _securityContextMock;

        private readonly List<UserEntity> _userList;
     
        private readonly Random _random;
        public AccountQueryProcessorTests()
        {
            _random = new Random();

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userList = new List<UserEntity>();
            _unitOfWorkMock.Setup(x => x.Query<UserEntity>())
                .Returns(() => _userList.AsQueryable());

            _tokenBuilderMock = new Mock<ITokenBuilder>(MockBehavior.Strict);
            _userQueryProcessorMock = new Mock<IUserQueryProcessor>();
            _securityContextMock = new Mock<ISecurityContext>(MockBehavior.Strict);

            _accountQueryProcessor = new AccountQueryProcessor(_unitOfWorkMock.Object, _tokenBuilderMock.Object,_userQueryProcessorMock.Object, _securityContextMock.Object);
            
        }

        [Fact]
        public void AuthenticateShouldReturnUserWithToken()
        {
            // arrange
            var password = _random.Next().ToString();
            var user = new UserEntity
            {
                Email = "test@mail.com",
                Password = password.WithBCrypt(),
                Roles = new List<UserRoleEntity>
                {
                    new UserRoleEntity {Role = new RoleEntity {RoleName = _random.Next().ToString()}},
                    new UserRoleEntity {Role = new RoleEntity {RoleName = _random.Next().ToString()}}
                }
            };
            _userList.Add(user);
            var expiryTokenDate = DateTime.UtcNow.ToLocalTime() + TokenAuthOption.ExpiresSpan;
            var token = _random.Next().ToString();
            _tokenBuilderMock.Setup(tb => tb.Build(
                    user.Email,
                    It.Is<string[]>(roles => roles.SequenceEqual(user.Roles.Select(x => x.Role.RoleName).ToArray())),
                    It.Is<DateTime>(d => d - expiryTokenDate < TimeSpan.FromSeconds(1))))
                .Returns(token);

            // act
            var result = _accountQueryProcessor.Authenticate(user.Email, password);

            // assert
            result.User.Should().Be(user);
            result.Token.Should().Be(token);
            result.ExpiresAt.Should().BeCloseTo(expiryTokenDate, 1000);
        }

        [Fact]
        public void AuthenticateShouldThrowBadRequestExceptionIfPasswordIsWrong()
        {
            // arrange
            var password = _random.Next().ToString();
            var user = new UserEntity
            {
                Email = "test@mail.com",
                Password = password.WithBCrypt()
            };

            // act
            Action execute = () => _accountQueryProcessor.Authenticate(user.Email, _random.Next().ToString());

            // assert
            execute.Should().Throw<BadRequestException>();
        }

        [Fact]
        public void AuthenticateShouldThrowBadRequestExceptionIfUserIsDeleted()
        {
            // arrange
            var password = _random.Next().ToString();
            var user = new UserEntity
            {
                Email = "test@mail.com",
                Password = password.WithBCrypt(),
                IsDeleted = true
            };
            _userList.Add(user);

            // act
            Action execute = () => _accountQueryProcessor.Authenticate(user.Email, password);

            // assert
            execute.Should().Throw<BadRequestException>();
        }

        [Fact]
        public async Task RegisterShouldCreateUserViaQuery()
        {
            // arrange
            var model = new RegisterModel
            {
                Email = "test@mail.com",
                Password = _random.Next().ToString(),
                FirstName = "testFirstName",
                Surname = "testSurname",
                PhoneNumber = "000000000"
            };

            var createdUser = new UserEntity();
            _userQueryProcessorMock.Setup(u => u.Create(It.Is<CreateUserModel>(m =>
                m.Email == model.Email
                && m.Password == model.Password
                && m.FirstName == model.FirstName
                && m.Surname == model.Surname
                && m.PhoneNumber == model.PhoneNumber
            ))).Returns(Task.FromResult(createdUser));

            // act
            var result = await _accountQueryProcessor.Register(model);

            // assert
            result.Should().Be(createdUser);
        }

        [Fact]
        public async Task ChangePasswordShouldCallUserQueryWithCurrentUserAsync()
        {
            // arrange
            var user = new UserEntity {Id = Guid.NewGuid()};
            _securityContextMock.SetupGet(x => x.UserEntity).Returns(user);

            var model = new ChangeUserPasswordModel {Password = _random.Next().ToString()};
            _userQueryProcessorMock.Setup(x => x.ChangePassword(user.Id, model))
                .Returns(Task.FromResult(0))
                .Verifiable();

            // act
            await _accountQueryProcessor.ChangePassword(model);

            // assert
            _userQueryProcessorMock.Verify();
        }
    }
}
