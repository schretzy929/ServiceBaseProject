/// <reference path="C:\Users\schretzenmaiers\Documents\Visual Studio 2013\Projects\ServiceBaseProject\ServiceBase\Pages/SubmitDocumentation.aspx" />
/// <reference path="C:\Users\schretzenmaiers\Documents\Visual Studio 2013\Projects\ServiceBaseProject\ServiceBase\Pages/SubmitDocumentation.aspx" />
/*
* JavaScript for SubmitDocumentation.aspx
* 
*/


//Naviagation Buttons for Doc Tracker

var index = 0;
var lastIndex;
$(document).ready(function () {
    $("#Submit-Btn").hide();
    window.onload = function () {
        $("#Submit-Nav-Link").addClass("active");
        $('#SD-Doc-Dropdown-Nav-Link').addClass("active");
        getLastIndex();
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

//Bootbox custome alert
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

function getLastIndex() {
    PageMethods.getLast(setLast);
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
    $("#AliasInput").val(obj.alias);
    $("#TitleInput").val(obj.documentTitle);
    $("#PDFHyperlinkInput").val(obj.documentLink);
    $("#PwTitleInput").val(obj.passwordTitle);
    $("#PwHyperlinkInput").val(obj.passwordHyperlink);
    //Public checkbox update
    if (obj.PDFaccess == 1) {
        $("#PDF-Public-Checkbox").prop("checked", true);
    }
    else {
        $("#PDF-Public-Checkbox").prop("checked", false);
    }
    if (obj.PWaccess == 1) {
        $("#PW-Public-Checkbox").prop("checked", true);
    }
    else {
        $("#PW-Public-Checkbox").prop("checked", false);
    }
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

    $("#AliasInput").val("");
    $("#TitleInput").val("");
    $("#PDFHyperlinkInput").val("");
    $("#PwTitleInput").val("");
    $("#PwHyperlinkInput").val("");
    $("#PDF-Public-Checkbox").prop("checked", false);
    $("#PW-Public-Checkbox").prop("checked", false);
    //Show submit btn
    $("#Submit-Btn").show("explode");
}



function setLast(int) {
    lastIndex = int;
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
            var $button = $('<button/>').attr({ id: 'find-btn' + i, value: obj.index, class: 'btn btn-block', onclick: "getDocAt('" + obj.index + "');return false" }).append(obj.alias);

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

function buildDoc() {
    var doc = {};
    doc.index = index;
    doc.alias = $("#AliasInput").val();
    doc.documentTitle = $("#TitleInput").val();
    doc.documentLink = $("#PDFHyperlinkInput").val();
    doc.passwordTitle = $("#PwTitleInput").val();
    doc.passwordHyperlink = $("#PwHyperlinkInput").val();
    if ($("#PDF-Public-Checkbox")[0].checked == true) {
        doc.PDFaccess = 1;
    }
    else {
        doc.PDFaccess = 2;
    }
    if ($("#PW-Public-Checkbox")[0].checked == true) {
        doc.PWaccess = 1;
    }
    else {
        doc.PWaccess = 2;
    }

    return doc;
}

$("#Update-Btn").click(function () {
    var doc = buildDoc();
    if ($('#AliasInput').val() != "" && ($('#TitleInput').val() != "" || $('#PwTitleInput').val() != "")) {

        $.ajax({
            type: 'Post',
            url: '../Pages/SubmitDocumentation.aspx/updateDoc',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ 'doc': doc }),
            async: false,
            success: function (r) {
                okayAlert(doc.documentTitle + "updated in ServiceBase!");
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
    }
    else {
        okayAlert("Please give the document an alias as well as a password title or document title.");
    }




});

//Only to be submitted when index = -1 
$("#Submit-Btn").click(function () {
    if (index == -1) {
        //TODO: What is required
        if ($('#AliasInput').val() != "" && ( $('#TitleInput').val() != "" || $('#PwTitleInput').val() != "" )) {
            var doc = buildDoc();

            $.ajax({
                type: 'Post',
                url: '../Pages/SubmitDocumentation.aspx/submit',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: JSON.stringify({ 'doc': doc }),
                async: false,
                success: function (r) {
                    okayAlert(doc.documentTitle + " added to ServiceBase!");
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
            okayAlert("Please give the document an alias as well as a password title or document title.");
        }
    }
});

