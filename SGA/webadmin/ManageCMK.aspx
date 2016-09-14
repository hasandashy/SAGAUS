<%@ Page Title="" Language="C#" MasterPageFile="~/webadmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ManageCMK.aspx.cs" Inherits="SGA.webadmin.ManageCMK" %>
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
                        <sga:left ID="menu1" runat="server"></sga:left>
                    </td>
                    <td valign="top">
                        <table width="99%" border="0" align="right" cellpadding="0" cellspacing="0" class="panbox">
                            <tr>
                                <td class="hd26">
                                    Contract Management Knowledge Assessment
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="tabs">
                                        <ul>
                                            <li><a href="#tabs-1">Manage Topics</a></li>
                                            <li><a href="#tabs-2">Manage Questions</a></li>
                                            <li><a href="#tabs-3">Manage Options</a></li>
                                        </ul>
                                        <div id="tabs-1">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        MANAGE TOPICS
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlTopics" runat="server" Visible="true">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="dtgList" runat="server" AllowPaging="false" AllowSorting="false"
                                                            AutoGenerateColumns="False" CssClass="grdMain" Width="100%" GridLines="None" OnItemCommand="dtgList_ItemCommand"
                                                            PageSize="20">
                                                            <HeaderStyle CssClass="gridHeader" />
                                                            <PagerStyle Mode="NumericPages" CssClass="pager" HorizontalAlign="Center" />
                                                            <ItemStyle CssClass="gridItem" />
                                                            <Columns>
                                                                <asp:BoundColumn DataField="topicId" HeaderText="Topic id" ItemStyle-HorizontalAlign="Center"
                                                                    ItemStyle-Width="3%" HeaderStyle-Width="8%"></asp:BoundColumn>
                                                                <asp:TemplateColumn HeaderText="Topic title" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%"
                                                                    HeaderStyle-Width="12%">
                                                                    <ItemTemplate>
                                                                        <%#Eval("topicTitle").ToString().Replace("<br />"," ") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:BoundColumn DataField="insDt" HeaderText="Insert date" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="12%" HeaderStyle-Width="12%"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="totalQuestion" HeaderText="Total questions" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="5%" HeaderStyle-Width="5%"></asp:BoundColumn>
                                                                <asp:TemplateColumn HeaderText="Description" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%"
                                                                    HeaderStyle-Width="50%">
                                                                    <ItemTemplate>
                                                                        <%#Eval("topicDescription").ToString()%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Select" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%"
                                                                    HeaderStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <input value='<%#Eval("topicId") %>' id="chkSelect" type="checkbox" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Action" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%"
                                                                    HeaderStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                       <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" AlternateText="Edit" CommandArgument='<%#Eval("topicId") %>' CommandName="Edit" ToolTip="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                                       
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:HiddenField ID="hdSelectIds" runat="server" />
                                                        <asp:ImageButton ID="iBtnSelect" runat="server" OnClick="iBtnSelect_Click" CausesValidation="false" ImageUrl="~/webadmin/images/select.png" />
                                                    </td>
                                                </tr>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlTopicsEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Topic Title
                                                                </td>
                                                                <td>
                                                                    <input type="text" name="txttopicTitle" id="txttopicTitle" runat="server" maxlength="250" />
                                                                    <asp:RequiredFieldValidator ID="rfvFirstname" runat="server" SetFocusOnError="true"
                                                                        CssClass="error" ControlToValidate="txttopicTitle" ValidationGroup="add" Text="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="txtrht">
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Topic Description
                                                                </td>
                                                                <td colspan="3">
                                                                    <textarea id="txtDescription" runat="server" rows="6" cols="80">
                                                                    </textarea>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true"
                                                                        CssClass="error" ControlToValidate="txtDescription" ValidationGroup="add" Text="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="3">
                                                                    <asp:ImageButton ID="imgSave" runat="server" ImageUrl="~/webadmin/images/save.png" OnClick="imgSave_Click" ValidationGroup="add" />
                                                                    <asp:ImageButton ID="imgCancel" runat="server" ImageUrl="~/webadmin/images/close.png" OnClick="imgCancel_Click" CausesValidation="false" />
                                                                    
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
                                                        MANAGE QUESTIONS
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlQuestions" runat="server" >
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdQuestions" runat="server" AllowPaging="false" AllowSorting="false"
                                                            AutoGenerateColumns="False" CssClass="grdMain" Width="100%" GridLines="None" OnItemCommand="grdQuestions_ItemCommand"
                                                            PageSize="20">
                                                            <HeaderStyle CssClass="gridHeader" />
                                                            <PagerStyle Mode="NumericPages" CssClass="pager" HorizontalAlign="Center" />
                                                            <ItemStyle CssClass="gridItem" />
                                                            <Columns>
                                                                <asp:BoundColumn DataField="questionId" HeaderText="Question id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                    ItemStyle-Width="3%" HeaderStyle-Width="8%"></asp:BoundColumn>
                                                                <asp:TemplateColumn HeaderText="Topic title" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%"
                                                                    HeaderStyle-Width="12%">
                                                                    <ItemTemplate>
                                                                        <%#Eval("topicTitle").ToString().Replace("<br />"," ") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Question text" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="55%"
                                                                    HeaderStyle-Width="55%">
                                                                    <ItemTemplate>
                                                                        <%#Eval("questionText").ToString()%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:BoundColumn DataField="hintWord" HeaderText="Hint Word" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="12%" HeaderStyle-Width="12%"></asp:BoundColumn>
                                                                
                                                                
                                                                <asp:TemplateColumn HeaderText="Action" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%"
                                                                    HeaderStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" AlternateText="Edit" CommandArgument='<%#Eval("questionId") %>' CommandName="Edit" ToolTip="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlQuestionsEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Question text
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="txtQuestion" runat="server" ValidationGroup="addQuestion" CausesValidation="false" Rows="10" Columns="80" TextMode="MultiLine"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                                                                        CssClass="error" ControlToValidate="txtQuestion" ValidationGroup="addQuestion" Text="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Hint Word
                                                                </td>
                                                                <td colspan="3">
                                                                    <input type="text" name="txtHintWord" id="txtHintWord" runat="server"  maxlength="100" />
                                                                    
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Hint Text
                                                                </td>
                                                                <td colspan="3">
                                                                    <textarea id="txtHintText" runat="server" rows="6" cols="80">
                                                                    </textarea>
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="3">
                                                                    <asp:ImageButton ID="imgUpdateQuestion" runat="server" ImageUrl="~/webadmin/images/save.png" OnClick="imgUpdateQuestion_Click" ValidationGroup="addQuestion" />
                                                                    <asp:ImageButton ID="imgCancelQuestion" runat="server" ImageUrl="~/webadmin/images/close.png" OnClick="imgCancelQuestion_Click" CausesValidation="false" />
                                                                    
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
                                                        MANAGE OPTIONS
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="pnlOptions" runat="server" >
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="grdOptions" runat="server" AllowPaging="false" AllowSorting="false"
                                                            AutoGenerateColumns="False" CssClass="grdMain" Width="100%" GridLines="None" OnItemCommand="grdOptions_ItemCommand"
                                                            PageSize="20">
                                                            <HeaderStyle CssClass="gridHeader" />
                                                            <PagerStyle Mode="NumericPages" CssClass="pager" HorizontalAlign="Center" />
                                                            <ItemStyle CssClass="gridItem" />
                                                            <Columns>
                                                                <asp:BoundColumn DataField="optionId" HeaderText="Option id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                    ItemStyle-Width="3%" HeaderStyle-Width="8%"></asp:BoundColumn>
                                                                <asp:TemplateColumn HeaderText="Option text" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%"
                                                                    HeaderStyle-Width="12%">
                                                                    <ItemTemplate>
                                                                        <%#Eval("optionText").ToString()%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Option level" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="55%"
                                                                    HeaderStyle-Width="55%">
                                                                    <ItemTemplate>
                                                                        <%#Eval("optionLevel").ToString()%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:BoundColumn DataField="optionValue" HeaderText="Option value" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="12%" HeaderStyle-Width="12%"></asp:BoundColumn>
                                                                
                                                                
                                                                <asp:TemplateColumn HeaderText="Action" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%"
                                                                    HeaderStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" AlternateText="Edit" CommandArgument='<%#Eval("optionId") %>' CommandName="Edit" ToolTip="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                                       
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlOptionsEdit" runat="server" Visible="false">
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Option text
                                                                </td>
                                                                <td colspan="3">
                                                                    <input type="text" name="txtOptionText" id="txtOptionText" runat="server"  maxlength="2" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="true"
                                                                        CssClass="error" ControlToValidate="txtOptionText" ValidationGroup="addOptions" Text="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Option Level
                                                                </td>
                                                                <td colspan="3">
                                                                    <input type="text" name="txtOptionLevel" id="txtOptionLevel" runat="server"  maxlength="20" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" SetFocusOnError="true"
                                                                        CssClass="error" ControlToValidate="txtOptionLevel" ValidationGroup="addOptions" Text="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Option value
                                                                </td>
                                                                <td colspan="3">
                                                                    <input type="text" name="txtOptionValue" id="txtOptionValue" runat="server"  maxlength="2" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" SetFocusOnError="true"
                                                                        CssClass="error" ControlToValidate="txtOptionValue" ValidationGroup="addOptions" Text="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="3">
                                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/webadmin/images/save.png" OnClick="ImageButton1_Click" ValidationGroup="addOptions" />
                                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/webadmin/images/close.png" OnClick="ImageButton2_Click" CausesValidation="false" />
                                                                    
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
