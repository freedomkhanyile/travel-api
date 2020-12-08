using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Travel.Data.Model.Entities
{
 
    public class ExperienceCategoryEntity : AuditEntity<Guid>
    {
        public Guid CategoryEntityId { get; set; }
        public Guid ExperienceEntityId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
