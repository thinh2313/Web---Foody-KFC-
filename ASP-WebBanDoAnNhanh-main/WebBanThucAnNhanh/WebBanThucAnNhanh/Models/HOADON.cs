//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBanThucAnNhanh.Models
{
    using System;
    using System.Collections.Generic;

    public partial class HOADON
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOADON()
        {
            this.CTHOADONs = new HashSet<CTHOADON>();
        }

        public int MAHOADON { get; set; }
        public Nullable<int> MANHANVIEN { get; set; }
        public int MADATHANG { get; set; }
        public Nullable<double> TONGTIEN { get; set; }
        public Nullable<System.DateTime> NGAYGIO { get; set; }
        public Nullable<double> TONGTHUE { get; set; }
        public string GHICHU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHOADON> CTHOADONs { get; set; }
        public virtual DATHANG DATHANG { get; set; }
        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}