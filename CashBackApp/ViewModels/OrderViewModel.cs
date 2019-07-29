using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CashBackApp.Api.ViewModels
{
    public class OrderViewModel
    {
        public string CompanyId { get; set; } = "";

        [Required]
        public Guid CustomerId { get; set; }

        public virtual List<OrderItemViewModel> OrderItems { get; set; }
}
}
