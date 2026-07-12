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
                    Dictionary<string, BTL_LTW_KinhDoanhLaptop.NguoiDung> dict = (Dictionary<string, BTL_LTW_KinhDoanhLaptop.NguoiDung>)Application["DanhSachTaiKhoan"];
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
            }
        }


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

                if (Application["DanhSachLaptop"] != null)
                {
                    List<Laptop> danhSach = (List<Laptop>)Application["DanhSachLaptop"];
                    string search = Request.QueryString["search"];

                    if (search != null && search != "")
                    {
                        List<Laptop> danhSachTimKiem = new List<Laptop>();
                        for (int i = 0; i < danhSach.Count; i++)
                        {
                            if (danhSach[i].TenSanPham.ToLower().Contains(search.ToLower()))
                            {
                                danhSachTimKiem.Add(danhSach[i]);
                            }
                        }

                        string htmlTimKiem = "";
                        for (int i = 0; i < danhSachTimKiem.Count; i++)
                        {
                            htmlTimKiem += "<div class='mot-san-pham'>";
                            htmlTimKiem += "<img src='" + ResolveUrl(danhSachTimKiem[i].HinhAnh) + "' alt='" + danhSachTimKiem[i].TenSanPham + "' class='hinh-anh-san-pham' />";
                            htmlTimKiem += "<div class='loai-san-pham'>LAPTOP CHÍNH HÃNG</div>";
                            htmlTimKiem += "<div class='ten-san-pham'>" + danhSachTimKiem[i].TenSanPham + "</div>";
                            htmlTimKiem += "<div class='gia-tien'>" + danhSachTimKiem[i].GiaTien.ToString("N0") + " ₫</div>";
                            htmlTimKiem += "<div class='hanh-dong-san-pham'>";
                            htmlTimKiem += "<a href='ChiTietSanPham.aspx?id=" + danhSachTimKiem[i].Id + "' class='btn-chi-tiet'>Chi tiết</a>";
                            htmlTimKiem += "<a href='TrangChu.aspx?addcart=" + danhSachTimKiem[i].Id + "' class='btn-gio-hang'>Giỏ hàng</a>";
                            htmlTimKiem += "</div></div>";
                        }
                        khungVanPhong.InnerHtml = htmlTimKiem;

                        if (danhSachTimKiem.Count > 0) divVanPhong.Visible = true; else divVanPhong.Visible = false;
                        divMongNhe.Visible = false;
                        divGaming.Visible = false;
                    }
                    else
                    {
                        List<Laptop> listGaming = new List<Laptop>();
                        List<Laptop> listMongNhe = new List<Laptop>();
                        List<Laptop> listVanPhong = new List<Laptop>();

                        for (int i = 0; i < danhSach.Count; i++)
                        {
                            string ten = danhSach[i].TenSanPham.ToLower();
                            bool isGaming = ten.Contains("gaming") || ten.Contains("nitro") || ten.Contains("legion") || ten.Contains("tuf") || ten.Contains("rog") || ten.Contains("msi") || ten.Contains("predator");
                            bool isMongNhe = ten.Contains("macbook") || ten.Contains("zenbook") || ten.Contains("xps") || ten.Contains("envy") || ten.Contains("swift") || ten.Contains("gram");

                            if (isGaming)
                            {
                                listGaming.Add(danhSach[i]);
                            }
                            else if (isMongNhe)
                            {
                                listMongNhe.Add(danhSach[i]);
                            }
                            else
                            {
                                listVanPhong.Add(danhSach[i]);
                            }
                        }

                        string htmlGaming = "";
                        for (int i = 0; i < listGaming.Count; i++)
                        {
                            htmlGaming += "<div class='mot-san-pham'>";
                            htmlGaming += "<img src='" + ResolveUrl(listGaming[i].HinhAnh) + "' alt='" + listGaming[i].TenSanPham + "' class='hinh-anh-san-pham' />";
                            htmlGaming += "<div class='loai-san-pham'>LAPTOP CHÍNH HÃNG</div>";
                            htmlGaming += "<div class='ten-san-pham'>" + listGaming[i].TenSanPham + "</div>";
                            htmlGaming += "<div class='gia-tien'>" + listGaming[i].GiaTien.ToString("N0") + " ₫</div>";
                            htmlGaming += "<div class='hanh-dong-san-pham'>";
                            htmlGaming += "<a href='ChiTietSanPham.aspx?id=" + listGaming[i].Id + "' class='btn-chi-tiet'>Chi tiết</a>";
                            htmlGaming += "<a href='TrangChu.aspx?addcart=" + listGaming[i].Id + "' class='btn-gio-hang'>Giỏ hàng</a>";
                            htmlGaming += "</div></div>";
                        }
                        khungGaming.InnerHtml = htmlGaming;
                        if (listGaming.Count > 0) divGaming.Visible = true; else divGaming.Visible = false;

                        string htmlMongNhe = "";
                        for (int i = 0; i < listMongNhe.Count; i++)
                        {
                            htmlMongNhe += "<div class='mot-san-pham'>";
                            htmlMongNhe += "<img src='" + ResolveUrl(listMongNhe[i].HinhAnh) + "' alt='" + listMongNhe[i].TenSanPham + "' class='hinh-anh-san-pham' />";
                            htmlMongNhe += "<div class='loai-san-pham'>LAPTOP CHÍNH HÃNG</div>";
                            htmlMongNhe += "<div class='ten-san-pham'>" + listMongNhe[i].TenSanPham + "</div>";
                            htmlMongNhe += "<div class='gia-tien'>" + listMongNhe[i].GiaTien.ToString("N0") + " ₫</div>";
                            htmlMongNhe += "<div class='hanh-dong-san-pham'>";
                            htmlMongNhe += "<a href='ChiTietSanPham.aspx?id=" + listMongNhe[i].Id + "' class='btn-chi-tiet'>Chi tiết</a>";
                            htmlMongNhe += "<a href='TrangChu.aspx?addcart=" + listMongNhe[i].Id + "' class='btn-gio-hang'>Giỏ hàng</a>";
                            htmlMongNhe += "</div></div>";
                        }
                        khungMongNhe.InnerHtml = htmlMongNhe;
                        if (listMongNhe.Count > 0) divMongNhe.Visible = true; else divMongNhe.Visible = false;

                        string htmlVanPhong = "";
                        for (int i = 0; i < listVanPhong.Count; i++)
                        {
                            htmlVanPhong += "<div class='mot-san-pham'>";
                            htmlVanPhong += "<img src='" + ResolveUrl(listVanPhong[i].HinhAnh) + "' alt='" + listVanPhong[i].TenSanPham + "' class='hinh-anh-san-pham' />";
                            htmlVanPhong += "<div class='loai-san-pham'>LAPTOP CHÍNH HÃNG</div>";
                            htmlVanPhong += "<div class='ten-san-pham'>" + listVanPhong[i].TenSanPham + "</div>";
                            htmlVanPhong += "<div class='gia-tien'>" + listVanPhong[i].GiaTien.ToString("N0") + " ₫</div>";
                            htmlVanPhong += "<div class='hanh-dong-san-pham'>";
                            htmlVanPhong += "<a href='ChiTietSanPham.aspx?id=" + listVanPhong[i].Id + "' class='btn-chi-tiet'>Chi tiết</a>";
                            htmlVanPhong += "<a href='TrangChu.aspx?addcart=" + listVanPhong[i].Id + "' class='btn-gio-hang'>Giỏ hàng</a>";
                            htmlVanPhong += "</div></div>";
                        }
                        khungVanPhong.InnerHtml = htmlVanPhong;
                        if (listVanPhong.Count > 0) divVanPhong.Visible = true; else divVanPhong.Visible = false;
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
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tong = tong + int.Parse(dt.Rows[i]["SoLuong"].ToString());
                }
                lblSoLuongGio.Text = tong.ToString();
            }
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
                    gioHang.Rows[i]["SoLuong"] = int.Parse(gioHang.Rows[i]["SoLuong"].ToString()) + 1;
                    gioHang.Rows[i]["ThanhTien"] = int.Parse(gioHang.Rows[i]["SoLuong"].ToString()) * spChon.GiaTien;
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
    }
}