using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Contracts
{
    public interface IAuditEntity
    {

        string CreateUserId { get; set; }
        DateTime CreateDate { get; set; }
        string ModifyUserId { get; set; }
        DateTime ModifyDate { get; set; }
        int StatusId { get; set; }
    }
}
