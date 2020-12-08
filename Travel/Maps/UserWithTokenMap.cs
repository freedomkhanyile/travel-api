using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Travel.Api.Models.Users;
using Travel.Queries.Models;

namespace Travel.Maps
{
    public class UserWithTokenMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            var map = configuration.CreateMap<UserWithToken, UserWithTokenModel>();
        }
    }
}
