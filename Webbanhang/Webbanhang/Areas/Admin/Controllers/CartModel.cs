using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webbanhang.Context;

namespace Webbanhang.Areas.Admin.Controllers
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}