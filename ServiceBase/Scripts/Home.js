$(document).ready(function () {
    $('#Home-Nav-Link').addClass("active");
    PageMethods.findCurrentProblems(updateDowntimeTable);

    //Problem Tracker Table refresh every 5 seconds
    window.setInterval(function () {
        PageMethods.refreshProblemTables(updateDowntimeTable);
    }, 5000);

    //TODO: No performance issues but next four click events can be summed up to one function
    //TODO: Redundant use of Hide table and remove class
    //TODO: The animation can be fixed by using div containers around table rows and using animations on those
    $("#Facility-Directory-Btn").click(function () {
        if (!$("#Facility-Directory-Btn").hasClass("active")) {
            hideBtnTables();
            $("#Facility-Directory-Btn").addClass("active");
            //Check if button has been clicked yet
            if ($('#Facility-Table').length) {
                $('#Facility-Table').show('Clip');
            }
            else {
                PageMethods.getFacilities(buildFacilityTable);
            }
        }
            //even clicks hide this table
        else {
            $("#Facility-Directory-Btn").removeClass("active");
            $("#Facility-Table").hide('Clip');
        }
    });

    $("#Abbreviation-Btn").click(function () {
        if (!$("#Abbreviation-Btn").hasClass("active")) {
            hideBtnTables();
            $("#Abbreviation-Btn").addClass("active");
            //check if button has been clicked yet
            if ($('#Abbreviation-Table').length) {
                $('#Abbreviation-Table').show('Clip');
            }
            else {
                PageMethods.getAbbreviations(buildAbbreviationTable);
            }
        }
            //Even Clicks hide this table
        else {
            $('#Abbreviation-Btn').removeClass("active");
            $('#Abbreviation-Table').hide('Clip');
        }
    });

    $("#Email-Templates-Btn").click(function () {
        if (!$("#Email-Templates-Btn").hasClass("active")) {
            hideBtnTables();
            $("#Email-Templates-Btn").addClass("active");
            //check if button has been clicked yet
            if ($('#Email-Table').length) {
                $('#Email-Table').show('Slide');
            }
            else {
                PageMethods.getEmailTmeplates(buildEmailTmeplateTable);
            }
        }
            //Even Clicks Hide this table
        else {
            $('#Email-Templates-Btn').removeClass("active");
            $('#Email-Table').hide('Slide');
        }
    });

    $("#Helpful-Hyperlink-Btn").click(function () {

        if (!$("#Helpful-Hyperlink-Btn").hasClass("active")) {
            hideBtnTables();
            $("#Helpful-Hyperlink-Btn").addClass("active");
            //check if button has been clicked yet
            if ($('#Helpful-Hyperlink-Table').length) {
                $('#Helpful-Hyperlink-Table').show('Clip');
            }
            else {
                PageMethods.getHelpfulHyperlinks(buildHelpfulHyperlinkTable);
            }
        }
            //Even Clicks Hide this table
        else {
            $('#Helpful-Hyperlink-Btn').removeClass("active");
            $('#Helpful-Hyperlink-Table').hide('Clip');
        }
    });
});

function hideBtnTables() {
    $("#Facility-Directory-Btn").removeClass("active");
    $("#Facility-Table").hide('Clip');
    $('#Abbreviation-Btn').removeClass("active");
    $('#Abbreviation-Table').hide('Clip');
    $('#Email-Templates-Btn').removeClass("active");
    $('#Email-Table').hide('Clip');
    $('#Helpful-Hyperlink-Btn').removeClass("active");
    $('#Helpful-Hyperlink-Table').hide('Clip');
}

function buildHelpfulHyperlinkTable(rowList) {
    var HyperlinkTable = "<table id='Helpful-Hyperlink-Table' class='table table-hover'";

    for (i = 0; i < rowList.length; i++) {
        var obj = JSON.parse(rowList[i]);
        HyperlinkTable += "<tr>\
                           <td><a href='" + obj.hyperlink + "' target='_blank'>" + obj.title + "</a></td>\
                           </tr>";
    }

    $('#Quick-References-Div').append(HyperlinkTable);
}

function buildEmailTmeplateTable(rowList) {
    var EmailTable = "<table id='Email-Table' class='table table-hover'";

    for (i = 0; i < rowList.length; i++) {
        var obj = JSON.parse(rowList[i]);
        EmailTable += "<tr>\
                           <td><a href='" + obj.template + "'>" + obj.title + "</a></td>\
                           </tr>";
    }

    $('#Quick-References-Div').append(EmailTable);
}

function buildAbbreviationTable(rowList) {
    var AbrTable = "<table id='Abbreviation-Table' class='table table-hover'";

    for (i = 0; i < rowList.length; i++) {
        var obj = JSON.parse(rowList[i]);
        AbrTable += "<tr>\
                           <td>" + obj.abbreviation + "</td>\
                           <td>" + obj.location + "</td>\
                           </tr>";
    }

    $('#Quick-References-Div').append(AbrTable);
}

function buildFacilityTable(rowList) {
    var facilityTable = "<table id='Facility-Table' class='table table-hover'";

    for (i = 0; i < rowList.length; i++) {
        var obj = JSON.parse(rowList[i]);
        facilityTable += "<tr>\
                           <td>" + obj.location + "</td>\
                           <td>" + obj.phoneNumber + "</td>\
                           <td>" + obj.details + "</td>\
                           </tr>";

    }

    $('#Quick-References-Div').append(facilityTable);
}

function updateDowntimeTable(doc) {
    //TODO: Check if docs in table rows are old or not incase someone leaves page up for long time
    if (doc != "") {
        var length = doc.length;
        for (var i = 0; i < length; i++) {
            var obj = JSON.parse(doc[i]);

            if (obj.problemCondition == 1) {
                row = buildDowntimeRow(obj);
                $('#Downtime-Degradation-Table tr:last').after(row);
            }
            else if (obj.problemCondition == 2) {
                row = buildKnownRow(obj);
                $('#Known-Downtime-Table tr:last').after(row);
            }
            else if (obj.problemCondition == 3) {
                row = buildResolvedRow(obj);
                $('#Resolved-Table tr:last').after(row);
            }
        }
    }
}

function buildDowntimeRow(obj) {
    var row = "<tr> <td style='color:#F00000 '> " + obj.affectedService + "</td> \
            <td> " + obj.leadTicket + " </td> \
            <td> " + obj.startDateTime + "</td> \
            <td> " + obj.description + "</td> </tr>";
    return row;
}
function buildResolvedRow(obj) {
    var row = "<tr> <td style='color:green'> " + obj.affectedService + "</td> \
            <td> " + obj.leadTicket + " </td> \
            <td> " + obj.startDateTime + "</td> \
            <td> " + obj.endDateTime + "</td> \
            <td> " + obj.description + "</td> </tr>";
    return row;
}
function buildKnownRow(obj) {
    var row = "<tr> <td style='background-color:yellow'> " + obj.affectedService + "</td> \
            <td> " + obj.leadTicket + " </td> \
            <td> " + obj.plannedStartDateTime + " </td> \
            <td> " + obj.plannedEndDateTime + "</td> \
            <td> " + obj.description + "</td>\
            <td> " + obj.startDateTime + "</td> \
            <td> " + obj.endDateTime + "</td></tr>";

    return row;
}