$(document).ready(() => {

    $("#popuplogin").dialog({
        autoOpen: false,
        title: "כניסת חברים",
        width: 400,
        height: 230,
        resizable: false,
        modal: true,
        show: 'fade',
        hide: 'fade',
        buttons: {
            "אישור": () => { login(); },
            "ביטול": () => { closeLogin(); }
        },
        open: () => {
            $('.ui-dialog-title').css('float', 'right');
            $('.ui-dialog-title').css('margin-right', '20px');
            $('.ui-dialog-titlebar').css('width', '92%');
        }
    });

    $("#message").dialog({
        autoOpen: false,
        width: 300,
        resizable: false,
        modal: true,
        buttons: { "ביטול": function () { $("#message").dialog("close"); } }
    });

    /*Header Login*/
    $('#header a.login').css('cursor', 'pointer');

    var loginBtn = $('#header a.login');
    loginBtn.click(() => {
        var n = isLoggedIn();
        n.done((loggedIn) => {
            if (loggedIn === 'False') {
                $(".add-recipe").hide();
                openLoginDialog(() => {
                    window.location = window.location.href;
                });
            } else {
                $(".add-recipe").show();
                logout();
            }
        });
    });

    var isLoggedIn = () => {
        return $.post('Handler.ashx', { method: 'IsLoggedIn' }, (result) => {
            return result;
        });
    };

    var login = () => {
        var userName = $("#textUserName").val();
        var password = $("#textPassword").val();

        var data = { method: 'Login', UserName: userName, Password: password };
        $.post('Handler.ashx', data, (data) => {
            if (data !== '' && data !== 'null') {
                var user = $.parseJSON(data);
                window.userId = user.UserId;
                switch (user.UserTypeId) {
                    case 1:
                        location.href = 'Admin/Admin.aspx';
                        break;
                    case 2:
                        loginBtn.html('יציאה');
                        $('.hello-user').html('שלום, ' + user.DisplayName);
                        $("#popuplogin").dialog('close');
                        $('.add-recipe').show();
                        break;
                }
            }
            else {
                alert('Login failed');
                return;
            }
        });
    };

    var closeLogin = () => {
        $("#popuplogin").dialog("close");
        SetSearchOption(1);
    };

    var openLoginDialog = () => {
        $("#textUserName").val('');
        $("#textPassword").val('');
        $("#popuplogin").dialog('open');
        $("#textUserName").focus();
    };

    var logout = () => {
        $.post('Handler.ashx', { method: 'Logout' }, () => {
            loginBtn.html('כניסה');
            $('.hello-user').html('שלום, אורח');
            ResetSearch(1);
            window.location = 'recipes.aspx';
            window.userId = null;
        });
    };

    (() => {
        var n = isLoggedIn();
        n.done((loggedIn) => {
            if (loggedIn === 'False') {
                $(".add-recipe").hide();
            } else {
                $(".add-recipe").show();
            }
        });
    })();
});