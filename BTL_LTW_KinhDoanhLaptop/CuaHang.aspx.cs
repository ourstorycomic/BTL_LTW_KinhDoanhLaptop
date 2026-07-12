using BTL_LTW_KinhDoanhLaptop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class CuaHang : System.Web.UI.Page
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
                LoadTatCaSanPham();
            }
        }

        private void LoadTatCaSanPham()
        {
            List<Laptop> danhSachGoc = Application["DanhSachLaptop"] as List<Laptop>;

            if (danhSachGoc != null)
            {
                string tuKhoa = Request.QueryString["search"];
                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    danhSachGoc = danhSachGoc.Where(x => x.TenSanPham.ToLower().Contains(tuKhoa.ToLower())).ToList();
                }
                rptLaptops.DataSource = danhSachGoc;
                rptLaptops.DataBind();
            }
        }

        protected void BoLoc_Changed(object sender, EventArgs e)
        {
            List<Laptop> danhSachGoc = Application["DanhSachLaptop"] as List<Laptop>;
            if (danhSachGoc == null) return;
            IEnumerable<Laptop> ketQuaLoc = danhSachGoc;

            List<string> cacHangDuocChon = new List<string>();
            foreach (ListItem item in cblThuongHieu.Items)
            {
                if (item.Selected)
                {
                    cacHangDuocChon.Add(item.Value);
                }
            }
            if (cacHangDuocChon.Count > 0)
            {
                ketQuaLoc = ketQuaLoc.Where(sp => cacHangDuocChon.Any(hang => sp.TenSanPham.ToLower().Contains(hang)));
            }
            
            string mucGia = rblMucGia.SelectedValue;
            if (mucGia == "1") 
            {
                ketQuaLoc = ketQuaLoc.Where(sp => sp.GiaTien < 15000000);
            }
            else if (mucGia == "2")
            {
                ketQuaLoc = ketQuaLoc.Where(sp => sp.GiaTien >= 15000000 && sp.GiaTien <= 25000000);
            }
            else if (mucGia == "3") 
            {
                ketQuaLoc = ketQuaLoc.Where(sp => sp.GiaTien > 25000000);
            }
            
            string kieuSapXep = ddlSapXep.SelectedValue;
            if (kieuSapXep == "asc") 
            {
                ketQuaLoc = ketQuaLoc.OrderBy(sp => sp.GiaTien);
            }
            else if (kieuSapXep == "desc") 
            {
                ketQuaLoc = ketQuaLoc.OrderByDescending(sp => sp.GiaTien);
            }
            else 
            {
                ketQuaLoc = ketQuaLoc.OrderByDescending(sp => sp.Id);
            }

            rptLaptops.DataSource = ketQuaLoc.ToList();
            rptLaptops.DataBind();
        }

        protected void btnMua_Click(object sender, EventArgs e)
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
    
        private void CapNhatSoLuongGio()
        {
            if (Session["GioHang"] != null)
            {
                System.Data.DataTable dt = (System.Data.DataTable)Session["GioHang"];
                int tong = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    tong += int.Parse(dr["SoLuong"].ToString());
                }
                lblSoLuongGio.Text = tong.ToString();
            }
        }
            }
}
