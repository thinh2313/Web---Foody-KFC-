using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanThucAnNhanh.Models;

namespace WebBanThucAnNhanh.Areas.QuanLy.Controllers
{
    public class LoaiController : Controller
    {
        QLThucAnNhanhEntities _db = new QLThucAnNhanhEntities();
        // GET: NhanVien/LoaiMon
        public ActionResult Index()
        {
            return View(_db.LOAIs.ToList());
        }

        // GET: NhanVien/LoaiMon/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NhanVien/LoaiMon/Create
        public ActionResult Create()
        {
            LOAI loai = new LOAI();
            return View(loai);
        }

        // POST: NhanVien/LoaiMon/Create
        [HttpPost]
        public ActionResult Create(LOAI loai)
        {
            try
            {
                // TODO: Add insert logic here

                _db.LOAIs.Add(loai);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: NhanVien/LoaiMon/Edit/5
        public ActionResult Edit(string id)
        {
            return View(_db.LOAIs.Where(s => s.MALOAI == id).FirstOrDefault()); 
        }

        // POST: NhanVien/LoaiMon/Edit/5
        [HttpPost]
        public ActionResult Edit( LOAI loai,string id)
        {
            try
            {
                // TODO: Add update logic here
                _db.Entry(loai).State = EntityState.Modified;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string id)
        {
            return View(_db.LOAIs.Where(s => s.MALOAI == id).FirstOrDefault());
        }

        // POST: NhanVien/DonViTinhMon/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, LOAI loai)
        {
            try
            {
                // TODO: Add delete logic here
                loai = _db.LOAIs.Where(s => s.MALOAI == id).FirstOrDefault();
                _db.LOAIs.Remove(loai);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data using other table, Error Delete");
            }
        }

        public PartialViewResult LoaiPartial()
        {
            var loaiList = _db.LOAIs.ToList();
            return PartialView(loaiList);
        }
        // GET: QuanLy/Loai
      
    }
}