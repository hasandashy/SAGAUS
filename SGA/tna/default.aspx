<%@ Page Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SGA.tna._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<article id="container">
					<section class="welcome">
						<p class="title30-orange" style="text-align:center;"><asp:Label ID="lblName" runat="server"></asp:Label>
						</p>
						<p class="title27">Wecome to <b>Skills for Procurement</b> - Assess and Build</p>
						<p>Below you will see the Assessment Pack that has been assigned to you. As you complete one assessment you will ‘unlock’ the next, and a <b>GO</b> button will appear. By clicking GO against the assessment, you will be taken directly to it. Each Assessment starts with a page of Instructions, and your timed Assessments will not begin until you have completed reading the Instructions.</p>
                        
                        <br />
                       <div runat="server" id="spanBegin" visible="true"> <div style="color:#ea4320 !important;text-align:left;font-size:20px;;float:left;font-weight:bold;">Before you begin your first Assessment,<br /> Please complete your Profile.</div><div style="float:right;"><asp:HyperLink ID="hyper" CssClass="my-profile" Text="BEGIN NOW" NavigateUrl="~/tna/MyProfile.aspx" runat="server"></asp:HyperLink></div></div>
					<br />
                    </section>
					<div class="dot-line">&nbsp;</div>
					<section class="color-box">
                        <article id="pnlTNA" runat="server" visible="false" class="info-box-shdw">
							<div class="icon"><img src="../innerimages/img-skills-self-assessment.gif" alt="Skills Self Assessment" /></div>
							<div class="head">Procurement Skills Self Assessment</div>
							<div class="desc">This is a self-assessment of your Procurement skills. You will be guided through eight dimensions of Procurement as you assess yourself on 72 questions. Allow for 40 – 60 minutes to complete this self-assessment.</div>
							<div class="info">
							<asp:HyperLink ID="hylTna" runat="server"></asp:HyperLink></div>
							<div class="clear"></div>
						</article>
						<article id="pnlSga" runat="server" visible="false" class="info-box-shdw">
							<div class="icon"><img src="../innerimages/img-category-management-challenge.gif" alt="Procurement Knowledge Evaluation" /></div>
							<div class="head">Procurement Knowledge Evaluation</div>
							<div class="desc">This is an evaluation of your Procurement knowledge. You will be guided through eight dimensions of Procurement and you will be asked nine multiple choice questions for each dimension. This is a timed assessment and at 60 minutes the assessment will close.</div>
							<div class="info">
					        <asp:HyperLink ID="hylSga" runat="server"></asp:HyperLink></div>
							<div class="clear"></div>
						</article>					
						
                        
                        <article id="pnlCMA" runat="server" visible="false" class="info-box-shdw">
							<div class="icon"><img src="../innerimages/img-department-maturity-profile.gif" alt="Contract Management Assessment" /></div>
							<div class="head">Contract Management Self Assessment</div>
							<div class="desc">This is a self-assessment of your Contract Management skills. You will be guided through eight dimensions of Contract Management as you assess yourself on 72 questions. Allow for 40 – 60 minutes to complete this self-assessment.</div>
							<div class="info">
							<asp:HyperLink ID="hylCMA" runat="server"></asp:HyperLink></div>
							<div class="clear"></div>
						</article>

                         <article id="pnlCMK" runat="server" visible="false" class="info-box-shdw">
							<div class="icon"><img src="../innerimages/img-contract-management-assessment.png" alt="Contract Management Assessment" /></div>
							<div class="head">Contract Management Knowledge Evaluation</div>
							<div class="desc">This is an evaluation of your Contract Management knowledge. You will be guided through eight dimensions of Contract Management and you will be asked nine multiple choice questions for each dimension. This is a timed assessment and at 60 minutes the assessment will close. </div>
							<div class="info">
							<asp:HyperLink ID="hylCMK" runat="server"></asp:HyperLink></div>
							<div class="clear"></div>
						</article>
                          <article id="pnlCAA" runat="server" visible="false" class="info-box-shdw">
							<div class="icon"><img src="../innerimages/img-behavioural-assessment.gif" alt="Commercial Awareness Assessment" /></div>
							<div class="head">Commercial Awareness Assessment</div>
							<div class="desc">This is an evaluation of commercial awareness. You will be guided through five scenarios and asked four multiple-choice questions for each. This is a timed assessment and at 60 minutes the assessment will close. </div>
							<div class="info">
							<asp:HyperLink ID="hylCAA" runat="server"></asp:HyperLink></div>
							<div class="clear"></div>
						</article>

                        
					</section>
					<div class="dot-line">&nbsp;</div>
				</article>         
                       
</asp:Content>

