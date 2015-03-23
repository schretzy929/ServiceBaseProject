var index = 0;
var lastIndex;

$(document).ready(function () {
    $("#Submit-Btn").hide();
    window.onload = function () {
        $("#Problem-Tracker-Nav-Link").addClass("active");
        //TODO: This is a lot of load time
        pullDropdownInfo();
        getFirst();
        PageMethods.findLastIndex(setLast);
        PageMethods.getLastProblem(updateURT); //Populate Last Updated Record Table
    };

    //Find Button Method
    $("#Find-Btn").click(function () {
        var alias = $("#Find-Text").val();
        if (alias != "") {
            //TODO: Have seperate Ticekt Search that displays ticket numbers in buttons and cuts search in half
            PageMethods.find(alias, updateFindTable);
        }
        else {
            okayAlert("Please enter some search criteria.");
        }
    });

    //Find Button EnterKey
    $('#Find-Text').keypress(function (e) {
        if (e.keyCode == 13) {
            $("#Find-Btn").click();
        }
    });

}); //End Doc Ready

function okayAlert(message) {
    bootbox.dialog({
        message: message,
        title: "ServiceBase",
        buttons: {
            success: {
                label: "Okay",
                className: "btn-primary",
            }//end success
        }//end button
    });
}

$(function () {
   // $('#Start-Date-Time').datetimepicker();
    $('#End-Date-Time').datetimepicker();
    $('#Planned-Start-Date-Time').datetimepicker();
    $('#Planned-End-Date-Time').datetimepicker();
});

//TODO: Seperate all naviagaion methods to seperate JS file, Requires change Problem to Doc in fn names
function getFirst() {
    index = 0;
    PageMethods.getProblem(index, updateForm);
}

function getPrevious() {
    if (index != 0 && index != -1) {
        index = index - 1;
        PageMethods.getProblem(index, updateForm);
    }
    else {
        okayAlert("There are no more previous documents.");
    }
}

function getNext() {
    if (index != lastIndex){
        index = index + 1;
        PageMethods.getProblem(index, updateForm);
    }
    else {
        okayAlert("There are no more doucments.");
    }
}

function getLast() {
    index = lastIndex;
    PageMethods.getProblem(index, updateForm);
}

function setLast(int) {
    lastIndex = int;
}

function getDocAt(listLocation) {
    index = +listLocation; //casted as int
    PageMethods.getProblem(index, updateForm);
}

//Mext 3 fns Populating Dropdown Lists
function pullDropdownInfo() {
    PageMethods.getProblemConditions(populateProblemConditions);
    PageMethods.getProblemImpacts(populateProblemImpacts);
}

function populateProblemConditions(conditionsList) {
    for(var i=0; i <conditionsList.length; i++)
    {
        var obj = JSON.parse(conditionsList[i]);
        $('#Condition-Dropdown').append($('<option/>', {
            value: obj.id,
            text: obj.name
        }));
    }
}

function populateProblemImpacts(impactsList) {
    for(var i=0; i<impactsList.length; i++)
    {
        var obj = JSON.parse(impactsList[i]);
        $('#Planned-Impact-Dropdown').append($('<option/>', {
            value: obj.id,
            text: obj.impactLevel
        }));
        $('#Actual-Impact-Dropdown').append($('<option/>', {
            value: obj.id,
            text: obj.impactLevel
        }));
    }
}

function updateForm(doc) {
    $("#Submit-Btn").hide("explode");
    $('#New-Btn').removeClass("active");
    $("#Update-Btn").show("explode");

    var obj = JSON.parse(doc);
    $("#Affected-Service-Text").val(obj.affectedService);
    $("#Start-Date-Time-Text").val(obj.startDateTime);
    $("#Condition-Dropdown").val(obj.problemCondition);
    $("#Ticket-Text").val(obj.leadTicket);
    $("#Actual-Impact-Dropdown").val(obj.actualImpact);
    $("#Reported-By-Text").val(obj.reportedBy);
    $("#End-Date-Time-Text").val(obj.endDateTime);
    $("#Issue-Description-Text").val(obj.description);
    $("#Planned-Start-Date-Time-Text").val(obj.plannedStartDateTime);
    $("#Planned-End-Date-Time-Text").val(obj.plannedEndDateTime);
    $("#Planned-Impact-Dropdown").val(obj.plannedImpact);
    
    toggleConditionFields();
}

function cleanForm() {
    $('#New-Btn').addClass("active");
    $("#Update-Btn").hide("explode");
    index = -1; //Index at start of list

    //clear table
    //TODO: Playing with styles here
    $("#Find-List").children().each(function () {
        $(this).hide('Blind').delay(1000).remove();

    });


    $("#Affected-Service-Text").val("");
    $("#Start-Date-Time-Text").val("");
    $("#Condition-Dropdown").val("");
    $("#Ticket-Text").val("");
    $("#Actual-Impact-Dropdown").val("");
    $("#Reported-By-Text").val("");
    $("#End-Date-Time-Text").val("");
    $("#Issue-Description-Text").val("");
    cleanPlannedPortion();

    //Show submit btn
    $("#Submit-Btn").show("explode");
}

//Update Last updated Record Table
function updateURT(lastProblem) {
    var obj = JSON.parse(lastProblem);

    $("#URT-Affected-Service").text(obj.affectedService)
    $("#URT-Lead-Ticket").text(obj.leadTicket);
    $("#URT-End-Time").text(obj.endDateTime);
    $("#URT-Issue-Description").text(obj.description);
}

$("#emailLink").click(function () {
    var mailContent = "mailto:ENG - Help Desk (INTERNAL USE ONLY)?subject=Issue: " + $("#Issue-Description-Text").val() + " | " + $("#Condition-Dropdown option:selected").text() + " | Lead Ticket: " + $("#Ticket-Text").val() + "\
        &body=Affected Service: " + "%0D%0A" + $("#Affected-Service-Text").val()
        + "%0D%0AStatus: " + "%0D%0A" + $("#Condition-Dropdown option:selected").text()
        + "%0D%0ALead Ticket: " + "%0D%0A" + $("#Ticket-Text").val()
        + "%0D%0AIssue Description:" + "%0D%0A" + $("#Issue-Description-Text").val()
        + "%0D%0AStart Date / Time: " + "%0D%0A" + $("#Start-Date-Time-Text").val()
        + "%0D%0AEnd Date / Time: " + "%0D%0A" + $("#End-Date-Time-Text").val();
    $("#emailLink").attr("href", mailContent);
});

function cleanPlannedPortion() {
    $("#Planned-Start-Date-Time-Text").val("");
    $("#Planned-End-Date-Time-Text").val("");
    $("#Planned-Impact-Dropdown").val("");
}

function showUpdateBtn() {
    //If In new form only show submit btn
    if ($('#New-Btn').hasClass("active")) {
        $("#Update-Btn").hide("explode");
        $('#Submit-Btn').show("explode");
    }
    //Not in new form, show update btn
    else {
        $("#Update-Btn").show("explode");
    }
}
function updateFindTable(doc) {
    if (doc != "") {
        //clear table
        //TODO: Playing with styles here
        $("#Find-List").children().each(function () {
            $(this).hide('Blind').delay(1000).remove();

        });
        var length = doc.length;
        for (var i = 0; i < length; i++) {
            var obj = JSON.parse(doc[i]);
            var $button = $('<button/>').attr({ id: 'find-btn' + i, value: obj.index, class: 'btn btn-block', onclick: "getDocAt('" + obj.index + "');return false" }).append(obj.affectedService);

            var tr = $("#Find-List").hide().append($button).show('Blind');
        }
    }
    else {
        okayAlert("No results found");
    }
}

$('#Condition-Dropdown').on('change', function () {
    toggleConditionFields();
});

function toggleConditionFields() {
    if ($("#Condition-Dropdown").val() == 2) {
        $('.Known-Downtime-Row').show('Slide');
    }
    else {
        $('.Known-Downtime-Row').hide('Slide');
        cleanPlannedPortion();
    }
}

$("#Update-Btn").click(function () {
    if (formComplete()) {
        var doc = buildDoc();

        $.ajax({
            type: 'Post',
            url: '../Pages/ProblemTrackerReporting.aspx/updateDoc',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ 'doc': doc }),
            async: false,
            success: function (r) {
                okayAlert(doc.affectedService + " updated in ServiceBase!");
                var alias = $("#Find-Text").val();
                if (alias != "") {
                    PageMethods.find(alias, updateFindTable);
                    PageMethods.getLastProblem(updateURT); //Populate Last Updated Record Table
                    getLast();
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(xhr.responseText);
                alert(thrownError);
            }
        });
    } else {
        okayAlert("Please fill out the required fields.")
    }
});

function buildDoc() {
    var doc = {};
    doc.index = index;
    doc.affectedService = $("#Affected-Service-Text").val();
    doc.leadTicket = $("#Ticket-Text").val();
    doc.startDateTime = $("#Start-Date-Time-Text").val();
    doc.endDateTime = $("#End-Date-Time-Text").val();
    doc.description = $("#Issue-Description-Text").val();
    doc.problemCondition = $("#Condition-Dropdown").val();
    doc.plannedEndDateTime = $("#Planned-End-Date-Time-Text").val();
    doc.reportedBy = $("#Reported-By-Text").val();
    doc.plannedStartDateTime = $("#Planned-Start-Date-Time-Text").val();
    doc.plannedImpact = $("#Planned-Impact-Dropdown").val();
    if (doc.plannedImpact == null) { doc.plannedImpact = 0; } //check for null case
    doc.actualImpact = $("#Actual-Impact-Dropdown").val();
    if (doc.actualImpact == null) { doc.actualImpact = 0; }

    return doc;
}

//Only to be submitted when index = -1 
$("#Submit-Btn").click(function () {
    if (index == -1) {
        if (formComplete()) {
            var doc = buildDoc();

            $.ajax({
                type: 'Post',
                url: '../Pages/ProblemTrackerReporting.aspx/submit',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: JSON.stringify({ 'doc': doc }),
                async: false,
                success: function (r) {
                    okayAlert(doc.affectedService + " added to ServiceBase!");
                    PageMethods.getLastProblem(updateURT); //Populate Last Updated Record Table
                    //getFirst();
                    lastIndex++;
                    getLast();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(xhr.responseText);
                    alert(thrownError);
                }
            });
        }
        else {
            okayAlert("Please fill out the required fields.");
        }
    }
});

function formComplete() {

    //Always Required 
    if ($('#Affected-Service-Text').val() == "" || $('#Start-Date-Time-Text').val() == "" || $('#Ticket-Text').val() == "" || $('#Reported-By-Text').val() == "" || $('#Issue-Description-Text').val() == "" || $("#Condition-Dropdown").val() == "" || $("#Actual-Impact-Dropdown").val() == "") {
        return false;
    }

    //Known Downtime
    if($('#Condition-Dropdown').val() == 2)
    {
        if ($('#Planned-Start-Date-Time-Text').val() == "" || $('#Planned-End-Date-Time-Text').val() == "" || $('#Planned-Impact-Dropdown').val() == "") {
            return false;
        }
    }

    //Resolution
    if ($('#Condition-Dropdown').val() == 3) {
        if ($('#End-Date-Time-Text').val() == "") {
            return false;
        }
    }

return true;

}