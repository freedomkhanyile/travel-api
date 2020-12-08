using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Travel.Data.Model.Entities
{
     public class BookingEntity:AuditEntity<Guid>
    {
        public string BookingStatusCode { get; set; }
        public DateTime DateOfBooking { get; set; }
        public bool SelfBooked { get; set; }
        public string OtherBookingDetails { get; set; }
        public Guid UserEntityId { get; set; }
        public virtual UserEntity UserEntity { get; set; }
        public bool IsDeleted { get; set; }

    }
}
