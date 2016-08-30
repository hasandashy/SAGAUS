<%@ Page Language="C#" MasterPageFile="~/webadmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ManagePlans.aspx.cs" Inherits="SGA.webadmin.ManagePlans" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register TagName="left" TagPrefix="sga" Src="~/controls/ctrlLeftMenu.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- SimpleTabs -->
    <link rel="stylesheet" href="css/jquery-ui.css">
<script  src="//code.jquery.com/jquery-1.9.1.js"></script>
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
                                    Manage Elearning Plans
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="tabs">
                                        <ul >
                                            <li><a href="#tabs-1">Procurement Management Plans</a></li>
                                            <li><a href="#tabs-2">Contract Management Plans</a></li>
                                        </ul>
                                        
                                        <div id="tabs-1">
                                                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        MANAGE Procurement Management Plans
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
                                                    <asp:BoundColumn DataField="planId" HeaderText="Plan Id" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="planName" HeaderText="Plan Heading" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="timeWeek" HeaderText="Completion Time" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%" HeaderStyle-Width="8%" >
                                                    </asp:BoundColumn>
                                                    
                                                    <asp:TemplateColumn HeaderText="Edit" ItemStyle-width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("planId") %>' CommandName="Edit" ImageUrl="~/webadmin/images/edit.png" />
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
                                                                    Plan Heading
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSSAPlanName" CssClass="txtBig" runat="server" MaxLength="250" style="width:698px;" ValidationGroup="ssa"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Plan Completion Time
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSSAPlanCompletionTime" CssClass="txtBig" runat="server" MaxLength="250" style="width:698px;" ValidationGroup="ssa"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Plan Details
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSAPlanDetails" runat="server" Height="400px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            
                                                            
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>

                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td >
                                                                    <asp:ImageButton ID="imgSSAEdit" runat="server" OnClick="imgSSAEdit_Click" ImageUrl="~/webadmin/images/save.png"  />
                                                                    <asp:ImageButton ID="imgSSACancel" runat="server" OnClick="imgSSACancel_Click" ImageUrl="~/webadmin/images/close.png"  CausesValidation="false" />
                                                                    
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
                                                        MANAGE Contract Management Plans
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
                                                    <asp:BoundColumn DataField="planId" HeaderText="Plan Id" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="planName" HeaderText="Plan Heading" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="timeWeek" HeaderText="Completion Time" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%" HeaderStyle-Width="8%" >
                                                    </asp:BoundColumn>
                                                    
                                                    <asp:TemplateColumn HeaderText="Edit" ItemStyle-width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("planId") %>' CommandName="Edit" ImageUrl="~/webadmin/images/edit.png" />
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
                                                                    Plan Heading
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCMAPlanName" CssClass="txtBig" runat="server" MaxLength="250" style="width:698px;" ValidationGroup="ssa"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Plan Completion Time
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCMAPlanCompletionTime" CssClass="txtBig" runat="server" MaxLength="250" style="width:698px;" ValidationGroup="ssa"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Plan Details
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMAPlanDetails" runat="server" Height="400px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            
                                                            
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>

                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td >
                                                                    <asp:ImageButton ID="imgCMAEdit" runat="server" OnClick="imgCMAEdit_Click" ImageUrl="~/webadmin/images/save.png"  />
                                                                    <asp:ImageButton ID="imgCMACancel" runat="server" OnClick="imgCMACancel_Click" ImageUrl="~/webadmin/images/close.png"  CausesValidation="false" />
                                                                    
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
