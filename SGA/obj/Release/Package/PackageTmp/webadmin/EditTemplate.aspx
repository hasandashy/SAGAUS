<%@ Page Language="C#" MasterPageFile="~/webadmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EditTemplate.aspx.cs" Inherits="SGA.webadmin.EditTemplate" %>
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
                                    Edit Email Templates
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
                                                        *Note : The variable of @v0,@v1 etc will be replaced on runtime.Do not change those but you can palce those in different-2 locations.
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
        <td class="grybox">
        
        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
  <tr>
    <td class="txtrht">Email title</td>
    <td><asp:TextBox ID="txtTitle" runat="server" MaxLength="250" CssClass="txtBig"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="Enter title" SetFocusOnError="true" ></asp:RequiredFieldValidator></td>
    
  </tr>
  <tr>
    <td class="txtrht">Email subject</td>
    <td><asp:TextBox ID="txtSubject" runat="server" MaxLength="500" CssClass="txtBig"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSuject" runat="server" ControlToValidate="txtSubject" ErrorMessage="Enter subject" SetFocusOnError="true" ></asp:RequiredFieldValidator></td>
    
  </tr>
  <tr>
    <td class="txtrht">Email body</td>
    <td><FCKeditorV2:FCKeditor ID="txtMailBody" runat="server" Height="500px" Width="703px">
                                    </FCKeditorV2:FCKeditor></td>
    
  </tr>
  
  <tr>
    <td></td>
    <td>&nbsp;</td>
  </tr>
  
  <tr>
    
    <td colspan="2" align="center">
    <asp:ImageButton ID="iBtnSave" runat="server" ImageUrl="images/save.png" 
            AlternateText="Save" width="96" height="37" onclick="iBtnSave_Click" />
    <a href="ManageEmailTemplates.aspx"><img src="images/close.png" width="96" height="37" alt="" /></a></td>
    
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
            </table>
        </td>
    </tr>
</asp:Content>

