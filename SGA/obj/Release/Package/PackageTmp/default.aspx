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
            //ddlJobLevel--- Job level best described as -
            var jobLevel = $("#<%=ddlJobLevel.ClientID %>").val();
            if (jobLevel == 0) {
                error = 1;
                emptyFields.push('Level of role best described as');
            }

            var MfirstName = $('#MfirstName').val();
            if (MfirstName == '' || MfirstName == "Your manager's first name") {
                error = 1;
                emptyFields.push("Your manager's first name");
            }
            var MlastName = $('#MlastName').val();
            if (MlastName == '' || MlastName == "Your manager's last name") {
                error = 1;
                emptyFields.push("Your manager's last name");
            }
            var Memail = $('#Memail').val();
            if (Memail == '' || Memail == "Your manager's email address") {
                error = 1;
                emptyFields.push("Your manager's email address");
            }
            //Memail

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
                var memailpass = 0;
                var manageDomain = Memail.replace(/.*@/, "");
                var domain = email.replace(/.*@/, "");


                if (domain.toLowerCase() == 'qld.gov.au') {
                    emailpass = 1;
                } else {
                    if (domain.toLowerCase() == 'translink.com.au') {
                        emailpass = 1;
                    }
                    else if (domain.toLowerCase() == 'tafeqld.edu.au')
                    {
                        emailpass = 1;
                    }
                    else if (domain.toLowerCase() == 'usq.edu.au') {
                        emailpass = 1;
                    }
                    else if (domain.toLowerCase() == 'qcaa.qld.edu.au') {
                        emailpass = 1;
                    }
                    else {
                        if ((domain.substr(domain.length - 10).toLowerCase()) == 'qld.gov.au') {
                            emailpass = 1;
                        }
                        else if ((domain.substr(domain.length - 14).toLowerCase()) == 'tafeqld.edu.au') {
                            emailpass = 1;
                        }
                        else if ((domain.substr(domain.length - 10).toLowerCase()) == 'usq.edu.au') {
                            emailpass = 1;
                        }
                        else if ((domain.substr(domain.length - 15).toLowerCase()) == 'qcaa.qld.edu.au') {
                            emailpass = 1;
                        }
                        else {
                            if ((domain.substr(domain.length - 16).toLowerCase()) == 'translink.com.au') {
                                emailpass = 1;
                            }
                        }
                    }
                }


                if (manageDomain.toLowerCase() == 'qld.gov.au') {
                    memailpass = 1;
                } else {
                    if (manageDomain.toLowerCase() == 'translink.com.au') {
                        memailpass = 1;
                    }
                    else if (manageDomain.toLowerCase() == 'tafeqld.edu.au') {
                        memailpass = 1;
                    }
                    else if (manageDomain.toLowerCase() == 'usq.edu.au') {
                        memailpass = 1;
                    }
                    else if (manageDomain.toLowerCase() == 'qcaa.qld.edu.au') {
                        memailpass = 1;
                    }
                    else {
                        if ((manageDomain.substr(manageDomain.length - 10).toLowerCase()) == 'qld.gov.au') {
                            memailpass = 1;
                        }
                        else if ((manageDomain.substr(manageDomain.length - 14).toLowerCase()) == 'tafeqld.edu.au') {
                            memailpass = 1;
                        }
                        else if ((manageDomain.substr(manageDomain.length - 10).toLowerCase()) == 'usq.edu.au') {
                            memailpass = 1;
                        }
                        else if ((manageDomain.substr(manageDomain.length - 15).toLowerCase()) == 'qcaa.qld.edu.au') {
                            memailpass = 1;
                        }
                        else {
                            if ((manageDomain.substr(manageDomain.length - 16).toLowerCase()) == 'translink.com.au') {
                                memailpass = 1;
                            }
                        }
                    }
                }

                if (parseInt(emailpass) == 0) {
                    $('#colorbox').css({ "display": "block" });
                    alertHtml = 'The email should only be registered with one of these \n(qld.gov.au,\ntranslink.com.au,\ntafeqld.edu.au,\nusq.edu.au,\nqcaa.qld.edu.au)';
                }
                else if (parseInt(memailpass) == 0) {
                    $('#colorbox').css({ "display": "block" });
                    alertHtml = 'The Manager email should only be registered with one of these \n(qld.gov.au,\ntranslink.com.au,\ntafeqld.edu.au,\nusq.edu.au,\nqcaa.qld.edu.au)';
                }
                else {
                    //$("#register").mask("Processing ...");
                    var json =
                        $.ajax({
                            type: "POST",
                            async: false,
                            url: "About_us.aspx/RegisterUser",
                            data: JSON.stringify({ 'fname': name, 'lname': surname, 'email': email, 'jobId': $("#<%=ddlJobRole.ClientID %>").val(), 'jobLevel': $("#<%=ddlJobLevel.ClientID %>").val(), 'managerFirstname': MfirstName, 'managerLastName': MlastName, 'managerEmail': Memail, 'agencyId': agency }),
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                if (data.d == 's') {
                                    $('#colorbox').css({ "display": "block" });
                                    alertHtml = "Thank you for registering. Your registration has been approved and in the next few minutes a confirmation email will be sent to your inbox. The email contains your username and password to login. Note: You may need to check your junk mail folder if this doesn't reach your inbox.";
                                    $('#FName').val('');
                                    $('#LName').val('');
                                    $('#Email').val('');
                                    $('#MfirstName').val('');
                                    $('#MlastName').val('');
                                    $('#Memail').val('');
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
						<li><img src="images/img-banner-01.jpg" /></li>
						<li>
            <img src="images/banner2.jpg" />
            
            <div class="newtag"><img src="images/newtag.png"  alt="" /></div>
            <div class="abs1">
            <div class="bannerwidth1">
            <div class="role">Is procurement a small part of your overall role?</div>
            
            <div class="especially">We have something <span>especially </span>for YOU!</div>
            
            <div class="non-professionals">
            <span>Procurement for <br />Non-procurement Professionals </span> 
            
            <p><i><img src="images/tickicon.png"  alt="" /></i>E-learning</p>
            <p><i><img src="images/tickicon.png"  alt="" /></i>1/2 day workshop, or </p>
            <p><i><img src="images/tickicon.png"  alt="" /></i>Full day workshop</p>
            </div>
            
            <div class="learn-button"><a href="http://demoevents.criticalskillsboost.com/workshops/contract-management/procurement-for-non-procurement-practitioners/">LEARN MORE >></a></div>
                   </div>     
            </div>
            </li>
            
            
            
            <li>
            <img src="images/banner3.jpg" />
            <div class="newtag"><img src="images/newtag.png"  alt="" /></div>
            <div class="abs1">
            <div class="bannerwidth1">
            <div class="nego">What type of negotiator are you?</div>
            
            <div class="negotiation">Take the Negotiation Assessment <span><a href="/negotiation_assessment">Read more ...</a></span></div>
            
            <div class="plus-Advanced">
            <i>PLUS</i> Access to Advanced Negotiation Training
            </div>
            
            <div class="learn-button"><a href="http://demoevents.criticalskillsboost.com/workshops/sourcing/advanced-negotiation/">LEARN MORE >></a></div>
                   </div>     
            </div>
            
            
            
            </li>
                        <li><img src="images/img-banner-06.jpg" /></li>
					</ul>
                    <img src="images/banner-bottom.png" alt="banner" style="width:100%;height:100%" />
					<%--<div class="banner-caption">Critical Skills Boost Program</div>--%>
                    
				</div>
				<!-- Banner end // -->
                 
				<!-- Content Area start -->
                <article id="container">
                          <p>This program is designed to offer you real-world training across core procurement competencies.</p>
<p>Conducting your Training Needs Analysis is the first step. This  will provide you with a tailored report detailing your procurement learning plan and your opportunity to cross high-impact e-Learning information and industry-leading workshops.</p>
<p><strong>With this program, we're delivering the skills boost you've been waiting for!</strong></p>

                  <div class="stepBoxes">
                  	<ul>
              <li>
                <h2>Step 1</h2>
                <p><strong>REGISTER below to access your online Training Needs Analysis.</strong></p>
                <p><img src="Images/imgStep1.jpg" alt="Step1" /></p>
                <p>This self-assessment, is a simple tool for you to evaluate yourself in a systemised and constructive manner. Allow for around 30-40 minutes to complete your assessment/s.</p>
<p>On completion you will receive a report of your results with personal recommendations for you.</p>
              </li>
              <li>
                <h2>Step 2</h2>
                <p><strong>REVIEW your personalised report and consider the recommendations.</strong></p>
                <p><img src="Images/imgStep2.jpg" alt="Step2" /></p>
                <p>It details your e-Learning plan and high-priority workshops.The report is based on the 70:20:10 model of learning.</p>
<p>Discuss your training plan with your line manager.</p>
              </li>
              <li>
                <h2>Step 3</h2>
                <p><strong>ACCESS your e-Learning program by clicking on the button below.</strong></p>
                <p><img src="Images/imgStep3.jpg" alt="Step3" /></p>
                <p>Using your login credentials, access your e-Learning plan, videos, materials, articles and templates.</p>
<p>Applying what you have learnt will improve your skills.</p>
<p>&nbsp;</p>
<p><a href="http://sipm.upsidelms.com/sipmlive/login/ad_MC_CS63_LRN.jsp" target="_blank" title="Access e-Learning" class="learnmore">Access e-Learning</a></p>
              </li>
              <li>
                <h2>Step 4</h2>
                <p><strong>BOOK your training workshop.</strong></p>
                <p><img src="Images/imgStep4.jpg" alt="Step4" /></p>
                <p>Discuss with your line manager your training recommendations and agree which options will best meet you and your agencies needs and objectives.</p>
<p>Either your manager/agency will book the entire team on a particular course OR you can book yourself onto your recommended workshop.<br/>
<a href="http://demoevents.criticalskillsboost.com " title="More Info">More info&gt;</a>
</p>
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
<asp:ListItem Value="19">Tourism, Major Events, Small Business and the Commonwealth Games</asp:ListItem>
<asp:ListItem Value="20">Other</asp:ListItem>

                            </asp:DropDownList></li>
                        	
                        	<li><span>*</span><asp:DropDownList ID="ddlJobRole" style="color:Red;"  runat="server">
                            <asp:ListItem Value="0">- Your Job ROLE is best described as. (**Please consider carefully)</asp:ListItem>
                            <asp:ListItem Value="1">Procurement Officer</asp:ListItem>
<asp:ListItem Value="2">Procurement Analyst</asp:ListItem>
<asp:ListItem Value="3">Procurement Advisor</asp:ListItem>
<asp:ListItem Value="4">Procurement Specialist</asp:ListItem>
<asp:ListItem Value="5">Contract Manager</asp:ListItem>
<asp:ListItem Value="6">Contract Manager (including procurement)</asp:ListItem>
<asp:ListItem Value="7">Category Manager</asp:ListItem>
<asp:ListItem Value="8">Procurement Director</asp:ListItem>
                            </asp:DropDownList> </li>
                        	<li><span>*</span><asp:DropDownList ID="ddlJobLevel" runat="server">
                            <asp:ListItem Value="0">- Level of role best described as -</asp:ListItem>
                            <asp:ListItem Value="1">Graduate</asp:ListItem> 
<asp:ListItem Value="2">Officer</asp:ListItem>
<asp:ListItem Value="3">Advisor</asp:ListItem>
<asp:ListItem Value="4">Senior advisor</asp:ListItem> 
<asp:ListItem Value="5">Operational Leader</asp:ListItem> 
<asp:ListItem Value="6">Director</asp:ListItem>
<asp:ListItem Value="7">Executive Level</asp:ListItem> 
                            </asp:DropDownList></li>
                        	
                        	<li><span>*</span><input type="text" value="Your manager's first name" title="Your manager's first name" class="txt-field2" id="MfirstName"/></li>
                        	<li><span>*</span><input type="text" value="Your manager's last name" title="Your manager's last name" class="txt-field2" id="MlastName"/></li>
                        	<li><span>*</span><input type="text" value="Your manager's email address" title="Your manager's email address" class="txt-field2" id="Memail"/></li>
                            <li><input type="checkbox" id="terms" name="terms" /> By ticking this box, I acknowledge that my manager will be notified of my registration to this program. I agree to the terms of the website and I understand that the information (including my personal information) entered is held offshore. Note:  
                            The Training Needs Analysis and e-Learning is FREE for Queensland Government departments to access until 30 June 2017 (fully funded by Office of the Chief Adviser, Queensland Government Procurement, Department of Housing and Public Works).</li>
                            <li class="txtRgt"><input type="submit" value="" class="btn-go2" id="btnSend" /></li>
                        </ul>
                   
                   
                   
                   
                   </div>
                   
                   
                   <div class="getBoard">
<p>&nbsp;</p>
<img src="images/imgNote.png" alt="" />
<p>&nbsp;</p>
            
            <p>The selections you make are really important as they effect the training you get recommended.</p>
<p>&nbsp;</p>
<p>The most important of these is selecting the correct role descriptor. Select the one that is the closest to your current role, considering the key activities you normally do.</p>
<p>&nbsp;</p>
<a href="/role_guidelines" title="">Click here first for further guidelines before selecting your role descriptor &gt;</a>
<div class="clear">&nbsp;</div>
<div class="light-orange-bar">Is Procurement just a small part of your role?</div>
<p style="text-align:center;line-height: 20px;font-size:18px;"><b><a style="color:#000;" href="http://demoevents.criticalskillsboost.com/workshops/contract-management/procurement-for-non-procurement-practitioners/">CLICK HERE</a> to learn about</b></p>
<p style="text-align:center;line-height: 20px;font-size:18px;">Procurement for <br />non-procurement Professionals</p>            
                
               
              
              </div>
                   
                   
                   </div>
                   
                   
                   
                  <div class="clear">&nbsp;</div>
                   
                   
                  <div class="orange-bar">Your learning, your way, your future.</div>
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

