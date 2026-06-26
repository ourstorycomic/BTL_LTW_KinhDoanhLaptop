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

                rptLaptops.DataSource = (List<Laptop>)Application["DanhSachLaptop"];
                rptLaptops.DataBind();
            }

        }
    }
}