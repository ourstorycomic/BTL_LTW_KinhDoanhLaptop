using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class TinTuc : System.Web.UI.Page
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
                
                string tk = Session["TaiKhoan"].ToString();
                lblTenTaiKhoan.InnerText = tk;

                if (Application["DanhSachTaiKhoan"] != null)
                {
                    Dictionary<string, BTL_LTW_KinhDoanhLaptop.NguoiDung> dict = (Dictionary<string, BTL_LTW_KinhDoanhLaptop.NguoiDung>)Application["DanhSachTaiKhoan"];
                    if (dict.ContainsKey(tk))
                    {
                        if (dict[tk].Avatar != null && dict[tk].Avatar != "")
                        {
                            imgAvatar.Src = dict[tk].Avatar;
                        }
                    }
                }

                if (tk == "admin")
                {
                    linkQuanTri.Visible = true;
                    linkThongKe.Visible = true;
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            HienThiTaiKhoan();
            CapNhatSoLuongGio();
        }
        
        private void CapNhatSoLuongGio()
        {
            if (Session["GioHang"] != null)
            {
                System.Data.DataTable dt = (System.Data.DataTable)Session["GioHang"];
                int tong = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tong = tong + int.Parse(dt.Rows[i]["SoLuong"].ToString());
                }
                lblSoLuongGio.Text = tong.ToString();
            }
        }
            }
}
