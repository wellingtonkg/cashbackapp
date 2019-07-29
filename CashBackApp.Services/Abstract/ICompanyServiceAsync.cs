using CashBackApp.Domain.Entities;

namespace CashBackApp.Services.Abstract
{
    public interface ICompanyServiceAsync : IEntityBaseServiceAsync<Company>
    {
        Company GetFirstCompany();
    }
}
