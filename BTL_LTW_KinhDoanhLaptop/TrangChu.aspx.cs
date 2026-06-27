using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class TrangChu : System.Web.UI.Page
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
            CapNhatSoLuongGio();

            if (!IsPostBack)
            {
                if (Application["DanhSachLaptop"] != null)
                {
                    List<Laptop> danhSach = (List<Laptop>)Application["DanhSachLaptop"];
                    string search = Request.QueryString["search"];
                    string brand = Request.QueryString["brand"];
                    
                    if (!string.IsNullOrEmpty(search))
                    {
                        search = search.ToLower();
                        danhSach = danhSach.Where(x => x.TenSanPham.ToLower().Contains(search)).ToList();
                    }
                    
                    if (!string.IsNullOrEmpty(brand))
                    {
                        brand = brand.ToLower();
                        danhSach = danhSach.Where(x => x.TenSanPham.ToLower().Contains(brand)).ToList();
                    }
                    
                    rptLaptops.DataSource = danhSach;
                    rptLaptops.DataBind();
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

        protected void btnThemGio_Click(object sender, EventArgs e)
        {
            if (Session["TaiKhoan"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }
            int maSP = int.Parse(((LinkButton)sender).CommandArgument);
            List<Laptop> danhSach = (List<Laptop>)Application["DanhSachLaptop"];
            Laptop spChon = danhSach.FirstOrDefault(x => x.Id == maSP);

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
                if (int.Parse(dr["MaSanPham"].ToString()) == maSP)
                {
                    dr["SoLuong"] = int.Parse(dr["SoLuong"].ToString()) + 1;
                    dr["ThanhTien"] = int.Parse(dr["SoLuong"].ToString()) * spChon.GiaTien;
                    daCo = true;
                    break;
                }
            }

            if (!daCo)
            {
                gioHang.Rows.Add(maSP, spChon.TenSanPham, 1, spChon.GiaTien, spChon.GiaTien);
            }

            Session["GioHang"] = gioHang;
            CapNhatSoLuongGio();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "toast", "showToast('Thêm giỏ hàng thành công!');", true);
        }
    }
}