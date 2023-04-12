using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Context;
using Webbanhang.Models;
namespace Webbanhang.Controllers
{
    public class HomeController : Controller
    {
        ASPQLWEntities1 objQLWEBEntities3 = new ASPQLWEntities1();
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListCategory = objQLWEBEntities3.Categories.ToList();

            objHomeModel.ListProduct = objQLWEBEntities3.Products.ToList();
            return View(objHomeModel);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
    [ValidateAntiForgeryToken]
        public ActionResult Register (User _user)
        {
            if(ModelState.IsValid)
            {
                var check = objQLWEBEntities3.Users.FirstOrDefault(s => s.Email == _user.Email);
                if(check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    objQLWEBEntities3.Configuration.ValidateOnSaveEnabled = false;
                    objQLWEBEntities3.Users.Add(_user);
                    objQLWEBEntities3.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại";
                    return View();
                }
            }
            return  View();
        }
        //dang nhap
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //Kiem tra va luu vao db

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Email, string Password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(Password);
                var data = objQLWEBEntities3.Users.Where(s => s.Email.Equals(Email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FistName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Đăng nhập thất bại";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Trang mô tả ứng dụng của bạn.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Trang liên hệ của bạn.";

            return View();
        }
    }
}