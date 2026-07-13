<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrangKhongTonTai.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.TrangKhongTonTai" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>404 - Trang không tồn tại</title>
    <link href="assets/css/Styles.css" rel="stylesheet" />
    <link rel="stylesheet" href="assets/font/fontawesome-free-6.4.0/css/all.css" />
    <style>
        .khu-vuc-404 {
            text-align: center;
            padding: 100px 20px;
            min-height: 50vh;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
        }
        .khu-vuc-404 h1 {
            font-size: 80px;
            color: #e3001b;
            margin-bottom: 20px;
        }
        .khu-vuc-404 h3 {
            font-size: 24px;
            color: #333;
            margin-bottom: 30px;
        }
        .khu-vuc-404 p {
            color: #666;
            margin-bottom: 40px;
        }
        .nut-ve-trang-chu {
            display: inline-block;
            background-color: #e3001b;
            color: white;
            padding: 12px 25px;
            text-decoration: none;
            border-radius: 5px;
            font-weight: bold;
            transition: background-color 0.3s;
        }
        .nut-ve-trang-chu:hover {
            background-color: #c00017;
        }
    </style>
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
                        <i class="fa-solid fa-cart-shopping" ></i> (<span id="lblSoLuongGio" runat="server">0</span>)
                    </a>

                    <div id="divChuaDangNhap" runat="server" class="khu-vuc-tai-khoan account-area">
                        <a href="DangNhap.aspx" class="login-link"><i class="fa-solid fa-user"></i> Đăng nhập</a>
                    </div>

                    <div id="divDaDangNhap" runat="server" class="khu-vuc-tai-khoan account-area user-dropdown" visible="false">
                        <img id="imgAvatar" runat="server" src="assets/img/lenovo.png" class="user-avatar" />
                        <span id="lblTenTaiKhoan" runat="server"></span>
                        <i class="fa-solid fa-caret-down"></i>
                        <div class="dropdown-content">
                            <a href="HoSo.aspx"><i class="fa-solid fa-address-card"></i> Hồ sơ cá nhân</a>
                            <a href="LichSuMuaHang.aspx"><i class="fa-solid fa-clock-rotate-left"></i> Lịch sử mua hàng</a>
                            <a id="linkQuanTri" runat="server" href="QuanTri.aspx" visible="false"><i class="fa-solid fa-gear"></i> Quản trị</a>
                            <a id="linkThongKe" runat="server" href="BaoCao.aspx" visible="false"><i class="fa-solid fa-chart-pie"></i> Thống kê</a>
                            <a href="DangNhap.aspx?logout=true" class="logout-link"><i class="fa-solid fa-right-from-bracket"></i> Đăng xuất</a>
                        </div>
                    </div>
                </div>
            </div>
        </header>

        <main class="khu-vuc-404">
            <h1>404</h1>
            <h3>Ôi hỏng! Trang bạn tìm không tồn tại.</h3>
            <p>Có thể đường dẫn đã bị thay đổi, bị xóa, hoặc bạn đã nhập sai địa chỉ.</p>
            <a href="TrangChu.aspx" class="nut-ve-trang-chu">QUAY VỀ TRANG CHỦ</a>
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
    <button id="nut-cuon-len" onclick="cuonLenDauTrang()" title="Lên đầu trang">
        <i class="fa-solid fa-arrow-up"></i>
    </button>
</body>
</html>
