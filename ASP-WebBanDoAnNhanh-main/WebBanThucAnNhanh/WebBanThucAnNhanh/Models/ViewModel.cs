using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanThucAnNhanh.Models
{
    public class ViewModel
    {
        public KHACHHANG khachhang { get; set; }
        public CTHOADON cthd { get; set; }
        public HOADON hoadon { get; set; }
        public LOAI loai { get; set; }
    }
}