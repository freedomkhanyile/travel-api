using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Api.Common.Exceptions;
using Travel.Api.Models.Category;
using Travel.Data.Access.DAL;
using Travel.Data.Model.Entities;
using Travel.Security;

namespace Travel.Queries.Queries
{
    public class PublicCategoryQueryProcessor : IPublicCategoryQueryProcessor
    {
        private readonly IUnitOfWork _uniOfWork;

        public PublicCategoryQueryProcessor(IUnitOfWork uniOfWork)
        {
            _uniOfWork = uniOfWork;
        }

        public IQueryable<CategoryEntity> Get()
        {
            return GetQuery();
        }

        private IQueryable<CategoryEntity> GetQuery()
        {
            var q = _uniOfWork.Query<CategoryEntity>()
                .Where(b => !b.IsDeleted).Include(b=>b.Images);
            return q;
        }

        public CategoryEntity Get(Guid id)
        {
            var category = GetQuery().FirstOrDefault(b => b.Id == id);
            if(category == null)
                throw new NotFoundException("category not found");
            return category;
        }


        public List<CategoryModel> GetByIds(List<string> experienceIds)
        {
            var items = GetQuery().Where(x => experienceIds.Contains(x.Id.ToString()));
            return null;
        }
    }
}
