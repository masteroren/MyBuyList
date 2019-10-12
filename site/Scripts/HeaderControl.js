$(function () {

    StartHeaderInterval();
});

function StartHeaderInterval() {
    var times = 1;
    var headerUpdateInterval = setInterval(function () {

        var recipes = $('#' + favRecipesNumClientId);

        $.post('ASHX/handler.ashx', { method: 'GetSelectedRecipes' }, function (data) {

            recipes.html('(' + data + ')');

            if (times == 2)
                clearInterval(headerUpdateInterval);

            times++;
        });
    }, 1000);
}



