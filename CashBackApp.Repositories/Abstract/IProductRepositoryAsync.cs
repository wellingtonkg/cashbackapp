using CashBackApp.Domain.Entities;
using CashBackApp.Domain.Enums;
using System.Collections.Generic;

namespace CashBackApp.Repositories.Abstract
{
    public interface IProductRepositoryAsync : IEntityBaseRepositoryAsync<Product>
    {
        List<Product> GetProductsByGenre(GenreEnum genre, int pageSize, int page, out int recordCount);
    }
}
