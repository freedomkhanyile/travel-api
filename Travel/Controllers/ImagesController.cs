using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Travel.Api.Models.Image;
using Travel.Data.Model.Entities;
using Travel.Filters;
using Travel.Maps;
using Travel.Queries.Queries;

namespace Travel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
    
        private readonly IImageQueryProcessor _imageQueryProcessor;
        private readonly IAutoMapper _mapper;
        public ImagesController(IImageQueryProcessor addressQueryProcessor, IAutoMapper mapper)
        {
            _imageQueryProcessor = addressQueryProcessor;
            _mapper = mapper;
        }


        [HttpGet]
        [QueryableResult]

        public IQueryable<CatergoryImageModel> GetAll()
        {
            var items = _imageQueryProcessor.Get();
            var resultModel = _mapper.Map<CategoryImageEntity, CatergoryImageModel>(items);
            return resultModel;
        }


        [HttpGet("{id}")]
        public CatergoryImageModel Get(Guid id)
        {
            var category = _imageQueryProcessor.Get(id);
            var resultModel = _mapper.Map<CatergoryImageModel>(category);
            return resultModel;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<CatergoryImageModel> PostAsync([FromBody] CatergoryImageModel model)
        {
            var item = await _imageQueryProcessor.Create(model);
            var resultModel = _mapper.Map<CatergoryImageModel>(item);
            return resultModel;
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<CatergoryImageModel> PutAsync(Guid id, [FromBody] CatergoryImageModel model)
        {
            var item = await _imageQueryProcessor.Update(id, model);
            var resultModel = _mapper.Map<CatergoryImageModel>(item);
            return resultModel;
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _imageQueryProcessor.Delete(id);
        }
    }
}
