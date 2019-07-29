using System;
using System.ComponentModel.DataAnnotations;

namespace CashBackApp.Api.ViewModels
{
    public class OrderItemViewModel
    {
        [Required]
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
