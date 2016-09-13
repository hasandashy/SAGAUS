<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Commercial_Awareness_Assesment.aspx.cs" Inherits="SGA.Commercial_Awareness_Assesment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <!-- Banner start -->
				<div class="banner-inner-caa">
					<div class="banner-cnt">
						<p class="banner-heading"><span>Commercial Awareness Assessment</span></p>
						<div class="banner-txt">
                            <p class="banner-heading" style="font-size:25px">Note: This is a timed assessment!<br />At 60 minutes the assessment will close</p>
						</div>
						<div class="clear"></div>
					</div>
				</div>
				<!--div class="banner-inner">
					<img src="images/img-banner-ss-assessment.jpg" alt="" />
				</div-->
				<!-- Banner end // -->
				<div class="dot-line1 no-mrg">&nbsp;</div>
				<!-- Content Area start -->
				<article id="container">
					<div class="col-660">
						<section class="cnt-lt-2 equal_height">
							<p class="title28">What is it?</p>
							<p>The Commercial Awareness Assessment is designed to diagnose commercial awareness across the Government of South Australia to deliver ‘value’ beyond traditional economic measures of value, such as price savings.</p>
							<p></p>
							<p class="mrg-bt-10 txt-orange dark">The eight dimensions are:</p>
							<div class="floatL wide50-1">
								<ul class="tick-mark2">
									<li>Stakeholder engagement</li>
									<li>Outcome focus</li>
									<li>Market steward</li>
									<li>Risk management</li>
                                    <li>Creating public value</li>
								</ul>
							</div>
							
							<div class="clear"></div>
							<p></p>
							<p>The Assessment is structured around five scenarios. For each scenario you will be asked four multiple-choice question.</p>
							<p></p>
							<p class="title28">What is it used for?</p>
							<p>The South Australian Government is committed to maximising the value derived from the procurement process. The aim of this assessment process is to provide a snapshot of the current level of procurement and contract management capability across the SA Government. </p>
							<p></p>
							<p>A group report will be prepared that summarises procurement capability and capacity across the public sector along with some analysis and options for improvement.</p>
							<p></p>
							<p class="title28">Your individual feedback report</p>
							<p>This report is tailored especially for you and in it you will find suggestions and recommendations that will help you develop skills and knowledge in the areas identified for you.</p>
							<p></p>
							<p>Your report will be delivered at the conclusion of the assessment period after completing your assigned Assessment Pack.  </p>
							<p></p>
							<p><strong>REVIEW</strong> the 70:20:10 recommendations in your report, consider your development priorities and consider how you can apply the suggestions in your workplace. </p>
							<p>&nbsp;</p>
						</section>
					</div>
					<div class="col-340">
						<section class="cnt-rt-2 equal_height">
							<p class="title28">Note!</p>
                            <p>As you work through the assessment you may find themes that are not directly relevant to your current role.</p>
                            <p></p>
                            <p>This is likely to happen for many people as this diagnostic considers a broad range of procurement capabilities.</p>
                            <p></p>
                            <p>It has purposefully been designed not to be role specific, instead it seeks to understand the South Australian Government procurement capability as a whole.</p>
                            <p></p>
                            <p class="title28">Remember!</p>
                            <p>This is a timed assessment and at 60 minutes the assessment will close.</p>
							<p></p>
						</section>
					</div>
					<div class="clear"></div>
					
					<p class="hide">&nbsp;</p>
					<p>&nbsp;</p>
				</article>
				<!-- Content Area end // -->
<script>
    $(document).ready(function () {
        $(".iphonNav ul li").removeClass("active");
        //alert($(".iphonNav ul li").eq(4));
        $(".iphonNav ul li:eq(2)").addClass("active");
    });
</script>
</asp:Content>
