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
            if (!IsPostBack)
            {

                if (Application["DanhSachLaptop"] != null)
                {

                    rptLaptops.DataSource = (List<Laptop>)Application["DanhSachLaptop"];
                    rptLaptops.DataBind();
                }
            }
        }
        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            LoadDanhSachLaptop(tuKhoa);
        }
        private void LoadDanhSachLaptop(string tuKhoa)
        {
            List<Laptop> danhSachGoc = Application["DanhSachLaptop"] as List<Laptop>;

            if (danhSachGoc != null)
            {
                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    tuKhoa = tuKhoa.ToLower(); 
                    var danhSachDaLoc = danhSachGoc.Where(sp => sp.TenSanPham.ToLower().Contains(tuKhoa)).ToList();
                    rptLaptops.DataSource = danhSachDaLoc;
                }
                else
                {
                    rptLaptops.DataSource = danhSachGoc;
                }
                rptLaptops.DataBind();
            }
        }
    }
}