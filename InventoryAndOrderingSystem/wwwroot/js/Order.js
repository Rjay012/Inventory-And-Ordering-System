$(document).ready(function () {
    ShowListingOrder();
});

function ShowListingOrder() {
    FetchData("/Order/RenderListingOrderTable", null).done(function (content) {
        $("#CustomerOrderList").html(content);

        var columns = [
            {
                'data': 'orderID', render: function (id, type, row) {
                    return "<p>" + row.productName + "</p>" +
                        "<p>Order No.: <span class='float-right'>" + row.orderID + "</span></p>" +
                        "<p>Price: <span class='float-right'>" + CurrencyFormat(parseFloat(row.price)) + "</span></p>" +
                        "<p>Quantity: <span class='float-right'>" + row.quantity + "</span></p>";
                }, 'width': '20%'
            },
            { 'data': 'productID' }, { 'data': 'customerName' }, { 'data': 'shippingAddress' }, { 'data': 'deliveryDate', 'width': '5%' },
            {
                'data': '', render: function (id, type, row) {
                    return CurrencyFormat(parseFloat(row.price) * parseInt(row.quantity));
                }, 'width': '5%'
            },
            { 'data': 'status', 'width': '5%' },
            {
                'data': '', render: function (id, type, row) {
                    var disableBtn = row.status != "on process" ? "disabled" : "";
                    return "<div class='dropdown'>" +
                        "<button class='btn btn-secondary dropdown-toggle' type='button' id='DropdownOrderAction' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false' " + disableBtn + ">Action</button>" +
                        "<div class='dropdown-menu' aria-labelledby='DropdownOrderAction'>" +
                        "<a class='dropdown-item' href='#' id='BtnOrderDeliveryDetail' OrderID='" + row.orderID + "' data-toggle='modal' data-target='#ModalOrder'>Accept</a>" +
                        "<a class='dropdown-item' href='#' onclick='RejectCustomerOrder(" + parseInt(row.orderID) + ", " + parseInt(row.productID) + ", " + parseInt(row.quantity) + ")'>Reject</a>" +
                        "</div>" +
                        "</div>";
                }
            },
            {
                'data': '', render: function (id, type, row) {
                    return "<button class='btn btn-danger' type='button' id='BtnDeleteOrder' OrderID='" + row.orderID + "' Status='" + row.status + "'>REMOVE</button>";
                }
            }
        ];

        var columnDefs = [{
            targets: [1],
            visible: false,
            searchable: false,
            orderable: false
        }];
        LoadTableViaServerSide("OrderListTable", "/Order/ListingOrder", columns, columnDefs, null);
    });
}

function CurrencyFormat(amount) {                  //thanks to : https://stackoverflow.com/questions/149055/how-to-format-numbers-as-currency-string
    var formatter = Intl.NumberFormat("en-PH", {
        style: "currency",
        currency: "PHP"
    }).format(amount);

    return formatter;
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