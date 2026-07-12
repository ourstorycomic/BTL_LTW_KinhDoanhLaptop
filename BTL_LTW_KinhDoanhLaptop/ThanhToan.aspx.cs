using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

            if (Request.QueryString["action"] == "xoa" && Request.QueryString["id"] != null)
            {
                XoaSanPham(Convert.ToInt32(Request.QueryString["id"]));
            }

            if (IsPostBack)
            {
                XuLyCapNhatGioHangTuDong();
            }

            HienThiGioHang();
            CapNhatSoLuongGio();
        }

        private void XuLyCapNhatGioHangTuDong()
        {
            DataTable dt = LayDuLieuThanhToan();
            if (dt != null)
            {
                bool daThayDoi = false;
                foreach (DataRow dr in dt.Rows)
                {
                    int maSP = Convert.ToInt32(dr["MaSanPham"]);
                    string tenTheInput = "sl_" + maSP;

                    if (Request.Form[tenTheInput] != null)
                    {
                        int soLuongMoi = 0;
                        if (int.TryParse(Request.Form[tenTheInput], out soLuongMoi) && soLuongMoi > 0)
                        {
                            if (Convert.ToInt32(dr["SoLuong"]) != soLuongMoi)
                            {
                                dr["SoLuong"] = soLuongMoi;
                                decimal donGia = Convert.ToDecimal(dr["DonGia"]);
                                dr["ThanhTien"] = donGia * soLuongMoi;
                                daThayDoi = true;
                            }
                        }
                    }
                }

                if (daThayDoi)
                {
                    LuuDuLieuThanhToan(dt);
                }
            }
        }

        private void HienThiGioHang()
        {
            DataTable dt = LayDuLieuThanhToan();

            if (dt != null && dt.Rows.Count > 0)
            {
                string html = "";
                decimal tong = 0;
                List<Laptop> kho = Application["DanhSachLaptop"] as List<Laptop>;

                foreach (DataRow dr in dt.Rows)
                {
                    int maSP = Convert.ToInt32(dr["MaSanPham"]);
                    string tenSP = dr["TenSanPham"].ToString();
                    decimal donGia = Convert.ToDecimal(dr["DonGia"]);
                    int soLuong = Convert.ToInt32(dr["SoLuong"]);
                    decimal thanhTien = Convert.ToDecimal(dr["ThanhTien"]);

                    string hinhAnh = "";
                    if (kho != null)
                    {
                        Laptop sp = kho.FirstOrDefault(x => x.Id == maSP);
                        if (sp != null && !string.IsNullOrEmpty(sp.HinhAnh))
                        {
                            hinhAnh = ResolveUrl(sp.HinhAnh);
                        }
                    }

                    tong += thanhTien;

                    html += "<tr>";
                    html += $"<td><img src='{hinhAnh}' style='width: 100px; height: 100px; object-fit: contain; border-radius: 5px;' alt='{tenSP}' /></td>";
                    html += $"<td style='font-weight: bold;'>{tenSP}</td>";
                    html += $"<td class='text-red'>{donGia:N0} ₫</td>";
                    html += $"<td><input type='number' name='sl_{maSP}' value='{soLuong}' min='1' onchange='this.form.submit();' style='width: 70px; text-align: center; padding: 8px; border: 1px solid #ccc; border-radius: 5px; outline: none;' /></td>";
                    html += $"<td class='text-red' style='font-weight: bold;'>{thanhTien:N0} ₫</td>";
                    html += $"<td><a href='ThanhToan.aspx?action=xoa&id={maSP}' style='color: white; background-color: #f44336; padding: 6px 12px; border-radius: 4px; text-decoration: none; font-size: 13px;' onclick=\"showConfirmPopup(this.href); return false;\"><i class='fa-solid fa-trash'></i> Xóa</a></td>";
                    html += "</tr>";
                }

                litGioHang.Text = html;
                lblTongTien.Text = "Tổng cộng: " + tong.ToString("N0") + " ₫";

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
                string tk = Session["TaiKhoan"].ToString();
                Dictionary<string, NguoiDung> ds = Application["DanhSachTaiKhoan"] as Dictionary<string, NguoiDung>;
                if (ds != null && ds.ContainsKey(tk))
                {
                    txtHoTen.Text = ds[tk].HoTen;
                    txtDienThoai.Text = ds[tk].SDT;
                    txtDiaChi.Text = ds[tk].DiaChi;
                }
            }
        }

        private void XoaSanPham(int maSP)
        {
            DataTable dt = LayDuLieuThanhToan();
            if (dt != null)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (Convert.ToInt32(dt.Rows[i]["MaSanPham"]) == maSP)
                    {
                        dt.Rows.RemoveAt(i);
                        break;
                    }
                }
                LuuDuLieuThanhToan(dt);

                string query = Request.QueryString["type"] == "buynow" ? "?type=buynow" : "";
                Response.Redirect("ThanhToan.aspx" + query);
            }
        }

        private void LuuDuLieuThanhToan(DataTable dt)
        {
            if (Request.QueryString["type"] == "buynow")
            {
                Session["MuaNgay"] = dt;
            }
            else
            {
                Session["GioHang"] = dt;
            }
        }

        private DataTable LayDuLieuThanhToan()
        {
            if (Request.QueryString["type"] == "buynow")
            {
                return (DataTable)Session["MuaNgay"];
            }
            return (DataTable)Session["GioHang"];
        }

        private void CapNhatSoLuongGio()
        {
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
            else
            {
                lblSoLuongGio.Text = "0";
            }
        }

        protected void btnTienHanh_Click(object sender, EventArgs e)
        {
            divThongTinKhachHang.Visible = true;
            btnThanhToan.Visible = true;
            btnTienHanh.Visible = false;
        }

        protected void btnThanhToan_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string dienThoai = txtDienThoai.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            if (hoTen.Length < 2 || !Regex.IsMatch(hoTen, @"^[\p{L}\s]+$"))
            {
                lblThongBao.Text = "Họ tên không hợp lệ (phải chứa ít nhất 2 chữ cái và không có số/ký tự đặc biệt)!";
                lblThongBao.Style["color"] = "red";
                return;
            }

            if (!Regex.IsMatch(dienThoai, @"^0\d{9}$"))
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
                if (txtTenTrenThe.Text.Trim().Length < 2)
                {
                    lblThongBao.Text = "Vui lòng nhập tên in trên thẻ!";
                    lblThongBao.Style["color"] = "red";
                    return;
                }
                if (!Regex.IsMatch(txtSoThe.Text.Trim(), @"^\d{16,19}$"))
                {
                    lblThongBao.Text = "Số thẻ không hợp lệ (phải từ 16-19 chữ số)!";
                    lblThongBao.Style["color"] = "red";
                    return;
                }
                if (!Regex.IsMatch(txtNgayHetHan.Text.Trim(), @"^(0[1-9]|1[0-2])\/\d{2}$"))
                {
                    lblThongBao.Text = "Ngày hết hạn không hợp lệ (định dạng MM/YY)!";
                    lblThongBao.Style["color"] = "red";
                    return;
                }
                if (!Regex.IsMatch(txtCCV.Text.Trim(), @"^\d{3,4}$"))
                {
                    lblThongBao.Text = "Mã CCV/CVV không hợp lệ (phải gồm 3-4 chữ số)!";
                    lblThongBao.Style["color"] = "red";
                    return;
                }
            }

            DataTable dtThanhToan = LayDuLieuThanhToan();

            if (dtThanhToan == null || dtThanhToan.Rows.Count == 0)
            {
                lblThongBao.Text = "Lỗi dữ liệu đơn hàng!";
                lblThongBao.Style["color"] = "red";
                return;
            }

            List<Laptop> kho = (List<Laptop>)Application["DanhSachLaptop"];

            foreach (DataRow dr in dtThanhToan.Rows)
            {
                int ma = int.Parse(dr["MaSanPham"].ToString());
                int slMua = int.Parse(dr["SoLuong"].ToString());

                Laptop sp = kho.FirstOrDefault(x => x.Id == ma);
                if (sp != null)
                {
                    sp.SoLuongTon -= slMua;
                    sp.SoLuongBan += slMua;
                }
            }

            Application["DanhSachLaptop"] = kho;

            if (Request.QueryString["type"] == "buynow")
            {
                Session["MuaNgay"] = null;
            }
            else
            {
                Session["GioHang"] = null;
                lblSoLuongGio.Text = "0";
            }

            lblThongBao.Text = "Đặt hàng thành công! Đơn hàng sẽ được giao đến " + diaChi;
            lblThongBao.Style["color"] = "green";

            litGioHang.Text = "";
            lblTongTien.Text = "";
            divThongTinKhachHang.Visible = false;
            btnThanhToan.Visible = false;

            string script = "showToast('Thanh toán thành công! Đơn hàng sẽ được giao đến " + diaChi + "'); setTimeout(function(){ window.location='TrangChu.aspx'; }, 3000);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupSuccess", script, true);
        }
    }
}