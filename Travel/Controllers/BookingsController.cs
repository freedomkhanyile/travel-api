using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Api.Models.Bookings;
using Travel.Data.Model.Entities;
using Travel.Filters;
using Travel.Maps;
using Travel.Queries.Queries;

namespace Travel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        public BookingsController(IBookingsQueryProcessor bookingsQuery, IAutoMapper mapper)
        {
            _bookingsQuery = bookingsQuery;
            _mapper = mapper;
        }
        private readonly IBookingsQueryProcessor _bookingsQuery;
        private readonly IAutoMapper _mapper;

        [HttpGet]
        [QueryableResult]

        public IQueryable<BookingModel> GetAll()
        {
            var bookings = _bookingsQuery.Get();
            var resultModel = _mapper.Map<BookingEntity, BookingModel>(bookings);
            return resultModel;
        }

        [HttpGet("{id}")]
        public BookingModel Get(Guid id)
        {
            var booking = _bookingsQuery.Get(id);
            var resultModel = _mapper.Map<BookingModel>(booking);
            return resultModel;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<BookingModel> PostAsync([FromBody] CreateBookingModel model)
        {
            var item = await _bookingsQuery.Create(model);
            var resultModel = _mapper.Map<BookingModel>(item);
            return resultModel;
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<BookingModel> PutAsync(Guid id, [FromBody] UpdateBookingModel model)
        {
            var item = await _bookingsQuery.Update(id, model);
            var resultModel = _mapper.Map<BookingModel>(item);
            return resultModel;
        }

        [HttpDelete("{id}")]
         public async Task DeleteAsync(Guid id)
        {
            await _bookingsQuery.Delete(id);
        }

    }
}