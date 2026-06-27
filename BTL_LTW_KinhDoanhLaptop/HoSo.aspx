﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HoSo.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.HoSo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Hồ sơ cá nhân</title>
    <link href="assets/css/Styles.css" rel="stylesheet" />
    <link rel="stylesheet" href="assets/font/fontawesome-free-6.4.0/css/all.css" />
</head>
<body>
    <form id="form1" runat="server" method="post">
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

<main class="flex-1-pad-20">
    <div class="profile-container">
        <h2 style="text-align: center; color: #333; margin-bottom: 20px;">HỒ SƠ CÁ NHÂN</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Green" style="display:block; text-align:center; margin-bottom:15px; font-weight:bold;"></asp:Label>
        
        <div style="text-align: center; margin-bottom: 20px;">
            <asp:Image ID="imgPreview" runat="server" Width="120px" Height="120px" style="border-radius: 50%; object-fit: cover; border: 3px solid #8bc34a; margin-bottom: 10px;" />
            <br />
            <asp:FileUpload ID="fileAvatar" runat="server" />
        </div>

        <div class="form-group">
            <label>Tài khoản</label>
            <asp:TextBox ID="txtTaiKhoan" runat="server" CssClass="form-control" ReadOnly="true" BackColor="#e9ecef"></asp:TextBox>
        </div>
        
        <div class="form-group">
            <label>Họ tên</label>
            <asp:TextBox ID="txtHoTen" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <label>Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
        </div>

        <div class="form-group">
            <label>Số điện thoại</label>
            <asp:TextBox ID="txtSDT" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>Địa chỉ</label>
            <asp:TextBox ID="txtDiaChi" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
        </div>

        <div class="form-group">
            <label>Mật khẩu mới (Để trống nếu không đổi)</label>
            <asp:TextBox ID="txtMatKhau" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
        </div>

        <div style="text-align: center;">
            <asp:Button ID="btnSave" runat="server" Text="Lưu thay đổi" CssClass="btn-save" OnClick="btnSave_Click" />
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
