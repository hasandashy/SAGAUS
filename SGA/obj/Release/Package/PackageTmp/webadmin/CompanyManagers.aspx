<%@ Page  Language="C#" MasterPageFile="~/webadmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="CompanyManagers.aspx.cs" Inherits="SGA.webadmin.CompanyManagers" %>
<%@ Register TagName="left" TagPrefix="sga" Src="~/controls/ctrlLeftMenu.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script src="//code.jquery.com/jquery-1.9.1.js"></script>
<script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
<link rel="Stylesheet" href="css/thickbox.css" />
<script src="js/thickbox.js"></script>
<script type="text/javascript" language="javascript" >
        jqueryInstance = $;
        var tb_pathToImage = "images/loading.gif";
        jQuery(document).ready(function () {
            tb_init('a.thickbox, area.thickbox, input.thickbox'); //pass where to apply thickbox
            imgLoader = new Image(); // preload image
            imgLoader.src = tb_pathToImage;
        });
</script>
<tr>
        <td class="inrbg">
            <table width="1280" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="227" valign="top" class="lftnav">
                        <sga:left id="menu1" runat="server"></sga:left>
                    </td>
                    <td valign="top">
                    <table width="99%" border="0" align="right" cellpadding="0"  cellspacing="0" class="panbox">
                            <tr>
                                <td valign="top" class="hd26">
                                    MANAGE COMPANY MANAGER
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td valign="top" style="min-height:1000px">
                                    <asp:Panel ID="pnlList" runat="server">
                                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="left" class="hd20">
                                                        Listing of Company Manager
                                                    </td>
                                                     <td align="right">
                                                        <asp:Button ID="btnAddnew" runat="server" Text="Add New" OnClick="btnAddnew_Click" CausesValidation="false" />
                                                    </td>
                                                </tr>
                                               
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:DataGrid ID="grdUsers" runat="server" AllowPaging="True" AllowSorting="true"
                                                AutoGenerateColumns="False" CssClass="grdMain" 
                                                OnItemDataBound="grdUsers_ItemDataBound" OnPageIndexChanged="grdUsers_PageIndexChanged" OnSortCommand="grdUsers_SortCommand"  OnItemCommand="grdUsers_ItemCommand"
                                                Width="100%" GridLines="None" PageSize="75">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="Name" SortExpression="firstname" ItemStyle-width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                               <a href="UserDetails.aspx?Id=<%#Eval("Id") %>&KeepThis=true&TB_iframe=true&height=500&width=1070" class="thickbox"><%# Eval("firstname") +" "+ Eval("lastname") %></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Email" SortExpression="email" ItemStyle-width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                               <%# Eval("email")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Company" SortExpression="company" ItemStyle-width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                               <%# Eval("company")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Registration date" SortExpression="dtInsertDate" ItemStyle-width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                               <asp:Label ID="lblRegisterDate" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Status" SortExpression="IsApproved" ItemStyle-width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                              <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Last Login" SortExpression="lastLoginDt" ItemStyle-width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblLastlogin" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    
                                                    <asp:TemplateColumn HeaderText="Action" ItemStyle-width="20%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnDelete" runat="server" CausesValidation="false" AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this user?');" CommandArgument='<%#Eval("Id") %>' CommandName="delete" ToolTip="Delete" ImageUrl="~/webadmin/images/disapprove_icon.png" />
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                    </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlAdd" Visible="false" runat="server">
                                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                                    <td class="hd20">
                                                        Add Company Manager
                                                    </td>
                                                </tr>
                                                <tr>
        <td class="grybox">
        
        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                        <tr>
                        <td class="txtrht">First Name</td>
                        <td> <input type="text" name="txtFirstname" id="txtFirstname" runat="server" maxlength="100"   />
                        <asp:RequiredFieldValidator ID="rfvFirstname" runat="server" SetFocusOnError="true" CssClass="error" ControlToValidate="txtFirstname" ValidationGroup="add" Text="*"></asp:RequiredFieldValidator>
                        </td>
                        <td class="txtrht">Last Name</td>
                        <td> <input type="text" name="txtLastname" id="txtLastname" runat="server" maxlength="100" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true" CssClass="error" ControlToValidate="txtLastname" ValidationGroup="add" Text="*"></asp:RequiredFieldValidator>
                        </td>
                        </tr>
                        <tr>
                            <td class="txtrht">Company Name</td>
                            <td>
                            <asp:DropDownList ID="ddlCompany" runat="server">
                                                                    </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" SetFocusOnError="true" CssClass="error" ControlToValidate="ddlCompany" ValidationGroup="add" Text="*"></asp:RequiredFieldValidator>
                            </td>
                            <td class="txtrht">Email Address</td>
                            <td><input type="text" name="txtEmailAddress" id="txtEmailAddress" runat="server" maxlength="250" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="true" ControlToValidate="txtEmailAddress" CssClass="error" ValidationGroup="add" Text="*"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailAddress" CssClass="error"
                                                ErrorMessage="Invalid email address." ForeColor="Red" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            </td>
                            </tr>
                       <tr>
                        <td class="txtrht">Password</td>
                        <td><input type="text" name="txtPassword" id="txtPassword" runat="server" maxlength="20" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" SetFocusOnError="true" ControlToValidate="txtPassword" CssClass="error"  ValidationGroup="add" Text="*"></asp:RequiredFieldValidator>
                        </td>
                        <td colspan="2" >&nbsp;</td>
                        </tr>
  
                        <tr>
                        <td colspan="4" align="center">
                            <asp:Label ID="lblError" runat="server" CssClass="error"></asp:Label>
                        </td>
                        </tr>
  
  <tr>
    
    <td colspan="4" align="center">
    <asp:ImageButton ID="imgSave" runat="server" ImageUrl="~/webadmin/images/save.png" OnClick="imgSave_Click" ValidationGroup="add" />
    <img src="images/close.png" width="96" height="37" alt="" /></td>
    
  </tr>
</table>
        
        </td>
      </tr>
                                    </table></asp:Panel>
                                </td>
                            </tr>
                    </table>
                    </td>
                </tr>
            </table>
        </td>
</tr>
</asp:Content>