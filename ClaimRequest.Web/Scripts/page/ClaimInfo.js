$('.datepicker').datepicker({
    autoclose: true,
    format: 'mm/dd/yyyy',
    todayHighlight: true
})

//Timepicker
$('.timepicker').timepicker({
    showInputs: false
})

$("#submit_claim_info").click(function (e) {
    e.preventDefault();
    $("#form_add_claim_info").submit();
});
$("#form_add_claim_info").submit(function (e) {
    e.preventDefault();
    debugger
    if ($("#form_add_claim_info").valid()) {
        var data = {
            ClaimInfoId: $("#ClaimInfoId").val(),
            ClaimInfoDate: $("#datepicker").val(),
            ClaimInfoFrom: $("#From").val(),
            ClaimInfoTo: $("#To").val(),
            TotalHour: $("#TotalHours").val(),
            Remark: $("#Remark").val()
        }
        $.ajax({
            type: "POST",
            url: "/Claims/ValidateModal",
            data: data,
            dataType: "html",
            success: function (response) {

                var result = {}
                try {
                    result = JSON.parse(response);

                } catch (e) {

                }
                if (result.IsSuccess) {
                    var content = "<tr index='" + result.data.ClaimInfoId + "'>"
                        + "<td>" + result.data.ClaimInfoDate + "</td>"
                        + "<td>" + result.data.Day + "</td>"
                        + "<td>" + result.data.ClaimInfoFrom + "</td>"
                        + "<td>" + result.data.ClaimInfoTo + "</td>"
                        + "<td>" + result.data.TotalHour.toFixed(2) + "</td>"
                        + "<td>" + (result.data.Remark == null ? "" : result.data.Remark) + "</td>"
                        + "<td style='padding: 5px;'>"
                        + "<button type='button' class='btn btn-info btn-sm '>Edit</button> | "
                        + "<button type='button' class='btn btn-danger btn-sm remove_claim_infor'>Remove</button>"
                        + "</td>"
                        + "</tr>";
                    if (result.data.IsNew) {
                        $("#table_list_claim_info tbody").append(content);

                    } else {
                        $("#table_list_claim_info tbody tr[index='" + result.data.ClaimInfoId + "']").replaceWith(content);
                    }
                    $("#modal-default").modal("hide")
                    CalculateHours();
                } else {
                    $("#modal-default").html(response)
                }
            }
        });
    } else {
        alert("Somethings when wrong, check the message for detail !!")
    }

});

$("#form_add_claim_info").validate({
    rules: {
        //Remarks: "required",
        TotalHour: {
            required: true,
            number: true
        },
        From: "required",
        To: {
            required: true
        },
        date: "required"
    },
    messages: {
        //Remarks: "This field is required",
        TotalHours: {
            required: "Please specify value for this field.",
            number: "This field must be number"
        }
    }
    ,
    onclick: false,
    onfocusout: false
    ,
    errorPlacement: function (error, element) {
        if (element[0].name == 'From' || element[0].name == 'To' || element[0].name == 'date') {
            error.insertAfter(element.parent());
        } else {
            error.insertAfter(element);
        }
    }
})
