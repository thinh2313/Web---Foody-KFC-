using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanThucAnNhanh.Models;

namespace WebBanThucAnNhanh.Areas.QuanLy.Controllers
{
    public class LoginQuanLyController : Controller
    {
        // GET: QuanLy/LoginQuanLy
        QLThucAnNhanhEntities database = new QLThucAnNhanhEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoginAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAccount(ADMIN _user)
        {
            //var user = database.ADMINs.Find(_user.Email);
            if (!CheckExistAccount(_user))
            {
                ViewBag.ErrorInfo = "Sai info";
                return View("Index");
            }

            database.Configuration.ValidateOnSaveEnabled = false;
            Session["Email"] = _user.Email;
            Session["ID"] = _user.ID;

            return RedirectToAction("Index", "Account");     
        }
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(ADMIN _user)
        {
            if (ModelState.IsValid)
            {

                var check_ID = database.ADMINs.Where(s => s.Email == _user.Email).FirstOrDefault();
                if (check_ID == null)
                {
                    database.Configuration.ValidateOnSaveEnabled = false;
                    database.ADMINs.Add(_user);
                    database.SaveChanges();
                    return RedirectToAction("Index","LoginQuanLY");
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
            var detailUser = database.ADMINs.Where(m => m.ID == ID).FirstOrDefault();
            return View(detailUser);
        }

        [HttpPost]
        public ActionResult EditAccount(ADMIN admin)
        {
            var detail = database.ADMINs.Where(m => m.ID == admin.ID);

            if (detail == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                database.Entry(admin).State = EntityState.Modified;
                database.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public bool CheckExistAccount(ADMIN _user)
        {
            var check = database.ADMINs.Where(s => s.Email == _user.Email && s.Pass == _user.Pass).FirstOrDefault();
            if (check != null)
            {
                return true;
            }

            return false;
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "LoginQuanLy");
        }
    }
}