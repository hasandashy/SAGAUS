<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlMoreInformation.ascx.cs" Inherits="SGA.controls.ctrlMoreInformation" %>
<script type="text/javascript">
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var alertHtml = '';
    $(document).ready(function () {
        $('#btnSend').colorbox({
            href: "Popup.aspx",
            width: "392px",
            height: "200px",
            onComplete: function () {
                $('#alertMessage').text(alertHtml);
            }
        });

        $('#btnSend').click(function () {
            //$('#colorbox').css({ "display": "block" });
            var error = 0;
            var emptyFields = new Array();
            var name = $('#FName').val();
            if (name == '' || name == 'First name') {
                error = 1;
                emptyFields.push('First name');
            }
            var surname = $('#LName').val();
            if (surname == '' || surname == 'Last name') {
                error = 1;
                emptyFields.push('Last name');
            }
            var company = $('#CName').val();
            if (company == '' || company == 'Company name') {
                error = 1;
                emptyFields.push('Company name');
            }
            var email = $('#Email').val();
            if (email == '' || email == 'Email address') {
                error = 1;
                emptyFields.push('Email');
            }
            if (error) {
                $('#colorbox').css({ "display": "block" });
                alertHtml = 'Please enter ' + emptyFields.join(', ');
            }
            else if (email != '' && !filter.test(email)) {
                $('#colorbox').css({ "display": "block" });
                alertHtml = 'Please enter valid email id';
            }
            else {
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "About_us.aspx/SendMail",
                    data: JSON.stringify({ 'fname': name, 'lname': surname, 'company': company, 'email': email, 'jobTitle': '', 'phone': '', 'city': '', 'country': '', 'comments': '', 'interest': '', 'needs': '' }),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data.d == 'success') {
                            $('#colorbox').css({ "display": "block" });
                            alertHtml = 'Mail sent successfully.';
                            $('#FName').val('');
                            $('#LName').val('');
                            $('#CName').val('');
                            $('#Email').val('');
                        }
                    }
                });
            }
        });

    });
</script>
<p>
    <input type="text" value="First name" title="First name" class="txt-field1"
        id="FName" maxlength="100" /></p>
<p>
    <input type="text" value="Last name" title="Last name" class="txt-field1"
        id="LName" maxlength="100" /></p>
<p>
    <input type="text" value="Company name" title="Company name" class="txt-field1"
        id="CName" maxlength="100" /></p>
<p>
    <input type="text" value="Email address" title="Email address" class="txt-field1"
        id="Email" maxlength="250" /></p>
<p class="txtRgt mrg-bt-0">
    <input type="submit" value="" class="btn-go1" id="btnSend" /></p>

