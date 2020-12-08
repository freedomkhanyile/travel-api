using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Travel.Api.Models.Experience;
using Travel.Data.Model.Entities;
using Travel.Filters;
using Travel.Maps;
using Travel.Queries.Queries;

namespace Travel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {

        private readonly IExperienceQueryProcessor _experienceQuery;
        private readonly IExperienceImageQueryProcessor _experienceImageQueryProcessor;
        private readonly IAutoMapper _mapper;
        public ExperienceController(IExperienceQueryProcessor experienceQuery, IAutoMapper mapper, IExperienceImageQueryProcessor experienceImageQueryProcessor)
        {
            _experienceQuery = experienceQuery;
            _mapper = mapper;
            _experienceImageQueryProcessor = experienceImageQueryProcessor;
        }
       

        [HttpGet]

        public IQueryable<ExperienceModel> GetAll()
        {
            var experiences = _experienceQuery.Get();
            var resultModel = _mapper.Map<ExperienceEntity, ExperienceModel>(experiences);
            return resultModel;
        }


        [HttpGet("{id}")]
        public ExperienceModel Get(Guid id)
        {
            var experience = _experienceQuery.Get(id);
            var resultModel = _mapper.Map<ExperienceModel>(experience);
            return resultModel;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ExperienceModel> PostAsync([FromBody] ExperienceModel model)
        {
            var item = await _experienceQuery.Create(model);
            var resultModel = _mapper.Map<ExperienceModel>(item);
            if (model.Images != null)
            {
                foreach (var image in model.Images)
                {
                    image.ExperienceEntityId = resultModel.Id;
                    var img = await this._experienceImageQueryProcessor.Create(image);
                }
            }
            return resultModel;
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<ExperienceModel> PutAsync(Guid id, [FromBody] ExperienceModel model)
        {
            var item = await _experienceQuery.Update(id, model);
            var resultModel = _mapper.Map<ExperienceModel>(item);
            return resultModel;
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _experienceQuery.Delete(id);
        }

    }
}
