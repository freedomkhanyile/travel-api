using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Travel.Api.Models.Users
{
    public class UpdateUserModel : BaseModel.BaseModel
    {
        public UpdateUserModel()
        {
            Roles = new string[0];
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
