using System;
using System.Collections.Generic;
using System.Linq;

namespace CashBackApp.Domain.Entities
{
    public class Order : EntityBase
    {
        public Order()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now.Date;
            OrderItems = new List<OrderItem>();
        }

        public Guid CompanyId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime Date { get; private set; }
        public double Total { get; set; } 
        public double CashbackTotal { get; set; }

        public virtual Company Company { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get;set;}

        public override bool IsValid()
        {
            if (CompanyId == Guid.Empty)
                throw new ArgumentException("The property CompanyId is not valid");

            if (CustomerId == Guid.Empty)
                throw new ArgumentException("The property CustomerId is not valid");

            if (Date == DateTime.MinValue)
                throw new ArgumentException("The property Date is not valid");

            if (Total <= 0)
                throw new ArgumentException("The property Total must be greater than 0");

            if (CashbackTotal < 0)
                throw new ArgumentException("The property CashbackTotal must be postive values");

            if (OrderItems.Count == 0)
                throw new ArgumentException("The property OrderItems must be items");

            return true;
        }
    }
}
