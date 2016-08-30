<%@ Page Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="SGA.tna.MyProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!-- Custom Form -->
		<script type="text/javascript" src="../js/custom-form-elements.js"></script>
		
		<!-- Accordion Menu -->
		<script type="text/javascript" src="../js/jquery.min.js"></script>
		<!-- Popup -->
		<script type="text/javascript" src="../Scripts/jquery.colorbox.js"></script>
		<script type="text/javascript" src="../js/custom.js"></script>
<script type="text/javascript" language="javascript">
    

    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var alertHtml = '';
    var lastpage = 'n';
    var redirect = 'y';
    function gup(name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
			results = regex.exec(location.search);
        return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }
    function FinalSubmit() {
        var backUrl = gup("id");
        if (backUrl == "1") {
            window.location.href = '/tna/category-management-test.aspx';
        } else if (backUrl == "2") {
            window.location.href = '/tna/skills-self-test.aspx';
        } else if (backUrl == "3") {
            window.location.href = '/tna/behavioural-assessment-test.aspx';
        } else if (backUrl == "4") {
            window.location.href = '/tna/department-maturity-test.aspx';
        } else if (backUrl == "5") {
            window.location.href = '/tna/negotiation-profile-test.aspx';
        } else if (backUrl == "6") {
            window.location.href = '/tna/contract-management-assessment-test.aspx';
        }
        else {
            window.location.href = '/tna/default.aspx';
        }
    }

    function sentBack() {

        //window.location.href = '/tna/default.aspx';
    }

    function setPassword(parameters) {
        $('#<%=password.ClientID %>').val(parameters);
        $('#<%=passwordplain.ClientID %>').val(parameters);
        
    }

    $(document).ready(function () {
        $('#edit').click(function () {
            $('#<%=password.ClientID %>').removeAttr("disabled");
            $('#<%=password.ClientID %>').hide();
            $('#<%=passwordplain.ClientID %>').show();
        })

        var backUrl = gup("id");
        $('#submitbutton,#submitbuttonnext').colorbox({
            href: "../Popup.aspx",
            width: "392px",
            height: "450px",
            onComplete: function () {
                if (lastpage == 'y') {

                    if (($("#<%= txtPhoneNo.ClientID%>").val() == 'Phone') && ($("#<%= txtDivision.ClientID%>").val() == 'Division') && ($("#<%=ddlLocation.ClientID %>").val() == 0) && ($("#<%= txtPosition.ClientID%>").val() == 'Position') && ($("#<%=ddlGoods.ClientID %>").val() == 0)) {
                        redirect = "y";
                        $('#title').text("Confirmation");
                        $('#colorbox').css({ "display": "block" });
                        $('#alertMessage').html("We noticed you haven't completed all of the fields. Providing this information helps us to support your learning. <b>If you are unsure how to answer, then please choose the answer you think is the 'closest fit' for you. Would you like to complete the rest of the fields, or proceed to your training needs analysis?</b> ");
                        $('#btnCancel').css("display", "block");
                        $('#btnOk').removeClass("btn-yes");
                        $('#btnOk').addClass("btn-back");

                        $('#btnCancel').removeClass("btn-back");
                        $('#btnCancel').addClass("btn-proceed");
                    } else {
                        $('#colorbox').css({ "display": "none" });
                        window.location.href = '/tna/default.aspx';
                    }

                    /*if (backUrl.length == 0) {
                    $('#colorbox').css({ "display": "none" });
                    window.location.href = '/tna/default.aspx';
                    }
                    else {
                    redirect = "y";
                    $('#title').text("Confirmation");
                    $('#colorbox').css({ "display": "block" });
                    if (backUrl == "1") {
                    $('#alertMessage').text("You are about to begin the Category Management Challenge. This is a timed event and must be taken in a single sitting. Are you ready to begin?");
                    } else if (backUrl == "2") {
                    $('#alertMessage').text("We noticed you haven't completed all of the fields. Providing this information helps us to support your learning. <b>If you are unsure how to answer, then please choose the answer you think is the 'closest fit' for you. Would you like to complete the rest of the fields, or proceed to your training needs analysis?</b> ");
                    } else if (backUrl == "3") {
                    $('#alertMessage').text("You are about to begin the Leadership Assessment. This assessment must be taken in a single sitting. Are you ready to begin?");
                    } else if (backUrl == "4") {
                    $('#alertMessage').text("You are about to begin the Department Maturity Profile. This assessment must be taken in a single sitting. Are you ready to begin?");
                    } else if (backUrl == "5") {
                    $('#alertMessage').text("You are about to begin the Negotiation Profile. This assessment must be taken in a single sitting. Are you ready to begin?");
                    } else if (backUrl == "6") {
                    $('#alertMessage').text("You are about to begin the Contract Management Assessment. This assessment must be taken in a single sitting. Are you ready to begin?");
                    }

                    //$('#alertMessage').text("You are about to begin the Category Management Challenge. This is a timed event and must be taken in a single sitting. Are you ready to begin?");
                    $('#btnCancel').css("display", "block");
                    $('#btnOk').removeClass("btn-yes");
                    $('#btnOk').addClass("btn-back");

                    $('#btnCancel').removeClass("btn-back");
                    $('#btnCancel').addClass("btn-proceed");
                    //$('#btnCancel').addClass("btn-yes");
                    }*/

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
            var password = $('#<%=passwordplain.ClientID %>').val();

            var name = $('#<%=fname.ClientID %>').val();
            if (name == '' || name == 'First name') {
                error = 1;
                emptyFields.push('First name');
            }
            var surname = $('#<%=lname.ClientID %>').val();
            if (surname == '' || surname == 'Last name') {
                error = 1;
                emptyFields.push('Last name');
            }

            var agency = $("#<%=ddlAgency.ClientID %>").val();
            if (agency == 0) {
                error = 1;
                emptyFields.push('Your Organisation');
            }

            if (password == '') {
                error = 1;
                emptyFields.push('Password');
            }

            

            var MfirstName = $('#<%=MfirstName.ClientID %>').val();
            if (MfirstName == '' || MfirstName == "Your manager's first name") {
                error = 1;
                emptyFields.push("Your manager's first name");
            }
            var MlastName = $('#<%=MlastName.ClientID %>').val();
            if (MlastName == '' || MlastName == "Your manager's last name") {
                error = 1;
                emptyFields.push("Your manager's last name");
            }
            var Memail = $('#<%=Memail.ClientID %>').val();
            if (Memail == '' || Memail == "Your manager's email address") {
                error = 1;
                emptyFields.push("Your manager's email address");
            }
            var location = $("#<%=ddlLocation.ClientID %>").val();
            if (location == 0) {
                error = 1;
                emptyFields.push('Your Location');
            }
            if (error) {
                $('#colorbox').css({ "display": "block" });
                alertHtml = 'Please select/enter ' + emptyFields.join(', ');
            }
            else if (Memail != '' && !filter.test(Memail)) {
                $('#colorbox').css({ "display": "block" });
                alertHtml = 'Please enter valid manager email id';
            }
            else {
                alertHtml = 's';
                lastpage = 'y';
                var emailpass = 0;
                var manageDomain = Memail.replace(/.*@/, "");
                if (manageDomain.toLowerCase() == 'qld.gov.au') {
                    emailpass = 1;
                } else {
                    if (manageDomain.toLowerCase() == 'translink.com.au') {
                        emailpass = 1;
                    }
                    else if (manageDomain.toLowerCase() == 'tafeqld.edu.au') {
                        emailpass = 1;
                    }
                    else if (manageDomain.toLowerCase() == 'usq.edu.au') {
                        emailpass = 1;
                    }
                    else if (manageDomain.toLowerCase() == 'qcaa.qld.edu.au') {
                        emailpass = 1;
                    }
                    else {
                        if ((manageDomain.substr(manageDomain.length - 10).toLowerCase()) == 'qld.gov.au') {
                            emailpass = 1;
                        }
                        else if ((manageDomain.substr(manageDomain.length - 14).toLowerCase()) == 'tafeqld.edu.au') {
                            emailpass = 1;
                        }
                        else if ((manageDomain.substr(manageDomain.length - 10).toLowerCase()) == 'usq.edu.au') {
                            emailpass = 1;
                        }
                        else if ((manageDomain.substr(manageDomain.length - 15).toLowerCase()) == 'qcaa.qld.edu.au') {
                            emailpass = 1;
                        }
                        else {
                            if ((manageDomain.substr(manageDomain.length - 16).toLowerCase()) == 'translink.com.au') {
                                emailpass = 1;
                            }
                        }
                    }
                }

                if (parseInt(emailpass) == 0) {
                    lastpage = 'n';
                    $('#colorbox').css({ "display": "block" });
                    alertHtml = 'The Manager email should only be registered with one of these \n(qld.gov.au,\ntranslink.com.au,\ntafeqld.edu.au,\nusq.edu.au,\nqcaa.qld.edu.au)';
                }
                else {
                    var json =
                        $.ajax({
                            type: "POST",
                            async: false,
                            url: "MyProfile.aspx/UpdateProfile",
                            data: JSON.stringify({ 'fname': name, 'lname': surname, 'managerFirstname': MfirstName, 'managerLastName': MlastName, 'managerEmail': Memail, 'agencyId': agency, 'phone': $("#<%= txtPhoneNo.ClientID%>").val(), 'division': $("#<%= txtDivision.ClientID%>").val(), 'locationId': $("#<%=ddlLocation.ClientID %>").val(), 'position': $("#<%= txtPosition.ClientID%>").val(), 'goodsId': $("#<%=ddlGoods.ClientID %>").val(), 'password': password }),
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
            }
        });
    });
</script>
<article id="container">
					<section class="welcome-inner">
						<p class="title40-orange"><asp:Label ID="lblName" runat="server"></asp:Label></p>
						<p class="title40">Here is your profile</p>
					</section>
					<section class="color-box">
						<article class="info-box-shdw-inner">
							<div class="my-pofile-form">
								<p class="txt28 orange">We need a little more information before you begin!</p>
								<p>&nbsp;</p>
                                <p>Providing this information helps us to support your learning. If you are unsure how to answer, then please choose the answer you think is the 'closest fit' for you. </p>
								<p>&nbsp;</p>
                                <p class="txt18-bold">REGISTRATION DETAILS</p>
                                <span class="error"></span>&nbsp;&nbsp;<b>First Name</b><br />
                                <span class="error">*</span>&nbsp;<input type="text" id="fname" name="fname" maxlength="100"  runat="server" class="text-box-2" />
                                <p>&nbsp;</p>
                                <span class="error"></span>&nbsp;&nbsp;<b>Last Name</b><br />
                                <span class="error">*</span>&nbsp;<input type="text" id="lname" name="lname" maxlength="100" runat="server" class="text-box-2" />
                                <p>&nbsp;</p>
                                <span class="error"></span>&nbsp;&nbsp;<b>Your Job Role</b><asp:HyperLink ID="hylJobRole" CssClass="infolock" runat="server"><span>If you require your Role or your Level to be changed please contact support@comprara.com.au before continuing. In your email, please let us know the reason why you require the change.</span></asp:HyperLink><br />
                                <div class="form-box1">
                                <span class="error"></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlJobRole" class="styled"  runat="server">
                           <asp:ListItem Value="0">Please select</asp:ListItem>
                            <asp:ListItem Value="1">Procurement Officer</asp:ListItem>
<asp:ListItem Value="2">Procurement Analyst</asp:ListItem>
<asp:ListItem Value="3">Procurement Advisor</asp:ListItem>
<asp:ListItem Value="4">Procurement Specialist</asp:ListItem>
<asp:ListItem Value="5">Contract Manager</asp:ListItem>
<asp:ListItem Value="6">Contract Manager (including procurement)</asp:ListItem>
<asp:ListItem Value="7">Category Manager</asp:ListItem>
<asp:ListItem Value="8">Procurement Director</asp:ListItem>
                            </asp:DropDownList>
                            
                                </div>
                                <span class="error"></span>&nbsp;&nbsp;<b>Level of role best described as</b><asp:HyperLink ID="hylJobLevel" CssClass="infolock" runat="server"><span>If you require your Role or your Level to be changed please contact support@comprara.com.au before continuing. In your email, please let us know the reason why you require the change.</span></asp:HyperLink><br />
                                <div class="form-box1">
									<span class="error"></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlJobLevel" class="styled" runat="server">
                           <asp:ListItem Value="0">Please select</asp:ListItem>
                            <asp:ListItem Value="1">Graduate</asp:ListItem>
<asp:ListItem Value="2">Officer</asp:ListItem>
<asp:ListItem Value="3">Advisor</asp:ListItem>
<asp:ListItem Value="4">Senior advisor</asp:ListItem> 
<asp:ListItem Value="5">Operational Leader</asp:ListItem> 
<asp:ListItem Value="6">Director</asp:ListItem>
<asp:ListItem Value="7">Executive Level</asp:ListItem> 
                            </asp:DropDownList>
                                    
								</div>
                                
                                 <div class="form-box1">
									<span class="error"></span>&nbsp;&nbsp;<b>Manager's First Name</b><br />
                                    <span class="error">*</span>&nbsp;<input type="text" class="text-box-2" runat="server" title="Your manager's first name" id="MfirstName"/>
								</div>
								<div class="form-box1">
                                    <span class="error"></span>&nbsp;&nbsp;<b>Manager's Last Name</b><br />
									<span class="error">*</span>&nbsp;<input type="text"  class="text-box-2" runat="server" title="Your manager's last name"  id="MlastName"/>
                                    
								</div>
                                <div class="form-box1">
									<span class="error"></span>&nbsp;&nbsp;<b>Manager's Email Address</b><br />
                                    <span class="error">*</span>&nbsp;<input type="text" class="text-box-2" runat="server" title="Your manager's email address"  id="Memail"/>
								</div>
                                 <p>&nbsp;</p>
                                 <p class="txt18-bold">LOGIN DETAILS</p>
                               
                                <span class="error"></span>&nbsp;&nbsp;<b>Your Email</b><br />
                                <span class="error">*</span>&nbsp;<input type="text" id="email" name="email" disabled="disabled" readonly="readonly" maxlength="250" runat="server" class="text-box-2" />
                                <p>&nbsp;</p>
                                <span class="error"></span>&nbsp;&nbsp;<a href="javascript:void('0')" id="edit">Edit Password</a>
                                <br />
                                <span class="error">*</span>&nbsp;<input type="text" id="passwordplain" name="passwordplain"  maxlength="20" runat="server" style="display:none" class="text-box-2" /><input type="password" id="password" name="password" disabled="disabled" maxlength="20" runat="server" class="text-box-2" />
                                
                                
                                <p>&nbsp;</p>
                                <p class="txt18-bold">MY DETAILS</p>
                                <span class="error"></span>&nbsp;&nbsp;<b>Your Organisation</b><br />
								<div class="form-box1">
									<span class="error">*</span>&nbsp;<asp:DropDownList ID="ddlAgency" class="styled" runat="server">
                            <asp:ListItem Value="0">Your Organisation</asp:ListItem>
                            <asp:ListItem Value="1">Premier and Cabinet</asp:ListItem>
<asp:ListItem Value="2">Aboriginal and Torres Strait Islander Partnerships</asp:ListItem>
<asp:ListItem Value="3">Agriculture and Fisheries</asp:ListItem>
<asp:ListItem Value="4">Communities, Child Safety and Disability Services</asp:ListItem>
<asp:ListItem Value="5">Education and Training</asp:ListItem>
<asp:ListItem Value="6">Energy and Water Supply</asp:ListItem>
<asp:ListItem Value="7">Environment and Heritage Protection</asp:ListItem>
<asp:ListItem Value="8">Health</asp:ListItem>
<asp:ListItem Value="9">Housing and Public Works</asp:ListItem>
<asp:ListItem Value="10">Infrastructure, Local Government and Planning</asp:ListItem>
<asp:ListItem Value="11">Justice and Attorney-General</asp:ListItem>
<asp:ListItem Value="12">National Parks, Sport and Racing</asp:ListItem>
<asp:ListItem Value="13">Natural Resources and Mines</asp:ListItem>
<asp:ListItem Value="14">Police, Fire and Emergency Services</asp:ListItem>
<asp:ListItem Value="15">Science, Information Technology and Innovation</asp:ListItem>
<asp:ListItem Value="16">State Development</asp:ListItem>
<asp:ListItem Value="17">Transport and Main Roads</asp:ListItem>
<asp:ListItem Value="18">Treasury</asp:ListItem>
<asp:ListItem Value="19">Tourism, Major Events, Small Business and the Commonwealth Gamesv</asp:ListItem>
<asp:ListItem Value="20">Other</asp:ListItem>
                            </asp:DropDownList>
                                    
								</div>
                                <span class="error"></span>&nbsp;&nbsp;<b>Your Division</b><br />
                                <span class="error">&nbsp;</span>&nbsp;<input type="text" id="txtDivision" name="txtDivision" title="Division"  maxlength="250" runat="server" class="text-box-2" />
                                <p>&nbsp;</p>
                                <span class="error"></span>&nbsp;&nbsp;<b>Your Location</b><br />
                                <div class="form-box1">
									<span class="error">*</span>&nbsp;<asp:DropDownList ID="ddlLocation" class="styled" runat="server">
                            <asp:ListItem Value="0">Location</asp:ListItem>
                            <%--<asp:ListItem Value="1">Brisbane </asp:ListItem>
<asp:ListItem Value="2">Central West</asp:ListItem>
<asp:ListItem Value="3">Darling Downs</asp:ListItem>
<asp:ListItem Value="4">Far North</asp:ListItem>
<asp:ListItem Value="5">Fitzroy</asp:ListItem>
<asp:ListItem Value="6">Gold Coast</asp:ListItem>
<asp:ListItem Value="7">Mackay</asp:ListItem>
<asp:ListItem Value="8">North West</asp:ListItem>
<asp:ListItem Value="9">Northern</asp:ListItem>
<asp:ListItem Value="10">South West</asp:ListItem>
<asp:ListItem Value="11">Sunshine Coast</asp:ListItem>
<asp:ListItem Value="12">West Moreton</asp:ListItem>
<asp:ListItem Value="13">Wide Bay-Burnett</asp:ListItem>--%>
<asp:ListItem Value="1">Brisbane </asp:ListItem>
<asp:ListItem Value="2">South West</asp:ListItem>
<asp:ListItem Value="3">Sunshine Coast</asp:ListItem>
<asp:ListItem Value="4">Gold Coast</asp:ListItem>
<asp:ListItem Value="5">Fitzroy</asp:ListItem>
<asp:ListItem Value="6">Mackay</asp:ListItem>
<asp:ListItem Value="7">Northern</asp:ListItem>
<asp:ListItem Value="8">North West</asp:ListItem>
<asp:ListItem Value="9">Wide Bay-Burnett</asp:ListItem>
<asp:ListItem Value="10">Far North Queensland</asp:ListItem>
<asp:ListItem Value="11">Darling Downs</asp:ListItem>
<asp:ListItem Value="12">Far North</asp:ListItem>
<asp:ListItem Value="13">South West Queensland</asp:ListItem>
<asp:ListItem Value="14">Northern Queensland</asp:ListItem>
                            </asp:DropDownList>
                                    
								</div>
                                <span class="error"></span>&nbsp;&nbsp;<b>Your Position</b><br />
                                <span class="error">&nbsp;</span>&nbsp;<input type="text" id="txtPosition" name="txtPosition" title="Position" maxlength="250" runat="server" class="text-box-2" />
								<p>&nbsp;</p>
                                <span class="error"></span>&nbsp;&nbsp;<b>What is the Nature of the goods/services that you most commonly procure, or manage contracts for?</b><br />
                                <div class="form-box1">
									<span class="error">&nbsp;</span>&nbsp;<asp:DropDownList ID="ddlGoods" class="styled" runat="server">
                            <asp:ListItem Value="0">Please select</asp:ListItem>
                            <asp:ListItem Value="1">Building Construction and Maintenance</asp:ListItem>
<asp:ListItem Value="2">General Goods and Services</asp:ListItem>
<asp:ListItem Value="3">ICT</asp:ListItem>
<asp:ListItem Value="4">Medical </asp:ListItem>
<asp:ListItem Value="5">Social Services</asp:ListItem>
<asp:ListItem Value="6">Transport Infrastructure & Services</asp:ListItem>


                            </asp:DropDownList>
                                    
								</div>
                                <span class="error"></span>&nbsp;&nbsp;<b>Phone No</b><br />
                                <span class="error">&nbsp;</span>&nbsp;<input type="text" id="txtPhoneNo" name="txtPhoneNo" title="Phone" maxlength="20" runat="server" class="text-box-2" />
								
								
								<div class="clear"></div>
								<p>&nbsp;</p>
								<p class="txtRgt"><input type="submit" id="submitbutton" value="BACK" class="btn-save"   /> 
                                <input type="submit" id="submitbuttonnext" value="NEXT" class="btn-next" /></p>
								<p>&nbsp;</p>
							</div>
						</article>
					</section>
					<div class="dot-line">&nbsp;</div>
				</article>
</asp:Content>
