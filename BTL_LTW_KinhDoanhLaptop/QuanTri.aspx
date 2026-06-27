﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuanTri.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.QuanTri" %>

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <meta charset="utf-8" />
        <title></title>
        <link href="assets/css/Styles.css" rel="stylesheet" />
        <link rel="stylesheet" href="assets/font/fontawesome-free-6.4.0/css/all.css" />
        <style>
            #gvProducts th,
            #gvProducts td {
                padding: 10px;
            }
        </style>
    </head>

    <body>
        <form id="form1" runat="server" method="post" enctype="multipart/form-data">
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

                        <div class="khu-vuc-tai-khoan account-area" id="divTaiKhoan" runat="server"></div>
                    </div>
                </div>
            </header>

            <main
                style="max-width: 1200px; width: 100%; margin: 20px auto; padding: 0 15px; flex: 1; box-sizing: border-box;">

                <h2 class="admin-title">QUẢN LÝ SẢN PHẨM</h2>


                <div class="admin-form"
                    style="background: white; padding: 20px; border: 1px solid #ddd; margin-bottom: 20px;">
                    <h3>Thêm / Sửa Sản Phẩm</h3>
                    <asp:HiddenField ID="txtId" runat="server" />

                    <div class="mb-15">
                        <label style="display: block; margin-bottom: 5px; font-weight: bold;">Tên sản phẩm:</label>
                        <asp:TextBox ID="txtTenSanPham" runat="server"
                            style="width: 100%; padding: 8px; border: 1px solid #ccc; box-sizing: border-box;">
                        </asp:TextBox>
                    </div>

                    <div class="mb-15">
                        <label style="display: block; margin-bottom: 5px; font-weight: bold;">Giá tiền (VND):</label>
                        <asp:TextBox ID="txtGiaTien" runat="server" type="number"
                            style="width: 100%; padding: 8px; border: 1px solid #ccc; box-sizing: border-box;">
                        </asp:TextBox>
                    </div>

                    <div class="mb-15">
                        <label style="display: block; margin-bottom: 5px; font-weight: bold;">Hình ảnh sản phẩm:</label>
                        <asp:FileUpload ID="fileHinhAnh" runat="server"
                            style="width: 100%; padding: 8px; border: 1px solid #ccc; box-sizing: border-box;" />
                        <asp:HiddenField ID="txtHinhAnhCu" runat="server" />
                        <asp:Image ID="imgPreview" runat="server"
                            style="max-width: 150px; margin-top: 10px; display: block;" Visible="false" />
                    </div>

                    <div class="mb-15">
                        <label style="display: block; margin-bottom: 5px; font-weight: bold;">Số lượng tồn:</label>
                        <asp:TextBox ID="txtSoLuongTon" runat="server" type="number" Text="10"
                            style="width: 100%; padding: 8px; border: 1px solid #ccc; box-sizing: border-box;">
                        </asp:TextBox>
                    </div>

                    <div>
                        <asp:Button ID="btnSave" runat="server" Text="Lưu" OnClick="btnSave_Click"
                            style="padding: 10px 20px; background: #8bc34a; color: white; border: none; cursor: pointer; font-weight: bold;" />
                        <asp:Button ID="btnCancel" runat="server" Text="Hủy" OnClick="btnCancel_Click"
                            style="padding: 10px 20px; background: #ccc; color: black; border: none; cursor: pointer; font-weight: bold; margin-left: 10px;" />
                    </div>
                </div>

                <div style="overflow-x: auto;">
                    
                <table class="bang-gio-hang">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Tên sản phẩm</th>
                            <th>Giá tiền</th>
                            <th>Số lượng</th>
                            <th>Hình ảnh</th>
                            <th>Hành động</th>
                        </tr>
                    </thead>
                    <tbody id="tbodyProducts" runat="server">
                    </tbody>
                </table>

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
        <script src="assets/js/main.js"></script>

    </body>

    </html>