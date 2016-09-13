<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Procurement_TNA.aspx.cs" Inherits="SGA.Procurement_TNA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<!-- Banner start -->
				<div class="banner-inner-pke">
					<div class="banner-cnt">
						<p class="banner-heading"><span>Procurement Self Assessment</span></p>
						
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
							<p>This is a self-assessment of your Procurement skills. You will be guided through eight dimensions of Procurement as you assess yourself on 72 questions. Allow for 40 – 60 minutes to complete this self-assessment.</p>
								<p></p>
							<p class="mrg-bt-10 txt-orange dark">The 8 dimesions are:</p>
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
							<p>It explores the Procurement Attributes, Business Attributes and Personal Attributes that drive excellence in procurement. As such, the skills that are uniquely aligned to capabilities required in procurement.</p>
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