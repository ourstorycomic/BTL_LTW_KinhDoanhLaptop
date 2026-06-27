function showToast(msg) {
    var x = document.getElementById("toast");
    if(x) {
        x.innerText = msg;
        x.className = "show";
        setTimeout(function(){ x.className = x.className.replace("show", ""); }, 3000);
    }
}
