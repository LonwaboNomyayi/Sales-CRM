$(document).ready(function () {
    if ($('#IsRegistered').val() ==="1") {
        swal({
            title: "Success",
            text: "All done. User added Successfully",
            icon: "success",
            buttons: true,
            showCancelButton: false,
        });
    }
});