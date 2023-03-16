using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Context;

namespace Webbanhang.Controllers
{
    public class CategoryController : Controller
    {
        QLWEBEntities3 objQLWEBEntities3 = new QLWEBEntities3();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objQLWEBEntities3.Categories.ToList();
            return View(lstCategory);
        }
    }
}