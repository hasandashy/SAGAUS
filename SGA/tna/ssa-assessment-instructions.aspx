<%@ Page Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="ssa-assessment-instructions.aspx.cs" Inherits="SGA.tna.ssa_assessment_instructions" %>
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
        window.location.href = '/tna/skills-self-test.aspx';
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
                        $('#alertMessage').html(alertHtml);
                        $('#btnCancel').css("display", "block");
                        $('#btnOk').removeClass("btn-yes");
                        $('#btnOk').addClass("btn-back");

                        $('#btnCancel').removeClass("btn-back");
                        //$('#btnCancel').addClass("btn-yes");
                        $('#btnCancel').addClass("btn-proceed");
                    }
                } else {
                    $('#colorbox').css({ "display": "none" });
                    window.location.href = '/tna/myprofile.aspx?id=2';
                }
            }
        });

        $('.my-profile').click(function () {
            var value = "<%=directSend %>";
            if (value == 0) {
                // show confirmation
                lastpage = 'y';
                alertHtml = "We noticed you haven't completed all of the fields. Providing this information helps us to support your learning. <b>If you are unsure how to answer, then please choose the answer you think is the 'closest fit' for you. Would you like to complete the rest of the fields, or proceed to your training needs analysis?</b> ";
            } else {
                // send to profile page
                lastpage = 'n';
            }
        });*/
    });
</script>
<!-- Content Area start -->
				<article id="container">
					<section class="welcome">
						<p class="title40-orange"><asp:Label ID="lblName" runat="server"></asp:Label></p>
						<p class="title40">Welcome to the Procurement Skills Self Assessment</p>
					</section>
					<div class="dot-line">&nbsp;</div>
					<section class="color-box">
						<article class="info-box-shdw-inner">
							<p class="title40">Instructions</p>
							<p>&nbsp;</p>
							<p class="txt16-bold">You are about to complete a self-assessment of your procurement skills. </p>
							<p>&nbsp;</p>
							<p class="txt30">01</p>
							<p>This self-assessment will explore the skills required to perform the end-to-end procurement function. It focuses on 8 phases of the procurement process and asks you to rate yourself across 72 capabilities in total. </p>
							<p>&nbsp;</p>
							<p class="txt16-bold">The 8 phases are:</p>
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
							<p>Please allow for around 30-45 minutes to complete your assessment. The assessment must be completed in one-sitting - if you do not complete the assessment to the end, you will need to start it again. It is therefore recommended that you complete this in a quiet space with no interruptions. </p>
							<hr class="divider-line" />
							<p class="txt30">03</p>
							<p>Once you complete the Training Needs Analysis you will receive an individual report with an assessment of your capability across the 8 phases of the procurement process. You will also receive your  Critical Skills Boost plan which will inform you on the e-Learning and workshop recommendation most relevant for you. </p>
							<p>&nbsp;</p>
							<p>&nbsp;</p>
                           
							<div class="floatR">
                            <a href="MyProfile.aspx?id=2"  class="update-profile">UPDATE PROFILE</a>
                            <a  id="hylProfile" runat="server" href="/tna/skills-self-test.aspx" class="my-profile">BEGIN NOW</a></div>
							<div class="clear"></div>
							<p>&nbsp;</p>
							
							
						</article>
                        
                        
  					</section>
					<div class="dot-line">&nbsp;</div>
				</article>
				<!-- Content Area end // -->
</asp:Content>

