using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanThucAnNhanh.Models;

namespace WebBanThucAnNhanh.Areas.QuanLy.Controllers
{
    public class DonViTinhMonController : Controller
    {
        QLThucAnNhanhEntities _db = new QLThucAnNhanhEntities();
        // GET: NhanVien/DonViTinhMon
        public ActionResult Index()
        {
            return View(_db.DONVITINHs.ToList());
        }

        // GET: NhanVien/DonViTinhMon/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NhanVien/DonViTinhMon/Create
        public ActionResult Create()
        {
            DONVITINH donvitinh = new DONVITINH();
            return View(donvitinh);
        }

        // POST: NhanVien/DonViTinhMon/Create
        [HttpPost]
        public ActionResult Create(DONVITINH donvitinh)
        {
            try
            {
                // TODO: Add insert logic here

                _db.DONVITINHs.Add(donvitinh);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View(_db.DONVITINHs.Where(s => s.MADONVITINH == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(DONVITINH donvitinh, int id)
        {
            try
            {
                // TODO: Add update logic here
                _db.Entry(donvitinh).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: NhanVien/DonViTinhMon/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_db.DONVITINHs.Where(s => s.MADONVITINH == id).FirstOrDefault());
        }

        // POST: NhanVien/DonViTinhMon/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, DONVITINH donvitinh)
        {
            try
            {
                // TODO: Add delete logic here
                donvitinh = _db.DONVITINHs.Where(s => s.MADONVITINH == id).FirstOrDefault();
                _db.DONVITINHs.Remove(donvitinh);
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
