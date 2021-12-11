namespace Model.Framwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MonAn")]
    public partial class MonAn
    {
        [Key]
        [StringLength(50)]
        public string MaMon { get; set; }

        [StringLength(50)]
        public string TenMon { get; set; }

        [StringLength(250)]
        public string HinhAnh { get; set; }

        [StringLength(50)]
        public string DonViTinh { get; set; }

        public int? GiaBan { get; set; }

        public int? Thue { get; set; }

        public string MoTa { get; set; }
    }
}
