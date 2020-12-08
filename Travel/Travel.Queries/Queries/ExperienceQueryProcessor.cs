using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Api.Common.Exceptions;
using Travel.Api.Models.Experience;
using Travel.Data.Access.DAL;
using Travel.Data.Model.Entities;
using Travel.Security;

namespace Travel.Queries.Queries
{
    public class ExperienceQueryProcessor : IExperienceQueryProcessor
    {
        private readonly IUnitOfWork _uniOfWork;
        private readonly ISecurityContext _securityContext;

        public ExperienceQueryProcessor(IUnitOfWork uniOfWork, ISecurityContext securityContext)
        {
            _uniOfWork = uniOfWork;
            _securityContext = securityContext;
        }

        public IQueryable<ExperienceEntity> Get()
        {
            return GetQuery();
        }

        private IQueryable<ExperienceEntity> GetQuery()
        {
            var q = _uniOfWork.Query<ExperienceEntity>()
                .Where(b => !b.IsDeleted).Include(b=>b.Images);
            if (_securityContext.IsAdministrator || _securityContext.IsStaff) return q;
            return q;
        }

        public ExperienceEntity Get(Guid id)
        {
            var experience = GetQuery().FirstOrDefault(b => b.Id == id);
            if(experience == null)
                throw new NotFoundException("Experience not found");
            return experience;
        }

        public async Task<ExperienceEntity> Create(ExperienceModel model)
        {
            var experience = new ExperienceEntity
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                PriceOnSpecial = model.PriceOnSpecial,
                OnSpecialStartDate = model.OnSpecialStartDate,
                OnSpecialEndDate = model.OnSpecialEndDate,
                CreateDate = DateTime.UtcNow.ToLocalTime(),
                CreateUserId = _securityContext.UserEntity.Id.ToString(),
                ModifyDate = DateTime.UtcNow.ToLocalTime(),
                ModifyUserId = _securityContext.UserEntity.Id.ToString(),
                IsDeleted = false,
                StatusId= 1
            };
            _uniOfWork.Add(experience);
            await _uniOfWork.CommitAsync();
            return experience;
        }

        public async Task<ExperienceEntity> Update(Guid id, ExperienceModel model)
        {
            var experience = GetQuery().FirstOrDefault(b => b.Id == id);
            if (experience == null)
                throw new NotFoundException("Experience not found");
            //booking.BookingStatusCode = model.BookingStatusCode;
   
             
            await _uniOfWork.CommitAsync();
            return experience;
        }

        public async Task Delete(Guid id)
        {
            var experience = GetQuery().FirstOrDefault(b => b.Id == id);
            if (experience == null)
                throw new NotFoundException("Experience not found");
            if (experience.IsDeleted) return;

            experience.IsDeleted = true;
            await _uniOfWork.CommitAsync();
        }

        public IQueryable<ExperienceEntity> GetAllDetailed()
        {
            var items = GetQuery();
            foreach(var item in items)
            {
                
            }
            return GetQuery();
        }
    }
}
