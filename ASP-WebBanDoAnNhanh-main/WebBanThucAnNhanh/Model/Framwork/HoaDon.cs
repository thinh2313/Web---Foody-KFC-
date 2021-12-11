namespace Model.Framwork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MaHoaDon { get; set; }

        [StringLength(50)]
        public string NguoiBan { get; set; }

        public DateTime? NgayBan { get; set; }

        public int? TongTien { get; set; }
    }
}
