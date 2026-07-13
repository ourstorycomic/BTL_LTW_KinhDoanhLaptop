using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

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

            if (donHangCuaToi.Count == 0)
            {
                lblKhongCoDon.Visible = true;
                rptDonHang.Visible = false;
            }
            else
            {
                lblKhongCoDon.Visible = false;
                rptDonHang.Visible = true;
                rptDonHang.DataSource = donHangCuaToi;
                rptDonHang.DataBind();
            }
        }
        protected void rptDonHang_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DonHang don = (DonHang)e.Item.DataItem;
                Label lblMaDon = (Label)e.Item.FindControl("lblMaDon");
                Label lblNgayDat = (Label)e.Item.FindControl("lblNgayDat");
                Label lblTongTien = (Label)e.Item.FindControl("lblTongTien");
                HtmlGenericControl lblTrangThai = (HtmlGenericControl)e.Item.FindControl("lblTrangThai");
                HtmlButton btnHanhDong = (HtmlButton)e.Item.FindControl("btnHanhDong");
                Repeater rptChiTiet = (Repeater)e.Item.FindControl("rptChiTiet");
                lblMaDon.Text = don.MaDon;
                lblNgayDat.Text = don.NgayDat.ToString("dd/MM/yyyy HH:mm");
                lblTongTien.Text = don.TongTien.ToString("N0");
                lblTrangThai.InnerText = don.TrangThai;
                if (don.TrangThai == "Đang vận chuyển" || don.TrangThai == "Chờ xác nhận")
                {
                    lblTrangThai.Attributes["class"] = "nhan-trang-thai mau-cam";
                }
                else
                {
                    lblTrangThai.Attributes["class"] = "nhan-trang-thai mau-xanh";
                }

                if (don.TrangThai == "Đã giao hàng")
                {
                    btnHanhDong.InnerText = "Mua lại";
                }
                else
                {
                    btnHanhDong.InnerText = "Theo dõi";
                }
                if (rptChiTiet != null)
                {
                    rptChiTiet.DataSource = don.DanhSachChiTiet;
                    rptChiTiet.DataBind();
                }
            }
        }
        protected void rptChiTiet_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ChiTietDonHang sp = (ChiTietDonHang)e.Item.DataItem;
                Image imgSp = (Image)e.Item.FindControl("imgSp");
                Label lblTenSp = (Label)e.Item.FindControl("lblTenSp");
                Label lblGiaSp = (Label)e.Item.FindControl("lblGiaSp");
                Label lblSoLuong = (Label)e.Item.FindControl("lblSoLuong");
                imgSp.ImageUrl = ResolveUrl(sp.HinhAnh);
                lblTenSp.Text = sp.TenSanPham;
                lblGiaSp.Text = sp.DonGia.ToString("N0");
                lblSoLuong.Text = sp.SoLuong.ToString();
            }
        }
    }
}