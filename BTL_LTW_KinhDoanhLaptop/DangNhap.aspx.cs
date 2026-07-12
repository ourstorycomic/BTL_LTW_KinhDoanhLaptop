using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class DangNhap : System.Web.UI.Page
    {
        
        
        private void HienThiTaiKhoan()
        {
            if (Session["TaiKhoan"] == null)
            {
                divTaiKhoan.InnerHtml = "<a href='DangNhap.aspx' class='login-link'><i class='fa-solid fa-user'></i> Đăng nhập</a>";
            }
            else
            {
                string avatar = "assets/img/lenovo.png";
                if (Application["DanhSachTaiKhoan"] != null)
                {
                    Dictionary<string, BTL_LTW_KinhDoanhLaptop.NguoiDung> dict = (Dictionary<string, BTL_LTW_KinhDoanhLaptop.NguoiDung>)Application["DanhSachTaiKhoan"];
                    string tk = Session["TaiKhoan"].ToString();
                    if (dict.ContainsKey(tk))
                    {
                        if (dict[tk].Avatar != null && dict[tk].Avatar != "")
                        {
                            avatar = dict[tk].Avatar;
                        }
                    }
                }

                string adminLinks = "";
                if (Session["TaiKhoan"].ToString() == "admin")
                {
                    adminLinks = "<a href='QuanTri.aspx'><i class='fa-solid fa-gear'></i> Quản trị</a>" +
                                 "<a href='BaoCao.aspx'><i class='fa-solid fa-chart-pie'></i> Thống kê</a>";
                }

                string html = "";
                html += "<div class='user-dropdown'>";
                html += "<img src='" + avatar + "' class='user-avatar' />";
                html += "<span>" + Session["TaiKhoan"].ToString() + "</span>";
                html += "<i class='fa-solid fa-caret-down'></i>";
                html += "<div class='dropdown-content'>";
                html += "<a href='HoSo.aspx'><i class='fa-solid fa-address-card'></i> Hồ sơ cá nhân</a>";
                html += adminLinks;
                html += "<a href='DangNhap.aspx?logout=true' class='logout-link'><i class='fa-solid fa-right-from-bracket'></i> Đăng xuất</a>";
                html += "</div>";
                html += "</div>";
                
                divTaiKhoan.InnerHtml = html;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["logout"] == "true")
            {
                Session.Remove("TaiKhoan");
                Response.Redirect("DangNhap.aspx");
                return;
            }

            HienThiTaiKhoan();
            lblThongBaoLoi.Text = "";

            if (Session["SoLanSai"] != null && (int)Session["SoLanSai"] >= 5)
            {
                KhoaDangNhap();
            }
        }

        protected void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (Session["SoLanSai"] != null && (int)Session["SoLanSai"] >= 5)
            {
                KhoaDangNhap();
                return;
            }

            string taiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                lblThongBaoLoi.Text = "Vui lòng nhập đầy đủ Tài khoản và Mật khẩu!";
                return;
            }

            // ================= ĐỌC TỪ GLOBAL =================
            // Kéo danh sách tài khoản từ bộ nhớ Global ra
            Dictionary<string, NguoiDung> dsTaiKhoan = Application["DanhSachTaiKhoan"] as Dictionary<string, NguoiDung>;

            // Kiểm tra xem ds có tồn tại không, có chứa tài khoản này không, và mật khẩu có khớp không
            bool dangNhapThanhCong = false;

            if (dsTaiKhoan != null && dsTaiKhoan.ContainsKey(taiKhoan) && dsTaiKhoan[taiKhoan].MatKhau == matKhau)
            {
                dangNhapThanhCong = true;
            }
            // =================================================

            if (dangNhapThanhCong)
            {
                // Reset số lần đếm về 0
                Session["SoLanSai"] = 0;
                Session["TaiKhoan"] = taiKhoan;
                Response.Redirect("TrangChu.aspx");
            }
            else
            {
                int soLanSai = 0;
                if (Session["SoLanSai"] != null)
                {
                    soLanSai = (int)Session["SoLanSai"];
                }

                soLanSai++;
                Session["SoLanSai"] = soLanSai;

                if (soLanSai >= 5)
                {
                    KhoaDangNhap();
                }
                else
                {
                    int soLanConLai = 5 - soLanSai;
                    lblThongBaoLoi.Text = $"Tên đăng nhập hoặc mật khẩu không đúng! Bạn còn {soLanConLai} lần thử.";
                }
            }
        }

        private void KhoaDangNhap()
        {
            lblThongBaoLoi.Text = "Tài khoản của bạn đã bị cấm đăng nhập do nhập sai quá 5 lần!";
            lblThongBaoLoi.ForeColor = System.Drawing.Color.Red;

            btnDangNhap.Enabled = false;
            btnDangNhap.BackColor = System.Drawing.Color.Gray;
            txtTaiKhoan.Enabled = false;
            txtMatKhau.Enabled = false;
        }
    }
}