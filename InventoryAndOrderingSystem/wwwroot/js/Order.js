$(document).ready(function () {
    ShowListingOrder();
});

function ShowListingOrder() {
    FetchData("/Order/ListingOrder", null).done(function (content) {
        $("#CustomerOrderList").html(content);
        $("#OrderListTable").DataTable();
    });
}

function RejectCustomerOrder(orderID, productID, quantity) {
    if (confirm("Sure you want to reject this order?") == true) {
        var orderModel = {
            OrderID: orderID,
            ProductID: productID,
            Quantity: quantity
        };

        FetchData("/Order/RejectCustomerOrder", { orderModel: orderModel }).done(function (response) {
            ShowListingOrder();
        });
    }
}

$(document).on("click", "#BtnDeleteOrder", function () {
    var orderID = parseInt($(this).attr("OrderID"));
    var status = $(this).attr("Status");

    if (status == "on process") {
        alert("You can't delete an order that is still on process!");
    }
    else {
        if (confirm("Sure you want to delete this order?") == true) {
            FetchData("/Order/Delete", { orderID: orderID }).done(function (response) {
                ShowListingOrder();
            });
        }
    }
});

$(document).on("click", "#BtnOrderDeliveryDetail", function () {
    FetchData("/Order/OrderDeliveryDetail", { orderID: parseInt($(this).attr("OrderID")) }).done(function (content) {
        $("#ModalOrderContent").html(content);
        $("#txtOrderDeliveryDate").datepicker();
    });
});