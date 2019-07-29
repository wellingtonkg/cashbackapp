using CashBackApp.Domain.Entities;
using CashBackApp.Repositories.Abstract;
using CashBackApp.Services.Abstract;

namespace CashBackApp.Services.Services
{
    public class CompanyServiceAsync : EntityBaseServiceAsync<Company>, ICompanyServiceAsync
    {
        private ICompanyRepositoryAsync _repostory;

        public CompanyServiceAsync(ICompanyRepositoryAsync repostory) : base(repostory)
        {
            _repostory = repostory;
        }

        public Company GetFirstCompany()
        {
            return _repostory.GetFirstCompany();
        }
    }
}
