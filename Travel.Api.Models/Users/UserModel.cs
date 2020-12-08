using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Api.Models.Users
{
    public class UserModel: BaseModel.BaseModel
    {
        public UserModel()
        {
            Roles = new string[0];
        }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string UserToken { get; set; }
        public string[] Roles { get; set; }
    }
}
