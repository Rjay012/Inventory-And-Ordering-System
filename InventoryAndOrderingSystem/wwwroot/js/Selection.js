$(document).ready(function () {
    ShowProductList();
});

$(document).on("click", "#BtnShowCustomerItem", function () {
    ShowCustomerOrder();
});

$(document).on("click", "#BtnOrderItem", function () {
    FetchData("/Order/OrderDetailModal", { productID: parseInt($(this).attr("ProductID")) }).done(function (content) {
        $("#ModalOrderItemContent").html(content);
    });
});

$(document).on("keyup", "#txtOrderQuantity", function () {
    var max = parseInt($(this).attr("max"));
    var value = parseInt($(this).val());

    if (value > max) {
        $(this).val(max);
    }
    else if (value == 0) {
        $(this).val(1);
    }
});

function ShowProductList() {
    FetchData("/Product/ListingProduct", null).done(function (content) {
        $("#ProductList").html(content);
    });
}

function ShowCustomerOrder() {
    FetchData("/Order/ListingCustomerOrder", null).done(function (content) {
        $("#ModalCustomerItemContent").html(content);
    });
}

function CancelOrder(OrderID, ProductID, Quantity) {
    var OrderDetail = {
        OrderID: OrderID,
        ProductID: ProductID,
        Quantity: Quantity
    };

    if (confirm("Sure you want to cancel your order?") == true) {
        FetchData("/Order/CancelOrder", { orderModel: OrderDetail }).done(function (response) {
            if (response.response == "success") {
                ShowCustomerOrder();
                ShowProductList();
            }
        });
    }
}