<%@ Page Language="C#"  AutoEventWireup="true" MasterPageFile="~/webadmin/AdminMaster.Master" CodeBehind="ManageReportText.aspx.cs" Inherits="SGA.webadmin.ManageReportText" %>
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
                                    Manage Report Text
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    
                                    <div id="tabs">
                                        <ul >
                                            <li><a href="#tabs-1">Procurement Report text</a></li>
                                            <li><a href="#tabs-2">CAA text</a></li>
                                        </ul>
                                        
                                        <div id="tabs-1">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        MANAGE PROCUREMENT ASSESSMENT REPORT TEXT
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
                                                                            <td class="txtrht">Page 2 heading</td>
                                                                            <td><asp:TextBox ID="txtSSAheading" runat="server" MaxLength="200" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtSSAheading" ErrorMessage="Enter Page 3 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 2 Para 1</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtSSA3Text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>                                                                      
                                                                        <tr>
                                                                             <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 2 Para 2</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtSSA4Text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>                                                                      
                                                                        <tr>
    
                                                                        <td colspan="2" align="center">
                                                                        <asp:ImageButton ID="iBtnSaveSSA" runat="server" CommandArgument="0" ValidationGroup="cmc" ImageUrl="images/save.png" 
                                                                                AlternateText="Save" width="96" height="37" onclick="iBtnSaveSSA_Click" /></td>
                                                                        
                                                                      </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        
                                        
                                        <div id="tabs-2">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        MANAGE COMMERCIAL AWARENESS ASSESSMENT TEXT
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
                                                                            <td class="txtrht">Page heading</td>
                                                                            <td><asp:TextBox ID="txtCMAheading" runat="server" ValidationGroup="cma" MaxLength="200" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="cma" ControlToValidate="txtCMAheading" ErrorMessage="Enter Page 3 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page text</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtCMA3Text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>                                                                      
                                                                    
                                                                       <tr>
    
                                                                        <td colspan="2" align="center">
                                                                        <asp:ImageButton ID="iBtnSaveCMA" runat="server" CommandArgument="0" ValidationGroup="cma" ImageUrl="images/save.png" 
                                                                                AlternateText="Save" width="96" height="37" onclick="iBtnSaveCMA_Click" /></td>
                                                                        
                                                                      </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
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


