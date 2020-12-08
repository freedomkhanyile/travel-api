using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Travel.Data.Access.Helpers;

namespace Travel.Data.Access.DAL
{
    public class MainDbContext: DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var mappings = MappingsHelper.GetMainMappings();

            foreach (var mapping in mappings)
            {
                mapping.Visit(modelBuilder);
            }

            modelBuilder.SeedRoleData();
            modelBuilder.SeedUserData();
            modelBuilder.SeedUserRoleData();
        }
    }
}
