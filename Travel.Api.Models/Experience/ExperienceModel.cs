using System;
using System.Collections.Generic;
using System.Text;
using Travel.Api.Models.Category;
using Travel.Api.Models.Image;
using Travel.Data.Model.Entities;

namespace Travel.Api.Models.Experience
{
   public class ExperienceModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal PriceOnSpecial { get; set; }
        public DateTime OnSpecialStartDate { get; set; }
        public DateTime OnSpecialEndDate { get; set; }
        public bool IsDeleted { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public int StatusId { get; set; }
        public List<ExperienceImageModel> Images { get; set; }


    }
}
