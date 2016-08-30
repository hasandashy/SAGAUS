<%@ Page Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="ba-assessment-instructions.aspx.cs" Inherits="SGA.tna.ba_assessment_instructions" %>
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
        window.location.href = '/tna/behavioural-assessment-test.aspx';
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
                    window.location.href = '/tna/myprofile.aspx?id=3';
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
                alertHtml = 'You are about to begin the Leadership Assessment. This assessment must be taken in a single sitting. Are you ready to begin?';
            }
        });*/
    });
</script>
<!-- Content Area start -->
				<article id="container">
					<section class="welcome">
						<p class="title40-orange"><asp:Label ID="lblName" runat="server"></asp:Label></p>
						<p class="title40">Welcome to the Leadership Assessment</p>
					</section>
					<div class="dot-line">&nbsp;</div>
					<section class="color-box">
						<article class="info-box-shdw-inner">
							<p class="title40">Instructions</p>
							<p>&nbsp;</p>
							<p class="txt16-bold">You are about to complete a self assessment of your Leadership Assessment. There is no 'good' or 'bad' profile - this provides an insight into your particular behavioural leanings and styles. </p>
							<p>&nbsp;</p>
							<p class="txt30">01</p>
							<p>This assessment provides a systematic self-analysis of your ability to both motivate, lead and empower a team of people and to effectively represent the procurement function within your organisation.  The assessment focused on eight phases of leadership and asks you to rate yourself across 72 capabilities in total.</p>
							<p>&nbsp;</p>
							<p class="txt16-bold">The 8 steps are:</p>
							<div class="step1">
								<p>1. Strategic Leader</p>
								<p>2. Building Trust</p>
								<p>3. Communicate and Engage</p>
								<p>4. Emotional Intelligence</p>
							</div>
							<div class="step2">
								<p>5. Endorse, Support and Develop</p>
								<p>6. Culture and Change Leader</p>
								<p>7. Relationship Management</p>
								<p>8. Coach and Mentor</p>
							</div>
							<div class="clear"></div>
							<hr class="divider-line" />
							
							<p class="txt30">02</p>
							<p>On completion of the assessment you will receive a PDF report of your results. You can use this report for your own records - as an evaluation of your behavioural styles. You can also use this report to facilitate discussion with your manager.</p>
							<p>&nbsp;</p>
							<p>&nbsp;</p>
							<div class="floatR">
                            <a href="MyProfile.aspx?id=3"  class="update-profile">UPDATE PROFILE</a>
                            <a  id="hylProfile" runat="server" href="/tna/behavioural-assessment-test.aspx" class="my-profile">BEGIN NOW</a></div>
							<div class="clear"></div>
							<p>&nbsp;</p>
							
						</article>
                        
                        
  					</section>
					<div class="dot-line">&nbsp;</div>
				</article>
				<!-- Content Area end // -->
</asp:Content>

