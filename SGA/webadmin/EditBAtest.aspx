<%@ Page Language="C#" MasterPageFile="~/webadmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EditBAtest.aspx.cs" Inherits="SGA.webadmin.EditBAtest" %>
<%@ Register TagName="left" TagPrefix="sga" Src="~/controls/ctrlLeftMenu.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!-- Stylesheet -->
		<link rel="stylesheet" href="../css/style.css" type="text/css" media="screen" />
<!-- Custom Form -->
<script type="text/javascript" src="../js/custom-form-elements-load.js"></script>
<!-- Accordion Menu -->
<script type="text/javascript" src="../js/jquery.min.js"></script>
<script type="text/javascript" src="../Scripts/jquery.colorbox.js"></script>
<script type="text/javascript" src="../js/custom.js"></script>
<style>
label, input.radio{
    margin-left: -22px;
    margin-top: -21px;
    position: absolute;
}
</style>
<script type="text/javascript" language="javascript">
    var alertHtml = '';
    var lastpage='n';
    function FinalSubmit(){
            document.getElementById("<%=btnFinal.ClientID %>").click();
        }
    $(document).ready(function () {

        $(".color").colorbox({
            href: "../Popup.aspx",
            width: "392px",
            height: "220px",
            onComplete: function () {
                if (alertHtml.length > 1) {
                    $('#colorbox').css({ "display": "block" });
                    $('#alertMessage').text(alertHtml);
                } else {
                    $('#colorbox').css({ "display": "none" });
                    if(alertHtml=="n"){
                        document.getElementById("<%=btnSubmitNext.ClientID %>").click();    
                        //$("#<%=pgNumber.ClientID %>").val("");
                    }else{
                        document.getElementById("<%=lnkPrev.ClientID %>").click();    
                        //$("#<%=pgNumber.ClientID %>").val("");
                    }
                    
                }
            }
        });

        $(".color").click(function () {
            var topicId = this.id.substr(this.id.length-1);
            var currentTopicId = <%=PageNumber %>;
            
            if(topicId > currentTopicId){
                // check for validation and press next
                var ct = $("#<%=hdCount.ClientID %>").val();
                var unsq = "";
                var pt = "";
                for (j = 0; j < ct; j++) {
                    if (j <= 9) {
                        pt = '0' + j;
                    } else {
                        pt = j;
                    }
                    var rbc = document.getElementsByName("ctl00$ContentPlaceHolder1$parentRepeater$ctl" + pt + "$rbtnlst");
                    for (i = 0; i < rbc.length; i++) {
                        if (rbc[i].checked == true) {
                            break;
                        }
                    }
                    if (i == rbc.length) {
                        unsq = unsq + (j + 1) + ",";
                    }
                }
                if (unsq != "") {
                    //alert("You have not answered one or all of the questions on this page, please select an option before continuing.");
                    alertHtml = "You have not answered one or all of the questions on this page, please select an option before continuing.";

                }
                else {
                    $("#<%=pgNumber.ClientID %>").val(topicId);
                    alertHtml = "n";
                    return true;
                }
            }else{
                $("#<%=pgNumber.ClientID %>").val(topicId);
                // back button pressed
                alertHtml="b";
                return true;
            }
            
        });

        $("#<%=btnSubmit.ClientID %>").colorbox({
            href: "../Popup.aspx",
            width: "392px",
            height: "220px",
            onComplete: function () {
                if(lastpage=='y'){
                    $('#colorbox').css({ "display": "none" });
                    document.getElementById("<%=btnFinal.ClientID %>").click();
                    /*$('#title').text("Submit assessement");
                    if (alertHtml.length > 1) {
                        $('#colorbox').css({ "display": "block" });
                        $('#alertMessage').text(alertHtml);
                        $('#btnCancel').css("display", "block");
                        $('#btnOk').removeClass("btn-yes");
                        $('#btnOk').addClass("btn-back");
                        $('#btnCancel').removeClass("btn-back");
                        $('#btnCancel').addClass("btn-yes");
                    } */
                }else{
                    if (alertHtml.length > 0) {
                        $('#colorbox').css({ "display": "block" });
                        $('#alertMessage').text(alertHtml);
                    } 
                }
            }
        });

        $("#<%=btnSubmit.ClientID %>").click(function () {
            var ct = $("#<%=hdCount.ClientID %>").val();
            var unsq = "";
            var pt = "";
            for (j = 0; j < ct; j++) {
                if (j <= 9) {
                    pt = '0' + j;
                } else {
                    pt = j;
                }
                var rbc = document.getElementsByName("ctl00$ContentPlaceHolder1$parentRepeater$ctl" + pt + "$rbtnlst");
                for (i = 0; i < rbc.length; i++) {
                    if (rbc[i].checked == true) {
                        break;
                    }
                }
                if (i == rbc.length) {
                    unsq = unsq + (j + 1) + ",";
                }
            }
            if (unsq != "") {
                    //alert("You have not answered one or all of the questions on this page, please select an option before continuing.");
                    alertHtml = "You have not answered one or all of the questions on this page, please select an option before continuing.";
                }
                else {
                    //alertHtml = "Are you sure you want to submit your assessment?";
                    lastpage='y';
                }
        });

        $("#<%=lnkNext.ClientID %>").colorbox({
            href: "../Popup.aspx",
            width: "392px",
            height: "220px",
            onComplete: function () {
                if (alertHtml.length > 0) {
                    $('#colorbox').css({ "display": "block" });
                    $('#alertMessage').text(alertHtml);
                } else {
                    $('#colorbox').css({ "display": "none" });
                    document.getElementById("<%=btnSubmitNext.ClientID %>").click();
                }
            }
        });


        $("#<%=lnkNext.ClientID %>").click(function () {
            var ct = $("#<%=hdCount.ClientID %>").val();
            var unsq = "";
            var pt = "";
            for (j = 0; j < ct; j++) {
                if (j <= 9) {
                    pt = '0' + j;
                } else {
                    pt = j;
                }
                var rbc = document.getElementsByName("ctl00$ContentPlaceHolder1$parentRepeater$ctl" + pt + "$rbtnlst");
                for (i = 0; i < rbc.length; i++) {
                    if (rbc[i].checked == true) {
                        break;
                    }
                }
                if (i == rbc.length) {
                    unsq = unsq + (j + 1) + ",";
                }
            }
            if (unsq != "") {
                //alert("You have not answered one or all of the questions on this page, please select an option before continuing.");
                alertHtml = "You have not answered one or all of the questions on this page, please select an option before continuing.";

            }
            else {
                alertHtml = "";
                return true;
            }
        });

    });
</script>

<tr>
        <td class="inrbg">
            <table width="1280" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="227" valign="top" style="vertical-align:top" class="lftnav">
                        <sga:left id="menu1" runat="server"></sga:left>
                    </td>
                    <td valign="top">
                        <table width="99%" border="0" align="right" cellpadding="0" cellspacing="0" class="panbox">
                            <tr>
                                <td class="hd26">
                                    EDIT Leadership Assessment TEST
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <!-- Content Area start -->
				<article id="container">
					<section class="welcome-test">
						<p class="title40 floatL">Leadership Assessment</p>
						<div class="clear"></div>
					</section>
					<div class="dot-line">&nbsp;</div>
					<article class="navigation">
                        <asp:HiddenField ID="pgNumber" runat="server" />
						<ul><asp:Repeater ID="rptrTopics" runat="server" OnItemCommand="rptrTopics_ItemCommand"
                                        >
                                        <ItemTemplate>
                                        <li><asp:LinkButton ID="lnkBtn" CssClass="color" runat="server" Text='<%#Eval("topicTitle")%>' CommandArgument='<%#Eval("topicId") %>'  CommandName="select">LinkButton</asp:LinkButton></li>    
                                  
                                        </ItemTemplate>
                                    </asp:Repeater>							
						</ul>
                        
					</article>
					<section class="color-box">
						<article class="test-info-box">
							<p class="title">Section <%=PageNumber +1 %>: <span class="orange"><asp:Label ID="lblTopic" runat="server"></asp:Label></span></p>
							<p>&nbsp;</p>
							<p><span class="dark">
                            DEFINITION OF THIS STAGE:</span> <asp:Label ID="lblDescription" runat="server"></asp:Label></p>
							<p>&nbsp;</p>
						</article>
						<article class="scoring-guide">
							<p class="txt18-bold txtCtr">SCORING GUIDE</p>
							<div class="guide-box1 guide-brd">
								<p class="guide-title">Novice</p>
								<div>
									<p class="guide-num">0</p>
									<p>No awareness.</p>
								</div>
							</div>
							<div class="guide-box2 guide-brd">
								<p class="guide-title">Awareness</p>
								<div class="wide50">
									<p class="guide-num">1</p>
									<p>Some awareness, never practiced.</p>
								</div>
								<div class="wide50">
									<p class="guide-num">2</p>
									<p>Starter knowledge, novice in practice.</p>
								</div>
							</div>
							<div class="guide-box2 guide-brd">
								<p class="guide-title">Understanding</p>
								<div class="wide50">
									<p class="guide-num">3</p>
									<p>Some knowledge, occasional practice.</p>
								</div>
								<div class="wide50">
									<p class="guide-num">4</p>
									<p>Intermediate knowledge, intermittent practice.</p>
								</div>
							</div>
							<div class="guide-box2 guide-brd">
								<p class="guide-title">Practitioner</p>
								<div class="wide50">
									<p class="guide-num">5</p>
									<p>Specialist knowledge, recurrent repeated practice</p>
								</div>
								<div class="wide50">
									<p class="guide-num">6</p>
									<p>Expert knowledge, frequent practice.</p>
								</div>
							</div>
							<div class="guide-box1">
								<p class="guide-title">Expert</p>
								<div>
									<p class="guide-num">7</p>
									<p>Master knowledge, constant practice.</p>
								</div>
							</div>
							<div class="clear"></div>
						</article>
						<article class="info-box-shdw-cat-mngmt">
							<div class="test-box">
								<asp:HiddenField ID="hdCount" runat="server" />
                                <asp:Repeater ID="parentRepeater" runat="server" OnItemDataBound="parentRepeater_ItemDataBound">
                                         <ItemTemplate>
                                            <article class="guide-test-cnt">
									            <div class="num">
                                                <asp:Label ID="lblNumber" runat="server"></asp:Label>
                                                </div>
                                                <div class="dtl">
										            <asp:Label ID="lblQuestionId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "questionId")%>'></asp:Label>
                                                    <asp:Label ID="lblQuestionText" runat="server" Text='<%#Eval("questionText")%>'></asp:Label>
                                                    
									            </div>
                                                <div class="opt">
                                                <p>&nbsp;</p>
                                                <asp:RadioButtonList ID="rbtnlst" runat="server"  RepeatDirection="Horizontal" cssclass="styled">                                                                    
                                                </asp:RadioButtonList>
                                                </div>
                                                <div class="clear"></div>
                                            </article>
                                         </ItemTemplate>
                                </asp:Repeater>
                                
								
							</div>
							<p>&nbsp;</p>
							<div class="score-box">
								<div class="score-btn">
                                    
                                    <asp:Button ID="lnkPrev" runat="server" OnClick="lnkPrev_Click" Text="BACK" CssClass="btn-save"
                                                                                Visible="False"></asp:Button>
                                    <asp:Button ID="lnkNext" CssClass="btn-next" runat="server" Text="NEXT" ></asp:Button>
                                    <asp:Button ID="btnSubmit" runat="server"  CssClass="btn-next"
                                                                                    Text="SUBMIT" Visible="False" />
                                    <asp:Button ID="btnSubmitNext" runat="server" Text="submit"  style="display:none"
                                        onclick="btnSubmitNext_Click" />
                                    <asp:Button ID="btnFinal" runat="server" Text="submit"  style="display:none"
                                        onclick="btnFinal_Click" />
                                    </div>
                                    
								<div class="clear"></div>
							</div>
							<p>&nbsp;</p>
						</article>
					</section>
					<div class="dot-line">&nbsp;</div>
				</article>
                                                    </td>
                                                </tr>
                                            </table>
 
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>


                
             <script type="text/javascript" language="javascript">
                 function StyleRadio() {
                     $('table.styled input:radio').addClass("styled");
                     Custom.init();
                 }
                 
    </script>
</asp:Content>
