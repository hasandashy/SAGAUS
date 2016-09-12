<%@ Page Language="C#"  AutoEventWireup="true" MasterPageFile="~/webadmin/AdminMaster.Master" CodeBehind="ManageReportSuggestions.aspx.cs" Inherits="SGA.webadmin.ManageReportSuggestions" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register TagName="left" TagPrefix="sga" Src="~/controls/ctrlLeftMenu.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!-- SimpleTabs -->
    <link rel="stylesheet" href="css/jquery-ui.css">
<script src="//code.jquery.com/jquery-1.9.1.js"></script>
<script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
<script>
    var selected_tab = 1;
    $(function () {
        var tabs = $("#tabs").tabs({
            activate: function (event, ui) {
                //alert(ui.newTab.index());
                selected_tab = ui.newTab.index();
            }
        });
        selected_tab = $("[id$=selected_tab]").val() != "" ? parseInt($("[id$=selected_tab]").val()) : 0;
        //alert(selected_tab);
        tabs.tabs("option", "active", selected_tab);
        $("form").submit(function () {
            $("[id$=selected_tab]").val(selected_tab);
        });
    });
</script>
<asp:HiddenField ID="selected_tab" runat="server" />
    <tr>
        <td class="inrbg">
            <table width="1280" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="227" valign="top" class="lftnav">
                        <sga:left id="menu1" runat="server"></sga:left>
                    </td>
                    <td valign="top">
                        <table width="99%" border="0" align="right" cellpadding="0" cellspacing="0" class="panbox">
                            <tr>
                                <td class="hd26">
                                    Manage Report Suggestions
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="tabs">
                                        <ul >
                                            <li><a href="#tabs-1">Purchasing Officer Pack</a></li>
                                            <li><a href="#tabs-2">Procurement Support Pack</a></li>
                                            <li><a href="#tabs-3">Procurement Analyst Pack</a></li>
                                             <li><a href="#tabs-4">Procurement Advisor Pack</a></li>
                                            <li><a href="#tabs-5">Procurement Specialist Pack</a></li>
                                            <li><a href="#tabs-6">Contract Manager Pack</a></li>
                                             <li><a href="#tabs-7">Category Manager Pack</a></li>
                                            <li><a href="#tabs-8">Procurement Director Pack</a></li>
                                                                                    </ul>
                                        
                                        <div id="tabs-1">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        PURCHASING OFFICER PACK SUGGESTIONS
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlSSAList" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdSSASuggestions" runat="server" AllowPaging="false" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" OnItemCommand="grdSSASuggestions_ItemCommand"
                                                Width="100%" GridLines="None" PageSize="10">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:BoundColumn DataField="Id" ItemStyle-Width="12%" HeaderText="Suggestion Id" HeaderStyle-Width="12%" >
                                                    </asp:BoundColumn>
                                                   
                                                    <asp:BoundColumn DataField="topicName" HeaderText="Topic Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-Width="20%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SuggestionText" HeaderText="Suggestion Defination" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderStyle-Width="60%" >
                                                    </asp:BoundColumn> 
                                                    
                                                    <asp:TemplateColumn HeaderText="Edit" ItemStyle-width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Id") %>' CommandName="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <asp:Panel ID="pnlSSAEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Suggestion Defination
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSASuggestion" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr style="display:none">
                                                                <td class="txtrht">
                                                                    Recommendations
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSARecommendation" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="2">
                                                                    <asp:ImageButton ID="imgSSAEdit" runat="server" ImageUrl="~/webadmin/images/save.png" OnClick="imgSSAEdit_Click" />
                                                                    <asp:ImageButton ID="imgSSACancel" runat="server" ImageUrl="~/webadmin/images/close.png" OnClick="imgSSACancel_Click" CausesValidation="false" />
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>
                                        <div id="tabs-2">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        PROCUREMENT SUPPORT PACK SUGGESTIONS
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlBAList" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdBASuggestions" runat="server" AllowPaging="false" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" OnItemCommand="grdBASuggestions_ItemCommand" 
                                                Width="100%" GridLines="None" PageSize="10">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:BoundColumn DataField="Id" ItemStyle-Width="12%" HeaderText="Suggestion Id" HeaderStyle-Width="12%" >
                                                    </asp:BoundColumn>
                                                   
                                                    <asp:BoundColumn DataField="topicName" HeaderText="Topic Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-Width="20%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SuggestionText" HeaderText="Suggestion Defination" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderStyle-Width="60%" >
                                                    </asp:BoundColumn> 
                                                    
                                                    <asp:TemplateColumn HeaderText="Edit" ItemStyle-width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Id") %>' CommandName="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <asp:Panel ID="pnlBAEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Suggestion Defination
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtBADefination" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr style="display:none">
                                                                <td class="txtrht">
                                                                    Considerations
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtBAConsideration" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="2">
                                                                    <asp:ImageButton ID="imgBAEdit" runat="server" ImageUrl="~/webadmin/images/save.png" OnClick="imgBAEdit_Click" />
                                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/webadmin/images/close.png" OnClick="imgBACancel_Click" CausesValidation="false" />
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>                                        
                                        <div id="tabs-3">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        PROCUREMENT ANALYST PACK SUGGESTIONS
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlCMAList" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdCMASuggestions" runat="server" AllowPaging="false" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" OnItemCommand="grdCMASuggestions_ItemCommand"
                                                Width="100%" GridLines="None" PageSize="10">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:BoundColumn DataField="Id" ItemStyle-Width="12%" HeaderText="Suggestion Id" HeaderStyle-Width="12%" >
                                                    </asp:BoundColumn>
                                                   
                                                    <asp:BoundColumn DataField="topicName" HeaderText="Topic Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-Width="20%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SuggestionText" HeaderText="Suggestion Defination" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderStyle-Width="60%" >
                                                    </asp:BoundColumn> 
                                                    
                                                    <asp:TemplateColumn HeaderText="Edit" ItemStyle-width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Id") %>' CommandName="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <asp:Panel ID="pnlCMAEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Suggestion Defination
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMASuggestion" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr style="display:none">
                                                                <td class="txtrht">
                                                                    Recommendations
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMARecommendation" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="2">
                                                                    <asp:ImageButton ID="imgCMAEdit" runat="server" ImageUrl="~/webadmin/images/save.png" OnClick="imgCMAEdit_Click" />
                                                                    <asp:ImageButton ID="imgCMACancel" runat="server" ImageUrl="~/webadmin/images/close.png" OnClick="imgCMACancel_Click" CausesValidation="false" />
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>
                                        <div id="tabs-4">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        PROCUREMENT ADVISOR PACK SUGGESTIONS
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlAdvisorList" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdAdvisorSuggestions" runat="server" AllowPaging="false" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" OnItemCommand="grdAdvisorSuggestions_ItemCommand"
                                                Width="100%" GridLines="None" PageSize="10">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:BoundColumn DataField="Id" ItemStyle-Width="12%" HeaderText="Suggestion Id" HeaderStyle-Width="12%" >
                                                    </asp:BoundColumn>
                                                   
                                                    <asp:BoundColumn DataField="topicName" HeaderText="Topic Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-Width="20%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SuggestionText" HeaderText="Suggestion Defination" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderStyle-Width="60%" >
                                                    </asp:BoundColumn> 
                                                    
                                                    <asp:TemplateColumn HeaderText="Edit" ItemStyle-width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Id") %>' CommandName="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <asp:Panel ID="pnlAdvisorEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Suggestion Defination
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtAdvisorDefination" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr style="display:none">
                                                                <td class="txtrht">
                                                                    Recommendations
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtAdvisorConsidration" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="2">
                                                                    <asp:ImageButton ID="ImgAdvisorEdit" runat="server" ImageUrl="~/webadmin/images/save.png" OnClick="imgAdvisorEdit_Click" />
                                                                    <asp:ImageButton ID="ImgAdvisorCancel" runat="server" ImageUrl="~/webadmin/images/close.png" OnClick="imgAdvisorCancel_Click" CausesValidation="false" />
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>
                                        <div id="tabs-5">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        Procurement Specialist Pack SUGGESTIONS
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlSpecialistList" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdSpecialistSuggetions" runat="server" AllowPaging="false" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" OnItemCommand="grdSpecialistSuggetions_ItemCommand" 
                                                Width="100%" GridLines="None" PageSize="10">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:BoundColumn DataField="Id" ItemStyle-Width="12%" HeaderText="Suggestion Id" HeaderStyle-Width="12%" >
                                                    </asp:BoundColumn>
                                                   
                                                    <asp:BoundColumn DataField="topicName" HeaderText="Topic Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-Width="20%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SuggestionText" HeaderText="Suggestion Defination" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderStyle-Width="60%" >
                                                    </asp:BoundColumn> 
                                                    
                                                    <asp:TemplateColumn HeaderText="Edit" ItemStyle-width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Id") %>' CommandName="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <asp:Panel ID="pnlSpecialistEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Suggestion Defination
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSpecialistDefination" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr style="display:none">
                                                                <td class="txtrht">
                                                                    Considerations
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSpecialistConsidration" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="2">
                                                                    <asp:ImageButton ID="ImgSpecialistEdit" runat="server" ImageUrl="~/webadmin/images/save.png" OnClick="ImgSpecialistEdit_Click" />
                                                                    <asp:ImageButton ID="ImgSpecialistCancel" runat="server" ImageUrl="~/webadmin/images/close.png" OnClick="ImgSpecialistCancel_Click" CausesValidation="false" />
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>                                        
                                        <div id="tabs-6">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        CONTRACT MANAGER PACK SUGGESTIONS
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlContractManagerList" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdContractManagerSuggestions" runat="server" AllowPaging="false" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" OnItemCommand="grdContractManagerSuggestions_ItemCommand"
                                                Width="100%" GridLines="None" PageSize="10">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:BoundColumn DataField="Id" ItemStyle-Width="12%" HeaderText="Suggestion Id" HeaderStyle-Width="12%" >
                                                    </asp:BoundColumn>
                                                   
                                                    <asp:BoundColumn DataField="topicName" HeaderText="Topic Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-Width="20%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SuggestionText" HeaderText="Suggestion Defination" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderStyle-Width="60%" >
                                                    </asp:BoundColumn> 
                                                    
                                                    <asp:TemplateColumn HeaderText="Edit" ItemStyle-width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Id") %>' CommandName="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <asp:Panel ID="pnlContractManagerEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Suggestion Defination
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtContractManagerDefination" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr style="display:none">
                                                                <td class="txtrht">
                                                                    Recommendations
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtContractManagerConsidration" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="2">
                                                                    <asp:ImageButton ID="ImgContractManagerEdit" runat="server" ImageUrl="~/webadmin/images/save.png" OnClick="ImgContractManagerEdit_Click" />
                                                                    <asp:ImageButton ID="ImgContractManagerCancel" runat="server" ImageUrl="~/webadmin/images/close.png" OnClick="ImgContractManagerCancel_Click" CausesValidation="false" />
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>
                                        <div id="tabs-7">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        CATEGORY MANAGER PACK SUGGESTIONS
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlCategoryManagerList" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdCategoryManagerSuggestions" runat="server" AllowPaging="false" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" OnItemCommand="grdCategoryManagerSuggestions_ItemCommand"
                                                Width="100%" GridLines="None" PageSize="10">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:BoundColumn DataField="Id" ItemStyle-Width="12%" HeaderText="Suggestion Id" HeaderStyle-Width="12%" >
                                                    </asp:BoundColumn>
                                                   
                                                    <asp:BoundColumn DataField="topicName" HeaderText="Topic Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-Width="20%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SuggestionText" HeaderText="Suggestion Defination" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderStyle-Width="60%" >
                                                    </asp:BoundColumn> 
                                                    
                                                    <asp:TemplateColumn HeaderText="Edit" ItemStyle-width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Id") %>' CommandName="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <asp:Panel ID="pnlCategoryManagerEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Suggestion Defination
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCategoryManagerDefination" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr style="display:none">
                                                                <td class="txtrht">
                                                                    Recommendations
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCategoryManagerConsidration" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="2">
                                                                    <asp:ImageButton ID="ImgCatManagerEdit" runat="server" ImageUrl="~/webadmin/images/save.png" OnClick="ImgCatManagerEdit_Click" />
                                                                    <asp:ImageButton ID="ImgCatManagerCancel" runat="server" ImageUrl="~/webadmin/images/close.png" OnClick="ImgCatManagerCancel_Click" CausesValidation="false" />
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>
                                        <div id="tabs-8">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        PROCUREMENT DIRECTOR PACK SUGGESTIONS
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlDirectorList" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdDirectorSuggetsions" runat="server" AllowPaging="false" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" OnItemCommand="grdDirectorSuggetsions_ItemCommand" 
                                                Width="100%" GridLines="None" PageSize="10">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:BoundColumn DataField="Id" ItemStyle-Width="12%" HeaderText="Suggestion Id" HeaderStyle-Width="12%" >
                                                    </asp:BoundColumn>
                                                   
                                                    <asp:BoundColumn DataField="topicName" HeaderText="Topic Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-Width="20%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SuggestionText" HeaderText="Suggestion Defination" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderStyle-Width="60%" >
                                                    </asp:BoundColumn> 
                                                    
                                                    <asp:TemplateColumn HeaderText="Edit" ItemStyle-width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Id") %>' CommandName="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <asp:Panel ID="pnlDirectorEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Suggestion Defination
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtDirectorDefination" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr style="display:none">
                                                                <td class="txtrht">
                                                                    Considerations
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtDirectorConsidration" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="2">
                                                                    <asp:ImageButton ID="ImgDirectorEdit" runat="server" ImageUrl="~/webadmin/images/save.png" OnClick="ImgDirectorEdit_Click" />
                                                                    <asp:ImageButton ID="ImgDirectorCancel" runat="server" ImageUrl="~/webadmin/images/close.png" OnClick="ImgDirectorCancel_Click" CausesValidation="false" />
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>  
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</asp:Content>



