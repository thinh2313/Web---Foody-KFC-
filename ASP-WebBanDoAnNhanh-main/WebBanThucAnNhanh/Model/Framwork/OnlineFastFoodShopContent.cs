using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Model.Framwork
{
    public partial class OnlineFastFoodShopContent : DbContext
    {
        public OnlineFastFoodShopContent()
            : base("name=OnlineFastFoodShopContent")
        {
        }

        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<MonAn> MonAn { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }
      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MonAn>()
                .Property(e => e.MaMon)
                .IsUnicode(false);

            modelBuilder.Entity<MonAn>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<MonAn>()
                .Property(e => e.DonViTinh)
                .IsUnicode(false);

            modelBuilder.Entity<MonAn>()
                .Property(e => e.MoTa)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.TaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);
        }
    }
}
