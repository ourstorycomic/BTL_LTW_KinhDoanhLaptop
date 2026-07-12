using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

                    if (tuDienTaiKhoan.ContainsKey(tenTaiKhoan))
                    {
                        if (tuDienTaiKhoan[tenTaiKhoan].Avatar != null && tuDienTaiKhoan[tenTaiKhoan].Avatar != "")
                        {
                            imgAvatar.Src = tuDienTaiKhoan[tenTaiKhoan].Avatar;
                        }
                    }
                }

                if (tenTaiKhoan == "admin")
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

        protected void Page_Load(object sender, EventArgs e)
        {
            HienThiTaiKhoan();

            if (Request.QueryString["action"] == "xoa" && Request.QueryString["id"] != null)
            {
                int idCanXoa = Convert.ToInt32(Request.QueryString["id"]);
                XoaSanPham(idCanXoa);
            }

            if (IsPostBack == true)
            {
                XuLyCapNhatGioHangTuDong();
            }

            HienThiGioHang();
            CapNhatSoLuongGio();
        }

        private void XuLyCapNhatGioHangTuDong()
        {
            DataTable bangDuLieu = LayDuLieuThanhToan();
            if (bangDuLieu != null)
            {
                bool coThayDoi = false;

                for (int i = 0; i < bangDuLieu.Rows.Count; i++)
                {
                    int maSP = Convert.ToInt32(bangDuLieu.Rows[i]["MaSanPham"]);
                    string tenTheInput = "sl_" + maSP;

                    if (Request.Form[tenTheInput] != null)
                    {
                        int soLuongMoi = 0;
                        if (int.TryParse(Request.Form[tenTheInput], out soLuongMoi) && soLuongMoi > 0)
                        {
                            if (Convert.ToInt32(bangDuLieu.Rows[i]["SoLuong"]) != soLuongMoi)
                            {
                                bangDuLieu.Rows[i]["SoLuong"] = soLuongMoi;
                                decimal donGia = Convert.ToDecimal(bangDuLieu.Rows[i]["DonGia"]);
                                bangDuLieu.Rows[i]["ThanhTien"] = donGia * soLuongMoi;
                                coThayDoi = true;
                            }
                        }
                    }
                }

                if (coThayDoi == true)
                {
                    LuuDuLieuThanhToan(bangDuLieu);
                }
            }
        }

        private void HienThiGioHang()
        {
            DataTable bangDuLieu = LayDuLieuThanhToan();

            if (bangDuLieu != null && bangDuLieu.Rows.Count > 0)
            {
                string chuoiHtml = "";
                decimal tongTien = 0;

                List<Laptop> danhSachGoc = null;
                if (Application["DanhSachLaptop"] != null)
                {
                    danhSachGoc = (List<Laptop>)Application["DanhSachLaptop"];
                }

                for (int i = 0; i < bangDuLieu.Rows.Count; i++)
                {
                    int maSP = Convert.ToInt32(bangDuLieu.Rows[i]["MaSanPham"]);
                    string tenSP = bangDuLieu.Rows[i]["TenSanPham"].ToString();
                    decimal donGia = Convert.ToDecimal(bangDuLieu.Rows[i]["DonGia"]);
                    int soLuong = Convert.ToInt32(bangDuLieu.Rows[i]["SoLuong"]);
                    decimal thanhTien = Convert.ToDecimal(bangDuLieu.Rows[i]["ThanhTien"]);

                    string hinhAnh = "";
                    if (danhSachGoc != null)
                    {
                        for (int j = 0; j < danhSachGoc.Count; j++)
                        {
                            if (danhSachGoc[j].Id == maSP)
                            {
                                if (danhSachGoc[j].HinhAnh != null && danhSachGoc[j].HinhAnh != "")
                                {
                                    hinhAnh = ResolveUrl(danhSachGoc[j].HinhAnh);
                                }
                                break;
                            }
                        }
                    }

                    tongTien = tongTien + thanhTien;

                    chuoiHtml += "<tr>";
                    chuoiHtml += "<td><img src='" + hinhAnh + "' style='width: 100px; height: 100px; object-fit: contain; border-radius: 5px;' alt='" + tenSP + "' /></td>";
                    chuoiHtml += "<td style='font-weight: bold;'>" + tenSP + "</td>";
                    chuoiHtml += "<td class='text-red'>" + donGia.ToString("N0") + " ₫</td>";
                    chuoiHtml += "<td><input type='number' name='sl_" + maSP + "' value='" + soLuong + "' min='1' onchange='this.form.submit();' style='width: 70px; text-align: center; padding: 8px; border: 1px solid #ccc; border-radius: 5px; outline: none;' /></td>";
                    chuoiHtml += "<td class='text-red' style='font-weight: bold;'>" + thanhTien.ToString("N0") + " ₫</td>";
                    chuoiHtml += "<td><a href='ThanhToan.aspx?action=xoa&id=" + maSP + "' style='color: white; background-color: #f44336; padding: 6px 12px; border-radius: 4px; text-decoration: none; font-size: 13px;' onclick=\"showConfirmPopup(this.href); return false;\"><i class='fa-solid fa-trash'></i> Xóa</a></td>";
                    chuoiHtml += "</tr>";
                }

                litGioHang.Text = chuoiHtml;
                lblTongTien.Text = "Tổng cộng: " + tongTien.ToString("N0") + " ₫";

                if (Request.QueryString["type"] == "buynow")
                {
                    TieuDeTrang.InnerText = "Xác nhận mua ngay";
                }

                if (IsPostBack == false)
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

            if (Session["TaiKhoan"] != null && IsPostBack == false)
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

        private void XoaSanPham(int maSP)
        {
            DataTable bangDuLieu = LayDuLieuThanhToan();
            if (bangDuLieu != null)
            {
                for (int i = bangDuLieu.Rows.Count - 1; i >= 0; i--)
                {
                    if (Convert.ToInt32(bangDuLieu.Rows[i]["MaSanPham"]) == maSP)
                    {
                        bangDuLieu.Rows.RemoveAt(i);
                        break;
                    }
                }

                LuuDuLieuThanhToan(bangDuLieu);

                string chuoiQuery = "";
                if (Request.QueryString["type"] == "buynow")
                {
                    chuoiQuery = "?type=buynow";
                }

                Response.Redirect("ThanhToan.aspx" + chuoiQuery);
            }
        }

        private void LuuDuLieuThanhToan(DataTable bangDuLieu)
        {
            if (Request.QueryString["type"] == "buynow")
            {
                Session["MuaNgay"] = bangDuLieu;
            }
            else
            {
                Session["GioHang"] = bangDuLieu;
            }
        }

        private DataTable LayDuLieuThanhToan()
        {
            if (Request.QueryString["type"] == "buynow")
            {
                return (DataTable)Session["MuaNgay"];
            }
            else
            {
                return (DataTable)Session["GioHang"];
            }
        }

        private void CapNhatSoLuongGio()
        {
            if (Session["GioHang"] != null)
            {
                DataTable bangDuLieu = (DataTable)Session["GioHang"];
                int tongSoLuong = 0;

                for (int i = 0; i < bangDuLieu.Rows.Count; i++)
                {
                    tongSoLuong += int.Parse(bangDuLieu.Rows[i]["SoLuong"].ToString());
                }

                lblSoLuongGio.Text = tongSoLuong.ToString();
            }
            else
            {
                lblSoLuongGio.Text = "0";
            }
        }

        protected void btnTienHanh_Click(object sender, EventArgs e)
        {
            if (Session["TaiKhoan"] == null)
            {
                string url = "ThanhToan.aspx";
                if (Request.QueryString["type"] == "buynow")
                {
                    url = url + "?type=buynow";
                }
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

            if (hoTen.Length < 2 || Regex.IsMatch(hoTen, @"^[\p{L}\s]+$") == false)
            {
                lblThongBao.Text = "Họ tên không hợp lệ (phải chứa ít nhất 2 chữ cái và không có số/ký tự đặc biệt)!";
                lblThongBao.Style["color"] = "red";
                return;
            }

            if (Regex.IsMatch(dienThoai, @"^0\d{9}$") == false)
            {
                lblThongBao.Text = "Số điện thoại phải bao gồm đúng 10 chữ số và bắt đầu bằng số 0!";
                lblThongBao.Style["color"] = "red";
                return;
            }

            if (diaChi.Length < 10)
            {
                lblThongBao.Text = "Địa chỉ giao hàng quá ngắn (yêu cầu ghi rõ số nhà, đường, xã/phường)!";
                lblThongBao.Style["color"] = "red";
                return;
            }

            string phuongThuc = rblPhuongThuc.SelectedValue;
            if (phuongThuc == "CARD")
            {
                if (txtTenTrenThe.Text.Length < 2)
                {
                    lblThongBao.Text = "Vui lòng nhập tên in trên thẻ!";
                    lblThongBao.Style["color"] = "red";
                    return;
                }

                if (Regex.IsMatch(txtSoThe.Text, @"^\d{16,19}$") == false)
                {
                    lblThongBao.Text = "Số thẻ không hợp lệ (phải từ 16-19 chữ số)!";
                    lblThongBao.Style["color"] = "red";
                    return;
                }

                if (Regex.IsMatch(txtNgayHetHan.Text, @"^(0[1-9]|1[0-2])\/\d{2}$") == false)
                {
                    lblThongBao.Text = "Ngày hết hạn không hợp lệ (định dạng MM/YY)!";
                    lblThongBao.Style["color"] = "red";
                    return;
                }

                if (Regex.IsMatch(txtCCV.Text, @"^\d{3,4}$") == false)
                {
                    lblThongBao.Text = "Mã CCV/CVV không hợp lệ (phải gồm 3-4 chữ số)!";
                    lblThongBao.Style["color"] = "red";
                    return;
                }
            }

            DataTable bangDuLieu = LayDuLieuThanhToan();

            if (bangDuLieu == null || bangDuLieu.Rows.Count == 0)
            {
                lblThongBao.Text = "Lỗi dữ liệu đơn hàng!";
                lblThongBao.Style["color"] = "red";
                return;
            }

            List<Laptop> danhSachGoc = (List<Laptop>)Application["DanhSachLaptop"];

            for (int i = 0; i < bangDuLieu.Rows.Count; i++)
            {
                int ma = int.Parse(bangDuLieu.Rows[i]["MaSanPham"].ToString());
                int soLuongMua = int.Parse(bangDuLieu.Rows[i]["SoLuong"].ToString());

                for (int j = 0; j < danhSachGoc.Count; j++)
                {
                    if (danhSachGoc[j].Id == ma)
                    {
                        danhSachGoc[j].SoLuongTon = danhSachGoc[j].SoLuongTon - soLuongMua;
                        danhSachGoc[j].SoLuongBan = danhSachGoc[j].SoLuongBan + soLuongMua;
                        break;
                    }
                }
            }

            Application["DanhSachLaptop"] = danhSachGoc;

            if (Request.QueryString["type"] == "buynow")
            {
                Session.Remove("MuaNgay");
            }
            else
            {
                Session.Remove("GioHang");
                lblSoLuongGio.Text = "0";
            }

            lblThongBao.Text = "Đặt hàng thành công! Đơn hàng sẽ được giao đến " + diaChi;
            lblThongBao.Style["color"] = "green";

            litGioHang.Text = "";
            lblTongTien.Text = "";
            divThongTinKhachHang.Visible = false;
            btnThanhToan.Visible = false;

            string maScript = "showToast('Thanh toán thành công! Đơn hàng sẽ được giao đến " + diaChi + "'); setTimeout(function(){ window.location='TrangChu.aspx'; }, 3000);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupSuccess", maScript, true);
        }
    }
}