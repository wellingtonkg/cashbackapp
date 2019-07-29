using CashBackApp.Domain.Enums;
using System;

namespace CashBackApp.Domain.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public GenreEnum Genre { get; set; }
        public double Price { get; set; }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentException("The property NAME is not valid");

            if (Price < 0)
                throw new ArgumentException("The property Price must be postive values");

            return true;
        }
    }
}
