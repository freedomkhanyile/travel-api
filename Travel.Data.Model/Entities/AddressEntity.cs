using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Travel.Data.Model.Entities
{
 
    public class AddressEntity : AuditEntity<Guid>
    {
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string OtherId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
