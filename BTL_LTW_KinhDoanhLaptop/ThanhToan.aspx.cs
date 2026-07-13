using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class ThanhToan : System.Web.UI.Page
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

                string tenTaiKhoan = Session["TaiKhoan"].ToString();
                lblTenTaiKhoan.InnerText = tenTaiKhoan;

                if (Application["DanhSachTaiKhoan"] != null)
                {
                    Dictionary<string, NguoiDung> tuDienTaiKhoan = (Dictionary<string, NguoiDung>)Application["DanhSachTaiKhoan"];
                    if (tuDienTaiKhoan.ContainsKey(tenTaiKhoan) && !string.IsNullOrEmpty(tuDienTaiKhoan[tenTaiKhoan].Avatar))
                    {
                        imgAvatar.Src = tuDienTaiKhoan[tenTaiKhoan].Avatar;
                    }
                }

                linkQuanTri.Visible = (tenTaiKhoan == "admin");
                linkThongKe.Visible = (tenTaiKhoan == "admin");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            HienThiTaiKhoan();
            if (Request.QueryString["action"] == "xoa" && Request.QueryString["id"] != null)
            {
                int idCanXoa = Convert.ToInt32(Request.QueryString["id"]);
                XoaSanPham(idCanXoa);
            }
            if (IsPostBack)
            {
                XuLyCapNhatGioHangTuDong();
            }

            HienThiGioHang();
            CapNhatSoLuongGio();
        }

        private List<Laptop> LayDuLieuThanhToan()
        {
            if (Request.QueryString["type"] == "buynow")
                return (List<Laptop>)Session["MuaNgay"];
            else
                return (List<Laptop>)Session["GioHang"];
        }

        private void LuuDuLieuThanhToan(List<Laptop> danhSach)
        {
            if (Request.QueryString["type"] == "buynow")
                Session["MuaNgay"] = danhSach;
            else
                Session["GioHang"] = danhSach;
        }

        private void XuLyCapNhatGioHangTuDong()
        {
            List<Laptop> gioHang = LayDuLieuThanhToan();
            if (gioHang != null)
            {
                bool coThayDoi = false;

                foreach (Laptop sp in gioHang)
                {
                    string tenTheInput = "sl_" + sp.Id;
                    if (Request.Form[tenTheInput] != null)
                    {
                        int soLuongMoi = 0;
                        if (int.TryParse(Request.Form[tenTheInput], out soLuongMoi) && soLuongMoi > 0)
                        {
                            if (sp.SoLuongTrongGio != soLuongMoi)
                            {
                                sp.SoLuongTrongGio = soLuongMoi;
                                coThayDoi = true;
                            }
                        }
                    }
                }

                if (coThayDoi)
                {
                    LuuDuLieuThanhToan(gioHang);
                }
            }
        }

        private void XoaSanPham(int maSP)
        {
            List<Laptop> gioHang = LayDuLieuThanhToan();
            if (gioHang != null)
            {
                gioHang.RemoveAll(sp => sp.Id == maSP);
                LuuDuLieuThanhToan(gioHang);

                string chuoiQuery = (Request.QueryString["type"] == "buynow") ? "?type=buynow" : "";
                Response.Redirect("ThanhToan.aspx" + chuoiQuery);
            }
        }

        private void CapNhatSoLuongGio()
        {
            List<Laptop> gioHang = (List<Laptop>)Session["GioHang"];
            if (gioHang != null)
            {
                int tongSoLuong = gioHang.Sum(sp => sp.SoLuongTrongGio);
                lblSoLuongGio.Text = tongSoLuong.ToString();
            }
            else
            {
                lblSoLuongGio.Text = "0";
            }
        }

        private void HienThiGioHang()
        {
            List<Laptop> gioHang = LayDuLieuThanhToan();

            if (gioHang != null && gioHang.Count > 0)
            {
                string chuoiHtml = "";
                decimal tongTien = 0;

                foreach (Laptop sp in gioHang)
                {
                    if (sp.SoLuongTrongGio == 0) sp.SoLuongTrongGio = 1;

                    decimal thanhTien = sp.GiaTien * sp.SoLuongTrongGio;
                    tongTien += thanhTien;
                    chuoiHtml += "<tr>";
                    chuoiHtml += "<td><img src='" + ResolveUrl(sp.HinhAnh) + "' style='width: 100px; height: 100px; object-fit: contain; border-radius: 5px;' /></td>";
                    chuoiHtml += "<td style='font-weight: bold;'>" + sp.TenSanPham + "</td>";
                    chuoiHtml += "<td class='text-red'>" + sp.GiaTien.ToString("N0") + " ₫</td>";
                    chuoiHtml += "<td><input type='number' name='sl_" + sp.Id + "' value='" + sp.SoLuongTrongGio + "' min='1' onchange='this.form.submit();' style='width: 70px; text-align: center; padding: 8px; border: 1px solid #ccc; border-radius: 5px; outline: none;' /></td>";
                    chuoiHtml += "<td class='text-red' style='font-weight: bold;'>" + thanhTien.ToString("N0") + " ₫</td>";
                    chuoiHtml += "<td><a href='ThanhToan.aspx?action=xoa&id=" + sp.Id + "' style='color: white; background-color: #f44336; padding: 6px 12px; border-radius: 4px; text-decoration: none; font-size: 13px;' onclick=\"showConfirmPopup(this.href); return false;\"><i class='fa-solid fa-trash'></i> Xóa</a></td>";
                    chuoiHtml += "</tr>";
                }

                litGioHang.Text = chuoiHtml;
                lblTongTien.Text = "Tổng cộng: " + tongTien.ToString("N0") + " ₫";

                if (Request.QueryString["type"] == "buynow")
                {
                    TieuDeTrang.InnerText = "Xác nhận mua ngay";
                }

                if (!IsPostBack)
                {
                    divThongTinKhachHang.Visible = false;
                    btnThanhToan.Visible = false;
                    btnTienHanh.Visible = true;
                }
            }
            else
            {
                litGioHang.Text = "<tr><td colspan='6' style='text-align: center; padding: 30px; color: #888;'>Không có sản phẩm nào để thanh toán!</td></tr>";
                divThongTinKhachHang.Visible = false;
                btnThanhToan.Visible = false;
                btnTienHanh.Visible = false;
                lblThongBao.Text = "Giỏ hàng của bạn đang trống!";
                lblThongBao.Style["color"] = "red";
            }
            if (Session["TaiKhoan"] != null && !IsPostBack)
            {
                string tenTaiKhoan = Session["TaiKhoan"].ToString();
                if (Application["DanhSachTaiKhoan"] != null)
                {
                    Dictionary<string, NguoiDung> tuDienTaiKhoan = (Dictionary<string, NguoiDung>)Application["DanhSachTaiKhoan"];
                    if (tuDienTaiKhoan.ContainsKey(tenTaiKhoan))
                    {
                        txtHoTen.Text = tuDienTaiKhoan[tenTaiKhoan].HoTen;
                        txtDienThoai.Text = tuDienTaiKhoan[tenTaiKhoan].SDT;
                        txtDiaChi.Text = tuDienTaiKhoan[tenTaiKhoan].DiaChi;
                    }
                }
            }
        }

        protected void btnTienHanh_Click(object sender, EventArgs e)
        {
            if (Session["TaiKhoan"] == null)
            {
                string url = "ThanhToan.aspx";
                if (Request.QueryString["type"] == "buynow") url += "?type=buynow";
                Response.Redirect("DangNhap.aspx?redirect=" + Server.UrlEncode(url));
                return;
            }

            divThongTinKhachHang.Visible = true;
            btnThanhToan.Visible = true;
            btnTienHanh.Visible = false;
        }

        protected void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (Session["TaiKhoan"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }
            string hoTen = txtHoTen.Text;
            string dienThoai = txtDienThoai.Text;
            string diaChi = txtDiaChi.Text;

            if (hoTen.Length < 2 || !Regex.IsMatch(hoTen, @"^[\p{L}\s]+$"))
            {
                lblThongBao.Text = "Họ tên không hợp lệ!"; lblThongBao.Style["color"] = "red"; return;
            }
            if (!Regex.IsMatch(dienThoai, @"^0\d{9}$"))
            {
                lblThongBao.Text = "Số điện thoại không hợp lệ!"; lblThongBao.Style["color"] = "red"; return;
            }
            if (diaChi.Length < 10)
            {
                lblThongBao.Text = "Địa chỉ giao hàng quá ngắn!"; lblThongBao.Style["color"] = "red"; return;
            }
            List<Laptop> gioHang = LayDuLieuThanhToan();

            if (gioHang == null || gioHang.Count == 0)
            {
                lblThongBao.Text = "Lỗi dữ liệu đơn hàng!"; lblThongBao.Style["color"] = "red"; return;
            }
            List<Laptop> danhSachGoc = (List<Laptop>)Application["DanhSachLaptop"];

            DonHang donMoi = new DonHang();
            Random r = new Random();
            donMoi.MaDon = "ORD-" + r.Next(10000, 99999);
            donMoi.TaiKhoan = Session["TaiKhoan"].ToString();
            donMoi.NgayDat = DateTime.Now;
            donMoi.TrangThai = "Chờ xác nhận";
            donMoi.DanhSachChiTiet = new List<ChiTietDonHang>();
            decimal tongTienDon = 0;

            foreach (Laptop spGioHang in gioHang)
            {
                ChiTietDonHang chiTiet = new ChiTietDonHang();
                chiTiet.IdLaptop = spGioHang.Id;
                chiTiet.TenSanPham = spGioHang.TenSanPham;
                chiTiet.DonGia = spGioHang.GiaTien;
                chiTiet.SoLuong = spGioHang.SoLuongTrongGio; 
                chiTiet.HinhAnh = spGioHang.HinhAnh;

                donMoi.DanhSachChiTiet.Add(chiTiet);
                tongTienDon += (chiTiet.DonGia * chiTiet.SoLuong);
                Laptop spGoc = danhSachGoc.FirstOrDefault(x => x.Id == spGioHang.Id);
                if (spGoc != null)
                {
                    spGoc.SoLuongTon -= chiTiet.SoLuong;
                    spGoc.SoLuongBan += chiTiet.SoLuong;
                }
            }

            donMoi.TongTien = tongTienDon;
            List<DonHang> danhSachDonHang = new List<DonHang>();
            if (Application["DanhSachDonHang"] != null)
            {
                danhSachDonHang = (List<DonHang>)Application["DanhSachDonHang"];
            }
            danhSachDonHang.Add(donMoi);
            Application["DanhSachDonHang"] = danhSachDonHang;
            Application["DanhSachLaptop"] = danhSachGoc; 
            if (Request.QueryString["type"] == "buynow")
                Session.Remove("MuaNgay");
            else
            {
                Session.Remove("GioHang");
                lblSoLuongGio.Text = "0";
            }
            string maScript = "showToast('Thanh toán thành công! Đơn hàng sẽ được giao đến " + diaChi + "'); setTimeout(function(){ window.location='TrangChu.aspx'; }, 3000);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupSuccess", maScript, true);
        }
    }
}