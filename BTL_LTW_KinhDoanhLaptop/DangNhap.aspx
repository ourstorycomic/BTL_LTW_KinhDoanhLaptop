<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DangNhap.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.DangNhap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng Nhập - Mobile Education</title>
    <link rel="stylesheet" type="text/css" href="TrangChu.css" />
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
                        <a href="DangNhap.aspx" title="Đăng nhập / Đăng ký">
                            <img src="Anh/taikhoan.png" alt="Tài khoản" class="icon-tai-khoan" />
                        </a>
                    </div>
                </div>
            </div>
        </header>

        <main class="vung-dang-nhap">
            <div class="khung-dang-nhap">
                <h2 class="tieu-de-dang-nhap">ĐĂNG NHẬP</h2>

                <div class="nhom-nhap-lieu floating-group">
                    <asp:TextBox ID="txtTaiKhoan" runat="server" CssClass="o-nhap-lieu floating-input" Placeholder=" "></asp:TextBox>
                    <label class="floating-label">Tên đăng nhập hoặc Email</label>
                </div>
               
                <div class="nhom-nhap-lieu floating-group">
                    <asp:TextBox ID="txtMatKhau" runat="server" CssClass="o-nhap-lieu floating-input" TextMode="Password" Placeholder=" "></asp:TextBox>
                    <label class="floating-label">Mật khẩu</label>
                </div>

                <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Size="13px"></asp:Label>

                <asp:Label ID="lblThongBaoLoi" runat="server" ForeColor="Red" Font-Size="13px"></asp:Label>

                <asp:Button ID="btnDangNhap" runat="server" Text="ĐĂNG NHẬP" CssClass="nut-dang-nhap" OnClick="btnDangNhap_Click" />

                <div class="link-ho-tro">
                    <a href="#">Quên mật khẩu?</a>
                    <a href="DangKy.aspx">Chưa có tài khoản? Đăng ký ngay</a>
                </div>
            </div>
        </main>

    </form>
</body>
</html>