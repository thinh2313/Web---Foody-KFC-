using Model.Framwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class AccountModel
    {
        private OnlineFastFoodShopContent content = null;

        public AccountModel()
        {
            content = new OnlineFastFoodShopContent();
        }
        public bool Login(string TaiKhoan, string MatKhau)
        {
            object[] sqlParams =
            {
                new SqlParameter("Taikhoan",TaiKhoan),
                new SqlParameter("MatKhau",MatKhau),
            };
            var res = content.Database.SqlQuery<bool>("Sp_TaiKhoan_MatKhau @TaiKhoan @MatKhau").SingleOrDefault();
            return res;
        }
    }
}
