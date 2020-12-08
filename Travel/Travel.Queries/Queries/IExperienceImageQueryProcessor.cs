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
    public interface IExperienceImageQueryProcessor
    {
        IQueryable<ExperienceImageEntity> Get();
        ExperienceImageEntity Get(Guid id);
        Task<ExperienceImageEntity> Create(ExperienceImageModel model);
        Task<ExperienceImageEntity> Update(Guid id, ExperienceImageModel model);
        Task Delete(Guid id);
    }
}
