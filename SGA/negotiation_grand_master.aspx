<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="negotiation_grand_master.aspx.cs" Inherits="SGA.negotiation_grand_master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<!-- Banner end // -->
				<div class="banner-inner-np">
					<div class="banner-cnt">
						<p class="banner-heading"><span>Negotiation Assessment</span></p>
						<div class="banner-txt">
                            <p class="banner-heading" style="font-size:32px;line-height:40px">So,… you think you are a <br />great negotiator?! <br />Let’s see how great you <br />really are!</p>
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
							<p class="title28 ">What is the profile about?</p>
							<p>This profile focuses upon the capabilities needed to negotiate effectively. There are eight dimensions which underpin the negotiation process, and the profile features nine questions for each of the eight directions.</p>
							<p>&nbsp;</p>
							<p>Negotiation is a little like chess, in that we each have some objectives, we deploy tactics to achieve our objectives, and we have to be thoughtful not only about what we do, but also what the other party does. This profile is based upon a multi-dimensional approach to negotiation, which explores a variety of different issues which may affect the outcome of the negotiation.</p>
							
							<p>&nbsp;</p>
							<p class="mrg-bt-10 txt-orange dark">The eight dimensions are:</p>
							<div class="floatL wide50-1">
								<ul class="tick-mark2">
									<li>The contextual dimension; who has the balance of power?</li>
									<li>The political dimension; which constituencies shape each party’s behaviour?</li>
									<li>The cultural dimension; what is the degree of overlap of how each party does things?</li>
									<li>The interpersonal dimension; what is the interpersonal dynamic between the parties?</li>
								</ul>
							</div>
							<div class="floatL wide50-1">
								<ul class="tick-mark2">
									<li>The intentional dimension; what are each party’s objectives?</li>
									<li>The procedural dimension; what are the phases of negotiation, and how should we behave?</li>
									<li>The motivational dimension; how do we persuade the other party?</li>
									<li>The tactical dimension; what are the tactics employs that may be considered?</li>
								</ul>
							</div>
							<div class="clear"></div>
							<p>&nbsp;</p>
							<p>On completion, you will receive a personalised report assessing your capability across all eight dimensions. The report will highlight your strengths and opportunities for you to develop your capability. The key output of the report is a suite of recommendations for you to consider to further your personal development in terms of your negotiation capability.</p>
							<p>&nbsp;</p>
							<p class="title28 ">What's in it for me?</p>
							<ol class="list2">
								<li>You will gain insight into a variety of dimensions relevant to the negotiation process.</li>
								<li>You will be able to benchmark your knowledge against other negotiators.</li>
								<li>You will get an independent assessment of your negotiation capability, which may help build a case for further development.</li>
								<li>You will receive feedback on your strengths, which may help develop your confidence in your own capability</li>
								<li>You will receive a personalised training plan specific to your strengths and your development needs.</li>
							</ol>
							<p>&nbsp;</p>
							
						</section>
					</div>
					<div class="col-340">
						<section class="cnt-rt-3 equal_height">
							<p class="txtCtr mrg-bt-10"><img src="images/img-ng-master2.jpg" alt="" class="img-shadow" /></p>
							<p>&nbsp;</p>
							<p class="txtCtr txt28" style="line-height:30px;">How challenging is the Assessment?</p>
							<p>&nbsp;</p>
							
							<div class="receive-cnt">
								<p>The profile is designed to gain insights into team and individual skill sets. To this end there are graduated levels of maturity with in each question set however, criteria are not designed to 'trip up' or trick individuals. There is no 'good' or 'bad' profile result but rather an insight into the overall picture. The profile generally takes around 30-40 minutes to complete.</p>
							</div>
							
							<div class="clear"></div>
							<p>&nbsp;</p>
						</section>
					</div>
					<div class="clear"></div>
					<div class="dot-line">&nbsp;</div>
					<p>&nbsp;</p>
				</article>
<script>
    $(document).ready(function () {
        $(".iphonNav ul li").removeClass("active");
        //alert($(".iphonNav ul li").eq(4));
        $(".iphonNav ul li:eq(2)").addClass("active");
    });
</script>
</asp:Content>
