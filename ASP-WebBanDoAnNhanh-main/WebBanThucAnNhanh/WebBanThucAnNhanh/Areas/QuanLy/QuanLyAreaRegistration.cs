using System.Web.Mvc;

namespace WebBanThucAnNhanh.Areas.QuanLy
{
    public class QuanLyAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "QuanLy";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "QuanLy",
                "QuanLy/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "WebBanThucAnNhanh.Areas.QuanLy.Controllers" }
            );
        }
    }
}