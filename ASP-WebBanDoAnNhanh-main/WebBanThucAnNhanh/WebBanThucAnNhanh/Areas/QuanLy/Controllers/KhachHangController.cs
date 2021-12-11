using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanThucAnNhanh.Models;

namespace WebBanThucAnNhanh.Areas.QuanLy.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: QuanLy/KhachHang

        QLThucAnNhanhEntities database = new QLThucAnNhanhEntities();
        public ActionResult Index()
        {
            return View(database.KHACHHANGs.ToList());
        }

        // GET: QuanLy/KhachHang/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuanLy/KhachHang/Create
        public ActionResult Create()
        {
            KHACHHANG khachhang = new KHACHHANG();
            return View(khachhang);
        }

        // POST: QuanLy/KhachHang/Create
        [HttpPost]
        public ActionResult Create(KHACHHANG khachhang)
        {
            try
            {
                // TODO: Add insert logic here
                database.KHACHHANGs.Add(khachhang);
                database.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        // GET: QuanLy/KhachHang/Edit/5
        public ActionResult Edit(int id)
        {
            return View(database.KHACHHANGs.Where(s => s.MAKHACHHANG == id).FirstOrDefault());
        }

        // POST: QuanLy/KhachHang/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, KHACHHANG khachhang)
        {
            try
            {
                // TODO: Add update logic here
                database.Entry(khachhang).State = EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: QuanLy/KhachHang/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuanLy/KhachHang/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, KHACHHANG khachhang)
        {
            try
            {
                // TODO: Add delete logic here
                khachhang = database.KHACHHANGs.Where(s => s.MAKHACHHANG == id).FirstOrDefault();
                database.KHACHHANGs.Remove(khachhang);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data using other table, Error Delete");
            }
        }
    }
}
