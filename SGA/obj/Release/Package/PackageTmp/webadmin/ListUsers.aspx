﻿<%@ Page Language="C#" MasterPageFile="~/webadmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ListUsers.aspx.cs" Inherits="SGA.webadmin.ListUsers" %>
<%@ Register TagName="left" TagPrefix="sga" Src="~/controls/ctrlLeftMenu.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- SimpleTabs -->
    <link rel="stylesheet" href="css/jquery-ui.css">
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

            $('a#<%=lnkSearch.ClientID %>,a#<%=lnkBASearch.ClientID %>,a#<%=lnkSSASearch.ClientID %>,a#<%=lnkNPSearch.ClientID %>').click(function () {
                $("[id$=selected_tab]").val(selected_tab);
            });

            $('.pager a,.gridHeader a,.gridHeaderASC a,.gridHeaderSortDESC a').click(function () {
                $("[id$=selected_tab]").val(selected_tab);
            });
        });

        function SelectAllCheckboxes(chk) {
            $('#<%=grdUsers.ClientID%>').find("input:checkbox").each(function () {
                if (this != chk) { this.checked = chk.checked; } 
            });
        }

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
                                    User Administration
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    
                                    <div id="tabs">
                                        <ul>
                                            <li><a href="#tabs-1">List User</a></li>
                                            <li><a href="#tabs-2">View/Edit User </a></li>
                                            <li><a href="#tabs-3">Permissions </a></li>
                                            <li><a href="#tabs-4">User Assess</a></li>
                                            <li><a href="#tabs-5">Procurement Assess</a></li>
                                            <li><a href="#tabs-6">Leadership Assess</a></li>
                                            <li><a href="#tabs-10">NP Test</a></li>
                                            <li><a href="#tabs-7">CMA Assess</a></li>
                                            <li><a href="#tabs-8">Add User</a></li>
                                            <li><a href="#tabs-9">Import</a></li>
                                        </ul>
                                        <div id="tabs-1">
                                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        USER LIST
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    First Name
                                                                </td>
                                                                <td>
                                                                    <input type="text" name="txtFname" id="txtFname" runat="server" maxlength="100" />
                                                                </td>
                                                                <td class="txtrht">
                                                                    Last Name
                                                                </td>
                                                                <td>
                                                                    <input type="text" name="txtLname" id="txtLname" runat="server" maxlength="100" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Select Users
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlOrder" runat="server">
                                                            <asp:ListItem Selected="True" Value="0" >Select</asp:ListItem>
                                                            <asp:ListItem Value="1" >Registered but not approved</asp:ListItem>
                                                            <asp:ListItem Value="2" >Registered but not logged in</asp:ListItem>
                                                            <asp:ListItem Value="3" >Active Users</asp:ListItem>
                                                            <asp:ListItem Value="4" >Expired Users</asp:ListItem>
                                                            <asp:ListItem Value="5" >Added by Admin</asp:ListItem>
                                                            <asp:ListItem Value="6" >Register by front end</asp:ListItem>
                                                        </asp:DropDownList> 
                                                                </td>
                                                                <td class="txtrht">
                                                                    Email
                                                                </td>
                                                                <td>
                                                                     <input type="text" name="txtEmail" id="txtEmail" runat="server" maxlength="250" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="txtrht">
                                                                    Registration date
                                                                </td>
                                                                <td colspan="3">
                                                                   From : 
                                                    <asp:TextBox runat="server" ID="txtFrom" style="width:100px" ></asp:TextBox>
                                                    <asp:ImageButton ID="imgStartdate" runat="server" Height="16px" ImageUrl="~/Images/cal.gif"
                                                        Width="16px" ImageAlign="Bottom" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgStartDate"
                                                        TargetControlID="txtFrom" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                    &nbsp;&nbsp;&nbsp;
                                                    To: 
                                                        <asp:TextBox ID="txtTo" runat="server" style="width:100px" ></asp:TextBox>
                                                        <asp:ImageButton ID="imgEndDate" runat="server" Height="16px" ImageUrl="~/Images/cal.gif"
                                                            Width="16px" ImageAlign="Bottom" />
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                            PopupButtonID="imgEndDate" TargetControlID="txtTo">
                                                        </ajaxToolkit:CalendarExtender>
                                                                  
                                                                </td>
                                                                
                                                            </tr>
                                                            
                                                            
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkSearch" runat="server" CausesValidation="false" 
                                        Text="Search" CssClass="rdbut" onclick="lnkSearch_Click" ></asp:LinkButton>
                                                                    
                                                                    <asp:LinkButton ID="btnExport" runat="server" CausesValidation="false" 
                                        Text="Export to Excel" CssClass="rdbut" onclick="btnExport_Click" ></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
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
                                                                <td align="right">
                                                                    <asp:ImageButton ID="iBtnLoginReminder" runat="server" OnClick="iBtnLoginReminder_Click" CausesValidation="false" ImageUrl="~/webadmin/images/sendLoginReminder.png" />
                                                                    <asp:ImageButton ID="ImageButton7" runat="server" OnClick="iBtnResendEmail_Click" CausesValidation="false" ImageUrl="~/webadmin/images/resend_button.png" />
                                                                    <asp:ImageButton ID="ImageButton8" runat="server" OnClick="iBtnApproveAll_Click" CausesValidation="false" ImageUrl="~/webadmin/images/approve_button.png" />
                                                                    <asp:ImageButton ID="ImageButton9" runat="server" OnClick="iBtnDisApproveAll_Click" CausesValidation="false" ImageUrl="~/webadmin/images/disapprove_button.png" />
                                                                    <asp:ImageButton ID="ImageButton10" runat="server" OnClick="iBtnDelete_Click" CausesValidation="false" OnClientClick="javascript:return confirm('Are you sure you want to delete selected users ?');" ImageUrl="~/webadmin/images/delete_button.png" />
                                                                    <asp:ImageButton ID="ImageButton11" runat="server" OnClick="iBtnSelect_Click" CausesValidation="false" ImageUrl="~/webadmin/images/select_button.png" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td >
                                                                &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
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
                                                    <asp:TemplateColumn ItemStyle-width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                                                        <HeaderTemplate>
                                                            <input type="checkbox" class="chkHeader" onclick="javascript:SelectAllCheckboxes(this);" id="chkHeader" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <input value='<%#Eval("Id") %>' id="chkSelect" class="chkItem" type="checkbox" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Action" ItemStyle-width="20%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton  ID="iBtnSelect" runat="server" CausesValidation="false" AlternateText="Select" CommandArgument='<%#Eval("Id") %>' CommandName="select" ToolTip="Select" ImageUrl="~/webadmin/images/select_icon.png" />
                                                            <asp:ImageButton ID="iBtnDelete" runat="server" CausesValidation="false" AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this user?');" CommandArgument='<%#Eval("Id") %>' CommandName="delete" ToolTip="Delete" ImageUrl="~/webadmin/images/disapprove_icon.png" />
                                                            <asp:ImageButton ID="iBtnApprove" runat="server" CausesValidation="false" AlternateText="Approve user" CommandArgument='<%#Eval("Id") %>' CommandName="approve" ToolTip="Approve user" ImageUrl="~/webadmin/images/approve.png" />
                                                            <asp:ImageButton ID="IbtnResend" runat="server" CausesValidation="false" AlternateText="Resend email" CommandArgument='<%#Eval("Id") %>' CommandName="send" ToolTip="Resend email" ImageUrl="~/webadmin/images/mail-32.png" />
                                                            <asp:ImageButton ID="iBtnLogin" runat="server" CausesValidation="false" AlternateText="Log in Reminder" CommandArgument='<%#Eval("Id") %>' Height="32" Width="32" CommandName="reminder" ToolTip="Log in Reminder" ImageUrl="~/webadmin/images/reminder.png" />
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
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:HiddenField ID="hdSelectIds" runat="server" />
                                                                    <asp:ImageButton ID="ImageButton17" runat="server" OnClick="iBtnLoginReminder_Click" CausesValidation="false" ImageUrl="~/webadmin/images/sendLoginReminder.png" />    
                                                                    <asp:ImageButton ID="iBtnResendEmail" runat="server" OnClick="iBtnResendEmail_Click" CausesValidation="false" ImageUrl="~/webadmin/images/resend_button.png" />
                                                                    <asp:ImageButton ID="iBtnApproveAll" runat="server" OnClick="iBtnApproveAll_Click" CausesValidation="false" ImageUrl="~/webadmin/images/approve_button.png" />
                                                                    <asp:ImageButton ID="iBtnDisApproveAll" runat="server" OnClick="iBtnDisApproveAll_Click" CausesValidation="false" ImageUrl="~/webadmin/images/disapprove_button.png" />
                                                                    <asp:ImageButton ID="iBtnDelete" runat="server" OnClick="iBtnDelete_Click" CausesValidation="false" OnClientClick="javascript:return confirm('Are you sure you want to delete selected users ?');" ImageUrl="~/webadmin/images/delete_button.png" />
                                                                    <asp:ImageButton ID="iBtnSelect" runat="server" OnClick="iBtnSelect_Click" CausesValidation="false" ImageUrl="~/webadmin/images/select_button.png" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <%--<tr>
                                                                <td align="right">
                                                                    <asp:ImageButton id="lnkPrev" runat="server" CausesValidation="false" AlternateText="Prev" ImageUrl="~/webadmin/images/prev.png" OnClick="lnkPrev_Click" />
                                                                    &nbsp;
                                                                    <asp:ImageButton id="lnkNext" runat="server" CausesValidation="false" AlternateText="Next" ImageUrl="~/webadmin/images/next.png" OnClick="lnkNext_Click" />
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>--%>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="tabs-2">
                                        
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
                                                    <td class="hd20">
                                                        VIEW/EDIT USER
                                                    </td>
                                                </tr>
      <tr>
      <td>&nbsp;</td>
      </tr>
  <tr>
        <td id="viewUser1" runat="server" class="grybox">
        
        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
  <tr>
    <td class="txtrht">First Name</td>
    <td> <asp:TextBox name="txtEditFname" id="txtEditFname" runat="server" maxlength="100" /></td>
    <td class="txtrht">Last Name</td>
    <td> <asp:TextBox name="txtEditLname" id="txtEditLname" runat="server" maxlength="100" /></td>
  </tr>
  <tr>
    <td class="txtrht">Your Organisation</td>
    <td><asp:DropDownList ID="ddlEditAgency" CssClass="styled" runat="server">
                            <asp:ListItem Value="0">Your Organisation</asp:ListItem>
                            <asp:ListItem Value="1">Premier and Cabinet</asp:ListItem>
<asp:ListItem Value="2">Aboriginal and Torres Strait Islander Partnerships</asp:ListItem>
<asp:ListItem Value="3">Agriculture and Fisheries</asp:ListItem>
<asp:ListItem Value="4">Communities, Child Safety and Disability Services</asp:ListItem>
<asp:ListItem Value="5">Education and Training</asp:ListItem>
<asp:ListItem Value="6">Energy and Water Supply</asp:ListItem>
<asp:ListItem Value="7">Environment and Heritage Protection</asp:ListItem>
<asp:ListItem Value="8">Health</asp:ListItem>
<asp:ListItem Value="9">Housing and Public Works</asp:ListItem>
<asp:ListItem Value="10">Infrastructure, Local Government and Planning</asp:ListItem>
<asp:ListItem Value="11">Justice and Attorney-General</asp:ListItem>
<asp:ListItem Value="12">National Parks, Sport and Racing</asp:ListItem>
<asp:ListItem Value="13">Natural Resources and Mines</asp:ListItem>
<asp:ListItem Value="14">Police, Fire and Emergency Services</asp:ListItem>
<asp:ListItem Value="15">Science, Information Technology and Innovation</asp:ListItem>
<asp:ListItem Value="16">State Development</asp:ListItem>
<asp:ListItem Value="17">Transport and Main Roads</asp:ListItem>
<asp:ListItem Value="18">Treasury</asp:ListItem>
<asp:ListItem Value="19">Tourism, Major Events, Small Business and the Commonwealth Games</asp:ListItem>
<asp:ListItem Value="20">Other</asp:ListItem>
                            </asp:DropDownList></td>
    <td class="txtrht">Email Address</td>
    <td><asp:TextBox name="txtEditEmailAddress" id="txtEditEmailAddress" ReadOnly="true" runat="server" maxlength="250" /></td>
  </tr>
  <tr>
    <td class="txtrht">Job Role</td>
    <td><asp:DropDownList ID="ddlEditJobRole" CssClass="styled" runat="server">
                                                <asp:ListItem Value="0">- Your Job ROLE is best described as -</asp:ListItem>
                            <asp:ListItem Value="1">Procurement Officer</asp:ListItem>
<asp:ListItem Value="2">Procurement Analyst</asp:ListItem>
<asp:ListItem Value="3">Procurement Advisor</asp:ListItem>
<asp:ListItem Value="4">Procurement Specialist</asp:ListItem>
<asp:ListItem Value="5">Contract Manager</asp:ListItem>
<asp:ListItem Value="6">Contract Manager (including procurement)</asp:ListItem>
<asp:ListItem Value="7">Category Manager</asp:ListItem>
<asp:ListItem Value="8">Procurement Director</asp:ListItem>
                                                </asp:DropDownList></td>
    <td class="txtrht">Job level</td>
    <td><asp:DropDownList ID="ddlEditJobLevel" CssClass="styled" runat="server">
                            <asp:ListItem Value="0">- Level of role best described as -</asp:ListItem>
                            <asp:ListItem Value="1">Graduate</asp:ListItem> 
<asp:ListItem Value="2">Officer</asp:ListItem>
<asp:ListItem Value="3">Advisor</asp:ListItem>
<asp:ListItem Value="4">Senior advisor</asp:ListItem> 
<asp:ListItem Value="5">Operational Leader</asp:ListItem> 
<asp:ListItem Value="6">Director</asp:ListItem>
<asp:ListItem Value="7">Executive Level</asp:ListItem>
                            </asp:DropDownList></td>
  </tr>
  <tr>
    <td class="txtrht">Manager First name</td>
    <td><asp:TextBox name="MEditfirstName" id="MEditfirstName" runat="server" maxlength="20" /></td>
    <td class="txtrht">Manager Last name</td>
    <td><asp:TextBox name="MEditlastName" id="MEditlastName" runat="server" maxlength="100"/></td>
  </tr>
  <tr>
    <td class="txtrht">Manager Email</td>
    <td><asp:TextBox name="MEditemail" id="MEditemail" runat="server" maxlength="20" /></td>
    <td class="txtrht">Division</td>
    <td><asp:TextBox name="txtDivision" id="txtDivision" runat="server" maxlength="100"/></td>
  </tr>
  <tr>
    <td class="txtrht">Password</td>
    <td><asp:TextBox name="txtEditPassword" id="txtEditPassword" runat="server" maxlength="20" /></td>
    <td class="txtrht">Phone</td>
    <td><asp:TextBox name="txtEditPhone" id="txtEditPhone" runat="server" maxlength="100"/></td>
  </tr>
  <tr>
    <td class="txtrht">Location</td>
    <td><asp:DropDownList ID="ddlEditLocation" class="styled" runat="server">
                            <asp:ListItem Value="0">Location</asp:ListItem>
                            <asp:ListItem Value="1">Brisbane </asp:ListItem>
<asp:ListItem Value="2">South West</asp:ListItem>
<asp:ListItem Value="3">Sunshine Coast</asp:ListItem>
<asp:ListItem Value="4">Gold Coast</asp:ListItem>
<asp:ListItem Value="5">Fitzroy</asp:ListItem>
<asp:ListItem Value="6">Mackay</asp:ListItem>
<asp:ListItem Value="7">Northern</asp:ListItem>
<asp:ListItem Value="8">North West</asp:ListItem>
<asp:ListItem Value="9">Wide Bay-Burnett</asp:ListItem>
<asp:ListItem Value="10">Far North Queensland</asp:ListItem>
<asp:ListItem Value="11">Darling Downs</asp:ListItem>
<asp:ListItem Value="12">Far North</asp:ListItem>
<asp:ListItem Value="13">South West Queensland</asp:ListItem>
<asp:ListItem Value="14">Northern Queensland</asp:ListItem>

                            </asp:DropDownList></td>
    <td class="txtrht">Position</td>
    <td><input type="text" id="txtEditPosition" name="txtEditPosition" title="Position" maxlength="250" runat="server" class="text-box-2" /></td>
  </tr>

  <tr>
    <td colspan="2">Nature of the goods/services that you most commonly procure, or manage contracts for?</td>
    <td colspan="2"><asp:DropDownList ID="ddlEditGoods" class="styled" runat="server">
                            <asp:ListItem Value="0">Nature of the goods/services that you most commonly procure, or manage contracts for?</asp:ListItem>
                            <asp:ListItem Value="1">Building Construction and Maintenance</asp:ListItem>
<asp:ListItem Value="2">General Goods and Services</asp:ListItem>
<asp:ListItem Value="3">ICT</asp:ListItem>
<asp:ListItem Value="4">Medical </asp:ListItem>
<asp:ListItem Value="5">Social Services</asp:ListItem>
<asp:ListItem Value="6">Transport Infrastructure & Services</asp:ListItem>


                            </asp:DropDownList></td>
  </tr>
  <tr>
    <td>Expiry Date</td>
    <td colspan="1" class="ssec"><asp:TextBox id="txtEditExiryDate" runat="server"  />
     <asp:ImageButton ID="ImageButton12" runat="server" Height="16px" ImageUrl="~/Images/cal.gif"
                                                        Width="16px" ImageAlign="Bottom" />
    <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server" PopupButtonID="ImageButton7"
                                                        TargetControlID="txtEditExiryDate" Format="dd-MM-yyyy">
                                                    </ajaxToolkit:CalendarExtender>
    </td>
    <td>Status:&nbsp;                	<asp:Label ID="lblEditStatus" runat="server"></asp:Label><br>
    </td>
    <td></td>
  </tr>
  <tr>
    
    <td colspan="4" align="center">
    <asp:ImageButton runat="server" ID="btnEditSaveProfile" onclick="btnEditSaveProfile_Click" ImageUrl="images/save.png" width="96" height="37"  />
    <asp:ImageButton runat="server" ID="btnEditProfileClose" ImageUrl="images/close.png" width="96" height="37" />
    <asp:ImageButton runat="server" ID="btnEditProfileExpire" onclick="btnEditProfileExpire_Click" ImageUrl="images/expired.png" width="192" height="37" />    </td>
  </tr>
</table>
        
        </td>
        <td id="viewUser2" runat="server" visible="false" class="grybox">
        <span class="error">
        Please select only one user to use this feature !
        </span>
        </td>
      </tr>
</table>
                                        </div>
                                        <div id="tabs-3">
                                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        MANAGE TEST PERSMISSION
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                    <asp:Button ID="btnUpdate" runat="server" CausesValidation="false" 
                                                Text="Update Permission" onclick="btnUpdate_Click" />
                                                <br /><br />
                                                <asp:DataGrid ID="dtgList" runat="server" AllowPaging="True" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" 
                                                OnItemDataBound="dtgList_ItemDataBound" OnPageIndexChanged="dtgList_PageIndexChanged"
                                                Width="100%" GridLines="None" PageSize="20">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:BoundColumn DataField="fullName" ItemStyle-Width="12%" HeaderText="Name" HeaderStyle-Width="12%" SortExpression="fullName">
                                                    </asp:BoundColumn>
                                                   
                                                    <asp:BoundColumn DataField="email" HeaderText="Email" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-Width="20%" SortExpression="email">
                                                    </asp:BoundColumn>
                                                    
                                                    <asp:TemplateColumn HeaderText="View CMC Result" Visible="false" ItemStyle-width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <input type="checkbox" id="chkSga" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("sgaResult") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="View Procurement Result"  ItemStyle-width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <input type="checkbox" id="chkTna" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("tnaResult") %>' />    
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                   <asp:TemplateColumn HeaderText="View Leadership Result"  ItemStyle-width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <input type="checkbox" id="chkPmp" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("pmpResult") %>' />    
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="View NP Result" Visible="true" ItemStyle-width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <input type="checkbox" id="chkNP" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("npResult") %>' />    
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="View MP Result" Visible="false" ItemStyle-width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <input type="checkbox" id="chkDmp" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("dmpResult") %>' />    
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="View CMA Result" ItemStyle-width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <input type="checkbox" id="chkCMA" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("cmaResult") %>' />    
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="CMC Test" Visible="false" ItemStyle-width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <input type="checkbox" id="chkSgatest" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("takeSGT") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Procurement Test" ItemStyle-width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <input type="checkbox" id="chkTnatest" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("takeTNA") %>' />    
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                   <asp:TemplateColumn HeaderText="Leadership Test" ItemStyle-width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <input type="checkbox" id="chkPmptest" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("takePMP") %>' />    
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="NP Test" Visible="true" ItemStyle-width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <input type="checkbox" id="chkNPtest" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("takeNP") %>' />    
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                  <asp:TemplateColumn HeaderText="MP Test" Visible="false" ItemStyle-width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <input type="checkbox" id="chkDmptest" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("takeDMP") %>' />    
                                                        </ItemTemplate>
                                                  </asp:TemplateColumn>
                                                  <asp:TemplateColumn HeaderText="CMA Test" ItemStyle-width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <input type="checkbox" id="chkCmatest" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("takeCMA") %>' />    
                                                        </ItemTemplate>
                                                  </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                            <asp:Button ID="btnUpdate1" runat="server" CausesValidation="false" onclick="btnUpdate_Click" Text="Update Permission" />
                                                    </td>
                                                </tr>
                                        </table>
                                        </div>

                                        <div id="tabs-4">
                                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                       ASSESSMENT STATUS
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                    
                                                <asp:DataGrid ID="grdUserTest" runat="server" AllowPaging="false" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" 
                                                Width="100%" GridLines="None" >
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:BoundColumn DataField="fullName" ItemStyle-Width="20%" HeaderText="Name" HeaderStyle-Width="20%" SortExpression="fullName">
                                                    </asp:BoundColumn>
                                                   
                                                    <asp:BoundColumn DataField="email" HeaderText="Email" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-Width="20%" >
                                                    </asp:BoundColumn>
                                                    
                                                    <asp:BoundColumn DataField="SSAtest" HeaderText="Procurement TNA" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn> 
                                                    <asp:BoundColumn DataField="BAtest" HeaderStyle-HorizontalAlign="Center" HeaderText="Leadership TNA" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                    
                                                    <asp:BoundColumn DataField="CMAtest" HeaderStyle-HorizontalAlign="Center" HeaderText="Contract Management" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="NPtest" HeaderStyle-HorizontalAlign="Center" HeaderText="Negotiation Profile" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-Width="10%" >
                                                    </asp:BoundColumn>  
                                                </Columns>
                                            </asp:DataGrid>
                                                    </td>
                                                </tr>
                                        </table>
                                        </div>

                                        
                                        <div id="tabs-5">
                                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                       Procurement Assessment
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    First Name
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtSSAFname" runat="server" maxlength="100" />
                                                                </td>
                                                                <td class="txtrht">
                                                                    Last Name
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtSSALname" runat="server" maxlength="100" />
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                               
                                                                <td class="txtrht">
                                                                    Assessment date
                                                                </td>
                                                                <td colspan="3">
                                                                    From : 
                                                    <asp:TextBox runat="server" ID="txtSSAFrom" style="width:100px" ></asp:TextBox>
                                                    <asp:ImageButton ID="ImageButton3" runat="server" Height="16px" ImageUrl="~/Images/cal.gif"
                                                        Width="16px" ImageAlign="Bottom" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="ImageButton3"
                                                        TargetControlID="txtSSAFrom" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                    &nbsp;&nbsp;&nbsp;
                                                    To: 
                                                        <asp:TextBox ID="txtSSATo" runat="server" style="width:100px" ></asp:TextBox>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" Height="16px" ImageUrl="~/Images/cal.gif"
                                                            Width="16px" ImageAlign="Bottom" />
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy"
                                                            PopupButtonID="ImageButton4" TargetControlID="txtSSATo">
                                                        </ajaxToolkit:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkSSASearch" runat="server" CausesValidation="false" 
                                        Text="Search" CssClass="rdbut" onclick="lnkSSASearch_Click" ></asp:LinkButton>
                                        <asp:LinkButton ID="lnkSSADownload" runat="server" CausesValidation="false" 
                                        Text="Export to excel" CssClass="rdbut" Visible="false" onclick="lnkSSADownload_Click" ></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                    
                                                <asp:DataGrid ID="grdSSA" runat="server" AllowPaging="True" AllowSorting="true"
                                                AutoGenerateColumns="False" CssClass="grdMain" 
                                                OnItemDataBound="grdSSA_ItemDataBound" OnItemCommand="grdSSA_ItemCommand" OnSortCommand="grdSSA_SortCommand" OnPageIndexChanged="grdSSA_PageIndexChanged"
                                                Width="100%" GridLines="None" PageSize="20">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:BoundColumn DataField="name" ItemStyle-Width="15%" HeaderText="Name" HeaderStyle-Width="15%" SortExpression="name">
                                                    </asp:BoundColumn>
                                                   
                                                    <asp:BoundColumn DataField="email" HeaderText="Email" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-Width="20%" SortExpression="email">
                                                    </asp:BoundColumn>
                                                    
                                                    <asp:TemplateColumn ItemStyle-Width="8%" HeaderStyle-Width="10%" SortExpression="Marks" HeaderText="Marks">
                                                        <ItemTemplate>
                                                           <%#Eval("marks")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    
                                                    <asp:TemplateColumn ItemStyle-Width="20%" HeaderStyle-Width="20%" SortExpression="testdate" HeaderText="Assesment Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAssesmentDate" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn ItemStyle-Width="19%" HeaderStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnDelete" runat="server" CausesValidation="false" AlternateText="Delete" style="height:25px;width:25px;" OnClientClick="return confirm('Are you sure you want to delete this record?');" CommandArgument='<%#Eval("testId") %>' CommandName="Delete" ToolTip="Delete" ImageUrl="~/webadmin/images/disapprove_icon.png" />
                                                             &nbsp;
                                                            <a target="_blank" href="ShowSSAPdf.aspx?id=<%#Eval("emailLink") %>">
                                            <img src="../innerimages/icon-pdf.gif" style="height:25px;width:25px;" alt="" /></a>
                                                            &nbsp;
                                                            <asp:ImageButton ID="iBtnGraph" runat="server" CausesValidation="false" AlternateText="Graph" style="height:25px;width:25px;" CommandArgument='<%#Eval("testId") %>' CommandName="Graph" ToolTip="Graph" ImageUrl="~/webadmin/images/img-graph-icon.gif" />
                                                            &nbsp;
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" AlternateText="Edit" style="height:25px;width:25px;" CommandArgument='<%#Eval("testId") %>' CommandName="Edit" ToolTip="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                            &nbsp;
                                                            <asp:ImageButton ID="iBtnDrill" runat="server" CausesValidation="false" AlternateText="Graph" style="height:30px;width:30px;"  CommandArgument='<%#Eval("testId") %>' CommandName="drilldown" ToolTip="drilldown" ImageUrl="~/webadmin/images/drilldown.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                            
                                                    </td>
                                                </tr>
                                        </table>
                                        </div>
                                        <div id="tabs-6">
                                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        Leadership Assessment
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    First Name
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtBAFname" runat="server" maxlength="100" />
                                                                </td>
                                                                <td class="txtrht">
                                                                    Last Name
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtBALname" runat="server" maxlength="100" />
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                               
                                                                <td class="txtrht">
                                                                    Assessment date
                                                                </td>
                                                                <td colspan="3">
                                                                    From : 
                                                    <asp:TextBox runat="server" ID="txtBAFrom" style="width:100px" ></asp:TextBox>
                                                    <asp:ImageButton ID="ImageButton5" runat="server" Height="16px" ImageUrl="~/Images/cal.gif"
                                                        Width="16px" ImageAlign="Bottom" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" PopupButtonID="ImageButton5"
                                                        TargetControlID="txtBAFrom" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                    &nbsp;&nbsp;&nbsp;
                                                    To: 
                                                        <asp:TextBox ID="txtBATo" runat="server" style="width:100px" ></asp:TextBox>
                                                        <asp:ImageButton ID="ImageButton6" runat="server" Height="16px" ImageUrl="~/Images/cal.gif"
                                                            Width="16px" ImageAlign="Bottom" />
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy"
                                                            PopupButtonID="ImageButton6" TargetControlID="txtBATo">
                                                        </ajaxToolkit:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkBASearch" runat="server" CausesValidation="false" 
                                        Text="Search" CssClass="rdbut" onclick="lnkBASearch_Click" ></asp:LinkButton>
                                        <asp:LinkButton ID="lnkBADownload" runat="server" CausesValidation="false" 
                                        Text="Export to excel" CssClass="rdbut" Visible="false" onclick="lnkBADownload_Click" ></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                    
                                                <asp:DataGrid ID="grdBA" runat="server" AllowPaging="True" AllowSorting="true"
                                                AutoGenerateColumns="False" CssClass="grdMain" 
                                                OnItemDataBound="grdBA_ItemDataBound" OnItemCommand="grdBA_ItemCommand" OnSortCommand="grdBA_SortCommand" OnPageIndexChanged="grdBA_PageIndexChanged"
                                                Width="100%" GridLines="None" PageSize="20">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:BoundColumn DataField="name" ItemStyle-Width="15%" HeaderText="Name" HeaderStyle-Width="15%" SortExpression="name">
                                                    </asp:BoundColumn>
                                                   
                                                    <asp:BoundColumn DataField="email" HeaderText="Email" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-Width="20%" SortExpression="email">
                                                    </asp:BoundColumn>
                                                    
                                                    <asp:TemplateColumn ItemStyle-Width="8%" HeaderStyle-Width="10%" SortExpression="Marks" HeaderText="Marks">
                                                        <ItemTemplate>
                                                           <%#Eval("marks")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    
                                                    <asp:TemplateColumn ItemStyle-Width="20%" HeaderStyle-Width="20%" SortExpression="testdate" HeaderText="Assesment Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAssesmentDate" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn ItemStyle-Width="22%" HeaderStyle-Width="22%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnDelete" runat="server" CausesValidation="false" AlternateText="Delete" style="height:25px;width:25px;" OnClientClick="return confirm('Are you sure you want to delete this record?');" CommandArgument='<%#Eval("testId") %>' CommandName="Delete" ToolTip="Delete" ImageUrl="~/webadmin/images/disapprove_icon.png" />
                                                              &nbsp;
                                                            <a target="_blank" href="ShowBAPdf.aspx?id=<%#Eval("emailLink") %>">
                                            <img src="../innerimages/icon-pdf.gif" style="height:25px;width:25px;" alt="" /></a>
                                                             &nbsp;
                                                            <asp:ImageButton ID="iBtnGraph" runat="server" CausesValidation="false" AlternateText="Graph" style="height:25px;width:25px;" CommandArgument='<%#Eval("testId") %>' CommandName="Graph" ToolTip="Graph" ImageUrl="~/webadmin/images/img-graph-icon.gif" />
                                                            &nbsp;
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" AlternateText="Edit" style="height:25px;width:25px;" CommandArgument='<%#Eval("testId") %>' CommandName="Edit" ToolTip="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                            &nbsp;
                                                            <asp:ImageButton ID="iBtnDrill" runat="server" CausesValidation="false" AlternateText="Graph" style="height:30px;width:30px;"  CommandArgument='<%#Eval("testId") %>' CommandName="drilldown" ToolTip="drilldown" ImageUrl="~/webadmin/images/drilldown.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                            
                                                    </td>
                                                </tr>
                                        </table>
                                        </div>
                                        <div id="tabs-7">
                                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        Contract Management Assessment
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    First Name
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtCMAFname" runat="server" maxlength="100" />
                                                                </td>
                                                                <td class="txtrht">
                                                                    Last Name
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtCMALname" runat="server" maxlength="100" />
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                
                                                                <td class="txtrht">
                                                                    Assessment date
                                                                </td>
                                                                <td colspan="3">
                                                                    From : 
                                                    <asp:TextBox runat="server" ID="txtCMAFrom" style="width:100px" ></asp:TextBox>
                                                    <asp:ImageButton ID="ImageButton18" runat="server" Height="16px" ImageUrl="~/Images/cal.gif"
                                                        Width="16px" ImageAlign="Bottom" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender14" runat="server" PopupButtonID="ImageButton18"
                                                        TargetControlID="txtCMAFrom" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                    &nbsp;&nbsp;&nbsp;
                                                    To: 
                                                        <asp:TextBox ID="txtCMATo" runat="server" style="width:100px" ></asp:TextBox>
                                                        <asp:ImageButton ID="ImageButton19" runat="server" Height="16px" ImageUrl="~/Images/cal.gif"
                                                            Width="16px" ImageAlign="Bottom" />
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender15" runat="server" Format="dd/MM/yyyy"
                                                            PopupButtonID="ImageButton19" TargetControlID="txtCMATo">
                                                        </ajaxToolkit:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkCMASearch" runat="server" CausesValidation="false" 
                                        Text="Search" CssClass="rdbut" onclick="lnkCMASearch_Click" ></asp:LinkButton>
                                        
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                    
                                                <asp:DataGrid ID="grdCMA" runat="server" AllowPaging="True" AllowSorting="true"
                                                AutoGenerateColumns="False" CssClass="grdMain" 
                                                OnItemDataBound="grdCMA_ItemDataBound" OnItemCommand="grdCMA_ItemCommand" OnSortCommand="grdCMA_SortCommand" OnPageIndexChanged="grdCMA_PageIndexChanged"
                                                Width="100%" GridLines="None" PageSize="20">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:BoundColumn DataField="name" ItemStyle-Width="15%" HeaderText="Name" HeaderStyle-Width="15%" SortExpression="name">
                                                    </asp:BoundColumn>
                                                   
                                                    <asp:BoundColumn DataField="email" HeaderText="Email" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-Width="20%" SortExpression="email">
                                                    </asp:BoundColumn>
                                                    
                                                    <asp:TemplateColumn ItemStyle-Width="8%" HeaderStyle-Width="10%" SortExpression="Marks" HeaderText="Marks">
                                                        <ItemTemplate>
                                                           <%#Eval("marks")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    
                                                    <asp:TemplateColumn ItemStyle-Width="20%" HeaderStyle-Width="20%" SortExpression="testdate" HeaderText="Assesment Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAssesmentDate" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn ItemStyle-Width="19%" HeaderStyle-Width="19%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnDelete" runat="server" CausesValidation="false" AlternateText="Delete" style="height:25px;width:25px;" OnClientClick="return confirm('Are you sure you want to delete this record?');" CommandArgument='<%#Eval("testId") %>' CommandName="Delete" ToolTip="Delete" ImageUrl="~/webadmin/images/disapprove_icon.png" />
                                                             &nbsp;
                                                            <a target="_blank" href="ShowCMAPdf.aspx?id=<%#Eval("emailLink") %>">
                                            <img src="../innerimages/icon-pdf.gif" style="height:25px;width:25px;" alt="" /></a>
                                                            &nbsp;
                                                            <asp:ImageButton ID="iBtnGraph" runat="server" CausesValidation="false" AlternateText="Graph" style="height:25px;width:25px;" CommandArgument='<%#Eval("testId") %>' CommandName="Graph" ToolTip="Graph" ImageUrl="~/webadmin/images/img-graph-icon.gif" />
                                                            &nbsp;
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" AlternateText="Edit" style="height:25px;width:25px;" CommandArgument='<%#Eval("testId") %>' CommandName="Edit" ToolTip="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                            &nbsp;
                                                            <asp:ImageButton ID="iBtnDrill" runat="server" CausesValidation="false" AlternateText="Graph" style="height:30px;width:30px;"  CommandArgument='<%#Eval("testId") %>' CommandName="drilldown" ToolTip="drilldown" ImageUrl="~/webadmin/images/drilldown.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                            
                                                    </td>
                                                </tr>
                                        </table>
                                        </div>
                                        
                                        
                                        <div id="tabs-8">
                                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        Add User
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
                            
                            <td class="txtrht">Email Address</td>
                            <td><input type="text" name="txtEmailAddress" id="txtEmailAddress" runat="server" maxlength="250" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="true" ControlToValidate="txtEmailAddress" CssClass="error" ValidationGroup="add" Text="*"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailAddress" CssClass="error"
                                                ErrorMessage="Invalid email address." ForeColor="Red" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            </td>
                            <td class="txtrht">Your Organisation</td>
                            <td><asp:DropDownList ID="ddlAgency" CssClass="styled" runat="server">
                            <asp:ListItem Value="0">Your Organisation</asp:ListItem>
                            <asp:ListItem Value="1">Premier and Cabinet</asp:ListItem>
<asp:ListItem Value="2">Aboriginal and Torres Strait Islander Partnerships</asp:ListItem>
<asp:ListItem Value="3">Agriculture and Fisheries</asp:ListItem>
<asp:ListItem Value="4">Communities, Child Safety and Disability Services</asp:ListItem>
<asp:ListItem Value="5">Education and Training</asp:ListItem>
<asp:ListItem Value="6">Energy and Water Supply</asp:ListItem>
<asp:ListItem Value="7">Environment and Heritage Protection</asp:ListItem>
<asp:ListItem Value="8">Health</asp:ListItem>
<asp:ListItem Value="9">Housing and Public Works</asp:ListItem>
<asp:ListItem Value="10">Infrastructure, Local Government and Planning</asp:ListItem>
<asp:ListItem Value="11">Justice and Attorney-General</asp:ListItem>
<asp:ListItem Value="12">National Parks, Sport and Racing</asp:ListItem>
<asp:ListItem Value="13">Natural Resources and Mines</asp:ListItem>
<asp:ListItem Value="14">Police, Fire and Emergency Services</asp:ListItem>
<asp:ListItem Value="15">Science, Information Technology and Innovation</asp:ListItem>
<asp:ListItem Value="16">State Development</asp:ListItem>
<asp:ListItem Value="17">Transport and Main Roads</asp:ListItem>
<asp:ListItem Value="18">Treasury</asp:ListItem>
<asp:ListItem Value="19">Tourism, Major Events, Small Business and the Commonwealth Games</asp:ListItem>
<asp:ListItem Value="20">Other</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true" InitialValue="0" CssClass="error" ControlToValidate="ddlAgency" ValidationGroup="add" Text="*"></asp:RequiredFieldValidator>
                            </td>
                            </tr>
                       <tr>
                        <td class="txtrht">Your Job level</td>
                        <td><asp:DropDownList ID="ddlJobLevel" CssClass="styled" runat="server">
                            <asp:ListItem Value="0">- Level of role best described as -</asp:ListItem>
                            <asp:ListItem Value="1">Graduate</asp:ListItem> 
<asp:ListItem Value="2">Officer</asp:ListItem>
<asp:ListItem Value="3">Advisor</asp:ListItem>
<asp:ListItem Value="4">Senior advisor</asp:ListItem> 
<asp:ListItem Value="5">Operational Leader</asp:ListItem> 
<asp:ListItem Value="6">Director</asp:ListItem>
<asp:ListItem Value="7">Executive Level</asp:ListItem>
                            </asp:DropDownList>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" SetFocusOnError="true" ControlToValidate="ddlJobLevel" CssClass="error" InitialValue="0" ValidationGroup="add" Text="*"></asp:RequiredFieldValidator>
                                                </td>
                        <td class="txtrht">Your Job ROLE</td>
                        <td><asp:DropDownList ID="ddlJobRole" CssClass="styled" runat="server">
                        <asp:ListItem Value="0">- Your Job ROLE is best described as -</asp:ListItem>
                            <asp:ListItem Value="1">Procurement Officer</asp:ListItem>
<asp:ListItem Value="2">Procurement Analyst</asp:ListItem>
<asp:ListItem Value="3">Procurement Advisor</asp:ListItem>
<asp:ListItem Value="4">Procurement Specialist</asp:ListItem>
<asp:ListItem Value="5">Contract Manager</asp:ListItem>
<asp:ListItem Value="6">Contract Manager (including procurement)</asp:ListItem>
<asp:ListItem Value="7">Category Manager</asp:ListItem>
<asp:ListItem Value="8">Procurement Director</asp:ListItem>
                            </asp:DropDownList> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" SetFocusOnError="true" ControlToValidate="ddlJobRole" InitialValue="0" CssClass="error"  ValidationGroup="add" Text="*"></asp:RequiredFieldValidator>
                        </td>
                        </tr>
                        
                        <tr>
                        <td class="txtrht">Manager's First Name</td>
                        <td> <input type="text" name="MfirstName" id="MfirstName" runat="server" maxlength="100"   />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" SetFocusOnError="true" CssClass="error" ControlToValidate="MfirstName" ValidationGroup="add" Text="*"></asp:RequiredFieldValidator>
                        </td>
                        <td class="txtrht">Manager's Last Name</td>
                        <td> <input type="text" name="MlastName" id="MlastName" runat="server" maxlength="100" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" SetFocusOnError="true" CssClass="error" ControlToValidate="MlastName" ValidationGroup="add" Text="*"></asp:RequiredFieldValidator>
                        </td>
                        </tr><tr>
                        <td class="txtrht">Manager's Email</td>
                        <td colspan="3"> <input type="text" name="Memail" id="Memail" runat="server" maxlength="100"   />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" SetFocusOnError="true" CssClass="error" ControlToValidate="Memail" ValidationGroup="add" Text="*"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="Memail" CssClass="error"
                                                ErrorMessage="Invalid email address." ForeColor="Red" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                        </td>
                        
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
                                        </table>
                                        </div>
                                        <div id="tabs-9">
                                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        Import Users using excel
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                        <tr>
                                                            <td class="txtrht">Upload excel</td>
                                                            <td><asp:FileUpload ID="FileInput" runat="server" />
                                                            <asp:RequiredFieldValidator ID="rfqUpload" runat="server" Display="Dynamic" ControlToValidate="FileInput"
            ErrorMessage="Please select file to upload" ValidationGroup="vg"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic"
            ControlToValidate="FileInput" runat="server" ErrorMessage="File should be in xlsx format."
            ValidationExpression="^.+\.(xlsx){1}$" ValidationGroup="vg"></asp:RegularExpressionValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="txtrht">&nbsp;</td>
                                                            <td >
        <asp:HyperLink ID="hylExcelupload" runat="server" NavigateUrl="~/webadmin/docs/SGAUsers_Upload_Template.xlsx" Style="text-decoration: underline;
            font-size: small;">Download sample copy of excel upload</asp:HyperLink>
            </td>
                                                        </tr>
                                                        <tr>
                                                        <td class="txtrht">&nbsp;</td>
                                                        <td >
                                                            <asp:Label ID="lblUploadError" runat="server" CssClass="error"></asp:Label>
                                                        </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="txtrht">&nbsp;</td>
                                                            <td >
                                                            <asp:ImageButton ID="imgUpload" runat="server" ImageUrl="~/webadmin/images/save.png" OnClick="imgUpload_Click" ValidationGroup="vg" />
                                                            
                                                            </td>
    
                                                          </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                        </table>
                                        </div>
                                        <div id="tabs-10">
                                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="hd20">
                                                        Negotiation Profile Assessment
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="grybox">
                                                        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
                                                            <tr>
                                                                <td class="txtrht">
                                                                    First Name
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtNPFname" runat="server" maxlength="100" />
                                                                </td>
                                                                <td class="txtrht">
                                                                    Last Name
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtNPLname" runat="server" maxlength="100" />
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                
                                                                <td class="txtrht">
                                                                    Assessment date
                                                                </td>
                                                                <td colspan="3">
                                                                    From : 
                                                    <asp:TextBox runat="server" ID="txtNPFrom" style="width:100px" ></asp:TextBox>
                                                    <asp:ImageButton ID="ImageButton15" runat="server" Height="16px" ImageUrl="~/Images/cal.gif"
                                                        Width="16px" ImageAlign="Bottom" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender12" runat="server" PopupButtonID="ImageButton15"
                                                        TargetControlID="txtNPFrom" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                    &nbsp;&nbsp;&nbsp;
                                                    To: 
                                                        <asp:TextBox ID="txtNPTo" runat="server" style="width:100px" ></asp:TextBox>
                                                        <asp:ImageButton ID="ImageButton16" runat="server" Height="16px" ImageUrl="~/Images/cal.gif"
                                                            Width="16px" ImageAlign="Bottom" />
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender13" runat="server" Format="dd/MM/yyyy"
                                                            PopupButtonID="ImageButton16" TargetControlID="txtNPTo">
                                                        </ajaxToolkit:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkNPSearch" runat="server" CausesValidation="false" 
                                        Text="Search" CssClass="rdbut" onclick="lnkNPSearch_Click" ></asp:LinkButton>
                                       
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                    
                                                <asp:DataGrid ID="grdNP" runat="server" AllowPaging="True" AllowSorting="true"
                                                AutoGenerateColumns="False" CssClass="grdMain" 
                                                OnItemDataBound="grdNP_ItemDataBound" OnItemCommand="grdNP_ItemCommand" OnSortCommand="grdNP_SortCommand" OnPageIndexChanged="grdNP_PageIndexChanged"
                                                Width="100%" GridLines="None" PageSize="20">
                                                <HeaderStyle CssClass="gridHeader" />
                                                <PagerStyle Mode="NumericPages"  CssClass="pager" HorizontalAlign="Center"  />
                                                <ItemStyle CssClass="gridItem"  />
                                                <Columns>
                                                    <asp:BoundColumn DataField="name" ItemStyle-Width="15%" HeaderText="Name" HeaderStyle-Width="15%" SortExpression="name">
                                                    </asp:BoundColumn>
                                                   
                                                    <asp:BoundColumn DataField="email" HeaderText="Email" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderStyle-Width="20%" SortExpression="email">
                                                    </asp:BoundColumn>
                                                    
                                                    <asp:TemplateColumn ItemStyle-Width="8%" HeaderStyle-Width="10%" SortExpression="Marks" HeaderText="Marks">
                                                        <ItemTemplate>
                                                           <%#Eval("marks")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    
                                                    <asp:TemplateColumn ItemStyle-Width="20%" HeaderStyle-Width="20%" SortExpression="testdate" HeaderText="Assesment<br>Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAssesmentDate" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn ItemStyle-Width="22%" HeaderStyle-Width="22%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBtnDelete" runat="server" CausesValidation="false" AlternateText="Delete" style="height:25px;width:25px;" OnClientClick="return confirm('Are you sure you want to delete this record?');" CommandArgument='<%#Eval("testId") %>' CommandName="Delete" ToolTip="Delete" ImageUrl="~/webadmin/images/disapprove_icon.png" />
                                                              &nbsp;
                                                            <a target="_blank" href="ShowNPPdf.aspx?id=<%#Eval("testId") %>&userId=<%#Eval("userId") %>">
                                            <img src="../innerimages/icon-pdf.gif" style="height:25px;width:25px;" alt="" /></a>
                                                             &nbsp;
                                                            <asp:ImageButton ID="iBtnGraph" runat="server" CausesValidation="false" AlternateText="Graph" style="height:25px;width:25px;" CommandArgument='<%#Eval("testId") %>' CommandName="Graph" ToolTip="Graph" ImageUrl="~/webadmin/images/img-graph-icon.gif" />
                                                            &nbsp;
                                                            <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" AlternateText="Edit" style="height:25px;width:25px;" CommandArgument='<%#Eval("testId") %>' CommandName="Edit" ToolTip="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                                            &nbsp;
                                                            <asp:ImageButton ID="iBtnDrill" runat="server" CausesValidation="false" AlternateText="Graph" style="height:30px;width:30px;"  CommandArgument='<%#Eval("testId") %>' CommandName="drilldown" ToolTip="drilldown" ImageUrl="~/webadmin/images/drilldown.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                            
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


