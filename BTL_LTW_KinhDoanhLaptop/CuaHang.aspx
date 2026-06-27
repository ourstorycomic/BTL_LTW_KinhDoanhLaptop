<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CuaHang.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.CuaHang" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="TrangChu.css" />
    <link rel="stylesheet" type="text/css" href="CuaHang.css" />
</head>
<body>
    <form id="form1" runat="server">
        <header class="phan-dau-trang">
            <div class="noi-dung-phan-dau">
                <a href="TrangChu.aspx" class="chu-logo">MOBILE EDUCATION</a>
                <nav class="menu-ngang">
                    <a href="TrangChu.aspx">Trang chủ</a>
                    <a href="GioiThieu.aspx">Giới thiệu</a>
                    <a href="CuaHang.aspx" style="color: #00ff00;">Cửa hàng</a> <a href="TinTuc.aspx">Tin tức</a>
                    <a href="#">Liên hệ</a>
                </nav>
                <div class="thanh-tim-kiem" style="display: flex; gap: 5px;">
                    <asp:TextBox ID="txtTimKiem" runat="server" placeholder="Tìm kiếm sản phẩm..." CssClass="o-nhap-tim-kiem"></asp:TextBox>
                    <asp:Button ID="btnTimKiem" runat="server" Text="Tìm" CssClass="btn-search" />
                </div>
            </div>
        </header>

        <main class="khung-chinh">
            
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
                    <span>Hiển thị 1-12 trong số 45 sản phẩm</span>
                    <div class="sap-xep">
                        <label>Sắp xếp theo: </label>
                        <div class="sap-xep">
                            <label>Sắp xếp theo: </label>
                            <asp:DropDownList ID="ddlSapXep" runat="server" AutoPostBack="true" OnSelectedIndexChanged="BoLoc_Changed">
                                <asp:ListItem Text="Mới nhất" Value="new"></asp:ListItem>
                                <asp:ListItem Text="Giá: Thấp đến Cao" Value="asc"></asp:ListItem>
                                <asp:ListItem Text="Giá: Cao đến Thấp" Value="desc"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
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
                                        <a href="#" class="btn-mua">Thêm giỏ hàng</a>
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
    </form>
</body>
</html>
