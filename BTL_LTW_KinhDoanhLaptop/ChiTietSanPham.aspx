<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChiTietSanPham.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.ChiTietSanPham" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="ChiTietSanPham.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <% if (spHienTai != null) { %>

                <div style="color: blue; font-size: 14px;">
                    <a href="TrangChu.aspx" style="color: blue; text-decoration: none;">Trang chủ</a> / Laptop / <%= spHienTai.TenSanPham %>
                </div>

                <div class="chi-tiet-layout">
                    <div class="cot-trai">
                        <img src="<%= ResolveUrl(spHienTai.HinhAnh) %>" class="anh-lon" alt="<%= spHienTai.TenSanPham %>" />
                    </div>

                    <div class="cot-phai">
                        <div class="ten-sp"><%= spHienTai.TenSanPham %></div>
                       <div>Mã SP: <b><%= spHienTai.Id %></b> | Đánh giá: ⭐⭐⭐⭐⭐</div>
                        
                        <div class="gia-sp"><%= String.Format("{0:N0} đ", spHienTai.GiaTien) %></div>

                        <div class="khung-thong-so">
                            <h3>Thông số sản phẩm</h3>
                            <ul>
                                <li>- <b>CPU:</b> Intel Core i5 120U</li>
                                <li>- <b>RAM:</b> 16GB DDR4</li>
                                <li>- <b>Ổ cứng:</b> 512GB SSD PCIe NVMe</li>
                                <li>- <b>Màn hình:</b> 15.6 inch FHD IPS</li>
                            </ul>
                        </div>

                        <asp:Button ID="btnMuaNgay" runat="server" Text="MUA NGAY" CssClass="btn-mua" OnClick="btnMuaNgay_Click" />
                        <asp:Button ID="btnThemVaoGio" runat="server" Text="THÊM VÀO GIỎ HÀNG" CssClass="btn-them" OnClick="btnThemVaoGio_Click" />
                    </div>
                </div>

                <div class="mo-ta-sp">
                    <h2>Đánh giá chi tiết</h2>
                    <p>Chiếc laptop <%= spHienTai.TenSanPham %> là một sản phẩm văn phòng và học tập "nhẹ ví, nhẹ balo" dành cho những ai cần một người bạn đồng hành đáng tin cậy. Thiết kế gọn nhẹ và mức giá thân thiện,hiệu xuất đỉnh cao giúp bạn có những trải nghiệm tốt nhất!</p>
                </div>

            <% } else { %>
                <h2>Không tìm thấy sản phẩm hoặc sản phẩm không tồn tại!</h2>
                <a href="TrangChu.aspx">Quay lại trang chủ</a>
            <% } %>
        </div>
    </form>
</body>
</html>
