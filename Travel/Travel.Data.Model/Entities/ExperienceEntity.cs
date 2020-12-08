using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Travel.Data.Model.Entities
{
 
    public class ExperienceEntity : AuditEntity<Guid>
    {
        public ExperienceEntity()
        {
            Images = new List<ExperienceImageEntity>();
        }
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceOnSpecial { get; set; }
        public DateTime OnSpecialStartDate { get; set; }
        public DateTime OnSpecialEndDate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual IList<ExperienceImageEntity> Images { get; set; }
    }
}
