﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChiTietSanPham.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.ChiTietSanPham" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="assets/css/Styles.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/ChiTietSanPham.css" />
    <link rel="stylesheet" href="assets/font/fontawesome-free-6.4.0/css/all.css" />
</head>
<body>
    <form id="form1" runat="server" style="display: flex; flex-direction: column; flex: 1; margin: 0; padding: 0;">
        <header class="phan-dau-trang">
            <div class="noi-dung-phan-dau">
                <a href="TrangChu.aspx" class="chu-logo">
                    MOBILE EDUCATION
                </a>
        
                <nav class="menu-ngang">
                    <a href="TrangChu.aspx">Trang chủ</a>
                    <a href="GioiThieu.aspx">Giới thiệu</a>
                    <a href="CuaHang.aspx">Cửa hàng</a>
                    <a href="TinTuc.aspx">Tin tức</a>
                </nav>

                <div class="cum-phai-header header-right">
                    <div class="search-bar-container">
                        <input type="text" id="txtSearch" placeholder="Tìm kiếm..." onkeypress="if(event.keyCode==13) { window.location.href='CuaHang.aspx?search=' + encodeURIComponent(this.value); return false; }" class="search-input" />
                        <button type="button" onclick="window.location.href='CuaHang.aspx?search=' + encodeURIComponent(document.getElementById('txtSearch').value);" class="search-btn"><i class="fa-solid fa-magnifying-glass"></i></button>
                    </div>

                    <a href="ThanhToan.aspx" class="cart-link">
                        <i class="fa-solid fa-cart-shopping" ></i> (<asp:Label ID="lblSoLuongGio" runat="server" Text="0"></asp:Label>)
                    </a>

                    <div class="khu-vuc-tai-khoan account-area" id="divTaiKhoan" runat="server"></div>
                </div>
            </div>
        </header>

        <div class="container main-container">
            <% if (spHienTai != null) { %>
                <div style="color: blue; font-size: 14px; margin-bottom: 20px;">
                    <a href="TrangChu.aspx" style="color: blue; text-decoration: none;">Trang chủ</a> / Laptop / <%= spHienTai.TenSanPham %>
                </div>

                <div class="chi-tiet-layout" style="display: flex; gap: 30px;">
                    <div class="cot-trai flex-1">
                        <img src="<%= ResolveUrl(spHienTai.HinhAnh) %>" class="anh-lon" alt="<%= spHienTai.TenSanPham %>" style="width: 100%; object-fit: contain;" />
                    </div>

                    <div class="cot-phai flex-1">
                        <div class="ten-sp product-title-lg"><%= spHienTai.TenSanPham %></div>
                        <div class="product-meta-info">Mã SP: <b><%= spHienTai.Id %></b> | Đánh giá: ⭐⭐⭐⭐⭐</div>
                        
                        <div class="gia-sp product-price-lg"><%= String.Format("{0:N0} đ", spHienTai.GiaTien) %></div>

                        <div class="khung-thong-so product-specs-box">
                            <h3 class="mb-10">Thông số sản phẩm</h3>
                            <ul class="specs-list">
                                <li>- <b>CPU:</b> Intel Core i5 120U</li>
                                <li>- <b>RAM:</b> 16GB DDR4</li>
                                <li>- <b>Ổ cứng:</b> 512GB SSD PCIe NVMe</li>
                                <li>- <b>Màn hình:</b> 15.6 inch FHD IPS</li>
                            </ul>
                        </div>

                        <div class="flex-gap-15">
                            <asp:Button ID="btnMuaNgay" runat="server" Text="MUA NGAY" class="btn-buy-now" OnClick="btnMuaNgay_Click" />
                            <asp:Button ID="btnThemVaoGio" runat="server" Text="THÊM VÀO GIỎ HÀNG" class="btn-add-cart" OnClick="btnThemVaoGio_Click" />
                        </div>
                    </div>
                </div>

                <div class="mo-ta-sp product-desc-section">
                    <h2 class="mb-15">Đánh giá chi tiết</h2>
                    <p class="line-height-16">Chiếc laptop <%= spHienTai.TenSanPham %> là một sản phẩm văn phòng và học tập "nhẹ ví, nhẹ balo" dành cho những ai cần một người bạn đồng hành đáng tin cậy. Thiết kế gọn nhẹ và mức giá thân thiện, hiệu suất đỉnh cao giúp bạn có những trải nghiệm tốt nhất!</p>
                </div>

            <% } else { %>
                <h2 class="not-found-msg">Không tìm thấy sản phẩm hoặc sản phẩm không tồn tại!</h2>
                <div class="mt-20-center">
                    <a href="TrangChu.aspx" class="btn-back-home">Quay lại trang chủ</a>
                </div>
            <% } %>
        </div>

        <footer class="footer-chuyen-nghiep">
            <div class="footer-container">
                <div class="footer-col">
                    <h3>MOBILE EDUCATION</h3>
                    <p>Hệ thống bán lẻ laptop chính hãng hàng đầu. Cam kết mang lại những sản phẩm công nghệ chất lượng cùng dịch vụ tận tâm nhất cho khách hàng.</p>
                </div>
                <div class="footer-col">
                    <h3>CHÍNH SÁCH</h3>
                    <ul>
                        <li><a href="#">Chính sách bảo hành</a></li>
                        <li><a href="#">Chính sách đổi trả</a></li>
                        <li><a href="#">Bảo mật thông tin</a></li>
                        <li><a href="#">Hướng dẫn thanh toán</a></li>
                    </ul>
                </div>
                <div class="footer-col">
                    <h3>THÔNG TIN LIÊN HỆ</h3>
                    <ul class="list-none-p0">
                        <li><i class="fa-solid fa-location-dot mr-8"></i> 96 Định Công, Hoàng Mai, Hà Nội</li>
                        <li><i class="fa-solid fa-phone mr-8"></i> Hotline: 1900.1234</li>
                        <li><i class="fa-solid fa-envelope mr-8"></i> Email: hotro@mobileeducation.vn</li>
                    </ul>
                </div>
                <div class="footer-col">
                    <h3>KẾT NỐI VỚI CHÚNG TÔI</h3>
                    <div class="social-links">
                        <a href="#"><i class="fa-brands fa-facebook social-icon"></i> Facebook</a>
                        <a href="#"><i class="fa-brands fa-youtube social-icon"></i> YouTube</a>
                        <a href="#"><i class="fa-brands fa-instagram social-icon"></i> Instagram</a>
                    </div>
                </div>
            </div>
            <div class="footer-bottom">
                <p>© 2026 Mobile Education - Cửa hàng Laptop Chính Hãng. Tất cả các quyền được bảo lưu.</p>
            </div>
        </footer>
    </form>

    <div id="toast"></div>
    <script src="assets/js/main.js"></script>

</body>
</html>
