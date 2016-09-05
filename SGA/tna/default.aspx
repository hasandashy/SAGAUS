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
							<div class="head">Procurement Skills Self Assessment</div>
							<div class="desc">An online self-assessment survey designed to explore the skills required to perform the end-to-end procurement function. It focuses on 8 phases of the procurement process and asks you to rate yourself across 72 capabilities in total.</div>
							<div class="info">
							<asp:HyperLink ID="hylTna" runat="server"></asp:HyperLink></div>
							<div class="clear"></div>
						</article>
						<article id="pnlSga" runat="server" visible="false" class="info-box-shdw">
							<div class="icon"><img src="../innerimages/img-category-management-challenge.gif" alt="Procurement Knowledge Evaluation" /></div>
							<div class="head">Procurement Knowledge Evaluation</div>
							<div class="desc">This assessment focuses on the skills required to perform procurement. It focuses on eight dimensions typically used in an end-to-end procurement process. For each dimension you will be asked nine questions.</div>
							<div class="info">
					        <asp:HyperLink ID="hylSga" runat="server"></asp:HyperLink></div>
							<div class="clear"></div>
						</article>					
						
                        
                        <article id="pnlCMA" runat="server" visible="false" class="info-box-shdw">
							<div class="icon"><img src="../innerimages/img-contract-management-assessment.png" alt="Contract Management Assessment" /></div>
							<div class="head">Contract Management Self Assessment</div>
							<div class="desc">An online self-assessment survey designed to explore the capability required to perform commercial contract management. Based on your responses to 72 questions, across the 8 categories of contract management, a profile of your capability will be built and recommendations for future development will be made.</div>
							<div class="info">
							<asp:HyperLink ID="hylCMA" runat="server"></asp:HyperLink></div>
							<div class="clear"></div>
						</article>

                         <article id="pnlCMK" runat="server" visible="false" class="info-box-shdw">
							<div class="icon"><img src="../innerimages/img-contract-management-assessment.png" alt="Contract Management Assessment" /></div>
							<div class="head">Contract Management Knowledge Evaluation</div>
							<div class="desc">This assessment targets contract and commercial managers. The diagnostic profiles capability in managing suppliers and contracts. The feedback uses 70:20:10 framework for extending traditional learning into the workplace to support you in managing commercial contracts more effectively. </div>
							<div class="info">
							<asp:HyperLink ID="hylCMK" runat="server"></asp:HyperLink></div>
							<div class="clear"></div>
						</article>

                        
					</section>
					<div class="dot-line">&nbsp;</div>
				</article>         
                       
</asp:Content>

