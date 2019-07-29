using System;

namespace CashBackApp.Domain.Entities
{
    public class Company : EntityBase
    {
        public string Name { get; set; }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentException("The property NAME is not valid");

            return true;
        }
    }
}
