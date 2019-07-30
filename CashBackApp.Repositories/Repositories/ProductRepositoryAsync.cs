using System.Collections.Generic;
using System.Linq;
using CashBackApp.Database.Context;
using CashBackApp.Domain.Entities;
using CashBackApp.Domain.Enums;
using CashBackApp.Repositories.Abstract;

namespace CashBackApp.Repositories.Repositories
{
    public class ProductRepositoryAsync : EntityBaseRepositoryAsync<Product>, IProductRepositoryAsync
    {
        private Context _context;

        public ProductRepositoryAsync(Context context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of products (disks)
        /// </summary>
        /// <param name="genre">Genre of Disk</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="page">Current Page</param>
        /// <param name="recordCount">Total of records</param>
        /// <returns>List of disks</returns>
        public List<Product> GetProductsByGenre(GenreEnum genre, int pageSize, int page, out int recordCount)
        {
            var query = _context.Set<Product>().Where(p => p.Genre == genre);

            //total of records
            recordCount = query.Count();

            //paginate result
            return query.OrderBy(o => o.Name)
                        .Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToList();
        }
    }
}
