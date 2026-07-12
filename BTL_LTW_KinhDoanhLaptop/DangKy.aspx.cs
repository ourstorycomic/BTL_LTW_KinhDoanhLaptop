using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class DangKy : System.Web.UI.Page
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
            HienThiTaiKhoan();
            lblThongBaoDK.Text = "";
        }

        protected void btnDangKy_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtTaiKhoanDK.Text.Trim();
            string hoTen = txtHoTenDK.Text.Trim();
            string sdt = txtSDTDK.Text.Trim();
            string email = txtEmailDK.Text.Trim();
            string matKhau = txtMatKhauDK.Text.Trim();
            string xacNhanMK = txtXacNhanMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(sdt) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(xacNhanMK))
            {
                lblThongBaoDK.Text = "Vui lòng nhập đầy đủ thông tin!";
                lblThongBaoDK.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string emailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                lblThongBaoDK.Text = "Định dạng Email không hợp lệ! (Ví dụ đúng: abc@gmail.com)";
                lblThongBaoDK.ForeColor = System.Drawing.Color.Red;
                return;
            }

            
            if (matKhau.Length < 6 || !Regex.IsMatch(matKhau, @"[A-Z]") || !Regex.IsMatch(matKhau, @"[\W_]"))
            {
                lblThongBaoDK.Text = "Mật khẩu phải có tối thiểu 6 ký tự, gồm ít nhất 1 chữ in hoa và 1 ký tự đặc biệt!";
                lblThongBaoDK.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (matKhau != xacNhanMK)
            {
                lblThongBaoDK.Text = "Mật khẩu xác nhận không trùng khớp!";
                lblThongBaoDK.ForeColor = System.Drawing.Color.Red;
                return;
            }

            Dictionary<string, NguoiDung> dsTaiKhoan = Application["DanhSachTaiKhoan"] as Dictionary<string, NguoiDung>;

            if (dsTaiKhoan == null)
            {
                dsTaiKhoan = new Dictionary<string, NguoiDung>();
            }

            if (dsTaiKhoan.ContainsKey(taiKhoan))
            {
                lblThongBaoDK.Text = "Tên đăng nhập này đã tồn tại. Vui lòng chọn tên khác!";
                lblThongBaoDK.ForeColor = System.Drawing.Color.Red;
                return;
            }

            NguoiDung nd = new NguoiDung {
                TaiKhoan = taiKhoan,
                MatKhau = matKhau,
                HoTen = hoTen,
                SDT = sdt,
                Email = email,
                DiaChi = ""
            };

            dsTaiKhoan.Add(taiKhoan, nd);
            Application["DanhSachTaiKhoan"] = dsTaiKhoan;

            string script = "showToast('Đăng ký tài khoản thành công!', 'success'); setTimeout(function(){ window.location='DangNhap.aspx'; }, 1500);";
            //window.location='DangNhap.aspx';";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "RedirectScript", script, true);
        }
    }
}