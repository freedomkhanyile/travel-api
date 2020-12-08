using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Travel.Data.Access.Maps.Common;
using Travel.Data.Model.Entities;

namespace Travel.Data.Access.Maps.Main
{
    public class CatergoryImageMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<CategoryImageEntity> ()
                .ToTable("CatergoryImages")
                .HasKey(x => x.Id);
        }
    }
}
