using System;
using CashBackApp.Domain.Interfaces;

namespace CashBackApp.Domain.Entities
{
    public abstract class EntityBase : IEntityBase
    {
        public Guid Id { get; set; }

        public abstract bool IsValid();
    }
}
