using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Api.Common.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) : base(message)
        {
        }
    }
}
