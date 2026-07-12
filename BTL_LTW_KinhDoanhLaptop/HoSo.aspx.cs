using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

                    nguoiDungHienTai.HoTen = txtHoTen.Text;
                    nguoiDungHienTai.Email = txtEmail.Text;
                    nguoiDungHienTai.SDT = txtSDT.Text;
                    nguoiDungHienTai.DiaChi = txtDiaChi.Text;

                    if (txtMatKhau.Text != "")
                    {
                        nguoiDungHienTai.MatKhau = txtMatKhau.Text;
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toast", "showToast('Cập nhật hồ sơ thành công!');", true);
                }
            }
        }
    }
}