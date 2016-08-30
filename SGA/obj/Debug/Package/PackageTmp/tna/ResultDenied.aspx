<%@ Page Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="ResultDenied.aspx.cs" Inherits="SGA.tna.ResultDenied" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript" src="../js/ddaccordion.js"></script>
<script type="text/javascript" src="../js/ddaccordion-menu.js"></script>
<script type="text/javascript" src="../js/custom-form-elements-load.js"></script>
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
							
							<div class="clear"></div>
						</div>
						<div class="clear"></div>
					</section>
					<div class="dot-line">&nbsp;</div>
					<section class="color-box">
						<article class="test-info-box">
							<p class="title orange">My Results and Reports</p>
							<p>&nbsp;</p>
							<p><span class="dark">INSTRUCTIONS:</span> Below you will find the results and reports each challenge and assessment you have taken.  In the left hand column you will note the menu where you can easily navigate.  If you would like to access your reports or compare your results, simply navigate through the links.  We encourage you to share the 'Category Management Challenge' since aggregate data from this challenge will provide an important insight into 'Category Management Capability' in Australia.  The richer the data - the greater the insights.</p>
						</article>
					</section>
					<section class="my-result-box">
						<article class="breadcrumb">
							<a href="#">Report Centre</a>&nbsp; &gt; &nbsp;<a href="#">Results Restricted</a>
						</article>
						<p>&nbsp;</p>
						<p>&nbsp;</p>
						<div class="my-result-container">
							<div class="col-lft">

                                <p class="title18"><span id="spSkills" runat="server">Procurement <br />Assessment</span></p>
                                <% if(isTnaResult) { %>
                                
                                <div class="acrd-menu">
                                <p><a href="#" class="menuitem submenuheader">Display Results</a></p>
								<div class="submenu">
										<ul>
											<li><a href="my-results-bar-graph-ssa.aspx" >&bull; Bar Graph</a></li>
											<li><a href="my-results-reports-ssa.aspx"  >&bull; Reports</a></li>
										</ul>
									</div>
                                </div>
                                <% } else { %>
                                <div class="result-info icon-graf">
									<p>This assessment is locked and no results are available.</p>
								</div>
                                <%} %>
                                

                                <p class="title18"><span id="spCMA" runat="server">Contract Management <br />Assessment</span></p>
                                <% if (isCMAResult)
                                   { %>
                                
                                <div class="acrd-menu">
                                <p><a href="#" class="menuitem submenuheader">Display Results</a></p>
								<div class="submenu">
										<ul>
											<li><a href="my-results-bar-graph-cma.aspx"   >&bull; Bar Graph</a></li>
											<li><a href="my-results-reports-cma.aspx"  >&bull; Reports</a></li>
										</ul>
									</div>
                                </div>
                                <% } else { %>
                                <div class="result-info icon-copy">
									<p>This assessment is locked and no results are available.</p>
								</div>
                                <%} %>
                               
                                <p class="title18"><span id="spBehaviour" runat="server">Leadership <br />Assessment</span></p>
                                <% if(isPmpResult) { %>
                                 
                                <div class="acrd-menu">
                                <p><a href="#" class="menuitem submenuheader">Display Results</a></p>
								<div class="submenu">
										<ul>
											<li><a href="my-results-bar-graph-ba.aspx"  >&bull; Bar Graph</a></li>
											<li><a href="my-results-reports-ba.aspx"  >&bull; Reports</a></li>
										</ul>
									</div>
                                </div>
                                <% } else { %>
                                <div class="result-info icon-bulb">
									<p>This assessment is locked and no results are available.</p>
								</div>
                                <%} %>
                                
							</div>
							<div class="col-cnt">
								<div class="wide640">
									<p class="txt28 orange mrg-bt-5 floatL">Results Restricted</p>
									<p class="floatR txt14">
                                    
                                    </p>
									<div class="clear"></div>
									<p>&nbsp;</p>
									
                                    <p class="txt28 orange mrg-bt-5">Thank you for completing the assessment. Your results have been saved. </p>
                                    
									<p>&nbsp;</p>
									<p>&nbsp;</p>
                                    
									<p>&nbsp;</p>
								</div>
								<hr class="divider-line" />
								<p>&nbsp;</p>
								
							</div>
							<div class="clear"></div>
							<p>&nbsp;</p>
						</div>
					</section>
					<div class="dot-line">&nbsp;</div>
				</article>
				
</asp:Content>

