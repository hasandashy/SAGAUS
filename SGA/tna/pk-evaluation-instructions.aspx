<%@ Page Title="" Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="pk-evaluation-instructions.aspx.cs" Inherits="SGA.tna.pk_evaluation_instructions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Custom Form -->
		<script type="text/javascript" src="../js/custom-form-elements.js"></script>
		
		<!-- Accordion Menu -->
		<script type="text/javascript" src="../js/jquery.min.js"></script>
		<script type="text/javascript" src="../js/ddaccordion.js"></script>
		<script type="text/javascript" src="../js/ddaccordion-menu.js"></script>
		<!-- Popup -->
		<script type="text/javascript" src="../Scripts/jquery.colorbox.js"></script>
		<script type="text/javascript" src="../js/custom.js"></script>

<script type="text/javascript" language="javascript">
    var alertHtml = '';
    var lastpage = 'n';
    var redirect = 'y';
    function sentBack() {
        window.location.href = '/tna/default.aspx';
    }
    function FinalSubmit() {
        $('#colorbox').css({ "display": "none" });
        window.location.href = '/tna/procurement-knowledge-evaluation-test.aspx';
    }

    $(document).ready(function () {
        $('.my-profile').colorbox({
            href: "../Popup.aspx",
            width: "392px",
            height: "450px",
            onComplete: function () {
                if (lastpage == 'y') {
                    $('#title').text("Confirmation");
                    if (alertHtml.length > 1) {
                        $('#colorbox').css({ "display": "block" });
                        $('#alertMessage').text(alertHtml);
                        $('#btnCancel').css("display", "block");
                        $('#btnOk').removeClass("btn-yes");
                        $('#btnOk').addClass("btn-back");
                        $('#btnCancel').removeClass("btn-back");
                        $('#btnCancel').addClass("btn-yes");
                    }
                } else {
                    $('#colorbox').css({ "display": "none" });
                    window.location.href = '/tna/myprofile.aspx?id=1';
                }
            }
        });

        $('.my-profile').click(function () {
            var value = "<%=directSend %>";
            if (value == 0) {
                // send to profile page
                lastpage = 'n';
            } else {
                // show confirmation
                lastpage = 'y';
                alertHtml = 'You are about to begin your Procurement Knowledge Evaluation. This is a timed event and must be taken in a single sitting. Are you ready to begin?';
            }
        });
    });
</script>

<!-- Content Area start -->
				<article id="container">
					<section class="welcome">
						<p class="title40-orange" style="text-align:center;"><asp:Label ID="lblName" runat="server"></asp:Label></p>
						<p class="title40" style="text-align:center;">Welcome to the Procurement Knowledge Evaluation</p>
					</section>
					<div class="dot-line">&nbsp;</div>
					<section class="color-box">
						<article class="info-box-shdw-inner">
							<p class="title40">Instructions</p>
							<p>&nbsp;</p>
							<p class="txt16-bold">You are about to complete an evaluation of your procurement knowledge.</p>
							<p>&nbsp;</p>
							<p class="txt30">01</p>
							<p>This evaluation focuses on the skills required to perform in procurement. It focuses on eight procurement dimensions and for each, you will be asked nine assessment questions.</p>
							<p>&nbsp;</p>
							<p class="txt16-bold">The 8 dimensions are:</p>
							<div class="step1">
								<p>1. Opportunity Analysis</p>
								<p>2. Market Analysis</p>
								<p>3. Strategy Development</p>
								<p>4. Market Engagement</p>
							</div>
							<div class="step2">
								<p>5. Negotiation</p>
								<p>6. Contract Implementation</p>
								<p>7. Supplier Relationship Management</p>
								<p>8. Strategy Refresh</p>
							</div>
							<div class="clear"></div>
							<hr class="divider-line" />
							<p class="txt30">02</p>
							<p>If you haven't already done so, please complete your profile. Here we ask you several key questions regarding your role and your experience. This provides the context for your assessment. You can edit or update this information at any time by clicking 'My Profile' at the top of the page.</p>
							<hr class="divider-line" />
							<p class="txt30">03</p>
							<p>On completion of the evaluation you will receive a PDF report of your results. This may happen automatically or at a later stage depending on what was agreed with your organisation. You can use this report for your own records - as an evaluation of your knowledge, as it stands today. You can also use this report to facilitate discussion with your manager.</p>
							<p>&nbsp;</p>
							<p>&nbsp;</p>
							<div class="floatR">
                            
                            
                            <a href="MyProfile.aspx?id=1"  class="update-profile">UPDATE PROFILE</a>
                            <a id="hylProfile"  runat="server" href="#" class="my-profile">BEGIN NOW</a>
                           </div>
							<div class="clear"></div>
							<p>&nbsp;</p>
							<div class="timed-task">
								<p class="title"><span>THIS IS A<br /><font class="orange">TIMED TASK!</font></span></p>
								<p>&nbsp;</p>
								<p>You are given 60 minutes to complete the evaluation. There are eight sections with nine questions each. We recommend that you allocate no more than 5 minutes for each section. This gives you 10 minutes to go back to the answers that you may want to revisit. When you click 'Next' you will be taken to 'My Profile' page.  Once you have completed your profile, click 'Next' and then the clock will start!  
                                <br /><br />
You will see the clock throughout the evaluation in the top right hand side of the page in orange! </p>
							</div>
						</article>
					</section>
					<div class="dot-line">&nbsp;</div>
				</article>
				<!-- Content Area end // -->
</asp:Content>
