using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Api.Models.Users;
using Travel.Data.Model.Entities;
using Travel.Filters;
using Travel.Maps;
using Travel.Queries.Queries;

namespace Travel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserQueryProcessor _userQuery;
        private readonly IAutoMapper _mapper;

        public UsersController(IUserQueryProcessor userQuery, IAutoMapper mapper)
        {
            _userQuery = userQuery;
            _mapper = mapper;
        }

        [HttpGet]
        [QueryableResult]
        public IQueryable<UserModel> Get()
        {
            var users = _userQuery.Get();
            var resultModel = _mapper.Map<UserEntity, UserModel>(users);
            return resultModel;
        }

        [HttpGet("{id}")]
        public UserModel Get(Guid id)
        {
            var user = _userQuery.Get(id);
            var resultModel = _mapper.Map<UserModel>(user);
            return resultModel;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<UserModel> PostAsync([FromBody] CreateUserModel model)
        {
            var item = await _userQuery.Create(model);
            var resultModel = _mapper.Map<UserModel>(item);
            return resultModel;
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<UserModel> PutAsync([FromBody] UpdateUserModel model, Guid id)
        {
            var item = await _userQuery.Update(id, model);
            var resultModel = _mapper.Map<UserModel>(item);
            return resultModel;
        }

        [HttpDelete("{id}")]
        public async Task DeletAsync(Guid id)
        {
            await _userQuery.Delete(id);
        }

    }
}
