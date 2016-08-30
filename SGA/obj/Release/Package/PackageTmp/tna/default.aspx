<%@ Page Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SGA.tna._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<article id="container">
					<section class="welcome">
						<p class="title40-orange"><asp:Label ID="lblName" runat="server"></asp:Label>
						</p>
						<p class="title40">Your Assessments</p>
						<p>Below you will see the assessments that you currently have access to. It may have been determined that you should complete more than one assessment - this is when a role covers more than one aspect. If you see a lock symbol, this is because you do not yet have access to this assessment. If on review, it is determined that you also need access to this assessment it will be unlocked for you.By clicking 'go' against each assessment, you will be directed to that particular assessment.</p>
					</section>
					<div class="dot-line">&nbsp;</div>
					<section class="color-box">
						
						<article id="pnlTNA" runat="server" visible="false" class="info-box-shdw">
							<div class="icon"><img src="../innerimages/img-skills-self-assessment.gif" alt="Skills Self Assessment" /></div>
							<div class="head">Procurement Training Needs Analysis</div>
							<div class="desc">An online self-assessment survey designed to explore the skills required to perform the end-to-end procurement function. It focuses on 8 phases of the procurement process and asks you to rate yourself across 72 capabilities in total.</div>
							<div class="info">
							<asp:HyperLink ID="hylTna" runat="server"></asp:HyperLink></div>
							<div class="clear"></div>
						</article>
						
                        
                        <article id="pnlCMA" runat="server" visible="false" class="info-box-shdw">
							<div class="icon"><img src="../innerimages/img-contract-management-assessment.png" alt="Contract Management Assessment" /></div>
							<div class="head">Contract Management Training Needs Analysis</div>
							<div class="desc">An online self-assessment survey designed to explore the capability required to perform commercial contract management. Based on your responses to 72 questions, across the 8 categories of contract management, a profile of your capability will be built and recommendations for future development will be made.</div>
							<div class="info">
							<asp:HyperLink ID="hylCMA" runat="server"></asp:HyperLink></div>
							<div class="clear"></div>
						</article>

                        <article id="pnlLeadership" runat="server" visible="false" class="info-box-shdw">
							<div class="icon"><img src="../innerimages/img-behavioural-assessment.gif" alt="Leadership Training Needs Analysis" /></div>
							<div class="head">Leadership Training Needs Analysis </div>
							<div class="desc">An online self-assessment survey to explore the skills required to motivate, lead and empower a team of people. It focuses on 8 stages of leadership and asks you to rate yourself across 72 capabilities in total.</div>
							<div class="info">
							<asp:HyperLink ID="hylPmp" runat="server"></asp:HyperLink></div>
							<div class="clear"></div>
						</article>
                        <article class="info-box-shdw">
							<div class="icon"><img src="../innerimages/calculator-icon.png" alt="Negotiation Profile" /></div>
							<div class="head">Negotiation Profile </div>
							<div class="desc">This is a self-evaluation of your negotiation skills. You will be guided through critical negotiation styles as you assess yourself on 72 pivotal negotiation skills.</div>
							<div class="info">
							<asp:HyperLink ID="hylNP" runat="server"></asp:HyperLink></div>
							<div class="clear"></div>
						</article>
					</section>
					<div class="dot-line">&nbsp;</div>
				</article>         
                       
</asp:Content>

