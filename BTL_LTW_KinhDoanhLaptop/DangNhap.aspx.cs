using BTL_LTW_KinhDoanhLaptop;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class DangNhap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["logout"] == "true")
            {
                Session.Remove("TaiKhoan");
                Response.Redirect("DangNhap.aspx");
                return;
            }

            HienThiTaiKhoan();

            if (!IsPostBack)
            {
                divThongBaoLoi.InnerText = "";
            }

            if (Session["SoLanSai"] != null)
            {
                int soLanSai = (int)Session["SoLanSai"];
                if (soLanSai >= 5)
                {
                    KhoaDangNhap();
                }
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

        protected void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (Session["SoLanSai"] != null)
            {
                int soLanSaiKiemTra = (int)Session["SoLanSai"];
                if (soLanSaiKiemTra >= 5)
                {
                    KhoaDangNhap();
                    return;
                }
            }

            string taiKhoan = Request.Form["txtTaiKhoan"];
            string matKhau = Request.Form["txtMatKhau"];

            if (taiKhoan == null) taiKhoan = "";
            if (matKhau == null) matKhau = "";

            if (matKhau == "")
            {
                divThongBaoLoi.InnerText = "Vui lòng nhập Mật khẩu!";
                return;
            }
            if (taiKhoan == "")
            {
                divThongBaoLoi.InnerText = "Vui lòng nhập đầy đủ Tài khoản!";
                return;
            }

            bool dangNhapThanhCong = false;

            if (Application["DanhSachTaiKhoan"] != null)
            {
                Dictionary<string, NguoiDung> dsTaiKhoan = (Dictionary<string, NguoiDung>)Application["DanhSachTaiKhoan"];

                if (dsTaiKhoan.ContainsKey(taiKhoan))
                {
                    if (dsTaiKhoan[taiKhoan].MatKhau == matKhau)
                    {
                        dangNhapThanhCong = true;
                    }
                }
                else
                {
                    foreach (var user in dsTaiKhoan.Values)
                    {
                        if (user.Email == taiKhoan && user.MatKhau == matKhau)
                        {
                            dangNhapThanhCong = true;
                            taiKhoan = user.TaiKhoan; // Update to actual username for Session
                            break;
                        }
                    }
                }
            }

            if (dangNhapThanhCong == true)
            {
                Session["SoLanSai"] = 0;
                Session["TaiKhoan"] = taiKhoan;
                
                string trangChuyenHuong = Request.QueryString["redirect"];
                if (trangChuyenHuong != null && trangChuyenHuong != "")
                {
                    Response.Redirect(trangChuyenHuong);
                }
                else
                {
                    Response.Redirect("TrangChu.aspx");
                }
            }
            else
            {
                int soLanSai = 0;
                if (Session["SoLanSai"] != null)
                {
                    soLanSai = (int)Session["SoLanSai"];
                }

                soLanSai = soLanSai + 1;
                Session["SoLanSai"] = soLanSai;

                if (soLanSai >= 5)
                {
                    KhoaDangNhap();
                }
                else
                {
                    int soLanConLai = 5 - soLanSai;
                    divThongBaoLoi.InnerText = "Tên đăng nhập hoặc mật khẩu không đúng! Bạn còn " + soLanConLai + " lần thử.";
                }
            }
        }

        private void KhoaDangNhap()
        {
            divThongBaoLoi.InnerText = "Tài khoản của bạn đã bị cấm đăng nhập do nhập sai quá 5 lần!";
            btnDangNhap.Disabled = true;
            btnDangNhap.Style["background-color"] = "gray";
        }
    }
}