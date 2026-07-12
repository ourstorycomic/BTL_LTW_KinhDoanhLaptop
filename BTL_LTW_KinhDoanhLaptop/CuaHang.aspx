<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CuaHang.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.CuaHang" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="assets/css/Styles.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/CuaHang.css" />
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

        <main class="khung-chinh" style="max-width: 1200px; margin: 20px auto; display: flex; gap: 20px;">
            
            <aside class="bo-loc-trai">
                <div class="khoi-loc">
                    <h3>THƯƠNG HIỆU</h3>
                    <asp:CheckBoxList ID="cblThuongHieu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="BoLoc_Changed">
                        <asp:ListItem Text="ASUS" Value="asus"></asp:ListItem>
                        <asp:ListItem Text="Lenovo" Value="lenovo"></asp:ListItem>
                        <asp:ListItem Text="Dell" Value="dell"></asp:ListItem>
                        <asp:ListItem Text="Macbook" Value="macbook"></asp:ListItem>
                        <asp:ListItem Text="Acer" Value="acer"></asp:ListItem>
                        <asp:ListItem Text="HP" Value="hp"></asp:ListItem>
                        <asp:ListItem Text="MSI" Value="msi"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>

                <div class="khoi-loc">
                    <h3>MỨC GIÁ</h3>
                    <asp:RadioButtonList ID="rblMucGia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="BoLoc_Changed">
                        <asp:ListItem Text="Tất cả các giá" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Dưới 15 triệu" Value="1"></asp:ListItem>
                        <asp:ListItem Text="15 - 25 triệu" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Trên 25 triệu" Value="3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </aside>

            <section class="danh-sach-sp-phai">
                
                <div class="thanh-cong-cu">
                    <span>Hiển thị kết quả lọc</span>
                    <div class="sap-xep">
                        <label>Sắp xếp theo: </label>
                        <asp:DropDownList ID="ddlSapXep" runat="server" AutoPostBack="true" OnSelectedIndexChanged="BoLoc_Changed">
                            <asp:ListItem Text="Mới nhất" Value="new"></asp:ListItem>
                            <asp:ListItem Text="Giá: Thấp đến Cao" Value="asc"></asp:ListItem>
                            <asp:ListItem Text="Giá: Cao đến Thấp" Value="desc"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="luoi-san-pham">
                    <asp:Repeater ID="rptLaptops" runat="server">
                        <ItemTemplate>
                            <div class="the-san-pham-chuan">
                                <div class="khung-anh-sp">
                                    <img src='<%# ResolveUrl(Eval("HinhAnh").ToString()) %>' alt='<%# Eval("TenSanPham") %>' />
                                </div>
                                <div class="thong-tin-sp">
                                    <div class="loai-sp">LAPTOP CHÍNH HÃNG</div>
                                    <h4 class="ten-sp"><%# Eval("TenSanPham") %></h4>
                    
                                    <div class="gia-sp"><%# String.Format("{0:N0} ₫", Eval("GiaTien")) %></div>
                    
                                    <div class="hanh-dong-sp">
                                        <a href="ChiTietSanPham.aspx?id=<%# Eval("Id") %>" class="btn-xem">Xem</a>
                                        <asp:LinkButton ID="btnMua" runat="server" CssClass="btn-mua" CommandArgument='<%# Eval("Id") %>' OnClick="btnMua_Click">Thêm giỏ hàng</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <div class="phan-trang">
                    <a href="#" class="active">1</a>
                    <a href="#">2</a>
                    <a href="#">3</a>
                    <a href="#">Tiếp ❯</a>
                </div>

            </section>
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
