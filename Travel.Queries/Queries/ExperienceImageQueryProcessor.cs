using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Api.Common.Exceptions;
using Travel.Api.Models.Image;
using Travel.Data.Access.DAL;
using Travel.Data.Model.Entities;
using Travel.Security;

namespace Travel.Queries.Queries
{
    public class ExperienceImageQueryProcessor : IExperienceImageQueryProcessor
    {
        private readonly IUnitOfWork _uniOfWork;
        private readonly ISecurityContext _securityContext;

        public ExperienceImageQueryProcessor(IUnitOfWork uniOfWork, ISecurityContext securityContext)
        {
            _uniOfWork = uniOfWork;
            _securityContext = securityContext;
        }

        public IQueryable<ExperienceImageEntity> Get()
        {
            return GetQuery();
        }

        private IQueryable<ExperienceImageEntity> GetQuery()
        {
            var q = _uniOfWork.Query<ExperienceImageEntity>()
                .Where(b => !b.IsDeleted);
            if (_securityContext.IsAdministrator || _securityContext.IsStaff) return q;
            var userId = _securityContext.UserEntity.Id.ToString();
            return q;
        }

        public ExperienceImageEntity Get(Guid id)
        {
            var image = GetQuery().FirstOrDefault(b => b.Id == id);
            if(image == null)
                throw new NotFoundException("image not found");
            return image;
        }

        public async Task<ExperienceImageEntity> Create(ExperienceImageModel model)
        {
            var image = new ExperienceImageEntity
            {
                Id = Guid.NewGuid(),
                Url = model.Url,
                ExperienceEntityId = model.ExperienceEntityId,
                CreateDate = DateTime.UtcNow.ToLocalTime(),
                CreateUserId = _securityContext.UserEntity.Id.ToString(),
                ModifyDate = DateTime.UtcNow.ToLocalTime(),
                ModifyUserId = _securityContext.UserEntity.Id.ToString(),
                IsDeleted = false,
                StatusId= 1
            };
            _uniOfWork.Add(image);
            await _uniOfWork.CommitAsync();
            return image;
        }

        public async Task<ExperienceImageEntity> Update(Guid id, ExperienceImageModel model)
        {
            var image = GetQuery().FirstOrDefault(b => b.Id == id);
            if (image == null)
                throw new NotFoundException("image not found");
   
             
            await _uniOfWork.CommitAsync();
            return image;
        }

        public async Task Delete(Guid id)
        {
            var image = GetQuery().FirstOrDefault(b => b.Id == id);
            if (image == null)
                throw new NotFoundException("image not found");
            if (image.IsDeleted) return;

            image.IsDeleted = true;
            await _uniOfWork.CommitAsync();
        }
    }
}
