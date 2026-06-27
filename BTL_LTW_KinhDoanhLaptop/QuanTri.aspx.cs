using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class QuanTri : System.Web.UI.Page
    {
        
        
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

            
            

            
            if (!IsPostBack)
            {
                if (Request.QueryString["action"] == "delete" && Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    List<Laptop> danhSach = (List<Laptop>)Application["DanhSachLaptop"];
                    Laptop spXoa = null;
                    foreach(Laptop sp in danhSach) {
                        if(sp.Id == id) {
                            spXoa = sp;
                            break;
                        }
                    }
                    if (spXoa != null)
                    {
                        danhSach.Remove(spXoa);
                        Application["DanhSachLaptop"] = danhSach;
                    }
                    Response.Redirect("QuanTri.aspx");
                }
                else if (Request.QueryString["action"] == "edit" && Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    List<Laptop> danhSach = (List<Laptop>)Application["DanhSachLaptop"];
                    Laptop spSua = null;
                    foreach(Laptop sp in danhSach) {
                        if(sp.Id == id) {
                            spSua = sp;
                            break;
                        }
                    }
                    if (spSua != null)
                    {
                        txtId.Value = spSua.Id.ToString();
                        txtTenSanPham.Text = spSua.TenSanPham;
                        txtGiaTien.Text = spSua.GiaTien.ToString();
                        txtHinhAnhCu.Value = spSua.HinhAnh;
                        imgPreview.ImageUrl = spSua.HinhAnh;
                        imgPreview.Visible = true;
                        txtSoLuongTon.Text = spSua.SoLuongTon.ToString();
                    }
                }
                LoadData();
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

        private void LoadData()
        {
            if (Application["DanhSachLaptop"] != null)
            {
                var danhSach = (List<Laptop>)Application["DanhSachLaptop"];
                string html = "";
                foreach (var sp in danhSach)
                {
                    html += "<tr>";
                    html += "<td>" + sp.Id + "</td>";
                    html += "<td>" + sp.TenSanPham + "</td>";
                    html += "<td>" + string.Format("{0:N0} ₫", sp.GiaTien) + "</td>";
                    html += "<td>" + sp.SoLuongTon + "</td>";
                    html += "<td><img src='" + ResolveUrl(sp.HinhAnh) + "' style='width: 50px; height: 50px; object-fit: contain;' /></td>";
                    html += "<td>";
                    html += "<a href='QuanTri.aspx?action=edit&id=" + sp.Id + "' style='color: blue; margin-right: 10px; text-decoration: none; font-weight: bold;'>Sửa</a>";
                    html += "<a href='QuanTri.aspx?action=delete&id=" + sp.Id + "' style='color: red; text-decoration: none; font-weight: bold;' onclick=\"return confirm('Bạn có chắc muốn xóa sản phẩm này?');\">Xóa</a>";
                    html += "</td>";
                    html += "</tr>";
                }
                tbodyProducts.InnerHtml = html;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<Laptop> danhSach = (List<Laptop>)Application["DanhSachLaptop"];
            if (danhSach == null) danhSach = new List<Laptop>();

            int id = string.IsNullOrEmpty(txtId.Value) ? 0 : int.Parse(txtId.Value);
            
            string hinhAnh = txtHinhAnhCu.Value;
            if (fileHinhAnh.HasFile)
            {
                string fileName = System.IO.Path.GetFileName(fileHinhAnh.FileName);
                string filePath = Server.MapPath("~/assets/img/" + fileName);
                fileHinhAnh.SaveAs(filePath);
                hinhAnh = "assets/img/" + fileName;
            }
            else if (string.IsNullOrEmpty(hinhAnh))
            {
                hinhAnh = "assets/img/lenovo.png";
            }

            if (id == 0) // Add new
            {
                int newId = 1;
                if (danhSach.Count > 0)
                {
                    int maxId = 0;
                    foreach (Laptop s in danhSach)
                    {
                        if (s.Id > maxId)
                        {
                            maxId = s.Id;
                        }
                    }
                    newId = maxId + 1;
                }
                Laptop sp = new Laptop
                {
                    Id = newId,
                    TenSanPham = txtTenSanPham.Text,
                    GiaTien = decimal.Parse(txtGiaTien.Text),
                    HinhAnh = hinhAnh,
                    SoLuongTon = int.Parse(txtSoLuongTon.Text)
                };
                danhSach.Add(sp);
            }
            else // Edit
            {
                Laptop sp = null;
                foreach (Laptop s in danhSach)
                {
                    if (s.Id == id)
                    {
                        sp = s;
                        break;
                    }
                }
                if (sp != null)
                {
                    sp.TenSanPham = txtTenSanPham.Text;
                    sp.GiaTien = decimal.Parse(txtGiaTien.Text);
                    sp.HinhAnh = hinhAnh;
                    sp.SoLuongTon = int.Parse(txtSoLuongTon.Text);
                }
            }

            Application["DanhSachLaptop"] = danhSach;
            ClearForm();
            LoadData();
        }

        

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtId.Value = "";
            txtTenSanPham.Text = "";
            txtGiaTien.Text = "";
            txtHinhAnhCu.Value = "";
            imgPreview.Visible = false;
            txtSoLuongTon.Text = "10";
        }
    }
}
