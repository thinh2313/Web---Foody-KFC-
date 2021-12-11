using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebBanThucAnNhanh.Models;

namespace WebBanThucAnNhanh.Controllers
{
    public class HomeController : Controller
    {
        QLThucAnNhanhEntities _db = new QLThucAnNhanhEntities();
        public ActionResult Index(string loai, int? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            if (loai == null)
            {
                var monanList = _db.MONANs.OrderByDescending(x => x.TENMONAN);
                return View(monanList.ToPagedList(pageNum,pageSize));
            }
            else
            {
                var monanList = _db.MONANs.OrderByDescending(x => x.TENMONAN)
                    .Where(x => x.MALOAI == loai);
                return View(monanList);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Details(string id)
        {
            return View(_db.MONANs.Where(s => s.MAMONAN == id).FirstOrDefault());
        }
       
    }
}