using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Api.Models.Bookings;
using Travel.Data.Model.Entities;

namespace Travel.Queries.Queries
{
    public interface IBookingsQueryProcessor
    {
        IQueryable<BookingEntity> Get();
        BookingEntity Get(Guid id);
        Task<BookingEntity> Create(CreateBookingModel model);
        Task<BookingEntity> Update(Guid id, UpdateBookingModel model);
        Task Delete(Guid id);
    }
}
