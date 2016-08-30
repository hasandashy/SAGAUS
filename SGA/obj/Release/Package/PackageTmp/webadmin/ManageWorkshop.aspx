<%@ Page Language="C#" MasterPageFile="~/webadmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ManageWorkshop.aspx.cs" Inherits="SGA.webadmin.ManageWorkshop" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register TagName="left" TagPrefix="sga" Src="~/controls/ctrlLeftMenu.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!-- SimpleTabs -->
    <link rel="stylesheet" href="css/jquery-ui.css">
<script  src="//code.jquery.com/jquery-1.9.1.js"></script>
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
                                    Manage Report Workshop Levels
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="tabs">
                                        <ul >
                                            <li><a href="#tabs-1">Procurement Management Workshop</a></li>
                                            <li><a href="#tabs-2">Contract Management Workshop</a></li>
                                        </ul>
                                        
                                        <div id="tabs-1">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        MANAGE Procurement Assessment Management Workshop
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
                                                    <asp:BoundColumn DataField="ratingScale" HeaderText="Rating scale" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="score" HeaderText="Score" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%" HeaderStyle-Width="8%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="test" HeaderText="Test type" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="14%" HeaderStyle-Width="14%" >
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
                                                                    <asp:TextBox ID="txtSSALevel" CssClass="txtBig" runat="server" MaxLength="250" style="width:698px;" ValidationGroup="ssa"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Rating Scale
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSSAScale" CssClass="txtBig" runat="server" MaxLength="250" style="width:698px;" ValidationGroup="ssa"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Score
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSSAScore" CssClass="txtBig" runat="server" MaxLength="4" style="width:698px;" ValidationGroup="ssa"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblSSATopic1" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    Heading : <asp:TextBox ID="txtSSATopicHeading1" style="width:632px;" runat="server"></asp:TextBox>
                                                                    <br />
                                                                    <FCKeditorV2:FCKeditor ID="txtSSATopicText1" runat="server" Height="400px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdSSAId1" runat="server" />
                                                                    <br />
                                                                    More link : <asp:TextBox ID="txtSSAMoreLink1" MaxLength="2000" style="width:632px;" runat="server"></asp:TextBox>
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
                                                                    Heading : <asp:TextBox ID="txtSSATopicHeading2" style="width:632px;" runat="server"></asp:TextBox>
                                                                    <br />
                                                                    <FCKeditorV2:FCKeditor ID="txtSSATopicText2" runat="server" Height="400px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdSSAId2" runat="server" />
                                                                    <br />
                                                                    More link : <asp:TextBox ID="txtSSAMoreLink2" MaxLength="2000" style="width:632px;" runat="server"></asp:TextBox>
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
                                                                    Heading : <asp:TextBox ID="txtSSATopicHeading3" style="width:632px;" runat="server"></asp:TextBox>
                                                                    <br />
                                                                    <FCKeditorV2:FCKeditor ID="txtSSATopicText3" runat="server" Height="400px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdSSAId3" runat="server" />
                                                                     <br />
                                                                    More link : <asp:TextBox ID="txtSSAMoreLink3" MaxLength="2000" runat="server" style="width:632px;"></asp:TextBox>
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
                                                                    Heading : <asp:TextBox ID="txtSSATopicHeading4" style="width:632px;" runat="server"></asp:TextBox>
                                                                    <br />
                                                                    <FCKeditorV2:FCKeditor ID="txtSSATopicText4" runat="server" Height="400px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdSSAId4" runat="server" />
                                                                    <br />
                                                                    More link : <asp:TextBox ID="txtSSAMoreLink4" MaxLength="2000" style="width:632px;" runat="server"></asp:TextBox>
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
                                                                    Heading : <asp:TextBox ID="txtSSATopicHeading5" style="width:632px;" runat="server"></asp:TextBox>
                                                                    <br />
                                                                    <FCKeditorV2:FCKeditor ID="txtSSATopicText5" runat="server" Height="400px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdSSAId5" runat="server" />
                                                                    <br />
                                                                    More link : <asp:TextBox ID="txtSSAMoreLink5" MaxLength="2000" runat="server" style="width:632px;"></asp:TextBox>
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
                                                                    Heading : <asp:TextBox ID="txtSSATopicHeading6" style="width:632px;" runat="server"></asp:TextBox>
                                                                    <br />
                                                                    <FCKeditorV2:FCKeditor ID="txtSSATopicText6" runat="server" Height="400px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdSSAId6" runat="server" />
                                                                    <br />
                                                                    More link : <asp:TextBox ID="txtSSAMoreLink6" MaxLength="2000" style="width:632px;" runat="server"></asp:TextBox>
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
                                                                    Heading : <asp:TextBox ID="txtSSATopicHeading7" style="width:632px;" runat="server"></asp:TextBox>
                                                                    <br />
                                                                    <FCKeditorV2:FCKeditor ID="txtSSATopicText7" runat="server" Height="400px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdSSAId7" runat="server" />
                                                                    <br />
                                                                    More link : <asp:TextBox ID="txtSSAMoreLink7" MaxLength="2000" style="width:632px;" runat="server"></asp:TextBox>
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
                                                                    Heading : <asp:TextBox ID="txtSSATopicHeading8" style="width:632px;" runat="server"></asp:TextBox>
                                                                    <br />
                                                                    <FCKeditorV2:FCKeditor ID="txtSSATopicText8" runat="server" Height="400px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdSSAId8" runat="server" />
                                                                    <br />
                                                                    More link : <asp:TextBox ID="txtSSAMoreLink8" MaxLength="2000" style="width:632px;" runat="server"></asp:TextBox>
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
                                        
                                        <div id="tabs-2">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        MANAGE Contract Management Workshop
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
                                                    <asp:BoundColumn DataField="ratingScale" HeaderText="Rating scale" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="score" HeaderText="Score" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%" HeaderStyle-Width="8%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="test" HeaderText="Test type" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="14%" HeaderStyle-Width="14%" >
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
                                                                    <asp:TextBox ID="txtCMALevel" CssClass="txtBig" runat="server" MaxLength="250" style="width:698px;" ValidationGroup="cma"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Rating Scale
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCMAScale" CssClass="txtBig" runat="server" MaxLength="250" style="width:698px;" ValidationGroup="cma"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Score
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCMAScore" CssClass="txtBig" runat="server" MaxLength="4" style="width:698px;" ValidationGroup="ssa"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="txtrht">
                                                                    <asp:Label ID="lblCMATopic1" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    Heading : <asp:TextBox ID="txtCMATopicHeading1" style="width:632px;" runat="server"></asp:TextBox>
                                                                    <br />
                                                                    <FCKeditorV2:FCKeditor ID="txtCMATopicText1" runat="server" Height="400px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMAId1" runat="server" />
                                                                    <br />
                                                                    More link : <asp:TextBox ID="txtCMAMoreLink1" MaxLength="2000" style="width:632px;" runat="server"></asp:TextBox>
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
                                                                    Heading : <asp:TextBox ID="txtCMATopicHeading2" style="width:632px;" runat="server"></asp:TextBox>
                                                                    <br />
                                                                    <FCKeditorV2:FCKeditor ID="txtCMATopicText2" runat="server" Height="400px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMAId2" runat="server" />
                                                                    <br />
                                                                    More link : <asp:TextBox ID="txtCMAMoreLink2" MaxLength="2000" style="width:632px;" runat="server"></asp:TextBox>
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
                                                                    Heading : <asp:TextBox ID="txtCMATopicHeading3" style="width:632px;" runat="server"></asp:TextBox>
                                                                    <br />
                                                                    <FCKeditorV2:FCKeditor ID="txtCMATopicText3" runat="server" Height="400px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMAId3" runat="server" />
                                                                     <br />
                                                                    More link : <asp:TextBox ID="txtCMAMoreLink3" MaxLength="2000" runat="server" style="width:632px;"></asp:TextBox>
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
                                                                    Heading : <asp:TextBox ID="txtCMATopicHeading4" style="width:632px;" runat="server"></asp:TextBox>
                                                                    <br />
                                                                    <FCKeditorV2:FCKeditor ID="txtCMATopicText4" runat="server" Height="400px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMAId4" runat="server" />
                                                                    <br />
                                                                    More link : <asp:TextBox ID="txtCMAMoreLink4" MaxLength="2000" style="width:632px;" runat="server"></asp:TextBox>
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
                                                                    Heading : <asp:TextBox ID="txtCMATopicHeading5" style="width:632px;" runat="server"></asp:TextBox>
                                                                    <br />
                                                                    <FCKeditorV2:FCKeditor ID="txtCMATopicText5" runat="server" Height="400px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMAId5" runat="server" />
                                                                    <br />
                                                                    More link : <asp:TextBox ID="txtCMAMoreLink5" MaxLength="2000" runat="server" style="width:632px;"></asp:TextBox>
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
                                                                    Heading : <asp:TextBox ID="txtCMATopicHeading6" style="width:632px;" runat="server"></asp:TextBox>
                                                                    <br />
                                                                    <FCKeditorV2:FCKeditor ID="txtCMATopicText6" runat="server" Height="400px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMAId6" runat="server" />
                                                                    <br />
                                                                    More link : <asp:TextBox ID="txtCMAMoreLink6" MaxLength="2000" style="width:632px;" runat="server"></asp:TextBox>
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
                                                                    Heading : <asp:TextBox ID="txtCMATopicHeading7" style="width:632px;" runat="server"></asp:TextBox>
                                                                    <br />
                                                                    <FCKeditorV2:FCKeditor ID="txtCMATopicText7" runat="server" Height="400px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMAId7" runat="server" />
                                                                    <br />
                                                                    More link : <asp:TextBox ID="txtCMAMoreLink7" MaxLength="2000" style="width:632px;" runat="server"></asp:TextBox>
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
                                                                    Heading : <asp:TextBox ID="txtCMATopicHeading8" style="width:632px;" runat="server"></asp:TextBox>
                                                                    <br />
                                                                    <FCKeditorV2:FCKeditor ID="txtCMATopicText8" runat="server" Height="400px" Width="703px"></FCKeditorV2:FCKeditor>
                                                                    <asp:HiddenField ID="hdCMAId8" runat="server" />
                                                                    <br />
                                                                    More link : <asp:TextBox ID="txtCMAMoreLink8" MaxLength="2000" style="width:632px;" runat="server"></asp:TextBox>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" >&nbsp;</td>
                                                            </tr>

                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td >
                                                                    <asp:ImageButton ID="imgCMAEdit" runat="server" OnClick="imgCMAEdit_Click" ImageUrl="~/webadmin/images/save.png"  />
                                                                    <asp:ImageButton ID="imgCMACancel" runat="server" OnClick="imgCMACancel_Click" ImageUrl="~/webadmin/images/close.png"  CausesValidation="false" />
                                                                    
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
