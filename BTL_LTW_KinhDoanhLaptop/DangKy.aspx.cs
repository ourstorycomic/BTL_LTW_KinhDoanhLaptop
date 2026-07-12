using BTL_LTW_KinhDoanhLaptop;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class DangKy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HienThiTaiKhoan();

            if (!IsPostBack)
            {
                divThongBaoDK.InnerText = "";
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

        protected void btnDangKy_Click(object sender, EventArgs e)
        {
            string taiKhoan = Request.Form["txtTaiKhoanDK"];
            string hoTen = Request.Form["txtHoTenDK"];
            string sdt = Request.Form["txtSDTDK"];
            string email = Request.Form["txtEmailDK"];
            string matKhau = Request.Form["txtMatKhauDK"];
            string xacNhanMK = Request.Form["txtXacNhanMatKhau"];

            if (taiKhoan == null) taiKhoan = "";
            if (hoTen == null) hoTen = "";
            if (sdt == null) sdt = "";
            if (email == null) email = "";
            if (matKhau == null) matKhau = "";
            if (xacNhanMK == null) xacNhanMK = "";

            if (taiKhoan == "" || hoTen == "" || sdt == "" || email == "" || matKhau == "" || xacNhanMK == "")
            {
                divThongBaoDK.InnerText = "Vui lòng nhập đầy đủ thông tin!";
                return;
            }

            string mauTaiKhoan = @"^[a-zA-Z0-9]{4,20}$";
            if (Regex.IsMatch(taiKhoan, mauTaiKhoan) == false)
            {
                divThongBaoDK.InnerText = "Tên tài khoản phải từ 4 đến 20 ký tự, không chứa ký tự đặc biệt hoặc dấu cách!";
                return;
            }

            string mauHoTen = @"^[a-zA-ZÀ-ỹ\s]+$";
            if (Regex.IsMatch(hoTen, mauHoTen) == false)
            {
                divThongBaoDK.InnerText = "Họ tên chỉ được chứa chữ cái và khoảng trắng!";
                return;
            }

            string mauSDT = @"^0[0-9]{9}$";
            if (Regex.IsMatch(sdt, mauSDT) == false)
            {
                divThongBaoDK.InnerText = "Số điện thoại không hợp lệ! Vui lòng nhập đúng 10 số và bắt đầu bằng số 0.";
                return;
            }

            string mauEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            if (Regex.IsMatch(email, mauEmail) == false)
            {
                divThongBaoDK.InnerText = "Định dạng Email không hợp lệ! (Ví dụ đúng: abc@gmail.com)";
                return;
            }

            if (matKhau.Length < 6 || Regex.IsMatch(matKhau, @"[A-Z]") == false || Regex.IsMatch(matKhau, @"[\W_]") == false)
            {
                divThongBaoDK.InnerText = "Mật khẩu phải có tối thiểu 6 ký tự, gồm ít nhất 1 chữ in hoa và 1 ký tự đặc biệt!";
                return;
            }

            if (matKhau != xacNhanMK)
            {
                divThongBaoDK.InnerText = "Mật khẩu xác nhận không trùng khớp!";
                return;
            }

            Dictionary<string, NguoiDung> danhSachTaiKhoan;

            if (Application["DanhSachTaiKhoan"] == null)
            {
                danhSachTaiKhoan = new Dictionary<string, NguoiDung>();
            }
            else
            {
                danhSachTaiKhoan = (Dictionary<string, NguoiDung>)Application["DanhSachTaiKhoan"];
            }

            if (danhSachTaiKhoan.ContainsKey(taiKhoan))
            {
                divThongBaoDK.InnerText = "Tên đăng nhập này đã tồn tại. Vui lòng chọn tên khác!";
                return;
            }

            NguoiDung nguoiDungMoi = new NguoiDung();
            nguoiDungMoi.TaiKhoan = taiKhoan;
            nguoiDungMoi.MatKhau = matKhau;
            nguoiDungMoi.HoTen = hoTen;
            nguoiDungMoi.SDT = sdt;
            nguoiDungMoi.Email = email;
            nguoiDungMoi.DiaChi = "";

            danhSachTaiKhoan.Add(taiKhoan, nguoiDungMoi);
            Application["DanhSachTaiKhoan"] = danhSachTaiKhoan;

            string chuoiScript = "alert('Đăng ký tài khoản thành công!'); window.location.href='DangNhap.aspx';";
            ClientScript.RegisterStartupScript(this.GetType(), "RedirectScript", chuoiScript, true);
        }
    }
}