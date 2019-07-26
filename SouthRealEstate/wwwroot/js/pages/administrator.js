
var residentalImages = [];
var residentalImagesIds = [1];

$(document).ready(function () {
    //initiate table
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

    //get cities
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

    //delete
    $(document).on("click", ".property-residental-delete", function () {
        var propertyId = $(this).attr('row_id').split('row_')[1];
        deleteResidentalProperty(propertyId);
    });
    
    //save
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

        debugger;
        var formData = new FormData();
        formData.append('residentalPropertyDTO', JSON.stringify(data));
        $.each(residentalImages, function (key, value) {
            formData.append('file', value.Image);
        });

        var urlUploadImages = `/api/properties/residental`;
        $.ajax({
            url: urlUploadImages,
            type: 'POST',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
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

    //upload images
    appendImages(residentalImagesIds);

    $(document).on("click", "#btn__more_images", function () {
        var lastImageId = residentalImagesIds[residentalImagesIds.length - 1];
        var newImageId = lastImageId+1;
        residentalImagesIds.push(newImageId);
        appendImages([newImageId]);
    });
});

function appendImages(idsArray) {
    $.each(idsArray, function (index, id) {
        let html = propertyAddImageTemplate(id);
        $('#property_images_container').append(html);

        //action bindings
        addUploadImageBinding(id);
    }); 
}

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

/* image uploads*/
function addUploadImageBinding(id) {
    $(document).on("change", `#uploadBtn_${id}`, function () {
        debugger;
        if (this.files.length > 0) {
            var newImage = {
                Id : id,
                Image : this.files[0]
            }
            residentalImages.push(newImage);
            $(`#uploadFile_${id}`).val(this.files[0].name);
        }
        else {
            residentalImages = residentalImages.filter((x) => x.Id !== id);
            $(`#uploadFile_${id}`).val('');
        }
    });
}

function uploadimage(formData, url) {
    $.ajax({
        url: url,
        type: 'POST',
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            console.log(data);
        }
    });
}

const propertyAddImageTemplate = (id) =>
    `<div class="mdl-textfield mdl-js-textfield mdl-textfield--file">
        <input class="mdl-textfield__input" placeholder="Image" type="text" id="uploadFile_${id}" readonly />
        <div class="mdl-button mdl-button--primary mdl-button--icon mdl-button--file">
            <i class="material-icons">attach_file</i><input type="file" id="uploadBtn_${id}">
        </div>
    </div>`;