﻿$(function () {
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

$(document).ready(function () {

    isLoggedIn(function (user) {
        if (user === null) {
            $('#removeRecipe').hide();
        }
    })

    $('#removeRecipe').click(function () {
        var recipeId = $('#hfRecipeId').val();

        baseUrl = window.mblRestHost;
        var url = baseUrl + 'DeleteRecipe';

        $.ajax(
            url, {
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                data: { recipeId: recipeId },
                success: function (response) {
                    window.location = 'Recipes.aspx';
                },
                error: function (xhr, status, error) {
                    console.log(error);
                }
            });
    })
});