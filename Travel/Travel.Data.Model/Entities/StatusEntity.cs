using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Travel.Data.Model.Entities
{
     public class StatusEntity: AuditEntity<int>
    {
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
