$(document).ready(function () {
    var table = $('#tbl__residental_properties').DataTable({
        ajax: "/api/properties/residental/datatable",
        "columns": [
            { "data": "Title" },
            { "data": "Description" },
            { "data": "Address" },
            //{ "data": "CityId" },
            { "data": "SizeMeters" },
            { "data": "BadRoomsCount" },
            { "data": "BathRoomsCount" },
            { "data": "Price" },
            { "data": "IsFeatured" },
            
            {
                data: "actions",
                render: function (data, type, row) {
                    return `<a class=\'property-residental-delete\'> Delete </a>
                            <a class=\'property-residental-edit\'> Edit </a>
                            <a class=\'property-residental-set-featured\'> Set Featured </a>`;
                },
                className: "dt-body-center"
            }
        ]
    });


    $(document).on("click", ".property-residental-delete", function () {
        debugger;
        var propertyId = $(this).parent().parent().attr('id').split('row_')[1];
        deleteResidentalProperty(propertyId);
    });


    $("#btn_addFeatureIgnore").click(function () {
        var featureNameInt = $('#dd_features').val();
        var ignoredItem = $('#tb_ignored_item').val().replace(/\s*$/, "");
        var clientIds = $('#dd_clients').val();

        var errText = '';
        if (ignoredItem === '') {
            errText = 'Please enter a value to ignored item';
        }
        else if (HasIlegalCharsWithOutSlash(ignoredItem)) {
            errText = "Use of ilegal characters ('<', '>', '/', ''')";
        }
        else if (clientIds == null) {
            errText = 'No clients choose';
        }

        if (errText != '') {
            toastr.error(errText);
            return;
        }

        var data = {
            "featureNameInt": featureNameInt,
            "ignoredItem": ignoredItem,
            "_clients": clientIds
        };
        $.post('/FeaturesManager/AddFeatureIgnore', data, function (data) {
            toastr.success(data);
            $('#dd_clients option').prop('selected', false).trigger('chosen:updated');
            $('#dd_features option').prop('selected', false).trigger('chosen:updated');
            $('#tb_ignored_item').val('');
            table.ajax.reload();
        })
            .fail(function (data) {
                toastr.error(data.statusText);
                table.ajax.reload();
            });
    });

});

function deleteResidentalProperty(propertyId) {

    $.ajax({
        url: `/api/properties/residental/${propertyId}`,
        method: 'DELETE',
        dataType: 'json',
        success: function (data) {
            $('#tbl__residental_properties').DataTable().ajax.reload();
            toastr.success("Success");
        },
        error: function (request, errorType, errorMessage) {
            console.log('Error: ' + errorType + ' with message: ' + errorMessage);
            toastr.error("error");
        },
        beforeSend: function () {
            //$.blockUI({ message: 'Please Wait...', overlayCSS: { backgroundColor: '#fff' } });
        },
        complete: function () {
            //$.unblockUI({ overlayCSS: { backgroundColor: '#00f' } });
        }
    });
}