using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Travel.Api.Models.ExperienceCatergory;
using Travel.Data.Model.Entities;
using Travel.Filters;
using Travel.Maps;
using Travel.Queries.Queries;

namespace Travel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceCatergoryController : ControllerBase
    {
        private readonly IExperienceCatergoryQueryProcessor _experienceCatergoryQueryProcessor;
        private readonly IAutoMapper _mapper;
        public ExperienceCatergoryController(IExperienceCatergoryQueryProcessor experienceCatergoryQueryProcessor, IAutoMapper mapper)
        {
            _experienceCatergoryQueryProcessor = experienceCatergoryQueryProcessor;
            _mapper = mapper;
        }


        [HttpGet]
        [QueryableResult]

        public IQueryable<ExperienceCatergoryModel> GetAll()
        {
            var items = _experienceCatergoryQueryProcessor.Get();
            var resultModel = _mapper.Map<ExperienceCategoryEntity, ExperienceCatergoryModel>(items);
            return resultModel;
        }


        [HttpGet("{id}")]
        public ExperienceCatergoryModel Get(Guid id)
        {
            var category = _experienceCatergoryQueryProcessor.Get(id);
            var resultModel = _mapper.Map<ExperienceCatergoryModel>(category);
            return resultModel;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ExperienceCatergoryModel> PostAsync([FromBody] ExperienceCatergoryModel model)
        {
            var item = await _experienceCatergoryQueryProcessor.Create(model);
            var resultModel = _mapper.Map<ExperienceCatergoryModel>(item);
            return resultModel;
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<ExperienceCatergoryModel> PutAsync(Guid id, [FromBody] ExperienceCatergoryModel model)
        {
            var item = await _experienceCatergoryQueryProcessor.Update(id, model);
            var resultModel = _mapper.Map<ExperienceCatergoryModel>(item);
            return resultModel;
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _experienceCatergoryQueryProcessor.Delete(id);
        }
    }
}
