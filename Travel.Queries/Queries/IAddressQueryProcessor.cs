using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Api.Models.Address;
using Travel.Api.Models.Category;
using Travel.Data.Model.Entities;

namespace Travel.Queries.Queries
{
    public interface IAddressQueryProcessor
    {
        IQueryable<AddressEntity> Get();
        AddressEntity Get(Guid id);
        Task<AddressEntity> Create(AddressModel model);
        Task<AddressEntity> Update(Guid id, AddressModel model);
        Task Delete(Guid id);
    }
}
