﻿$(document).ready(() => {

    notificatiosCallbacks = [];
    this.registerToLoginNotifications = (callback) => {
        notificatiosCallbacks.push(callback);
    };

    $("#popuplogin").dialog({
        classes: {
            "ui-dialog": "login-dialog"
        },
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
        $('.hide-on-logout').hide();
        var n = isLoggedIn();
        n.done((userInfo) => {
            if (userInfo !== '' && userInfo !== null) {
                logout();
            } else {
                openLoginDialog(() => {
                    window.location = window.location.href;
                });
            }
        });
    });

    var isLoggedIn = () => {
        return $.post('ASHX/LoginActions.ashx', { method: 'IsLoggedIn' }, (result) => {
            return result;
        });
    };

    var login = () => {
        var userName = $("#textUserName").val();
        var password = $("#textPassword").val();

        var data = { method: 'Login', UserName: userName, Password: password };
        $.post('ASHX/LoginActions.ashx', data, (data) => {
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
                        $('.on-login').show();
                        $('.on-logout').hide();
                        $("#popuplogin").dialog('close');
                        break;
                }

                notificatiosCallbacks.forEach((callback) => {
                    callback({
                        loggedIn: true,
                        userId: user.UserId
                    });
                });
            }
            else {
                alert('Login failed');
                $('.login-trigger').hide();

                notificatiosCallbacks.forEach((callback) => {
                    callback({
                        loggedIn: false
                    });
                });

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
        $.post('ASHX/LoginActions.ashx', { method: 'Logout' }, () => {
            loginBtn.html('כניסה');
            $('.hello-user').html('שלום, אורח');
            ResetSearch(1);
            window.userId = null;
            $('.on-login').hide();
            $('.on-logout').show();

            notificatiosCallbacks.forEach((callback) => {
                callback({
                    loggedIn: false
                });
            });
        });
    };

    (() => {
        var n = isLoggedIn();
        n.done((userInfo) => {
            if (userInfo !== '' && userInfo !== null) {
                var user = $.parseJSON(userInfo);
                $('.on-login').show();
                $('.on-logout').hide();
                notificatiosCallbacks.forEach((callback) => {
                    callback({
                        loggedIn: user !== null,
                        userId: user.UserId
                    });
                });
            } else {
                $('.on-login').hide();
                $('.on-logout').show();
            }
        });
    })();
});