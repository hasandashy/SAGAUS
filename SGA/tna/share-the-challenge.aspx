<%@ Page Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="share-the-challenge.aspx.cs" Inherits="SGA.tna.share_the_challenge" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript" src="../js/ddaccordion.js"></script>
<script type="text/javascript" src="../js/ddaccordion-menu.js"></script>
<script type="text/javascript" src="../js/custom-form-elements-load.js"></script>
<script type="text/javascript" src="../Scripts/jquery.colorbox.js"></script>
<script type="text/javascript" src="../js/custom.js"></script>
<script type="text/javascript" language="javascript">
    function FinalSubmit() {
        window.location.href = '/tna/my-results-reports.aspx';
    }
    $(function () {
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        $("#message").val($("#message").val().replace("{FromName}", "<%=_name %>"));

        

        var alertHtml = '';
        var alertTitle = '';

        $('#share').colorbox({
            href: "../Popup.aspx",
            width: "392px",
            height: "200px",
            onComplete: function () {
                $("#title").text(alertTitle);
                $('#alertMessage').text(alertHtml);
                $('#btnCancel').css("display", "block");
                $('#btnCancel').removeClass("btn-back");
                $('#btnCancel').addClass("btn-nothanks");
                $('#btnOk').removeClass("btn-ok");
                $('#btnOk').addClass("btn-yess");
            }
        });

        $("#fname").blur(function () {
            $("#message").val($("#message").val().replace("{ToName}", $(this).val()));
        });

        $('#share').click(function () {
            var error = 0;
            var emptyFields = new Array();
            var fname = $('#fname').val();
            if (fname == '' || fname == 'First name ...') {
                error = 1;
                emptyFields.push('First name');
            }
            var lname = $('#lname').val();
            if (lname == '' || lname == 'Last name ...') {
                error = 1;
                emptyFields.push('Last name');
            }
            var email = $('#email').val();
            if (email == '' || email == 'Email address ...') {
                error = 1;
                emptyFields.push('Email address');
            }
            var company = $('#company').val();
            if (company == '' || email == 'Company name ...') {
                error = 1;
                emptyFields.push('Company name');
            }

            var personType = "";

            $("input[name='-']:checked").each(function () {
                personType = $(this).val();
            });

            if (personType.length <= 0) {
                error = 1;
                emptyFields.push('person type');
            }

            if (error) {
                $('#colorbox').css({ "display": "block" });

                alertHtml = 'Please enter/select ' + emptyFields.join(', ');
            }
            else if (email != '' && !filter.test(email)) {
                $('#colorbox').css({ "display": "block" });
                alertHtml = 'Please enter valid email id';
            }
            else {
                var json =
                        $.ajax({
                            type: "POST",
                            async: false,
                            url: "share-the-challenge.aspx/SaveShareInfo",
                            data: JSON.stringify({ 'fname': fname, 'lname': lname, 'email': email, 'company': company, 'personType': personType, 'message': $('#message').val() }),
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                if (data.d == 's') {
                                    alertTitle = 'Success';
                                    $('#colorbox').css({ "display": "block" });
                                    alertHtml = 'You have shared the Category Management Challenge! Would you like to share it again with someone else?';
                                    $('#fname').val('First name ...');
                                    $('#lname').val('Last name ...');
                                    $('#email').val('Email address ...');
                                    $('#company').val('Company name ...');
                                    $('#message').val('Dear {ToName}\r\n\r\nI have just completed this insightful Category Management Challenge. I recommend you also take the challenge! \r\n\r\nRegards,\r\n\r\n{FromName}');
                                    $("#message").val($("#message").val().replace("{FromName}", "<%=_name %>"));
                                    $("input[name='-']").each(function () {
                                        $(this).removeAttr("checked");
                                    });
                                }
                            }
                        });
            }
        })
    });
</script>
<!-- Content Area start -->
				<article id="container">
					<section class="welcome-test">
						<p class="title40 floatL orange">Congratulations! <span class="txt20">You have completed your assessment.</span></p>
						
						<div class="clear"></div>
					</section>
					<div class="dot-line">&nbsp;</div>
					<section class="color-box">
						<article class="test-info-box">
							<p class="title orange">My Results</p>
							<p>&nbsp;</p>
                            <span>Below you will find the results for each assessment you have taken. In the left hand column, you will note the menu where you can easily navigate your bar-graphs. </span>
							<div class="clear"></div>
                            <br /><p><span class="dark">NOTE:</span> Your report will be delivered after the conclusion of the assessment period. To receive your report you need to complete all assessments assigned to you.</p>
						</article>
					</section>
					<section class="my-result-box">
						<article class="breadcrumb">
							<a href="#">Report Centre</a>&nbsp; &gt; &nbsp;<a href="#">Category Management Challenge</a>&nbsp; <span>&gt; &nbsp;Share</span>
						</article>
						<p>&nbsp;</p>
						<p>&nbsp;</p>
						<div class="my-result-container">
						<div class="col-lft">

                              
                                <% if(isTnaResult) { %>
                                  <p class="title18"><span id="spSkills" runat="server">Procurement Skills Self  <br />Assessment</span></p>
                                <div class="acrd-menu">
                                <p><a href="#" class="menuitem submenuheader">Display Results</a></p>
								<div class="submenu">
										<ul>
											<li><a href="my-results-bar-graph-ssa.aspx" >&bull; Bar Graph</a></li>
										</ul>
									</div>
                                </div>
                                <% } %>
                                
                                 <% if(isPkeResult) { %>
                                 <p class="title18"><span id="spPKE" runat="server">Procurement Knowledge   <br />Evaluation</span></p>
                                <div class="acrd-menu">
                                <p><a href="#" class="menuitem submenuheader">Display Results</a></p>
								<div class="submenu">
										<ul>
											<li><a href="my-result-bar-graph.aspx"  >&bull; Bar Graph</a></li>
										</ul>
									</div>
                                </div>
                                <% } %>
                               
                               
                                <% if (isCMAResult)
                                    { %>
                                 <p class="title18"><span id="spCMA" runat="server">Contract Management Self <br />Assessment</span></p>
                                <div class="acrd-menu">
                                <p><a href="#" class="menuitem submenuheader">Display Results</a></p>
								<div class="submenu">
										<ul>
											<li><a href="my-results-bar-graph-cma.aspx"   >&bull; Bar Graph</a></li>
										</ul>
									</div>
                                </div>
                                <% } %>
                               
                                
                               
                               
                               
								<% if (isCmkResult)
                                { %>
                                 <p class="title18"><span id="spCMK" runat="server">Contract Management Knowledge <br /> Evaluation</span></p>
                                <div class="acrd-menu">
									<p><a href="#" class="menuitem submenuheader">Display Results</a></p>
									<div class="submenu">
										<ul>
											<li><a href="my-results-bar-graph-cmk.aspx" >&bull; Bar Graph</a></li>
										</ul>
									</div>
								</div>
								<% } %>        
                                <% if (isCaaResult)
                                { %>
                                 <p class="title18"><span id="spCaa" runat="server">Commercial Awareness Knowledge <br /> Evaluation</span></p>
                                <div class="acrd-menu">
									<p><a href="#" class="menuitem submenuheader">Display Results</a></p>
									<div class="submenu">
										<ul>
											<li><a href="my-results-bar-graph-caa.aspx" >&bull; Bar Graph</a></li>
										</ul>
									</div>
								</div>
								<% } %>                         
							</div>
							<div class="col-cnt">
								<div class="share-challange-2">
									<div class="cloud-1"><img src="/innerimages/img-cloud-2.gif" alt="" /></div>
									<p class="share-title-1">SHARE THE</p>
									<p class="share-title-2">CHALLENGE</p>
									<p class="share-title-3">WITH A COLLEAGUE OR PEER!</p>
								</div>
								<p>&nbsp;</p>
								<p>&nbsp;</p>
								<div class="wide578">
									<p class="mrg-bt-5">Aggregate data from this challenge will provide an important insight into 'Category Management Capability' in Australia.</p>
									<p class="txt16-bold mrg-bt-5">The richer the data - the greater the insights.</p>
									<p>Please send this on to peers, colleagues, subordinates or your manager. In the field to the right you can edit your email to each person so you know exactly what is being sent. Thanks!</p>	
									<p>&nbsp;</p>
									<p>&nbsp;</p>
									<div class="wide278 floatL">
										<div class="form-box2">
											<input type="text" id="fname" name="fname" value="First name ..." title="First name ..." maxlength="100" class="text-box-1" />
										</div>
										<div class="form-box2">
											<input type="text" id="lname" name="lname" value="Last name ..." title="Last name ..." maxlength="100" class="text-box-1" />
										</div>
										<div class="form-box2">
											<input type="text" id="email" name="email" value="Email address ..." title="Email address ..." maxlength="250" class="text-box-1" />
										</div>
										<div class="form-box2">
											<input type="text" id="company" name="company" value="Company name ..." title="Company name ..." maxlength="100" class="text-box-1" />
										</div>
										<p>&nbsp;</p>
										<p class="dark mrg-bt-5">This person is my:</p>
										<div class="chkbx-box-1">
											<input type="radio" name="-" value="1" class="styled" /> Manager
										</div>
										<div class="chkbx-box-1">
											<input type="radio" name="-" value="2" class="styled" /> Peer
										</div>
										<div class="chkbx-box-1">
											<input type="radio" name="-" value="3" class="styled" /> Subordinate
										</div>
										<div class="chkbx-box-1">
											<input type="radio" name="-" value="4" class="styled" /> Industry Colleague
										</div>
									</div>
									<div class="wide278 floatR">
										<textarea cols="1" rows="10" id="message" name="message" class="text-box-1">
Dear {ToName}

I have just completed this insightful Category Management Challenge. I recommend you also take the challenge! 

Regards,

{FromName}
										</textarea>
										<p>&nbsp;</p>
										<p class="txtRgt"><input type="submit" id="share" value="SHARE" class="btn-share" /></p>
										<p class="mrg-bt-5">&nbsp;</p>
										<p class="floatR txt14"><%--<a id="sendResult" href="#"><span class="icon-email">Send results<br />to my email</span></a>--%></p>
										<div class="clear"></div>
									</div>
									<div class="clear"></div>
								</div>
								<p>&nbsp;</p>
							</div>
							<div class="clear"></div>
							<p>&nbsp;</p>
						</div>
					</section>
					<div class="dot-line">&nbsp;</div>
				</article>
				<!-- Content Area end // -->
                <script type="text/javascript" language="javascript">
                    function StyleRadio() {
                        $('table.styled input:radio').addClass("styled");
                        Custom.init();
                    }
                 </script>
</asp:Content>

