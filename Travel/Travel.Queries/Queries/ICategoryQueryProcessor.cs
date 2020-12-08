using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Api.Models.Category;
using Travel.Data.Model.Entities;

namespace Travel.Queries.Queries
{
    public interface ICategoryQueryProcessor
    {
        IQueryable<CategoryEntity> Get();
        CategoryEntity Get(Guid id);
        Task<CategoryEntity> Create(CategoryModel model);
        Task<CategoryEntity> Update(Guid id, CategoryModel model);
        Task Delete(Guid id);
    }
}
