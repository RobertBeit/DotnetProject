$(document).ready(function () {
    $("#PageHeader").text("Administration Portal");
});

$("#AddAgencyType").click(function () {
    $("#UpdateAgencyTypeHeader").text("Add Agency Type");
    $("#UpdateAgencyType").val("");
    //$("#SortOrder").val("");
    $("#UpdateAgencyTypeId").val(0);
    $("#AllCertificates").prop("checked", true);
    $("#UpdateAgencyTypeForm").show();
});

$("#EditAgencyType").click(function () {
    if ($("#AgencyType").val() == "-1") {
        alert("You must select an Agency Type before you can edit it.");
    }
    else {

        $.ajax({
            url: "/Administration/GetAgencyType/",
            cache: false,
            async: false,
            type: 'GET',
            data: { id: $("#AgencyType").val() },
            dataType: "json",
            success: function (data) {
                if (data.length > 0) {
                    $("#UpdateAgencyTypeHeader").text("Edit Agency Type");
                    $("#UpdateAgencyType").val(data[0].Value);
                    $("#UpdateAgencyTypeId").val($("#AgencyType").val());
                    if (data[2].Value == "True") {
                        $("#AllCertificates").prop("checked", true);
                    }
                    else {
                        $("#AllCertificates").prop("checked", false);
                    }
                    $("#UpdateIndustryType").val(data[3].Value);
                    $("#UpdateAgencyTypeForm").show();
                }
                else {
                    alert("An error occurred while retrieving the Agency Type.");
                }
            },
            error: function (xhr) {
                if (xhr.status == 401) {
                    alert(xhr.statusText);
                }
                else {
                    alert("An error occurred retrieving the Agency Type.");
                }
            }
        });
    }
});

$("#DeleteAgencyType").click(function () {
    if ($("#AgencyType").val() == "-1") {
        alert("You must select an Agency Type before you can delete it.");
    }
    else {
        if (confirm("Are you sure that you want to delete this Agency Type?")) {
            $.ajax({
                url: "/Administration/DeleteAgencyType/",
                cache: false,
                async: false,
                type: 'GET',
                data: { id: $("#AgencyType").val() },
                dataType: "json",
                success: function (data) {
                    if (data.length > 0) {
                        displayErrorMessage(data[0].Value);
                    }
                    else {
                        $("#AgencyType option[value='" + $("#AgencyType").val() + "']").remove();
                        $("#AgencyType").val("-1");
                    }
                },
                error: function (xhr) {
                    if (xhr.status == 401) {
                        alert(xhr.statusText);
                    }
                    else {
                        alert("An error occurred while trying to delete the Agency Type.");
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

$("#btnCloseUpdateAgencyType").click(function () {
    $("#UpdateAgencyTypeForm").hide();
});

$("#UpdateAgencyType").on("keydown", function (event) {
    if ($(this).hasClass("invalid-data")) {
        $(this).removeClass("invalid-data");
        $(this).popover("hide");
    }
});

$("#btnUpdateAgencyType").click(function () {

    var valid = true;
    if ($("#UpdateAgencyType").val() == "") {
        $("#UpdateAgencyType").addClass("invalid-data");
        $("#UpdateAgencyType").attr("data-content", "Required");
        $("#UpdateAgencyType").popover("show");
        valid = false;
    }

    if (valid) {
        updateAgencyType();
        $("#UpdateAgencyTypeForm").hide();
    }

});

function updateAgencyType() {
    var newAgencyType = $("#UpdateAgencyType").val();
    $.ajax({
        url: "/Administration/UpdateAgencyType/",
        cache: false,
        type: 'GET',
        data: { id: $("#UpdateAgencyTypeId").val(), name: $("#UpdateAgencyType").val(), allCertificates: $("#AllCertificates").prop("checked"), industryTypeId: $("#UpdateIndustryType").val() },
        dataType: "json",
        success: function (data) {
            if (data.length > 0) {
                var agencyTypes = $("#AgencyType");
                var firstItem = $("option:first", agencyTypes);
                var firstItemHtml = '<option value="' + firstItem.val() + '" selected="selected">' + firstItem.text() + '</option>';
                $("option", agencyTypes).remove();
                agencyTypes.append(firstItemHtml);
                $.each(data, function (key, val) {
                    if (val.Value == newAgencyType) {
                        agencyTypes.append('<option selected value="' + val.Key + '">' + val.Value + '</option>');
                    }
                    else {
                        agencyTypes.append('<option value="' + val.Key + '">' + val.Value + '</option>');
                    }
                });
            }
            else {
                displayErrorMessage("An error occurred while trying to update this Agency Type.");
            }
        },
        error: function (xhr) {
            if (xhr.status == 401) {
                alert(xhr.statusText);
            }
            else {
                alert("An error occurred while trying to update the exemption.");
            }
        }
    });
}

$("#UpdateAgencyTypeCertificates").click(function () {
    $(this).spin("small");
    if ($("#AgencyType").val() == "-1") {
        alert("You must select an Agency Type before you can add it to the Certificates where it does not exist.");
        $(this).spin(false);
    }
    else {
        if (confirm("Are you sure that you want to add this Agency Type to the Certificates where it does not exist?")) {
            $.ajax({
                url: "/Administration/AddAgencyTypesToCertificates/",
                cache: false,
                async: false,
                type: 'GET',
                data: { agencyTypeId: $("#AgencyType").val() },
                dataType: "json",
                success: function (data) {
                    $("#UpdateAgencyTypeCertificates").spin(false);
                    if (data.length > 0) {
                        alert("An error occurred while trying to add this Agency Type to the Certificates where it does not exist.");
                    }
                    else {
                        alert("This Agency Type was successfully added to the Certificates where it did not exist.");
                    }
                },
                error: function (xhr) {
                    if (xhr.status == 401) {
                        alert(xhr.statusText);
                    }
                    else {
                        alert("An error occurred while trying to add this Agency Type to the Certificates where it does not exist.");
                    
                    }
                    $("#UpdateAgencyTypeCertificates").spin(false);
                }
            });
        }
        else {
            $(this).spin(false);
        }
    }

});