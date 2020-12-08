using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Travel.Data.Model.Entities
{
     public class RoleEntity: AuditEntity<int>
    {
        public string RoleName { get; set; }
    }
}
