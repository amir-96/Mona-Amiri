// Header shadow

window.addEventListener("scroll", function () {
  const header = document.getElementById("desktop-header");
  const goTopArrow = document.getElementById("go-top-arrow");
  const scrollPosition = window.scrollY || window.pageYOffset;

  if (scrollPosition > 0) {
    header.classList.add("bg-white");
    header.classList.add("shadow");
    goTopArrow.classList.remove("hidden");
  } else {
    header.classList.remove("bg-white");
    header.classList.remove("shadow");
    goTopArrow.classList.add("hidden");
  }
});

// Mobile menu

function openMobileMenu() {
  const mobileMenu = document.getElementById("mobile-menu");

  mobileMenu.style.right = "0";
}

function closeMobileMenu() {
  const mobileMenu = document.getElementById("mobile-menu");

  mobileMenu.style.right = "-100vw";
}

// Reservation section

const tabs = document.querySelectorAll(".tabs_wrap ul li");
const personalElements = document.querySelectorAll(".personal-form");
const beautifierElements = document.querySelectorAll(".beautifier-form");
const serviceElements = document.querySelectorAll(".service-form");
const timeElements = document.querySelectorAll(".time-form");
const paymentElements = document.querySelectorAll(".payment-form");
const all = document.querySelectorAll("#tab");
var selectedMakeupArtist = "";

beautifierElements.forEach((item) => {
  item.style.display = "none";
});

serviceElements.forEach((item) => {
  item.style.display = "none";
});

timeElements.forEach((item) => {
  item.style.display = "none";
});

paymentElements.forEach((item) => {
  item.style.display = "none";
});

function goFirstTab() {
  const firstTab = document.getElementById("personal-form");
  const secondTab = document.getElementById("beautifier-form");

  secondTab.classList.remove("active");

  firstTab.classList.add("active");

  beautifierElements.forEach((item) => {
    item.style.display = "none";
  });

  personalElements.forEach((item) => {
    item.style.display = "block";
  });
}

function goSecondTab() {
  const firstTab = document.getElementById("personal-form");
  const secondTab = document.getElementById("beautifier-form");

  firstTab.classList.remove("active");

  secondTab.classList.add("active");

  personalElements.forEach((item) => {
    item.style.display = "none";
  });

  beautifierElements.forEach((item) => {
    item.style.display = "block";
  });
}

function goSecondTab2() {
  const secondTab = document.getElementById("beautifier-form");
  const thirdTab = document.getElementById("service-form");

  thirdTab.classList.remove("active");

  secondTab.classList.add("active");

  serviceElements.forEach((item) => {
    item.style.display = "none";
  });

  beautifierElements.forEach((item) => {
    item.style.display = "block";
  });
}

function goThirdTab() {
  selectedMakeupArtist = document.querySelector('input[name="beautifierRadio"]:checked');
  const secondTab = document.getElementById("beautifier-form");
  const thirdTab = document.getElementById("service-form");

  secondTab.classList.remove("active");

  thirdTab.classList.add("active");

  beautifierElements.forEach((item) => {
    item.style.display = "none";
  });

  serviceElements.forEach((item) => {
    item.style.display = "block";
  });

  if (selectedMakeupArtist) {
    var artistName = selectedMakeupArtist.value;

    var radioButtons = document.querySelectorAll('.service-radio');

    radioButtons.forEach((radioButton) => {
      if (radioButton.id !== `service-${artistName}`) {
        radioButton.parentElement.style.display = "none";
      } else {
        radioButton.parentElement.style.display = "flex";
      }
    })
  } else {
    var radioButtons = document.querySelectorAll('.service-radio');

    radioButtons.forEach((radioButton) => {
      radioButton.parentElement.style.display = "none";
    })
  }
}

function goThirdTab2() {
  const thirdTab = document.getElementById("service-form");
  const fourthTab = document.getElementById("time-form");

  fourthTab.classList.remove("active");

  thirdTab.classList.add("active");

  timeElements.forEach((item) => {
    item.style.display = "none";
  });

  serviceElements.forEach((item) => {
    item.style.display = "block";
  });
}

function goFourthTab() {
  const thirdTab = document.getElementById("service-form");
  const fourthTab = document.getElementById("time-form");

  thirdTab.classList.remove("active");

  fourthTab.classList.add("active");

  serviceElements.forEach((item) => {
    item.style.display = "none";
  });

  timeElements.forEach((item) => {
    item.style.display = "block";
  });

  if (selectedMakeupArtist) {
    var artistName = selectedMakeupArtist.value;

    var radioButtons = document.querySelectorAll('.time-radio');

    radioButtons.forEach((radioButton) => {
      if (radioButton.id !== `time-${artistName}`) {
        radioButton.parentElement.style.display = "none";
      } else {
        radioButton.parentElement.style.display = "flex";
      }
    })
  } else {
    var radioButtons = document.querySelectorAll('.service-radio');

    radioButtons.forEach((radioButton) => {
      radioButton.parentElement.style.display = "none";
    })
  }
}

function goFourthTab2() {
  const fourthTab = document.getElementById("time-form");
  const fifthTab = document.getElementById("payment-form");

  fifthTab.classList.remove("active");

  fourthTab.classList.add("active");

  paymentElements.forEach((item) => {
    item.style.display = "none";
  });

  timeElements.forEach((item) => {
    item.style.display = "block";
  });
}

function goFifthTab() {
  var choosenName = document.getElementById('choosen-name').value;
  var choosenPhone = document.getElementById('choosen-phone').value;
  var choosenMakeupArtist = document.querySelector('input[name="beautifierRadio"]:checked').value;
  var choosenService = document.querySelector('input[name="serviceRadio"]:checked').value;
  var choosenPrice = document.querySelector('input[name="serviceRadio"]:checked').getAttribute("data");
  var choosenTime = document.querySelector('input[name="timeRadio"]:checked').getAttribute("data");

  document.getElementById('set-choosen-name').innerText = 'نام: ' + choosenName;
  document.getElementById('set-choosen-phone').innerText = 'شماره همراه: ' + choosenPhone;
  document.getElementById('set-choosen-makeup-artist').innerText = 'آرایشگر: ' + choosenMakeupArtist;
  document.getElementById('set-choosen-service').innerText = 'سرویس: ' + choosenService;
  document.getElementById('set-choosen-time').innerText = choosenTime;
  document.getElementById('set-choosen-price').innerText = 'قیمت: ' + choosenPrice + ' تومان';

  const fourthTab = document.getElementById("time-form");
  const fifthTab = document.getElementById("payment-form");

  fourthTab.classList.remove("active");

  fifthTab.classList.add("active");

  timeElements.forEach((item) => {
    item.style.display = "none";
  });

  paymentElements.forEach((item) => {
    item.style.display = "block";
  });
}

const nameInput = document.querySelector('input[name="name"]');
const phoneInput = document.querySelector('input[name="phone"]');
const nameError = document.querySelector("#name-error");
const phoneError = document.querySelector("#phone-error");
const nextButton = document.querySelector("#next-button");
// nextButton.disabled = true;

phoneInput.addEventListener("change", function () {
  const phoneRegex = /09\d{9}/;

  if (!phoneRegex.test(phoneInput.value)) {
    phoneError.textContent = "شماره همراه وارد شده معتبر نیست";
    nextButton.disabled = true;
  } else {
    phoneError.textContent = "";
    nextButton.disabled = false;
  }
});

nameInput.addEventListener("change", function () {
  const nameRegex = /\d/;

  if (nameRegex.test(nameInput.value) || nameInput.value.length === 0) {
    nameError.textContent = "نام صحیح نمی باشد";
    nextButton.disabled = true;
  } else {
    nameError.textContent = "";
    nextButton.disabled = false;
  }
});

// Disable next tab button

const goThirdTabButton = document.querySelector(".go-third-tab-button");

goThirdTabButton.disabled = true;

function enableThirdButton() {
  goThirdTabButton.disabled = false;
}

const goFourthTabButton = document.querySelector(".go-fourth-tab-button");

goFourthTabButton.disabled = true;

function enableFourthTab() {
  goFourthTabButton.disabled = false;
}

const goFifthTabButton = document.querySelector(".go-fifth-tab-button");

goFifthTabButton.disabled = true;

function enableFifthTab() {
  goFifthTabButton.disabled = false;
}