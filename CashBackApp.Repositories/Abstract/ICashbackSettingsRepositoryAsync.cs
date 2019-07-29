using CashBackApp.Domain.Entities;
using CashBackApp.Domain.Enums;
using System;

namespace CashBackApp.Repositories.Abstract
{
    public interface ICashbackSettingsRepositoryAsync : IEntityBaseRepositoryAsync<CashbackSettings>
    {
        CashbackSettings GetCashbackByGenreAndDay(Guid companyId, GenreEnum genre, DayOfWeek dayOfWeek);
    }
}
