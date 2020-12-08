using System;
using System.Collections.Generic;
using System.Text;
using Travel.Api.Models.Experience;
using Travel.Api.Models.Image;

namespace Travel.Api.Models.Category
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public int StatusId { get; set; }
        public List<CatergoryImageModel> Images { get; set; }
    }
}
