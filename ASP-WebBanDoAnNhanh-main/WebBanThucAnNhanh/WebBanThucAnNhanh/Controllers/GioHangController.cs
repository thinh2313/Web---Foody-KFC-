using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using WebBanThucAnNhanh.Models;
using System.Runtime.Remoting;
using PayPal.Api;
using System.Data.Entity.Validation;

namespace WebBanThucAnNhanh.Controllers
{
    public class GioHangController : Controller
    {
        QLThucAnNhanhEntities _db = new QLThucAnNhanhEntities();
        // GET: GioHang
        public GioHang GetGioHang()
        {
            GioHang giohang = Session["GioHang"] as GioHang;
            if(giohang == null || Session["GioHang"] == null)
            {
                giohang = new GioHang();
                Session["GioHang"] = giohang;
            }
            return giohang;
        }
        public ActionResult AddToCart(string id)
        {
            var monan = _db.MONANs.SingleOrDefault(s => s.MAMONAN == id);
            if(monan!=null)
            {
                GetGioHang().Add_Product_Cart(monan);
            }
            return RedirectToAction("ShowToCart", "GioHang");
        }
        //Trang giỏ hàng
        public ActionResult ShowToCart()
        {
            GioHang giohang = Session["GioHang"] as GioHang;
            return View(giohang);
        }
        public ActionResult Update_Quantity_Cart(FormCollection form)
        {
            GioHang giohang = Session["GioHang"] as GioHang;
            string ma_monan = form["MA_MONAN"];
            int quantity = int.Parse(form["Quantity"]);
            giohang.Update_quantity(ma_monan, quantity);
            return RedirectToAction("ShowToCart", "GioHang");
        }
        public ActionResult RemoveCart(string id)
        {
            GioHang giohang = Session["GioHang"] as GioHang;
            giohang.Remove_CartItem(id);
            return RedirectToAction("ShowToCart", "GioHang");
        }
        public PartialViewResult BagCart()
        {
            int _t_item = 0;
            GioHang giohang = Session["GioHang"] as GioHang;
            if(giohang != null)
            {
                _t_item = giohang.Total_Quantity();
            }
            ViewBag.infoGioHang = _t_item;
            return PartialView("BagCart");
        }
        public ActionResult CheckoutSuccess()
        {
            

            return View();
        }
        public ActionResult Checkout()
        {
            var GioHang = GetGioHang();
            if (GioHang.Total_quantity() == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpPost]
        public ActionResult Checkout(DIACHIGH model)
        {
            var giohang = GetGioHang();
            if (giohang.Total_quantity() == 0)
            {
                ModelState.AddModelError("", "Gio Hang ko duoc de trong");
            }
            if (ModelState.IsValid)
            {
                if(Session["MAKH"] == null)
                {
                    var diachigh = new DIACHIGH()
                    {
                        HOTEN = model.HOTEN,
                        SDT = model.SDT,
                        SONHA = model.SONHA,
                        PHUONG = model.PHUONG,
                        QUAN = model.QUAN,
                    };
                    _db.DIACHIGHs.Add(diachigh);
                    _db.SaveChanges();

                    var dathang = new DATHANG()
                    {
                        MADIACHI = diachigh.MADIACHI,
                        NGAY = DateTime.Now,
                        TONGTIEN = giohang.Total_money(),

                    };
                    _db.DATHANGs.Add(dathang);
                    _db.SaveChanges();

                    var ctdathang = new List<CT_MONAN_DATHANG>();
                    foreach (var item in giohang.Items)
                    {
                        var detail = new CT_MONAN_DATHANG()
                        {
                            MAMONAN = item._monan.MAMONAN,
                            MADATHANG = dathang.MADATHANG,
                            SOLUONG = item._quantity,
                            GIABAN = item._monan.GIABAN,
                        };
                        ctdathang.Add(detail);
                    }
                    foreach (var detail in ctdathang)
                    {
                        _db.CT_MONAN_DATHANG.Add(detail);
                    }
                    _db.SaveChanges();
                    giohang.ClearCart();

                    return RedirectToAction("CheckoutSuccess");
                }
                else 
                {
                    var diachigh = new DIACHIGH()
                    {
                        HOTEN = model.HOTEN,
                        SDT = model.SDT,
                        SONHA = model.SONHA,
                        PHUONG = model.PHUONG,
                        QUAN = model.QUAN,
                        MAKHACHHANG = (int)Session["MAKH"],
                    };
                    _db.DIACHIGHs.Add(diachigh);
                    _db.SaveChanges();

                    var dathang = new DATHANG()
                    {
                        MADIACHI = diachigh.MADIACHI,
                        NGAY = DateTime.Now,
                        TONGTIEN = giohang.Total_money(),

                    };
                    _db.DATHANGs.Add(dathang);
                    _db.SaveChanges();

                    var ctdathang = new List<CT_MONAN_DATHANG>();
                    foreach (var item in giohang.Items)
                    {
                        var detail = new CT_MONAN_DATHANG()
                        {
                            MAMONAN = item._monan.MAMONAN,
                            MADATHANG = dathang.MADATHANG,
                            SOLUONG = item._quantity,
                            GIABAN = item._monan.GIABAN,
                        };
                        ctdathang.Add(detail);
                    }
                    foreach (var detail in ctdathang)
                    {
                        _db.CT_MONAN_DATHANG.Add(detail);
                    }
                    _db.SaveChanges();
                    giohang.ClearCart();

                    return RedirectToAction("CheckoutSuccess");
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui Long nhap du du lieu.");
            }
            return View(model);
        }

        // Work with Paypal Payment
        private Payment payment;

        // Create a paypment using an APIContext
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var listItems = new ItemList() { items = new List<Item>() };

            GioHang giohang = Session["GioHang"] as GioHang;
            foreach (var cart in giohang.Items)
            {
                listItems.items.Add(new Item()
                {
                    name = cart._monan.TENMONAN,
                    currency = "USD",
                    price = giohang.moneyUSD().ToString(),
                    quantity = cart._quantity.ToString(),
                    sku = "sku"
                });
            }

            var payer = new Payer() { payment_method = "paypal" };

            // Do the configuration RedirectURLs here with redirectURLs object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // Create details object
            var details = new Details()
            {
                tax = "1",
                shipping = "2",
                subtotal = giohang.Total_moneyUSD().ToString("N0")
            };

            // Create amount object
            var amount = new Amount()
            { 
                currency = "USD",
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(),// tax + shipping + subtotal
                details = details
            };

            // Create transaction
            var transactionList = new List<Transaction>();
            transactionList.Add(new Transaction()
            {
                description = "Chien Testing transaction description",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = listItems
            });

            payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            return payment.Create(apiContext);
        }
        // Create ExecutePayment method
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            payment = new Payment() { id = paymentId };
            return payment.Execute(apiContext, paymentExecution);
        }

        public ActionResult PaymentWithPaypal(DIACHIGH model)
        {
            var guidParam = Request.Params["guid"];
            if ((model.HOTEN?.Trim().Length > 0
                && model.PHUONG?.Trim().Length > 0
                && model.QUAN?.Trim().Length > 0
                && model.SONHA?.Trim().Length > 0
                )|| Session[guidParam] != null)
            {
                if (Session[guidParam] == null)
                {
                    var giohang = GetGioHang();
                    if (giohang.Total_quantity() == 0)
                    {
                        ModelState.AddModelError("", "Gio Hang ko duoc de trong");
                    }
                    if (ModelState.IsValid)
                    {
                        if (Session["MAKH"] == null)
                        {
                            var diachigh = new DIACHIGH()
                            {
                                HOTEN = model.HOTEN,
                                SDT = model.SDT,
                                SONHA = model.SONHA,
                                PHUONG = model.PHUONG,
                                QUAN = model.QUAN,
                            };

                            _db.DIACHIGHs.Add(diachigh);
                            _db.SaveChanges();

                            var dathang = new DATHANG()
                            {
                                MADIACHI = diachigh.MADIACHI,
                                NGAY = DateTime.Now,
                                TONGTIEN = giohang.Total_money(),

                            };
                            _db.DATHANGs.Add(dathang);
                            _db.SaveChanges();

                            var ctdathang = new List<CT_MONAN_DATHANG>();
                            foreach (var item in giohang.Items)
                            {
                                var detail = new CT_MONAN_DATHANG()
                                {
                                    MAMONAN = item._monan.MAMONAN,
                                    MADATHANG = dathang.MADATHANG,
                                    SOLUONG = item._quantity,
                                    GIABAN = item._monan.GIABAN,
                                };
                                ctdathang.Add(detail);
                            }
                            foreach (var detail in ctdathang)
                            {
                                _db.CT_MONAN_DATHANG.Add(detail);
                            }
                            _db.SaveChanges();
                        }
                        else
                        {
                            var diachigh = new DIACHIGH()
                            {
                                HOTEN = model.HOTEN,
                                SDT = model.SDT,
                                SONHA = model.SONHA,
                                PHUONG = model.PHUONG,
                                QUAN = model.QUAN,
                                MAKHACHHANG = (int)Session["MAKH"],
                            };
                            _db.DIACHIGHs.Add(diachigh);
                            _db.SaveChanges();

                            var dathang = new DATHANG()
                            {
                                MADIACHI = diachigh.MADIACHI,
                                NGAY = DateTime.Now,
                                TONGTIEN = giohang.Total_money(),

                            };
                            _db.DATHANGs.Add(dathang);
                            _db.SaveChanges();

                            var ctdathang = new List<CT_MONAN_DATHANG>();
                            foreach (var item in giohang.Items)
                            {
                                var detail = new CT_MONAN_DATHANG()
                                {
                                    MAMONAN = item._monan.MAMONAN,
                                    MADATHANG = dathang.MADATHANG,
                                    SOLUONG = item._quantity,
                                    GIABAN = item._monan.GIABAN,
                                };
                                ctdathang.Add(detail);
                            }
                            foreach (var detail in ctdathang)
                            {
                                _db.CT_MONAN_DATHANG.Add(detail);
                            }
                            _db.SaveChanges();
                        }
                    }
                }
                // Gettings context from the paypal bases on clientId and clientSecret for payment
                APIContext apiContext = PaypalConfiguration.GetAPIContext();

                try
                {
                    string payerId = Request.Params["PayerID"];
                    if (string.IsNullOrEmpty(payerId))
                    {
                        // Creating a payment
                        string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/GioHang/PaymentWithPaypal?";
                        var guid = Convert.ToString((new Random()).Next(100000));
                        var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);

                        // Get links returned from paypal response to create call funciton
                        var links = createdPayment.links.GetEnumerator();
                        string paypalRedirectUrl = string.Empty;

                        while (links.MoveNext())
                        {
                            Links link = links.Current;
                            if (link.rel.ToLower().Trim().Equals("approval_url"))
                            {
                                paypalRedirectUrl = link.href;
                            }
                        }

                        Session.Add(guid, createdPayment.id);
                        return Redirect(paypalRedirectUrl);
                    }
                    else
                    {
                        var executedPayment = ExecutePayment(apiContext, payerId, Session[guidParam] as string);
                        if (executedPayment.state.ToLower() != "approved")
                        {
                            Session.Remove("GioHang");
                            return View("Failure");
                        }
                    }
                }
                catch (Exception ex)
                {
                    PaypalLogger.Log("Error: " + ex.Message);
                    Session.Remove("GioHang");
                    return View("Failure");
                }
                Session.Remove("GioHang");
                return View("CheckoutSuccess");
            }

            return View("CheckoutSuccess");
        }
    }
}