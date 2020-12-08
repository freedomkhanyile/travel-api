using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Api.Models.Bookings
{
    public class BookingModel
    {
        public Guid Id { get; set; }
        public string BookingStatusCode { get; set; }
        public DateTime DateOfBooking { get; set; }
        public bool SelfBooked { get; set; }
        public string OtherBookingDetails { get; set; }
        public Guid UserEntityId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
