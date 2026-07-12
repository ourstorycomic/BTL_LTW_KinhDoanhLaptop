using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

namespace BTL_LTW_KinhDoanhLaptop
{
    public partial class QuanTri : System.Web.UI.Page
    {
        public int TongSoLuongTon = 0;
        public decimal TongGiaTriKho = 0;
        public int TongTaiKhoan = 0;
        public string SanPhamBanChay = "Chưa có dữ liệu";

        protected void Page_Load(object sender, EventArgs e)
        {
            HienThiTaiKhoan();

            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() != "admin")
            {
                Response.Redirect("TrangChu.aspx");
                return;
            }

            if (!IsPostBack)
            {
                string action = Request.QueryString["action"];
                string idTruyenVao = Request.QueryString["id"];

                if (action == "delete" && idTruyenVao != null)
                {
                    int id = int.Parse(idTruyenVao);
                    List<Laptop> danhSach = (List<Laptop>)Application["DanhSachLaptop"];
                    Laptop spXoa = null;

                    for (int i = 0; i < danhSach.Count; i++)
                    {
                        if (danhSach[i].Id == id)
                        {
                            spXoa = danhSach[i];
                            break;
                        }
                    }

                    if (spXoa != null)
                    {
                        danhSach.Remove(spXoa);
                        Application["DanhSachLaptop"] = danhSach;
                    }
                    Response.Redirect("QuanTri.aspx");
                }
                else if (action == "edit" && idTruyenVao != null)
                {
                    int id = int.Parse(idTruyenVao);
                    List<Laptop> danhSach = (List<Laptop>)Application["DanhSachLaptop"];
                    Laptop spSua = null;

                    for (int i = 0; i < danhSach.Count; i++)
                    {
                        if (danhSach[i].Id == id)
                        {
                            spSua = danhSach[i];
                            break;
                        }
                    }

                    if (spSua != null)
                    {
                        txtId.Value = spSua.Id.ToString();
                        txtTenSanPham.Text = spSua.TenSanPham;
                        txtGiaTien.Text = spSua.GiaTien.ToString();
                        txtHinhAnhCu.Value = spSua.HinhAnh;
                        imgPreview.ImageUrl = spSua.HinhAnh;
                        imgPreview.Visible = true;
                        txtSoLuongTon.Text = spSua.SoLuongTon.ToString();
                    }
                }
                LoadData();
            }

            if (Session["GioHang"] != null)
            {
                DataTable dt = (DataTable)Session["GioHang"];
                int tongGioHang = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tongGioHang += Convert.ToInt32(dt.Rows[i]["SoLuong"]);
                }
                lblSoLuongGio.Text = tongGioHang.ToString();
            }
        }

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

                string tkDangNhap = Session["TaiKhoan"].ToString();
                lblTenTaiKhoan.InnerText = tkDangNhap;

                if (Application["DanhSachTaiKhoan"] != null)
                {
                    Dictionary<string, NguoiDung> dict = (Dictionary<string, NguoiDung>)Application["DanhSachTaiKhoan"];

                    if (dict.ContainsKey(tkDangNhap))
                    {
                        if (dict[tkDangNhap].Avatar != null && dict[tkDangNhap].Avatar != "")
                        {
                            imgAvatar.Src = dict[tkDangNhap].Avatar;
                        }
                    }
                }

                if (tkDangNhap == "admin")
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

        private void LoadData()
        {
            if (Application["DanhSachLaptop"] != null)
            {
                List<Laptop> danhSach = (List<Laptop>)Application["DanhSachLaptop"];
                string html = "";
                for (int i = 0; i < danhSach.Count; i++)
                {
                    Laptop sp = danhSach[i];
                    html += "<tr>";
                    html += "<td>" + sp.Id + "</td>";
                    html += "<td>" + sp.TenSanPham + "</td>";
                    html += "<td>" + sp.GiaTien.ToString("N0") + " ₫</td>";
                    html += "<td>" + sp.SoLuongTon + "</td>";
                    html += "<td><img src='" + ResolveUrl(sp.HinhAnh) + "' style='width: 50px; height: 50px; object-fit: contain;' /></td>";
                    html += "<td>";
                    html += "<a href='QuanTri.aspx?action=edit&id=" + sp.Id + "' style='color: blue; margin-right: 10px; text-decoration: none; font-weight: bold;'>Sửa</a>";
                    html += "<a href='QuanTri.aspx?action=delete&id=" + sp.Id + "' style='color: red; text-decoration: none; font-weight: bold;' onclick=\"return confirm('Bạn có chắc muốn xóa sản phẩm này?');\">Xóa</a>";
                    html += "</td>";
                    html += "</tr>";
                }
                tbodyProducts.InnerHtml = html;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<Laptop> danhSach;
            if (Application["DanhSachLaptop"] != null)
            {
                danhSach = (List<Laptop>)Application["DanhSachLaptop"];
            }
            else
            {
                danhSach = new List<Laptop>();
            }

            int id = 0;
            if (txtId.Value != "")
            {
                id = int.Parse(txtId.Value);
            }

            string hinhAnh = txtHinhAnhCu.Value;

            if (fileHinhAnh.HasFile == true)
            {
                string tenFile = System.IO.Path.GetFileName(fileHinhAnh.FileName);
                string duongDan = Server.MapPath("~/assets/img/" + tenFile);
                fileHinhAnh.SaveAs(duongDan);
                hinhAnh = "assets/img/" + tenFile;
            }
            else if (hinhAnh == "")
            {
                hinhAnh = "assets/img/lenovo.png";
            }

            if (id == 0)
            {
                int idMoi = 1;
                if (danhSach.Count > 0)
                {
                    int maxId = 0;
                    for (int i = 0; i < danhSach.Count; i++)
                    {
                        if (danhSach[i].Id > maxId)
                        {
                            maxId = danhSach[i].Id;
                        }
                    }
                    idMoi = maxId + 1;
                }

                Laptop spMoi = new Laptop();
                spMoi.Id = idMoi;
                spMoi.TenSanPham = txtTenSanPham.Text;
                spMoi.GiaTien = decimal.Parse(txtGiaTien.Text);
                spMoi.HinhAnh = hinhAnh;
                spMoi.SoLuongTon = int.Parse(txtSoLuongTon.Text);

                danhSach.Add(spMoi);
            }
            else
            {
                Laptop spSua = null;
                for (int i = 0; i < danhSach.Count; i++)
                {
                    if (danhSach[i].Id == id)
                    {
                        spSua = danhSach[i];
                        break;
                    }
                }

                if (spSua != null)
                {
                    spSua.TenSanPham = txtTenSanPham.Text;
                    spSua.GiaTien = decimal.Parse(txtGiaTien.Text);
                    spSua.HinhAnh = hinhAnh;
                    spSua.SoLuongTon = int.Parse(txtSoLuongTon.Text);
                }
            }

            Application["DanhSachLaptop"] = danhSach;
            ClearForm();
            LoadData();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtId.Value = "";
            txtTenSanPham.Text = "";
            txtGiaTien.Text = "";
            txtHinhAnhCu.Value = "";
            imgPreview.Visible = false;
            txtSoLuongTon.Text = "10";
        }
    }
}