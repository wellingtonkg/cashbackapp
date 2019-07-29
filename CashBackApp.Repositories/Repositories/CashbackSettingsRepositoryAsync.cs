using CashBackApp.Database.Context;
using CashBackApp.Domain.Entities;
using CashBackApp.Domain.Enums;
using CashBackApp.Repositories.Abstract;
using System;
using System.Linq;

namespace CashBackApp.Repositories.Repositories
{
    public class CashbackSettingsRepositoryAsync : EntityBaseRepositoryAsync<CashbackSettings>, ICashbackSettingsRepositoryAsync
    {
        private Context _context;

        public CashbackSettingsRepositoryAsync(Context context) : base(context)
        {
            _context = context;
        }

        public CashbackSettings GetCashbackByGenreAndDay(Guid companyId, GenreEnum genre, DayOfWeek dayOfWeek)
        {
            var result = FindBy(p =>
                p.Active &&
                p.CompanyId == companyId &&
                p.DayOfWeek == dayOfWeek &&
                p.Genre == genre).FirstOrDefault();

            return result;
        }
    }
}
