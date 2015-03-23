/*
// The ID of the sub category queried based off <a> text
// Changing text of link must be changed in DB as well
*/
var activeDiv = '#Administrative';
var selectedSubCategory; //which category is currently open

$(document).ready(function () {
    $('#Learning-Center-Nav-Link').addClass("active");
    activateTab('#Administrative');

   $('#Administrative-Tab').click(function () {
       activateTab('#Administrative');
    });

   $('#Operating-Systems-Tab').click(function () {
       activateTab('#Operating-Systems');
   });

   $('#Clinical-Tab').click(function () {
       activateTab('#Clinical');
    });

   $('#Mobile-Devices-Tab').click(function () {
       activateTab('#Mobile-Devices');
    });

   $('#Software-Tab').click(function () {
       activateTab('#Software');
    });

   $('#Tools-Tab').click(function () {
       activateTab('#Tools');
    });

   $('#Misc-Tab').click(function () {
       activateTab('#Misc');
   });

    //SubCategory Selected
   $('a[name=Sub-Group]').click(function () {
       //test if already oppened
       if (selectedSubCategory != this.id) {
           closeSelectedSubCategory();
           selectedSubCategory = this.id;
           //Check if category has been selected yet
           if (!categoryExists()) {
               //DB Id of sub category is found queried based off <a> text
               var subCategoryTitle = $('#' + selectedSubCategory).text();
               PageMethods.getCategoryList(subCategoryTitle, buildList);
           }
           else {
               showExistingCategory();
           }
       }
           
   });

});
function showExistingCategory() {
    $('#' + selectedSubCategory).addClass('active');
    $('li[name=' + selectedSubCategory + '-Sub-Category-List]').show();
}

function categoryExists() {
    if ($('li[name=' + selectedSubCategory + '-Sub-Category-List]').length) {
        return true;
    }
    return false;
}

function buildList(docObjList){

    //TODO: Slide in elements
    $('#' + selectedSubCategory).addClass('active');
    var listElement = "";

    for(var i=0; i<docObjList.length; i++)
    {
        var obj = JSON.parse(docObjList[i]);
        //Someone named files with spaces this makes sure these files still read entierly
        listElement += "<li name='" + selectedSubCategory + "-Sub-Category-List' class='list-group-item'\
                            ><a href='" + obj.documentLink + "' target='_blank' >" + obj.alias + "</a>\
                            </li>";

    }
    $('#' + selectedSubCategory + '-Li').append(listElement);
}

function closeSelectedSubCategory() {
    $('#' + selectedSubCategory).removeClass('active');
    $('li[name=' + selectedSubCategory + '-Sub-Category-List]').hide();
}

function activateTab(curItem) {
    clearDiv();
    $(curItem+'-Tab').addClass("active");
    $(curItem + '-Div').show();
    activeDiv = curItem;
}

function clearDiv() {
    $(activeDiv + '-Tab').removeClass("active");
    $(activeDiv + '-Div').hide();
}