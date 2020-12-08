using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Api.Models.ExperienceCatergory
{
    public class ExperienceCatergoryModel
    {
        public Guid Id { get; set; }
        public Guid CatergoryId { get; set; }
        public Guid ExperienceId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedUserId { get; set; }
        public bool IsDeleted { get; set; }
        public int StatusId { get; set; }
    }
}
