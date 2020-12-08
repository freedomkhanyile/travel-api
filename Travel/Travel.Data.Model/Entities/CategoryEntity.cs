using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Travel.Data.Model.Entities
{
 
    public class CategoryEntity : AuditEntity<Guid>
    {
        public CategoryEntity()
        {
            Images = new List<CategoryImageEntity>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public virtual IList<CategoryImageEntity> Images { get; set; }
    }
}
