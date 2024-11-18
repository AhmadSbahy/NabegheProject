const darkMode = localStorage.getItem("darkMode");
const darkModeCheckbox = document.querySelector("#dark-mode-checkbox");

if (darkMode === "true") {
    document.documentElement.classList.add("dark");
    darkModeCheckbox.checked = true;
} else {
    document.documentElement.classList.remove("dark");
    darkModeCheckbox.checked = false;
}

const darkModeButton = document.querySelector("#dark-mode-button");
darkModeButton.addEventListener("click", toggleDarkMode);
darkModeCheckbox.addEventListener("change", toggleDarkMode);

function toggleDarkMode() {
    if (document.documentElement.classList.contains("dark")) {
        document.documentElement.classList.remove("dark");
        localStorage.setItem("darkMode", "false");
        darkModeCheckbox.checked = false;
    } else {
        document.documentElement.classList.add("dark");
        localStorage.setItem("darkMode", "true");
        darkModeCheckbox.checked = true;
    }
}

const singleSwiperSlider = new Swiper(".single-swiper-slider", {
    spaceBetween: 20,
    slidesPerView: 1,
    loop: true,
    effect: "creative",
    creativeEffect: {
        prev: {
            shadow: true,
            translate: [0, 0, -400],
        },
        next: {
            translate: ["100%", 0, 0],
        },
    },
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
    pagination: {
        el: ".swiper-pagination",
    },
    autoplay: {
        delay: 3500,
        disableOnInteraction: false,
    },
});

const col3SwiperSlider = new Swiper(".col3-swiper-slider", {
    spaceBetween: 20,
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
    breakpoints: {
        992: {
            slidesPerView: 3,
        },
        576: {
            slidesPerView: 2,
        },
        0: {
            slidesPerView: 1,
        },
    },
});

const col4SwiperSlider = new Swiper(".col4-swiper-slider", {
    spaceBetween: 20,
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
    breakpoints: {
        992: {
            slidesPerView: 4,
        },
        768: {
            slidesPerView: 3,
        },
        480: {
            slidesPerView: 2,
        },
        0: {
            slidesPerView: 1,
        },
    },
});

const autoSwiperSlider = new Swiper(".auto-swiper-slider", {
    slidesPerView: "auto",
    spaceBetween: 30,
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
});

const cardSwiperSlider = new Swiper(".card-swiper-slider", {
    effect: "cards",
    grabCursor: true,
    autoplay: {
        delay: 3000,
    },
    cardsEffect: {
        rotate: 50,
        slideShadows: false,
    },
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
});

const players = document.querySelectorAll(".js-player");

if (players) {
    Array.from(players).map(
        (p) =>
            new Plyr(p, {
                // options
            })
    );
}

const scrollToTopBtn = document.getElementById("scrollToTopBtn");

if (scrollToTopBtn) {
    scrollToTopBtn.addEventListener("click", function (event) {
        window.scrollTo({
            top: 0,
            behavior: "smooth",
        });
    });
}

function confirmDelete(url, e) {
    e.preventDefault();

    Swal.fire({
        text: "آیا از انجام این عملیات مطمئن هستید؟",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "بله",
        cancelButtonText: "خیر"
    }).then((result) => {
        if (result.isConfirmed) {
            // اجرای درخواست AJAX
            fetch(url, {
                method: 'DELETE', // یا 'POST' بسته به API شما
                headers: {
                    'Content-Type': 'application/json',
                    'X-Requested-With': 'XMLHttpRequest' // اختیاری، برای مشخص کردن درخواست AJAX
                }
            })
                .then(response => {
                    if (response.ok) {
                        // در صورت موفقیت‌آمیز بودن حذف، پیامی به کاربر نمایش داده و صفحه را رفرش کنید
                        Swal.fire({
                            text: "عملیات با موفقیت انجام شد.",
                            icon: "success",
                            confirmButtonColor: "#3085d6",
                            confirmButtonText: "باشه"
                        }).then(() => {
                            // رفرش کردن صفحه یا حذف آیتم از DOM
                            location.reload(); // یا حذف آیتم از لیست بدون رفرش
                        });
                    } else {
                        // در صورت بروز خطا
                        Swal.fire({
                            text: "خطایی رخ داده است. دوباره تلاش کنید.",
                            icon: "error",
                            confirmButtonColor: "#3085d6",
                            confirmButtonText: "باشه"
                        });
                    }
                })
                .catch(error => {
                    // نمایش پیام در صورت بروز خطای شبکه
                    Swal.fire({
                        text: "مشکلی در شبکه پیش آمده است.",
                        icon: "error",
                        confirmButtonColor: "#3085d6",
                        confirmButtonText: "باشه"
                    });
                });
        }
    });
}
function fillPageId(pageId) {
    $("#Page").val(pageId);
    $("#filter-search").submit();
}
