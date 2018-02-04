<%@ Page Language="C#" MasterPageFile="~/webadmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ManageReportRecommendations.aspx.cs" Inherits="SGA.webadmin.ManageReportRecommendations" %>
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
                                    Manage Report Recommendation
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="tabs">
                                        <ul >
                                            <li><a href="#tabs-1">Procurement Recomm</a></li>
                                            <li><a href="#tabs-3">CMA Recomm</a></li>
                                            <li><a href="#tabs-4">CAA Recomm</a></li>
                                             <li><a href="#tabs-5">Officer & Support</a></li>
                                            <li><a href="#tabs-6">Contract Manager</a></li>
                                            <li><a href="#tabs-7">Advisor & Analyst</a></li>
                                            <li><a href="#tabs-8">Specialist</a></li>
                                            <li><a href="#tabs-9">Manager & Director</a></li>
                                        </ul>
                                        
                                        <div id="tabs-1">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        MANAGE Procurement Assessment Recommendations
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlSSAList" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdSSASuggestions" runat="server" AllowPaging="false" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" OnItemCommand="grdSSASuggestions_ItemCommand"
                                                Width="100%" GridLines="None" PageSize="10">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:BoundColumn DataField="level" HeaderText="Level" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                                                                   
                                                      <asp:BoundColumn DataField="openingStatement" HeaderText="Opening Statement" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderStyle-Width="60%" >
                                                    </asp:BoundColumn>                                         
                                                  
                                                 
                                                    <asp:TemplateColumn HeaderText="Edit" ItemStyle-width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Id") %>' CommandName="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <asp:Panel ID="pnlSSAEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Level
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSSALevel" CssClass="txtBig" runat="server" MaxLength="250" ValidationGroup="ssa"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>                                                           
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Recommendations
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSARecommendation" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblSSATopic1" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSATopicText1" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdSSAId1" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblSSATopic2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSATopicText2" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdSSAId2" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblSSATopic3" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSATopicText3" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdSSAId3" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblSSATopic4" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSATopicText4" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdSSAId4" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblSSATopic5" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSATopicText5" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdSSAId5" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblSSATopic6" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSATopicText6" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdSSAId6" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblSSATopic7" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSATopicText7" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdSSAId7" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblSSATopic8" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSATopicText8" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdSSAId8" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>

                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td >
                                                                    <asp:ImageButton ID="imgSSAEdit" runat="server" OnClick="imgSSAEdit_Click" ImageUrl="~/webadmin/images/save.png"  />
                                                                    <asp:ImageButton ID="imgSSACancel" runat="server" OnClick="imgSSACancel_Click" ImageUrl="~/webadmin/images/close.png"  CausesValidation="false" />
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>  
                                        <div id="tabs-3">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        MANAGE Contract Management Recommendations
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlCMAList" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdCMASuggestions" runat="server" AllowPaging="false" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" OnItemCommand="grdCMASuggestions_ItemCommand"
                                                Width="100%" GridLines="None" PageSize="10">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                   <asp:BoundColumn DataField="level" HeaderText="Level" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                                                                    
                                                      <asp:BoundColumn DataField="openingStatement" HeaderText="Opening Statement" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderStyle-Width="60%" >
                                                    </asp:BoundColumn>                                         
                                                  
                                                  
                                                    <asp:TemplateColumn HeaderText="Edit" ItemStyle-width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Id") %>' CommandName="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <asp:Panel ID="pnlCMAEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Level
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCMALevel" CssClass="txtBig" runat="server" MaxLength="250" ValidationGroup="cma"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                          
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Recommendations
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMARecommendation" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblCMATopic1" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMATopicText1" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMAId1" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblCMATopic2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMATopicText2" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMAId2" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblCMATopic3" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMATopicText3" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMAId3" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblCMATopic4" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMATopicText4" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMAId4" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblCMATopic5" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMATopicText5" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMAId5" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblCMATopic6" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMATopicText6" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMAId6" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblCMATopic7" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMATopicText7" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMAId7" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblCMATopic8" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMATopicText8" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMAId8" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>

                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td >
                                                                    <asp:ImageButton ID="imgCMAEdit" runat="server" OnClick="imgCMAEdit_Click" ValidationGroup="cma" ImageUrl="~/webadmin/images/save.png"  />
                                                                    <asp:ImageButton ID="imgCMACancel" runat="server" OnClick="imgCMACancel_Click" ImageUrl="~/webadmin/images/close.png"  CausesValidation="false" />
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>
                                        <div id="tabs-4">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        MANAGE Commercial Awareness Recommendations
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlNPList" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdNPSuggestions" runat="server" AllowPaging="false" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" OnItemCommand="grdNPSuggestions_ItemCommand" 
                                                Width="100%" GridLines="None" PageSize="10">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                   
                                                    <asp:BoundColumn DataField="level" HeaderText="Level" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                                                                 
                                                      <asp:BoundColumn DataField="openingStatement" HeaderText="Opening Statement" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderStyle-Width="60%" >
                                                    </asp:BoundColumn>                                         
                                                  
                                               
                                                    <asp:TemplateColumn HeaderText="Edit" ItemStyle-width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Id") %>' CommandName="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <asp:Panel ID="pnlNPEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                             <tr>
                                                                <td class="txtrht">
                                                                    Level
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNPLevel" CssClass="txtBig" runat="server" MaxLength="250" ValidationGroup="ba"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                         
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Recommendations
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtNPRecommendation" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblNPTopic1" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtNPTopicText1" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdNPId1" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblNPTopic2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtNPTopicText2" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdNPId2" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblNPTopic3" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtNPTopicText3" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdNPId3" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblNPTopic4" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtNPTopicText4" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdNPId4" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblNPTopic5" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtNPTopicText5" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdNPId5" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                          
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="2">
                                                                    <asp:ImageButton ID="imgNPEdit" runat="server" OnClick="imgNPEdit_Click" ValidationGroup="ba" ImageUrl="~/webadmin/images/save.png"  />
                                                                    <asp:ImageButton ID="imgNPCancel" runat="server" OnClick="imgNPCancel_Click" ImageUrl="~/webadmin/images/close.png"  CausesValidation="false" />
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>
                                        <div id="tabs-5">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        MANAGE Procurement Suggestions
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlProcSuggList" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdProcSugg" runat="server" AllowPaging="false" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" OnItemCommand="grrdProcSugg_ItemCommand" 
                                                Width="100%" GridLines="None" PageSize="10">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                   
                                                    <asp:BoundColumn DataField="level" HeaderText="Level" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                                                                 
                                                      <asp:BoundColumn DataField="openingStatement" HeaderText="Opening Statement" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderStyle-Width="60%" >
                                                    </asp:BoundColumn>                                         
                                                  
                                               
                                                    <asp:TemplateColumn HeaderText="Edit" ItemStyle-width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Id") %>' CommandName="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <asp:Panel ID="pnlProcSuggEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                             <tr>
                                                                <td class="txtrht">
                                                                    Level
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtProcSugglevel" CssClass="txtBig" runat="server" MaxLength="250" ValidationGroup="ba"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                         
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Recommendations
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtProcSuggRecommendation" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblProcSuggTopic1" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtProcSuggTxt1" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdProcSuggId1" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblProcSuggTopic2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtProcSuggTxt2" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdProcSuggId2" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblProcSuggTopic3" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtProcSuggTxt3" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdProcSuggId3" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblProcSuggTopic4" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtProcSuggTxt4" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdProcSuggId4" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblProcSuggTopic5" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtProcSuggTxt5" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdProcSuggId5" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                              <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblProcSuggTopic6" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtProcSuggTxt6" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdProcSuggId6" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblProcSuggTopic7" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtProcSuggTxt7" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdProcSuggId7" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblProcSuggTopic8" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtProcSuggTxt8" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdProcSuggId8" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                          
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="2">
                                                                    <asp:ImageButton ID="ImgProcSgEdit" runat="server" OnClick="ImgProcSgEdit_Click" ValidationGroup="ba" ImageUrl="~/webadmin/images/save.png"  />
                                                                    <asp:ImageButton ID="ImgProcSgCancel" runat="server" OnClick="ImgProcSgCancel_Click" ImageUrl="~/webadmin/images/close.png"  CausesValidation="false" />
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>
                                        <div id="tabs-6">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        MANAGE Contact Management Suggestions
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlCMSuggList" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdCMSugg" runat="server" AllowPaging="false" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" OnItemCommand="grdCMSugg_ItemCommand" 
                                                Width="100%" GridLines="None" PageSize="10">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                   
                                                    <asp:BoundColumn DataField="level" HeaderText="Level" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                                                                 
                                                      <asp:BoundColumn DataField="openingStatement" HeaderText="Opening Statement" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderStyle-Width="60%" >
                                                    </asp:BoundColumn>                                         
                                                  
                                               
                                                    <asp:TemplateColumn HeaderText="Edit" ItemStyle-width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Id") %>' CommandName="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <asp:Panel ID="pnlCMSuggEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                             <tr>
                                                                <td class="txtrht">
                                                                    Level
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCMSuggLevel" CssClass="txtBig" runat="server" MaxLength="250" ValidationGroup="ba"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                         
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Recommendations
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMSuggRecommendation" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblCMSuggTopic1" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMSuggTxt1" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMSuggId1" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblCMSuggTopic2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMSuggTxt2" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMSuggId2" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblCMSuggTopic3" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMSuggTxt3" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMSuggId3" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblCMSuggTopic4" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMSuggTxt4" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMSuggId4" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblCMSuggTopic5" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMSuggTxt5" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMSuggId5" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                               <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblCMSuggTopic6" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMSuggTxt6" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMSuggId6" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblCMSuggTopic7" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMSuggTxt7" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMSuggId7" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblCMSuggTopic8" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtCMSuggTxt8" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMSuggId8" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                          
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="2">
                                                                    <asp:ImageButton ID="imgCMSgEdit" runat="server" OnClick="imgCMSgEdit_Click" ValidationGroup="ba" ImageUrl="~/webadmin/images/save.png"  />
                                                                    <asp:ImageButton ID="imgCMSgCancel" runat="server" OnClick="imgCMSgCancel_Click" ImageUrl="~/webadmin/images/close.png"  CausesValidation="false" />
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>
                                        <div id="tabs-7">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        MANAGE Advisor & Analysts Suggestions
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlAASuggList" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdAAsugg" runat="server" AllowPaging="false" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" OnItemCommand="grdAAsugg_ItemCommand" 
                                                Width="100%" GridLines="None" PageSize="10">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                   
                                                    <asp:BoundColumn DataField="level" HeaderText="Level" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                                                                 
                                                      <asp:BoundColumn DataField="openingStatement" HeaderText="Opening Statement" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderStyle-Width="60%" >
                                                    </asp:BoundColumn>                                         
                                                  
                                               
                                                    <asp:TemplateColumn HeaderText="Edit" ItemStyle-width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Id") %>' CommandName="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <asp:Panel ID="pnlAASuggEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                             <tr>
                                                                <td class="txtrht">
                                                                    Level
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAAsuggLevel" CssClass="txtBig" runat="server" MaxLength="250" ValidationGroup="ba"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                         
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Recommendations
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtAAsuggRecommendation" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblAASuggTopic1" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtAASuggTxt1" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidAASuggId1" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblAASuggTopic2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtAASuggTxt2" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidAASuggId2" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblAASuggTopic3" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtAASuggTxt3" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidAASuggId3" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblAASuggTopic4" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtAASuggTxt4" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidAASuggId4" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblAASuggTopic5" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtAASuggTxt5" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidAASuggId5" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                              <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblAASuggTopic6" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtAASuggTxt6" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidAASuggId6" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblAASuggTopic7" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtAASuggTxt7" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidAASuggId7" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblAASuggTopic8" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtAASuggTxt8" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidAASuggId8" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                          
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="2">
                                                                    <asp:ImageButton ID="ImgProcAAEdit" runat="server" OnClick="ImgProcAAEdit_Click" ValidationGroup="ba" ImageUrl="~/webadmin/images/save.png"  />
                                                                    <asp:ImageButton ID="ImgProcAACancel" runat="server" OnClick="ImgProcAACancel_Click" ImageUrl="~/webadmin/images/close.png"  CausesValidation="false" />
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>

                                        <div id="tabs-8">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        MANAGE Specialist Suggestions
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlSSSuggList" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdSSsugg" runat="server" AllowPaging="false" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" OnItemCommand="grdSSsugg_ItemCommand" 
                                                Width="100%" GridLines="None" PageSize="10">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                   
                                                    <asp:BoundColumn DataField="level" HeaderText="Level" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                                                                 
                                                      <asp:BoundColumn DataField="openingStatement" HeaderText="Opening Statement" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderStyle-Width="60%" >
                                                    </asp:BoundColumn>                                         
                                                  
                                               
                                                    <asp:TemplateColumn HeaderText="Edit" ItemStyle-width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Id") %>' CommandName="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <asp:Panel ID="pnlSSSuggEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                             <tr>
                                                                <td class="txtrht">
                                                                    Level
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSSsuggLevel" CssClass="txtBig" runat="server" MaxLength="250" ValidationGroup="ba"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                         
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Recommendations
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSsuggRecommendation" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblSSSuggTopic1" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSSuggTxt1" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidSSSuggId1" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblSSSuggTopic2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSSuggTxt2" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidSSSuggId2" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblSSSuggTopic3" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSSuggTxt3" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidSSSuggId3" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblSSSuggTopic4" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSSuggTxt4" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidSSSuggId4" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblSSSuggTopic5" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSSuggTxt5" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidSSSuggId5" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                              <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblSSSuggTopic6" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSSuggTxt6" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidSSSuggId6" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblSSSuggTopic7" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSSuggTxt7" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidSSSuggId7" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblSSSuggTopic8" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtSSSuggTxt8" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidSSSuggId8" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                          
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="2">
                                                                    <asp:ImageButton ID="ImgProcSSEdit" runat="server" OnClick="ImgProcSSEdit_Click" ValidationGroup="ba" ImageUrl="~/webadmin/images/save.png"  />
                                                                    <asp:ImageButton ID="ImgProcSSCancel" runat="server" OnClick="ImgProcSSCancel_Click" ImageUrl="~/webadmin/images/close.png"  CausesValidation="false" />
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                            </table>
                                        </div>

                                        <div id="tabs-9">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        MANAGE Manager & Director Suggestions
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlMDSuggList" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdMDsugg" runat="server" AllowPaging="false" AllowSorting="false"
                                                AutoGenerateColumns="False" CMDClass="grdMain" OnItemCommand="grdMDsugg_ItemCommand" 
                                                Width="100%" GridLines="None" PageSize="10">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                   
                                                    <asp:BoundColumn DataField="level" HeaderText="Level" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                                                                 
                                                      <asp:BoundColumn DataField="openingStatement" HeaderText="Opening Statement" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderStyle-Width="60%" >
                                                    </asp:BoundColumn>                                         
                                                  
                                               
                                                    <asp:TemplateColumn HeaderText="Edit" ItemStyle-width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Id") %>' CommandName="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <asp:Panel ID="pnlMDSuggEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                             <tr>
                                                                <td class="txtrht">
                                                                    Level
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtMDsuggLevel" CssClass="txtBig" runat="server" MaxLength="250" ValidationGroup="ba"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                         
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Recommendations
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtMDsuggRecommendation" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblMDSuggTopic1" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtMDSuggTxt1" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidMDSuggId1" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblMDSuggTopic2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtMDSuggTxt2" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidMDSuggId2" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblMDSuggTopic3" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtMDSuggTxt3" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidMDSuggId3" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblMDSuggTopic4" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtMDSuggTxt4" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidMDSuggId4" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblMDSuggTopic5" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtMDSuggTxt5" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidMDSuggId5" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                              <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblMDSuggTopic6" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtMDSuggTxt6" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidMDSuggId6" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblMDSuggTopic7" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtMDSuggTxt7" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidMDSuggId7" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblMDSuggTopic8" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <FCKeditorV2:FCKeditor ID="txtMDSuggTxt8" runat="server" Height="400px" Width="603px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hidMDSuggId8" runat="server" />
                                                                </td>
                                                                
                                                            </tr>
                                                          
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="2">
                                                                    <asp:ImageButton ID="ImgProcMDEdit" runat="server" OnClick="ImgProcMDEdit_Click" ValidationGroup="ba" ImageUrl="~/webadmin/images/save.png"  />
                                                                    <asp:ImageButton ID="ImgProcMDCancel" runat="server" OnClick="ImgProcMDCancel_Click" ImageUrl="~/webadmin/images/close.png"  CausesValidation="false" />
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
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

