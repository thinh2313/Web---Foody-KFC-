using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanThucAnNhanh.Models;


namespace WebBanThucAnNhanh.Areas.QuanLy.Controllers
{
    public class FeedbackController : Controller
    {
        QLThucAnNhanhEntities db = new QLThucAnNhanhEntities();
        // GET: QuanLy/Feedback
        public ActionResult Index()
        {
            return View(db.FEEDBACKs.ToList());
        }

        // GET: QuanLy/Feedback/Details/5
        public ActionResult Details(int id)
        {
            return View(db.FEEDBACKs.Where(s => s.MAFEEDBACK == id).FirstOrDefault());
        }

        // GET: QuanLy/Feedback/Create
        public ActionResult Create()
        {
            FEEDBACK feedback = new FEEDBACK();
            return View(feedback);
        }

        // POST: QuanLy/Feedback/Create
        [HttpPost]
        public ActionResult Create(FEEDBACK feedback)
        {
            try
            {
                // TODO: Add insert logic here

                db.FEEDBACKs.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: QuanLy/Feedback/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuanLy/Feedback/Edit/5
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

        // GET: QuanLy/Feedback/Delete/5
        public ActionResult Delete(int id)
        {
            return View(db.FEEDBACKs.Where(s => s.MAFEEDBACK == id).FirstOrDefault());
        }

        // POST: NhanVien/DonViTinhMon/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FEEDBACK feedback)
        {
            try
            {
                // TODO: Add delete logic here
                feedback = db.FEEDBACKs.Where(s => s.MAFEEDBACK == id).FirstOrDefault();
                db.FEEDBACKs.Remove(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data using other table, Error Delete");
            }
        }
    }
}
