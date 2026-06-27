﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DangKy.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.DangKy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title></title>
    <link href="assets/css/Styles.css" rel="stylesheet" />
    <link rel="stylesheet" href="assets/font/fontawesome-free-6.4.0/css/all.css" />
</head>
<body>
    <form id="form1" runat="server">
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

        <main class="vung-dang-nhap">
            <div class="khung-dang-nhap">
                <h2 class="tieu-de-dang-nhap">ĐĂNG KÝ TÀI KHOẢN</h2>
                
                <div class="nhom-nhap-lieu">
                    <asp:TextBox ID="txtTaiKhoanDK" runat="server" CssClass="o-nhap-lieu" Placeholder="Tên đăng nhập"></asp:TextBox>
                </div>

                <div class="nhom-nhap-lieu">
                    <asp:TextBox ID="txtHoTenDK" runat="server" CssClass="o-nhap-lieu" Placeholder="Họ và tên"></asp:TextBox>
                </div>

                <div class="nhom-nhap-lieu">
                    <asp:TextBox ID="txtSDTDK" runat="server" CssClass="o-nhap-lieu" Placeholder="Số điện thoại"></asp:TextBox>
                </div>
                
                <div class="nhom-nhap-lieu">
                    <asp:TextBox ID="txtEmailDK" runat="server" CssClass="o-nhap-lieu" Placeholder="Email (Ví dụ: abc@gmail.com)"></asp:TextBox>
                </div>
                
                <div class="nhom-nhap-lieu">
                    <asp:TextBox ID="txtMatKhauDK" runat="server" CssClass="o-nhap-lieu" TextMode="Password" Placeholder="Mật khẩu"></asp:TextBox>
                </div>

                <div class="nhom-nhap-lieu">
                    <asp:TextBox ID="txtXacNhanMatKhau" runat="server" CssClass="o-nhap-lieu" TextMode="Password" Placeholder="Nhập lại mật khẩu"></asp:TextBox>
                </div>

                <asp:Label ID="lblThongBaoDK" runat="server" Font-Size="13px" Font-Bold="true"></asp:Label>

                <asp:Button ID="btnDangKy" runat="server" Text="ĐĂNG KÝ" CssClass="nut-dang-nhap" OnClick="btnDangKy_Click" />

                <div class="link-ho-tro" style="justify-content: center; margin-top: 20px;">
                    <a href="DangNhap.aspx">Đã có tài khoản? Đăng nhập tại đây</a>
                </div>
            </div>
        </main>
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
