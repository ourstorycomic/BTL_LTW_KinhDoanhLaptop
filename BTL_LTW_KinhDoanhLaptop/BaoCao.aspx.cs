using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class BaoCao : System.Web.UI.Page
    {
        public int TongSanPham = 0;
        public int TongSoLuongTon = 0;
        public decimal TongGiaTriKho = 0;
        public int TongTaiKhoan = 0;
        public string SanPhamBanChay = "Chưa có dữ liệu";

        
        
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
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() != "admin")
            {
                Response.Redirect("TrangChu.aspx");
                return;
            }

            if (Application["DanhSachLaptop"] != null)
            {
                var danhSach = (List<Laptop>)Application["DanhSachLaptop"];
                TongSanPham = danhSach.Count;
                TongSoLuongTon = 0;
                TongGiaTriKho = 0;
                Laptop bestSeller = null;
                
                foreach (Laptop sp in danhSach)
                {
                    TongSoLuongTon += sp.SoLuongTon;
                    TongGiaTriKho += (sp.SoLuongTon * sp.GiaTien);
                    if (bestSeller == null) 
                    {
                        bestSeller = sp;
                    }
                    else if (sp.SoLuongBan > bestSeller.SoLuongBan)
                    {
                        bestSeller = sp;
                    }
                }
                

                if (bestSeller != null && bestSeller.SoLuongBan > 0)
                {
                    SanPhamBanChay = bestSeller.TenSanPham + " (" + bestSeller.SoLuongBan + ")";
                }
                
                string htmlTable = "";
                foreach(Laptop sp in danhSach)
                {
                    if (sp.SoLuongBan > 0)
                    {
                        htmlTable += "<tr>";
                        htmlTable += "<td><img src='" + ResolveUrl(sp.HinhAnh) + "' style='width: 50px; height: 50px; object-fit: contain;' /></td>";
                        htmlTable += "<td>" + sp.TenSanPham + "</td>";
                        htmlTable += "<td>" + string.Format("{0:N0} ₫", sp.GiaTien) + "</td>";
                        htmlTable += "<td>" + sp.SoLuongBan + "</td>";
                        htmlTable += "<td>" + string.Format("{0:N0} ₫", sp.GiaTien * sp.SoLuongBan) + "</td>";
                        htmlTable += "</tr>";
                    }
                }
                if (htmlTable == "")
                {
                    htmlTable = "<tr><td colspan='5' style='text-align:center;'>Chưa có sản phẩm nào được bán ra.</td></tr>";
                }
                tbodyThongKe.InnerHtml = htmlTable;

            }

            if (Application["DanhSachTaiKhoan"] != null)
            {
                var tk = (Dictionary<string, NguoiDung>)Application["DanhSachTaiKhoan"];
                TongTaiKhoan = tk.Count;
            }

            // Also update cart info in header just like TrangChu
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
    }
}
