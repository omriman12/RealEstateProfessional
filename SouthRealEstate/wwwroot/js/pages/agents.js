
var agentImage;

$(document).ready(function () {
    //initiate table
    var table = $('#tbl__agents').DataTable({
        responsive: true,
        ajax: "/api/agents/datatable",
        "columns": [
            { "data": "Name" },
            { "data": "Phone" },
            { "data": "Email" },
            { "data": "Details" },
            {
                width: "20%" ,
                data: "actions",
                render: function (data, type, row) {
                    console.log();
                    return `<div class=\'agents-tbl-actions-wrapper\'>
                                <button row_id=\'${row.DT_RowId}'\' class=\'agent-delete mdl-button mdl-js-button mdl-button--accent\'> Delete </button >
                            </div>`;
                    //<button row_id=\'${row.DT_RowId}'\' class=\'agent-edit mdl-button mdl-js-button mdl-button--primary\'> Edit </button >
                },
                className: "dt-body-center"
            }
        ]
    });
    
    //delete
    $(document).on("click", ".agent-delete", function () {
        var agentId = $(this).attr('row_id').split('row_')[1];
        deleteAgent(agentId);
    });
    
    //save
    $("#btn__add_agent").click(function () {
        var agent_name = $('#agent_name').val();
        var agent_phone = $('#agent_phone').val();
        var agent_email = $('#agent_email').val();
        var agent_Details = $('#agent_Details').val();

        var errText = '';
        errText = validateAgnetsForm(agent_name, agent_phone, agent_email, agent_Details);

        if (errText !== '') {
            toastr.error(errText);
            return;
        }

        var data = {
            Name: agent_name,
            Phone: agent_phone,
            Email: agent_email,
            Details: agent_Details
        };

        var formData = new FormData();
        formData.append('agentDTO', JSON.stringify(data));
        formData.append('file', agentImage);

        var urlAddAgent = `/api/agents`;
        $.ajax({
            url: urlAddAgent,
            type: 'POST',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                $('#tbl__agents').DataTable().ajax.reload();
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


function deleteAgent(agentId) {

    $.ajax({
        url: `/api/agents/${agentId}`,
        method: 'DELETE',
        dataType: 'json',
        success: function (data) {
            $('#tbl__agents').DataTable().ajax.reload();
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

function validateAgnetsForm(agent_name, agent_phone, agent_email, agent_Details) {
    var errText = '';
    if (agent_name === '' ||
        agent_phone === '' ||
        agent_email === '' ||
        agent_Details === '') {
        errText = 'Please fill all the missing fields';
    }

    return errText;
}
/* image uploads*/
function addUploadImageBinding(id) {
    $(document).on("change", `#uploadAgentImageBtn`, function () {
        if (this.files.length > 0) {
            agentImage = this.files[0];
            $(`#uploadFile`).val($(`#uploadAgentImage`).val(this.files[0].name));
            
        }
        else {
            $(`#uploadFile`).val('');
            agentImage = null;
        }
    });
}
