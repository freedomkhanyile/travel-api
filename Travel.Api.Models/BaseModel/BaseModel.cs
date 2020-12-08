using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Travel.Api.Models.BaseModel
{
 

    public abstract class BaseModel 
    {
        public Guid Id { get; set; }
        [MaxLength(256)]
        public string CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        [MaxLength(256)]
        public string ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public int StatusId { get; set; }
    }
}
