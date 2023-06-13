$(document).ready(function () {
    $("#PageHeader").text("Administration Portal");
});

$("#AddFilingNameFeeType").click(function () {
    $("#UpdateFilingNameFeeTypeHeader").text("Add FilingNameFee Type");
    $("#UpdateFilingNameFeeType").val("");
    $("#UpdateFilingNameFeeTypeId").val(0);
    
    $("#UpdateFilingNameFeeTypeForm").show();
});

$("#EditFilingNameFeeType").click(function () {
    if ($("#FilingNameFeeType").val() == "-1") {
        alert("You must select an FilingNameFee Type before you can edit it.");
    }
    else {

        $.ajax({
            url: "/Administration/GetFilingNameFeeType/",
            cache: false,
            async: false,
            type: 'GET',
            data: { id: $("#FilingNameFeeType").val() },
            dataType: "json",
            success: function (data) {
                if (data.length > 0) {
                    $("#UpdateFilingNameFeeTypeHeader").text("Edit FilingNameFee Type");
                    $("#UpdateFilingNameFeeType").val(data[0].Value);
                    $("#UpdateFilingNameFeeTypeId").val($("#FilingNameFeeType").val());
                   
                    $("#UpdateFilingNameFeeTypeForm").show();
                }
                else {
                    alert("An error occurred while retrieving the FilingNameFee Type.");
                }
            },
            error: function (xhr) {
                if (xhr.status == 401) {
                    alert(xhr.statusText);
                }
                else {
                   
                }
            }
        });        
    }
});

$("#DeleteFilingNameFeeType").click(function () {
    if ($("#FilingNameFeeType").val() == "-1") {
        alert("You must select an FilingNameFee Type before you can delete it.");
    }
    else {
        if (confirm("Are you sure that you want to delete this FilingNameFee Type?")) {
            $.ajax({
                url: "/Administration/DeleteFilingNameFeeType/",
                cache: false,
                async: false,
                type: 'GET',
                data: { id: $("#FilingNameFeeType").val() },
                dataType: "json",
                success: function (data) {
                    if (data.length > 0) {
                        displayErrorMessage(data[0].Value);
                    }
                    else {
                        $("#FilingNameFeeType option[value='" + $("#FilingNameFeeType").val() + "']").remove();
                        $("#FilingNameFeeType").val("-1");
                    }
                },
                error: function (xhr) {
                    if (xhr.status == 401) {
                        alert(xhr.statusText);
                    }
                    else {
                        alert("An error occurred while trying to delete the FilingNameFee Type.");
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

$("#btnCloseUpdateFilingNameFeeType").click(function () {
    $("#UpdateFilingNameFeeTypeForm").hide();
});

$("#UpdateFilingNameFeeType").on("keydown", function (event) {
    if ($(this).hasClass("invalid-data")) {
        $(this).removeClass("invalid-data");
        $(this).popover("hide");
    }
});

$("#btnUpdateFilingNameFeeType").click(function () {
    var valid = true;
    if ($("#UpdateFilingNameFeeType").val() == "") {
        $("#UpdateFilingNameFeeType").addClass("invalid-data");
        $("#UpdateFilingNameFeeType").attr("data-content", "Required");
        $("#UpdateFilingNameFeeType").popover("show");
        valid = false;
    }
    if (valid) {
        updateFilingNameFeeType();
        $("#UpdateFilingNameFeeTypeForm").hide();
    }
});

function updateFilingNameType() {
    var newFilingNameFeeType = $("#UpdateFilingNameFeeType").val();
    $.ajax({
        url: "/Administration/UpdateFilingNameFeeType/",
        cache: false,
        type: 'GET',
        data: { id: $("#UpdateFilingNameFeeTypeId").val(), name: $("#UpdateFilingNameFeeType").val()},
        dataType: "json",
        success: function (data) {
            if (data.length > 0) {
                var FilingNameFeeTypes = $("#FilingNameFeeType");
                var firstItem = $("option:first", FilingNameFeeTypes);
                var firstItemHtml = '<option value="' + firstItem.val() + '" selected="selected">' + firstItem.text() + '</option>';
                $("option", FilingNameFeeTypes).remove();
                FilingNameFeeTypes.append(firstItemHtml);
                $.each(data, function (key, val) {
                    if (val.Value == newFilingNameFeeType) {
                        FilingNameFeeTypes.append('<option selected value="' + val.Key + '">' + val.Value + '</option>');
                    }
                    else {
                        FilingNameFeeTypes.append('<option value="' + val.Key + '">' + val.Value + '</option>');
                    }
                });
            }
            else {
                displayErrorMessage("An error occurred while trying to update this FilingNameFee Type.");
            }
        },
        error: function (xhr) {
            if (xhr.status == 401) {
                alert(xhr.statusText);
            }
            else {
                alert("An error occurred while trying to update the FilingNameFee Type.");
            }
        }
    });
}

