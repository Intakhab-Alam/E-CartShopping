using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CartShopping.ViewModel
{
    public class ShoppingViewModel
    {
        public System.Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal ItemPrice { get; set; }
        public string ImagePath { get; set; }
        public string Category { get; set; }
    }
}