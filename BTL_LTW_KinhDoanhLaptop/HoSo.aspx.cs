using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class HoSo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TaiKhoan"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }

            HienThiTaiKhoan();

            if (!IsPostBack)
            {
                TaiDuLieuHoSo();
                lblMessage.Text = "";
            }

            if (Session["GioHang"] != null)
            {
                DataTable dt = (DataTable)Session["GioHang"];
                int tongGioHang = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tongGioHang += Convert.ToInt32(dt.Rows[i]["SoLuong"]);
                }
                lblSoLuongGio.Text = tongGioHang.ToString();
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

        private void TaiDuLieuHoSo()
        {
            if (Application["DanhSachTaiKhoan"] != null)
            {
                Dictionary<string, NguoiDung> tuDienTaiKhoan = (Dictionary<string, NguoiDung>)Application["DanhSachTaiKhoan"];
                string tenTaiKhoan = Session["TaiKhoan"].ToString();

                if (tuDienTaiKhoan.ContainsKey(tenTaiKhoan))
                {
                    NguoiDung nguoiDungHienTai = tuDienTaiKhoan[tenTaiKhoan];

                    txtTaiKhoan.Text = nguoiDungHienTai.TaiKhoan;
                    txtHoTen.Text = nguoiDungHienTai.HoTen;
                    txtEmail.Text = nguoiDungHienTai.Email;
                    txtSDT.Text = nguoiDungHienTai.SDT;
                    txtDiaChi.Text = nguoiDungHienTai.DiaChi;

                    if (nguoiDungHienTai.Avatar == null || nguoiDungHienTai.Avatar == "")
                    {
                        imgPreview.ImageUrl = "assets/img/lenovo.png";
                    }
                    else
                    {
                        imgPreview.ImageUrl = nguoiDungHienTai.Avatar;
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Application["DanhSachTaiKhoan"] != null)
            {
                Dictionary<string, NguoiDung> tuDienTaiKhoan = (Dictionary<string, NguoiDung>)Application["DanhSachTaiKhoan"];
                string tenTaiKhoan = Session["TaiKhoan"].ToString();

                if (tuDienTaiKhoan.ContainsKey(tenTaiKhoan))
                {
                    NguoiDung nguoiDungHienTai = tuDienTaiKhoan[tenTaiKhoan];

                    string hoTenMoi = txtHoTen.Text.Trim();
                    string emailMoi = txtEmail.Text.Trim();
                    string sdtMoi = txtSDT.Text.Trim();
                    string matKhauMoi = txtMatKhau.Text.Trim();

                    if (hoTenMoi == "")
                    {
                        lblMessage.Text = "Vui lòng nhập Họ tên!";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    if (emailMoi == "")
                    {
                        lblMessage.Text = "Vui lòng nhập Email!";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    if (sdtMoi == "")
                    {
                        lblMessage.Text = "Vui lòng nhập số điện thoại!";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    string mauChuVaKhoangTrang = @"^[a-zA-ZÀ-ỹ\s]+$";
                    if (Regex.IsMatch(hoTenMoi, mauChuVaKhoangTrang) == false)
                    {
                        lblMessage.Text = "Họ tên không hợp lệ! Chỉ được chứa chữ cái và khoảng trắng.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    string mauEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                    if (Regex.IsMatch(emailMoi, mauEmail) == false)
                    {
                        lblMessage.Text = "Định dạng Email không hợp lệ! (Ví dụ đúng: abc@gmail.com)";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    string mauSDT = @"^0[0-9]{9}$";
                    if (Regex.IsMatch(sdtMoi, mauSDT) == false)
                    {
                        lblMessage.Text = "Số điện thoại không hợp lệ! Phải có đúng 10 số và bắt đầu bằng số 0.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    if (matKhauMoi != "")
                    {
                        if (matKhauMoi.Length < 6 || Regex.IsMatch(matKhauMoi, @"[A-Z]") == false || Regex.IsMatch(matKhauMoi, @"[\W_]") == false)
                        {
                            lblMessage.Text = "Mật khẩu mới phải có tối thiểu 6 ký tự, gồm ít nhất 1 chữ in hoa và 1 ký tự đặc biệt!";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                    }

                    nguoiDungHienTai.HoTen = hoTenMoi;
                    nguoiDungHienTai.Email = emailMoi;
                    nguoiDungHienTai.SDT = sdtMoi;
                    nguoiDungHienTai.DiaChi = txtDiaChi.Text.Trim();

                    if (matKhauMoi != "")
                    {
                        nguoiDungHienTai.MatKhau = matKhauMoi;
                    }

                    if (fileAvatar.HasFile == true)
                    {
                        string tenFile = Path.GetFileName(fileAvatar.FileName);
                        string duongDanLuuFile = Server.MapPath("~/assets/img/" + tenFile);

                        fileAvatar.SaveAs(duongDanLuuFile);

                        nguoiDungHienTai.Avatar = "assets/img/" + tenFile;
                        imgPreview.ImageUrl = nguoiDungHienTai.Avatar;
                    }

                    Application["DanhSachTaiKhoan"] = tuDienTaiKhoan;
                    lblMessage.Text = "Cập nhật hồ sơ thành công!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toast", "showToast('Cập nhật hồ sơ thành công!');", true);
                }
            }
        }
    }
}