using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class ChiTietSanPham : System.Web.UI.Page
    {
        public Laptop spHienTai;

        protected void Page_Load(object sender, EventArgs e)
        {
            HienThiTaiKhoan();
            LayThongTinSanPham();
            CapNhatSoLuongGio();
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

        private void LayThongTinSanPham()
        {
            string id = Request.QueryString["id"];

            if (id != null && id != "")
            {
                if (Application["DanhSachLaptop"] != null)
                {
                    List<Laptop> danhSach = (List<Laptop>)Application["DanhSachLaptop"];
                    int idHienTai = int.Parse(id);

                    for (int i = 0; i < danhSach.Count; i++)
                    {
                        if (danhSach[i].Id == idHienTai)
                        {
                            spHienTai = danhSach[i];
                            break;
                        }
                    }
                }
            }

            if (spHienTai != null)
            {
                divChiTietSanPham.Visible = true;
                divKhongTimThay.Visible = false;

                lblBreadcrumbTen.InnerText = spHienTai.TenSanPham;
                imgAnhLon.Src = ResolveUrl(spHienTai.HinhAnh);
                imgAnhLon.Alt = spHienTai.TenSanPham;
                divTenSP.InnerText = spHienTai.TenSanPham;
                bMaSP.InnerText = spHienTai.Id.ToString();
                divGiaSP.InnerText = spHienTai.GiaTien.ToString("N0") + " đ";
                
                pMoTaChiTiet.InnerText = "Chiếc laptop " + spHienTai.TenSanPham + " là một sản phẩm văn phòng và học tập \"nhẹ ví, nhẹ balo\" dành cho những ai cần một người bạn đồng hành đáng tin cậy. Thiết kế gọn nhẹ và mức giá thân thiện, hiệu suất đỉnh cao giúp bạn có những trải nghiệm tốt nhất!";
            }
            else
            {
                divChiTietSanPham.Visible = false;
                divKhongTimThay.Visible = true;
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
                    tong += Convert.ToInt32(dt.Rows[i]["SoLuong"]);
                }

                lblSoLuongGio.Text = tong.ToString();
            }
        }

        private void ThemVaoGio()
        {
            if (spHienTai == null)
            {
                return;
            }

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
                if (Convert.ToInt32(gioHang.Rows[i]["MaSanPham"]) == spHienTai.Id)
                {
                    int soLuongMoi = Convert.ToInt32(gioHang.Rows[i]["SoLuong"]) + 1;
                    gioHang.Rows[i]["SoLuong"] = soLuongMoi;
                    gioHang.Rows[i]["ThanhTien"] = soLuongMoi * spHienTai.GiaTien;
                    daCo = true;
                    break;
                }
            }

            if (daCo == false)
            {
                gioHang.Rows.Add(spHienTai.Id, spHienTai.TenSanPham, 1, spHienTai.GiaTien, spHienTai.GiaTien);
            }

            Session["GioHang"] = gioHang;
        }

        protected void btnThemVaoGio_Click(object sender, EventArgs e)
        {
            ThemVaoGio();
            CapNhatSoLuongGio();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "toast", "showToast('Thêm giỏ hàng thành công!');", true);
        }

        protected void btnMuaNgay_Click(object sender, EventArgs e)
        {
            if (spHienTai == null)
            {
                return;
            }

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