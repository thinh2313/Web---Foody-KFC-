using WebBanThucAnNhanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace WebBanThucAnNhanh.Controllers
{
    public class LoginUserController : Controller
    {
        QLThucAnNhanhEntities database = new QLThucAnNhanhEntities();
        // GET: LoginUser
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAccount(KHACHHANG _user)
        {

            var check = database.KHACHHANGs.Where(s => s.EMAIL == _user.EMAIL && s.PASSWORD == _user.PASSWORD).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Sai info";
                return View("Login");
            }
            else
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["EMAIL"] = _user.EMAIL;
                Session["PASSWORD"] = _user.PASSWORD;
                Session["MaKH"] = check.MAKHACHHANG;
                int a = (int)Session["MaKH"];
                return RedirectToAction("Index", "Home");
            }
            //return View();
        }
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(KHACHHANG _user)
        {
            if (ModelState.IsValid)
            {

                var check_ID = database.KHACHHANGs.Where(s => s.EMAIL == _user.EMAIL).FirstOrDefault();
                if (check_ID == null)
                {
                    database.Configuration.ValidateOnSaveEnabled = false;
                    database.KHACHHANGs.Add(_user);
                    database.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.ErrorRegister = "This ID is exist";
                    return View();
                }
            }
            return View();
        }
        public ActionResult EditAccount(int ID)
        {
            var detailUser = database.KHACHHANGs.Where(m => m.MAKHACHHANG == ID).FirstOrDefault();
            return View(detailUser);
        }

        [HttpPost]
        public ActionResult EditAccount(KHACHHANG khachhang)
        {
            var detail = database.KHACHHANGs.Where(m => m.MAKHACHHANG == khachhang.MAKHACHHANG);

            if (detail == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                database.Entry(khachhang).State = EntityState.Modified;
                database.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        public bool CheckExistAccount(KHACHHANG khachang)
        {
            var check = database.KHACHHANGs.Where(s => s.EMAIL == khachang.EMAIL && s.PASSWORD == khachang.PASSWORD).FirstOrDefault();
            if (check != null)
            {
                return true;
            }

            return false;
        }
        public ActionResult DonDat(int id)
        {
            var bill = database.HOADONs.Where(m => m.DATHANG.DIACHIGH.MAKHACHHANG == id).ToList();
            return View(bill);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index","Home");
        }
    }
}