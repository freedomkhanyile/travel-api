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
    public class PublicCategoryController : ControllerBase
    {
        private readonly IPublicCategoryQueryProcessor _categoryQueryProcessor;
        private readonly IImageQueryProcessor __imageQueryProcessor;
        private readonly IAutoMapper _mapper;
        public PublicCategoryController(IPublicCategoryQueryProcessor categoryQueryProcessor, IAutoMapper mapper, IImageQueryProcessor imageQueryProcessor)
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
    }
}
