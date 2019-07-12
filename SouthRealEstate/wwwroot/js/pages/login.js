$(document).ready(function () {

    $(document).on("click", "#btn__login", function () {
        var user = $('#login_user').val();
        var password = $('#login_password').val();

        var data = {
            "UserName": user,
            "Password": password,
        };

        $.post('/api/acount/login', data, function (data) {
            window.location.replace("/Administrator");
        })
        .fail(function (httpObj, textStatus) {
            if (httpObj.status === 401) {
                toastr.warning('user/password doesnt match');
            }
            else {
                toastr.error(textStatus);
            }
        });        
    })

});
