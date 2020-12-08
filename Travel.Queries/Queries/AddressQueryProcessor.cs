using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Api.Common.Exceptions;
using Travel.Api.Models.Address;
using Travel.Data.Access.DAL;
using Travel.Data.Model.Entities;
using Travel.Security;

namespace Travel.Queries.Queries
{
    public class AddressQueryProcessor : IAddressQueryProcessor
    {
        private readonly IUnitOfWork _uniOfWork;
        private readonly ISecurityContext _securityContext;

        public AddressQueryProcessor(IUnitOfWork uniOfWork, ISecurityContext securityContext)
        {
            _uniOfWork = uniOfWork;
            _securityContext = securityContext;
        }

        public IQueryable<AddressEntity> Get()
        {
            return GetQuery();
        }

        private IQueryable<AddressEntity> GetQuery()
        {
            var q = _uniOfWork.Query<AddressEntity>()
                .Where(b => !b.IsDeleted);
            if (_securityContext.IsAdministrator || _securityContext.IsStaff) return q;
            var userId = _securityContext.UserEntity.Id.ToString();
            return q;
        }

        public AddressEntity Get(Guid id)
        {
            var address = GetQuery().FirstOrDefault(b => b.Id == id);
            if(address == null)
                throw new NotFoundException("address not found");
            return address;
        }

        public async Task<AddressEntity> Create(AddressModel model)
        {
            var address = new AddressEntity
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                AddressLine3 = model.AddressLine3,
                City = model.City,
                Province = model.Province,
                PostalCode = model.PostalCode,
                CreateDate = DateTime.UtcNow.ToLocalTime(),
                CreateUserId = _securityContext.UserEntity.Id.ToString(),
                ModifyDate = DateTime.UtcNow.ToLocalTime(),
                ModifyUserId = _securityContext.UserEntity.Id.ToString(),
                IsDeleted = false,
                StatusId= 1
            };
            _uniOfWork.Add(address);
            await _uniOfWork.CommitAsync();
            return address;
        }

        public async Task<AddressEntity> Update(Guid id, AddressModel model)
        {
            var address = GetQuery().FirstOrDefault(b => b.Id == id);
            if (address == null)
                throw new NotFoundException("address not found");
   
             
            await _uniOfWork.CommitAsync();
            return address;
        }

        public async Task Delete(Guid id)
        {
            var address = GetQuery().FirstOrDefault(b => b.Id == id);
            if (address == null)
                throw new NotFoundException("address not found");
            if (address.IsDeleted) return;

            address.IsDeleted = true;
            await _uniOfWork.CommitAsync();
        }
    }
}
