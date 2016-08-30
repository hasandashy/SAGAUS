<%@ Page Language="C#" MasterPageFile="~/ManagerMaster.Master" AutoEventWireup="true" CodeBehind="editResult.aspx.cs" Inherits="SGA.manager.editResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Custom Form -->
<script type="text/javascript" src="../js/custom-form-elements-load.js"></script>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        var directSelected = "<%=_user %>";
        var dirSel = directSelected.split(',');
        for (var i = 0; i < dirSel.length; i++) {
            $("input[name='user']").each(function () {
                if ($(this).val() == dirSel[i]) {
                    $(this).attr("checked", "checked");
                }
            });
        }
    });
</script>
<style>
#ContentPlaceHolder1_grdQuestions tr
{
    border-bottom:1px solid #cccccc;    
}
</style>
<!-- Content Area start -->
				<article id="container">
					<section class="welcome-test">
						<p class="title40 floatL">Edit Results</p>
						<br>
						<div class="clear"></div>
					</section>
					<div class="dot-line">&nbsp;</div>
					
					<section class="color-box">
						<article class="info-box-shdw editdashboard">
						  <table width="100%" border="0" cellspacing="2" cellpadding="5">
						    <tr>
						      <td width="70%"><strong>Company Users </strong></td>
						      <td width="30%" align="left"><strong>Assessment Type</strong></td>
					        </tr>
						    <tr>
						      <td>
                                    <asp:DataList ID="ddlUsers" runat="server" RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal">
                                        <ItemTemplate>
                                            <div style="padding:5px;">
										    <input type="checkbox" name="user" id="user" value='<%#Eval("Id") %>' class="styled" />
										    <%#Eval("name") %></div>
                                            <div class="clear"></div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                    
								</td>
						      <td align="left">
                              <asp:DropDownList ID="ddlAssessmentType" runat="server" AutoPostBack="True" 
                                      onselectedindexchanged="ddlAssessmentType_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select Assessment</asp:ListItem>
                                <asp:ListItem Value="1">Skills Self Assessment</asp:ListItem>
                                <asp:ListItem Value="2">Behavioural Assessment</asp:ListItem>
                                <asp:ListItem Value="3">Negotiation Profile Assessment</asp:ListItem>
                                <asp:ListItem Value="4">Department Maturity Profile Assessment</asp:ListItem>
                                <asp:ListItem Value="5">Contract Management Assessment</asp:ListItem>
                              </asp:DropDownList>
                              <br />
                              <asp:DropDownList ID="ddlTopics" runat="server">
                                <asp:ListItem Value="0">Select Topics</asp:ListItem>
                              </asp:DropDownList>
                              </td>
					        </tr>
                            <tr>
						      <td colspan="2"><strong>Job Role </strong>
                              <br />
                               <asp:DropDownList ID="ddlJobRole"  runat="server">
                            <asp:ListItem Value="0">Job role best described as ...</asp:ListItem>
                            <asp:ListItem Value="1">Analyst</asp:ListItem>
                            <asp:ListItem Value="2">Procurement Support</asp:ListItem>
                            <asp:ListItem Value="3">Strategic Sourcing</asp:ListItem>
                            <asp:ListItem Value="4">Vendor Manager/ Supplier Relationship Manager</asp:ListItem>
                            <asp:ListItem Value="5">Category Manager</asp:ListItem>
                            <asp:ListItem Value="6">Procurement Leader</asp:ListItem>
                            <asp:ListItem Value="7">Supply Chain</asp:ListItem>
                            <asp:ListItem Value="8">Non-Procurement: CXO</asp:ListItem>
                            <asp:ListItem Value="9">Non-Procurement: Director</asp:ListItem>
                            <asp:ListItem Value="10">Non-Procurement: Manager</asp:ListItem>
                            <asp:ListItem Value="11">Non-Procurement: Professional</asp:ListItem>
                            <asp:ListItem Value="12">Non-Procurement: Trainee</asp:ListItem>
                            </asp:DropDownList>
                              </td>
					        </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblError" runat="server" CssClass="error"></asp:Label>
                                    <br />
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn-next" Text="Filter" 
                                        onclick="btnSubmit_Click" />
                                </td>
                            </tr>
					      </table>
						 
					  </article>
                      <article class="info-box-shdw editdashboard">
                          <div class="floatR">
                          <asp:Button ID="btnUpdateAllTop" runat="server" style="float:right" CssClass="my-profile" CausesValidation="false" 
                                                            Text="Update All" onclick="btnUpdateAllTop_Click" />
                          </div>
                          <br>
						<div class="clear"></div>
						  <asp:DataGrid ID="grdQuestions" runat="server" 
        ShowFooter="false" ShowHeader="true" AutoGenerateColumns="false" 
        GridLines="None" AllowPaging="false" AllowSorting="false" 
        onitemdatabound="grdQuestions_ItemDataBound" 
        onitemcommand="grdQuestions_ItemCommand">
        <HeaderStyle Font-Bold="true" />
        
                            <Columns>
                                <asp:TemplateColumn FooterStyle-BorderWidth="1" HeaderText="Question" ItemStyle-Width="60%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%#Eval("questionname")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn FooterStyle-BorderWidth="1" HeaderText="Username" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%#Eval("name")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn FooterStyle-BorderWidth="1" HeaderText="Score" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%#Eval("optionValue")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn FooterStyle-BorderWidth="1" HeaderText="Reviewed Score" ItemStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlMarks" Width="70%" runat="server"></asp:DropDownList>&nbsp;<asp:Image ID="imgArrow" runat="server" style="width:16px;height:16px" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn FooterStyle-BorderWidth="1" HeaderText="Update" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" runat="server" Text="Update" CommandName="edit" CssClass="btn-next" CommandArgument='<%#Eval("replyId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                          </asp:DataGrid>
                          <br />
						<div class="clear"></div>
                          <div class="floatR">
						  <asp:Button ID="btnUpdateAllBottom" runat="server" style="float:right" CssClass="my-profile" CausesValidation="false" 
                                                            Text="Update All" onclick="btnUpdateAllTop_Click" /> 
                           
                         </div>
                         <div  style="height:50px;background-color:#ffffff;display:block;width:100%;"></div>
					  </article>
					</section>
                    <section class="color-box">
						
					</section>
					<div class="dot-line">&nbsp;</div>
				</article>
				<!-- Content Area end // -->
<script type="text/javascript" language="javascript">
    function StyleRadio() {
        $('table.styled input:radio').addClass("styled");
        Custom.init();
    }

    </script>
</asp:Content>

