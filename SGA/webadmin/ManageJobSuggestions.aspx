<%@ Page Language="C#" MasterPageFile="~/webadmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ManageJobSuggestions.aspx.cs" Inherits="SGA.webadmin.ManageJobSuggestions" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
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
                                    Manage JobRole Suggestions
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
                                                        <asp:Panel ID="pnlList" runat="server">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td class="trrd">
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td width="5%">
                                                                                Id
                                                                            </td>
                                                                            <td width="20%">
                                                                                Job Role
                                                                            </td>
                                                                            <td width="70%">
                                                                                Page 14 Paragragh 1
                                                                            </td>
                                                                            <td width="5%" align="center">
                                                                                Edit Suggestion
                                                                            </td>
                                                                            
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="3">
                                                                </td>
                                                            </tr>
                                                            <asp:Repeater ID="rptUsers" runat="server" 
                                                                onitemcommand="rptUsers_ItemCommand" >
                                                                <ItemTemplate>
                                                                    <tr >
                                                                        <td class="grybox txt13">
                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                 <tr>
                                                                                        <td width="5%">
                                                                                            <%#Eval("Id") %>
                                                                                        </td>
                                                                                        <td width="20%">
                                                                                            <%#Eval("jobRole")%>
                                                                                        </td>
                                                                                        <td width="70%">
                                                                                            <%#Eval("page14Para1")%>
                                                                                        </td>
                                                                                        <td width="5%" align="center">
                                                                                             <asp:ImageButton ID="iBtnEdit" runat="server" CommandArgument='<%#Eval("Id") %>' CommandName="edit" ImageUrl="~/webadmin/images/edit.png" />
                                                                                            
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
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlSSAEdit" runat="server" Visible="false">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Job Role
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblJobRole" runat="server" style="font-weight:bold"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    JobRole Suggestion
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtJobRoleSuggestion" runat="server" Height="600px" Width="803px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                     Page 14 Paragragh 1
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtPage14Para1" runat="server" Height="600px" Width="803px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                     Page 14 Paragragh 2
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtPage14Para2" runat="server" Height="600px" Width="803px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
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
                                                </table>
                                                </asp:Panel>
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