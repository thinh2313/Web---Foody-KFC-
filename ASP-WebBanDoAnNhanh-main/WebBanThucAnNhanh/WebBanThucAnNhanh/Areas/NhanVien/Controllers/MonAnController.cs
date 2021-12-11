using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanThucAnNhanh.Models;
using System.IO;
using System.Data.Entity;

namespace WebBanThucAnNhanh.Areas.NhanVien.Controllers
{
    public class MonAnController : Controller
    {
        QLThucAnNhanhEntities _db = new QLThucAnNhanhEntities();
        // GET: Admin/MonAn
        public ActionResult Index()
        {
            var model = _db.MONANs.Where(s => s.STATUS != 0).ToList();
            return View(model);
        }

        public ActionResult Trash()
        {
            var model = _db.MONANs.Where(s => s.STATUS == 0).ToList();
            return View(model);
        }
        // GET: Admin/MonAn/Details/5
        public ActionResult Status(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            MONAN monan = _db.MONANs.Find(id);
            if (monan.STATUS == 1)
            {
                monan.STATUS = 2;
            }
            else
            {
                monan.STATUS = 1;
            }
            _db.Entry(monan).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Deltrash(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            MONAN monan = _db.MONANs.Find(id);
            monan.STATUS = 0;
            _db.Entry(monan).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Retrash(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Trash", "MonAn");
            }
            MONAN monan = _db.MONANs.Find(id);
            monan.STATUS = 2;
            _db.Entry(monan).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Trash", "MonAn");

        }
        public ActionResult Details(string id)
        {
            return View(_db.MONANs.Where(s => s.MAMONAN == id).FirstOrDefault());
        }

        // GET: Admin/MonAn/Create
        public ActionResult Create()
        {
            List<LOAI> list = _db.LOAIs.ToList();
            ViewBag.listLoai = new SelectList(list, "MaLoai", "TenLoai", "Select cate");
            MONAN monan = new MONAN();
            return View(monan);
        }

        // POST: Admin/MonAn/Create
        [HttpPost]
        public ActionResult Create(MONAN monan)
        {
            try
            {
                List<LOAI> list = _db.LOAIs.ToList();

                // TODO: Add insert logic here
                if (monan.UploadImage != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(monan.UploadImage.FileName);
                    string extension = Path.GetExtension(monan.UploadImage.FileName);
                    fileName = fileName + extension;
                    monan.HINHANH = "~/Assets/admin/img/" + fileName;
                    monan.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Assets/admin/img/"), fileName));
                }
                ViewBag.listLoai = new SelectList(list, "MaLoai", "TenLoai", 1);
                _db.MONANs.Add(monan);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        // GET: Admin/MonAn/Edit/5
        public ActionResult Edit(string id)
        {
            return View(_db.MONANs.Where(s => s.MAMONAN == id).FirstOrDefault());
        }

        // POST: Admin/MonAn/Edit/5
        [HttpPost]
        public ActionResult Edit(MONAN monan, string id)
        {
            try
            {
                // TODO: Add update logic here
               
                _db.Entry(monan).State = EntityState.Modified;
                
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/MonAn/Delete/5
        public ActionResult Delete(string id)
        {
            return View(_db.MONANs.Where(s => s.MAMONAN == id).FirstOrDefault());
        }

        // POST: Admin/MonAn/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, MONAN monan)
        {
            try
            {
                // TODO: Add delete logic here
                monan = _db.MONANs.Where(s => s.MAMONAN == id).FirstOrDefault();
                _db.MONANs.Remove(monan);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data using other table, Error Delete");
            }
        }
        public ActionResult SelectFood()
        {
            LOAI se_loai = new LOAI();
            se_loai.listLoai = _db.LOAIs.ToList<LOAI>();
            return PartialView(se_loai);
        }
        public ActionResult SelectUnit()
        {
            DONVITINH se_donvi = new DONVITINH();
            se_donvi.listUnit = _db.DONVITINHs.ToList<DONVITINH>();
            return PartialView(se_donvi);
        }
        public ActionResult Searchprice(double min = double.MinValue, double max = double.MaxValue)
        {
            var list = _db.MONANs.Where(p => (double)p.GIABAN >= min && (double)p.GIABAN <= max).ToList();
            return View(list);
        }
    }
}
