using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class BaoCao : System.Web.UI.Page
    {
        public int TongSanPham = 0;
        public int TongSoLuongTon = 0;
        public decimal TongGiaTriKho = 0;
        public int TongTaiKhoan = 0;
        public string SanPhamBanChay = "Chưa có dữ liệu";

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
                List<Laptop> danhSach = (List<Laptop>)Application["DanhSachLaptop"];

                TongSanPham = danhSach.Count;

                int maxBan = 0;
                string htmlTable = "";

                for (int i = 0; i < danhSach.Count; i++)
                {
                    Laptop sp = danhSach[i];

                    TongSoLuongTon += sp.SoLuongTon;
                    TongGiaTriKho += (sp.SoLuongTon * sp.GiaTien);

                    if (sp.SoLuongBan > maxBan)
                    {
                        maxBan = sp.SoLuongBan;
                        SanPhamBanChay = sp.TenSanPham + " (" + maxBan + ")";
                    }

                    if (sp.SoLuongBan > 0)
                    {
                        decimal doanhThu = sp.GiaTien * sp.SoLuongBan;

                        htmlTable += "<tr>";
                        htmlTable += "<td><img src='" + ResolveUrl(sp.HinhAnh) + "' style='width: 50px; height: 50px; object-fit: contain;' /></td>";
                        htmlTable += "<td>" + sp.TenSanPham + "</td>";
                        htmlTable += "<td>" + sp.GiaTien.ToString("N0") + " ₫</td>";
                        htmlTable += "<td>" + sp.SoLuongBan + "</td>";
                        htmlTable += "<td>" + doanhThu.ToString("N0") + " ₫</td>";
                        htmlTable += "</tr>";
                    }
                }

                if (htmlTable == "")
                {
                    htmlTable = "<tr><td colspan='5' style='text-align:center;'>Chưa có sản phẩm nào được bán ra.</td></tr>";
                }

                tbodyThongKe.InnerHtml = htmlTable;
                
                pTongSanPham.InnerText = TongSanPham.ToString();
                pTongSoLuongTon.InnerText = TongSoLuongTon.ToString();
                pTongGiaTriKho.InnerText = TongGiaTriKho.ToString("N0") + " ₫";
                pSanPhamBanChay.InnerText = SanPhamBanChay;
            }

            if (Application["DanhSachTaiKhoan"] != null)
            {
                Dictionary<string, NguoiDung> tk = (Dictionary<string, NguoiDung>)Application["DanhSachTaiKhoan"];
                TongTaiKhoan = tk.Count;
                pTongTaiKhoan.InnerText = TongTaiKhoan.ToString();
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

                string tkDangNhap = Session["TaiKhoan"].ToString();
                lblTenTaiKhoan.InnerText = tkDangNhap;

                if (Application["DanhSachTaiKhoan"] != null)
                {
                    Dictionary<string, NguoiDung> dict = (Dictionary<string, NguoiDung>)Application["DanhSachTaiKhoan"];

                    if (dict.ContainsKey(tkDangNhap))
                    {
                        if (dict[tkDangNhap].Avatar != null && dict[tkDangNhap].Avatar != "")
                        {
                            imgAvatar.Src = dict[tkDangNhap].Avatar;
                        }
                    }
                }

                if (tkDangNhap == "admin")
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
    }
}