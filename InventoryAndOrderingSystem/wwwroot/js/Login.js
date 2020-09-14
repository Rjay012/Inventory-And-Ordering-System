$(document).ready(function () {
    ShowLoginForm();

    function ShowLoginForm() {
        FetchData("/Login/LoginCard", null).done(function (content) {
            $(".card-body").html(content);
        });
    }
});