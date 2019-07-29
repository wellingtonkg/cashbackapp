using CashBackApp.Api.Core;
using CashBackApp.Api.ViewModels;
using CashBackApp.Domain.Entities;
using CashBackApp.Domain.Enums;
using CashBackApp.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CashBackApp.Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServiceAsync _orderServiceAsync;
        private readonly ICompanyServiceAsync _companyServiceAsync;

        public OrderController(
            IOrderServiceAsync orderServiceAsync,
            ICompanyServiceAsync companyServiceAsync)
        {
            _orderServiceAsync = orderServiceAsync;
            _companyServiceAsync = companyServiceAsync;
        }


        /// <summary>
        /// Get an order by Id
        /// </summary>
        /// <param name="id">order Id</param>
        /// <returns>Order</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            Order Order = await _orderServiceAsync.GetSingleAsync(id, 
                i => i.Company, 
                i => i.Customer,
                i => i.OrderItems);

            if (Order == null)
                return NotFound();


            return Ok(Order);
        }

        /// <summary>
        /// Get a list of orders by a range of dates and pagination
        /// </summary>
        /// <param name="startDate">Start Date dd/MM/yyyy</param>
        /// <param name="endDate">End Date dd/MM/yyyy</param>
        /// <param name="pageSize">page size</param>
        /// <param name="page">current page</param>
        /// <returns>List of orders</returns>
        [HttpGet()]
        public async Task<IActionResult> Get(string startDateStr, string endDateStr, int pageSize = 10, int page = 1)
        {
            DateTime startDate;
            DateTime endDate;

            if (!DateTime.TryParse(startDateStr, out startDate))
                throw new InvalidCastException("Incorret format startDate");

            if (!DateTime.TryParse(endDateStr, out endDate))
                throw new InvalidCastException("Incorret format endDate");

            int recordCount = 0;
            var query = _orderServiceAsync.GetOrders(startDate, endDate, pageSize, page, out recordCount);
            Response.AddPagination(page, pageSize, recordCount);

            // TODO: Mapear para DTO
            return Ok(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cart">Cart information, is not necessary to fill the company</param>
        /// <returns>Order Registred</returns>
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody]OrderViewModel cart)
        {
            if (cart == null)
                return NotFound();

            //Get a company to make the relation
            Company company = (!string.IsNullOrEmpty(cart.CompanyId)) ? await _companyServiceAsync.GetSingleAsync(Guid.Parse(cart.CompanyId)) : _companyServiceAsync.GetFirstCompany();

            if (company == null)
                return NotFound("Company not found");

            //Create the object order
            var order = new Order
            {
                CompanyId = company.Id,
                CustomerId = cart.CustomerId,
            };

            //Create the object orderItems
            foreach (var item in cart.OrderItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
            }

            //Save 
            await _orderServiceAsync.AddAsync(order);
            await _orderServiceAsync.CommitAsync();

            return Ok(order);
        }
    }
}
