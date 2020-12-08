using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Api.Models.Experience;
using Travel.Data.Model.Entities;

namespace Travel.Queries.Queries
{
    public interface IExperienceQueryProcessor
    {
        IQueryable<ExperienceEntity> Get();
        ExperienceEntity Get(Guid id);
        Task<ExperienceEntity> Create(ExperienceModel model);
        Task<ExperienceEntity> Update(Guid id, ExperienceModel model);
        Task Delete(Guid id);
    }
}
