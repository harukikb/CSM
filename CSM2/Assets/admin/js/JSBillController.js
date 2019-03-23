var bill = {
    init: function () {
        bill.registerEvents();
    },
    registerEvents: function () {
        
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);

            var id = btn.data('id');

            if (confirm("Bạn thực sự muốn xóa hóa đơn này?")) {
                $.ajax({
                    type: 'POST',
                    url: '/Admin/Bill/Delete',
                    data: { billID: id },
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
                    url: "/Admin/Bill/EditCount",
                    type: "POST",
                    data: {
                        orderID: id,
                        count: value,
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
bill.init();