$(function () {
    $('#' + lnkNewRecipe).click(function () {
        var data = { method: 'IsLoggedIn' };
        $.post('Handler.ashx', data, function (data) {
            if (data == '') {
                OpenLoginDialog(function () {
                    window.location = 'RecipeEdit.aspx';
                });
            }
            else
                window.location = 'RecipeEdit.aspx';
        });
    })
})

$(document).ready(function () {

    $('#removeRecipe').click(function () {
        var recipeId = $('#hfRecipeId').val();

        baseUrl = window.mblRestHost;
        var url = baseUrl + 'Recipes/delete/' + recipeId;

        $.post(url, {}, function (response) {
            console.log(response)
        }, 'json');
    })
});