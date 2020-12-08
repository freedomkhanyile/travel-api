using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Travel.Api.Models.Bookings
{
    public class CreateBookingModel
    {
        [Required]
        public string BookingStatusCode { get; set; }
        [Required]
        public DateTime DateOfBooking { get; set; }
        [Required]
        public bool SelfBooked { get; set; }
        public string OtherBookingDetails { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }
}
