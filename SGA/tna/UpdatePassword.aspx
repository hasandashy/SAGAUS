<%@ Page  Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="UpdatePassword.aspx.cs" Inherits="SGA.tna.UpdatePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!-- Custom Form -->
		<script type="text/javascript" src="../js/custom-form-elements.js"></script>
		
		<!-- Accordion Menu -->
		<script type="text/javascript" src="../js/jquery.min.js"></script>
		<!-- Popup -->
		<script type="text/javascript" src="../Scripts/jquery.colorbox.js"></script>
		<script type="text/javascript" src="../js/custom.js"></script>
        <script type="text/javascript" language="javascript">
           var alertHtml = '';
            var lastpage = 'n';
            var redirect = 'y';
            


            $(document).ready(function () {
                $('#submitbutton,#submitbuttonnext').colorbox({
                    href: "../Popup.aspx",
                    width: "392px",
                    height: "450px",
                    onComplete: function () {
                        if (lastpage == 'y') {
                            $('#colorbox').css({ "display": "none" });
                            window.location.href = '/tna/default.aspx';
                        }
                        else {
                            redirect = "n";
                            $('#colorbox').css({ "display": "block" });
                            $('#alertMessage').html(alertHtml);
                        }
                        

                    }
                });
                $('#submitbutton,#submitbuttonnext').click(function () {
                    var error = 0;
                    var emptyFields = new Array();
                    var password = $('#<%=newpassword.ClientID %>').val();

                    if (password == '' || password == "New password") {
                        error = 1;
                        emptyFields.push("New Password");
                    }

                    if (error) {
                        $('#colorbox').css({ "display": "block" });
                        alertHtml = 'Please enter ' + emptyFields.join(', ');
                    }
                    
                    else {
                        alertHtml = 's';
                        lastpage = 'y';

                        var json =
                        $.ajax({
                            type: "POST",
                            async: false,
                            url: "UpdatePassword.aspx/UpdateNewPassword",
                            data: JSON.stringify({ 'password': password }),
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                if (data.d == 's') {
                                    alertHtml = 's';
                                    lastpage = 'y';
                                }
                            }
                        });
                    }
                });
            });
</script>
<article id="container">
					<section class="welcome-inner">
						<p class="title40-orange" style="text-align:center;"><asp:Label ID="lblName" runat="server"></asp:Label></p>
						<p class="title40">Welcome to your Assessments</p>
					</section>
					<section class="color-box">
						<article class="info-box-shdw-inner">
							<div class="my-pofile-form">
								<p class="txt28 orange">Please choose your own password.</p>
								<p>&nbsp;</p>
								<p class="txt18-bold">Login Details</p>
                                <span class="error"></span>&nbsp;<input type="text" id="email" name="email" readonly="readonly" maxlength="20" runat="server" class="text-box-2" />
                                <p>&nbsp;</p>
                                <span class="error"></span>&nbsp;<input type="text" id="password" name="password"  maxlength="20" runat="server" class="text-box-2" />
                                <p>&nbsp;</p>
                                <span class="error"></span>&nbsp;<input type="text" id="newpassword" name="newpassword" value="New password"  maxlength="20" runat="server" class="text-box-2" />
                                <p>&nbsp;</p>
                                
								<div class="clear"></div>
								<p>&nbsp;</p>
								<p class="txtRgt">
                                <input type="submit" id="submitbuttonnext" value="NEXT" class="btn-next" /></p>
								<p>&nbsp;</p>
							</div>
						</article>
					</section>
					<div class="dot-line">&nbsp;</div>
				</article>
</asp:Content>


