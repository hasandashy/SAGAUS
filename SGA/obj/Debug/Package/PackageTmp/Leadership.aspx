<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Leadership.aspx.cs" Inherits="SGA.Leadership" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<!-- Banner start -->
				<div class="banner-inner-ssa">
					<div class="banner-cnt">
						<p class="banner-heading"><span>Leadership Assessment</span></p>
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
							<p>The training needs analysis is an online self-assessment survey.  The questions are designed to explore the skills required to motivate, lead and empower a team of people.  It focuses on 8 stages of leadership and asks you to rate yourself across 72 capabilities in total.</p>
							<p></p>
							<p class="mrg-bt-10 txt-orange dark">The 8 phases</p>
							<div class="floatL wide50-1">
								<ul class="tick-mark2">
									<li>Strategic Leader</li>
									<li>Building Trust</li>
									<li>Communicate and Engage</li>
									<li>Emotional Intelligence</li>
								</ul>
							</div>
							<div class="floatL wide50-1">
								<ul class="tick-mark2">
									<li>Endorse, Support and Develop</li>
									<li>Culture and Change Leader</li>
									<li>Relationship Management</li>
									<li>Coach and Mentor</li>
								</ul>
							</div>
							<div class="clear"></div>
							<p></p>
							
							<p class="title28">What is it used for?</p>
							<p>The Queensland public sector is focussed on building sustainable leadership capability across agencies and government.  It is important to address skills gaps across the entire public sector, and as individuals we should also be aware and focused on our specific development needs.</p>
							<p></p>
							<p>On completion you will receive an individual report with an assessment of your capability across a range of leadership competencies.  These will highlight the different stages of leadership focusing on your strengths and evolving skill sets.</p>
							<p></p>
							<p>The key output is a set of recommendations for you to consider.  These recommendations are based on the 70-20-10 model of learning.</p>
							<p></p>
							<p class="title28">Your Training Needs Analysis report</p>
							<p>The aim of your individual report is to provide you with a systematic insight of your skill-set as it stands today, along with ideas for you to apply 70-20-10 learning principles in your workplace.</p>
							<p></p>
							<p>The 70-20-10 learning model suggests 70% of development consist of on-the-job learning, supported by 20% coaching and/or mentoring and, 10% classroom based training.</p>
							<p></p>
							<p>Your report contains suggestions for each of these learning principles.  You will also receive a personalised e-Learning Plan and high priority training workshop recommendations.</p>
							<p></p>
                            <p>The 70-20-10 model’s three components reinforce one another, adding up to a whole that is greater than the sum of its parts.</p>
							<p>&nbsp;</p>
						</section>
					</div>
					<div class="col-340">
						<section class="cnt-rt-2 equal_height">
							<p class="title28">Note!</p>
                            <p>As you work through the assessment you may find questions or themes that are not directly relevant to your current role.</p>
                            <p></p>
                            <p>This is likely to happen for many people as this self-assessment considers all aspects of leadership, regardless of level.</p>
                            <p></p>
                            <p>It is purposefully designed not to be role specific but rather to understand leadership capability across agencies and Queensland government.</p>
                            <p></p>
                            <p>The needs that are uncovered across agencies will help to formulate training that specifically aligns to, and resolves gaps in leadership capability and effectiveness.</p>
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
