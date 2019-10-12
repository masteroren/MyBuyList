$(document).ready(() => {

    var list = $('#ingridiantsList');
    list.hide();
    var ingridiantsContainer = $('.ingredients-container');

    var updateIngrediant = $('.update-ingrediant');
    var addIngrediant = $('.add-ingrediant');
    updateIngrediant.hide();

    var selectedIngrediant;

    var getIngridiants = (prefix) => {
        list.addClass('ingridiant-list');
        list.click((data) => {
            $('#ingridiantName').val(data.target.text);
            $('#ingridiantId').val(data.target.value);
            list.hide();
        });

        if (prefix.length < 3) {
            list.hide();
            return;
        }

        $.get("ASHX/Handler.ashx", { method: "GetIngrediants", searchQuery: prefix }, (response) => {
            if (response.results && response.results.length !== 0) {
                list.text('');

                $.each(response.results, (index, item) => {
                    list.append($('<option/>').text(item.FoodName).val(item.FoodId));
                });
                list.show();
            }
        }, 'json');
    };

    var editClicked = (e) => {
        console.log('edit clicked...', e.target.dataset);
        $('.input-section-item.quantity0 input').val(e.target.dataset.quantity0 === 0 ? '' : e.target.dataset.quantity0);
        $('.input-section-item.quantity1 select').val(e.target.dataset.quantity1);
        $('.input-section-item.unit select').val(e.target.dataset.unit);
        $('.input-section-item.name input').val(e.target.dataset.name);
        $('.input-section-item.remark input').val(e.target.dataset.howTo);
        updateIngrediant.show();
        addIngrediant.hide();

        selectedIngrediant = e.target.dataset.id;
    };

    var deleteClicked = (e) => {
        console.log('delete clicked...', e.target.dataset);
        var id = e.target.dataset.id;
        $(`.list-item-${id}`).remove();
    };

    $('.edit-ingrediant').click(editClicked);
    $('.delete-ingrediant').click(deleteClicked);

    updateIngrediant.click(() => {
        var quantity0 = $('.input-section-item.quantity0 input').val();
        var quantity1 = $('.input-section-item.quantity1 select option:selected').text();
        var quantity1val = $('.input-section-item.quantity1 select').val();
        var unit = $('.input-section-item.unit select option:selected').text();
        var unitId = $('.input-section-item.unit select').val();
        var name = $('.input-section-item.name input').val();
        var remark = $('.input-section-item.remark input').val();
        var displayName = `${quantity1}${quantity0 === '0' ? '' : quantity0} ${unit} ${name}`;
        $(`.list-item-${selectedIngrediant} .display-name`)[0].innerText = displayName;

        $(`.list-item-${selectedIngrediant} .edit-ingrediant`)[0].dataset.quantity0 = quantity0 === '0' ? '' : quantity0;
        $(`.list-item-${selectedIngrediant} .edit-ingrediant`)[0].dataset.quantity1 = quantity1val;
        $(`.list-item-${selectedIngrediant} .edit-ingrediant`)[0].dataset.unit = unitId;
        $(`.list-item-${selectedIngrediant} .edit-ingrediant`)[0].dataset.name = name;

        addIngrediant.show();
        updateIngrediant.hide();

        $('.input-section-item.quantity0 input').val('');
        $('.input-section-item.quantity1 select').val('');
        $('.input-section-item.unit select').val('0');
        $('.input-section-item.name input').val('');
        $('.input-section-item.remark input').val('');
    });

    addIngrediant.click(() => {
        addItem({
            id: $('.input-section-item.name select').val(),
            quantity0: $('.input-section-item.quantity0 input').val(),
            quantity1: $('.input-section-item.quantity1 select option:selected').text(),
            quantity1val: $('.input-section-item.quantity1 select').val(),
            unit: $('.input-section-item.unit select option:selected').text(),
            unitId: $('.input-section-item.unit select').val(),
            name: $('.input-section-item.name input').val(),
            remark: $('.input-section-item.remark input').val()
        });

        $('.input-section-item.quantity0 input').val('');
        $('.input-section-item.quantity1 select').val('');
        $('.input-section-item.unit select').val('0');
        $('.input-section-item.name input').val('');
        $('.input-section-item.remark input').val('');
    });

    var addItem = (data) => {
        if (data.name === '') { return; }

        var displayName = `${data.quantity1}${data.quantity0} ${data.unit} ${data.name}`;

        var div = $('<div/>').addClass(`list-item list-item-${data.nameId}`);
        var a1 = $('<a/>')
            .addClass('list-btn edit-ingrediant')
            .text('עריכה')
            .click(editClicked)
            .attr('data-id', data.id)
            .attr('data-quantity0', data.quantity0)
            .attr('data-quantity1', data.quantity1val)
            .attr('data-unit', data.unitId)
            .attr('data-name', data.name)
            .attr('data-howto', data.remark);
        var a2 = $('<a/>').addClass('list-btn delete-ingrediant').text('מחיקה').click(deleteClicked).attr('data-id', data.nameId);
        var span = $('<a/>').addClass('display-name').text(displayName);
        div.append(a1).append(a2).append(span);
        ingridiantsContainer.append(div);

        $.post('ASHX/Recipes.ashx', { method: 'AddIngredianToRecipe', ...data }, () => { });
    };

    $('#updateIngridiant').hide();

    $('#ingridiantName').keyup(function () {
        var prefix = $('#ingridiantName').val();
        getIngridiants(prefix);
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
                break;
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

function ingrediantSelected(source, eventArgs) {
    $('#IngridiantId').val(eventArgs.get_value());
}