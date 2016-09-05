<%@ Page Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="my-results-bar-graph-cma.aspx.cs" Inherits="SGA.tna.my_results_bar_graph_cma" %>

<%@ Register Src="~/controls/ctrlCMAGraph.ascx" TagName="baGragh" TagPrefix="sga" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../js/ddaccordion.js"></script>
    <script type="text/javascript" language="javascript">
        ddaccordion.init({
            headerclass: "submenuheader", //Shared CSS class name of headers group
            contentclass: "submenu", //Shared CSS class name of contents group
            revealtype: "click", //Reveal content when user clicks or onmouseover the header? Valid value: "click", "clickgo", or "mouseover"
            mouseoverdelay: 200, //if revealtype="mouseover", set delay in milliseconds before header expands onMouseover
            collapseprev: false, //Collapse previous content (so only one open at any time)? true/false 
            defaultexpanded: [0, 1, 2], //index of content(s) open by default [index1, index2, etc] [] denotes no content
            onemustopen: true, //Specify whether at least one header should be open always (so never all headers closed)
            animatedefault: false, //Should contents open by default be animated into view?
            persiststate: false, //persist state of opened contents within browser session?
            toggleclass: ["", ""], //Two CSS classes to be applied to the header when it's collapsed and expanded, respectively ["class1", "class2"]
            togglehtml: ["suffix", "<img src='/innerimages/arw-rt.gif' class='statusicon' />", "<img src='/innerimages/arw-bt.gif' class='statusicon' />"], //Additional HTML added to the header when it's collapsed and expanded, respectively  ["position", "html1", "html2"] (see docs)
            animatespeed: "fast", //speed of animation: integer in milliseconds (ie: 200), or keywords "fast", "normal", or "slow"
            oninit: function (headers, expandedindices) { //custom code to run when headers have initalized
                //do nothing
            },
            onopenclose: function (header, index, state, isuseractivated) { //custom code to run whenever a header is opened or closed
                //do nothing
            }
        })
    </script>

    <!-- Content Area start -->
    <article id="container">
        <section class="welcome-test">
            <p class="title40 floatL orange">Congratulations! <span class="txt20">You have completed your assessment.</span></p>
            <div class="score-ur">
                <%--<div class="percent">
                            <asp:Label ID="lblPercentage" runat="server"></asp:Label>
                            </div>
							<div class="content">IS YOUR<br />RATING</div>--%>
                <div class="clear"></div>
            </div>
            <div class="clear"></div>
        </section>
        <div class="dot-line">&nbsp;</div>
        <section class="color-box">
            <article class="test-info-box">
                <p class="title orange">My Results and Reports</p>
                <p>&nbsp;</p>
                <p><span class="dark">INSTRUCTIONS:</span> Below you will find the results and reports for each challenge and assessment you have taken. In the left hand column you will note the menu where you can easily navigate. If you would like to access your reports, simply navigate through the links.</p>
            </article>
        </section>
        <section class="my-result-box">
            <article class="breadcrumb">
                <a href="#">Report Centre</a>&nbsp; &gt; &nbsp;<a href="#">Contract Management Assessment Results</a>&nbsp; <span>&gt; &nbsp;Bar Graph</span>
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
                        <sga:baGragh ID="graph1" runat="server"></sga:baGragh>
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

