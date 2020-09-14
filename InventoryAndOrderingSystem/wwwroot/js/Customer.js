$(document).ready(function () {
    ShowListingCustomer();
});

$(document).on("click", "#BtnAddCustomer", function () {
    var password = $("#txtPassword").val();
    var confirmPassword = $("#txtConfirmPassword").val();

    if (password === confirmPassword) {
        $("#BtnConfirmAddCustomer").click();
    }
    else {
        alert("Password confirmation should matched!");
    }
});

$(document).on("click", "#BtnShowModalAddCustomer", function () {
    FetchData("/Customer/AddCustomerModal", null).done(function (content) {
        $("#ModalCustomerContent").html(content);
    });
});

$(document).on("click", "#BtnEditCustomer", function () {
    var password = $("#txtEditPassword").val();
    var confirmPassword = $("#txtEditConfirmPassword").val();

    if (password === confirmPassword) {
        if (confirm("Sure you wan to edit this customer info.?") == true) {
            $("#BtnConfirmEditCustomer").click();
        }
    }
});

function ShowListingCustomer() {
    FetchData("/Customer/ListingCustomer", null).done(function (content) {
        $("#CustomerList").html(content);
        $("#CustomerListTable").DataTable();
    });
}

function ShowEditCustomerDetail(customerID) {
    FetchData("/Customer/EditCustomerDetail", { customerID: customerID }).done(function (content) {
        $("#ModalCustomerContent").html(content);
    });
}

function DeleteCustomer(customerID) {
    if (confirm("Sure you want to delete this customer?") == true) {
        FetchData("/Customer/Delete", { customerID: customerID }).done(function () {
            ShowListingCustomer();
        });
    }
}