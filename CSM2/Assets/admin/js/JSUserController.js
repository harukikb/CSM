var config = {
    page: 1,
    filter: ''
};
var userController = {
    init: function () {
        userController.registerEvent();
    },
    registerEvent: function () {
        //change status
        $('.btn-status').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);

            var id = btn.data('id');
            var text = btn.text() === "Kích hoạt" ? "Khóa" : "Kích hoạt";
            if (confirm("Bạn muốn "+text+" thành viên này?")) {
                $.ajax({
                    type: 'POST',
                    url: '/Admin/User/ChangeStatus',
                    data: { userID: id },
                    dataType: 'json',
                    success: function (response) {
                        if (response) {
                            btn.attr('class', 'label label-success');
                            btn.text('Kích hoạt');
                        } else {
                            btn.attr('class', 'label label-danger');
                            btn.text('Khóa');
                        }
                    },
                    error: function (response) {
                        alert(response.message);
                    }
                });
            }


        });

        //delete
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            if (confirm("Bạn thực sự muốn xóa thành viên này?")) {
                $.ajax({
                    type: 'POST',
                    url: '/Admin/User/Delete',
                    data: { userID: id },
                    dataType: 'json',
                    success: function(response) {
                        if (response) {
                            $('#row_' + id).remove();
                        } else {
                            alert("Không thể xóa vì bạn đang đăng nhập");
                            
                        } 
                    },
                    error: function (response) {
                        alert(response.message);
                    }
                   
                });
            }
        });
        

        //sửa dữ liệu
        $('.edit').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                var id = $(this).data("id");
                var column = $(this).data("column");
                var value = $(this).val();
                $.ajax({
                    url: "/Admin/User/EditName",
                    type: "POST",
                    data: {
                        userID: id,
                        column: column,
                        name: value,
                    },
                    success: function (data) {
                        if (data) {
                            alert("Cập nhật thành công!");
                        }
                    }
                });
            }
        });


    },
};
userController.init();