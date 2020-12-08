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
    public class CategoryQueryProcessor : ICategoryQueryProcessor
    {
        private readonly IUnitOfWork _uniOfWork;
        private readonly ISecurityContext _securityContext;

        public CategoryQueryProcessor(IUnitOfWork uniOfWork, ISecurityContext securityContext)
        {
            _uniOfWork = uniOfWork;
            _securityContext = securityContext;
        }

        public IQueryable<CategoryEntity> Get()
        {
            return GetQuery();
        }

        private IQueryable<CategoryEntity> GetQuery()
        {
            var q = _uniOfWork.Query<CategoryEntity>()
                .Where(b => !b.IsDeleted).Include(b=>b.Images);
            if (_securityContext.IsAdministrator || _securityContext.IsStaff) return q;
            var userId = _securityContext.UserEntity.Id.ToString();
            return q;
        }

        public CategoryEntity Get(Guid id)
        {
            var category = GetQuery().FirstOrDefault(b => b.Id == id);
            if(category == null)
                throw new NotFoundException("category not found");
            return category;
        }

        public async Task<CategoryEntity> Create(CategoryModel model)
        {
            var category = new CategoryEntity
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                CreateDate = DateTime.UtcNow.ToLocalTime(),
                CreateUserId = _securityContext.UserEntity.Id.ToString(),
                ModifyDate = DateTime.UtcNow.ToLocalTime(),
                ModifyUserId = _securityContext.UserEntity.Id.ToString(),
                IsDeleted = false,
                StatusId= 1
            };
            _uniOfWork.Add(category);
            await _uniOfWork.CommitAsync();
            return category;
        }

        public async Task<CategoryEntity> Update(Guid id, CategoryModel model)
        {
            var category = GetQuery().FirstOrDefault(b => b.Id == id);
            if (category == null)
                throw new NotFoundException("category not found");
   
             
            await _uniOfWork.CommitAsync();
            return category;
        }

        public async Task Delete(Guid id)
        {
            var category = GetQuery().FirstOrDefault(b => b.Id == id);
            if (category == null)
                throw new NotFoundException("category not found");
            if (category.IsDeleted) return;

            category.IsDeleted = true;
            await _uniOfWork.CommitAsync();
        }

        public List<CategoryModel> GetByIds(List<string> experienceIds)
        {
            var items = GetQuery().Where(x => experienceIds.Contains(x.Id.ToString()));
            return null;
        }
    }
}
