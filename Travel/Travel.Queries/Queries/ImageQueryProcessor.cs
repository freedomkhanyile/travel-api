using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Api.Common.Exceptions;
using Travel.Api.Models.Image;
using Travel.Data.Access.DAL;
using Travel.Data.Model.Entities;
using Travel.Security;

namespace Travel.Queries.Queries
{
    public class ImageQueryProcessor : IImageQueryProcessor
    {
        private readonly IUnitOfWork _uniOfWork;

        public ImageQueryProcessor(IUnitOfWork uniOfWork)
        {
            _uniOfWork = uniOfWork;
        }

        public IQueryable<CategoryImageEntity> Get()
        {
            return GetQuery();
        }

        private IQueryable<CategoryImageEntity> GetQuery()
        {
            var q = _uniOfWork.Query<CategoryImageEntity>()
                .Where(b => !b.IsDeleted);
            return q;
        }

        public CategoryImageEntity Get(Guid id)
        {
            var image = GetQuery().FirstOrDefault(b => b.Id == id);
            if(image == null)
                throw new NotFoundException("image not found");
            return image;
        }

        public async Task<CategoryImageEntity> Create(CatergoryImageModel model)
        {
            var image = new CategoryImageEntity
            {
                Id = Guid.NewGuid(),
                Url = model.Url,
                CategoryEntityId = model.CategoryEntityId,
                CreateDate = DateTime.UtcNow.ToLocalTime(),
                CreateUserId = model.CreateUserId,
                ModifyDate = DateTime.UtcNow.ToLocalTime(),
                ModifyUserId = model.ModifyUserId,
                IsDeleted = false,
                StatusId= 1
            };
            _uniOfWork.Add(image);
            await _uniOfWork.CommitAsync();
            return image;
        }

        public async Task<CategoryImageEntity> Update(Guid id, CatergoryImageModel model)
        {
            var image = GetQuery().FirstOrDefault(b => b.Id == id);
            if (image == null)
                throw new NotFoundException("image not found");
   
             
            await _uniOfWork.CommitAsync();
            return image;
        }

        public async Task Delete(Guid id)
        {
            var image = GetQuery().FirstOrDefault(b => b.Id == id);
            if (image == null)
                throw new NotFoundException("image not found");
            if (image.IsDeleted) return;

            image.IsDeleted = true;
            await _uniOfWork.CommitAsync();
        }
    }
}
