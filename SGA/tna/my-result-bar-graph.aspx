<%@ Page Title="" Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="my-result-bar-graph.aspx.cs" Inherits="SGA.tna.my_result_bar_graph" %>

<%@ Register Src="~/controls/ctrlCMCGraph.ascx" TagName="cmcGragh" TagPrefix="sga" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../js/ddaccordion.js"></script>
    <script type="text/javascript" src="../js/ddaccordion-menu.js"></script>

    <!-- Content Area start -->
    <article id="container">
        <section class="welcome-test">
            <p class="title40 floatL orange">Congratulations! <span class="txt20">You have completed your assessment.</span></p>
            <div class="score-ur">
                <div class="percent">
                    <asp:Label ID="lblPercentage" runat="server"></asp:Label>
                </div>
                <%--<div class="content">IS YOUR<br />
                    RATING</div>--%>
                <div class="clear"></div>
            </div>
            <div class="clear"></div>
        </section>
        <div class="dot-line">&nbsp;</div>
        <section class="color-box">
            <article class="test-info-box">
							<p class="title orange">My Results</p>
							<p>&nbsp;</p>
                            <span>Below you will find the results for each assessment you have taken. In the left hand column, you will note the menu where you can easily navigate your bar-graphs. </span>
							<div class="clear"></div>
                            <br /><p><span class="dark">NOTE:</span> Your report will be delivered one week after the conclusion of the assessment period. To receive your report you need to complete all assessments assigned to you.</p>
						</article>
        </section>
        <section class="my-result-box">
            <article class="breadcrumb">
                <a href="#">Report Centre</a>&nbsp; &gt; &nbsp;<a href="#">Skills Test Results</a>&nbsp; <span>&gt; &nbsp;Bar Graph</span>
            </article>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <div class="my-result-container">
              <div class="col-lft">

                              
                                <% if(isTnaResult) { %>
                                  <p class="title18"><span id="spSkills" runat="server">Procurement Skills Self  <br />Assessment</span></p>
                                <div class="acrd-menu">
                                <p><a href="#" class="menuitem submenuheader">Display Results</a></p>
								<div class="submenu">
										<ul>
											<li><a href="my-results-bar-graph-ssa.aspx" >&bull; Bar Graph</a></li>
										</ul>
									</div>
                                </div>
                                <% } %>
                                
                                 <% if(isPkeResult) { %>
                                 <p class="title18"><span id="spPKE" runat="server">Procurement Knowledge   <br />Evaluation</span></p>
                                <div class="acrd-menu">
                                <p><a href="#" class="menuitem submenuheader">Display Results</a></p>
								<div class="submenu">
										<ul>
											<li><a href="my-result-bar-graph.aspx"  >&bull; Bar Graph</a></li>
										</ul>
									</div>
                                </div>
                                <% } %>
                               
                               
                                <% if (isCMAResult)
                                    { %>
                                 <p class="title18"><span id="spCMA" runat="server">Contract Management Self <br />Assessment</span></p>
                                <div class="acrd-menu">
                                <p><a href="#" class="menuitem submenuheader">Display Results</a></p>
								<div class="submenu">
										<ul>
											<li><a href="my-results-bar-graph-cma.aspx"   >&bull; Bar Graph</a></li>
										</ul>
									</div>
                                </div>
                                <% } %>
                               
                                
                               
                               
                               
								<% if (isCmkResult)
                                { %>
                                 <p class="title18"><span id="spCMK" runat="server">Contract Management Knowledge <br /> Evaluation</span></p>
                                <div class="acrd-menu">
									<p><a href="#" class="menuitem submenuheader">Display Results</a></p>
									<div class="submenu">
										<ul>
											<li><a href="my-results-bar-graph-cmk.aspx" >&bull; Bar Graph</a></li>
										</ul>
									</div>
								</div>
								<% } %>        
                                <% if (isCaaResult)
                                { %>
                                 <p class="title18"><span id="spCaa" runat="server">Commercial Awareness Knowledge <br /> Evaluation</span></p>
                                <div class="acrd-menu">
									<p><a href="#" class="menuitem submenuheader">Display Results</a></p>
									<div class="submenu">
										<ul>
											<li><a href="my-results-bar-graph-caa.aspx" >&bull; Bar Graph</a></li>
										</ul>
									</div>
								</div>
								<% } %>                         
							</div>
                <div class="col-cnt">
                    <div class="wide578">
                        <p class="txt28 orange mrg-bt-5">Bar Graph</p>
                        <p class="dark mrg-bt-5">How to interpret these results.</p>
                        <p>In the graph below you can see your results for each dimension <br />
                            This provides a visual display of your strengths and development opportunities.</p>
                        <p>&nbsp;</p>
                        <p>&nbsp;</p>
                    </div>
                    <p class="txtCtr">
                        <!-- Graph comes up here -->
                        <sga:cmcGragh ID="graph1" showCompare="0" runat="server"></sga:cmcGragh>
                    </p>
                    <p>&nbsp;</p>
							<div style="float:right;">				
                                   <asp:HyperLink ID="hyper" CssClass="my-grph" Text="ASSESSMENT HOME PAGE" NavigateUrl="~/tna/default.aspx" runat="server"></asp:HyperLink>
								
							</div><br />
                    <hr class="divider-line" />
                    <p>&nbsp;</p>

                </div>
                <div class="clear"></div>
                <p>&nbsp;</p>
            </div>
        </section>
        <div class="dot-line">&nbsp;</div>
    </article>
    <!-- Content Area end // -->
</asp:Content>
