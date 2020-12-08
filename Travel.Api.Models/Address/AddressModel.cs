using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Api.Models.Address
{
    public class AddressModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string OtherId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedUserId { get; set; }
        public bool IsDeleted { get; set; }
        public int StatusId { get; set; }
    }
}
