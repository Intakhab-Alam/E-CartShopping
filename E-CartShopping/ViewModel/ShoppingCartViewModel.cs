﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CartShopping.ViewModel
{
    public class ShoppingCartViewModel
    {
        public string ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public string ImagePath { get; set; }
        public string ItemName { get; set; }

    }
}