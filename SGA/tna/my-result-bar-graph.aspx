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
                <div class="content">IS YOUR<br />
                    RATING</div>
                <div class="clear"></div>
            </div>
            <div class="clear"></div>
        </section>
        <div class="dot-line">&nbsp;</div>
        <section class="color-box">
            <article class="test-info-box">
                <p class="title orange">My Results and Reports</p>
                <p>&nbsp;</p>
                <p><span class="dark">INSTRUCTIONS:</span> Below you will find the results and report for each assessment you have taken. In the left hand column you will note the menu where you can easily navigate. If you would like to access your reports or compare your results, simply navigate through the links. We encourage you to share the 'Negotiation Profile Assessment' since aggregate data from this challenge will provide an important insight into 'Category Management Capability' in Australia. The richer the data - the greater the insights.</p>
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

                                <p class="title18"><span id="spSkills" runat="server">Procurement Skills Self  <br />Assessment</span></p>
                                <% if(isTnaResult) { %>
                                
                                <div class="acrd-menu">
                                <p><a href="#" class="menuitem submenuheader">Display Results</a></p>
								<div class="submenu">
										<ul>
											<li><a href="my-results-bar-graph-ssa.aspx" >&bull; Bar Graph</a></li>
										</ul>
									</div>
                                </div>
                                <% } else { %>
                                <div class="result-info icon-graf">
									<p>This assessment is locked and no results are available.</p>
								</div>
                                <%} %>
                                
                                <p class="title18"><span id="spCMA" runat="server">Contract Management Self <br />Assessment</span></p>
                                <% if (isCMAResult)
                                   { %>
                                
                                <div class="acrd-menu">
                                <p><a href="#" class="menuitem submenuheader">Display Results</a></p>
								<div class="submenu">
										<ul>
											<li><a href="my-results-bar-graph-cma.aspx"   >&bull; Bar Graph</a></li>
										</ul>
									</div>
                                </div>
                                <% } else { %>
                                <div class="result-info icon-copy">
									<p>This assessment is locked and no results are available.</p>
								</div>
                                <%} %>
                                
                                <p class="title18"><span id="spPKE" runat="server">Procurement Knowledge   <br />Evaluation</span></p>
                                <% if(isPkeResult) { %>
                                
                                <div class="acrd-menu">
                                <p><a href="#" class="menuitem submenuheader">Display Results</a></p>
								<div class="submenu">
										<ul>
											<li><a href="my-result-bar-graph.aspx"  >&bull; Bar Graph</a></li>
										</ul>
									</div>
                                </div>
                                <% } else { %>
                                <div class="result-info icon-bulb">
									<p>This assessment is locked and no results are available.</p>
								</div>
                                <%} %>
                                <p class="title18"><span id="spCMK" runat="server">Contract Management Knowledge <br /> Evaluation</span></p>
								<% if (isCmkResult)
                                { %>
                                <div class="acrd-menu">
									<p><a href="#" class="menuitem submenuheader">Display Results</a></p>
									<div class="submenu">
										<ul>
											<li><a href="my-results-bar-graph-cmk.aspx" >&bull; Bar Graph</a></li>
										</ul>
									</div>
								</div>
								<% } else { %>
                                <div class="result-info icon-calcualtor">
									<p>Your results are restricted of this assessment.</p>
								</div>
                                <%} %>
							</div>
                <div class="col-cnt">
                    <div class="wide578">
                        <p class="txt28 orange mrg-bt-5">Bar Graph</p>
                        <p class="dark mrg-bt-5">How to interpret these results.</p>
                        <p>In the graph below you can see your results for each step of the process.<br />
                            This provides a visual display of your strengths and development opportunities.</p>
                        <p>&nbsp;</p>
                        <p>&nbsp;</p>
                    </div>
                    <p class="txtCtr">
                        <!-- Graph comes up here -->
                        <sga:cmcGragh ID="graph1" showCompare="0" runat="server"></sga:cmcGragh>
                    </p>
                    <p>&nbsp;</p>
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
