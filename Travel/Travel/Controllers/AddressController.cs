using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Travel.Api.Models.Address;
using Travel.Data.Model.Entities;
using Travel.Filters;
using Travel.Maps;
using Travel.Queries.Queries;

namespace Travel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressQueryProcessor _addressQueryProcessor;
        private readonly IAutoMapper _mapper;
        public AddressController(IAddressQueryProcessor addressQueryProcessor, IAutoMapper mapper)
        {
            _addressQueryProcessor = addressQueryProcessor;
            _mapper = mapper;
        }


        [HttpGet]
        [QueryableResult]

        public IQueryable<AddressModel> GetAll()
        {
            var items = _addressQueryProcessor.Get();
            var resultModel = _mapper.Map<AddressEntity, AddressModel>(items);
            return resultModel;
        }


        [HttpGet("{id}")]
        public AddressModel Get(Guid id)
        {
            var category = _addressQueryProcessor.Get(id);
            var resultModel = _mapper.Map<AddressModel>(category);
            return resultModel;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<AddressModel> PostAsync([FromBody] AddressModel model)
        {
            var item = await _addressQueryProcessor.Create(model);
            var resultModel = _mapper.Map<AddressModel>(item);
            return resultModel;
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<AddressModel> PutAsync(Guid id, [FromBody] AddressModel model)
        {
            var item = await _addressQueryProcessor.Update(id, model);
            var resultModel = _mapper.Map<AddressModel>(item);
            return resultModel;
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _addressQueryProcessor.Delete(id);
        }
    }
}
