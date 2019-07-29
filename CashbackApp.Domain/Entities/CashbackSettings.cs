using CashBackApp.Domain.Enums;
using System;

namespace CashBackApp.Domain.Entities
{
    public class CashbackSettings : EntityBase
    {
        public Guid CompanyId { get; set; }
        public bool Active { get; set; } = true;
        public GenreEnum Genre { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public double Percentage { get; set; }

        public CashbackSettings()
        {

        }

        public override bool IsValid()
        {
            if(CompanyId == Guid.Empty)
                throw new ArgumentException("The property CompanyId is not valid");

            if (Percentage < 0 || Percentage > 100)
                throw new ArgumentException(Percentage.GetType().ToString());

            return true;
        }
    }
}
