using System;
using System.Collections.Generic;
using System.Text;
using Travel.Api.Models.Communication;

namespace Travel.Communication.Email
{
    public interface IEmailService
    {
        bool SendEmail(EmailModel model);
    }
}
