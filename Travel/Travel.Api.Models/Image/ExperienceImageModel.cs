using System;
using System.Collections.Generic;
using System.Text;
using Travel.Data.Model.Entities;

namespace Travel.Api.Models.Image
{
    public class ExperienceImageModel
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public int StatusId { get; set; }
        public bool IsDeleted { get; set; }

        public Guid ExperienceEntityId { get; set; }
    }
}
