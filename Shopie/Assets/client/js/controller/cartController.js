var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/";
        });


        $('#btnBuyNow').off('click').on('click', function () {
            window.location.href = "/thanh-toan";
        });
        $(document).ready(function () {
            $('input.item_quantity').change(function () {
                var listProduct = $('.item_quantity');
                var cartList = [];
                $.each(listProduct, function (i, item) {
                    cartList.push({
                        Quantity: $(item).val(),
                        Product: {
                            ID: $(item).data('id')
                        }
                    });
                });
                $.ajax({
                    url: '/Cart/Update',
                    data: { cartModel: JSON.stringify(cartList) },
                    dataType: 'json',
                    type: 'POST',
                    success: function (res) {
                        if (res.status == true) {
                            window.location.href = "/gio-hang";
                        }
                    }
                })
            })
        });
       
        $('#btnBuyNow').off('click').on('click', function () {
            window.location.href = "/thanh-toan";
        });
        $(document).ready(function () {
            $('button.btn').click(function (e) {
                e.preventDefault();
                $.ajax({
                    data: { id: $(this).data('id') },
                    url: '/Cart/Delete',
                    dataType: 'json',
                    type: 'POST',
                    success: function (res) {
                        if (res.status == true) {
                            window.location.href = "/gio-hang";
                        }
                    }
                })
            })
        });
        $('#btnDeleteCart1,#btnDeleteCart2').off('click').on('click', function () {

            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });

        $('#btnDelete1').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/Cart/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });
    }
}
cart.init();