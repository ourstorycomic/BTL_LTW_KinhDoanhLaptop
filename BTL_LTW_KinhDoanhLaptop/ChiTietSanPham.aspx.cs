using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class ChiTietSanPham : System.Web.UI.Page
    {
        public Laptop spHienTai;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LayThongTinSanPham();
            }
        }

        private void LayThongTinSanPham()
        {
            string id = Request.QueryString["id"];

            if (!string.IsNullOrEmpty(id))
            {
                List<Laptop> danhSach = Application["DanhSachLaptop"] as List<Laptop>;

                if (danhSach != null)
                {
                    int idHienTai = int.Parse(id);
                    spHienTai = danhSach.FirstOrDefault(sp => sp.Id == idHienTai);
                }
            }
        }
        protected void btnThemVaoGio_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('Đã thêm vào giỏ hàng thành công!');</script>");
        }
        protected void btnMuaNgay_Click(object sender, EventArgs e)
        {
            
        }
    }


}
