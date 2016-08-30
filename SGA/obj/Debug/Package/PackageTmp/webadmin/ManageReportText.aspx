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
                                            <li><a href="#tabs-2">Leadership Report text</a></li>
                                            <li><a href="#tabs-3">CMA Report text</a></li>
                                            <li><a href="#tabs-4">NP Report text</a></li>
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
                                                                            <td class="txtrht">Page 3 heading</td>
                                                                            <td><asp:TextBox ID="txtSSAheading" runat="server" MaxLength="200" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtSSAheading" ErrorMessage="Enter Page 3 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 3 text</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtSSA3Text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="txtrht">Page 5 text</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtSSA5Text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                        </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 14 heading</td>
                                                                            <td><asp:TextBox ID="txtSSA14Heading" runat="server" MaxLength="200" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSSA14Heading" ErrorMessage="Enter Page 14 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 14 text</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtSSA14Text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                          <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 16 heading</td>
                                                                            <td><asp:TextBox ID="txtSSA16Heading" runat="server" MaxLength="200" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSSA16Heading" ErrorMessage="Enter Page 16 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 16 text</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtSSA16text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 17 heading</td>
                                                                            <td><asp:TextBox ID="txtSSA17Heading" runat="server" MaxLength="200" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSSA17Heading" ErrorMessage="Enter Page 17 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 17 text</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtSSA17text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="txtrht">Page 18 heading</td>
                                                                            <td><asp:TextBox ID="txtSSA18Heading" runat="server" MaxLength="200" ValidationGroup="cmc" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="cmc" ControlToValidate="txtSSA18Heading" ErrorMessage="Enter Page 18 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 18 sub paragraph 1</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtSSA18SubPara1" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 18 sub paragraph 2</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtSSA18SubPara2" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="txtrht">Page 20 heading</td>
                                                                            <td><asp:TextBox ID="txtSSA20Heading" runat="server" MaxLength="200" ValidationGroup="cmc" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvSSA20Heading" runat="server" ValidationGroup="cmc" ControlToValidate="txtSSA20Heading" ErrorMessage="Enter Page 20 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 20 paragraph</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtSSA20text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="txtrht">Page 21 heading</td>
                                                                            <td><asp:TextBox ID="txtSSA21Heading" runat="server" MaxLength="200" ValidationGroup="cmc" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvSSA21Heading" runat="server" ValidationGroup="cmc" ControlToValidate="txtSSA21Heading" ErrorMessage="Enter Page 20 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 21 paragraph</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtSSA21text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 22 Address</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtSSA19Address" runat="server" Height="300px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Disclaimer</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtSSADisclaimer" runat="server" Height="300px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
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
                                                        MANAGE LEADERSHIP ASSESSMENT CHALLENGE REPORT TEXT
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
                                                                            <td class="txtrht">Page 3 heading</td>
                                                                            <td><asp:TextBox ID="txtBA3heading" runat="server" MaxLength="200" ValidationGroup="ba" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvBA3Heading" runat="server" ValidationGroup="ba" ControlToValidate="txtBA3heading" ErrorMessage="Enter Page 3 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 3 text</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtBA3text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="txtrht">Page 5 text</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtBA5text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                        </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 14 heading</td>
                                                                            <td><asp:TextBox ID="txtBA14Heading" runat="server" MaxLength="200" ValidationGroup="ba" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvBA14Heading" runat="server" ValidationGroup="ba" ControlToValidate="txtBA14Heading" ErrorMessage="Enter Page 14 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 14 text</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtBA14text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                          <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 16 heading</td>
                                                                            <td><asp:TextBox ID="txtBA16Heading" runat="server" MaxLength="200" ValidationGroup="ba" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvBA16Heading" runat="server" ValidationGroup="ba" ControlToValidate="txtBA16Heading" ErrorMessage="Enter Page 16 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 16 text</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtBA16text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 17 heading</td>
                                                                            <td><asp:TextBox ID="txtBA17Heading" runat="server" MaxLength="200" ValidationGroup="ba" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvBA17Heading" runat="server" ValidationGroup="ba" ControlToValidate="txtBA17Heading" ErrorMessage="Enter Page 17 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 17 text</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtBA17text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="txtrht">Page 18 heading</td>
                                                                            <td><asp:TextBox ID="txtBA18Heading" runat="server" MaxLength="200" ValidationGroup="ba" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvBA18Heading" runat="server" ValidationGroup="ba" ControlToValidate="txtBA18Heading" ErrorMessage="Enter Page 18 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 18 sub paragraph 1</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtBA18subPara1" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 18 sub paragraph 2</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtBA18subPara2" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="txtrht">Page 20 heading</td>
                                                                            <td><asp:TextBox ID="txtBA20Heading" runat="server" MaxLength="200" ValidationGroup="ba" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvBA20Heading" runat="server" ValidationGroup="ba" ControlToValidate="txtBA20Heading" ErrorMessage="Enter Page 20 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 20 paragraph</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtBA20text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="txtrht">Page 21 heading</td>
                                                                            <td><asp:TextBox ID="txtBA21Heading" runat="server" MaxLength="200" ValidationGroup="ba" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvBA21Heading" runat="server" ValidationGroup="ba" ControlToValidate="txtBA21Heading" ErrorMessage="Enter Page 21 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 21 paragraph</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtBA21text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 22 Address</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtBA16Address" runat="server" Height="300px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Disclaimer</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtBADisclaimer" runat="server" Height="300px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
    
                                                                        <td colspan="2" align="center">
                                                                        <asp:ImageButton ID="iBtnBASave" runat="server" CommandArgument="0" ValidationGroup="ba" ImageUrl="images/save.png" 
                                                                                AlternateText="Save" width="96" height="37" onclick="iBtnBASave_Click" /></td>
                                                                        
                                                                      </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        
                                        <div id="tabs-3">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        MANAGE CONTRACT MANAGEMENT ASSESSMENT REPORT TEXT
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
                                                                            <td class="txtrht">Page 3 heading</td>
                                                                            <td><asp:TextBox ID="txtCMAheading" runat="server" ValidationGroup="cma" MaxLength="200" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="cma" ControlToValidate="txtCMAheading" ErrorMessage="Enter Page 3 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 3 text</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtCMA3Text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="txtrht">Page 5 text</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtCMA5Text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                        </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 14 heading</td>
                                                                            <td><asp:TextBox ID="txtCMA14Heading" runat="server" MaxLength="200" ValidationGroup="cma" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCMA14Heading" ValidationGroup="cma" ErrorMessage="Enter Page 14 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 14 text</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtCMA14Text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                          <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 16 heading</td>
                                                                            <td><asp:TextBox ID="txtCMA16Heading" runat="server" ValidationGroup="cma" MaxLength="200" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="cma" ControlToValidate="txtCMA16Heading" ErrorMessage="Enter Page 16 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 16 text</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtCMA16Text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 17 heading</td>
                                                                            <td><asp:TextBox ID="txtCMA17Heading" runat="server" MaxLength="200" ValidationGroup="cma" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="cma" ControlToValidate="txtCMA17Heading" ErrorMessage="Enter Page 17 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 17 text</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtCMA17Text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="txtrht">Page 18 heading</td>
                                                                            <td><asp:TextBox ID="txtCMA18Heading" runat="server" MaxLength="200" ValidationGroup="cma" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="cma" ControlToValidate="txtCMA18Heading" ErrorMessage="Enter Page 18 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 18 sub paragraph 1</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtCMA18Para1" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 18 sub paragraph 2</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtCMA18Para2" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="txtrht">Page 20 heading</td>
                                                                            <td><asp:TextBox ID="txtCMA20Heading" runat="server" MaxLength="200" ValidationGroup="cma" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="cma" ControlToValidate="txtCMA20Heading" ErrorMessage="Enter Page 20 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 20 paragraph</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtCMA20Text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="txtrht">Page 21 heading</td>
                                                                            <td><asp:TextBox ID="txtCMA21Heading" runat="server" MaxLength="200" ValidationGroup="cma" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="cma" ControlToValidate="txtCMA20Heading" ErrorMessage="Enter Page 20 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 21 paragraph</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtCMA21Text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 22 Address</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtCMAAddress" runat="server" Height="300px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Disclaimer</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtCMADisclaimer" runat="server" Height="300px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
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
                                        <div id="tabs-4">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        MANAGE NEGOTIATION PROFILE CHALLENGE REPORT TEXT
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
                                                                            <td class="txtrht">Page 3 heading</td>
                                                                            <td><asp:TextBox ID="txtNP3heading" runat="server" MaxLength="200" ValidationGroup="np" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="np" ControlToValidate="txtNP3heading" ErrorMessage="Enter Page 3 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 3 text</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtNP3text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="txtrht">Page 5 text</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtNP5text" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 15 heading</td>
                                                                            <td><asp:TextBox ID="txtNP15heading" runat="server" MaxLength="200" ValidationGroup="np" CssClass="txtBig"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="np" ControlToValidate="txtNP15heading" ErrorMessage="Enter Page 3 heading" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 15 sub paragraph 1</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtNP15SubPara1" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 15 sub paragraph 2</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtNP15SubPara2" runat="server" Height="500px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Page 16 Address</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtNP16Address" runat="server" Height="300px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
    
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2" style="height:5px;"></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td class="txtrht">Disclaimer</td>
                                                                            <td><FCKeditorV2:FCKeditor ID="txtNPDisclaimer" runat="server" Height="300px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                            </td>
                                                                         </tr>
                                                                         <tr>
                                                                            <td colspan="2">&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
    
                                                                        <td colspan="2" align="center">
                                                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument="0" ValidationGroup="np" ImageUrl="images/save.png" 
                                                                                AlternateText="Save" width="96" height="37" onclick="iBtnNPSave_Click" />
                                                                        </td>
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


