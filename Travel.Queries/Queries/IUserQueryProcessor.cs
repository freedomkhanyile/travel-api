using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Api.Models.Users;
using Travel.Data.Model.Entities;

namespace Travel.Queries.Queries
{
    public interface IUserQueryProcessor
    {
        IQueryable<UserEntity> Get();
        UserEntity Get(Guid id);
        UserEntity GetByEmail(string email);
        UserEntity GetByToken(string token);
        Task<UserEntity> Create(CreateUserModel model);
        Task<UserEntity> Update(Guid id, UpdateUserModel model);
        Task Delete(Guid id);
        Task ChangePassword(Guid id, ChangeUserPasswordModel model);
    }
}
