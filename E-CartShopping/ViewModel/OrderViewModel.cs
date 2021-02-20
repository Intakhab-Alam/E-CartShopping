using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CartShopping.ViewModel
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
    }
}