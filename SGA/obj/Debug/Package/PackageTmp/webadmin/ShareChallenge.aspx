<%@ Page Language="C#" MasterPageFile="~/webadmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ShareChallenge.aspx.cs" Inherits="SGA.webadmin.ShareChallenge" %>
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
                                    Category Management Challenge Share Information
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
                                                    <asp:BoundColumn DataField="firstname" SortExpression="firstname" ItemStyle-Width="10%" HeaderText="First name" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="lastname" SortExpression="lastname" ItemStyle-Width="15%" HeaderText="Last name" HeaderStyle-Width="15%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="email" SortExpression="email" ItemStyle-Width="15%" HeaderText="Email" HeaderStyle-Width="15%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="company" SortExpression="company" ItemStyle-Width="15%" HeaderText="Company" HeaderStyle-Width="15%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="personType" SortExpression="personType" ItemStyle-Width="10%" HeaderText="Type" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="name" SortExpression="name" ItemStyle-Width="20%" HeaderText="Shared By" HeaderStyle-Width="20%" >
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Share date" SortExpression="insDt" ItemStyle-width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
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
