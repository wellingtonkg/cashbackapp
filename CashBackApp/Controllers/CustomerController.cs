using CashBackApp.Api.Core;
using CashBackApp.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CashBackApp.Api.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly ICustomerServiceAsync _customerServiceAsync;

        public CustomerController(ICustomerServiceAsync customerServiceAsync)
        {
            _customerServiceAsync = customerServiceAsync;
        }

        /// <summary>
        /// Get a list of customers
        /// </summary>
        /// <param name="actualPage">current Page</param>
        /// <param name="pageSize">page size</param>
        /// <returns>Get a list of all customers</returns>
        [HttpGet()]
        public async Task<IActionResult> Get(string search, int pageSize = 10, int page = 1)
        {
            int recordCount = 0;
            var query = _customerServiceAsync.GetCustomers(search, pageSize, page, out recordCount);
            Response.AddPagination(page, pageSize, recordCount);

            // TODO: Mapear para DTO
            return Ok(query);
        }
    }
}
