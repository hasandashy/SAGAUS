<%@ Page  Language="C#" MasterPageFile="~/webadmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="BrowserTracker.aspx.cs" Inherits="SGA.webadmin.BrowserTracker" %>
<%@ Register TagName="left" TagPrefix="sga" Src="~/controls/ctrlLeftMenu.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script> 
<style>
.ui-menu .ui-menu-item a
{
    font-weight:normal;
    font-family:'MyriadProRegular';
    font-size:13px;    
}
</style>
<script type="text/javascript">
    $(document).ready(function () {
        SearchText();
    });
    function SearchText() {
        $(".autosuggest").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "BrowserTracker.aspx/GetAutoCompleteData",
                    data: "{'email':'" + document.getElementById('<%=txtEmail.ClientID %>').value + "'}",
                    dataType: "json",
                    success: function (data) {
                        response(data.d);
                    },
                    error: function (result) {
                        alert("Error");
                    }
                });
            }
        });
    }
</script>
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
                                    User Browser Tracker
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
                                                <asp:Panel ID="pnlList" runat="server">
                                                <tr>
                                                    <td class="grybox">
                                                    <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    User email
                                                                </td>
                                                                <td>
                                                                    <input type="text" name="txtEmail" id="txtEmail" class="autosuggest" runat="server" maxlength="250" />
                                                                </td>
                                                                <td class="txtrht">
                                                                    Browser name
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlEmailTemplate" runat="server">
                                                        </asp:DropDownList> 
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:LinkButton ID="lnkSearch" runat="server" CausesValidation="false" 
                                        Text="Search" CssClass="rdbut" OnClick="lnkSearch_Click"  ></asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" 
                                        Text="Cancel" CssClass="rdbut" OnClick="lnkCancel_Click"  ></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMsg" runat="server" CssClass="error" style="font-weight:bold"></asp:Label>
                                                        <br />
                                                        <asp:DataGrid ID="dtgList" runat="server" AllowPaging="True" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" 
                                                OnItemDataBound="dtgList_ItemDataBound" OnPageIndexChanged="dtgList_PageIndexChanged" OnSortCommand="dtgList_SortCommand" 
                                                Width="100%" GridLines="None" PageSize="20">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="Date" SortExpression="insDt" ItemStyle-width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSenddate" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="email" SortExpression="email" ItemStyle-Width="25%" HeaderText="Email Address" HeaderStyle-Width="25%" >
                                                    </asp:BoundColumn>
                                                    
                                                    <asp:BoundColumn DataField="browsername" SortExpression="browsername" ItemStyle-Width="18%" HeaderText="Browser name" HeaderStyle-Width="18%" >
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Operating system"  ItemStyle-width="20%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOsname" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td>
                                                        &nbsp;
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
</asp:Content>

