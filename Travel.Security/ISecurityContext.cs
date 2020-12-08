using System;
using System.Collections.Generic;
using System.Text;
using Travel.Data.Model.Entities;

namespace Travel.Security
{
    public interface ISecurityContext
    {
        UserEntity UserEntity { get; }

        bool IsAdministrator { get; }
        bool IsStaff { get; }
    }
}
