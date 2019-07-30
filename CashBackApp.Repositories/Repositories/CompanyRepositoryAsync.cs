using System;
using System.Collections.Generic;
using System.Linq;
using CashBackApp.Database.Context;
using CashBackApp.Domain.Entities;
using CashBackApp.Domain.Enums;
using CashBackApp.Repositories.Abstract;

namespace CashBackApp.Repositories.Repositories
{
    public class CompanyRepositoryAsync : EntityBaseRepositoryAsync<Company>, ICompanyRepositoryAsync
    {
        private Context _context;

        public CompanyRepositoryAsync(Context context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the first company of database
        /// </summary>
        /// <returns>Company</returns>
        public Company GetFirstCompany()
        {
            return _context.Set<Company>().FirstOrDefault();
        }
    }
}
