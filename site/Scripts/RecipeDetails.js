$(function () {
    $('#' + lnkNewRecipe).click(function () {
        var data = { method: 'IsLoggedIn' };
        $.post('Handler.ashx', data, function (data) {
            if (data === '') {
                OpenLoginDialog(function () {
                    window.location = 'RecipeEdit.aspx';
                });
            }
            else
                window.location = 'RecipeEdit.aspx';
        });
    })
})

$(document).ready(() => {

    isLoggedIn(function (user) {
        if (user === null) {
            $('#removeRecipe').hide();
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
    })
});