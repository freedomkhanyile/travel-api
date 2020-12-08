using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Travel.Api.Models.Users;
using Travel.Data.Model.Entities;

namespace Travel.Maps
{
    public class UserMap: IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            var map = configuration.CreateMap<UserEntity, UserModel>();
            map.ForMember(x => x.Roles, 
                x => x.MapFrom(u => u.Roles
                                                            .Select(r => r.Role.RoleName)
                                                            .ToArray()));

        }
    }
}
