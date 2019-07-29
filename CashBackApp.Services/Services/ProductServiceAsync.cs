using System.Collections.Generic;
using CashBackApp.Domain.Entities;
using CashBackApp.Domain.Enums;
using CashBackApp.Repositories.Abstract;
using CashBackApp.Services.Abstract;

namespace CashBackApp.Services.Services
{
    public class ProductServiceAsync : EntityBaseServiceAsync<Product>, IProductServiceAsync
    {
        private IProductRepositoryAsync _repostory;

        public ProductServiceAsync(IProductRepositoryAsync repostory) : base(repostory)
        {
            _repostory = repostory;
        }

        public List<Product> GetProductsByGenre(GenreEnum genre, int pageSize, int page, out int recordCount)
        {
            return _repostory.GetProductsByGenre(genre, pageSize, page, out recordCount);
        }
    }
}
