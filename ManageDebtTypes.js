$(document).ready(function () {
    $("#PageHeader").text("Administration Portal");
});

$("#AddDebtType").click(function () {
    $("#UpdateDebtTypeHeader").text("Add Debt Type");
    $("#UpdateDebtType").val("");
    $("#UpdateDebtTypeId").val(0);
    $("#AllCertificates").prop("checked", true);
    $("#UpdateDebtTypeForm").show();
});

$("#EditDebtType").click(function () {
    if ($("#DebtType").val() == "-1") {
        alert("You must select an Debt Type before you can edit it.");
    }
    else {

        $.ajax({
            url: "/Administration/GetDebtType/",
            cache: false,
            async: false,
            type: 'GET',
            data: { id: $("#DebtType").val() },
            dataType: "json",
            success: function (data) {
                if (data.length > 0) {
                    $("#UpdateDebtTypeHeader").text("Edit Debt Type");
                    $("#UpdateDebtType").val(data[0].Value);
                    $("#UpdateDebtTypeId").val($("#DebtType").val());
                    if (data[1].Value == "True") {
                        $("#AllCertificates").prop("checked", true);
                    }
                    else {
                        $("#AllCertificates").prop("checked", false);
                    }
                    $("#UpdateDebtTypeForm").show();
                }
                else {
                    alert("An error occurred while retrieving the Debt Type.");
                }
            },
            error: function (xhr) {
                if (xhr.status == 401) {
                    alert(xhr.statusText);
                }
                else {
                    alert("An error occurred while trying to update the Certicate Research Status.");
                }
            }
        });        
    }
});

$("#DeleteDebtType").click(function () {
    if ($("#DebtType").val() == "-1") {
        alert("You must select an Debt Type before you can delete it.");
    }
    else {
        if (confirm("Are you sure that you want to delete this Debt Type?")) {
            $.ajax({
                url: "/Administration/DeleteDebtType/",
                cache: false,
                async: false,
                type: 'GET',
                data: { id: $("#DebtType").val() },
                dataType: "json",
                success: function (data) {
                    if (data.length > 0) {
                        displayErrorMessage(data[0].Value);
                    }
                    else {
                        $("#DebtType option[value='" + $("#DebtType").val() + "']").remove();
                        $("#DebtType").val("-1");
                    }
                },
                error: function (xhr) {
                    if (xhr.status == 401) {
                        alert(xhr.statusText);
                    }
                    else {
                        alert("An error occurred while trying to delete the Debt Type.");
                    }
                }
            });
        }
    }
});

function displayErrorMessage(errMsg) {
    $("#closeErrorAlert").off("click");
    $("#closeErrorAlert").click(function () {
        $("#errorAlert").hide();
    });
    $("#errorAlert").show();
    $("#errorAlertMessage").text(errMsg);  
}

$("#btnCloseUpdateDebtType").click(function () {
    $("#UpdateDebtTypeForm").hide();
});

$("#UpdateDebtType").on("keydown", function (event) {
    if ($(this).hasClass("invalid-data")) {
        $(this).removeClass("invalid-data");
        $(this).popover("hide");
    }
});

$("#btnUpdateDebtType").click(function () {
    var valid = true;
    if ($("#UpdateDebtType").val() == "") {
        $("#UpdateDebtType").addClass("invalid-data");
        $("#UpdateDebtType").attr("data-content", "Required");
        $("#UpdateDebtType").popover("show");
        valid = false;
    }
    if (valid) {
        updateDebtType();
        $("#UpdateDebtTypeForm").hide();
    }
});

function updateDebtType() {
    var newDebtType = $("#UpdateDebtType").val();
    $.ajax({
        url: "/Administration/UpdateDebtType/",
        cache: false,
        type: 'GET',
        data: { id: $("#UpdateDebtTypeId").val(), name: $("#UpdateDebtType").val(), allCertificates: $("#AllCertificates").prop("checked") },
        dataType: "json",
        success: function (data) {
            if (data.length > 0) {
                var DebtTypes = $("#DebtType");
                var firstItem = $("option:first", DebtTypes);
                var firstItemHtml = '<option value="' + firstItem.val() + '" selected="selected">' + firstItem.text() + '</option>';
                $("option", DebtTypes).remove();
                DebtTypes.append(firstItemHtml);
                $.each(data, function (key, val) {
                    if (val.Value == newDebtType) {
                        DebtTypes.append('<option selected value="' + val.Key + '">' + val.Value + '</option>');
                    }
                    else {
                        DebtTypes.append('<option value="' + val.Key + '">' + val.Value + '</option>');
                    }
                });
            }
            else {
                displayErrorMessage("An error occurred while trying to update this Debt Type.");
            }
        },
        error: function (xhr) {
            if (xhr.status == 401) {
                alert(xhr.statusText);
            }
            else {
                alert("An error occurred while trying to update the Debt Type.");
            }
        }
    });
}

$("#UpdateDebtTypeCertificates").click(function () {
    $(this).spin("small");
    if ($("#DebtType").val() == "-1") {
        alert("You must select a Debt Type before you can add it to the Certificates where it does not exist.");
        $(this).spin(false);
    }
    else {
        if (confirm("Are you sure that you want to add this Debt Type to the Certificates where it does not exist?")) {
            $.ajax({
                url: "/Administration/AddDebtTypesToCertificates/",
                cache: false,
                async: false,
                type: 'GET',
                data: { debtTypeId: $("#DebtType").val() },
                dataType: "json",
                success: function (data) {
                    $("#UpdateDebtTypeCertificates").spin(false);
                    if (data.length > 0) {
                        alert("An error occurred while trying to add this Debt Type to the Certificates where it does not exist.");
                    }
                    else {
                        alert("This Debt Type was successfully added to the Certificates where it did not exist.");
                    }
                },
                error: function (xhr) {
                    if (xhr.status == 401) {
                        alert(xhr.statusText);
                    }
                    else {
                        alert("An error occurred while trying to add this Debt Type to the Certificates where it does not exist.");
                    
                    }
                    $("#UpdateDebtTypeCertificates").spin(false);
                }      
            });
        }
        else {
            $(this).spin(false);
        }
    }

});