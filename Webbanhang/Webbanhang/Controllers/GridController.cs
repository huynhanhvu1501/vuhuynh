using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Context;

namespace Webbanhang.Controllers
{
    public class GridController : Controller
    {
        ASPQLWEntities1 objQLWEBEntities3 = new ASPQLWEntities1();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objQLWEBEntities3.Products.ToList();
            return View(lstCategory);
        }
        public ActionResult Listing(int Id)
        {
            var listProduct = objQLWEBEntities3.Products.Where(n => n.CategoryId == 1).ToList();
            return View(listProduct);
        }
    }
}