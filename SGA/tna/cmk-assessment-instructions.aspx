<%@ Page Title="" Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="cmk-assessment-instructions.aspx.cs" Inherits="SGA.tna.cmk_assessment_instructions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../js/custom-form-elements.js"></script>
		
		<!-- Accordion Menu -->
		<script type="text/javascript" src="../js/jquery.min.js"></script>
		<!-- Popup -->
		<script type="text/javascript" src="../Scripts/jquery.colorbox.js"></script>
		<script type="text/javascript" src="../js/custom.js"></script>
<script type="text/javascript" language="javascript">
    /*var alertHtml = '';
    var lastpage = 'n';
    var redirect = 'y';
    function sentBack() {
        window.location.href = '/tna/default.aspx';
    }
    function FinalSubmit() {
        $('#colorbox').css({ "display": "none" });
        window.location.href = '/tna/contract-management-assessment-test.aspx';
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
                    window.location.href = '/tna/myprofile.aspx?id=6';
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
                alertHtml = 'You are about to begin the Contract Management Knowledge Evaluation. This is a timed event and must be taken in a single sitting. Are you ready to begin?';
            }
        });
    });*/
</script>
<!-- Content Area start -->
				<article id="container">
					<section class="welcome">
						<p class="title40-orange" style="text-align:center;"><asp:Label ID="lblName" runat="server"></asp:Label></p>
						<p class="title40" style="text-align:center;">Welcome to the Contract Management Knowledge Evaluation</p>
					</section>
					<div class="dot-line">&nbsp;</div>
					<section class="color-box">
						<article class="info-box-shdw-inner">
							<p class="title40">Instructions</p>
							<p>&nbsp;</p>
							<p class="txt16-bold">You are about to complete an evaluation of your commercial contract management capability. The aim of this exercise is to provide an independent assessment of your capabilities as they stand today.</p>
							<p>&nbsp;</p>
							<p class="txt30">01</p>
							<p>This assessment focuses on the capabilities required to perform commercial contract management. It focuses on the eight underpinning capabilities driving effective commercial contract management. You will be presented with a series of scenarios designed to explore your capability in each area.</p>
							<p>&nbsp;</p>
							<p class="txt16-bold">The eight capabilities are:</p>
							<div class="step1">
								<p>1.	Planning and Governance</p>
								<p>2.	Measuring Performance</p>
								<p>3.	Building Relationships</p>
								<p>4.	Review and Feedback</p>
							</div>
							<div class="step2">
								<p>5.	Sponsor Improvement</p>
								<p>6.	Administer the Contract</p>
								<p>7.	Managing Risk</p>
								<p>8.	Commercial Awareness</p>
							</div>
							<div class="clear"></div>
							<hr class="divider-line" />
							<p class="txt30">02</p>
							<p>Before you start the assessment you will be taken to your profile where we ask you several key questions regarding your role and your experience. This provides the context for your assessment. You can edit or update this information at any time by clicking 'My Profile' at the top of the page.</p>
							<hr class="divider-line" />
							<p class="txt30">03</p>
							<p>This assessment focuses on the capability required to perform commercial contract management and based on your responses to 24 questions, both profiles your current capability and provides recommendations for future development. On completion of the assessment you will receive a PDF report of your results by email. You can use this report for your own records and/or to facilitate a discussion with your manager.</p>
							<p>&nbsp;</p>
							<p>&nbsp;</p>
                           
							<div class="floatR">
                            <%--<a href="MyProfile.aspx?id=5"  class="update-profile">UPDATE PROFILE</a>--%>
                            <a  id="hylProfile" runat="server" href="cmk-relationsip-context.aspx" class="my-profile">BEGIN NOW</a></div>
							<div class="clear"></div>
							<p>&nbsp;</p>
							
							
						</article>
                        
                        
  					</section>
					<div class="dot-line">&nbsp;</div>
				</article>
				<!-- Content Area end // -->
</asp:Content>
