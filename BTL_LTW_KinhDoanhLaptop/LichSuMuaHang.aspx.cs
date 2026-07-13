using System;
using System.Collections.Generic;
using System.Linq; 

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class LichSuMuaHang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TaiKhoan"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }
            if (!IsPostBack)
            {
                HienThiDanhSachDonHang();
            }
        }

        private void HienThiDanhSachDonHang()
        {
            string tenTaiKhoan = Session["TaiKhoan"].ToString();
            List<DonHang> tatCaDonHang = (List<DonHang>)Application["DanhSachDonHang"];
            List<DonHang> donHangCuaToi = new List<DonHang>();

            if (tatCaDonHang != null)
            {
                donHangCuaToi = tatCaDonHang.Where(d => d.TaiKhoan == tenTaiKhoan).OrderByDescending(d => d.NgayDat).ToList();
            }
            string chuoiHTML = "";

            if (donHangCuaToi.Count == 0)
            {
                chuoiHTML = "<p style='text-align:center; color:#888; font-size: 16px; padding: 50px;'>Bạn chưa mua đơn hàng nào.</p>";
            }
            else
            {
                foreach (DonHang don in donHangCuaToi)
                {
                    string mauTrangThai = "mau-xanh";
                    if (don.TrangThai == "Đang vận chuyển" || don.TrangThai == "Chờ xác nhận")
                    {
                        mauTrangThai = "mau-cam";
                    }
                    chuoiHTML += "<div class='the-don-hang'>";
                    chuoiHTML += "   <div class='phan-dau-don'>";
                    chuoiHTML += "       <span class='ma-don'>Order <b>#" + don.MaDon + "</b></span>";
                    chuoiHTML += "       <span class='nhan-trang-thai " + mauTrangThai + "'>" + don.TrangThai + "</span>";
                    chuoiHTML += "       <div class='clear-float'></div>";
                    chuoiHTML += "   </div>";
                    chuoiHTML += "   <p class='ngay-dat'>Ngày đặt: " + don.NgayDat.ToString("dd/MM/yyyy HH:mm") + "</p>";
                    foreach (ChiTietDonHang sp in don.DanhSachChiTiet)
                    {
                        chuoiHTML += "   <div class='san-pham-trong-don'>";
                        chuoiHTML += "       <img src='" + ResolveUrl(sp.HinhAnh) + "' class='anh-sp-don' />";
                        chuoiHTML += "       <div class='thong-tin-sp-don'>";
                        chuoiHTML += "           <p class='ten-sp-don'>" + sp.TenSanPham + "</p>";
                        chuoiHTML += "           <p class='gia-sp-don'>" + sp.DonGia.ToString("N0") + "đ</p>";
                        chuoiHTML += "       </div>";
                        chuoiHTML += "       <div class='so-luong-don'>x" + sp.SoLuong + "</div>";
                        chuoiHTML += "       <div class='clear-float'></div>";
                        chuoiHTML += "   </div>";
                    }

                    chuoiHTML += "   <div class='duong-ke-ngang'></div>";

                    chuoiHTML += "   <div class='tong-tien-don'>";
                    chuoiHTML += "       <span class='chu-tong'>Tổng chi tiết:</span>";
                    chuoiHTML += "       <span class='so-tien-tong'>" + don.TongTien.ToString("N0") + "đ</span>";
                    chuoiHTML += "       <div class='clear-float'></div>";
                    chuoiHTML += "   </div>";

                    chuoiHTML += "   <div class='vung-nut-hanh-dong'>";
                    chuoiHTML += "       <button class='nut-trang'>Xem chi tiết</button>";

                    if (don.TrangThai == "Đã giao hàng")
                    {
                        chuoiHTML += "       <button class='nut-den'>Mua lại</button>";
                    }
                    else
                    {
                        chuoiHTML += "       <button class='nut-den'>Theo dõi</button>";
                    }

                    chuoiHTML += "   </div>";
                    chuoiHTML += "</div>";
                }
            }

            // Đẩy toàn bộ HTML vừa tạo ra giao diện
            khungChuaDonHang.InnerHtml = chuoiHTML;
        }
    }
}