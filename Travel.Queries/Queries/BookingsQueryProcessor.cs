using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Api.Common.Exceptions;
using Travel.Api.Models.Bookings;
using Travel.Data.Access.DAL;
using Travel.Data.Model.Entities;
using Travel.Security;

namespace Travel.Queries.Queries
{
    public class BookingsQueryProcessor: IBookingsQueryProcessor
    {
        private readonly IUnitOfWork _uniOfWork;
        private readonly ISecurityContext _securityContext;

        public BookingsQueryProcessor(IUnitOfWork uniOfWork, ISecurityContext securityContext)
        {
            _uniOfWork = uniOfWork;
            _securityContext = securityContext;
        }

        public IQueryable<BookingEntity> Get()
        {
            return GetQuery();
        }

        private IQueryable<BookingEntity> GetQuery()
        {
            var q = _uniOfWork.Query<BookingEntity>()
                .Where(b => !b.IsDeleted);
            if (_securityContext.IsAdministrator || _securityContext.IsStaff) return q;
            var userId = _securityContext.UserEntity.Id;
            q = q.Where(x => x.UserEntityId == userId);
            return q;
        }

        public BookingEntity Get(Guid id)
        {
            var booking = GetQuery().FirstOrDefault(b => b.Id == id);
            if(booking == null)
                throw new NotFoundException("Booking not found");
            return booking;
        }

        public async Task<BookingEntity> Create(CreateBookingModel model)
        {
            var booking = new BookingEntity
            {
                Id = Guid.NewGuid(),
                UserEntityId = _securityContext.UserEntity.Id,
                BookingStatusCode = model.BookingStatusCode,
                DateOfBooking = model.DateOfBooking,
                OtherBookingDetails = model.OtherBookingDetails,
                SelfBooked = model.SelfBooked,
                CreateDate = DateTime.UtcNow.ToLocalTime(),
                CreateUserId = _securityContext.UserEntity.Id.ToString(),
                ModifyDate = DateTime.UtcNow.ToLocalTime(),
                ModifyUserId = _securityContext.UserEntity.Id.ToString(),
                IsDeleted = false,
                StatusId = 1
            };
            _uniOfWork.Add(booking);
            await _uniOfWork.CommitAsync();
            return booking;
        }

        public async Task<BookingEntity> Update(Guid id, UpdateBookingModel model)
        {
            var booking = GetQuery().FirstOrDefault(b => b.Id == id);
            if (booking == null)
                throw new NotFoundException("Booking not found");
            booking.BookingStatusCode = model.BookingStatusCode;
            booking.DateOfBooking = model.DateOfBooking;
            booking.SelfBooked = model.SelfBooked;
            booking.OtherBookingDetails = model.OtherBookingDetails;
            booking.ModifyDate = DateTime.UtcNow.ToLocalTime();
            booking.ModifyUserId = _securityContext.UserEntity.Id.ToString();
            booking.IsDeleted = model.IsDeleted;

            await _uniOfWork.CommitAsync();
            return booking;
        }

        public async Task Delete(Guid id)
        {
            var booking = GetQuery().FirstOrDefault(b => b.Id == id);
            if (booking == null)
                throw new NotFoundException("Booking not found");
            if (booking.IsDeleted) return;

            booking.IsDeleted = true;
            await _uniOfWork.CommitAsync();
        }
    }
}
