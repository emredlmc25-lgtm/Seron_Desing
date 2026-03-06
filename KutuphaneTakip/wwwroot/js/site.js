(function () {
  const shell = document.getElementById("appShell");
  const toggleButton = document.getElementById("sidebarToggle");
  const overlay = document.getElementById("sideOverlay");

  if (!shell || !toggleButton) {
    return;
  }

  const storageKey = "seron-sidebar-collapsed";
  const mobileWidth = 992;

  const isMobile = () => window.innerWidth < mobileWidth;

  const setCollapsed = (collapsed) => {
    shell.classList.toggle("sidebar-collapsed", collapsed);
    localStorage.setItem(storageKey, collapsed ? "1" : "0");
  };

  const closeMobileMenu = () => {
    shell.classList.remove("sidebar-mobile-open");
  };

  const initialCollapsed = localStorage.getItem(storageKey) === "1";
  if (!isMobile() && initialCollapsed) {
    setCollapsed(true);
  }

  toggleButton.addEventListener("click", () => {
    if (isMobile()) {
      shell.classList.toggle("sidebar-mobile-open");
      return;
    }

    setCollapsed(!shell.classList.contains("sidebar-collapsed"));
  });

  if (overlay) {
    overlay.addEventListener("click", closeMobileMenu);
  }

  document.querySelectorAll(".side-link, .side-cta").forEach((link) => {
    link.addEventListener("click", () => {
      if (isMobile()) {
        closeMobileMenu();
      }
    });
  });

  window.addEventListener("resize", () => {
    if (!isMobile()) {
      shell.classList.remove("sidebar-mobile-open");
      return;
    }

    shell.classList.remove("sidebar-collapsed");
  });
})();
