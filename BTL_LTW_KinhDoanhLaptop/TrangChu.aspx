<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrangChu.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.TrangChu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="TrangChu.css" />   
</head>
<body>
    <form id="form1" runat="server" method="post">
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

        <main class="khung-chinh">
            
            <aside class="menu-trai">
                <div class="tieu-de-menu">
                   LAPTOP TIÊU BIỂU
                </div>
                <ul class="danh-sach-menu">
                    <li><a href="#">▸ ASUS</a></li>
                    <li><a href="#">▸ Lenovo</a></li>
                    <li><a href="#">▸ Dell</a></li>
                    <li><a href="#">▸ Macbook</a></li>
                    <li><a href="#">▸ Acer</a></li>
                    <li><a href="#">▸ Phụ kiện</a></li>
                </ul>
            </aside>

            <section class="noi-dung-phai">
                
                <div class="banner-quang-cao">
                <button type="button" class="nut-chuyen prev" onclick="chuyenSlide(-1)">❮</button>
    
                <img id="imgBanner" src="Anh/Anhnen1.png" alt="Banner Laptop" />
    
                <button type="button" class="nut-chuyen next" onclick="chuyenSlide(1)">❯</button>
                </div>

                <div class="khung-chua-cac-san-pham">
                    <asp:Repeater ID="rptLaptops" runat="server">
    <ItemTemplate>
        <div class="mot-san-pham">
            <img src='<%# ResolveUrl(Eval("HinhAnh").ToString()) %>' alt='<%# Eval("TenSanPham") %>' class="hinh-anh-san-pham" />
            
            <div class="loai-san-pham">LAPTOP CHÍNH HÃNG</div>
            <div class="ten-san-pham"><%# Eval("TenSanPham") %></div>
            
            <div class="danh-gia">
                <span class="sao-vang">★★★★★</span> <span class="so-danh-gia">(12)</span>
            </div>
            
            <div class="gia-tien"><%# String.Format("{0:N0} ₫", Eval("GiaTien")) %></div>
            
            <div class="hanh-dong-san-pham">
                <a href="ChiTietSanPham.aspx?id=<%# Eval("Id") %>" class="btn-chi-tiet">Chi tiết</a>
                <a href="#" class="btn-gio-hang">Giỏ hàng</a>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
                </div>

            </section>
        </main>
    </form>
    <script type="text/javascript">
        var vitrihientai = 1;
        function chuyenSlide(buoc){
            vitrihientai += buoc;
            if (vitrihientai < 1) {
                vitrihientai = 3;
            }
            if (vitrihientai > 3) {
                vitrihientai = 1;
            }
            var anh = document.getElementById("imgBanner");
            if (vitrihientai == 1) {
                anh.src = "Anh/Anhnen1.png"
            }
            else if (vitrihientai == 2) {
                anh.src = "Anh/Anhnen2.png"
            }
            else if (vitrihientai == 3) {
                anh.src = "Anh/Anhnen3.png"
            }
        }
    </script>
</body>
</html>
