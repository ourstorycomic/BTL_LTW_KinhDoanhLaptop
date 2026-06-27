using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class HoSo : System.Web.UI.Page
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
            if (Session["TaiKhoan"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadData();
            }

            if (Session["GioHang"] != null)
            {
                DataTable dt = (DataTable)Session["GioHang"];
                int tong = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    tong += int.Parse(dr["SoLuong"].ToString());
                }
                lblSoLuongGio.Text = tong.ToString();
            }
        }

        private void LoadData()
        {
            if (Application["DanhSachTaiKhoan"] != null)
            {
                var dict = (Dictionary<string, NguoiDung>)Application["DanhSachTaiKhoan"];
                string tk = Session["TaiKhoan"].ToString();
                if (dict.ContainsKey(tk))
                {
                    NguoiDung nd = dict[tk];
                    txtTaiKhoan.Text = nd.TaiKhoan;
                    txtHoTen.Text = nd.HoTen;
                    txtEmail.Text = nd.Email;
                    txtSDT.Text = nd.SDT;
                    txtDiaChi.Text = nd.DiaChi;
                    imgPreview.ImageUrl = string.IsNullOrEmpty(nd.Avatar) ? "assets/img/lenovo.png" : nd.Avatar;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Application["DanhSachTaiKhoan"] != null)
            {
                var dict = (Dictionary<string, NguoiDung>)Application["DanhSachTaiKhoan"];
                string tk = Session["TaiKhoan"].ToString();
                if (dict.ContainsKey(tk))
                {
                    NguoiDung nd = dict[tk];
                    nd.HoTen = txtHoTen.Text;
                    nd.Email = txtEmail.Text;
                    nd.SDT = txtSDT.Text;
                    nd.DiaChi = txtDiaChi.Text;
                    
                    if (!string.IsNullOrEmpty(txtMatKhau.Text))
                    {
                        nd.MatKhau = txtMatKhau.Text;
                    }

                    if (fileAvatar.HasFile)
                    {
                        string fileName = Path.GetFileName(fileAvatar.FileName);
                        string filePath = Server.MapPath("~/assets/img/" + fileName);
                        fileAvatar.SaveAs(filePath);
                        nd.Avatar = "assets/img/" + fileName;
                        imgPreview.ImageUrl = nd.Avatar;
                    }

                    Application["DanhSachTaiKhoan"] = dict;
                    lblMessage.Text = "Cập nhật hồ sơ thành công!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toast", "showToast('Cập nhật hồ sơ thành công!');", true);
                    
                    // Reload header avatar by redirecting or it will reload on postback anyway
                    // but since the image might be cached, it's fine.
                }
            }
        }
    }
}
