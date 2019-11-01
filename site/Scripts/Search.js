$(document).ready(function () {

    registerToLoginNotifications((loginNotification) => {
        if (loginNotification.loggedIn) {
            $('.on-login').show();
        } else {
            $('.on-login').hide();
        }
    });

    var searchValue;
    var searchText;
    var searchCategory;

    var input = $('.search-value');
    //var select = $('#header div.search-box select');
    //var searchBtn = $('.search-button');

    $('.on-login').hide();

    input.autocomplete({
        classes: {
            'ui-autocomplete': 'search-results'
        },
        source: function (request, response) {
            var data = { method: 'SearchValues', term: request.term };
            $.post('ASHX/Handler.ashx', data, (result) => {
                if (result !== '') {
                    var filteredData = $.parseJSON(result);
                    response(filteredData);
                }
            });
        },
        minLength: 3,
        select: (event, ui) => {
            searchValue = ui.item.value;
            searchText = ui.item.label;
            window.location = 'RecipeDetails.aspx?RecipeId=' + ui.item.value;
        },
        search: (event, ui) => {
            //searchCategory = select.val();
        },
        open: () => {
            //searchCategory = select.val();
            var text = input.val();
            $('ul.ui-autocomplete li div').each(function () {
                var listText = $(this).html();
                var newListText = listText.replace(text, '<span style="color: orange"><b>' + text + '</b></span>');
                $(this).html(newListText);
            });
        }
    });

    //searchBtn.click(() => {

    //    searchCategory = select.val();
    //    var searchValue = input.val();

    //    switch (searchCategory) {
    //        case '0':
    //            //if (category == 0)
    //            //    window.location = 'RecipeDetails.aspx?RecipeId=' + value;
    //            //if (category == 1)
    //            //    window.location = 'MenuDetails.aspx?menuId=' + value;
    //            break;
    //        case '1':
    //            if (searchValue !== '')
    //                window.location = 'Recipes.aspx?page=1&orderby=LastUpdate&disp=BySearchSimple&term=' + searchValue + '&category=' + searchCategory;
    //            else
    //                window.location = 'Recipes.aspx?page=1&orderby=LastUpdate&disp=All';
    //            break;
    //        case '2':
    //            window.location = 'Recipes.aspx?page=1&orderby=LastUpdate&disp=MyRecipes&term=' + searchValue + '&category=' + searchCategory;
    //            break;
    //        case '3':
    //            window.location = 'Recipes.aspx?page=1&orderby=LastUpdate&disp=MyFavoriteRecipes&term=' + searchValue + '&category=' + searchCategory;
    //            break;
    //        case '4':
    //            if (searchValue !== '')
    //                window.location = 'Menus.aspx?page=1&orderby=LastUpdate&disp=BySearchSimple&term=' + searchValue + '&category=' + searchCategory;
    //            else
    //                window.location = 'Menus.aspx?page=1&orderby=LastUpdate&disp=All';
    //            break;
    //        case '5':
    //            window.location = 'Menus.aspx?page=1&orderby=LastUpdate&disp=MyRecipes&term=' + searchValue + '&category=' + searchCategory;
    //            break;
    //        case '6':
    //            window.location = 'Menus.aspx?page=1&orderby=LastUpdate&disp=MyFavoriteRecipes&term=' + searchValue + '&category=' + searchCategory;
    //            break;
    //    }
    //});

    //SetSearchOptions(options);

    //select.change(function () {
    //    searchCategory = $(this).val();
    //    var option = $('#header div.search-box select option:selected');
    //    var requireLogin = option.hasClass('requireLogin');
    //    if (requireLogin) {
    //        var data = { method: 'IsLoggedIn' };
    //        $.post('ASHX/Handler.ashx', data, function (data) {
    //            if (data === '') {
    //                OpenLoginDialog();
    //            }
    //        });
    //    }

    //    $('#header div.search-box input[type=text]').val('');
    //});
});

function SetSearchOptions(options) {
    var select = $('#header div.search-box select');
    select.html('');

    options =
        [
            { "value": 1, "text": "מתכונים" },
            { "value": 2, "text": "המתכונים שלי", "class": "requireLogin" },
            { "value": 3, "text": "המתכונים המועדפים שלי", "class": "requireLogin" },
            { "value": 4, "text": "תפריטים" },
            { "value": 5, "text": "התפריטים שלי", "class": "requireLogin" },
            { "value": 6, "text": "התפריטים המועדפים שלי", "class": "requireLogin" }
        ];

    for (var i = 0; i < options.length; i++) {
        select
            .append($('<option/>')
                .text(options[i].text)
                .val(options[i].value)
                .addClass(options[i].class));
    }
}

function SetSearchOption(selected) {
    $('#header div.search-box select').val(selected);
}

function ResetSearch() {
    SetSearchOption(1);
    $('#header div.search-box input[type=text]').val('');
}