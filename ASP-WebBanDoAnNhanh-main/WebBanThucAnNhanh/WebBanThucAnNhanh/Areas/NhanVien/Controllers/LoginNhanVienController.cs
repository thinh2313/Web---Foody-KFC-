using WebBanThucAnNhanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace WebBanThucAnNhanh.Areas.NhanVien.Controllers
{
    public class LoginUserController : Controller
    {
        QLThucAnNhanhEntities database = new QLThucAnNhanhEntities();
        // GET: LoginUser
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAccount(NHANVIEN _user)
        {
            var check = database.NHANVIENs.Where(s => s.EMAIL == _user.EMAIL && s.PASSWORD == _user.PASSWORD).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Sai info";
                return View("Index");
            }
            else
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["EMAIL"] = _user.EMAIL;
                Session["PASSWORD"] = _user.PASSWORD;
                Session["TENNV"] = _user.TENNV;
                Session["ID"] = check.MANHANVIEN;
                int a = (int)Session["ID"];
                return RedirectToAction("Index", "MonAn");
            }

        }
        public ActionResult EditAccount(int ID)
        {
            var detailUser = database.NHANVIENs.Where(m => m.MANHANVIEN == ID).FirstOrDefault();
            return View(detailUser);
        }

        [HttpPost]
        public ActionResult EditAccount(NHANVIEN nhanvien)
        {
            var detail = database.NHANVIENs.Where(m => m.MANHANVIEN == nhanvien.MANHANVIEN);

            if (detail == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                database.Entry(nhanvien).State = EntityState.Modified;
                database.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public bool CheckExistAccount(NHANVIEN _user)
        {
            var check = database.NHANVIENs.Where(s => s.EMAIL == _user.EMAIL && s.PASSWORD == _user.PASSWORD).FirstOrDefault();
            if (check != null)
            {
                return true;
            }

            return false;
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "LoginUser");
        }
    }
}