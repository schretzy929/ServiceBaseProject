var index = 0;
var lastIndex;
$(document).ready(function () {
    $("#Submit-Btn").hide();
    window.onload = function () {
        $("#Submit-Nav-Link").addClass("active");
        $("#Info-Gath-Dropdown-Nav-Link").addClass("active");
        PageMethods.getLast(setLast);
        getFirst();
    };

    //Find Button Method
    $("#Find-Btn").click(function () {
        var alias = $("#Find-Text").val();
        if (alias != "") {
            PageMethods.find(alias, updateFindTable);
        }
        else {
            okayAlert("Please enter some search criteria.")
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

function setLast(int) {
    lastIndex = int;
}

function getFirst() {
    index = 0;
    PageMethods.getDoc(index, updateForm);
}
function getPrevious() {
    if (index != 0 && index != -1) {
        index = index - 1;
        PageMethods.getDoc(index, updateForm);
    }
    else {
        okayAlert("There are no more previous documents.")
    }
}

function getNext() {
    if (index != lastIndex) {
        index = index + 1;
        PageMethods.getDoc(index, updateForm);
    }
    else {
        okayAlert("There are no more documents.")
    }

}

function getLast() {
    index = lastIndex;
    PageMethods.getDoc(index, updateForm);
}


function getDocAt(listLocation) {
    index = +listLocation; //casted as int
    PageMethods.getDoc(index, updateForm);
}

function updateForm(doc) {
    $("#Submit-Btn").hide("explode");
    $('#New-Btn').removeClass("active");
    $("#Update-Btn").show("explode");

    var obj = JSON.parse(doc);
    $("#Searchable-Alias-Input").val(obj.searchableAlias);
    $("#PDF-Title-Input").val(obj.infoGathTitle);
    $("#PDF-Link-Input").val(obj.infoGathLink);
    $("#Assignment-Group-Input").val(obj.assignmentGroup);
    $("#Support-Info-Input").val(obj.supportInfo);
    $("#Support-Hours-Input").val(obj.supportHours);
    $("#Contact-Input").val(obj.contactInfo);
    $("#PW-Attributes-Input").val(obj.passwordAttributes);
    $("#PW-Title-Input").val(obj.passwordTitle);
    $("#PW-Link-Input").val(obj.passwordHyperlink);
    $("#Aliases-Input").val(obj.aliases);
    
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

    $("#Searchable-Alias-Input").val("");
    $("#PDF-Title-Input").val("");
    $("#PDF-Link-Input").val("");
    $("#Assignment-Group-Input").val("");
    $("#Support-Info-Input").val("");
    $("#Support-Hours-Input").val("");
    $("#Contact-Input").val("");
    $("#PW-Attributes-Input").val("");
    $("#PW-Title-Input").val("");
    $("#PW-Link-Input").val("");
    $("#Aliases-Input").val("");

    //Show submit btn
    $("#Submit-Btn").show("explode");
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
            var $button = $('<button/>').attr({ id: 'find-btn' + i, value: obj.index, class: 'btn btn-block', onclick: "getDocAt('" + obj.index + "');return false" }).append(obj.searchableAlias);

            var tr = $("#Find-List").hide().append($button).show('Blind');
        }
    }
    else {
        okayAlert("No results found.");
    }

}

function showUpdateBtn() {
    if ($('#New-Btn').hasClass("active")) {
        $("#Update-Btn").hide("explode");
        $('#Submit-Btn').show("explode");
    }
    else {
        $("#Update-Btn").show("explode");
    }
}

function hideButtons() {
    $("#Submit-Btn").hide("explode");
    $("#Update-Btn").hide("explode");
}

//Build InfoGathering JSON Object for Ajax push to Server
function buildDoc() {
    var doc = {};
    doc.index = index; 
    doc.searchableAlias = $("#Searchable-Alias-Input").val();
    doc.infoGathTitle = $("#PDF-Title-Input").val();
    doc.infoGathLink = $("#PDF-Link-Input").val();
    doc.assignmentGroup = $("#Assignment-Group-Input").val();
    doc.supportInfo = $("#Support-Info-Input").val();
    doc.supportHours = $("#Support-Hours-Input").val();
    doc.contactInfo = $("#Contact-Input").val();
    doc.passwordAttributes = $("#PW-Attributes-Input").val();
    doc.passwordTitle = $("#PW-Title-Input").val();
    doc.passwordHyperlink = $("#PW-Link-Input").val();
    doc.aliases = $("#Aliases-Input").val();

    return doc;
}

$("#Update-Btn").click(function () {
    if ($("#Searchable-Alias-Input").val() != "" && $("#PDF-Title-Input").val() != "" && $("#PDF-Link-Input").val() != "") {
        var doc = buildDoc();

        $.ajax({
            type: 'Post',
            url: '../Pages/SubmitInfoGathering.aspx/updateDoc',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ 'doc': doc }),
            async: false,
            success: function (r) {
                okayAlert(doc.infoGathTitle + " updated in ServiceBase!");
                var alias = $("#Find-Text").val();
                if (alias != "") {
                    PageMethods.find(alias, updateFindTable);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(xhr.responseText);
                alert(thrownError);
            }
        });
    } else {
        okayAlert("Please give the document a Searchable Alias, Document Title and Document Hyperlink.");
    }
});

//Only to be submitted when index = -1 
$("#Submit-Btn").click(function () {
    if (index == -1) {
        if ($("#Searchable-Alias-Input").val() != "" && $("#PDF-Title-Input").val()!="" && $("#PDF-Link-Input").val() != "") {
            var doc = buildDoc();

            $.ajax({
                type: 'Post',
                url: '../Pages/SubmitInfoGathering.aspx/submit',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: JSON.stringify({ 'doc': doc }),
                async: false,
                success: function (r) {
                    okayAlert(doc.infoGathTitle + " added to ServiceBase!");
                    getFirst();
                    lastIndex++;
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(xhr.responseText);
                    alert(thrownError);
                }
            });
        }
        else {
            okayAlert("Please give the document a Searchable Alias, Document Title and Document Hyperlink.");
        }
    }
});