using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_LTW_KinhDoanhLaptop
{
    public class Laptop
    {
        public int Id { get; set; }
        public string TenSanPham { get; set; }
        public decimal GiaTien { get; set; }
        public string HinhAnh { get; set; }
        public int SoLuongTon { get; set; }
        public int SoLuongBan { get; set; }
    }
    
    public class NguoiDung
    {
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string Avatar { get; set; }
    }
}