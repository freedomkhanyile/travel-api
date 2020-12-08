using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Travel.Data.Model.Entities
{
 
    public class CategoryImageEntity : AuditEntity<Guid>
    {
        public string Url { get; set; }
        public bool IsDeleted { get; set; }
        public Guid CategoryEntityId { get; set; }
    }
}
