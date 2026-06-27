<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GioiThieu.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.GioiThieu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="GioiThieu.css" />
    <link rel="stylesheet" type="text/css" href="TrangChu.css" />
</head>
<body>
    <form id="form1" runat="server">
                <header class="phan-dau-trang">
    <div class="noi-dung-phan-dau">
        <a href="#" class="chu-logo">
            MOBILE EDUCATION
        </a>
        
        <nav class="menu-ngang">
            <a href="TrangChu.aspx">Trang chủ</a>
            <a href="GioiThieu.aspx">Giới thiệu</a>
            <a href="CuaHang.aspx">Cửa hàng</a>
            <a href="TinTuc.aspx">Tin tức</a>
            <a href="#">Liên hệ</a>
        </nav>

        <div class="thanh-tim-kiem">
            <asp:TextBox ID="txtTimKiem" runat="server" placeholder="Tìm kiếm sản phẩm..." CssClass="o-nhap-tim-kiem"></asp:TextBox>
    
            <asp:Button ID="btnTimKiem" runat="server" Text="Tìm" OnClick="btnTimKiem_Click" CssClass="btn-search" />
        </div>

    </div>
</header>
        <section class="banner-gioi-thieu">
    <div class="noi-dung-banner">
        <h1>ĐỘI NGŨ MOBILE EDUCATION</h1>
        <p>CAM KẾT ĐEM LẠI TRẢI NGHIỆM CÔNG NGHỆ HOÀN HẢO</p>
        <span class="icon-ten-lua">🚀</span> 
    </div>
</section>

<section class="su-menh-section">
    <div class="cot-trai-hinh-anh">
        <img src="Anh/Sumenh.png" alt="Sứ mệnh Mobile Education" />
    </div>
    <div class="cot-phai-noi-dung">
        <h2>GIẢI PHÁP CÔNG NGHỆ TOÀN DIỆN CHO BẠN</h2>
        <p>MOBILE EDUCATION không chỉ bán laptop mà còn cung cấp cho bạn những giải pháp học tập và làm việc tối ưu nhất.</p>
        <p>Trong bối cảnh công nghệ phát triển như hiện nay, việc sở hữu một chiếc máy tính phù hợp là chìa khóa để thành công. Dù bạn là sinh viên, nhân viên văn phòng hay game thủ, chúng tôi luôn có thiết bị đáp ứng đúng nhu cầu của bạn.</p>
        <p>Chúng tôi tự hào phân phối các dòng sản phẩm chính hãng từ ASUS, Dell, Lenovo, Macbook... với chế độ hậu mãi chuyên nghiệp.</p>
    </div>
</section>

<section class="cam-ket-section">
    <h2 class="tieu-de-do">MOBILE EDUCATION - ĐỐI TÁC ĐÁNG TIN CẬY</h2>
    <div class="khung-chua-cam-ket">
        <div class="cam-ket-trai">
            <div class="hop-cam-ket">Sản phẩm 100% chính hãng, đầy đủ giấy tờ, nguyên seal từ nhà sản xuất.</div>
            <div class="hop-cam-ket">Mức giá cạnh tranh nhất thị trường cùng nhiều chương trình khuyến mãi cho HSSV.</div>
        </div>
        
        <div class="cam-ket-giua">
            <img src="Anh/hoptac.png" alt="Hợp tác tin cậy" />
        </div>

        <div class="cam-ket-phai">
            <div class="hop-cam-ket">Chính sách bảo hành tận tâm, hỗ trợ cài đặt phần mềm miễn phí trọn đời.</div>
            <div class="hop-cam-ket">Giao hàng tốc hành toàn quốc, kiểm tra hàng trước khi thanh toán.</div>
        </div>
    </div>
</section>
    </form>
</body>
</html>
