using System;
using System.Collections.Generic;
using System.Text;
using Travel.Contracts;

namespace Travel.Data.Model.Entities
{
    public abstract class BaseEntity
    {
    }

    public abstract class Entity<T> : BaseEntity, IEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
