using System;

namespace CashBackApp.Domain.Entities
{
    public class OrderItem : EntityBase
    {
        public OrderItem()
        {
            Id = Guid.NewGuid();
        }

        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid? CashbackSettingsId { get; set; }

        public double Value { get; set; }
        public int Quantity { get; set; }
        public double Total { get { return Value * Quantity; } }
        public double CashbackValue { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual CashbackSettings CashbackSettings { get; set; }

        public override bool IsValid()
        {
            if (ProductId == Guid.Empty)
                throw new ArgumentException("The property ProductId is not valid");

            if (Quantity <= 0)
                throw new ArgumentException("The property Quantity must be greater than 0");

            if (Total <= 0)
                throw new ArgumentException("The property Total must be greater than 0");

            if (CashbackValue < 0)
                throw new ArgumentException("The property CashbackTotal must be postive values");

            return true;
        }
    }
}
