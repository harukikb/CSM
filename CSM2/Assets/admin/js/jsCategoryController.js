var category = {
    init: function () {
        category.registerEvents();
    },
    registerEvents: function () {
        $('.btn-status').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);

            var id = btn.data('id');
            var text = btn.text() === "Kích hoạt" ? "khóa" : "kích hoạt";
            if (confirm("Bạn muốn " + text + " sản phẩm này?")) {
                $.ajax({
                    type: 'POST',
                    url: '/Admin/Category/ChangeStatus',
                    data: { id: id },
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
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);

            var id = btn.data('id');

            if (confirm("Bạn thực sự muốn xóa danh mục này?")) {
                $.ajax({
                    type: 'POST',
                    url: '/Admin/Category/Delete',
                    data: { id: id },
                    dataType: 'json',
                    success: function (response) {
                        if (response) {
                            $('#row_' + id).remove();
                        }
                    },
                    error: function (response) {
                        alert(response.message);
                    }
                });
            };
        });
        $('.edit').off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                var id = $(this).data("id");
                var value = $(this).val();
                $.ajax({
                    url: "/Admin/Category/EditName",
                    type: "POST",
                    data: {
                        cateID: id,
                        cateName: value,
                    },
                    success: function (data) {
                        if (data) {
                            alert("Update Success!");
                        }
                    }
                });
            }
        });
    }
}
category.init();