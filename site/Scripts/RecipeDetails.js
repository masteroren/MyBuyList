$(document).ready(() => {

    registerToLoginNotifications((loginNotification) => {
        if (loginNotification.loggedIn) {
            $('.hide-on-logout').show();
            checkFavorits(loginNotification.userId);
        } else {
            $('.hide-on-logout').hide();
        }
    });

    $('#removeRecipe').click(function () {
        var recipeId = $('#hfRecipeId').val();

        var url = $('#RestUrl').val() + '/api/recipes/' + recipeId;

        $.ajax(
            url, {
                type: 'DELETE',
                contentType: 'application/json',
                dataType: 'json',
                success: function (response) {
                    window.location = 'Recipes.aspx';
                },
                error: function (xhr, status, error) {
                    console.log(error);
                }
            });
    });

    var checkFavorits = (userId) => {
        var recipeId = $('#hfRecipeId').val();
        var data = {
            method: 'IsUserFavoritRecipe',
            recipeId,
            userId
        };

        $.post('ASHX/Recipes.ashx', data, (res) => {
            var oRes = $.parseJSON(res);
            $('.add-to-favorites').hide();
            $('.remove-from-favorites').hide();
            if (oRes !== null) {
                $('.remove-from-favorites').show();
            } else {
                $('.add-to-favorites').show();
            }
        });
    };
});