<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reset_Password.aspx.cs" Inherits="SGA.Reset_Password" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script type="text/javascript">
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        var alertHtml = '';
        var success = '';
        var refresh = 'y';
        $(document).ready(function () {
            $('.btn-submit').colorbox({
                href: "Popup.aspx",
                width: "392px",
                height: "200px",
                onComplete: function () {
                    if (success == 's') {
                        $('#title').text("Success!");
                    } else {
                        $('#title').text("Alert");
                    }
                    $('#alertMessage').text(alertHtml);
                }
            });

            $('.btn-submit').click(function () {
                var json =
                        $.ajax({
                            type: "POST",
                            async: false,
                            url: "About_us.aspx/ForgotPassword",
                            data: JSON.stringify({ 'email': $('#email-add').val() }),
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                if (data.d == 's') {
                                    success = 's';
                                    $('#colorbox').css({ "display": "block" });
                                    alertHtml = 'A new password has been emailed to you. Use this new password to log back in. ';
                                    $('#email-add').val('');

                                } else if (data.d == 'f') {
                                    success = 'f';
                                    $('#colorbox').css({ "display": "block" });
                                    alertHtml = 'The information you entered does not match.';
                                }
                            }
                        });
            });

        });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="reset-password">
	<div class="reset-cnt">
		<p class="title28">Reset Password</p>
		<p>To reset your or password, enter the required details.<br />You will receive a system-generated email with<br />further instructions to complete the password reset.</p>
		<p>&nbsp;</p>
		<p>Fields with a <span class="required">*</span> are required fields.</p>
	</div>
	<div class="reset-form">
		<form action="#" method="post">
			<h1>Enter your details</h1>
			<label for="email-add">Email address<span class="required">*</span></label>
			<span class="txt-bx">
            <input type="text" name="email-add" maxlength="250" id="email-add" />
            </span>
			
			<div class="clear"></div>
			<%--<div class="captcha">
                <span style="margin-right:10px;">
                <img src="Captcha.ashx?" id="imgCode" style="width:145px;height:37px;" alt="" />
                </span>
				<a href="javascript:void(0);"  id="newchapta"><span>
                <img src="images/captcha-btn.png" />
                </span></a>
			</div>--%>
			<input type="submit" value="" class="btn-submit" />
            
			<div class="clear"></div>
		</form>
	</div>
	<div class="clear"></div>
</div>
    </form>
</body>
</html>
<script type="text/javascript">
    // with this method this captcha gets chaned on client side ! have fun
    /*$(function (e) {
        $("#newchapta").click(function (e) {
            var imgNew = $("#imgCode");
            d = new Date();
            imgNew.attr("src", 'Captcha.ashx?' + d.getTime());
        });

    });*/

    
    
</script>
