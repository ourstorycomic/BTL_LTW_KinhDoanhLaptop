using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace BTL_LTW_KinhDoanhLaptop
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            List<Laptop> danhSachLaptop = new List<Laptop>
            {
        new Laptop { Id = 1, TenSanPham = "Laptop Lenovo IdeaPad Slim 3", GiaTien = 19490000, HinhAnh = "assets/img/lenovo.png", SoLuongTon = 50, SoLuongBan = 0 },
        new Laptop { Id = 2, TenSanPham = "Laptop HP 15", GiaTien = 17290000, HinhAnh = "assets/img/acer.png", SoLuongTon = 50, SoLuongBan = 0 },
        new Laptop { Id = 3, TenSanPham = "Laptop Acer Aspire 7", GiaTien = 22990000, HinhAnh = "assets/img/dell.png", SoLuongTon = 50, SoLuongBan = 0 },
        new Laptop { Id = 4, TenSanPham = "Laptop MSI Katana 15", GiaTien = 31990000, HinhAnh = "assets/img/dell2.png", SoLuongTon = 50, SoLuongBan = 0 },

        new Laptop { Id = 5, TenSanPham = "MacBook Air M1 2020", GiaTien = 18990000, HinhAnh = "assets/img/lenovo.png", SoLuongTon = 50, SoLuongBan = 0 },
        new Laptop { Id = 6, TenSanPham = "Laptop Dell Inspiron 15", GiaTien = 15490000, HinhAnh = "assets/img/lenovo.png", SoLuongTon = 50, SoLuongBan = 0 },
        new Laptop { Id = 7, TenSanPham = "Laptop Asus TUF Gaming", GiaTien = 25990000, HinhAnh = "assets/img/lenovo.png", SoLuongTon = 50, SoLuongBan = 0 },
        new Laptop { Id = 8, TenSanPham = "Laptop Gigabyte G5", GiaTien = 20490000, HinhAnh = "assets/img/lenovo.png", SoLuongTon = 50, SoLuongBan = 0 }
            };
            Application["DanhSachLaptop"] = danhSachLaptop;

            Dictionary<string, NguoiDung> danhSachTaiKhoan = new Dictionary<string, NguoiDung>();
            danhSachTaiKhoan.Add("admin", new NguoiDung { TaiKhoan = "admin", MatKhau = "123456", HoTen = "Admin", Email = "admin@example.com", SDT = "0123456789", DiaChi = "Hà Nội" });

            Application["DanhSachTaiKhoan"] = danhSachTaiKhoan;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}