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
        protected void Page_Load(object sender, EventArgs e)
        {
            // Xóa thông báo lỗi cũ
            lblThongBaoLoi.Text = "";

            // Kiểm tra ngay khi vừa load trang: Nếu đã sai quá 5 lần trước đó thì khóa luôn
            if (Session["SoLanSai"] != null && (int)Session["SoLanSai"] >= 5)
            {
                KhoaDangNhap();
            }
        }

        protected void btnDangNhap_Click(object sender, EventArgs e)
        {
            // Nếu đã bị khóa thì không cho chạy code bên dưới nữa
            if (Session["SoLanSai"] != null && (int)Session["SoLanSai"] >= 5)
            {
                KhoaDangNhap();
                return;
            }

            string taiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            // Validate dữ liệu trống
            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                lblThongBaoLoi.Text = "Vui lòng nhập đầy đủ Tài khoản và Mật khẩu!";
                return;
            }

            // TÌM MẬT KHẨU TRONG BỘ NHỚ TẠM DỰA VÀO TÊN TÀI KHOẢN ĐÃ NHẬP
            string matKhauDaLuu = Application[taiKhoan] as string;

            // Kiểm tra: Nếu tìm thấy tài khoản VÀ mật khẩu trùng khớp
            // (Giữ lại admin 123456 làm tài khoản dự phòng)
            if ((matKhauDaLuu != null && matKhauDaLuu == matKhau) || (taiKhoan == "admin" && matKhau == "123456"))
            {
                // Đăng nhập THÀNH CÔNG: Reset số lần đếm về 0
                Session["SoLanSai"] = 0;
                Session["TaiKhoan"] = taiKhoan;
                Response.Redirect("TrangChu.aspx");
            }
            else
            {
                // Đăng nhập THẤT BẠI: Tăng biến đếm
                int soLanSai = 0;
                if (Session["SoLanSai"] != null)
                {
                    soLanSai = (int)Session["SoLanSai"];
                }

                soLanSai++; // Cộng thêm 1 lần sai
                Session["SoLanSai"] = soLanSai; // Lưu ngược lại vào Session

                // Kiểm tra xem đã vượt mốc 5 lần chưa
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

        // ĐÂY LÀ HÀM BỊ THIẾU GÂY RA LỖI CỦA BẠN:
        // Hàm phụ dùng để khóa form cho gọn code
        private void KhoaDangNhap()
        {
            lblThongBaoLoi.Text = "Tài khoản của bạn đã bị cấm đăng nhập do nhập sai quá 5 lần!";
            lblThongBaoLoi.ForeColor = System.Drawing.Color.Red;

            // Vô hiệu hóa nút đăng nhập và ô nhập liệu
            btnDangNhap.Enabled = false;
            btnDangNhap.BackColor = System.Drawing.Color.Gray;
            txtTaiKhoan.Enabled = false;
            txtMatKhau.Enabled = false;
        }
    }
}