<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThanhToan.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.ThanhToan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="assets/css/Styles.css" />
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

        <main style="max-width: 1200px; width: 100%; margin: 20px auto; padding: 0 15px; flex: 1; box-sizing: border-box;">
            <div style="background: white; padding: 30px; border: 1px solid #e0e0e0;">
                <h2 style="margin-bottom: 25px; color: #333;" id="TieuDeTrang" runat="server">Thông tin đơn hàng</h2>
                
                <asp:Repeater ID="rptGioHang" runat="server">
                    <HeaderTemplate>
                        <table style="width:100%; border-collapse:collapse; margin-bottom: 20px;">
                            <tr style="background:#8bc34a; color:white;">
                                <th style="padding:15px; border: 1px solid #ddd;">Tên sản phẩm</th>
                                <th style="padding:15px; border: 1px solid #ddd;">Số lượng</th>
                                <th style="padding:15px; border: 1px solid #ddd;">Đơn giá</th>
                                <th style="padding:15px; border: 1px solid #ddd;">Thành tiền</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="text-align:center;">
                            <td style="padding:15px; border: 1px solid #ddd;"><%# Eval("TenSanPham") %></td>
                            <td style="padding:15px; border: 1px solid #ddd;"><%# Eval("SoLuong") %></td>
                            <td style="padding:15px; border: 1px solid #ddd; color: #e3001b;"><%# String.Format("{0:N0} ₫", Eval("DonGia")) %></td>
                            <td style="padding:15px; border: 1px solid #ddd; color: #e3001b; font-weight: bold;"><%# String.Format("{0:N0} ₫", Eval("ThanhTien")) %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate></table></FooterTemplate>
                </asp:Repeater>

                <div style="text-align: right; font-size: 20px; font-weight: bold; color: #e3001b; margin-bottom: 20px;">
                    <asp:Label ID="lblTongTien" runat="server" Text=""></asp:Label>
                </div>

                <div id="divThongTinKhachHang" runat="server" style="background: #f9f9f9; padding: 20px; border: 1px solid #ddd; margin-bottom: 20px;">
                    <h3 style="margin-bottom: 20px; color: #333; font-size: 18px;">Thông tin nhận hàng</h3>
                    
                    <div class="mb-15">
                        <label style="display: block; font-weight: bold; margin-bottom: 5px;">Họ và tên:</label>
                        <asp:TextBox ID="txtHoTen" runat="server" style="width: 100%; padding: 10px; border: 1px solid #ccc; box-sizing: border-box;"></asp:TextBox>
                    </div>

                    <div class="mb-15">
                        <label style="display: block; font-weight: bold; margin-bottom: 5px;">Số điện thoại:</label>
                        <asp:TextBox ID="txtDienThoai" runat="server" style="width: 100%; padding: 10px; border: 1px solid #ccc; box-sizing: border-box;"></asp:TextBox>
                    </div>

                    <div class="mb-15">
                        <label style="display: block; font-weight: bold; margin-bottom: 5px;">Địa chỉ giao hàng:</label>
                        <asp:TextBox ID="txtDiaChi" runat="server" TextMode="MultiLine" Rows="3" style="width: 100%; padding: 10px; border: 1px solid #ccc; box-sizing: border-box;"></asp:TextBox>
                    </div>
                </div>

                <div style="text-align: right;">
                    <asp:Button ID="btnTienHanh" runat="server" Text="Tiến hành thanh toán" OnClick="btnTienHanh_Click" style="padding: 12px 35px; background: #8bc34a; color: white; border: none; font-size: 16px; font-weight: bold; cursor: pointer;" />
                    <asp:Button ID="btnThanhToan" runat="server" Text="Đặt hàng" OnClick="btnThanhToan_Click" style="padding: 12px 35px; background: #8bc34a; color: white; border: none; font-size: 16px; font-weight: bold; cursor: pointer;" />
                </div>
                
                <div style="text-align: right;">
                    <asp:Label ID="lblThongBao" runat="server" Text="" style="display:block; margin-top:15px; font-size: 16px;"></asp:Label>
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
</html>
