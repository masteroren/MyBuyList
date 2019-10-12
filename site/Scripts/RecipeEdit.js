$(document).ready(() => {

    $('.hide-on-logout').hide();

    registerToLoginNotifications((loginNotification) => {
        if (loginNotification.loggedIn) {
            $('.hide-on-logout').show();
        } else {
            $('.hide-on-logout').hide();
        };
    });
});