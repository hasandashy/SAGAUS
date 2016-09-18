<%@ Page Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="cma-relationsip-context.aspx.cs" Inherits="SGA.tna.cma_relationsip_context" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        window.location.href = '/tna/contract-management-assessment-test.aspx';
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
        });*/
    });
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
							<p class="title40">THE RELATIONSHIP CONTEXT</p>
							<p>&nbsp;</p>
							<p class="txt16-bold">The background of the relationship between Cleaning Co. and you</p>
							<p>&nbsp;</p>
							
							<p>•&nbsp;&nbsp;You are in a full time role with your organisation<br /><br />
•&nbsp;&nbsp;Managing commercial contracts is part of your full-time role but is not your primary role<br /><br />
•&nbsp;&nbsp;You are responsible for managing one commercial contract with an external service provider, Cleaning Co.<br /><br />
•&nbsp;&nbsp;Cleaning Co. provides office-cleaning services across your organisation. They were appointed by a team in your head office<br /><br />
•&nbsp;&nbsp;You were not involved in their appointment as the service provider<br /><br />
•&nbsp;&nbsp;Within the portfolio of contracts negotiated by the central procurement team, this contract is considered low to medium risk, and of relatively low value<br /><br />
•&nbsp;&nbsp;You are responsible for managing Cleaning Co. in your region<br /><br />
</p>
							<p>&nbsp;</p>
                            <p>The questions that follow explore issues involved in managing the contract with Cleaning Co. </p>
                            <p>&nbsp;</p>
                            <p>Each question describes a different scenario, when answering them treat each question independently.</p>
                            <p>&nbsp;</p>
                            <p>Question 3 is a one-off event therefore your response to the situation should be based on that question alone and not on the basis that it is part of a pattern of events that include the incidents described in questions 1 and 2.</p>
							<p>&nbsp;</p>
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

