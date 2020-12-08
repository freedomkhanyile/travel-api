using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Data.Model.Entities
{
    public class ImageEntity : AuditEntity<Guid>
    {
        public string Url { get; set; }
        public bool IsDeleted { get; set; }
        public string OtherId { get; set; }
    }
}
