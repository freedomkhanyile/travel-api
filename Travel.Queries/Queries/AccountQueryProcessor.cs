using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Travel.Api.Common.Exceptions;
using Travel.Api.Models.Accounts;
using Travel.Api.Models.Users;
using Travel.Data.Access.DAL;
using Travel.Data.Access.Helpers;
using Travel.Data.Model.Entities;
using Travel.Queries.Models;
using Travel.Security;
using Travel.Security.Auth;

namespace Travel.Queries.Queries
{
    public class AccountQueryProcessor: IAccountQueryProcessor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenBuilder _tokenBuilder;
        private readonly IUserQueryProcessor _userQueryProcessor;
        private readonly ISecurityContext _securityContext;

        public AccountQueryProcessor(IUnitOfWork unitOfWork, ITokenBuilder tokenBuilder, IUserQueryProcessor userQueryProcessor, ISecurityContext securityContext)
        {
            _unitOfWork = unitOfWork;
            _tokenBuilder = tokenBuilder;
            _userQueryProcessor = userQueryProcessor;
            _securityContext = securityContext;
        }

        public UserWithToken Authenticate(string email, string password)
        {
            var user = (from u in _unitOfWork.Query<UserEntity>()
                        where u.Email == email && !u.IsDeleted
                        select u)
                .Include(r => r.Roles)
                .ThenInclude(r => r.Role)
                .FirstOrDefault();

            if(user == null)
                throw new BadRequestException("email/password not valid!");

            if(string.IsNullOrEmpty(password) || !user.Password.VerifyWithBCrypt(password))
                throw new BadRequestException("email/password not valid!");


            var expiryPeriod = DateTime.Now.ToLocalTime() + TokenAuthOption.ExpiresSpan;
            var token = _tokenBuilder.Build(user.Email, user.Roles.Select(r => r.Role.RoleName).ToArray(),
                expiryPeriod);

            return new UserWithToken
            {
                ExpiresAt = expiryPeriod,
                Token = token,
                User = user
            };
        }

        public UserEntity GetUserByEmailToken(string email)
        {
            var user = _userQueryProcessor.GetByEmail(email);
            user.UserToken = Guid.NewGuid().ToString();
            return user;
        }

        public UserWithToken GetUserByToken(string token)
        {
            var user = _userQueryProcessor.GetByToken(token);
            // log this user in so that we can change the password or by bypass the login process on activation.

            var expiryPeriod = DateTime.Now.ToLocalTime() + TokenAuthOption.ExpiresSpan;
            var genToken = _tokenBuilder.Build(user.Email, user.Roles.Select(r => r.Role.RoleName).ToArray(),
                expiryPeriod);

            return new UserWithToken
            {
                ExpiresAt = expiryPeriod,
                Token = genToken,
                User = user
            };
        }

        public async Task<UserEntity> Register(RegisterModel model)
        {
            var registerModel = new CreateUserModel
            {
                FirstName = model.FirstName,
                Surname = model.Surname,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Password = model.Password,
                Roles = model.Roles
            };

            var user = await _userQueryProcessor.Create(registerModel);
            return user;
        }

        public async Task ChangePassword(ChangeUserPasswordModel requestModel)
        {
            await _userQueryProcessor.ChangePassword(_securityContext.UserEntity.Id, requestModel);
        }
        }
}
