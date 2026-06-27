using BTL_LTW_KinhDoanhLaptop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class CuaHang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

       if (!IsPostBack)
            {
                LoadTatCaSanPham();
    }
}

        private void LoadTatCaSanPham()
        {
            List<Laptop> danhSachGoc = Application["DanhSachLaptop"] as List<Laptop>;

            if (danhSachGoc != null)
            {
                rptLaptops.DataSource = danhSachGoc;
                rptLaptops.DataBind();
            }
        }
            protected void BoLoc_Changed(object sender, EventArgs e)
        {
            List<Laptop> danhSachGoc = Application["DanhSachLaptop"] as List<Laptop>;
            if (danhSachGoc == null) return;
            IEnumerable<Laptop> ketQuaLoc = danhSachGoc;

            List<string> cacHangDuocChon = new List<string>();
            foreach (ListItem item in cblThuongHieu.Items)
            {
                if (item.Selected)
                {
                    cacHangDuocChon.Add(item.Value);
                }
            }
            if (cacHangDuocChon.Count > 0)
            {
                ketQuaLoc = ketQuaLoc.Where(sp => cacHangDuocChon.Any(hang => sp.TenSanPham.ToLower().Contains(hang)));
            }
            string mucGia = rblMucGia.SelectedValue;
            if (mucGia == "1") 
            {
                ketQuaLoc = ketQuaLoc.Where(sp => sp.GiaTien < 15000000);
            }
            else if (mucGia == "2")
            {
                ketQuaLoc = ketQuaLoc.Where(sp => sp.GiaTien >= 15000000 && sp.GiaTien <= 25000000);
            }
            else if (mucGia == "3") 
            {
                ketQuaLoc = ketQuaLoc.Where(sp => sp.GiaTien > 25000000);
            }
            string kieuSapXep = ddlSapXep.SelectedValue;
            if (kieuSapXep == "asc") 
            {
                ketQuaLoc = ketQuaLoc.OrderBy(sp => sp.GiaTien);
            }
            else if (kieuSapXep == "desc") 
            {
                ketQuaLoc = ketQuaLoc.OrderByDescending(sp => sp.GiaTien);
            }
            else 
            {
                ketQuaLoc = ketQuaLoc.OrderByDescending(sp => sp.Id);
            }
            rptLaptops.DataSource = ketQuaLoc.ToList();
            rptLaptops.DataBind();
        }
    }
    }
