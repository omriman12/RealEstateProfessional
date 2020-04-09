
$(document).ready(function () {
    const singlePropertyTemplate = ({ Id, Title, Description, Address, SizeMeters, BadRoomsCount, BathRoomsCount, Price, IsNew, PropertyImages }) =>
        `<!-- Price -->
            <div class="list-price">
                <p>${Price}</p>
            </div>
            <h5>${Title}</h5>
            <p class="location"><img src="/img/icons/location.png" alt="">${Address}</p>
            <p>${Description}</p>
            <!-- Meta -->
            <div class="property-meta-data d-flex align-items-end">
                ${IsNew ? `<div class="new-tag" >
                    <img src="/img/icons/new.png" alt="">
                </div>` : ''}
                <div class="bathroom">
                    <img src="/img/icons/bathtub.png" alt="">
                    <span>${BadRoomsCount}</span>
                </div>
                <div class="garage">
                    <img src="/img/icons/garage.png" alt="">
                    <span>${BadRoomsCount}</span>
                </div>
                <div class="space">
                    <img src="/img/icons/space.png" alt="">
                    <span>${SizeMeters}m</span>
                </div>
            </div>
            <!-- Core Features -->
            <ul class="listings-core-features d-flex align-items-center">
                <li><i class="fa fa-check" aria-hidden="true"></i> Gated Community</li>
                <li><i class="fa fa-check" aria-hidden="true"></i> Automatic Sprinklers</li>
                <li><i class="fa fa-check" aria-hidden="true"></i> Fireplace</li>
                <li><i class="fa fa-check" aria-hidden="true"></i> Window Shutters</li>
                <li><i class="fa fa-check" aria-hidden="true"></i> Ocean Views</li>
                <li><i class="fa fa-check" aria-hidden="true"></i> Heated Floors</li>
                <li><i class="fa fa-check" aria-hidden="true"></i> Heated Floors</li>
                <li><i class="fa fa-check" aria-hidden="true"></i> Private Patio</li>
                <li><i class="fa fa-check" aria-hidden="true"></i> Window Shutters</li>
                <li><i class="fa fa-check" aria-hidden="true"></i> Fireplace</li>
                <li><i class="fa fa-check" aria-hidden="true"></i> Beach Access</li>
                <li><i class="fa fa-check" aria-hidden="true"></i> Rooftop Terrace</li>
            </ul>`;


    const singlePropertyImagesTemplate = (PropertyImages) => {
        return PropertyImages.map(x => `<img src="/img/uploads/${x}" alt="">`);
    };


    var urlParams = new URLSearchParams(window.location.search);
    let propertyId = urlParams.get('id');

    $.ajax({
        url: `/api/properties/residental/${propertyId}`,
        type: 'GET',
        success: function (data) {
            console.log(data);
            $('#sp__single_prop_content').html(singlePropertyTemplate(data));

            $('#sp__single_prop_images').html(singlePropertyImagesTemplate(data.PropertyImages));


            $('.featured-properties-slides, .single-listings-sliders').owlCarousel({
                items: 1,
                margin: 0,
                loop: true,
                autoplay: true,
                autoplayTimeout: 5000,
                smartSpeed: 1000,
                nav: true,
                navText: ['<i class="ti-angle-left"></i>', '<i class="ti-angle-right"></i>']
            });
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
