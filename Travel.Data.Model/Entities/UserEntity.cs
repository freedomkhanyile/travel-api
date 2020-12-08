using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Travel.Data.Model.Entities
{
     public class UserEntity : AuditEntity<Guid>
    {
        public UserEntity()
        {
            Roles = new List<UserRoleEntity>();
        }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public string UserToken { get; set; }
        public virtual IList<UserRoleEntity> Roles { get; set; }
    }
}
