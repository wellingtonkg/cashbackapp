using CashBackApp.Domain.Entities;
using CashBackApp.Repositories.Abstract;
using CashBackApp.Services.Abstract;

namespace CashBackApp.Services.Services
{
    public class CashbackSettingsServiceAsync : EntityBaseServiceAsync<CashbackSettings>, ICashbackSettingsServiceAsync
    {
        private IEntityBaseRepositoryAsync<CashbackSettings> _repostory;

        public CashbackSettingsServiceAsync(IEntityBaseRepositoryAsync<CashbackSettings> repostory) : base(repostory)
        {
            _repostory = repostory;
        }
    }
}
