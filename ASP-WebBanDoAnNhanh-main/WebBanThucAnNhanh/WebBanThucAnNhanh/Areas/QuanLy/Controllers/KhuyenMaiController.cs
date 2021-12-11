using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanThucAnNhanh.Models;

namespace WebBanThucAnNhanh.Areas.QuanLy.Controllers
{
    public class KhuyenMaiController : Controller
    {
        QLThucAnNhanhEntities database = new QLThucAnNhanhEntities();
        // GET: QuanLy/KhuyenMai
        public ActionResult Index()
        {
            return View(database.KHUYENMAIs.ToList());
        }

        // GET: QuanLy/KhuyenMai/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuanLy/KhuyenMai/Create
        public ActionResult Create()
        {
            KHUYENMAI khuyenmai = new KHUYENMAI();
            return View(khuyenmai);
        }

        // POST: QuanLy/KhuyenMai/Create
        [HttpPost]
        public ActionResult Create(KHUYENMAI khuyenmai)
        {
            try
            {
                // TODO: Add insert logic here
                database.KHUYENMAIs.Add(khuyenmai);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: QuanLy/KhuyenMai/Edit/5
        public ActionResult Edit(string  id)
        {
            return View(database.KHUYENMAIs.Where(s => s.MAKHUYENMAI == id).FirstOrDefault());
        }

        // POST: QuanLy/KhuyenMai/Edit/5
        [HttpPost]
        public ActionResult Edit(KHUYENMAI khuyenmai)
        {
            try
            {
                // TODO: Add update logic here
               
                database.Entry(khuyenmai).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: QuanLy/KhuyenMai/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuanLy/KhuyenMai/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data using other table, Error Delete");
            }
        }
    }
}
