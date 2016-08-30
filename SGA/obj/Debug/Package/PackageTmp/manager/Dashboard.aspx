<%@ Page  Language="C#" MasterPageFile="~/ManagerMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SGA.manager.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!-- Content Area start -->
				<article id="container">
					<section class="welcome-test">
						<p class="title40 floatL"><asp:Label ID="lblName" runat="server"></asp:Label></p>
						<div class="clear"></div>
                        <p class="title40"><asp:Label ID="lblCompanyName" runat="server"></asp:Label> :: Dashboard</p>
						
					</section>
					<div class="dot-line">&nbsp;</div>
					<section class="color-box">
						<article class="info-box-shdw">
							<div class="icon"><img src="../../innerimages/img-category-management-challenge.gif" alt="Category Management Challenge" /></div>
							<div class="head">Category Management Challenge (<asp:Label ID="lblCMCCount" runat="server"></asp:Label>)</div>
							<div class="desc">This self-assessment focuses on the skills required to perform the category management function. It focuses on the eight-step category management process and for each step you will be asked.</div>
							
							<div class="clear"></div>
						</article>
						<article class="info-box-shdw">
							<div class="icon"><img src="../../innerimages/img-skills-self-assessment.gif" alt="Skills Self Assessment" /></div>
							<div class="head">Skills Self Assessment (<asp:Label ID="lblSSACount" runat="server"></asp:Label>)</div>
							<div class="desc">This is a self-evaluation of your procurement skills. You will be guided through the eight phases of the complete category management process as you assess yourself on 72 pivotal procurement skills.</div>
							
							<div class="clear"></div>
						</article>
                        <article class="info-box-shdw">
							<div class="icon"><img src="../innerimages/img-behavioural-assessment.gif" alt="Behavioural Assessment" /></div>
							<div class="head">Behavioural Assessment (<asp:Label ID="lblBACount" runat="server"></asp:Label>)</div>
							<div class="desc">This is a self-evaluation of your behavioural competencies. You will be guided through critical behavioural styles as you assess yourself on 72 behaviour descriptions.</div>
							
							<div class="clear"></div>
						</article>
						<article class="info-box-shdw">
							<div class="icon"><img src="../innerimages/calculator-icon.png" alt="Negotiation Profile" /></div>
							<div class="head">Negotiation Profile (<asp:Label ID="lblNPCount" runat="server"></asp:Label>)</div>
							<div class="desc">This is a self-evaluation of your negotiation skills. You will be guided through critical negotiation styles as you assess yourself on 72 pivotal negotiation skills.</div>
							
							<div class="clear"></div>
						</article>
						<article class="info-box-shdw">
							<div class="icon"><img src="../innerimages/img-department-maturity-profile.gif" alt="Department Maturity Profile" /></div>
							<div class="head">Department Maturity Profile (<asp:Label ID="lblMPCount" runat="server"></asp:Label>)</div>
							<div class="desc">This is your independent assessment of the procurement function that you operate within. You will be guided through 72 questions that will form a picture of where your procurement organization is, in the journey of assessing its maturity.  </div>
							
							<div class="clear"></div>
						</article>
                        <article class="info-box-shdw">
							<div class="icon"><img src="../innerimages/img-contract-management-assessment.png" alt="Contract Management Assessment" /></div>
							<div class="head">Contract Management Assessment (<asp:Label ID="lblCMACount" runat="server"></asp:Label>)</div>
							<div class="desc">This assessment targets contract and commercial managers. The diagnostic profiles capability in managing suppliers and contracts. The feedback uses 70:20:10 framework for extending traditional learning into the workplace to support you in managing commercial contracts more effectively.  </div>
							
							<div class="clear"></div>
						</article>
					</section>
					<div class="dot-line">&nbsp;</div>
				</article>
				<!-- Content Area end // -->
</asp:Content>
