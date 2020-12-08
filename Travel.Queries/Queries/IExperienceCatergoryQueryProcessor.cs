using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Api.Models.ExperienceCatergory;
using Travel.Data.Model.Entities;

namespace Travel.Queries.Queries
{
    public interface IExperienceCatergoryQueryProcessor
    {
        IQueryable<ExperienceCategoryEntity> Get();
        ExperienceCategoryEntity Get(Guid id);
        Task<ExperienceCategoryEntity> Create(ExperienceCatergoryModel model);
        Task<ExperienceCategoryEntity> Update(Guid id, ExperienceCatergoryModel model);
        Task Delete(Guid id);
    }
}
