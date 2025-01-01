let navbar1 = document.getElementById("navbarId");
window.onscroll = function () {
    if (window.scrollY > 76) {
        navbar1.classList.add("navbarBg");
    }
    else {
        navbar1.classList.remove("navbarBg");
    }
}