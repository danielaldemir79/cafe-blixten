

(function () {
  var ToggleSidebarButton = document.getElementById("ToggleSidebar");
  var SidebarCloseButton = document.getElementById("SidebarClose");

  ToggleSidebarButton.addEventListener("click", function (event) {
    event.preventDefault();
    document.getElementById("Sidebar").classList.toggle("open");
  });

  SidebarCloseButton.addEventListener("click", function (event) {
    event.preventDefault();
    document.getElementById("Sidebar").classList.remove("open");
  });

  window.addEventListener("resize", function (event) {
    if (window.matchMedia("(max-width: 900px)").matches
      && document.getElementById("Sidebar").classList.contains("open")) {
      document.getElementById("Sidebar").classList.remove("open");
    }
  });
})();