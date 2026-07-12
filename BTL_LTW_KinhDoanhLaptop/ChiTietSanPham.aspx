<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChiTietSanPham.aspx.cs" Inherits="BTL_LTW_KinhDoanhLaptop.ChiTietSanPham" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="assets/css/Styles.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/ChiTietSanPham.css" />
    <link rel="stylesheet" href="assets/font/fontawesome-free-6.4.0/css/all.css" />
    
    <style>
        .bang_thong_so { width: 100%; border-collapse: collapse; margin-top: 15px; }
        .bang_thong_so th, .bang_thong_so td { padding: 10px; border: 1px solid #ddd; vertical-align: middle; }
        .bang_thong_so tr.table-light td { background-color: #f8f9fa; font-weight: bold; font-size: 16px; }
        
        .ten_sp_mo_ta, .tieu_de_nho { color: #0d6efd; font-weight: 600; margin-top: 15px; text-align: center; }
        .phan_anh_mo_ta { text-align: center; margin: 20px 0; }
        .phan_anh_mo_ta img { max-width: 80%; height: auto; border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.1); }
        .khung_mo_ta { border: 1px solid #ddd; padding: 20px; border-radius: 8px; background: #fff; }
        
        .khung_nhap_binh_luan { display: flex; gap: 15px; background: #f9f9f9; padding: 20px; border-radius: 8px; border: 1px solid #e0e0e0; }
        .anh_dai_dien img { border-radius: 50%; object-fit: cover; }
        .form_nhap_lieu { flex: 1; }
        
        .danh_sach_sp { display: flex; gap: 20px; flex-wrap: wrap; margin-top: 20px; }
        .the_san_pham { width: calc(25% - 15px); background: #fff; padding: 15px; border: 1px solid #e0e0e0; border-radius: 8px; transition: transform 0.3s ease, box-shadow 0.3s ease; }
        .the_san_pham:hover { transform: translateY(-5px); box-shadow: 0 10px 20px rgba(0,0,0,0.12); }
        .anh_san_pham { width: 100%; height: 180px; object-fit: contain; }
        .ten_san_pham { font-size: 14px; margin-bottom: 10px; font-weight: 600; color: #333; display: -webkit-box; -webkit-line-clamp: 2; -webkit-box-orient: vertical; overflow: hidden; }
        .ten_san_pham:hover { color: #0d6efd; }
        .cau_hinh_tom_tat { list-style: none; padding: 10px; background: #f4f4f4; margin-top: 10px; font-size: 12px; border-radius: 4px; }
        .cau_hinh_tom_tat li { margin-bottom: 5px; border-bottom: 1px dashed #ddd; padding-bottom: 3px; }
        .cau_hinh_tom_tat li:last-child { border-bottom: none; }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="display: flex; flex-direction: column; flex: 1; margin: 0; padding: 0;">
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

        <div class="container main-container">
            <% if (spHienTai != null) { %>
                <div style="color: blue; font-size: 14px; margin-bottom: 20px;">
                    <a href="TrangChu.aspx" style="color: blue; text-decoration: none;">Trang chủ</a> / Laptop / <%= spHienTai.TenSanPham %>
                </div>

                <div class="chi-tiet-layout" style="display: flex; gap: 30px;">
                    <div class="cot-trai flex-1">
                        <img src="<%= ResolveUrl(spHienTai.HinhAnh) %>" class="anh-lon" alt="<%= spHienTai.TenSanPham %>" style="width: 100%; object-fit: contain;" />
                    </div>

                    <div class="cot-phai flex-1">
                        <div class="ten-sp product-title-lg"><%= spHienTai.TenSanPham %></div>
                        <div class="product-meta-info">Mã SP: <b><%= spHienTai.Id %></b> | Đánh giá: ⭐⭐⭐⭐⭐</div>
                        
                        <div class="gia-sp product-price-lg"><%= String.Format("{0:N0} đ", spHienTai.GiaTien) %></div>

                        <div class="khung-thong-so product-specs-box">
                            <h3 class="mb-10">Thông số sản phẩm</h3>
                            <ul class="specs-list">
                                <li>- <b>CPU:</b> Intel Core i5 120U</li>
                                <li>- <b>RAM:</b> 16GB DDR4</li>
                                <li>- <b>Ổ cứng:</b> 512GB SSD PCIe NVMe</li>
                                <li>- <b>Màn hình:</b> 15.6 inch FHD IPS</li>
                            </ul>
                        </div>

                        <div class="flex-gap-15">
                            <asp:Button ID="btnMuaNgay" runat="server" Text="MUA NGAY" class="btn-buy-now" OnClick="btnMuaNgay_Click" />
                            <asp:Button ID="btnThemVaoGio" runat="server" Text="THÊM VÀO GIỎ HÀNG" class="btn-add-cart" OnClick="btnThemVaoGio_Click" />
                        </div>
                    </div>
                </div>

                <div class="mo-ta-sp product-desc-section">
                    <h2 class="mb-15">Đánh giá chi tiết</h2>
                    <p class="line-height-16">Chiếc laptop <%= spHienTai.TenSanPham %> là một sản phẩm văn phòng và học tập "nhẹ ví, nhẹ balo" dành cho những ai cần một người bạn đồng hành đáng tin cậy. Thiết kế gọn nhẹ và mức giá thân thiện, hiệu suất đỉnh cao giúp bạn có những trải nghiệm tốt nhất!</p>
                </div>

                <div class="khung_thong_so" style="margin-top: 40px;">
                        <h2 style="border-bottom: 2px solid #ddd; padding-bottom: 10px;">Thông số kỹ thuật chi tiết</h2>
                        <table class="bang_thong_so" id="bang_thong_so_chi_tiet">
                            <tbody>
                                <tr class="table-light"><td colspan="2">Bộ vi xử lý</td></tr>
                                <tr><td style="width: 30%;">Công nghệ CPU</td><td>AMD Ryzen™ 7 7735HS</td></tr>
            
                                <tr class="dong_an_di"><td>Số nhân</td><td>8</td></tr>
                                <tr class="dong_an_di"><td>Số luồng</td><td>16</td></tr>
                                <tr class="dong_an_di"><td>Tốc độ tối đa</td><td>upto 4.75GHz</td></tr>
                                <tr class="dong_an_di"><td>Bộ nhớ đệm</td><td>4MB L2 / 16MB L3</td></tr>

                                <tr class="table-light"><td colspan="2">Bộ nhớ trong (RAM)</td></tr>
                                <tr><td>RAM</td><td>16GB SO-DIMM</td></tr>
                                <tr class="dong_an_di"><td>Loại RAM</td><td>DDR5</td></tr>
                                <tr class="dong_an_di"><td>Tốc độ Bus RAM</td><td>4800</td></tr>
                                <tr class="dong_an_di"><td>Số khe cắm</td><td>Two DDR5 SO-DIMM slots, dual-channel capable</td></tr>
                                <tr class="dong_an_di"><td>Hỗ trợ RAM tối đa</td><td>Up to 16GB DDR5-4800 offering</td></tr>

                                <tr class="table-light"><td colspan="2">Ổ cứng</td></tr>
                                <tr><td>Dung lượng</td><td>512GB SSD M.2 2242 PCIe® 4.0x4 NVMe®</td></tr>
                                <tr class="dong_an_di"><td>Storage Support</td><td>Up to two drives, 2x M.2 SSD<br>• M.2 SSD up to 1TB each</td></tr>
                                <tr class="dong_an_di"><td>Storage Slot</td><td>Two M.2 2242 PCIe® 4.0 x4 slots</td></tr>

                                <tr class="table-light"><td colspan="2">Màn hình</td></tr>
                                <tr><td>Kích thước màn hình</td><td>15.6 inch</td></tr>
                                <tr><td>Độ phân giải</td><td>FHD (1920x1080)</td></tr>
                                <tr class="dong_an_di"><td>Tần số quét</td><td>144Hz</td></tr>
                                <tr class="dong_an_di"><td>Công nghệ màn hình</td><td>IPS 300nits Anti-glare, 100% sRGB, 144Hz, FreeSync™</td></tr>

                                <tr class="table-light"><td colspan="2">Đồ Họa (VGA)</td></tr>
                                <tr><td>Card màn hình</td><td>NVIDIA® GeForce RTX™ 4050 6GB GDDR6, Boost Clock 1605MHz, TGP 65W, 194 AI TOPS</td></tr>

                                <tr class="table-light dong_an_di"><td colspan="2">Kết nối (Network)</td></tr>
                                <tr class="dong_an_di"><td>WLAN</td><td>Wi-Fi® 6, 802.11ax 2x2</td></tr>
                                <tr class="dong_an_di"><td>Ethernet</td><td>100/1000M (RJ-45)</td></tr>
                                <tr class="dong_an_di"><td>Bluetooth</td><td>BT5.2</td></tr>

                                <tr class="table-light dong_an_di"><td colspan="2">Bàn phím, Chuột</td></tr>
                                <tr class="dong_an_di"><td>Kiểu bàn phím</td><td>White Backlit, English</td></tr>
                                <tr class="dong_an_di"><td>Chuột</td><td>Buttonless Mylar® surface multi-touch touchpad, supports Precision TouchPad (PTP)</td></tr>

                                <tr class="table-light dong_an_di"><td colspan="2">Giao tiếp mở rộng</td></tr>
                                <tr class="dong_an_di">
                                    <td>Kết nối USB</td>
                                    <td>
                                        • 2x USB-A (USB 5Gbps / USB 3.2 Gen 1)<br>
                                        • 1x USB-C® (USB 5Gbps / USB 3.2 Gen 1), with PD 3.0<br>
                                        • 1x HDMI® 2.1, up to 8K/60Hz<br>
                                        • 1x Headphone / microphone combo jack (3.5mm)<br>
                                        • 1x Ethernet (RJ-45)<br>
                                        • 1x Power connector
                                    </td>
                                </tr>
                                <tr class="dong_an_di"><td>Camera</td><td>HD 720p with Privacy Shutter</td></tr>

                                <tr class="table-light dong_an_di"><td colspan="2">Thông tin khác</td></tr>
                                <tr class="dong_an_di"><td>LOA</td><td>2 Loa</td></tr>
                                <tr class="dong_an_di"><td>Kiểu Pin</td><td>57.5Wh</td></tr>
                                <tr class="dong_an_di"><td>Sạc pin</td><td>Đi kèm</td></tr>
                                <tr class="dong_an_di"><td>Hệ điều hành</td><td>Windows 11 Home Single Language, English</td></tr>
                                <tr class="dong_an_di"><td>Kích thước</td><td>359.2 x 236 x 19.9-22.95 mm</td></tr>
                                <tr class="dong_an_di"><td>Trọng lượng</td><td>1.8 kg</td></tr>
                                <tr class="dong_an_di"><td>Màu sắc</td><td>Luna Grey</td></tr>
                                <tr class="dong_an_di"><td>Chất liệu</td><td>PC-ABS (Top), PC-ABS (Bottom)</td></tr>
                                <tr class="dong_an_di"><td>Xuất xứ</td><td>Trung Quốc</td></tr>
                            </tbody>
                        </table>
    
                        <div class="khung_nut_xem_them">
                            <button type="button" id="btn_hien_thong_so" class="nut_xem_them" onclick="batTatThongSo()">
                                Xem thêm cấu hình chi tiết <i class="fa-solid fa-chevron-down"></i>
                            </button>
                        </div>
                    </div>

                <div class="phan_mo_ta_san_pham" style="margin-top: 40px; border: 1px solid #ddd; padding: 25px; border-radius: 8px; background: #fff;">
    
                    <div id="noi_dung_bai_viet_mo_ta" class="khung_chua_mo_ta">
        
                        <h3 class="tieu_de_xanh">1. Thông Số Chi Tiết và Cấu Hình Tối Ưu</h3>
                        <table class="bang_mo_ta_chi_tiet">
                            <thead>
                                <tr>
                                    <th>Thông số</th>
                                    <th>Chi tiết</th>
                                    <th>Lợi ích Cốt lõi</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>CPU (Vi xử lý)</td>
                                    <td><strong>AMD Ryzen 7 7735HS</strong> (8 nhân/16 luồng, Max 4.75 GHz)</td>
                                    <td><strong>Hiệu suất xử lý mạnh mẽ,</strong> đảm bảo FPS ổn định cho gaming và đa nhiệm.</td>
                                </tr>
                                <tr>
                                    <td>VGA (Đồ họa)</td>
                                    <td><strong>RTX™ 4050 6GB GDDR6</strong> (TGP 65W, AI TOPS: 194)</td>
                                    <td><strong>Đồ họa thế hệ mới,</strong> VRAM 6GB tối ưu gaming FHD, hỗ trợ DLSS 3 và hiệu suất AI.</td>
                                </tr>
                                <tr>
                                    <td>Màn hình</td>
                                    <td><strong>15.6" FHD IPS 144Hz,</strong> 100% sRGB, FreeSync</td>
                                    <td><strong>Tốc độ quét siêu nhanh 144Hz,</strong> màu sắc chuẩn xác (100% sRGB), loại bỏ xé hình (FreeSync).</td>
                                </tr>
                                <tr>
                                    <td>RAM</td>
                                    <td><strong>16GB DDR5-4800</strong> (1 khe, 2 khe tổng cộng)</td>
                                    <td>Tốc độ bộ nhớ nhanh, có khả năng <strong>nâng cấp linh hoạt</strong> (thường tối đa 32GB).</td>
                                </tr>
                                <tr>
                                    <td>Ổ cứng</td>
                                    <td><strong>512GB SSD PCIe® 4.0x4 NVMe®</strong></td>
                                    <td>Tốc độ tải game và khởi động máy tức thì, chuẩn SSD Gen 4 tiên tiến.</td>
                                </tr>
                                <tr>
                                    <td>Cân nặng</td>
                                    <td><strong>1.8 kg</strong></td>
                                    <td>Thiết kế <strong>siêu mỏng nhẹ,</strong> lý tưởng cho laptop gaming 15.6 inch.</td>
                                </tr>
                            </tbody>
                        </table>

                        <h3 class="tieu_de_xanh">2. Đánh Giá Trải Nghiệm Gaming & Di Động</h3>
                        <div class="muc_danh_gia">
                            <h4>2.1. Hiệu Năng Thế Hệ Mới và Sức Mạnh RTX 4050</h4>
                            <ul class="danh_sach_danh_gia">
                                <li><strong>Chip Ryzen 7 7735HS:</strong> Vi xử lý AMD dòng HS (High Standard) với 8 nhân và 16 luồng cung cấp hiệu suất đa nhiệm mạnh mẽ, đảm bảo máy xử lý mượt mà mọi tựa game AAA và các tác vụ sáng tạo cơ bản.</li>
                                <li><strong>VGA RTX 4050 6GB (Thế hệ mới):</strong> Card đồ họa <strong>RTX 4050</strong> là lựa chọn tuyệt vời trong phân khúc gaming. Với <strong>DLSS 3</strong> và công nghệ <strong>Frame Generation</strong>, bạn có thể tăng FPS lên đáng kể, trải nghiệm game mượt mà hơn ở độ phân giải FHD 144Hz. Hiệu suất <strong>AI TOPS (194)</strong> hỗ trợ tối ưu hóa hiệu năng gaming và các tác vụ AI.</li>
                            </ul>

                            <h4>2.2. Màn Hình Chuẩn Màu 144Hz & Thiết Kế Siêu Nhẹ</h4>
                            <ul class="danh_sach_danh_gia">
                                <li><strong>Màn Hình 144Hz Chuẩn Màu:</strong> Tần số quét <strong>144Hz</strong> giúp game thủ phản ứng nhanh hơn và loại bỏ hiện tượng xé hình nhờ công nghệ <strong>FreeSync.</strong> Độ phủ màu <strong>100% sRGB</strong> là điểm cộng lớn, giúp chiếc LOQ này không chỉ mạnh mẽ trong game mà còn lý tưởng cho các công việc chỉnh sửa ảnh, video cơ bản.</li>
                                <li><strong>Siêu Nhẹ 1.8 kg:</strong> Với cân nặng chỉ <strong>1.8 kg</strong>, Lenovo LOQ 15ARP10E là một trong những chiếc laptop gaming 15.6 inch nhẹ nhất. Điều này giúp giảm đáng kể gánh nặng khi mang theo di chuyển hàng ngày.</li>
                                <li><strong>RAM DDR5 và Khả năng Nâng cấp:</strong> <strong>16GB RAM DDR5</strong> đảm bảo hiệu suất nhanh chóng. Thiết kế 1 khe trống (hoặc 2 khe có khả năng nâng cấp) cho phép người dùng dễ dàng mở rộng RAM khi cần.</li>
                            </ul>
                        </div>

                        <h3 class="tieu_de_xanh">3. Lý Do Nên Mua Chiếc Laptop Này?</h3>
                        <p style="margin-bottom: 15px;">Lenovo LOQ 15ARP10E là lựa chọn hoàn hảo nếu bạn:</p>
                        <ul class="danh_sach_danh_gia">
                            <li><strong>Là game thủ</strong> tìm kiếm hiệu suất <strong>RTX 4050</strong> thế hệ mới và công nghệ <strong>DLSS 3</strong> ở mức giá tối ưu.</li>
                            <li><strong>Ưu tiên màn hình 144Hz</strong> với <strong>100% sRGB</strong> để kết hợp gaming và sáng tạo.</li>
                            <li><strong>Cần một chiếc laptop gaming</strong> có thiết kế <strong>siêu mỏng nhẹ</strong> (1.8 kg) và cấu hình DDR5 hiện đại.</li>
                        </ul>

                        <div id="lop_phu_mo_ta" class="lop_phu_mo_ta"></div>
                    </div>

                    <div class="khung_nut_thu_gon">
                        <button type="button" id="btn_thu_gon_mo_ta" class="nut_thu_gon_btn" onclick="batTatBaiVietMoTa()">
                            XEM THÊM <i class="fa-solid fa-angles-down"></i>
                        </button>
                    </div>
                </div>

                <div class="phan_binh_luan" style="margin-top: 40px;">
                    <h2 style="margin-bottom: 15px;">Bình luận</h2>
                    <div class="khung_nhap_binh_luan">
                        <div class="anh_dai_dien">
                            <img src="assets/img/dell.png" alt="Avatar" width="50" height="50" />
                        </div>
                        <div class="form_nhap_lieu">
                            <textarea id="txt_noi_dung_binh_luan" rows="3" placeholder="Viết bình luận của bạn vào đây..." style="width: 100%; padding: 10px; border: 1px solid #ccc; border-radius: 4px; resize: vertical;"></textarea>
                            <div style="text-align: right; margin-top: 10px;">
                                <button type="button" style="padding: 8px 20px; background-color: #0d6efd; color: white; border: none; border-radius: 4px; cursor: pointer; font-weight: bold;">Gửi bình luận</button>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="phan_san_pham_tuong_tu" style="margin-top: 40px; margin-bottom: 50px;">
                    <h2 style="border-bottom: 2px solid #0d6efd; display: inline-block; padding-bottom: 5px; color: #0d6efd;">SẢN PHẨM TƯƠNG TỰ</h2>
                    <div class="danh_sach_sp">
                        <div class="the_san_pham">
                            <img src="assets/img/lenovo.png" class="anh_san_pham" alt="Laptop LENOVO LOQ">
                            <div class="thong_tin_san_pham">
                                <h4 class="ten_san_pham">Laptop LENOVO LOQ 15ARP10E 83S0004FVN</h4>
                                <div style="color: red; font-weight: bold; font-size: 16px;">30.990.000 đ</div>
                                <div style="text-decoration: line-through; color: #888; font-size: 12px;">36.990.000 đ</div>
            
                                <ul class="cau_hinh_tom_tat">
                                    <li><i class="fa-solid fa-microchip"></i> Ryzen 7</li>
                                    <li><i class="fa-solid fa-memory"></i> 16GB</li>
                                    <li><i class="fa-solid fa-hard-drive"></i> 512GB</li>
                                </ul>
            
                                <div class="khung_nut_hanh_dong">
                                    <a href="ChiTietSanPham.aspx?id=1" class="nut_chi_tiet">Chi tiết</a>
                                    <a href="ThanhToan.aspx?action=add&id=1" class="nut_gio_hang">Giỏ hàng</a>
                                </div>
                            </div> </div>

                        <div class="the_san_pham">
                            <img src="assets/img/dell2.png" class="anh_san_pham" alt="Laptop LENOVO LOQ">
                            <div class="thong_tin_san_pham">
                                <h4 class="ten_san_pham">Laptop LENOVO LOQ 15IAX9E 83LK0079VN</h4>
                                <div style="color: red; font-weight: bold; font-size: 16px;">23.990.000 đ</div>
                                <div style="text-decoration: line-through; color: #888; font-size: 12px;">24.990.000 đ</div>
            
                                <ul class="cau_hinh_tom_tat">
                                    <li><i class="fa-solid fa-microchip"></i> Core i5</li>
                                    <li><i class="fa-solid fa-memory"></i> 16GB</li>
                                    <li><i class="fa-solid fa-hard-drive"></i> 512GB</li>
                                </ul>
            
                                <div class="khung_nut_hanh_dong">
                                    <a href="ChiTietSanPham.aspx?id=2" class="nut_chi_tiet">Chi tiết</a>
                                    <a href="ThanhToan.aspx?action=add&id=2" class="nut_gio_hang">Giỏ hàng</a>
                                </div>
                            </div>
                        </div>

                        <div class="the_san_pham">
                            <img src="assets/img/dell2.png" class="anh_san_pham" alt="Laptop LENOVO LOQ">
                            <div class="thong_tin_san_pham">
                                <h4 class="ten_san_pham">Laptop LENOVO LOQ 15IAX9E 83LK0079VN</h4>
                                <div style="color: red; font-weight: bold; font-size: 16px;">23.990.000 đ</div>
                                <div style="text-decoration: line-through; color: #888; font-size: 12px;">24.990.000 đ</div>
            
                                <ul class="cau_hinh_tom_tat">
                                    <li><i class="fa-solid fa-microchip"></i> Core i5</li>
                                    <li><i class="fa-solid fa-memory"></i> 16GB</li>
                                    <li><i class="fa-solid fa-hard-drive"></i> 512GB</li>
                                </ul>
            
                                <div class="khung_nut_hanh_dong">
                                    <a href="ChiTietSanPham.aspx?id=3" class="nut_chi_tiet">Chi tiết</a>
                                    <a href="ThanhToan.aspx?action=add&id=3" class="nut_gio_hang">Giỏ hàng</a>
                                </div>
                            </div>
                        </div>

                        <div class="the_san_pham">
                            <img src="assets/img/dell2.png" class="anh_san_pham" alt="Laptop LENOVO LOQ">
                            <div class="thong_tin_san_pham">
                                <h4 class="ten_san_pham">Laptop LENOVO LOQ 15IAX9E 83LK0079VN</h4>
                                <div style="color: red; font-weight: bold; font-size: 16px;">23.990.000 đ</div>
                                <div style="text-decoration: line-through; color: #888; font-size: 12px;">24.990.000 đ</div>
            
                                <ul class="cau_hinh_tom_tat">
                                    <li><i class="fa-solid fa-microchip"></i> Core i5</li>
                                    <li><i class="fa-solid fa-memory"></i> 16GB</li>
                                    <li><i class="fa-solid fa-hard-drive"></i> 512GB</li>
                                </ul>
            
                                <div class="khung_nut_hanh_dong">
                                    <a href="ChiTietSanPham.aspx?id=4" class="nut_chi_tiet">Chi tiết</a>
                                    <a href="ThanhToan.aspx?action=add&id=4" class="nut_gio_hang">Giỏ hàng</a>
                                </div>
                            </div>
                        </div>
                    </div>
                <% } else { %>
                <h2 class="not-found-msg">Không tìm thấy sản phẩm hoặc sản phẩm không tồn tại!</h2>
                <div class="mt-20-center">
                    <a href="TrangChu.aspx" class="btn-back-home">Quay lại trang chủ</a>
                </div>
            <% } %>
        </div>

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
    <script>
    function batTatThongSo() {
        var cacDongAn = document.querySelectorAll('.dong_an_di');
        var nutBam = document.getElementById('btn_hien_thong_so');
        var dangAn = window.getComputedStyle(cacDongAn[0]).display === 'none';
        if (dangAn) {
            cacDongAn.forEach(function(dong) {
                dong.style.display = 'table-row';
            });
            nutBam.innerHTML = 'Thu gọn <i class="fa-solid fa-chevron-up"></i>';
        } else {
            cacDongAn.forEach(function(dong) {
                dong.style.display = 'none';
            });
            nutBam.innerHTML = 'Xem thêm cấu hình chi tiết <i class="fa-solid fa-chevron-down"></i>';
        }
    }
        function batTatBaiVietMoTa() {
            var khungNoiDung = document.getElementById('noi_dung_bai_viet_mo_ta');
            var lopPhu = document.getElementById('lop_phu_mo_ta');
            var nutBam = document.getElementById('btn_thu_gon_mo_ta');
            if (khungNoiDung.classList.contains('mo_rong_mo_ta')) {
                khungNoiDung.classList.remove('mo_rong_mo_ta');
                lopPhu.style.display = 'block';
                nutBam.innerHTML = 'XEM THÊM <i class="fa-solid fa-angles-down"></i>';
            } else {
                khungNoiDung.classList.add('mo_rong_mo_ta');
                lopPhu.style.display = 'none';
                nutBam.innerHTML = 'THU GỌN <i class="fa-solid fa-angles-up"></i>';
            }
        }
</script>

</body>
</html>