<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Procurement_TNA.aspx.cs" Inherits="SGA.Procurement_TNA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<!-- Banner start -->
				<div class="banner-inner-ssa">
					<div class="banner-cnt">
						<p class="banner-heading"><span>Procurement Assessment</span></p>
						<div class="banner-txt">
                            <p class="banner-heading" style="font-size:32px">Training Needs Analysis</p>
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
							<p>This self-assessment focusses on the skills required to perform the category management function. It focusses on an eight-step category management process and asks you for rate yourself across 72 self- evaluations.</p>
							<p></p>
                            <p>It explores the Procurement Attributes, Business Attributes and Personal Attributes that drive excellence in each step of the category management process. As such, skills are uniquely aligned to the distinct phase and capabilities of procurement.</p>
							<p></p>
							<p class="mrg-bt-10 txt-orange dark">The 8 steps are:</p>
							<div class="floatL wide50-1">
								<ul class="tick-mark2">
									<li>Opportunity Analysis</li>
									<li>Market Analysis</li>
									<li>Strategy Development</li>
									<li>Market Engagement</li>
								</ul>
							</div>
							<div class="floatL wide50-1">
								<ul class="tick-mark2">
									<li>Negotiation</li>
									<li>Contract Implementation</li>
									<li>Supplier Relationship Management</li>
									<li>Strategy Refresh</li>
								</ul>
							</div>
							<div class="clear"></div>
							<p></p>
							<p>On completion you will receive an individual report with an assessment of your capability across a range of technical competencies. It will highlight each phase of the category management process focussing on both your strengths and your evolving skill-sets. The key output is a set of recommendations for both you and your manager to consider in order to optimise department and individual performance.</p>
							<p></p>
							<p class="title28">What is it used for?</p>
							<p>The information gathered in your Training Needs Analysis is used to inform which e-Learning and workshop recommendation would be the most relevant for you. </p>
							<p></p>
							<p>Once you complete it you will receive an individual report with an assessment of your capability across a range of procurement competencies.  It will highlight each phase of the procurement process focusing on both your strengths and your evolving skill-sets. </p>
							<p></p>
							<p class="title28">Your Training Needs Analysis report</p>
							<p>The aim of your individual report is to provide you systematic insight of your skill set as it stands today together with ideas for you to apply 70:20:10 learning in your workplace. </p>
							<p></p>
							<p>The 70:20:10 learning model suggests that 70% of development consists of on the job learning, supported by 20% coaching and mentoring and 10% classroom training, with each component reinforcing the others. </p>
							<p></p>
							<p>In your report you will find suggestions for each of these learning components. You will also receive your personalised e-Learning plan and high-priority training workshops. </p>
							<p>&nbsp;</p>
						</section>
					</div>
					<div class="col-340">
						<section class="cnt-rt-2 equal_height">
							<p class="title28">Note!</p>
                            <p>As you work through the questionnaire you may find themes that are not directly relevant to your current role.</p>
                            <p></p>
                            <p>This is likely happen for many people as this questionnaire considers the end-to-end commercial contract management process.</p>
                            <p></p>
                            <p>It has purposefully been designed to not be role specific, instead it seeks to understand the capability of end-to-end contract management across agencies and Queensland Government.</p>
                            <p></p>
                            <p class="title28">Remember!</p>
                            <p>With this questionnaire there are no incorrect answers.</p>
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