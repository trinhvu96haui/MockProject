var Action = "";
var dayOfWeek = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Satuday"]
$("#form_add_claim_conntent").on("click", ".remove_claim_infor", function () {
    $(this).parents("tr").remove();
    CalculateHours();
});
$("#form_add_claim_conntent").on("click", ".edit_claim_infor", function () {
    var tr = $(this).parents("tr");
    var data = {
        ClaimInfoId: $(tr[0]).attr("index"),
        ClaimInfoDate: $(tr[0]).find("td:eq(0)").html(),
        ClaimInfoFrom: $(tr[0]).find("td:eq(2)").html(),
        ClaimInfoTo: $(tr[0]).find("td:eq(3)").html(),
        TotalHour: $(tr[0]).find("td:eq(4)").html(),
        Remark: $(tr[0]).find("td:eq(5)").html()
    }
    debugger
    $.ajax({
        type: "POST",
        url: "/Claims/GetModal",
        data: data,
        dataType: "html",
        success: function (response) {
            $("#modal-default").html(response)
            $("#modal-default").modal("show")
        }
    });
});

$("#form_add_claim_conntent").on("click", "#btn_add_claim_modal", function (e) {
    e.preventDefault();
    $.ajax({
        type: "POST",
        url: "/Claims/GetModalAddInfor",
        //data: "",
        dataType: "html",
        success: function (response) {
            $("#modal-default").html(response)
            $("#modal-default").modal("show")
        }
    });
});
$("#form_add_claim_conntent").on("submit", "#form_add_claim", function (e) {

    e.preventDefault();
    if ($("#form_add_claim").valid()) {
        var array = [];
        $.each($("tbody tr"), function (indexInArray, valueOfElement) {
            array.push({
                ClaimId: $("#ClaimId").val(),
                ClaimInfoId: $(this).attr("index"),
                Date: $(valueOfElement).find("td:eq(0)").html(),
                From: $(valueOfElement).find("td:eq(2)").html(),
                To: $(valueOfElement).find("td:eq(3)").html(),
                TotalNumHour: $(valueOfElement).find("td:eq(4)").html(),
                Remark: $(valueOfElement).find("td:eq(5)").html()
            });
        });
        var data = {
            Claim: {
                ClaimId: $("#ClaimId").val(),
                StaffId: User.id,
                ProjectId: $("#Claim_ProjectId").val(),
                claimInfos: array,
                AuditTrail: $("#AuditTrail").val(),
                Remarks: $("#Claim_Remarks").val(),
                TotalWorkingHours: $("#total_Working_Hours").val(),
                Status: $("#ClaimStatus").val(),
            },
            ClaimAction: Action
        }
        debugger
        $.ajax({
            type: "Post",
            url: "/Claims/Save",
            data: data,
            dataType: "html",
            success: function (response) {
                debugger
                var result = {}
                try {
                    result = JSON.parse(response);

                } catch (e) {

                }
                if (result.IsSuccess) {
                    alert("Successfully")
                    window.location.replace("/Claim/Detail?claimId=" + result.ClaimId)

                    //toastr.success(
                    //    'Successfully!',
                    //    '',
                    //    {
                    //        timeOut: 600,
                    //        fadeOut: 600,
                    //        onHidden: function () {
                    //            window.location.replace("/Claim/Detail?claimId=" + result.ClaimId)
                    //        }
                    //    }
                    //);

                } else {
                    $("#form_add_claim_conntent").html(response);
                    CalculateHours();
                    $("#RoleInProject").val($("#Claim_ProjectId").find("option:selected").attr("roleP"));
                    $("#ProjectDuration").val($("#Claim_ProjectId").find("option:selected").attr("duration"));
                    alert("Somethings went wrong, check the error message for detail !!")
                }
            }
        });
    } else {
        alert("Somethings when wrong, check the message for detail !!")
    }
});
$("#form_add_claim_conntent").on("click", "#Claim_submit", function (e) {
    debugger
    e.preventDefault();
    if ($("tbody tr").length == 0) {
        alert("Please insert some claim information !!")
    } else {
        if (!$("#form_add_claim").valid()) {
            alert("Somethings when wrong, check the message for detail !! !!")

        } else {
            if (confirm("This action will Submit Claim.Please click ‘OK’ to submit the claim or ‘Cancel’ to close the dialog.")) {
                Action = "Submit";
                $("#form_add_claim").submit();
            }
        }
    }
});
$("#form_add_claim_conntent").on("click", "#Claim_save", function (e) {
    debugger
    e.preventDefault();
    if ($("tbody tr").length == 0) {
        alert("Please insert some claim information !!")
    } else {
        if (!$("#form_add_claim").valid()) {
            alert("Somethings when wrong, check the message for detail !!")
        } else {
            Action = "Save";
            $("#form_add_claim").submit();
        }
    }
});


$("#form_add_claim").validate({
    rules: {
        //Remarks: "required",
        Claim_ProjectId: "required",
        Claim_Remarks: {
            pattern: /(\<\w*)((\s\/\>)|(.*\<\/\w*\>))/
        }
    },
    messages: {
        //Remarks: "This field is required",
        Claim_ProjectId: "Please specify value for this field.",
        Claim_Remarks: {
            pattern: "Html character not allowed"
        }
    },
    errorPlacement: function (error, element) {
        if ($(element).attr("name") == 'Claim_ProjectId') {
            error.insertAfter(element.next());
        } else {
            error.insertAfter(element);
        }
    },
    highlight: function (element, errorClass, validClass) {
        $(element).addClass(errorClass).removeClass(validClass);
        //$(element.form).find("label[for=" + element.id + "]")
        //    .addClass(errorClass);
        if ($(element).attr("name") == 'Claim_ProjectId') {
            $(element).next().addClass(errorClass)
        }
    },
    unhighlight: function (element, errorClass, validClass) {
        $(element).removeClass(errorClass).addClass(validClass);
        //$(element.form).find("label[for=" + element.id + "]")
        //    .removeClass(errorClass);
        if ($(element).attr("name") == 'Claim_ProjectId') {
            $(element).next().removeClass(errorClass)

        }
    }
});
//$('.select2').select2()

$("#form_add_claim_conntent").on("change", "#Claim_ProjectId", function (e) {
    $("#RoleInProject").val($(this).find("option:selected").attr("roleP"));
    $("#ProjectDuration").val($(this).find("option:selected").attr("duration"));
    $(this).next().removeClass("error")
});

function CalculateHours() {
    var sum = 0;
    $.each($("#table_list_claim_info tbody tr"), function (indexInArray, valueOfElement) {
        sum += $(this).find("td:eq(4)").html() * 1
    });
    $("#total_Working_Hours").val(sum);
}

CalculateHours();
$("#RoleInProject").val($("#Claim_ProjectId").find("option:selected").attr("roleP"));
$("#ProjectDuration").val($("#Claim_ProjectId").find("option:selected").attr("duration"));
jQuery.validator.addMethod("pattern", function (value, element, params) {
    //debugger
    var a = params.test(value);
    return !(params.test(value));
})
