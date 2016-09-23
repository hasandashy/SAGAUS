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
							<p>This assessment focuses on eight dimensions that drive effective procurement.</p>
							<p>&nbsp;</p>
							<p class="txt16-bold">The eight dimensions are:</p>
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
                            <p>&nbsp;</p>
                            <p>As you work through the assessment you may find themes that are not directly relevant to your current role. This is likely to happen for many people as this diagnostic considers a broad range of procurement capabilities. It has purposefully been designed not to be role specific, instead it seeks to understand the South Australian Government procurement capability as a whole.</p>
							<div class="clear"></div>
							<hr class="divider-line" />
							<p class="txt30">02</p>
							<p>For each procurement dimension you will be asked nine multiple choice questions.</p>
							<hr class="divider-line" />
							<p class="txt30">03</p>
							<p>Each multiple choice question has four answer options. Choose whichever you feel is the most correct answer. </p>
							<p>&nbsp;</p>
							<p>&nbsp;</p>
							<div class="floatR">
                            
                            
                            <%--<a href="MyProfile.aspx?id=1"  class="update-profile">UPDATE PROFILE</a>--%>
                            <a id="hylProfile"  runat="server" href="#" class="my-profile">BEGIN NOW</a>
                           </div>
							<div class="clear"></div>
							<p>&nbsp;</p>
							<div class="timed-task">
								<p class="title"><span>THIS IS A<br /><font class="orange">TIMED TASK!</font></span></p>
								<p>&nbsp;</p>
								<p>You are given 60 minutes to complete the evaluation. There are eight sections with nine questions in each. You will see the clock throughout the evaluation in the top right hand side of the page in orange and you are given a guide of your completion progress as you go. </p>
							</div>
						</article>
					</section>
					<div class="dot-line">&nbsp;</div>
				</article>
				<!-- Content Area end // -->
</asp:Content>
