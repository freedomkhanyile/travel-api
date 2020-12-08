using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Travel.Api.Models.Communication
{
    public class EmailModel
    {
        public string FromEmail { get; set; }
        public string Alias { get; set; }
        [Required]
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        [Required]
        public string Subject { get; set; }
        public string Message { get; set; }

        // multiple receivers and senders for a single server call implementation.
        public List<string> FromEmailList { get; set; }
        public List<string> ToEmailList { get; set; }
    }
}
