using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanThucAnNhanh.Models;
using System.IO;
using System.Data.Entity;
using System.Net;

namespace WebBanThucAnNhanh.Areas.QuanLy.Controllers
{
    public class HoaDonController : Controller
    {

        QLThucAnNhanhEntities database = new QLThucAnNhanhEntities();
        // GET: QuanLy/HoaDon
        public ActionResult Index()
        {
            return View(database.HOADONs.ToList());
        }

        // GET: QuanLy/HoaDon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var hoadon = database.HOADONs.Find(id);
            if (hoadon == null)
            {
                return HttpNotFound();
            }
            return View(hoadon);
        }

        // POST: QuanLy/DonHang/Delete/5
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
