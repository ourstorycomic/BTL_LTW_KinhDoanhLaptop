<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThanhToan.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.ThanhToan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Thanh Toán - Mobile Education</title>
    <link rel="stylesheet" type="text/css" href="assets/css/Styles.css?v=1" />
    <link rel="stylesheet" href="assets/font/fontawesome-free-6.4.0/css/all.css" />
</head>

<body>
    <form id="form1" runat="server" style="display: flex; flex-direction: column; flex: 1; margin: 0; padding: 0; min-height: 100vh;">
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
                        <input type="text" id="txtSearch" placeholder="Tìm kiếm..."
                            onkeypress="if(event.keyCode==13) { window.location.href='CuaHang.aspx?search=' + encodeURIComponent(this.value); return false; }"
                            class="search-input" />
                        <button type="button"
                            onclick="window.location.href='CuaHang.aspx?search=' + encodeURIComponent(document.getElementById('txtSearch').value);"
                            class="search-btn"><i class="fa-solid fa-magnifying-glass"></i></button>
                    </div>

                    <a href="ThanhToan.aspx" class="cart-link">
                        <i class="fa-solid fa-cart-shopping"></i> (<asp:Label ID="lblSoLuongGio" runat="server"
                            Text="0"></asp:Label>)
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
                            <a id="linkQuanTri" runat="server" href="QuanTri.aspx" visible="false"><i class="fa-solid fa-gear"></i> Quản trị</a>
                            <a id="linkThongKe" runat="server" href="BaoCao.aspx" visible="false"><i class="fa-solid fa-chart-pie"></i> Thống kê</a>
                            <a href="DangNhap.aspx?logout=true" class="logout-link"><i class="fa-solid fa-right-from-bracket"></i> Đăng xuất</a>
                        </div>
                    </div>
                </div>
            </div>
        </header>

        <main class="main-container flex-1"
            style="max-width: 1200px; width: 100%; margin: 20px auto; padding: 0 15px; flex: 1; box-sizing: border-box;">
            <div style="background: white; padding: 30px; border: 1px solid #e0e0e0; border-radius: 8px;">
                <h2 style="margin-bottom: 25px; color: #333;" id="TieuDeTrang" runat="server">Thông tin đơn hàng</h2>

                <table style="width:100%; border-collapse:collapse; margin-bottom: 20px;">
                    <thead>
                        <tr style="background:#8bc34a; color:white;">
                            <th style="padding:15px; border: 1px solid #ddd;">Hình ảnh</th>
                            <th style="padding:15px; border: 1px solid #ddd;">Tên sản phẩm</th>
                            <th style="padding:15px; border: 1px solid #ddd;">Đơn giá</th>
                            <th style="padding:15px; border: 1px solid #ddd;">Số lượng</th>
                            <th style="padding:15px; border: 1px solid #ddd;">Thành tiền</th>
                            <th style="padding:15px; border: 1px solid #ddd;">Thao tác</th>
                        </tr>
                    </thead>
                    <tbody style="text-align: center;">
                        <asp:Literal ID="litGioHang" runat="server"></asp:Literal>
                    </tbody>
                </table>

                <div style="display: flex; justify-content: flex-end; margin: 20px 0; padding-top: 15px; border-top: 2px solid #eee;">
                    <div style="font-size: 22px; font-weight: bold; color: #e3001b;">
                        <asp:Label ID="lblTongTien" runat="server" Text=""></asp:Label>
                    </div>
                </div>

                <div id="divThongTinKhachHang" runat="server"
                    style="background: #f9f9f9; padding: 20px; border: 1px solid #ddd; margin-bottom: 20px; border-radius: 8px; box-shadow: 0 2px 10px rgba(0,0,0,0.05);">
                    <h3 style="margin-bottom: 20px; color: #333; font-size: 18px; border-bottom: 1px solid #ddd; padding-bottom: 10px;">
                        Thông tin nhận hàng</h3>

                    <div class="mb-15" style="margin-bottom: 15px;">
                        <label style="display: block; font-weight: bold; margin-bottom: 8px;">Họ và tên:</label>
                        <asp:TextBox ID="txtHoTen" runat="server"
                            style="width: 100%; padding: 10px; border: 1px solid #ccc; box-sizing: border-box; border-radius: 5px; outline: none;">
                        </asp:TextBox>
                    </div>

                    <div class="mb-15" style="margin-bottom: 15px;">
                        <label style="display: block; font-weight: bold; margin-bottom: 8px;">Số điện thoại:</label>
                        <asp:TextBox ID="txtDienThoai" runat="server"
                            style="width: 100%; padding: 10px; border: 1px solid #ccc; box-sizing: border-box; border-radius: 5px; outline: none;">
                        </asp:TextBox>
                    </div>

                    <div class="mb-15" style="margin-bottom: 15px;">
                        <label style="display: block; font-weight: bold; margin-bottom: 8px;">Địa chỉ giao
                            hàng:</label>
                        <asp:TextBox ID="txtDiaChi" runat="server" TextMode="MultiLine" Rows="3"
                            style="width: 100%; padding: 10px; border: 1px solid #ccc; box-sizing: border-box; border-radius: 5px; outline: none;">
                        </asp:TextBox>
                    </div>

                    <h3 style="margin-top: 30px; margin-bottom: 20px; color: #333; font-size: 18px; border-bottom: 1px solid #ddd; padding-bottom: 10px;">
                        Phương thức thanh toán</h3>

                    <div class="mb-15" style="margin-bottom: 15px;">
                        <asp:RadioButtonList ID="rblPhuongThuc" runat="server" RepeatDirection="Vertical"
                            CellPadding="5" onchange="hienThiKhungThe()">
                            <asp:ListItem Value="COD" Selected="True">Thanh toán khi nhận hàng</asp:ListItem>
                            <asp:ListItem Value="CARD">Thẻ tín dụng / Ghi nợ</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div id="divCardDetails"
                        style="display: none; background: #fff; padding: 15px; border: 1px dashed #e3001b; margin-bottom: 15px; border-radius: 5px;">
                        <p style="font-weight: bold; color: #333; margin-bottom: 15px;">Nhập thông tin thẻ thanh toán:</p>
                        <div style="margin-bottom: 15px;">
                            <label style="display: block; font-weight: bold; margin-bottom: 5px;">Tên in trên thẻ:</label>
                            <asp:TextBox ID="txtTenTrenThe" runat="server"
                                style="width: 100%; padding: 10px; border: 1px solid #ccc; border-radius: 4px; outline: none; box-sizing: border-box; text-transform: uppercase;">
                            </asp:TextBox>
                        </div>
                        <div style="margin-bottom: 15px;">
                            <label style="display: block; font-weight: bold; margin-bottom: 5px;">Số thẻ:</label>
                            <asp:TextBox ID="txtSoThe" runat="server"
                                style="width: 100%; padding: 10px; border: 1px solid #ccc; border-radius: 4px; outline: none; box-sizing: border-box;">
                            </asp:TextBox>
                        </div>
                        <div style="display: flex; gap: 15px;">
                            <div style="flex: 1;">
                                <label style="display: block; font-weight: bold; margin-bottom: 5px;">Ngày hết hạn (MM/YY):</label>
                                <asp:TextBox ID="txtNgayHetHan" runat="server"
                                    style="width: 100%; padding: 10px; border: 1px solid #ccc; border-radius: 4px; outline: none; box-sizing: border-box;"
                                    placeholder="VD: 12/25"></asp:TextBox>
                            </div>
                            <div style="flex: 1;">
                                <label style="display: block; font-weight: bold; margin-bottom: 5px;">Mã CCV/CVV:</label>
                                <asp:TextBox ID="txtCCV" runat="server"
                                    style="width: 100%; padding: 10px; border: 1px solid #ccc; border-radius: 4px; outline: none; box-sizing: border-box;"
                                    TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div style="text-align: right; margin-top: 20px;">
                    <asp:Button ID="btnTienHanh" runat="server" Text="Tiến hành thanh toán ❯"
                        OnClick="btnTienHanh_Click"
                        style="padding: 14px 40px; background: linear-gradient(to right, #8bc34a, #689f38); color: white; border: none; border-radius: 30px; font-size: 16px; font-weight: bold; cursor: pointer; box-shadow: 0 4px 15px rgba(139, 195, 74, 0.4); text-transform: uppercase;" />

                    <asp:Button ID="btnThanhToan" runat="server" Text="Xác nhận Đặt hàng ✔"
                        OnClick="btnThanhToan_Click"
                        style="padding: 14px 40px; background: linear-gradient(to right, #e3001b, #c62828); color: white; border: none; border-radius: 30px; font-size: 16px; font-weight: bold; cursor: pointer; box-shadow: 0 4px 15px rgba(227, 0, 27, 0.4); text-transform: uppercase;" />
                </div>

                <div style="text-align: right;">
                    <asp:Label ID="lblThongBao" runat="server" Text=""
                        style="display:block; margin-top:15px; font-size: 16px;"></asp:Label>
                </div>
            </div>
        </main>

        <footer class="footer-chuyen-nghiep">
            <div class="footer-container">
                <div class="footer-col">
                    <h3>MOBILE EDUCATION</h3>
                    <p>Hệ thống bán lẻ laptop chính hãng hàng đầu. Cam kết mang lại những sản phẩm công nghệ chất
                        lượng cùng dịch vụ tận tâm nhất cho khách hàng.</p>
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
    <script src="assets/js/main.js?v=1"></script>

</body>

</html>