$(document).ready(function () {
    var table = $('#tbl__residental_properties').DataTable({
        responsive: true,
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
                width: "20%" ,
                data: "actions",
                render: function (data, type, row) {
                    console.log();
                    return `<div class=\'properties-tbl-actions-wrapper\'>
                                <button row_id=\'${row.DT_RowId}'\' class=\'property-residental-delete mdl-button mdl-js-button mdl-button--accent\'> Delete </button >
                                <button row_id=\'${row.DT_RowId}'\' class=\'property-residental-edit mdl-button mdl-js-button mdl-button--primary\'> Edit </button >
                            </div>`;
                },
                className: "dt-body-center"
            }
        ]
    });


    $.ajax({
        url: `/api/cities`,
        method: 'GET',
        dataType: 'json',
        success: function (cities) {
            $.each(cities, function (index, city) {
                $('#property_city_sb').append($('<option></option>').val(city.Id).html(city.Name));
            }); 
        },
        error: function (request, errorType, errorMessage) {
            console.log('Error: ' + errorType + ' with message: ' + errorMessage);
            toastr.error("error gettings cities list");
        },
        beforeSend: function () {
            //$.blockUI({ message: 'Please Wait...', overlayCSS: { backgroundColor: '#fff' } });
        },
        complete: function () {
            //$.unblockUI({ overlayCSS: { backgroundColor: '#00f' } });
        }
    });


    $(document).on("click", ".property-residental-delete", function () {
        var propertyId = $(this).attr('row_id').split('row_')[1];
        deleteResidentalProperty(propertyId);
    });


    $("#btn__add_property").click(function () {
        var property_title = $('#property_title').val();
        var property_desc = $('#property_desc').val();
        var property_add = $('#property_add').val();
        var property_city = $('#property_city_sb').val();
        var property_size = $('#property_size').val();
        var property_badrooms = $('#property_badrooms').val();
        var property_bathrooms = $('#property_bathrooms').val();
        var property_price = $('#property_price').val();
        var property_is_featured = $('#property_is_featured').val();

        var errText = '';
        errText = validateForm(property_title, property_desc, property_city, property_add, property_size,
            property_badrooms, property_bathrooms, property_price);

        if (errText !== '') {
            toastr.error(errText);
            return;
        }

        var data = {
            Title: property_title,
            Description: property_desc,
            Address: property_add,
            CityId: property_city,
            SizeMeters: property_size,
            BadRoomsCount: property_badrooms,
            BathRoomsCount: property_bathrooms,
            Price: property_price,
            IsFeatured: property_is_featured === 'on',
        };

        $.ajax({
            url: `/api/properties/residental`,
            method: 'POST',
            data: data,
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


function validateForm(property_title, property_desc, property_city, property_add, property_size,
    property_badrooms, property_bathrooms, property_pric) {
    var errText = '';
    if (property_title === '' ||
        property_desc === '' ||
        property_city === '' ||
        property_add === '' ||
        property_size === '' ||
        property_badrooms === '' ||
        property_bathrooms === '' ||
        property_price === '') {
        errText = 'Please fill all the missing fields';
    }

    return errText;
}