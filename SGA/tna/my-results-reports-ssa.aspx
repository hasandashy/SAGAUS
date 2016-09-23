<%@ Page Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="my-results-reports-ssa.aspx.cs" Inherits="SGA.tna.my_results_reports_ssa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript" src="../js/ddaccordion.js"></script>
<script type="text/javascript" src="../js/ddaccordion-menu.js"></script>
<script type="text/javascript" src="../js/custom-form-elements-load.js"></script>
<script type="text/javascript" src="../Scripts/jquery.colorbox.js"></script>
<script type="text/javascript" language="javascript">
    ddaccordion.init({
        headerclass: "submenuheader", //Shared CSS class name of headers group
        contentclass: "submenu", //Shared CSS class name of contents group
        revealtype: "click", //Reveal content when user clicks or onmouseover the header? Valid value: "click", "clickgo", or "mouseover"
        mouseoverdelay: 200, //if revealtype="mouseover", set delay in milliseconds before header expands onMouseover
        collapseprev: false, //Collapse previous content (so only one open at any time)? true/false 
        defaultexpanded: [0,1,2], //index of content(s) open by default [index1, index2, etc] [] denotes no content
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
							<!--<div class="percent">57%</div>
							<div class="content">IS YOUR<br />SCORE</div>-->
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
							<a href="#">Report Centre</a>&nbsp; &gt; &nbsp;<a href="#">Procurement Assessment Results</a>&nbsp; <span>&gt; &nbsp;Reports</span>
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
								<div class="wide640">
									<p class="txt28 orange mrg-bt-5 floatL">Reports</p>
									<%--<p class="floatR txt14">
                                    <a id="sendResult" href="#"><span class="icon-email">Send results<br />to my email</span></a>
                                    </p>--%>
									<div class="clear"></div>
									<p>&nbsp;</p>
									
                                    <asp:Repeater id="rptSgaTest" runat="server" onitemdatabound="rptSgaTest_ItemDataBound" onitemcommand="rptSgaTest_ItemCommand">
                                        <ItemTemplate>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
											<td width="40%"><span class="dark">Time stamp</span><br />
                                            
                                            <asp:Label ID="lblConvertedDate" runat="server" ></asp:Label>
                                            <asp:Label ID="lblDate" runat="server" Visible="false"  Text='<%#Eval("testDate")%>' CssClass="adminheader2"></asp:Label>

                                            </td>
											<td width="30%"><span class="dark">Score</span><br />
                                            <%#Eval("percentage") %> %
                                            </td>
											
                                            <td width="10%">
                                            <asp:ImageButton ID="ibtnGraph" runat="server" CommandArgument='<%#Eval("testId") %>' CommandName="bar" ImageUrl="~/innerimages/img-graph-icon.gif" />
                                            </td>
											<%--<td width="10%"><input type="checkbox" class="styled" id="selectedtest" name="selectedtest" value="<%#Eval("testId") %>" /></td>--%>
										</tr>
                                        <hr class="divider-line" />
                                        </table>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    
									<p>&nbsp;</p>
									<p>&nbsp;</p>
                                    <div class="floatR">
                                        <asp:Button ID="btnprev" runat="server" OnClick="btnprev_Click" class="btn-save" Text="Back"></asp:Button>
                                        <asp:Button ID="btnnext" runat="server" OnClick="btnnext_Click" class="btn-next" Text="Next"></asp:Button>
                                    </div>
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
				<!-- Content Area end // -->
                <script type="text/javascript" language="javascript">
                    function StyleRadio() {
                        $('table.styled input:radio').addClass("styled");
                        Custom.init();
                    }
                    var alertHtml = '';
                    var alertTitle = '';

                    /*$('sendResult').colorbox({
                        href: "../Popup.aspx",
                        width: "392px",
                        height: "200px",
                        onComplete: function () {
                            $("#title").text(alertTitle);
                            $('#alertMessage').text(alertHtml);
                        }
                    });

                    $('sendResult').click(function () {
                        var error = 0;
                        var emptyFields = new Array();
                        var specification = "";

                        $("input[name='selectedtest']:checked").each(function () {
                            specification += $(this).val() + ",";
                        });

                        if (specification.length <= 0) {
                            error = 1;
                        }

                        if (error) {
                            $('#colorbox').css({ "display": "block" });

                            alertTitle = 'Please select';
                            alertHtml = 'To have your results sent to you be email, please select the results you would like sent by ticking the \'tick-box\'.';
                        }
                        else {
                            var json =
                        $.ajax({
                            type: "POST",
                            async: false,
                            url: "my-results-reports-ssa.aspx/EmailResultBack",
                            data: JSON.stringify({ 'testIds': specification }),
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                if (data.d == 's') {
                                    alertTitle = 'Success!';
                                    $('#colorbox').css({ "display": "block" });
                                    alertHtml = 'Your results have now been sent to you by email. Please check your inbox!';
                                }
                            }
                        });
                        }
                        $('#colorbox').css({ "display": "block" });
                    });*/
                 </script>
</asp:Content>

