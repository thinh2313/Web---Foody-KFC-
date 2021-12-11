using System.Web.Mvc;

namespace WebBanThucAnNhanh.Areas.NhanVien
{
    public class NhanVienAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NhanVien";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NhanVien_Create",
                "NhanVien/{controller}/{action}/{id}",
                new { action = "Create", id = UrlParameter.Optional },
                new[] { "WebBanThucAnNhanh.Areas.NhanVien.Controllers" }
            );
            context.MapRoute(
                "NhanVien_default",
                "NhanVien/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "WebBanThucAnNhanh.Areas.NhanVien.Controllers" }
            );
        }
    }
}