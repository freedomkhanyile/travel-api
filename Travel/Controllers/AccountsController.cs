using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Api.Models.Accounts;
using Travel.Api.Models.Users;
using Travel.Filters;
using Travel.Maps;
using Travel.Queries.Queries;

namespace Travel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        public AccountsController(IAccountQueryProcessor accountQuery, IAutoMapper mapper)
        {
            _accountQuery = accountQuery;
            _mapper = mapper;
        }

        private readonly IAccountQueryProcessor _accountQuery;
        private readonly IAutoMapper _mapper;

        [HttpPost("Login")]
        [ValidateModel]
        public UserWithTokenModel Login([FromBody] LoginModel model)
        {
            var result = _accountQuery.Authenticate(model.Email, model.Password);
            var resultModel = _mapper.Map<UserWithTokenModel>(result);
            return resultModel;
        }


        [HttpPost("Register")]
        [ValidateModel]
        public async Task<UserModel> Register([FromBody] RegisterModel model)
        {
            var result = await _accountQuery.Register(model);
            var resultModel = _mapper.Map<UserModel>(result);
            return resultModel;
        }

        [HttpPost("ByEmail")]
        [ValidateModel]
        public UserModel ByEmail([FromBody] ForgotPasswordModel model)
        {
            var result = _accountQuery.GetUserByEmailToken(model.Email);
            var resultModel = _mapper.Map<UserModel>(result);
            return resultModel;
        }

        [HttpPost("ByToken")]
        [ValidateModel]
        public UserWithTokenModel ByToken([FromBody] ForgotPasswordModel model)
        {
            var result = _accountQuery.GetUserByToken(model.Token);
            var resultModel = _mapper.Map<UserWithTokenModel>(result);
            return resultModel;
        }

        [HttpPost("ChangePassword")]
        [ValidateModel]
        public IActionResult ChangePassword([FromBody] ChangeUserPasswordModel model)
        {
            _accountQuery.ChangePassword(model);
            return Ok();
        }
    }
}