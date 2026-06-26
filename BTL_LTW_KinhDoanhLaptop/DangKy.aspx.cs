using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions; // Thêm thư viện này để dùng Regex kiểm tra email
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class DangKy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

            // 1. Kiểm tra xem có để trống ô nào không (Bao gồm cả Họ tên và SDT)
            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(sdt) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(xacNhanMK))
            {
                lblThongBaoDK.Text = "Vui lòng nhập đầy đủ thông tin!";
                lblThongBaoDK.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // 2. KIỂM TRA ĐỊNH DẠNG EMAIL (Phải có dạng abc@gmail.com)
            // Sử dụng Regex chuẩn để kiểm tra cấu trúc email
            string emailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                lblThongBaoDK.Text = "Định dạng Email không hợp lệ! (Ví dụ đúng: abc@gmail.com)";
                lblThongBaoDK.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // 3. Kiểm tra mật khẩu xác nhận có khớp không
            if (matKhau != xacNhanMK)
            {
                lblThongBaoDK.Text = "Mật khẩu xác nhận không trùng khớp!";
                lblThongBaoDK.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // 4. LƯU TÀI KHOẢN VÀO BỘ NHỚ TẠM (Application) để trang Đăng Nhập kiểm tra được
            Application[taiKhoan] = matKhau;

            // Bạn có thể lưu thêm Họ tên hoặc SĐT vào Application nếu sau này muốn dùng ở trang chủ:
            // Application[taiKhoan + "_HoTen"] = hoTen;

            // 5. ĐĂNG KÝ THÀNH CÔNG -> TỰ ĐỘNG CHUYỂN VỀ TRANG ĐĂNG NHẬP
            // Sử dụng một đoạn Javascript nhỏ để hiển thị thông báo "Đăng ký thành công" trước, 
            // sau khi người dùng bấm OK thì nó sẽ tự chuyển hướng về DangNhap.aspx
            string script = "alert('Đăng ký tài khoản thành công! Hệ thống sẽ tự động chuyển bạn về trang Đăng nhập.'); window.location='DangNhap.aspx';";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "RedirectScript", script, true);
        }
    }
}