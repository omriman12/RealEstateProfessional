const featuredPropertyTemplate = ({ Id, Title, Description, Address, SizeMeters, BadRoomsCount, BathRoomsCount, Price, IsNew, PropertyImage }) => 
    `<div class="col-12 col-md-6 col-xl-4" >
        <div class="single-featured-property mb-50 wow fadeInUp" data-wow-delay="100ms" id="${Id}">
            <!-- Property Thumbnail -->
            <div class="property-thumb">
                <img src="/img/uploads/${PropertyImage}" alt="">
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

const featuredAgentTemplate = ({ Id, Name, Phone, Email, Details, ImageName }) =>
    `<!-- Editor Content -->
    <div class="editor-content-area">
        <!-- Section Heading -->
        <div class="section-heading wow fadeInUp" data-wow-delay="250ms">
            <img src="/img/icons/prize.png" alt="">
            <h2>${Name}</h2>
            <p>Realtor</p>
        </div>
        <p class="wow fadeInUp" data-wow-delay="500ms">${Details}</p>
        <div class="address wow fadeInUp" data-wow-delay="750ms">
            <h6><img src="/img/icons/phone-call.png" alt=""> ${Phone}</h6>
            <h6><img src="/img/icons/envelope.png" alt=""> ${Email}</h6>
        </div>
        <div class="signature mt-50 wow fadeInUp" data-wow-delay="1000ms">
            <img src="/img/core-img/signature.png" alt="">
        </div>
    </div>

    <!-- Editor Thumbnail -->
    <div class="editor-thumbnail">
        <img src="/img/uploads/${ImageName}" alt="">
    </div>`;
    
$(document).ready(function () {
    $.ajax({
        url: '/api/properties/residental/featured',
        method: 'get',
        dataType: 'json',
        success: function (data) {
            $('#featured-properties-content').html(data.map(featuredPropertyTemplate).join(''));
        },
        error: function (request, errorType, errorMessage) {
            console.log(request.responseText);
        },
        beforeSend: function () {
            //$.blockUI({ message: 'Please Wait...', overlayCSS: { backgroundColor: '#fff' } });
        },
        complete: function () {
            //$.unblockUI({ overlayCSS: { backgroundColor: '#00f' } });
        }
    });

    $.ajax({
        url: '/api/agents',
        method: 'get',
        dataType: 'json',
        success: function (data) {
            let randomAgent = data[Math.floor(Math.random() * data.length)];
            $('#featured-agent-content').html(featuredAgentTemplate(randomAgent));
        },
        error: function (request, errorType, errorMessage) {
            console.log(request.responseText);
        },
        beforeSend: function () {
            //$.blockUI({ message: 'Please Wait...', overlayCSS: { backgroundColor: '#fff' } });
        },
        complete: function () {
            //$.unblockUI({ overlayCSS: { backgroundColor: '#00f' } });
        }
    });
});