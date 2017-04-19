$(document).ready(function () {

    $('.input-group.date').datepicker({
        format: 'dd/mm/yyyy',
        language: 'pt-BR',
        autoclose: true
    });

    $('.mask-me').mask("00/00/0000", { placeholder: "__/__/____" });

    loadData();

    $("#SearchCityId").change(function () {
        loadRegions();
    })

    function loadRegions() {
        $("#SearchRegionId").empty();
        $.ajax({
            type: 'POST',
            url: 'Home/GetRegionByCity',
            dataType: 'json',
            data: { id: $("#SearchCityId").val() },
            success: function (result) {
                if (result.success) {
                    $.each(result.data, function (i, city) {
                        $("#SearchRegionId").append('<option value="' + city.id + '">' + city.name + '</option>')
                    });
                }
                else {
                    alert(result.error);
                }
            },
            error: function (ex) {
                alert('Failed to retrieve states.' + ex);
            }
        });
        return false;
    }

    $("#clearButton").click(function () {
        clear();
    })

    function clear() {
        $(".form-control").val('');
        loadRegions();
        loadData();
    }

    $("#searchButton").click(function () {
        loadData();
    })

    function loadData() {
        var body = $("#dataTable").find('tbody');
        body.empty();
        $.ajax({
            type: 'POST',
            url: 'Home/LoadData',
            dataType: 'json',
            data: {
                name: $("#SearchName").val(),
                gender: $("#SearchGenderId").val(),
                city: $("#SearchCityId").val(),
                region: $("#SearchRegionId").val(),
                inital: $("#SearchInitialDate").val(),
                final: $("#SearchFinalDate").val(),
                classification: $("#SearchClassificationId").val(),
                seller: $("#SearchUserId").val()
            },
            success: function (result) {
                if (result.success) {
                    $.each(result.data, function (i, customer) {
                        body
                            .append($('<tr>')
                                .append($('<td>')
                                    .append(customer.Classification)
                                )
                                .append($('<td>')
                                    .append(customer.Name)
                                )
                                .append($('<td>')
                                    .append(customer.Phone)
                                )
                                .append($('<td>')
                                    .append(customer.Gender)
                                )
                                .append($('<td>')
                                    .append(customer.City)
                                )
                                .append($('<td>')
                                    .append(customer.Region)
                                )
                                .append($('<td>')
                                    .append(customer.LastPurchase)
                                )
                                .append(customer.Seller != null?
                                    $('<td>').append(customer.Seller): ''
                                )
                            );
                    });
                }
                else {
                    alert(result.error);
                }
            },
            error: function (ex) {
                alert('Failed to retrieve states.' + ex);
            }
        });
        return false;
    }

});