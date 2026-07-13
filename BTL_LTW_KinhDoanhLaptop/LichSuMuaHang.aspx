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
        <div id="vung-chua-noi-dung">
            <div class="menu-trai-newbie">
                <div class="tieu-de-menu">Quản lý tài khoản</div>
                <ul class="danh-sach-newbie">
                    <li><a href="HoSo.aspx"><i class="fa-solid fa-user"></i> Hồ sơ cá nhân</a></li>
                     <li><a href="TrangChu.aspx"><i class="fa-solid fa-user"></i> Trang Chủ</a></li>
                    <li><a href="LichSuMuaHang.aspx" style="color: green; background-color: #333;"><i class="fa-solid fa-box"></i> Lịch sử mua hàng</a></li>
                    <li><a href="DoiMatKhau.aspx"><i class="fa-solid fa-lock"></i> Đổi mật khẩu</a></li>
                    <li><a href="DangNhap.aspx?logout=true"><i class="fa-solid fa-right-from-bracket"></i> Đăng xuất</a></li>
                </ul>
            </div>

            <div class="noi-dung-phai-newbie">
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

                <div id="khungChuaDonHang" runat="server">
                </div>

                <div class="clear-float"></div>
            </div>

            <div class="clear-float"></div>
        </div> 
    </form>
</body>
</html>