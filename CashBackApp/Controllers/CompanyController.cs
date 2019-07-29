using CashBackApp.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CashBackApp.Api.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : BaseController
    {
        private readonly ICompanyServiceAsync _companyServiceAsync;

        public CompanyController(ICompanyServiceAsync companyServiceAsync)
        {
            _companyServiceAsync = companyServiceAsync;
        }

        /// <summary>
        /// Get a list of companies
        /// </summary>
        /// <param name="actualPage">current Page</param>
        /// <param name="pageSize">page size</param>
        /// <returns>Get a list of all companies</returns>
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var query = await _companyServiceAsync.GetAllAsync();

            // TODO: Mapear para DTO
            return Ok(query);
        }
    }
}
