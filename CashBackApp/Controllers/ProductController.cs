using CashBackApp.Api.Core;
using CashBackApp.Domain.Entities;
using CashBackApp.Domain.Enums;
using CashBackApp.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CashBackApp.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServiceAsync _productServiceAsync;

        public ProductController(IProductServiceAsync productServiceAsync)
        {
            _productServiceAsync = productServiceAsync;
        }


        /// <summary>
        /// Get a disk by id
        /// </summary>
        /// <param name="id">ID of disk</param>
        /// <returns>Product</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            Product product = await _productServiceAsync.GetSingleAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        /// <summary>
        /// Get a list of disks by genre
        /// </summary>
        /// <param name="genre">genre</param>
        /// <param name="actualPage">Current atual</param>
        /// <param name="pageSize">Total by Page</param>
        /// <returns>Products</returns>
        [HttpGet()]
        public async Task<IActionResult> GetProductsByGenre(GenreEnum genre, int pageSize = 10, int page = 1)
        {
            int recordCount = 0;
            var list = _productServiceAsync.GetProductsByGenre(genre, pageSize, page, out recordCount);
            Response.AddPagination(page, pageSize, recordCount);

            return Ok(list);
        }        
    }
}
