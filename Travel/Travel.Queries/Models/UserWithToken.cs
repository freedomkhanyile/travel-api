using System;
using System.Collections.Generic;
using System.Text;
using Travel.Data.Model.Entities;

namespace Travel.Queries.Models
{
    public class UserWithToken
    {
        public string Token { get; set; }
        public UserEntity User { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
