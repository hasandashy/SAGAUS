<%@ Page Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="cma-assessment-instructions.aspx.cs" Inherits="SGA.tna.cma_assessment_instructions" %>
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
                alertHtml = 'You are about to begin the Contract Management Self Assessment. This assessment must be taken in a single sitting. Are you ready to begin?';
            }
        });
    });*/
</script>
<!-- Content Area start -->
				<article id="container">
					<section class="welcome">
						<p class="title40-orange" style="text-align:center;"><asp:Label ID="lblName" runat="server"></asp:Label></p>
						<p class="title40" style="text-align:center;">Welcome to the Contract Management Self Assessment</p>
					</section>
					<div class="dot-line">&nbsp;</div>
					<section class="color-box">
						<article class="info-box-shdw-inner">
							<p class="title40">Instructions</p>
							<p>&nbsp;</p>
							<p class="txt16-bold">You are about to complete an evaluation of your commercial contract management capability. The aim of this exercise is to provide an independent assessment of your capabilities as they stand today.</p>
							<p>&nbsp;</p>
							<p class="txt30">01</p>
							<p>This assessment focuses on eight underpinning capabilities that drive effective contract management. They are:</p>
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
                            <p>&nbsp;</p>
                            <p>As you work through the assessment you may find themes that are not directly relevant to your current role. This is likely to happen for many people as this diagnostic considers a broad range of procurement capabilities. It has purposefully been designed not to be role specific, instead it seeks to understand the South Australian Government procurement capability as a whole.</p>
							<div class="clear"></div>
							<hr class="divider-line" />
							<p class="txt30">02</p>
							<p>For each Contract Management dimension you will be asked to assess yourself on nine specific capabilities. A scoring guide is provided at the top of the Assessment page that you can refer to when assessing yourself.</p>
							<hr class="divider-line" />
							<p class="txt30">03</p>
							<p>For each statement, click the radio button that represents your level of skill for each (0 Novice – 7 Expert). Once you have completed each Dimension, click ’NEXT’ to be taken to the next Dimension. </p>
							<p>&nbsp;</p>
							<p>&nbsp;</p>
                           
							<div class="floatR">
                            <a href="MyProfile.aspx?id=3"  class="update-profile">UPDATE PROFILE</a>
                            <a  id="hylProfile" runat="server" href="/tna/contract-management-assessment-test.aspx" class="my-profile">BEGIN NOW</a></div>
							<div class="clear"></div>
							<p>&nbsp;</p>
							
							
						</article>
                        
                        
  					</section>
					<div class="dot-line">&nbsp;</div>
				</article>
				<!-- Content Area end // -->
</asp:Content>

