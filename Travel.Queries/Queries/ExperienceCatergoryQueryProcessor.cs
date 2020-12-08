using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Api.Common.Exceptions;
using Travel.Api.Models.Address;
using Travel.Api.Models.ExperienceCatergory;
using Travel.Data.Access.DAL;
using Travel.Data.Model.Entities;
using Travel.Security;

namespace Travel.Queries.Queries
{
    public class ExperienceCatergoryQueryProcessor : IExperienceCatergoryQueryProcessor
    {
        private readonly IUnitOfWork _uniOfWork;
        private readonly ISecurityContext _securityContext;

        public ExperienceCatergoryQueryProcessor(IUnitOfWork uniOfWork, ISecurityContext securityContext)
        {
            _uniOfWork = uniOfWork;
            _securityContext = securityContext;
        }

        public IQueryable<ExperienceCategoryEntity> Get()
        {
            return GetQuery();
        }

        private IQueryable<ExperienceCategoryEntity> GetQuery()
        {
            var q = _uniOfWork.Query<ExperienceCategoryEntity>()
                .Where(b => !b.IsDeleted);
            if (_securityContext.IsAdministrator || _securityContext.IsStaff) return q;
            var userId = _securityContext.UserEntity.Id.ToString();
            return q;
        }

        public ExperienceCategoryEntity Get(Guid id)
        {
            var address = GetQuery().FirstOrDefault(b => b.Id == id);
            if(address == null)
                throw new NotFoundException("address not found");
            return address;
        }

        public async Task<ExperienceCategoryEntity> Create(ExperienceCatergoryModel model)
        {
            var address = new ExperienceCategoryEntity
            {
                Id = Guid.NewGuid(),
                CategoryEntityId = model.CatergoryId,
                ExperienceEntityId = model.ExperienceId,
                CreateDate = DateTime.UtcNow.ToLocalTime(),
                CreateUserId = _securityContext.UserEntity.Id.ToString(),
                ModifyDate = DateTime.UtcNow.ToLocalTime(),
                ModifyUserId = _securityContext.UserEntity.Id.ToString(),
                IsDeleted = false,
                StatusId= 1
            };
            _uniOfWork.Add(address);
            await _uniOfWork.CommitAsync();
            return address;
        }

        public async Task<ExperienceCategoryEntity> Update(Guid id, ExperienceCatergoryModel model)
        {
            var address = GetQuery().FirstOrDefault(b => b.Id == id);
            if (address == null)
                throw new NotFoundException("address not found");
   
             
            await _uniOfWork.CommitAsync();
            return address;
        }

        public async Task Delete(Guid id)
        {
            var address = GetQuery().FirstOrDefault(b => b.Id == id);
            if (address == null)
                throw new NotFoundException("address not found");
            if (address.IsDeleted) return;

            address.IsDeleted = true;
            await _uniOfWork.CommitAsync();
        }

        public List<ExperienceCategoryEntity> GetByExperienceId(string experienceId)
        {
            return GetQuery().Where(x=>x.ExperienceEntityId.ToString() == experienceId).ToList();
        }

    }
}
