function deleteTodo(id) {


    Swal.fire({

        title: "حذف تسک",

        text: "آیا از حذف این تسک مطمئن هستید؟",

        icon: "warning",

        showCancelButton: true,

        confirmButtonText: "حذف",

        cancelButtonText: "لغو"


    }).then((result) => {


        if (result.isConfirmed) {

            window.location.href = "/Todo/Delete/" + id;

        }


    });


}
function deleteUser(id) {
    Swal.fire({

        title: "حذف کاربر",

        text: "آیا از حذف این کاربر مطمئن هستید؟",

        icon: "warning",

        showCancelButton: true,

        confirmButtonText: "حذف",

        cancelButtonText: "لغو"


    }).then((result) => {

        if (result.isConfirmed) {
            window.location.href = "/User/Delete/" + id;
        }

    });
}