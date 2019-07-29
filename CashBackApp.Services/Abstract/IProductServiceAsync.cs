using CashBackApp.Domain.Entities;
using CashBackApp.Domain.Enums;
using System.Collections.Generic;

namespace CashBackApp.Services.Abstract
{
    public interface IProductServiceAsync : IEntityBaseServiceAsync<Product>
    {
        List<Product> GetProductsByGenre(GenreEnum genre, int pageSize, int page, out int recordCount);
    }
}
