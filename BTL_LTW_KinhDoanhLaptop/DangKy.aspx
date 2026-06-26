<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DangKy.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.DangKy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng Ký - Mobile Education</title>
    <link rel="stylesheet" type="text/css" href="TrangChu.css" />
</head>
<body>
    <form id="form1" runat="server">
        
        <header class="phan-dau-trang">
            <div class="noi-dung-phan-dau">
                <a href="TrangChu.aspx" class="chu-logo">MOBILE EDUCATION</a>
                <nav class="menu-ngang">
                    <a href="TrangChu.aspx">Trang chủ</a>
                    <a href="#">Giới thiệu</a>
                    <a href="#">Cửa hàng</a>
                    <a href="#">Tin tức</a>
                    <a href="#">Liên hệ</a>
                </nav>
                <div class="cum-phai-header">
                    <div class="thanh-tim-kiem">
                        <input type="text" placeholder="Tìm kiếm sản phẩm..." />
                    </div>
                    <div class="khu-vuc-tai-khoan">
                        <a href="DangNhap.aspx">
                            <img src="Anh/taikhoan.png" alt="Tài khoản" class="icon-tai-khoan" />
                        </a>
                    </div>
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

    </form>
</body>
</html>