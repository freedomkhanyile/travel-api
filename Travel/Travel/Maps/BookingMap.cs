using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Travel.Api.Models.Bookings;
using Travel.Data.Model.Entities;

namespace Travel.Maps
{
    public class BookingMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            var map = configuration.CreateMap<BookingEntity, BookingModel>();
            map.ForMember(x => x.UserEntityId, x => x.MapFrom(x => x.UserEntity.Id));
        }
    }
}
