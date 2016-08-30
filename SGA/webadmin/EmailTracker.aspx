<%@ Page Language="C#" MasterPageFile="~/webadmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EmailTracker.aspx.cs" Inherits="SGA.webadmin.EmailTracker" %>
<%@ Register TagName="left" TagPrefix="sga" Src="~/controls/ctrlLeftMenu.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                    url: "EmailTracker.aspx/GetAutoCompleteData",
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
                                    Send Email Tracker
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
                                                                    Send Date
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtSenddate" style="width:100px" ></asp:TextBox>
                                                    <asp:ImageButton ID="imgStartdate" runat="server" Height="16px" ImageUrl="~/Images/cal.gif"
                                                        Width="16px" ImageAlign="Bottom" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgStartDate"
                                                        TargetControlID="txtSenddate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Email Template
                                                                </td>
                                                                <td colspan="3">
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
                                                        <asp:DataGrid ID="dtgList" runat="server" AllowPaging="True" AllowSorting="true"
                                                AutoGenerateColumns="False" CssClass="grdMain" 
                                                OnItemDataBound="dtgList_ItemDataBound" OnPageIndexChanged="dtgList_PageIndexChanged" OnSortCommand="dtgList_SortCommand"  OnItemCommand="dtgList_ItemCommand"
                                                Width="100%" GridLines="None" PageSize="20">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:BoundColumn DataField="emailReceiver" SortExpression="emailReceiver" ItemStyle-Width="15%" HeaderText="Email to" HeaderStyle-Width="15%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="subject" SortExpression="subject" ItemStyle-Width="48%" HeaderText="Email subject" HeaderStyle-Width="48%" >
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Send date" SortExpression="sendDate" ItemStyle-width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSenddate" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="emailFrom" SortExpression="emailFrom" ItemStyle-Width="10%" HeaderText="Email from" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                    
                                                    <asp:TemplateColumn HeaderText="View" ItemStyle-width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEmail" runat="server" CausesValidation="false" AlternateText="Email details" CommandArgument='<%#Eval("Id") %>' CommandName="Email" ToolTip="email details" ImageUrl="~/webadmin/images/mail-32.png" />
                                                            <asp:ImageButton ID="iBtnResend" runat="server" CausesValidation="false" AlternateText="Resend email" CommandArgument='<%#Eval("Id") %>' CommandName="Resend" ToolTip="resend email"  ImageUrl="~/webadmin/images/resend-48.png" />
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
                                                <asp:Panel ID="pnlDetail" Visible="false"  runat="server">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td width="20%"><b>Email Reciver :</b></td>
                                                                <td width="80%">
                                                                    <asp:Label ID="lblReceiver" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td ><b>Email Sent :</b></td>
                                                                <td >
                                                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td ><b>Email Subject :</b></td>
                                                                <td >
                                                                    <asp:Label ID="lblSubject" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td ><b>Email Body :</b></td>
                                                                <td >
                                                                    <asp:Label ID="lblEmailBody" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" CausesValidation="false" Text="Back <<" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
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

