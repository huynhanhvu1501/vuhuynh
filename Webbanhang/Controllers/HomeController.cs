using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Context;
using Webbanhang.Models;
namespace Webbanhang.Controllers
{
    public class HomeController : Controller
    {
        QLWEBEntities3 objQLWentities = new QLWEBEntities3();
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListCategory = objQLWentities.Categories.ToList();

            objHomeModel.ListProduct = objQLWentities.Products.ToList();
            return View(objHomeModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}