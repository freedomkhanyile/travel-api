using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Travel.Api.Models.Users
{
    public class ForgotPasswordModel
    {
         public string Email { get; set; }
         public string Token { get; set; }
    }
}
