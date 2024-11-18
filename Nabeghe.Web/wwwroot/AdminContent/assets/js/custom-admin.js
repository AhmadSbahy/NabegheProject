//function confirmDelete(url,e) {

//    e.preventDefault();

//    Swal.fire({
//        text: "آیا از انجام این عملیات مطمئن هستید؟",
//        icon: "question",
//        showCancelButton: true,
//        confirmButtonColor: "#3085d6",
//        cancelButtonColor: "#d33",
//        confirmButtonText: "بله",
//        cancelButtonText: "خیر"
//    }).then((result) => {
//        if (result.isConfirmed) {
//            location.href = url;
//        }
//    });

//}

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