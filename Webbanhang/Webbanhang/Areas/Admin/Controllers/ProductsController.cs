using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Context;

namespace Webbanhang.Areas.Admin.Controllers
{
   
    public class ProductsController : Controller
    {

        // GET: Admin/Product
        ASPQLWEntities1 objQLWEBEntities3 = new ASPQLWEntities1();
        // GET: Admin/ProductAD
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {

            var lstProduct = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstProduct = objQLWEBEntities3.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = objQLWEBEntities3.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {

            this.LoadData();
            return View();
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product objProduct)
        {
             this.LoadData();

            if (ModelState.IsValid)
            {
                try
                {
                    if (objProduct.ImageUpload != null)
                    {
                        //tinhsang.png
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                        //tenhinh
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                        //png
                        fileName = fileName + extension;
                        //tenhinh.png
                        objProduct.Avatar = fileName;
                        //lưu file hình
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                    }

                    objQLWEBEntities3.Products.Add(objProduct);
                    objQLWEBEntities3.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objQLWEBEntities3.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objQLWEBEntities3.Products.Where(n => n.Id == id).FirstOrDefault();

            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = objQLWEBEntities3.Products.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objQLWEBEntities3.Products.Remove(objProduct);
            objQLWEBEntities3.SaveChanges();
            return RedirectToAction("index");
        }
        //edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = objQLWEBEntities3.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Product objProduct)
        {
            if (ModelState.IsValid)
            {
                if (objProduct.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                    string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                    fileName = fileName + extension;
                    objProduct.Avatar = fileName;
                    objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                objProduct.UpdateOnUtc = DateTime.Now;
                objQLWEBEntities3.Entry(objProduct).State = EntityState.Modified;
                objQLWEBEntities3.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(objProduct);
        }
        void LoadData()
        {
            Common objCommon = new Common();
            //lây dữ liệu db

            var lstCat = objQLWEBEntities3.Categories.ToList();
            //conver sang select
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);

            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "id", "Name");
            //lấy dữ liệu thuowg hiêu

            var lstBrand = objQLWEBEntities3.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            //convert sang select

            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "id", "Name");

            List<ProductType> lstProductType = new List<ProductType>();

            ProductType objProductType = new ProductType();
            objProductType.Id = 1;
            objProductType.Name = "Giảm giá sốc";

            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 2;
            objProductType.Name = "Đề xuất";

            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);

            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "id", "Name");
        }
    }
}
    
