using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Travel.Data.Access.Maps.Common;
using Travel.Data.Model.Entities;

namespace Travel.Data.Access.Maps.Main
{
    public class StatusMap: IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<StatusEntity>()
                .ToTable("Statuses")
                .HasKey(x => x.Id);
        }
    }
}
