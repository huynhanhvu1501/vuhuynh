﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Context;

namespace Webbanhang.Controllers
{
    public class ProductController : Controller
    {
        QLWEBEntities3 objQLWEBEntities3 = new QLWEBEntities3();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objQLWEBEntities3.Products.Where(n=>n.Id==Id).FirstOrDefault();
            return View(objProduct);  
        }
    }
}