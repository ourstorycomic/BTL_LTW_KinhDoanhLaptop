using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class TrangChu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Application["DanhSachLaptop"] != null)
            {

<<<<<<< Updated upstream
                rptLaptops.DataSource = (List<Laptop>)Application["DanhSachLaptop"];
                rptLaptops.DataBind();
            }

=======
            if (!IsPostBack)
            {
                if (Application["DanhSachLaptop"] != null)
                {
                    List<Laptop> danhSach = (List<Laptop>)Application["DanhSachLaptop"];
                    string search = Request.QueryString["search"];
                    string brand = Request.QueryString["brand"];
                    
                    if (!string.IsNullOrEmpty(search))
                    {
                        search = search.ToLower();
                        danhSach = danhSach.Where(x => x.TenSanPham.ToLower().Contains(search)).ToList();
                    }
                    
                    if (!string.IsNullOrEmpty(brand))
                    {
                        brand = brand.ToLower();
                        danhSach = danhSach.Where(x => x.TenSanPham.ToLower().Contains(brand)).ToList();
                    }
                    
                    rptLaptops.DataSource = danhSach;
                    rptLaptops.DataBind();
                }
            }
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
        }

        protected void btnThemGio_Click(object sender, EventArgs e)
        {
            if (Session["TaiKhoan"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }
            int maSP = int.Parse(((LinkButton)sender).CommandArgument);
            List<Laptop> danhSach = (List<Laptop>)Application["DanhSachLaptop"];
            Laptop spChon = danhSach.FirstOrDefault(x => x.Id == maSP);

            DataTable gioHang;
            if (Session["GioHang"] == null)
            {
                gioHang = new DataTable();
                gioHang.Columns.Add("MaSanPham", typeof(int));
                gioHang.Columns.Add("TenSanPham", typeof(string));
                gioHang.Columns.Add("SoLuong", typeof(int));
                gioHang.Columns.Add("DonGia", typeof(decimal));
                gioHang.Columns.Add("ThanhTien", typeof(decimal));
                gioHang.Columns.Add("HinhAnh", typeof(string));
            }
            else
            {
                gioHang = (DataTable)Session["GioHang"];
            }

            bool daCo = false;
            foreach (DataRow dr in gioHang.Rows)
            {
                if (int.Parse(dr["MaSanPham"].ToString()) == maSP)
                {
                    dr["SoLuong"] = int.Parse(dr["SoLuong"].ToString()) + 1;
                    dr["ThanhTien"] = int.Parse(dr["SoLuong"].ToString()) * spChon.GiaTien;
                    daCo = true;
                    break;
                }
            }

            if (!daCo)
            {
                gioHang.Rows.Add(maSP, spChon.TenSanPham, 1, spChon.GiaTien, spChon.GiaTien,spChon.HinhAnh);
            }

            Session["GioHang"] = gioHang;
            CapNhatSoLuongGio();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "toast", "showToast('Thêm giỏ hàng thành công!');", true);
>>>>>>> Stashed changes
        }
    }
}