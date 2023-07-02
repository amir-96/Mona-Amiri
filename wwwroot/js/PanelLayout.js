function showMobilePanel() {
  const mobilePanel = document.getElementById("mobile-panel");
  const MobileItems = document.getElementById("mobile-items-menu");

  if (mobilePanel.style.display === "none") {
    mobilePanel.style.display = "block";
    MobileItems.style.right = "0";
  }
}

function closeMobilePanel() {
  const mobilePanel = document.getElementById("mobile-panel");
  const MobileItems = document.getElementById("mobile-items-menu");

  if (mobilePanel.style.display === "block") {
    mobilePanel.style.display = "none";
    MobileItems.style.right = "-70%";
  }
}
