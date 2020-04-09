
$(document).ready(function () {
    $(document).on("click", ".single-featured-property .property-thumb", function () {        
        window.location = `/properties/singleproperty?id=${this.parentNode.getAttribute('name')}`;
    });

    const featuredPropertyTemplate = ({ Id, Title, Description, Address, SizeMeters, BadRoomsCount, BathRoomsCount, Price, IsNew, PropertyImages }) =>
        `<div class="col-12 col-md-6 col-xl-4" >
        <div class="single-featured-property mb-50 wow fadeInUp" data-wow-delay="100ms" name="${Id}">
            <!-- Property Thumbnail -->
            <div class="property-thumb">
                <img src="/img/uploads/${PropertyImages[0]}" alt="">
                <div class="tag">
                    <span>For Sale</span>
                </div>
                <div class="list-price">
                    <p>$${Price}</p>
                </div>
            </div>
            <!-- Property Content -->
            <div class="property-content">
                <h5>${Title}</h5>
                <p class="location"><img src="/img/icons/location.png" alt="">${Address}</p>
                <p>${Description}</p>
                <div class="property-meta-data d-flex align-items-end justify-content-between">
                    ${IsNew ? `<div class="new-tag" >
                        <img src="/img/icons/new.png" alt="">
                    </div>` : ''}
                    <div class="bathroom">
                        <img src="/img/icons/garage.png" alt="">
                        <span>${BadRoomsCount}</span>
                    </div>
                    <div class="bathroom">
                        <img src="/img/icons/bathtub.png" alt="">
                        <span>${BathRoomsCount}</span>
                    </div>
                    <div class="space">
                        <img src="/img/icons/space.png" alt="">
                        <span>${SizeMeters}m</span>
                    </div>
                </div>
            </div>
            </div>
        </div>
    </div>`;


    $("#btn_search_property").click(function () {
        var freeSearch = $('#search_general').val();
        var searchCties = $('#search_cities').val();
        var searchPropertyType = $('#search_property_type').val();
        var searchBedroomsFrom = $('#search_bedrooms_from').val();
        var searchBedroomsTo = $('#search_bedrooms_to').val();
        var searchSizeFromTo = $('#search_size');
        var searchPriceFromTo = $('#search_price');

        var errText = '';
        errText = validateSearchPropertiesForm(freeSearch, searchCties, searchPropertyType, searchBedroomsFrom, searchBedroomsTo, searchSizeFromTo, searchPriceFromTo);

        if (errText !== '') {
            toastr.error(errText);
            return;
        }

        searchBedroomsFrom = searchBedroomsFrom === '0' ? null : parseInt(searchBedroomsFrom);
        searchBedroomsTo = searchBedroomsTo === '0' ? null : parseInt(searchBedroomsTo);
        searchPropertyType = searchPropertyType === '0' ? null : parseInt(searchPropertyType);
        var searchSizeFrom = parseInt(searchSizeFromTo.slider('values')[0]);
        var searchSizeTo = parseInt(searchSizeFromTo.slider('values')[1]);
        var searchPriceFrom = parseInt(searchPriceFromTo.slider('values')[0]);
        var searchPriceTo = parseInt(searchPriceFromTo.slider('values')[1]);
        var searchCtiesÍd = searchCties === '0' ? null : parseInt(searchCties);
        
        var data = {
            FreeSearch: freeSearch,
            CityId: searchCtiesÍd,
            PropertyType: searchPropertyType,
            BadRoomsCountTo: searchBedroomsFrom,
            SearchBedroomsTo: searchBedroomsTo,
            SizeMetersFrom: searchSizeFrom,
            SizeMetersTo: searchSizeTo,
            PriceFrom: searchPriceFrom,
            PriceTo: searchPriceTo
        };

        $.ajax({
            url: `/api/properties/search`,
            type: 'POST',
            data: data,
            success: function (data) {
                //console.log(data);
                $('#properties_content').html(data.map(featuredPropertyTemplate).join(''));
            },
            error: function (request, errorType, errorMessage) {
                console.log('Error: ' + errorType + ' with message: ' + errorMessage);
                toastr.error("an error occurred, try again later");
            },
            beforeSend: function () {
                //$.blockUI({ message: 'Please Wait...', overlayCSS: { backgroundColor: '#fff' } });
            },
            complete: function () {
                //$.unblockUI({ overlayCSS: { backgroundColor: '#00f' } });
            }
        });
    });


    //get cities for search
    $.ajax({
        url: `/api/cities`,
        method: 'GET',
        dataType: 'json',
        success: function (cities) {
            //debugger;
            $.each(cities, function (index, city) {
                $('#search_cities').append($('<option></option>').val(city.Id).html(city.Name));
            });

            //$('#search_cities').niceSelect();
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

    function validateSearchPropertiesForm(search_general, search_cities, search_property_type, search_bedrooms_from, search_bedrooms_to, search_size, search_price) {
        var errText = '';
        //if (search_general === '' ||
        //    search_cities === '' ||
        //    search_property_type === '' ||
        //    search_bedrooms_from === '' ||
        //    search_bedrooms_to === '' ||
        //    search_size === '' ||
        //    search_price === '') {
        //    errText = 'Please fill all the missing fields';
        //}

        return errText;
    }

    // :: Slider Range
    $('.slider-range-size').each(function () {
        var min = jQuery(this).data('min');
        var max = jQuery(this).data('max');
        var unit = jQuery(this).data('unit');
        var value_min = jQuery(this).data('value-min');
        var value_max = jQuery(this).data('value-max');
        var t = $(this);
        $(this).slider({
            range: true,
            min: min,
            max: max,
            step: 1,
            values: [value_min, value_max],
            slide: function (event, ui) {
                var result = ui.values[0] + unit + ' - ' + ui.values[1] + unit;
                t.closest('.slider-range').find('.range').html(result);
            }
        });
    });

    $('.slider-range-price').each(function () {
        var min = jQuery(this).data('min');
        var max = jQuery(this).data('max');
        var unit = jQuery(this).data('unit');
        var value_min = jQuery(this).data('value-min');
        var value_max = jQuery(this).data('value-max');
        var t = $(this);
        $(this).slider({
            range: true,
            min: min,
            max: max,
            step: 10000,
            values: [value_min, value_max],
            slide: function (event, ui) {
                var result = ui.values[0] + unit + ' - ' + ui.values[1] + unit;
                t.closest('.slider-range').find('.range').html(result);
            }
        });
    });

    $("#btn_search_property").trigger("click");

});
