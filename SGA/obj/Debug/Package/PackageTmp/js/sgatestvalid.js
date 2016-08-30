var alertHtml = '';
$(document).ready(function () {
    $("#ContentPlaceHolder1_lnkNext").colorbox({
        href: "../Popup.aspx",
        width: "392px",
        height: "220px",
        onComplete: function () {
            if (alertHtml.length > 0) {
                $('#colorbox').css({ "display": "block" });
                $('#alertMessage').text(alertHtml);
            } else {
                document.getElementById("ContentPlaceHolder1_btnSubmitNext").click();
                //parent.$.fn.colorbox.close();
                //$('#cboxOverlay').css({ "display": "none" });
            }
        }
    });


    $("#ContentPlaceHolder1_lnkNext").click(function () {
        var ct = $("#ContentPlaceHolder1_hdCount").val();
        alert(ct);
        var unsq = "";
        var pt = "";
        for (j = 0; j < ct; j++) {
            if (j <= 9) {
                pt = '0' + j;
            } else {
                pt = j;
            }
            var rbc = document.getElementsByName("ctl00$ContentPlaceHolder1$parentRepeater$ctl" + pt + "$RadioButtonList1");
            for (i = 0; i < rbc.length; i++) {
                if (rbc[i].checked == true) {
                    break;
                }
            }
            if (i == rbc.length) {
                unsq = unsq + (j + 1) + ",";
            }
        }
        if (unsq != "") {
            //alert("You have not answered one or all of the questions on this page, please select an option before continuing.");
            alertHtml = "You have not answered one or all of the questions on this page, please select an option before continuing.";

        }
        else {
            alertHtml = "";
            return true;
        }
    });

});