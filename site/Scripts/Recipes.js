$(document).ready(function () {
    $('#' + lnkNewRecipeClientId).click(function () {
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
    });

    $('#' + ButtonRecipesRefreshClientID).hide();

    baseUrl = window.mblRestHost;
});

var userId = null;

getRecipes = () => {
    
};

function addRemoveRecipe(obj, recipeId) {

    var addRemoveLink = $(obj);

    var url = baseUrl + 'ShoppingList/removeRecipe';

    getShoppingList();
    getRecipes();

    if (addRemoveLink.hasClass('add-recipe')) {
        addRemoveLink.html('הסר מרשימת הקניות');
        addRemoveLink.css('color', 'Red');
        addRemoveLink.switchClass('add-recipe', 'remove-recipe');

        addRecipe({
            data: {
                recipeId: recipeId
            }
        });

        return;
    }

    if (addRemoveLink.hasClass('remove-recipe')) {
        addRemoveLink.html('הוסף לרשימת הקניות');
        addRemoveLink.css('color', '#656565');
        addRemoveLink.switchClass('remove-recipe', 'add-recipe');

        removeRecipe({
            data: {
                recipeId: recipeId
            }
        })
    }
}

function UnSelectRecipe(recipeId, recipeName) {

    $('#' + RecipeIdClientID).val(recipeId);
    $('#' + RecipeNameClientID).val(recipeName);
    var btn = $('#' + ButtonRecipesRefreshClientID);
    btn.click();
}