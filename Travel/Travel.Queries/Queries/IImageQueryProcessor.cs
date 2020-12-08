using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Api.Models.Address;
using Travel.Api.Models.Category;
using Travel.Api.Models.Image;
using Travel.Data.Model.Entities;

namespace Travel.Queries.Queries
{
    public interface IImageQueryProcessor
    {
        IQueryable<CategoryImageEntity> Get();
        CategoryImageEntity Get(Guid id);
        Task<CategoryImageEntity> Create(CatergoryImageModel model);
        Task<CategoryImageEntity> Update(Guid id, CatergoryImageModel model);
        Task Delete(Guid id);
    }
}
