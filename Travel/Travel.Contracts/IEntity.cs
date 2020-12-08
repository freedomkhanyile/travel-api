using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Contracts
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
