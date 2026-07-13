using System;
using System.Collections.Generic;
using System.Data;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class TrangKhongTonTai : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HienThiSoLuongGioHang();
            HienThiTaiKhoan();
        }

        private void HienThiSoLuongGioHang()
        {
            if (Session["GioHang"] != null)
            {
                DataTable dtGioHang = (DataTable)Session["GioHang"];
                int tongSoLuong = 0;
                foreach (DataRow row in dtGioHang.Rows)
                {
                    tongSoLuong += Convert.ToInt32(row["SoLuong"]);
                }
                lblSoLuongGio.InnerText = tongSoLuong.ToString();
            }
            else
            {
                lblSoLuongGio.InnerText = "0";
            }
        }

        private void HienThiTaiKhoan()
        {
            if (Session["TaiKhoan"] == null)
            {
                divChuaDangNhap.Visible = true;
                divDaDangNhap.Visible = false;
            }
            else
            {
                divChuaDangNhap.Visible = false;
                divDaDangNhap.Visible = true;

                string tenTaiKhoan = Session["TaiKhoan"].ToString();
                lblTenTaiKhoan.InnerText = tenTaiKhoan;

                if (Application["DanhSachTaiKhoan"] != null)
                {
                    Dictionary<string, NguoiDung> tuDienTaiKhoan = (Dictionary<string, NguoiDung>)Application["DanhSachTaiKhoan"];
                    if (tuDienTaiKhoan.ContainsKey(tenTaiKhoan))
                    {
                        if (!string.IsNullOrEmpty(tuDienTaiKhoan[tenTaiKhoan].Avatar))
                        {
                            imgAvatar.Src = tuDienTaiKhoan[tenTaiKhoan].Avatar;
                        }
                    }
                }

                if (tenTaiKhoan == "admin")
                {
                    linkQuanTri.Visible = true;
                    linkThongKe.Visible = true;
                }
                else
                {
                    linkQuanTri.Visible = false;
                    linkThongKe.Visible = false;
                }
            }
        }
    }
}
