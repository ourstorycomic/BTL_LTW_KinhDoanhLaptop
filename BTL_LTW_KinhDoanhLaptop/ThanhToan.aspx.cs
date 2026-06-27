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
            CapNhatSoLuongGio();

            if (!IsPostBack)
            {
                DataTable dt = LayDuLieuThanhToan();

                if (dt != null && dt.Rows.Count > 0)
                {
                    rptGioHang.DataSource = dt;
                    rptGioHang.DataBind();

                    decimal tong = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        tong += Convert.ToDecimal(dr["ThanhTien"]);
                    }
                    lblTongTien.Text = "Tổng cộng: " + tong.ToString("N0") + " ₫";

                    if (Request.QueryString["type"] == "buynow")
                    {
                        TieuDeTrang.InnerText = "Xác nhận mua ngay";
                    }

                    divThongTinKhachHang.Visible = false;
                    btnThanhToan.Visible = false;
                    btnTienHanh.Visible = true;
                }
                else
                {
                    divThongTinKhachHang.Visible = false;
                    btnThanhToan.Visible = false;
                    btnTienHanh.Visible = false;
                    lblThongBao.Text = "Không có sản phẩm nào để thanh toán!";
                    lblThongBao.Style["color"] = "red";
                }

                if (Session["TaiKhoan"] != null)
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

            rptGioHang.DataSource = null;
            rptGioHang.DataBind();
            lblTongTien.Text = "";
            divThongTinKhachHang.Visible = false;
            btnThanhToan.Visible = false;

            string script = "showToast('Thanh toán thành công! Đơn hàng sẽ được giao đến " + diaChi + "'); setTimeout(function(){ window.location='TrangChu.aspx'; }, 3000);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupSuccess", script, true);
        }
    }
}