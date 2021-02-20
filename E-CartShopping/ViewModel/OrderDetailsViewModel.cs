using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CartShopping.ViewModel
{
    public class OrderDetailsViewModel
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public string ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}