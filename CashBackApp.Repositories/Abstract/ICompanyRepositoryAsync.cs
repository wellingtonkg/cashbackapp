using CashBackApp.Domain.Entities;
using System.Collections.Generic;

namespace CashBackApp.Repositories.Abstract
{
    public interface ICompanyRepositoryAsync : IEntityBaseRepositoryAsync<Company>
    {
        Company GetFirstCompany();
    }
}
