using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Travel.Api.Common.Exceptions;
using Travel.Api.Models.Users;
using Travel.Data.Access.DAL;
using Travel.Data.Access.Helpers;
using Travel.Data.Model.Entities;
using Travel.Security;

namespace Travel.Queries.Queries
{
    public class UserQueryProcessor: IUserQueryProcessor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISecurityContext _securityContext;

        public UserQueryProcessor(IUnitOfWork unitOfWork, ISecurityContext securityContext)
        {
            _unitOfWork = unitOfWork;
            _securityContext = securityContext;
        }

        public IQueryable<UserEntity> Get()
        {
            return GetQuery();
        }

        private IQueryable<UserEntity> GetQuery()
        {
            return _unitOfWork.Query<UserEntity>()
                .Where(u => !u.IsDeleted)
                .Include(r => r.Roles)
                .ThenInclude(x=> x.Role);

        }
        public UserEntity Get(Guid id)
        {
            var user = GetQuery().FirstOrDefault(u => u.Id == id);
            if(user == null)
                throw  new NotFoundException("UserEntity not found");
            return user;
        }

        public UserEntity GetByEmail(string email)
        {
            var user = GetQuery().FirstOrDefault(u => u.Email == email);
            if (user == null)
                throw new NotFoundException("UserEntity not found");
            return user;
        }

        public UserEntity GetByToken(string token)
        {
            var user = GetQuery().FirstOrDefault(u => u.UserToken == token);
            if (user == null)
                throw new NotFoundException("UserEntity not found");
            return user;
        }

        public async Task<UserEntity> Create(CreateUserModel model)
        {
            var email = model.Email.Trim();
            if(GetQuery().Any(u => u.Email == email))
                throw new BadRequestException("This email address is already in use.");
            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                Surname = model.Surname,
                Email = email,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password.Trim().WithBCrypt(),
                CreateUserId = model.CreateUserId,
                CreateDate = DateTime.UtcNow.ToLocalTime(),
                ModifyUserId = model.ModifyUserId,
                ModifyDate = DateTime.UtcNow.ToLocalTime(),
                StatusId = 1
            };
            AddUserRoles(user, model.Roles);
            _unitOfWork.Add(user);
            await _unitOfWork.CommitAsync();
            return user;
        }

        public async Task<UserEntity> Update(Guid id, UpdateUserModel model)
        {
            var user = GetQuery().FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new NotFoundException("UserEntity not found");
            user.FirstName = model.FirstName;
            user.Surname = model.Surname;
            user.PhoneNumber = model.PhoneNumber;
            user.ModifyUserId = model.ModifyUserId;
            user.ModifyDate = DateTime.UtcNow.ToLocalTime();
            AddUserRoles(user, model.Roles);
            await _unitOfWork.CommitAsync();
            return user;
        }

        public async Task Delete(Guid id)
        {
            var user = GetQuery().FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new NotFoundException("UserEntity is not found");
            }

            if (user.IsDeleted) return;

            user.IsDeleted = true;
            await _unitOfWork.CommitAsync();
        }

        public async Task ChangePassword(Guid id, ChangeUserPasswordModel model)
        {
            var user = Get(id);
            user.Password = model.Password.WithBCrypt();
            await _unitOfWork.CommitAsync();
        }


        #region Private UserEntity Methods

        private void AddUserRoles(UserEntity userEntity, string[] RoleNames)
        {
            userEntity.Roles.Clear();
            foreach (var roleName in RoleNames)
            {
                var role = _unitOfWork.Query<RoleEntity>().FirstOrDefault(x => x.RoleName == roleName);
                if (role == null)
                {
                    throw new NotFoundException($"Role - {roleName} is not found");
                }
                userEntity.Roles.Add(new UserRoleEntity
                {
                    UserEntity = userEntity, 
                    Role = role,
                    CreateUserId = "system",
                    CreateDate = DateTime.UtcNow.ToLocalTime(),
                    ModifyUserId ="system",
                    ModifyDate = DateTime.UtcNow.ToLocalTime(),
                    StatusId = 1
                });
            }
        }

        #endregion
    }
}
