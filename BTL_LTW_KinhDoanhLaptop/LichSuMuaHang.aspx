<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LichSuMuaHang.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.LichSuMuaHang" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lịch sử mua hàng</title>
    <link href="assets/css/LichSuMuaHang.css" rel="stylesheet" />
    <link href="assets/css/Styles.css" rel="stylesheet" />
    <link rel="stylesheet" href="assets/font/fontawesome-free-6.4.0/css/all.css" />
</head>
<body>
    <form id="form1" runat="server">
        <header class="phan-dau-trang">
            <div class="noi-dung-phan-dau">
                <a href="TrangChu.aspx" class="chu-logo">MOBILE EDUCATION</a>
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
                    <div id="divChuaDangNhap" runat="server" class="khu-vuc-tai-khoan account-area">
                        <a href="DangNhap.aspx" class="login-link"><i class="fa-solid fa-user"></i> Đăng nhập</a>
                    </div>
                    <div id="divDaDangNhap" runat="server" class="khu-vuc-tai-khoan account-area user-dropdown" visible="false">
                        <img id="imgAvatar" runat="server" src="assets/img/lenovo.png" class="user-avatar" />
                        <span id="lblTenTaiKhoan" runat="server"></span>
                        <i class="fa-solid fa-caret-down"></i>
                        <div class="dropdown-content">
                            <a href="HoSo.aspx"><i class="fa-solid fa-address-card"></i> Hồ sơ cá nhân</a>
                            <a href="LichSuMuaHang.aspx"><i class="fa-solid fa-box"></i> Lịch sử mua hàng</a>
                            <a id="linkQuanTri" runat="server" href="QuanTri.aspx" visible="false"><i class="fa-solid fa-gear"></i> Quản trị</a>
                            <a id="linkThongKe" runat="server" href="BaoCao.aspx" visible="false"><i class="fa-solid fa-chart-pie"></i> Thống kê</a>
                            <a href="DangNhap.aspx?logout=true" class="logout-link"><i class="fa-solid fa-right-from-bracket"></i> Đăng xuất</a>
                        </div>
                    </div>
                </div>
            </div>
        </header>

        <div id="vung-chua-noi-dung"> 
           <div class="noi-dung-phai">
                <h2 style="margin-top: 0;">Lịch sử mua hàng</h2>

                <div class="vung-loc-don-hang">
                    <input type="text" class="o-nhap-nho" placeholder="Mã đơn hàng" />
                    <input type="text" class="o-nhap-nho" placeholder="Tên sản phẩm" />
                    <button class="nut-den-nho"><i class="fa-solid fa-magnifying-glass"></i></button>

                    <div class="cac-tab-trang-thai">
                        <a href="#" class="tab-dang-chon">Tất cả</a>
                        <a href="#">Đang giao</a>
                        <a href="#">Đã giao</a>
                        <a href="#">Đã hủy</a>
                    </div>
                    <div class="clear-float"></div>
                </div>

                <asp:Repeater ID="rptDonHang" runat="server" OnItemDataBound="rptDonHang_ItemDataBound">
                    <ItemTemplate>
                        <div class="the-don-hang">
                            <div class="phan-dau-don">
                                <span class="ma-don">Order <b>#<asp:Label ID="lblMaDon" runat="server"></asp:Label></b></span>
                                <span id="lblTrangThai" runat="server" class="nhan-trang-thai"></span>
                                <div class="clear-float"></div>
                            </div>
                            <p class="ngay-dat">Ngày đặt: <asp:Label ID="lblNgayDat" runat="server"></asp:Label></p>

                            <asp:Repeater ID="rptChiTiet" runat="server" OnItemDataBound="rptChiTiet_ItemDataBound">
                                <ItemTemplate>
                                    <div class="san-pham-trong-don">
                                        <asp:Image ID="imgSp" runat="server" CssClass="anh-sp-don" />
                                        <div class="thong-tin-sp-don">
                                            <p class="ten-sp-don"><asp:Label ID="lblTenSp" runat="server"></asp:Label></p>
                                            <p class="gia-sp-don"><asp:Label ID="lblGiaSp" runat="server"></asp:Label>đ</p>
                                        </div>
                                        <div class="so-luong-don">x<asp:Label ID="lblSoLuong" runat="server"></asp:Label></div>
                                        <div class="clear-float"></div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            <div class="duong-ke-ngang"></div>

                            <div class="tong-tien-don">
                                <span class="chu-tong">Tổng chi tiết:</span>
                                <span class="so-tien-tong"><asp:Label ID="lblTongTien" runat="server"></asp:Label>đ</span>
                                <div class="clear-float"></div>
                            </div>

                            <div class="vung-nut-hanh-dong">
                                <button type="button" class="nut-trang">Xem chi tiết</button>
                                <button id="btnHanhDong" runat="server" type="button" class="nut-den"></button>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                
                <asp:Label ID="lblKhongCoDon" runat="server" Visible="false" 
                    Text="<p style='text-align:center; color:#888; font-size: 16px; padding: 50px;'>Bạn chưa mua đơn hàng nào.</p>"></asp:Label>

                <div class="clear-float"></div>
            </div>
            <div class="clear-float"></div>
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
</body>
</html>