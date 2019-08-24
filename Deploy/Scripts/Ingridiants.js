var ingridiantsContainer;

var IngridiantsApi = function () {

    var ingridiantsArr = new Array();
    var fractures = new Array();

    this.SelectedIngrediant;

    this.Length = function () {
        return ingridiantsArr.length;
    };

    this.getIngridiants = function (prefix) {
        var list = $('#ingridiantsList');

        if (prefix.length < 3) {
            list.text('').hide();
            return;
        }

        var url = $('#apiUrl').val() + '/api/foods';
        $.get(url, { searchQuery: prefix }, function (response) {
            if (response.results) {
                list.text('');
                $.each(response.results, function (index, item) {
                    var div = $('<div/>', {
                        class: 'ingridiant-list-item'
                    }).appendTo(list);

                    $('<span/>', {
                        text: item.FoodName
                    }).appendTo(div);

                    $('<input/>', {
                        type: 'hidden',
                        value: item.FoodId
                    }).appendTo(div);
                });
                list.show();

                $('.ingridiant-list-item').click(function (event) {
                    var foodId = $(this).children('input').val();
                    var foodName = $(this).children('span').text();

                    $('#ingridiantName').val(foodName);
                    $('#ingridiantId').val(foodId);
                    list.hide();
                });
            }
        }, 'json');
    };

    this.Exists = function (ingridiant) {
        for (var i = 0; i < ingridiantsArr.length; i++) {
            if (ingridiantsArr[i].Id === ingridiant.Id)
                return true;
        }
        return false;
    };

    this.IndexOf = function (id) {
        for (var i = 0; i < ingridiantsArr.length; i++) {
            if (ingridiantsArr[i].Id === id)
                return i;
        }
        return -1;
    };

    this.Add = function (ingridiant) {

        //ingridiant.Id = ingridiantsArr.length - 1;
        ingridiantsArr.push(ingridiant);
    };

    this.Update = function (ingridiant, indx) {
        ingridiantsArr[indx].FoodId = ingridiant.FoodId;
        ingridiantsArr[indx].Quantity = ingridiant.Quantity;
        ingridiantsArr[indx].FractionDisplay = ingridiant.FractionDisplay;
        ingridiantsArr[indx].MeasurementUnitId = ingridiant.MeasurementUnitId;
        ingridiantsArr[indx].MeasureUnitName = ingridiant.MeasureUnitName;
        ingridiantsArr[indx].Remarks = ingridiant.Remarks;
        ingridiantsArr[indx].FoodName = ingridiant.FoodName;
        ingridiantsArr[indx].DisplayIngredient = ingridiant.DisplayIngredient;
    };

    this.Delete = function (index) {
        ingridiantsArr.splice(index, 1);
    };

    this.GetItem = function (index) {
        return ingridiantsArr[index];
    };

    this.GetList = function () {
        return ingridiantsArr;
    };

    this.SetList = function (arr) {
        for (var i = 0; i < arr.length; i++) {

            var ingrediant = new Ingridiant();

            ingrediant.IngredientId = arr[i].IngredientId;
            ingrediant.FoodId = arr[i].FoodId;
            ingrediant.FoodName = arr[i].FoodName;
            ingrediant.MeasurementUnitId = arr[i].MeasurementUnitId;
            ingrediant.MeasureUnitName = arr[i].MeasureUnitName;
            ingrediant.Quantity = arr[i].Quantity;
            ingrediant.FractionDisplay = arr[i].FractionDisplay;
            ingrediant.Remarks = arr[i].Remarks;
            ingrediant.q = parseInt(arr[i].Quantity);
            ingrediant.f = arr[i].Quantity - ingrediant.q;
            this.Add(ingrediant);
        }
    };
};

var Ingridiant = function () {

    this.IngredientId;
    this.RecipeId;
    this.FoodId;
    this.FoodName;
    this.MeasurementUnitId;
    this.MeasurementUnitName;
    this.Quantity;
    this.Remarks;
    this.CompleteValue;
    this.FractionValue;
    this.FractionDisplay;
    this.DisplayIngredient;
    this.DecimalSeperator;

    this.q;
    this.f;

    this.IsValid = function () {
        return this.Quantity !== '' && this.FoodId !== '';
        //return (this.Quantity !== '' || this.FractionValue !== '') && this.FoodId !== '';
    };
};

var ingridiantsApi;
var recipeId;

$(document).ready(function () {

    InitIngediantsControl();

    var savedIngridiants = $('#hfIngridiants').val();
    if (savedIngridiants !== '') {
        ShowSavedListIngridiant(JSON.parse(savedIngridiants));
    }
    recipeId = $('#hfRecipeId').val() === '' ? -1 : $('#hfRecipeId').val();

    $('#updateIngridiant').hide();

    $('#ingridiantName').keyup(function () {
        var prefix = $('#ingridiantName').val();
        ingridiantsApi.getIngridiants(prefix);
    });

    $('#addIngridiant').mousedown(function () {
        $(this).css('background-image', 'url("Images/btn_AddProduct_down.png")');
    });

    $('#addIngridiant').mouseup(function () {
        $(this).css('background-image', 'url("Images/btn_AddProduct_over.png")');
    });

    $('#addIngridiant').mouseout(function () {
        $(this).css('background-image', 'url("Images/btn_AddProduct_up.png")');
    });

    $('#addIngridiant').mouseover(function () {
        $(this).css('background-image', 'url("Images/btn_AddProduct_over.png")');
    });

    $('#addIngridiant, #updateIngridiant').click(function () {

        var mode = $('#mode').val();
        var ingridiant;
        switch (mode) {
            case 'update': {
                var index = $('#selectedIndex').val();
                ingridiant = ingridiantsApi.GetItem(index);
                break;
            }
            default:
                ingridiant = new Ingridiant();
                ingridiant.IngredientId = $('#ingridiantId').val();
                break
        }

        ingridiant.RecipeId = recipeId;
        ingridiant.q = parseFloat($('#txtQuantity').val());
        ingridiant.f = $('#' + fractionClientId + ' option:selected').val() ? parseFloat($('#' + fractionClientId + ' option:selected').val().replace(',', '.')) : 0;
        ingridiant.Quantity = ingridiant.q + ingridiant.f;
        ingridiant.FractionDisplay = $('#' + fractionClientId + ' option:selected').text();
        ingridiant.MeasureUnitName = $('#' + unitClientId + ' option:selected').text();
        ingridiant.MeasurementUnitId = $('#' + unitClientId).val();
        ingridiant.FoodId = $('#ingridiantId').val();
        ingridiant.FoodName = $('#ingridiantName').val();
        ingridiant.Remarks = $('#' + foodRemarkClientId).val();

        if (!ingridiant.IsValid()) return;

        switch (mode) {
            case 'update': {
                ingridiantsApi.Update(ingridiant, index);
                break;
            }
            default:
                ingridiantsApi.Add(ingridiant);
                break
        }

        ShowList();

        $('#txtQuantity').val('');
        $('#' + fractionClientId).val('');
        $('#ingridiantName').val('');
        $('#' + foodRemarkClientId).val('');

        $('#addIngridiant').show();
        $('#updateIngridiant').hide();

        $('#mode').val('');
    });

    $('#updateIngridiant').mousedown(function () {
        $(this).css('background-image', 'url("Images/btn_EditProduct_down.png")');
    });

    $('#updateIngridiant').mouseup(function () {
        $(this).css('background-image', 'url("Images/btn_EditProduct_over.png")');
    });

    $('#updateIngridiant').mouseout(function () {
        $(this).css('background-image', 'url("Images/btn_EditProduct_up.png")');
    });

    $('#updateIngridiant').mouseover(function () {
        $(this).css('background-image', 'url("Images/btn_EditProduct_over.png")');
    });
});

function ShowList() {

    var ingredientsContainer = $('#ingredientsContainer');
    ingredientsContainer.html('');

    for (var i = 0; i < ingridiantsApi.Length(); i++) {

        var ingridiant = ingridiantsApi.GetItem(i);

        ingridiant.DisplayIngredient =
            ingridiant.FractionDisplay +
            ingridiant.q + ' ' +
            ingridiant.MeasureUnitName + ' ' +
            ingridiant.FoodName;

        var div = $('<div/>').addClass('row').attr('data-id', i);
        var spanEdit = $('<a />').html('עדכן').addClass('listBtn').addClass('updateBtn').addClass('modifyBtn').addClass('col').attr('data-id', i);
        div.append(spanEdit);
        var spanDelete = $('<a />').html('מחק').addClass('listBtn').addClass('deleteBtn').addClass('deleteBtn').addClass('col').attr('data-id', i);
        div.append(spanDelete);
        var spanDisplayIngredient = $('<span />').html(ingridiant.DisplayIngredient).addClass('floatRight').addClass('col');
        div.append(spanDisplayIngredient);
        var spanFoodRemark = $('<span />').html(ingridiant.Remarks).addClass('floatRight');
        div.append(spanFoodRemark);

        ingredientsContainer.append(div);
    }

    var listAsJSON = JSON.stringify(ingridiantsApi.GetList());
    $('#hfIngridiants').val(listAsJSON);
    initEvents();
}

function InitIngediantsControl() {
    if (!ingridiantsApi) {
        ingridiantsApi = new IngridiantsApi();
    }
}

function ShowSavedListIngridiant(arr) {
    if (!ingridiantsApi) {
        ingridiantsApi = new IngridiantsApi();
    }
    ingridiantsApi.SetList(arr);
    ShowList();
}

function initEvents() {

    $('.modifyBtn').click(function () {

        var index = $(this).attr('data-id');
        var ingridiant = ingridiantsApi.GetItem(index);

        $('#mode').val('update');
        $('#selectedIndex').val(index);

        $('#txtQuantity').val(ingridiant.q);
        $('#' + fractionClientId).val(ingridiant.f);
        $('#ingridiantName').val(ingridiant.FoodName);
        $('#' + foodRemarkClientId).val(ingridiant.Remarks);
        $('#' + unitClientId).val(ingridiant.MeasurementUnitId);
        $('#ingridiantId').val(ingridiant.FoodId);

        $('#addIngridiant').hide();
        $('#updateIngridiant').show();
    });

    $('.deleteBtn').click(function () {
        var index = $(this).attr('data-id');
        ingridiantsApi.Delete(index);
        $('.row[data-id=' + index + ']').hide();

        var listAsJSON = JSON.stringify(ingridiantsApi.GetList());
        $('#hfIngridiants').val(listAsJSON);

        ShowList();
    });
}

function OnClientItemSelected(behaviour, args) {
    var value = args._value.Second;
    $('#' + hfSelectedIngridiantClientId).val(value);
}

function OnClientShowing(behaviour, e) {
    var ResultsDiv = behaviour.get_completionList();

    for (var i = 0; i < ResultsDiv.childNodes.length; i++) {

        var item = ResultsDiv.childNodes[i];
        var text = item._value.First;
        item.innerText = text;
    }
}