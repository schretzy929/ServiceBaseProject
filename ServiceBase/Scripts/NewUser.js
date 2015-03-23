/*Newly added Navs should be disabled for this first page*/
$(document).ready(function () {
    $('#submit-btn').click(function () {
        var firstName = $('#First-Name').val();
        var lastName = $('#Last-Name').val();

        if(firstName != "" && lastName != "")
        {
            PageMethods.addUserInfo(firstName, lastName, onSuccess);
            
        }
        else {
            bootbox.dialog({
                message: "You must give a first and last name.",
                title: "ServiceBase",
                buttons: {
                    success: {
                        label: "Try again",
                        className: "btn-danger",
                    }//end success
                }//end button
            });
        }
    });
});

function onSuccess() {
    bootbox.dialog({
        message: "Thank you, your record has been updated.",
        title: "ServiceBase",
        buttons: {
            success: {
                label: "Start!",
                className: "btn-primary",
                callback: function() {
                    window.location.replace("Home.aspx");
                }//End callback
            }//end success
        }//end button
    });
}
