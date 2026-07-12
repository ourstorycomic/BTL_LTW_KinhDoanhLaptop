using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class GioiThieu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HienThiTaiKhoan();
            CapNhatSoLuongGio();

            if (!IsPostBack)
            {
                // Nếu cần xử lý lần đầu load trang thì viết vào đây
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
                        if (tuDienTaiKhoan[tenTaiKhoan].Avatar != null && tuDienTaiKhoan[tenTaiKhoan].Avatar != "")
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

        private void CapNhatSoLuongGio()
        {
            if (Session["GioHang"] != null)
            {
                DataTable dt = (DataTable)Session["GioHang"];
                int tongSoLuong = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tongSoLuong += Convert.ToInt32(dt.Rows[i]["SoLuong"]);
                }

                lblSoLuongGio.Text = tongSoLuong.ToString();
            }
        }
    }
}