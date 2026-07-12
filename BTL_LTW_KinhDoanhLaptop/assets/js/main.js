function showToast(msg) {
    var thongBao = document.getElementById("toast");
    if (thongBao != null) {
        thongBao.innerText = msg;
        thongBao.className = "show";
        setTimeout(function () {
            thongBao.className = thongBao.className.replace("show", "");
        }, 3000);
    }
}

function showConfirmPopup(url) {
    var hopThoai = document.getElementById("confirmModal");

    if (hopThoai == null) {
        hopThoai = document.createElement("div");
        hopThoai.id = "confirmModal";
        hopThoai.className = "modal";
        var chuoiHtml = "";
        chuoiHtml += "<div class='modal-content'>";
        chuoiHtml += "<h3 style='margin-bottom: 15px; color: #333;'>Xác nhận xóa</h3>";
        chuoiHtml += "<p>Bạn có chắc chắn muốn xóa sản phẩm này khỏi giỏ hàng?</p>";
        chuoiHtml += "<div class='modal-buttons'>";
        chuoiHtml += "<button class='btn-no' onclick='closeConfirmPopup()'>Hủy</button> ";
        chuoiHtml += "<button id='btnConfirmYes' class='btn-yes'>Xóa</button>";
        chuoiHtml += "</div>";
        chuoiHtml += "</div>";

        hopThoai.innerHTML = chuoiHtml;
        document.body.appendChild(hopThoai);
    }

    var nutDongY = document.getElementById("btnConfirmYes");
    nutDongY.onclick = function () {
        window.location.href = url;
    };

    hopThoai.style.display = "flex";
}

function closeConfirmPopup() {
    var hopThoai = document.getElementById("confirmModal");
    if (hopThoai != null) {
        hopThoai.style.display = "none";
    }
}

function batTatThongSo() {
    var cacDongAn = document.querySelectorAll('.dong_an_di');
    var nutBam = document.getElementById('btn_hien_thong_so');

    var dangAn = true;
    if (cacDongAn.length > 0) {
        if (window.getComputedStyle(cacDongAn[0]).display !== 'none') {
            dangAn = false;
        }
    }

    if (dangAn == true) {
        for (var i = 0; i < cacDongAn.length; i++) {
            cacDongAn[i].style.display = 'table-row';
        }
        nutBam.innerHTML = 'Thu gọn <i class="fa-solid fa-chevron-up"></i>';
    } else {
        for (var j = 0; j < cacDongAn.length; j++) {
            cacDongAn[j].style.display = 'none';
        }
        nutBam.innerHTML = 'Xem thêm cấu hình chi tiết <i class="fa-solid fa-chevron-down"></i>';
    }
}

function batTatBaiVietMoTa() {
    var khungNoiDung = document.getElementById('noi_dung_bai_viet_mo_ta');
    var lopPhu = document.getElementById('lop_phu_mo_ta');
    var nutBam = document.getElementById('btn_thu_gon_mo_ta');
    if (khungNoiDung.className.indexOf('mo_rong_mo_ta') !== -1) {
        khungNoiDung.className = khungNoiDung.className.replace('mo_rong_mo_ta', '').trim();
        lopPhu.style.display = 'block';
        nutBam.innerHTML = 'XEM THÊM <i class="fa-solid fa-angles-down"></i>';
    } else {
        khungNoiDung.className += ' mo_rong_mo_ta';
        lopPhu.style.display = 'none';
        nutBam.innerHTML = 'THU GỌN <i class="fa-solid fa-angles-up"></i>';
    }
}

function applyFilters() {
    var danhSachThuongHieu = [];
    var cacODanhDau = document.querySelectorAll('#cblThuongHieu input[type="checkbox"]');

    for (var i = 0; i < cacODanhDau.length; i++) {
        if (cacODanhDau[i].checked == true) {
            danhSachThuongHieu.push(cacODanhDau[i].value);
        }
    }

    var mucGia = "0";
    var cacORadioGia = document.querySelectorAll('#rblMucGia input[type="radio"]');
    for (var j = 0; j < cacORadioGia.length; j++) {
        if (cacORadioGia[j].checked == true) {
            mucGia = cacORadioGia[j].value;
            break;
        }
    }

    var sapXep = document.getElementById('ddlSapXep').value;

    var tuKhoa = "";
    var chuoiTimKiem = window.location.search;
    if (chuoiTimKiem.indexOf("search=") !== -1) {
        var params = new URLSearchParams(chuoiTimKiem);
        tuKhoa = params.get('search');
    }

    var urlMoi = 'CuaHang.aspx?';

    if (tuKhoa != null && tuKhoa !== "") {
        urlMoi += 'search=' + encodeURIComponent(tuKhoa) + '&';
    }
    if (danhSachThuongHieu.length > 0) {
        urlMoi += 'brand=' + danhSachThuongHieu.join(',') + '&';
    }
    if (mucGia !== "" && mucGia !== "0") {
        urlMoi += 'price=' + mucGia + '&';
    }
    if (sapXep !== "" && sapXep !== "new") {
        urlMoi += 'sort=' + sapXep;
    }

    window.location.href = urlMoi;
}
window.onload = function () {
    var cacTheInput = document.querySelectorAll('#cblThuongHieu input, #rblMucGia input, #ddlSapXep');

    for (var i = 0; i < cacTheInput.length; i++) {
        cacTheInput[i].onchange = function () {
            applyFilters();
        };
    }
    
    if (document.getElementById('divCardDetails') != null) {
        hienThiKhungThe();
    }
};

function hienThiKhungThe() {
    var danhSachRadio = document.querySelectorAll('input[type="radio"]');
    var giaTriChon = '';

    for (var i = 0; i < danhSachRadio.length; i++) {
        if (danhSachRadio[i].name.indexOf('rblPhuongThuc') !== -1 && danhSachRadio[i].checked == true) {
            giaTriChon = danhSachRadio[i].value;
            break;
        }
    }

    var khungThe = document.getElementById('divCardDetails');
    if (khungThe != null) {
        if (giaTriChon === 'CARD') {
            khungThe.style.display = 'block';
        } else {
            khungThe.style.display = 'none';
        }
    }
}