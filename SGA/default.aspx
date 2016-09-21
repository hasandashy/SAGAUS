<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SGA._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        /*function gup(name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }
        var backUrl = gup("id");
        $("#register").focus();
        */
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        var alertHtml = '';
        $(document).ready(function () {

            $('#btnSend').colorbox({
                href: "Popup.aspx",
                width: "492px",
                height: "300px",
                onComplete: function () {
                    //$("#register").unmask();
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
                var email = $('#Email').val();
                if (email == '' || email == 'Email address') {
                    error = 1;
                    emptyFields.push('Email');
                }
                var agency = $("#<%=ddlAgency.ClientID %>").val();
                if (agency == 0) {
                    error = 1;
                    emptyFields.push('Your Organisation');
                }


                var jobRole = $("#<%=ddlJobRole.ClientID %>").val();
                if (jobRole == 0) {
                    error = 1;
                    emptyFields.push('Your Job ROLE is best described as');
                }


                if (($("#terms").is(':checked')) == false) {
                    error = 1;
                    emptyFields.push("\r\nPlease tick the checkbox to agree to the Terms and Conditions.");
                }

                if (error) {
                    $('#colorbox').css({ "display": "block" });
                    alertHtml = 'Please select/enter ' + emptyFields.join(', ');
                }
                else if (email != '' && !filter.test(email)) {
                    $('#colorbox').css({ "display": "block" });
                    alertHtml = 'Please enter valid email id';
                }
                else {
                    var emailpass = 0;
                    var domain = email.replace(/.*@/, "");


                    if (domain.toLowerCase() == 'sa.gov.au') {
                        emailpass = 1;
                    } else {
                        if (domain.toLowerCase() == 'courts.sa.gov.au') {
                            emailpass = 1;
                        }
                        else if (domain.toLowerCase() == 'police.sa.gov.au') {
                            emailpass = 1;
                        }
                        else if (domain.toLowerCase() == 'tafesa.edu.au') {
                            emailpass = 1;
                        }                       
                    }


                    if (parseInt(emailpass) == 0) {
                        $('#colorbox').css({ "display": "block" });
                        alertHtml = 'The email should only be registered with one of these \n(sa.gov.au,\ncourts.sa.gov.au,\npolice.sa.gov.au,\ntafesa.edu.au)';
                    }
                    else {
                        //$("#register").mask("Processing ...");
                        var json =
                            $.ajax({
                                type: "POST",
                                async: false,
                                url: "About_us.aspx/RegisterUser",
                                data: JSON.stringify({ 'fname': name, 'lname': surname, 'email': email, 'jobId': $("#<%=ddlJobRole.ClientID %>").val(), 'agencyId': agency }),
                                dataType: "json",
                                contentType: "application/json; charset=utf-8",
                                success: function (data) {
                                    if (data.d == 's') {
                                        $('#colorbox').css({ "display": "block" });
                                        alertHtml = "Thank you for registering. Your registration has been approved and in the next few minutes a confirmation email will be sent to your inbox. The email contains your username and password to login. Note: You may need to check your junk mail folder if this doesn't reach your inbox.";
                                        $('#FName').val('');
                                        $('#LName').val('');
                                        $('#Email').val('');
                                    } else if (data.d == 'e') {
                                        $('#colorbox').css({ "display": "block" });
                                        alertHtml = 'Some error occured in the process, Please try again.';
                                    } else if (data.d == 'u') {
                                        $('#colorbox').css({ "display": "block" });
                                        alertHtml = 'User name already exists.';
                                    } else if (data.d == 'd') {
                                        $('#colorbox').css({ "display": "block" });
                                        alertHtml = 'Email address already exists.';
                                    }
                                }
                            });
                        }

                    }
            });

        });
    </script>
    <!-- Banner start -->
    <div id="banner-home">
        <ul class="bxslider">
            <li>
                <img src="images/img-banner-01.jpg" /></li>
            <li>
                <img src="images/img-banner-06.png" /></li>
        </ul>
        <img src="images/banner-bottom.png" alt="banner" style="width: 100%; height: 100%" />
    </div>
    <!-- Banner end // -->

    <!-- Content Area start -->
    <article id="container">
                         <%-- <p>This program is designed to offer you real-world training across core procurement competencies.</p>
<p>Conducting your Training Needs Analysis is the first step. This  will provide you with a tailored report detailing your procurement learning plan and your opportunity to cross high-impact e-Learning information and industry-leading workshops.</p>
<p><strong>With this program, we're delivering the skills boost you've been waiting for!</strong></p>--%>

                  <div class="stepBoxes">
            <ul>
              <li>
                <h2>Step 1</h2>
                <p><strong>REGISTER yourself as a project participant.</strong></p>
                <p class="txtCtr"><img src="Images/imgStep1.jpg" alt="Step1" /></p>
                <p>Below is a Registration box. This is where you register to complete your Assessments.</p>
              </li>
              <li>
                <h2>Step 2</h2>
                <p><strong>COMPLETE your Assessment Pack.</strong></p>
                <p class="txtCtr"><img src="Images/imgStep2.jpg" alt="Step2" /></p>
                <p>Depending on your role you may need to take three assesments.</p>
              </li>
              <li>
                <h2>Step 3</h2>
                <p><strong>RECEIVE your Individual Feedback Report.</strong></p>
                <p class="txtCtr"><img src="Images/imgStep3.jpg" alt="Step3" /></p>
                <p>In it you will find suggestions to help you develop skills and knowledge in the areas identified for you.</p>
              </li>
              <li>
                <h2>Step 4</h2>
                <p><strong>REVIEW your 70:20:10 recommendations.</strong></p>
                <p class="txtCtr"><img src="Images/imgStep4.jpg" alt="Step4" /></p>
                <p>Consider your development priorities and consider how you can apply the suggestions in your workplace.</p>
              </li>
            </ul>
          </div>
                   
                  <div class="dot-line clear">&nbsp;</div>
                   
                   <div id="register" class="formArea">
                   		<h2 >REGISTER FOR THE PROGRAM TODAY!</h2>
                        <p style="text-align:center;">(If you have already registered, please login at the top of the page.)</p>
                   
                   <div class="form">
       					<h6>Your Details.</h6>
            <strong>We need some information from you to make sure we direct you to the most relevant assessment.</strong>
                          
                   		<ul>
                        	<li><span>*</span><input type="text" value="First name" title="First name" class="txt-field2" id="FName"/></li>
                        	<li><span>*</span><input type="text" value="Last name" title="Last name" class="txt-field2" id="LName"/></li>
                        	<li><span>*</span><input type="text" value="Email address" title="Email address" class="txt-field2" id="Email"/></li>
                        	<li><span>*</span><asp:DropDownList ID="ddlAgency"  runat="server">
                            <asp:ListItem Value="0">Your Organisation</asp:ListItem>
                          <asp:ListItem Value="1">Attorney-Generals Department</asp:ListItem>
                            <asp:ListItem Value="2">Courts Administration Authority</asp:ListItem>
                            <asp:ListItem Value="3">Department for Communities and Social Inclusion</asp:ListItem>
                            <asp:ListItem Value="4">Department for Correctional Services</asp:ListItem>
                            <asp:ListItem Value="5">Department for Education and Child Development</asp:ListItem>
                            <asp:ListItem Value="6">Department of Environment Water and Natural Resources</asp:ListItem>
                            <asp:ListItem Value="7">Department of Planning Transport and Infrastructure</asp:ListItem>
                            <asp:ListItem Value="8">Department of State Development</asp:ListItem>
                            <asp:ListItem Value="9">Department of the Premier and Cabinet </asp:ListItem>
                            <asp:ListItem Value="10">Department of Treasury and Finance</asp:ListItem>
                            <asp:ListItem Value="11">Primary Industries and Regions SA</asp:ListItem>
                            <asp:ListItem Value="12">SA Fire and Emergency Services Commission</asp:ListItem>
                            <asp:ListItem Value="13">SA Health</asp:ListItem>
                            <asp:ListItem Value="14">South Australia Police</asp:ListItem>
                            <asp:ListItem Value="15">South Australian Tourism Commission</asp:ListItem>
                            <asp:ListItem Value="16">TAFE SA</asp:ListItem>  

                            </asp:DropDownList></li>
                        	
                        	<li><span>*</span><asp:DropDownList ID="ddlJobRole" style="color:Red;"  runat="server">
                            <asp:ListItem Value="0">- Your Job ROLE is best described as. (**Please consider carefully)</asp:ListItem>
                            <asp:ListItem Value="1">Purchasing Officer</asp:ListItem>
<asp:ListItem Value="2">Procurement/ Purchasing Support</asp:ListItem>
<asp:ListItem Value="3">Procurement/ Purchasing Analyst</asp:ListItem>
<asp:ListItem Value="4">Procurement Officer/ Advisor</asp:ListItem>
<asp:ListItem Value="5">Procurement Specialist</asp:ListItem>
<asp:ListItem Value="6">Contract Manager</asp:ListItem>
 <asp:ListItem Value="7">Category Manager</asp:ListItem>
<asp:ListItem Value="8">Procurement Manager/ Director</asp:ListItem>
                            </asp:DropDownList> </li>                        	
                        	
                            <li><input type="checkbox" id="terms" name="terms" />  
                            By ticking this box, I agree to the <a href="Terms.aspx">terms and conditions</a> of this website and acknowledge how Comprara will protect my personal information while aggregating and analysing the data received.
                            </li>
                            <li class="txtRgt"><input type="submit" value="" class="btn-go2" id="btnSend" /></li>
                        </ul>
                   
                   
                   
                   
                   </div>
                   
                   
                   <div class="getBoard">
              <p></p>
              <img src="images/imgNote.png" alt="" />
              <p>&nbsp;</p>
             
              <p>The selections you make when registering are important as they directly affect the assesments you will gain access to.</p>
              <p>&nbsp;</p>
                       
              <p>The most important is to select the ROLE that best describes your current role.</p>
              
              <a href="/role_guidelines" title="">Click here first for further guidelines before selecting your role descriptor &gt;</a> </div>
                   
                   
                   </div>
                   
                   
                   
                <div class="clear">&nbsp;</div>
          <div class="orange-bar">Your individual assessment report will come packed with lots of ideas!</div>
          <p class="hide">&nbsp;</p>
                </article>

    <script>
        $(document).ready(function () {
            $(".iphonNav ul li").removeClass("active");
            //alert($(".iphonNav ul li").eq(4));
            $(".iphonNav ul li:eq(0)").addClass("active");
        });
    </script>
</asp:Content>

