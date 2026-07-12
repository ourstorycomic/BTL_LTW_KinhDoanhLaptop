<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaoCao.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.BaoCao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title></title>
    <link href="assets/css/Styles.css" rel="stylesheet" />
    <link rel="stylesheet" href="assets/font/fontawesome-free-6.4.0/css/all.css" />
</head>
<body>
    <form id="form1" runat="server" method="post" class="full-height-form">
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

        <main style="max-width: 1200px; width: 100%; margin: 40px auto; padding: 0 15px; flex: 1; box-sizing: border-box;">
            <h1 style="color: #333; font-size: 24px; font-weight: 600; margin-bottom: 30px; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;">Tổng quan hệ thống</h1>
            
            <div style="display: grid; grid-template-columns: repeat(auto-fit, minmax(240px, 1fr)); gap: 20px; margin-bottom: 30px; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;">
                
                <div style="background: white; padding: 20px; border: 1px solid #e2e8f0; border-radius: 10px; box-shadow: 0 1px 3px rgba(0,0,0,0.05); display: flex; align-items: center; justify-content: space-between;">
                    <div>
                        <h3 style="margin: 0; color: #64748b; font-size: 13px; font-weight: 600; text-transform: uppercase; letter-spacing: 0.5px;">Tổng Sản Phẩm</h3>
                        <p style="font-size: 24px; font-weight: 700; color: #0f172a; margin: 8px 0 0 0;"><%= TongSanPham %></p>
                    </div>
                    
                </div>
                
                <div style="background: white; padding: 20px; border: 1px solid #e2e8f0; border-radius: 10px; box-shadow: 0 1px 3px rgba(0,0,0,0.05); display: flex; align-items: center; justify-content: space-between;">
                    <div>
                        <h3 style="margin: 0; color: #64748b; font-size: 13px; font-weight: 600; text-transform: uppercase; letter-spacing: 0.5px;">Tổng Số Lượng Tồn</h3>
                        <p style="font-size: 24px; font-weight: 700; color: #0f172a; margin: 8px 0 0 0;"><%= TongSoLuongTon %></p>
                    </div>
                    
                </div>
                
                <div style="background: white; padding: 20px; border: 1px solid #e2e8f0; border-radius: 10px; box-shadow: 0 1px 3px rgba(0,0,0,0.05); display: flex; align-items: center; justify-content: space-between;">
                    <div>
                        <h3 style="margin: 0; color: #64748b; font-size: 13px; font-weight: 600; text-transform: uppercase; letter-spacing: 0.5px;">Tổng Giá Trị Kho</h3>
                        <p style="font-size: 24px; font-weight: 700; color: #0f172a; margin: 8px 0 0 0;"><%= String.Format("{0:N0} ₫", TongGiaTriKho) %></p>
                    </div>
                    
                </div>
                
                <div style="background: white; padding: 20px; border: 1px solid #e2e8f0; border-radius: 10px; box-shadow: 0 1px 3px rgba(0,0,0,0.05); display: flex; align-items: center; justify-content: space-between;">
                    <div>
                        <h3 style="margin: 0; color: #64748b; font-size: 13px; font-weight: 600; text-transform: uppercase; letter-spacing: 0.5px;">Người Dùng Đăng Ký</h3>
                        <p style="font-size: 24px; font-weight: 700; color: #0f172a; margin: 8px 0 0 0;"><%= TongTaiKhoan %></p>
                    </div>
                    
                </div>
                
                <div style="background: white; padding: 20px; border: 1px solid #e2e8f0; border-radius: 10px; box-shadow: 0 1px 3px rgba(0,0,0,0.05); display: flex; align-items: center; justify-content: space-between; grid-column: 1 / -1;">
                      <div style="display: flex; align-items: center; gap: 20px;">
                          <div>
                            <h3 style="margin: 0; color: #64748b; font-size: 13px; font-weight: 600; text-transform: uppercase; letter-spacing: 0.5px;">Sản Phẩm Bán Chạy Nhất</h3>
                            <p style="font-size: 20px; font-weight: 600; color: #0f172a; margin: 5px 0 0 0;"><%= SanPhamBanChay %></p>
                        </div>
                    </div>
                </div>
            </div>
        
              <div style="margin-top: 40px;">
                  <h2 style="color: #333; font-size: 20px; font-weight: 600; margin-bottom: 20px; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;">Chi Tiết Sản Phẩm Bán Được</h2>
                  <div style="overflow-x: auto;">
                      <table class="bang-gio-hang">
                          <thead>
                              <tr>
                                  <th>Hình ảnh</th>
                                  <th>Tên sản phẩm</th>
                                  <th>Giá bán</th>
                                  <th>Số lượng bán</th>
                                  <th>Doanh thu</th>
                              </tr>
                          </thead>
                          <tbody id="tbodyThongKe" runat="server">
                          </tbody>
                      </table>
                  </div>
              </div>
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
