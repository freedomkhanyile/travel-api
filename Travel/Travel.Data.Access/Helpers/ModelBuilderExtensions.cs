using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Travel.Data.Access.Constants;
using Travel.Data.Model.Entities;

namespace Travel.Data.Access.Helpers
{
    public static class ModelBuilderExtensions
    {
        public static void SeedRoleData(this ModelBuilder builder)
        {
            builder.Entity<RoleEntity>().HasData(
                
                new RoleEntity
                {
                    Id = 1,
                    RoleName = RoleConstants.Admin,
                     CreateDate = DateTime.Now,
                     CreateUserId = "sys",
                     ModifyDate = DateTime.Now,
                     ModifyUserId = "sys",
                     StatusId = 1
                }, new RoleEntity
                {
                    Id = 2,
                    RoleName = RoleConstants.Staff,
                     CreateDate = DateTime.Now,
                     CreateUserId = "sys",
                     ModifyDate = DateTime.Now,
                     ModifyUserId = "sys",
                     StatusId = 1
                }, 
                new RoleEntity
                {

                    Id = 3,
                    RoleName = RoleConstants.Traveler,
                     CreateDate = DateTime.Now,
                     CreateUserId = "sys",
                     ModifyDate = DateTime.Now,
                     ModifyUserId = "sys",
                     StatusId = 1
                },  
                new RoleEntity
                {

                    Id = 4,
                    RoleName = RoleConstants.Supplier,
                     CreateDate = DateTime.Now,
                     CreateUserId = "sys",
                     ModifyDate = DateTime.Now,
                     ModifyUserId = "sys",
                     StatusId = 1
                });
        }

        public static void SeedUserData(this ModelBuilder builder)
        {
            var defaultPassword = "12345";
            builder.Entity<UserEntity>().HasData(new UserEntity
            {
                Id = Guid.Parse("c6e641f7-ce8d-4de5-aef7-bc4237251335"),
                Email = "admin@mail.com",
                FirstName = "john",
                Surname = "Doe",
                Password = defaultPassword.WithBCrypt(),
                CreateDate = DateTime.Now,
                CreateUserId = "sys",
                ModifyDate = DateTime.Now,
                ModifyUserId = "sys",
                StatusId = 1
            });
        }

        public static void SeedUserRoleData(this ModelBuilder builder)
        {
            builder.Entity<UserRoleEntity>().HasData(
                new UserRoleEntity
                {
                    Id = 1,
                    RoleEntityId = 1,
                    UserEntityId = Guid.Parse("c6e641f7-ce8d-4de5-aef7-bc4237251335"),
                    CreateDate = DateTime.Now,
                    CreateUserId = "sys",
                    ModifyDate = DateTime.Now,
                    ModifyUserId = "sys",
                    StatusId = 1
                }
            );
        }
    }
}
