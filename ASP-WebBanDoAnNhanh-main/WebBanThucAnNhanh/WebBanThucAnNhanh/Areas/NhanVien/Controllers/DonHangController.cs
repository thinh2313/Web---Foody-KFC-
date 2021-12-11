using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanThucAnNhanh.Models;
using System.IO;
using System.Data.Entity;
using System.Net;

namespace WebBanThucAnNhanh.Areas.NhanVien.Controllers
{
    public class DonHangController : Controller
    {
        QLThucAnNhanhEntities _db = new QLThucAnNhanhEntities();
        // GET: QuanLy/DonHang
        public ActionResult ShowMonAn()
        {
            if (Session["GioHang"] == null)
                return RedirectToAction("ShowMonAn", "DonHang");
            GioHang _gioHang = Session["GioHang"] as GioHang;
            return View(_gioHang);
        }
        public ActionResult Index()
        {
            return View(_db.DATHANGs.ToList());
        }
        public ActionResult XacNhan(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            DATHANG dathang = _db.DATHANGs.Find(id);
            if (dathang.TRANGTHAI == 0)
            {
                dathang.TRANGTHAI = 1;
            }
            _db.Entry(dathang).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult HoanTat(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            //int temp = (int)Session["MAKH"];
            DATHANG dathang = _db.DATHANGs.Find(id);
            if (dathang.TRANGTHAI == 1)
            {
                if (ModelState.IsValid)
                {

                    var hoadon = new HOADON()
                    {   
                        MADATHANG = dathang.MADATHANG,
                        //Đừng thêm [double] vô cái này chỉ cần vô model Đặt Hàng copy Nullable<double> Của tổng tiền rồi quăng qua cho hóa đơn
                        TONGTIEN = dathang.TONGTIEN,
                        NGAYGIO = dathang.NGAY,
                        MANHANVIEN = (int)Session["ID"],
                    };
                    _db.HOADONs.Add(hoadon);
                    _db.SaveChanges();

                    var mONAN_DATHANG = _db.CT_MONAN_DATHANG.Where(m => m.MADATHANG == id).ToList();
                    foreach (var item in mONAN_DATHANG)
                    {
                        var cthoadon = new CTHOADON()
                        {
                            MAHOADON = hoadon.MAHOADON,
                            MADATHANG = item.MADATHANG,
                            MAMONAN = item.MAMONAN,
                            SOLUONG = item.SOLUONG,
                        };
                        _db.CTHOADONs.Add(cthoadon);
                        _db.SaveChanges();
                    }

                    dathang.TRANGTHAI = 2;
                    _db.Entry(dathang).State = EntityState.Modified;
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("Index");

        }
        public ActionResult Huy(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            DATHANG dathang = _db.DATHANGs.Find(id);
            if (dathang.TRANGTHAI == 1)
            {
                dathang.TRANGTHAI = 3;
            }
            _db.Entry(dathang).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        // GET: QuanLy/DonHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dathang = _db.DATHANGs.Find(id);
            if (dathang == null)
            {
                return HttpNotFound();
            }
            return View(dathang);
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
                return View();
            }
        }
    }
}
