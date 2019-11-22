const singleAgentTemplate = ({ Id, Name, Phone, Email, Details, ImageName}) => 
    `<div class="col-12 col-sm-6 col-lg-4">
        <div class="single-team-member mb-100 wow fadeInUp" data-wow-delay="250ms">
            <!-- Team Member Thumb -->
            <div class="team-member-thumb">
                <img src="/img/uploads/${ImageName}" alt="">
            </div>
            <!-- Team Member Info -->
            <div class="team-member-info">
                <div class="section-heading">
                    <img src="/img/icons/prize.png" alt="">
                    <h2>${Name}</h2>
                    <p>Realtor</p>
                </div>
                <div class="address">
                    <h6><img src="/img/icons/phone-call.png" alt=""> ${Phone}</h6>
                    <h6><img src="/img/icons/envelope.png" alt=""> ${Email}</h6>
                </div>
            </div>
        </div>
    </div>`;
    
$(document).ready(function () {
    $.ajax({
        url: '/api/agents',
        method: 'get',
        dataType: 'json',
        success: function (data) {
            $('#agents-content').html(data.map(singleAgentTemplate).join(''));
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