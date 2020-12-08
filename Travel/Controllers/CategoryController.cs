using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Travel.Api.Models.Category;
using Travel.Data.Model.Entities;
using Travel.Filters;
using Travel.Maps;
using Travel.Queries.Queries;

namespace Travel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryQueryProcessor _categoryQueryProcessor;
        private readonly IImageQueryProcessor __imageQueryProcessor;
        private readonly IAutoMapper _mapper;
        public CategoryController(ICategoryQueryProcessor categoryQueryProcessor, IAutoMapper mapper, IImageQueryProcessor imageQueryProcessor)
        {
            _categoryQueryProcessor = categoryQueryProcessor;
            __imageQueryProcessor = imageQueryProcessor;
            _mapper = mapper;
        }


        [HttpGet]

        public IQueryable<CategoryModel> GetAll()
        {
            var categories = _categoryQueryProcessor.Get();
            var resultModel = _mapper.Map<CategoryEntity, CategoryModel>(categories);
            return resultModel;
        }


        [HttpGet("{id}")]
        public CategoryModel Get(Guid id)
        {
            var category = _categoryQueryProcessor.Get(id);
            var resultModel = _mapper.Map<CategoryModel>(category);
            return resultModel;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<CategoryModel> PostAsync([FromBody] CategoryModel model)
        {
            var item = await _categoryQueryProcessor.Create(model);
            var resultModel = _mapper.Map<CategoryModel>(item);
            if(model.Images != null)
            {
                foreach(var image in model.Images)
                {
                    image.CategoryEntityId = resultModel.Id;
                    image.CreateUserId = resultModel.CreateUserId;
                    image.ModifyUserId = resultModel.ModifyUserId;
                    var img = await this.__imageQueryProcessor.Create(image);
                }
            }
            return resultModel;
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<CategoryModel> PutAsync(Guid id, [FromBody] CategoryModel model)
        {
            var item = await _categoryQueryProcessor.Update(id, model);
            var resultModel = _mapper.Map<CategoryModel>(item);
            return resultModel;
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _categoryQueryProcessor.Delete(id);
        }
    }
}
