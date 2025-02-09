function onSuccessSendEmail(res) {
    if (res.status == 100) {
        Swal.fire({
            title: "موفق",
            text: res.message,
            icon: "success"
        }).then((result) => {
            if (result.isConfirmed) {
                location.reload();
            } else {

                setTimeout(() => {
                    location.reload();
                }, 5000);

            }
        })
    } else {
        Swal.fire({
            title: "خطا",
            text: res.message,
            icon: "error"
        })
    }
}