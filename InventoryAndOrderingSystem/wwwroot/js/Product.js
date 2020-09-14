$(document).ready(function () {
    ProductList();
});

$(document).on("click", "#BtnShowModalAddProduct", function () {
    FetchData("/Product/CreateProductModal", null).done(function (content) {
        $("#ModalProductContent").html(content);
    });
});

$(document).on("click", "#BtnEditProduct", function () {
    if (confirm("Sure you want to edit this product?") == true) {
        $("#BtnConfirmEditProduct").click();
    }
});

function ProductList() {
    FetchData("/Product/ListingProductTable", null).done(function (content) {
        $("#ProductListTable").html(content);
        $("#ProductListTableRender").DataTable();
    });
}

function ShowEditProductDetail(ProductID) {
    FetchData("/Product/EditProductModal", { ProductID: ProductID }).done(function (content) {
        $("#ModalProductContent").html(content);
    });
}

function Delete(ProductID) {
    if (confirm("Sure you want to delete this product?") == true) {
        FetchData("/Product/Delete", { ProductID: ProductID }).done(function () {
            ProductList();
        });
    }
}