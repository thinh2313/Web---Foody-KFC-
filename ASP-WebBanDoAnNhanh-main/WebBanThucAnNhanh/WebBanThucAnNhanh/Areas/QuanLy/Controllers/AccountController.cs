using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanThucAnNhanh.Models;

namespace WebBanThucAnNhanh.Areas.QuanLy.Controllers
{
    public class AccountController : Controller
    {
        QLThucAnNhanhEntities _db = new QLThucAnNhanhEntities();
        // GET: QuanLy/Account
        public ActionResult Index()
        {
            return View(_db.NHANVIENs.ToList());
        }

        // GET: QuanLy/Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuanLy/Account/Create
        public ActionResult Create()
        {
            NHANVIEN account = new NHANVIEN();
            return View(account);
        }

        // POST: QuanLy/Account/Create
        [HttpPost]
        public ActionResult Create(NHANVIEN nhanvien)
        {
            try
            {
                // TODO: Add insert logic here

                _db.NHANVIENs.Add(nhanvien);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: QuanLy/Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_db.NHANVIENs.Where(s => s.MANHANVIEN == id).FirstOrDefault());
        }

        // POST: QuanLy/Account/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, NHANVIEN nhanvien)
        {
            try
            {
                // TODO: Add update logic here
                _db.Entry(nhanvien).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: QuanLy/Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_db.NHANVIENs.Where(s => s.MANHANVIEN == id).FirstOrDefault());
        }

        // POST: QuanLy/Account/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, NHANVIEN nhanvien)
        {
            try
            {
                // TODO: Add delete logic here
                nhanvien = _db.NHANVIENs.Where(s => s.MANHANVIEN == id).FirstOrDefault();
                _db.NHANVIENs.Remove(nhanvien);
                _db.SaveChanges();
             
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data using other table, Error Delete");
            }
        }
    }
}
