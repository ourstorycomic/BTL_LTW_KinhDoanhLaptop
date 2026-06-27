<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TinTuc.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.TinTuc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="utf-8" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="assets/css/TinTuc.css" /> 
    <link rel="stylesheet" type="text/css" href="assets/css/Styles.css" />
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
        <section class="trang-tin-tuc" style="max-width: 1200px; margin: 0 auto;">
    <h2 class="tieu-de-tin-tuc">TIN TỨC CÔNG NGHỆ & KHUYẾN MÃI</h2>
    
    <div class="luoi-tin-tuc">
        <article class="the-tin-tuc">
            <div class="anh-tin-tuc">
                <img src="assets/img/lenovo.png" alt="Đánh giá laptop" />
            </div>
            <div class="noi-dung-tin">
                <span class="ngay-dang">15/10/2023</span>
                <h3><a href="#">Đánh giá chi tiết Asus TUF Gaming 2023: Liệu có đáng tiền?</a></h3>
                <p>Khám phá sức mạnh tản nhiệt và hiệu năng thực tế của dòng laptop gaming quốc dân...</p>
                <a href="#" class="btn-doc-tiep">Đọc tiếp ➔</a>
            </div>
        </article>

        <article class="the-tin-tuc">
            <div class="anh-tin-tuc">
                <img src="assets/img/lenovo.png" alt="Mẹo vặt" />
            </div>
            <div class="noi-dung-tin">
                <span class="ngay-dang">12/10/2023</span>
                <h3><a href="#">5 Mẹo giúp tăng tuổi thọ pin Laptop cho sinh viên</a></h3>
                <p>Pin chai luôn là nỗi ám ảnh. Hãy áp dụng ngay 5 mẹo nhỏ này để pin laptop của bạn luôn "trâu"...</p>
                <a href="#" class="btn-doc-tiep">Đọc tiếp ➔</a>
            </div>
        </article>

        <article class="the-tin-tuc">
            <div class="anh-tin-tuc">
                <img src="assets/img/lenovo.png" alt="Khuyến mãi" />
            </div>
            <div class="noi-dung-tin">
                <span class="ngay-dang">10/10/2023</span>
                <h3><a href="#">Back to School: Giảm giá lên tới 3 triệu đồng cho Macbook</a></h3>
                <p>Chương trình ưu đãi lớn nhất năm dành riêng cho các bạn Tân sinh viên khi mua sắm tại cửa hàng...</p>
                <a href="#" class="btn-doc-tiep">Đọc tiếp ➔</a>
            </div>
        </article>
    </div>
</section>
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
