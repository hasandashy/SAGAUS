<%@ Page Language="C#" MasterPageFile="~/webadmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ManageEmailTemplates.aspx.cs" Inherits="SGA.webadmin.ManageEmailTemplates" %>
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
                                    Manage Email Templates
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
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td class="trrd">
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td width="240">
                                                                                Email template <br />title
                                                                            </td>
                                                                            <td width="380">
                                                                                Subject
                                                                            </td>
                                                                            <td width="200">
                                                                                Last updated
                                                                            </td>
                                                                            <td width="100" align="center">
                                                                                Edit Details
                                                                            </td>
                                                                            
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="3">
                                                                </td>
                                                            </tr>
                                                            <asp:Repeater ID="rptUsers" runat="server" OnItemDataBound="rptUsers_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr >
                                                                        <td class="grybox txt13">
                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                 <tr>
                                                                                        <td width="240">
                                                                                            <%#Eval("Title") %>
                                                                                        </td>
                                                                                        <td width="380">
                                                                                            <%#Eval("subject")%>
                                                                                        </td>
                                                                                        <td width="200">
                                                                                            <asp:Label ID="lblDate" runat="server"></asp:Label>
                                                                                        </td>
                                                                                        <td width="100" align="center">
                                                                                             
                                                                                             <a href="EditTemplate.aspx?Id=<%#Eval("Id") %>">
                                                                                                <img title="Edit" src="/webadmin/images/edit.png" alt="Edit" />
                                                                                            </a>
                                                                                        </td>
                                                                            
                                                                                    </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="3"></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            
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
            </table>
        </td>
    </tr>
</asp:Content>
