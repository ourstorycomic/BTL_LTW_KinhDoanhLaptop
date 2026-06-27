<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TinTuc.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.TinTuc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="TinTuc.css" /> 
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
        <section class="trang-tin-tuc">
    <h2 class="tieu-de-tin-tuc">TIN TỨC CÔNG NGHỆ & KHUYẾN MÃI</h2>
    
    <div class="luoi-tin-tuc">
        <article class="the-tin-tuc">
            <div class="anh-tin-tuc">
                <img src="Anh/danhgia.png" alt="Đánh giá laptop" />
            </div>
            <div class="noi-dung-tin">
                <span class="ngay-dang">15/10/2023</span>
                <h3><a href="Anh/danhgia.png">Đánh giá chi tiết Asus TUF Gaming 2023: Liệu có đáng tiền?</a></h3>
                <p>Khám phá sức mạnh tản nhiệt và hiệu năng thực tế của dòng laptop gaming quốc dân...</p>
                <a href="#" class="btn-doc-tiep">Đọc tiếp ➔</a>
            </div>
        </article>

        <article class="the-tin-tuc">
            <div class="anh-tin-tuc">
                <img src="Anh/meo.png" alt="Mẹo vặt" />
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
                <img src="Anh/sale.png" alt="Khuyến mãi" />
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
    </form>
</body>
</html>
