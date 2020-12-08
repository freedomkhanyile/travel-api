using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Travel.Api.Models.Accounts;
using Travel.Api.Models.Users;
using Travel.Data.Model.Entities;
using Travel.Queries.Models;

namespace Travel.Queries.Queries
{
    public interface IAccountQueryProcessor
    {
        UserWithToken Authenticate(string email, string password);
        UserEntity GetUserByEmailToken(string email);
        UserWithToken GetUserByToken(string token);
        Task<UserEntity> Register(RegisterModel model);
        Task ChangePassword(ChangeUserPasswordModel requestModel);

    }
}
