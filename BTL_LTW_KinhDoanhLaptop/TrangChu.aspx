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
                    <a href="#">Trang chủ</a>
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

        <main class="khung-chinh">
            
            <aside class="menu-trai">
                <div class="tieu-de-menu">
                    IMAC & MAC MINI
                </div>
                <ul class="danh-sach-menu">
                    <li><a href="#">▸ Macbook Pro Retina</a></li>
                    <li><a href="#">▸ Macbook Air</a></li>
                    <li><a href="#">▸ Macbook 12 inch</a></li>
                    <li><a href="#">▸ Macbook Pro Touch</a></li>
                    <li><a href="#">▸ Phụ kiện</a></li>
                </ul>
            </aside>

            <section class="noi-dung-phai">
                
                <div class="banner-quang-cao">
                    <img src="Anh/banner.jpg" alt="Banner Khuyến Mãi" onerror="this.style.display='none'" />
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
                <a href="#" class="btn-chi-tiet">Chi tiết</a>
                <a href="#" class="btn-gio-hang">Giỏ hàng</a>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
                </div>
                

            </section>
        </main>
    </form>
</body>
</html>
