using BTL_LTW_KinhDoanhLaptop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class CuaHang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HienThiTaiKhoan();
            CapNhatSoLuongGio();

            if (!IsPostBack)
            {
                string addcart = Request.QueryString["addcart"];
                if (addcart != null && addcart != "")
                {
                    int idSP = int.Parse(addcart);
                    ThemVaoGioHang(idSP);
                    
                    string urlTemplate = Request.Url.AbsolutePath + "?";
                    for (int i = 0; i < Request.QueryString.Count; i++)
                    {
                        string key = Request.QueryString.Keys[i];
                        if (key != null && key.ToLower() != "addcart")
                        {
                            urlTemplate += key + "=" + HttpUtility.UrlEncode(Request.QueryString[key]) + "&";
                        }
                    }
                    urlTemplate += "added=1";
                    Response.Redirect(urlTemplate);
                }

                if (Request.QueryString["added"] == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toast", "showToast('Thêm giỏ hàng thành công!');", true);
                }

                SetFilterControlsFromQueryString();
                LoadTatCaSanPham();
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

                string tk = Session["TaiKhoan"].ToString();
                lblTenTaiKhoan.InnerText = tk;

                if (Application["DanhSachTaiKhoan"] != null)
                {
                    Dictionary<string, NguoiDung> dict = (Dictionary<string, NguoiDung>)Application["DanhSachTaiKhoan"];

                    if (dict.ContainsKey(tk))
                    {
                        if (dict[tk].Avatar != null && dict[tk].Avatar != "")
                        {
                            imgAvatar.Src = dict[tk].Avatar;
                        }
                    }
                }

                if (tk == "admin")
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

        private void SetFilterControlsFromQueryString()
        {
            string brandQs = Request.QueryString["brand"];
            if (brandQs != null && brandQs != "")
            {
                string[] brands = brandQs.Split(',');
                for (int i = 0; i < cblThuongHieu.Items.Count; i++)
                {
                    for (int j = 0; j < brands.Length; j++)
                    {
                        if (cblThuongHieu.Items[i].Value == brands[j])
                        {
                            cblThuongHieu.Items[i].Selected = true;
                            break;
                        }
                    }
                }
            }

            string priceQs = Request.QueryString["price"];
            if (priceQs != null && priceQs != "")
            {
                rblMucGia.SelectedValue = priceQs;
            }

            string sortQs = Request.QueryString["sort"];
            if (sortQs != null && sortQs != "")
            {
                ddlSapXep.SelectedValue = sortQs;
            }
        }

        private void LoadTatCaSanPham()
        {
            if (Application["DanhSachLaptop"] != null)
            {
                List<Laptop> danhSachGoc = (List<Laptop>)Application["DanhSachLaptop"];
                List<Laptop> ketQuaLoc = new List<Laptop>();

                for (int i = 0; i < danhSachGoc.Count; i++)
                {
                    ketQuaLoc.Add(danhSachGoc[i]);
                }

                string tuKhoa = Request.QueryString["search"];
                if (tuKhoa != null && tuKhoa != "")
                {
                    List<Laptop> tam = new List<Laptop>();
                    for (int i = 0; i < ketQuaLoc.Count; i++)
                    {
                        if (ketQuaLoc[i].TenSanPham.ToLower().Contains(tuKhoa.ToLower()))
                        {
                            tam.Add(ketQuaLoc[i]);
                        }
                    }
                    ketQuaLoc = tam;
                }

                string brandQs = Request.QueryString["brand"];
                if (brandQs != null && brandQs != "")
                {
                    string[] brands = brandQs.Split(',');
                    List<Laptop> tam = new List<Laptop>();
                    for (int i = 0; i < ketQuaLoc.Count; i++)
                    {
                        bool cothuonghieu = false;
                        for (int j = 0; j < brands.Length; j++)
                        {
                            if (ketQuaLoc[i].TenSanPham.ToLower().Contains(brands[j].ToLower()))
                            {
                                cothuonghieu = true;
                                break;
                            }
                        }
                        if (cothuonghieu)
                        {
                            tam.Add(ketQuaLoc[i]);
                        }
                    }
                    ketQuaLoc = tam;
                }

                string priceQs = Request.QueryString["price"];
                if (priceQs != null && priceQs != "")
                {
                    List<Laptop> tam = new List<Laptop>();
                    for (int i = 0; i < ketQuaLoc.Count; i++)
                    {
                        if (priceQs == "1" && ketQuaLoc[i].GiaTien < 15000000)
                        {
                            tam.Add(ketQuaLoc[i]);
                        }
                        else if (priceQs == "2" && ketQuaLoc[i].GiaTien >= 15000000 && ketQuaLoc[i].GiaTien <= 25000000)
                        {
                            tam.Add(ketQuaLoc[i]);
                        }
                        else if (priceQs == "3" && ketQuaLoc[i].GiaTien > 25000000)
                        {
                            tam.Add(ketQuaLoc[i]);
                        }
                    }
                    ketQuaLoc = tam;
                }

                string sortQs = Request.QueryString["sort"];
                for (int i = 0; i < ketQuaLoc.Count - 1; i++)
                {
                    for (int j = i + 1; j < ketQuaLoc.Count; j++)
                    {
                        bool canDoiCho = false;
                        if (sortQs == "asc")
                        {
                            if (ketQuaLoc[i].GiaTien > ketQuaLoc[j].GiaTien) canDoiCho = true;
                        }
                        else if (sortQs == "desc")
                        {
                            if (ketQuaLoc[i].GiaTien < ketQuaLoc[j].GiaTien) canDoiCho = true;
                        }
                        else
                        {
                            if (ketQuaLoc[i].Id < ketQuaLoc[j].Id) canDoiCho = true;
                        }
                        if (canDoiCho)
                        {
                            Laptop temp = ketQuaLoc[i];
                            ketQuaLoc[i] = ketQuaLoc[j];
                            ketQuaLoc[j] = temp;
                        }
                    }
                }

                int pageSize = 6;
                int currentPage = 1;
                string pageQs = Request.QueryString["page"];
                if (pageQs != null && pageQs != "")
                {
                    try
                    {
                        currentPage = int.Parse(pageQs);
                    }
                    catch
                    {
                        currentPage = 1;
                    }
                }

                if (currentPage < 1) currentPage = 1;
                int totalItems = ketQuaLoc.Count;
                int totalPages = totalItems / pageSize;
                if (totalItems % pageSize != 0) totalPages++;
                if (currentPage > totalPages && totalPages > 0) currentPage = totalPages;

                List<Laptop> pagedData = new List<Laptop>();
                int viTriBatDau = (currentPage - 1) * pageSize;
                for (int i = viTriBatDau; i < viTriBatDau + pageSize; i++)
                {
                    if (i < ketQuaLoc.Count)
                    {
                        pagedData.Add(ketQuaLoc[i]);
                    }
                }

                string htmlSP = "";
                for (int i = 0; i < pagedData.Count; i++)
                {
                    htmlSP += "<div class='the-san-pham-chuan'>";
                    htmlSP += "<div class='khung-anh-sp'>";
                    htmlSP += "<img src='" + ResolveUrl(pagedData[i].HinhAnh) + "' alt='" + pagedData[i].TenSanPham + "' />";
                    htmlSP += "</div>";
                    htmlSP += "<div class='thong-tin-sp'>";
                    htmlSP += "<div class='loai-sp'>LAPTOP CHÍNH HÃNG</div>";
                    htmlSP += "<h4 class='ten-sp'>" + pagedData[i].TenSanPham + "</h4>";
                    htmlSP += "<div class='gia-sp'>" + pagedData[i].GiaTien.ToString("N0") + " ₫</div>";
                    htmlSP += "<div class='hanh-dong-sp'>";
                    htmlSP += "<a href='ChiTietSanPham.aspx?id=" + pagedData[i].Id + "' class='btn-xem'>Xem</a>";
                    htmlSP += "<a href='CuaHang.aspx?addcart=" + pagedData[i].Id + "' class='btn-mua'>Thêm giỏ hàng</a>";
                    htmlSP += "</div></div></div>";
                }
                khungDanhSachSP.InnerHtml = htmlSP;

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
            for (int i = 0; i < Request.QueryString.Count; i++)
            {
                string key = Request.QueryString.Keys[i];
                if (key != null && key.ToLower() != "page")
                {
                    urlTemplate += key + "=" + HttpUtility.UrlEncode(Request.QueryString[key]) + "&";
                }
            }
            urlTemplate += "page={0}";

            string html = "<div class='phan-trang'>";
            if (currentPage > 1)
            {
                string linkTruoc = string.Format(urlTemplate, currentPage - 1);
                html += "<a href='" + linkTruoc + "'>❮ Trước</a>";
            }
            for (int i = 1; i <= totalPages; i++)
            {
                if (i == currentPage)
                {
                    html += "<a href='#' class='active'>" + i + "</a>";
                }
                else
                {
                    string linkSo = string.Format(urlTemplate, i);
                    html += "<a href='" + linkSo + "'>" + i + "</a>";
                }
            }
            if (currentPage < totalPages)
            {
                string linkTiep = string.Format(urlTemplate, currentPage + 1);
                html += "<a href='" + linkTiep + "'>Tiếp ❯</a>";
            }

            html += "</div>";
            litPhanTrang.Text = html;
        }

        private void ThemVaoGioHang(int maSP)
        {
            List<Laptop> danhSach = (List<Laptop>)Application["DanhSachLaptop"];
            if (danhSach == null) return;
            Laptop spChon = null;

            for (int i = 0; i < danhSach.Count; i++)
            {
                if (danhSach[i].Id == maSP)
                {
                    spChon = danhSach[i];
                    break;
                }
            }

            if (spChon == null) return;

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
            for (int i = 0; i < gioHang.Rows.Count; i++)
            {
                if (int.Parse(gioHang.Rows[i]["MaSanPham"].ToString()) == maSP)
                {
                    int slMoi = int.Parse(gioHang.Rows[i]["SoLuong"].ToString()) + 1;
                    gioHang.Rows[i]["SoLuong"] = slMoi;
                    gioHang.Rows[i]["ThanhTien"] = slMoi * spChon.GiaTien;
                    daCo = true;
                    break;
                }
            }

            if (daCo == false)
            {
                gioHang.Rows.Add(maSP, spChon.TenSanPham, 1, spChon.GiaTien, spChon.GiaTien);
            }

            Session["GioHang"] = gioHang;
        }

        private void CapNhatSoLuongGio()
        {
            if (Session["GioHang"] != null)
            {
                System.Data.DataTable dt = (System.Data.DataTable)Session["GioHang"];
                int tong = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tong += int.Parse(dt.Rows[i]["SoLuong"].ToString());
                }
                lblSoLuongGio.Text = tong.ToString();
            }
        }
    }
}