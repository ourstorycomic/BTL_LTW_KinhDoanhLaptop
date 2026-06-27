using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class ChiTietSanPham : System.Web.UI.Page
    {
        public Laptop spHienTai;

        
        
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
            LayThongTinSanPham();
            CapNhatSoLuongGio();
        }

        private void LayThongTinSanPham()
        {
            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                List<Laptop> danhSach = Application["DanhSachLaptop"] as List<Laptop>;
                if (danhSach != null)
                {
                    int idHienTai = int.Parse(id);
                    foreach (Laptop sp in danhSach)
                    {
                        if (sp.Id == idHienTai)
                        {
                            spHienTai = sp;
                            break;
                        }
                    }
                }
            }
        }

        private void CapNhatSoLuongGio()
        {
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

        private void ThemVaoGio()
        {
            if (spHienTai == null) return;

            DataTable gioHang;
            if (Session["GioHang"] == null)
            {
                gioHang = new DataTable();
                gioHang.Columns.Add("MaSanPham", typeof(int));
                gioHang.Columns.Add("TenSanPham", typeof(string));
                gioHang.Columns.Add("SoLuong", typeof(int));
                gioHang.Columns.Add("DonGia", typeof(decimal));
                gioHang.Columns.Add("ThanhTien", typeof(decimal));
            }
            else
            {
                gioHang = (DataTable)Session["GioHang"];
            }

            bool daCo = false;
            foreach (DataRow dr in gioHang.Rows)
            {
                if (int.Parse(dr["MaSanPham"].ToString()) == spHienTai.Id)
                {
                    dr["SoLuong"] = int.Parse(dr["SoLuong"].ToString()) + 1;
                    dr["ThanhTien"] = int.Parse(dr["SoLuong"].ToString()) * spHienTai.GiaTien;
                    daCo = true;
                    break;
                }
            }

            if (!daCo)
            {
                gioHang.Rows.Add(spHienTai.Id, spHienTai.TenSanPham, 1, spHienTai.GiaTien, spHienTai.GiaTien);
            }

            Session["GioHang"] = gioHang;
        }

        protected void btnThemVaoGio_Click(object sender, EventArgs e)
        {
            if (Session["TaiKhoan"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }
            ThemVaoGio();
            CapNhatSoLuongGio();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "toast", "showToast('Thêm giỏ hàng thành công!');", true);
        }

        protected void btnMuaNgay_Click(object sender, EventArgs e)
        {
            if (Session["TaiKhoan"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }
            if (spHienTai == null) return;

            DataTable dtMuaNgay = new DataTable();
            dtMuaNgay.Columns.Add("MaSanPham", typeof(int));
            dtMuaNgay.Columns.Add("TenSanPham", typeof(string));
            dtMuaNgay.Columns.Add("SoLuong", typeof(int));
            dtMuaNgay.Columns.Add("DonGia", typeof(decimal));
            dtMuaNgay.Columns.Add("ThanhTien", typeof(decimal));

            dtMuaNgay.Rows.Add(spHienTai.Id, spHienTai.TenSanPham, 1, spHienTai.GiaTien, spHienTai.GiaTien);
            Session["MuaNgay"] = dtMuaNgay;

            Response.Redirect("ThanhToan.aspx?type=buynow");
        }
    }
}