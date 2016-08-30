<%@ Page  Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="np-assessment-instructions.aspx.cs" Inherits="SGA.tna.np_assessment_instructions" %>
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
    function sentBack() {
        window.location.href = '/tna/default.aspx';
    }
    function FinalSubmit() {
        $('#colorbox').css({ "display": "none" });
        window.location.href = '/tna/negotiation-profile-test.aspx';
    }

    $(document).ready(function () {
        /*$('.my-profile').colorbox({
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
                    window.location.href = '/tna/myprofile.aspx?id=5';
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
                alertHtml = 'You are about to begin the Negotiation Profile. This assessment must be taken in a single sitting. Are you ready to begin?';
            }
        });*/
    });
</script>
<!-- Content Area start -->
				<article id="container">
					<section class="welcome">
						<p class="title40-orange"><asp:Label ID="lblName" runat="server"></asp:Label></p>
						<p class="title40">Welcome to the Negotiation Profile</p>
					</section>
					<div class="dot-line">&nbsp;</div>
					<section class="color-box">
						<article class="info-box-shdw-inner">
							<p class="title40">Instructions</p>
							<p>&nbsp;</p>
							<p class="txt16-bold">You are about to complete a  self-evaluation of your procurement skills. The aim of this exercise is to provide a systematic self analysis of your skill-set as it stands today.</p>
							<p>&nbsp;</p>
							<p class="txt30">01</p>
							<p>This self-assessment focuses on the skills required to perform the category management function. It focuses on the eight-step category management process and for each step you will be asked for nine (9) self evaluations.</p>
							<p>&nbsp;</p>
							<p class="txt16-bold">The 8 steps are:</p>
							<div class="step1">
								<p>1. Contextual</p>
								<p>2. Political</p>
								<p>3. Cultural</p>
								<p>4. Interpersonal factors</p>
							</div>
							<div class="step2">
								<p>5. Intentional</p>
								<p>6. Procedural issues</p>
								<p>7. Motivational</p>
								<p>8. Tactical</p>
							</div>
							<div class="clear"></div>
							<hr class="divider-line" />
							<p class="txt30">02</p>
							<p>Before you start the assessment you will be taken to your profile where we ask you several key questions regarding your role and your experience. This provides the context for your assessment. You can edit or update this information at any time by clicking 'My Profile' at the top of the page.</p>
							<hr class="divider-line" />
							<p class="txt30">03</p>
							<p>A self-assessment is simply a tool to evaluate oneself in a systemised and constructive manner. On completion of the assessment you will receive a PDF report of your results by email. You can use this report for your own records - as an evaluation of your skill set as it stands today.You can also use this report to facilitate discussion with your manager.</p>
							<p>&nbsp;</p>
							<p>&nbsp;</p>
                           
							<div class="floatR">
                            <a href="MyProfile.aspx?id=5"  class="update-profile">UPDATE PROFILE</a>
                            <a  id="hylProfile" runat="server" href="/tna/negotiation-profile-test.aspx" class="my-profile">BEGIN NOW</a></div>
							<div class="clear"></div>
							<p>&nbsp;</p>
							
							
						</article>
                        
                        
  					</section>
					<div class="dot-line">&nbsp;</div>
				</article>
				<!-- Content Area end // -->
</asp:Content>
