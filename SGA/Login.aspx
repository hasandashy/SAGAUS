<%@ Page Title="SAGOV Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SGA.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #home-data-container{ background:#F0F0F0; padding:30px;}
.home-title{font-family: Arial, Helvetica, sans-serif; font-size:36px; text-align:center; margin-bottom:20px;}
.home-shadow-box{ width:48.5%; background:#fff; -moz-box-shadow:2px 2px 4px #666; -webkit-box-shadow:2px 2px 4px #666; box-shadow:2px 2px 4px #666; float:left; margin-bottom:25px; font-size:14px;}
.home-shadow-box ul {list-style: initial;margin: 10px 0 0 13px;padding: 0;}
.home-shadow-box ul li {margin: 0 0 3px;padding: 0 0 0 6px;}
.home-box-title{ border-bottom:4px solid #005EB8; padding:10px 20px; font-family:Arial, Helvetica, sans-serif; font-size:21px;}
.paper-img{ float:right; margin:15px 0 0 15px; width:153px; height:131px; background:url(../images/paper_img.png) left top no-repeat;}
.home-box-form{ float:left; margin:5px 0; width:100%}
.home-box-form span{ float:left; width:35%}
.home-box-form input.txt-field {font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#231f20;  padding:4px 8px; width:60%; float:left;  border:1px solid #9c9b9c; -moz-border-radius:6px; -webkit-border-radius:6px; border-radius:6px; -moz-box-shadow:1px 1px 2px #999; -webkit-box-shadow:1px 1px 2px #999; box-shadow:1px 1px 2px #666}
.signin-box .home-shadow-box{max-width:328px; width:98%; float:none; margin:auto; margin-top:20px; margin-bottom:20px; 
box-sizing: border-box; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-shadow:none;}

.signin-box .home-box-form span {
	float: left;
	width: 100%;
	padding-bottom: 2px; box-sizing: border-box; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; color:#333333; font-weight:bold;
}

.signin-box .home-box-form input.txt-field{box-sizing: border-box; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; 
width:100%; margin-bottom:8px; border-radius:0 !important; box-shadow: none;
height: 36px;}
.signin-box .home-box-form input.txt-field  {

	line-height: 1.6;
	color: #555555;
	border: 1px solid #cccccc;
	border-radius: 0;
	-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
	box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
	-webkit-transition: border linear 0.2s, box-shadow linear 0.2s;
	-moz-transition: border linear 0.2s, box-shadow linear 0.2s;
	-o-transition: border linear 0.2s, box-shadow linear 0.2s;
	transition: border linear 0.2s, box-shadow linear 0.2s;
	
}

.signin-box .home-box-form input.txt-field:focus {
	border-color: #66afe9;
	outline: 0;
	-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(102, 175, 233, 0.6);
	box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(102, 175, 233, 0.6);
}

.signin-box .btn-default{width:100%; text-align:center;  font-weight:normal; padding:8px; font-size:16px; background:#0072bc; border:0; border-radius:0;}
.signin-box .btn-default:hover{color:#fff;}

.remember-me{ padding-bottom:15px;}

.remember-me span {
	top: 2px;
	position: relative;
	left: 4px; font-weight:bold;
}

.reset-password{ text-align:left; padding-top:25px; float:left; width:100%;}
.reset-password a { color:#0072bc; text-decoration:underline;}
.reset-password a:hover{ text-decoration:none;}

.signin-txt{ text-align:left; float:none; width:100%; max-width:760px; width:100%; margin:auto; padding-top:0px; text-align:center;}
.signin-txt span { display:block; font-weight:bold; color:#4a4344; padding-top:15px; display:inline-block; }
.signin-box{ background:#fff !important; border-bottom:8px solid #e4e4e4;}
.sign-in-box {
	width: 250px;
	float: right;
	position: absolute;
	right: 30px;
	top: 10px;
}
.sign-in-box .title {
    font-size: 16px;
    font-weight: bold;
    text-align: center;
    margin: 0 0 5px 0;
    width: 100%;
    float: left;
}
	.sign-in-box .form-group {
	width: 100%;
	float: left;
	margin: 0 0 6px 0;
}
	.sign-in-box .form-group label { font-size:13px; font-weight:bold; padding:0 0 2px 0; display:block;}

	.remember-me{ font-size:12px; font-weight:bold;}
.btnSubmit {
	background: #116db1;
	border: 0;
	padding: 5px 8px;
	color: #fff;
	text-transform: uppercase;
	font-size: 16px;
	width: 100%;
	display: inline-block;
	cursor: pointer;
}
	.btnSubmit:hover{background:#116db1;}
.sign-in-box a {
	color: #116db1;
	text-decoration: underline;
	width: 43%;
	float:right;
	padding: 3px 0 0 0;
	display: inline-block;
	font-size: 12px;
}
	.sign-in-box a:hover{ color:#000;}
	
	.sign-in-box .form-group .form-control {

	line-height: 1.6;
	color: #555555;
	border: 1px solid #cccccc;
	border-radius: 0;
	-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
	box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
	-webkit-transition: border linear 0.2s, box-shadow linear 0.2s;
	-moz-transition: border linear 0.2s, box-shadow linear 0.2s;
	-o-transition: border linear 0.2s, box-shadow linear 0.2s;
	transition: border linear 0.2s, box-shadow linear 0.2s;
	
}

.sign-in-box .form-group .form-control:focus {
	border-color: #66afe9;
	outline: 0;
	-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(102, 175, 233, 0.6);
	box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075), 0 0 8px rgba(102, 175, 233, 0.6);
}
.sign-in-box .form-group .form-control{box-sizing: border-box; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; 
width:100%; margin-bottom:0px; border-radius:0 !important; box-shadow: none; padding: 6px 12px;
height: 28px;}
select.styled {
position: relative !important;
width: 100% !important;
opacity: 1 !important;
filter: alpha(opacity=1) !important;
z-index: 5 !important;
min-height: auto !important;
box-shadow: 1px 1px 2px #666 !important;
border-radius: 6px !important;
border: 1px solid #d9d8d6 !important;
padding: 4px 4px !important;
}
.text-center {
    text-align: center;
}
.mrg-bt-10 {
    margin-bottom: 10px;
}
    </style>
    <script type="text/javascript" >
        $(function () {
            Custom.init();
            var details = Get_Cookie('logindetails');
            if (details != null) {
                var strArr = details.split(':');
                if (strArr.length > 0) {
                    $('#<%=txtUserName.ClientID %>').val(strArr[0]);
                    $('#<%=txtPassword.ClientID %>').val(strArr[1]);
                }
            }
        });
    
        var loginHtml = '';
        $(document).ready(function () {
            $('#<%=homehide.ClientID %>').colorbox({
                href: "Popup.aspx",
                width: "608px",
                height: "400px",
                onComplete: function () {
                    if (loginHtml == 's') {
                        $('#colorbox').css({ "display": "none" });
                        window.location.href = 'index.aspx';
                    } else {
                        $('#alertTitle').html("UNSUCCESSFUL LOGIN");
                        $('#alertMessage').text(loginHtml);
                    }
                }
            });
            $('#<%=homehide.ClientID %>').click(function () {
                var error = 0;
                var emptyFields = new Array();
                var uname = $('#<%=txtUserName.ClientID %>').val();
                if (uname == '' || uname == 'Username') {
                    error = 1;
                    emptyFields.push('Username');
                }
                var password = $('#<%=txtPassword.ClientID %>').val();
                if (password == '') {
                    error = 1;
                    emptyFields.push('Password');
                }
                var checked = false;
                if ($("#chkRemember1").is(':checked')) {
                    checked = true;
                }

                if (error) {
                    $('#colorbox').css({ "display": "block" });
                    loginHtml = 'Please enter ' + emptyFields.join(', ');
                } else {
                    $.ajax({
                        type: "POST",
                        async: false,
                        url: "About_us.aspx/Login",
                        data: JSON.stringify({ 'username': uname, 'password': password, 'rememberMe': checked }),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d == 's') {
                            
                                if (checked == true) {
                                    Set_Cookie('logindetails', uname + ':' + password, '', '/', '', '');
                                } else {
                                    Delete_Cookie('logindetails', '/', '');
                                }
                                loginHtml = 's';
                            } else if (data.d == 'e') {
                                $('#colorbox').css({ "display": "block" });
                                loginHtml = 'Your account has expired; email the AHRI’s Learning & Development Team at education@ahri.com.au requesting your account reactivation.';
                            } else if (data.d == 'i') {
                                $('#colorbox').css({ "display": "block" });
                                loginHtml = 'Your username and/or password was entered incorrectly. If you cannot remember these details, click \'Forgotten Password\' on the homepage and we will resend them to you. ';
                            } else if (data.d == 'u') {
                                $('#colorbox').css({ "display": "block" });
                                loginHtml = 'Your username and/or password was entered incorrectly. If you cannot remember these details, click \'Forgotten Password\' on the homepage and we will resend them to you. ';
                            }
                        }
                    });
                }
            });
        });
    </script>
    <article id="home-data-container" class="signin-box">
    	<p class="home-title">SIGN IN </p>
		
		<div class="text-center mrg-bt-10"><p>Sign in to access your Training Needs Analysis, and your reports. </p></div>

        
        
       
       
        

<div class="home-shadow-box ">
        
            <div>
	        <asp:Panel ID="pnlLogin" runat="server" DefaultButton="homehide">
            <div class="pad-20">
            <p><div class="home-box-form"><span>Username</span>
                <asp:TextBox ID="txtUserName" runat="server"  MaxLength="100" CssClass="txt-field"   ></asp:TextBox>
                </div></p>
            <p><div class="home-box-form"><span>Password</span><asp:TextBox ID="txtPassword" runat="server" MaxLength="20" TextMode="Password" CssClass="txt-field"  ></asp:TextBox></div></p>
             
           <p class="remember-me"><input id="chkRemember1" name="chkRemember1" type="checkbox"  /> <span>Remember me</span></p>
		   
            <asp:Button id="homehide" runat="server" name="homehide" Text="Sign In" class="btn-default floatR" />
			<%-- <p class="reset-password"><a id="reset-form" href="Forgot_Password.aspx">Reset my Password</a></p>--%>
            <div class="clear"></div>
            </div>
            </asp:Panel>
</div>
        </div>


	<%--	<p class="signin-txt">If do not yet have an AHRI profile, please <a href="https://secure.ahri.com.au/AHRI_Core_Content/Contacts/Sign_In.aspx?LoginRedirect=true&returnUrl=%2fSSO%2fSSOLanding.aspx%3fWebsiteKey%3d3ebe5e6a-bf0f-427f-9784-418e70781f78%26ssofrom%3dhttps%253a%252f%252fwww.ahri.com.au%252f" target="_blank">click here</a> to create a new one.--%>
          
	<%--	<br /><br />
		Having trouble? Please contact AHRI on (03) 9918 9200 </p>
		--%>
        
        
        <div class="clear"></div>
    </article>
</asp:Content>
