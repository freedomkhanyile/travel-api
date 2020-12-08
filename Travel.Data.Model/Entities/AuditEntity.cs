using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Travel.Contracts;

namespace Travel.Data.Model.Entities
{
   public abstract class AuditEntity<T>: Entity<T>, IAuditEntity
    {
        [MaxLength(256)]
        public string CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        [MaxLength(256)]
        public string ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public int StatusId { get; set; }
    }
}
