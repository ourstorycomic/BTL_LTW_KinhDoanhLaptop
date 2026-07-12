function showToast(msg) {
    var x = document.getElementById("toast");
    if(x) {
        x.innerText = msg;
        x.className = "show";
        setTimeout(function(){ x.className = x.className.replace("show", ""); }, 3000);
    }
}

function showConfirmPopup(url) {
    var modal = document.getElementById("confirmModal");
    if (!modal) {
        modal = document.createElement("div");
        modal.id = "confirmModal";
        modal.className = "modal";
        modal.innerHTML = `
            <div class="modal-content">
                <h3 style="margin-bottom: 15px; color: #333;">Xác nhận xóa</h3>
                <p>Bạn có chắc chắn muốn xóa sản phẩm này khỏi giỏ hàng?</p>
                <div class="modal-buttons">
                    <button class="btn-no" onclick="closeConfirmPopup()">Hủy</button>
                    <button id="btnConfirmYes" class="btn-yes">Xóa</button>
                </div>
            </div>
        `;
        document.body.appendChild(modal);
    }
    
    var btnYes = document.getElementById("btnConfirmYes");
    btnYes.onclick = function() {
        window.location.href = url;
    };
    
    modal.style.display = "flex";
}

function closeConfirmPopup() {
    var modal = document.getElementById("confirmModal");
    if (modal) {
        modal.style.display = "none";
    }
}
