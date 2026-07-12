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
                SetFilterControlsFromQueryString();
                LoadTatCaSanPham();
            }
        }

        private void SetFilterControlsFromQueryString()
        {
            string brandQs = Request.QueryString["brand"];
            if (!string.IsNullOrEmpty(brandQs))
            {
                string[] brands = brandQs.Split(',');
                foreach (ListItem item in cblThuongHieu.Items)
                {
                    if (brands.Contains(item.Value))
                    {
                        item.Selected = true;
                    }
                }
            }

            string priceQs = Request.QueryString["price"];
            if (!string.IsNullOrEmpty(priceQs))
            {
                rblMucGia.SelectedValue = priceQs;
            }

            string sortQs = Request.QueryString["sort"];
            if (!string.IsNullOrEmpty(sortQs))
            {
                ddlSapXep.SelectedValue = sortQs;
            }
        }

        private void LoadTatCaSanPham()
        {
            List<Laptop> danhSachGoc = Application["DanhSachLaptop"] as List<Laptop>;

            if (danhSachGoc != null)
            {
                IEnumerable<Laptop> ketQuaLoc = danhSachGoc;

                string tuKhoa = Request.QueryString["search"];
                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    ketQuaLoc = ketQuaLoc.Where(x => x.TenSanPham.ToLower().Contains(tuKhoa.ToLower()));
                }

                string brandQs = Request.QueryString["brand"];
                if (!string.IsNullOrEmpty(brandQs))
                {
                    string[] brands = brandQs.Split(',');
                    ketQuaLoc = ketQuaLoc.Where(sp => brands.Any(b => sp.TenSanPham.ToLower().Contains(b.ToLower())));
                }

                string priceQs = Request.QueryString["price"];
                if (!string.IsNullOrEmpty(priceQs))
                {
                    if (priceQs == "1")
                        ketQuaLoc = ketQuaLoc.Where(sp => sp.GiaTien < 15000000);
                    else if (priceQs == "2")
                        ketQuaLoc = ketQuaLoc.Where(sp => sp.GiaTien >= 15000000 && sp.GiaTien <= 25000000);
                    else if (priceQs == "3")
                        ketQuaLoc = ketQuaLoc.Where(sp => sp.GiaTien > 25000000);
                }

                string sortQs = Request.QueryString["sort"];
                if (sortQs == "asc")
                    ketQuaLoc = ketQuaLoc.OrderBy(sp => sp.GiaTien);
                else if (sortQs == "desc")
                    ketQuaLoc = ketQuaLoc.OrderByDescending(sp => sp.GiaTien);
                else
                    ketQuaLoc = ketQuaLoc.OrderByDescending(sp => sp.Id);

                // --- Phân trang ---
                int pageSize = 6; 
                int currentPage = 1;
                string pageQs = Request.QueryString["page"];
                if (!string.IsNullOrEmpty(pageQs))
                {
                    int.TryParse(pageQs, out currentPage);
                }
                if (currentPage < 1) currentPage = 1;

                int totalItems = ketQuaLoc.Count();
                int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                if (currentPage > totalPages && totalPages > 0) currentPage = totalPages;

                var pagedData = ketQuaLoc.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

                rptLaptops.DataSource = pagedData;
                rptLaptops.DataBind();

                GeneratePaginationHtml(currentPage, totalPages);
            }
        }

        private void GeneratePaginationHtml(int currentPage, int totalPages)
        {
            if (totalPages <= 1)
            {
                litPhanTrang.Text = "";
                return;
            }

            string urlTemplate = Request.Url.AbsolutePath + "?";
            foreach (string key in Request.QueryString.AllKeys)
            {
                if (key != null && key.ToLower() != "page")
                {
                    urlTemplate += key + "=" + HttpUtility.UrlEncode(Request.QueryString[key]) + "&";
                }
            }
            urlTemplate += "page={0}";

            string html = "<div class='phan-trang'>";
            
            if (currentPage > 1)
            {
                html += $"<a href='{string.Format(urlTemplate, currentPage - 1)}'>❮ Trước</a>";
            }

            for (int i = 1; i <= totalPages; i++)
            {
                if (i == currentPage)
                {
                    html += $"<a href='#' class='active'>{i}</a>";
                }
                else
                {
                    html += $"<a href='{string.Format(urlTemplate, i)}'>{i}</a>";
                }
            }

            if (currentPage < totalPages)
            {
                html += $"<a href='{string.Format(urlTemplate, currentPage + 1)}'>Tiếp ❯</a>";
            }

            html += "</div>";
            litPhanTrang.Text = html;
        }

        protected void btnMua_Click(object sender, EventArgs e)
        {
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
