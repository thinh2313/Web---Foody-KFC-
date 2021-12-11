using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanThucAnNhanh.Areas.NhanVien.Controllers
{
    public class ThongTinController : Controller
    {
        // GET: NhanVien/ThongTin
        public ActionResult Index()
        {
            return View();
        }

        // GET: NhanVien/ThongTin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NhanVien/ThongTin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NhanVien/ThongTin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: NhanVien/ThongTin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NhanVien/ThongTin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: NhanVien/ThongTin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NhanVien/ThongTin/Delete/5
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
                return View();
            }
        }
    }
}
