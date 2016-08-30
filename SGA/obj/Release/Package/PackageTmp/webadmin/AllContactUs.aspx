<%@ Page  Language="C#" MasterPageFile="~/webadmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AllContactUs.aspx.cs" Inherits="SGA.webadmin.AllContactUs" %>
<%@ Register TagName="left" TagPrefix="sga" Src="~/controls/ctrlLeftMenu.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                                    ALL STANDING OFFER ARRANGEMENT REQUEST
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
                                                        <asp:DataGrid ID="dtgList" runat="server" AllowPaging="True" AllowSorting="true"
                                                AutoGenerateColumns="False" CssClass="grdMain" 
                                                OnItemDataBound="dtgList_ItemDataBound" OnPageIndexChanged="dtgList_PageIndexChanged" OnSortCommand="dtgList_SortCommand" 
                                                Width="100%" GridLines="None" PageSize="20">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:BoundColumn DataField="Name" SortExpression="firstName" ItemStyle-Width="15%" HeaderText="Name" HeaderStyle-Width="15%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="organisation" SortExpression="organisation" ItemStyle-Width="15%" HeaderText="Organisation" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="email" SortExpression="email" ItemStyle-Width="15%" HeaderText="Email Address" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                    
                                                    <asp:BoundColumn DataField="interest" SortExpression="interest" ItemStyle-Width="50%" HeaderText="Interest" HeaderStyle-Width="50%" >
                                                    </asp:BoundColumn>
                                                   
                                                    <asp:TemplateColumn HeaderText="Date" SortExpression="sendDate" ItemStyle-width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSenddate" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
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