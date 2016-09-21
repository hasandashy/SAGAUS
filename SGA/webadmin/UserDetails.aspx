<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="SGA.webadmin.UserDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Details</title>
    <link href="css/esourcing.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../js/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scrMgr" runat="server">
        </asp:ScriptManager>
        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
            <tr>
                <td colspan="4">
                    <b>User Details</b>
                </td>
            </tr>
            <tr>
                <td class="txtrht">First Name
                </td>
                <td>
                    <asp:TextBox name="txtEditFname" ID="txtEditFname" runat="server" MaxLength="100" />
                </td>
                <td class="txtrht">Last Name
                </td>
                <td>
                    <asp:TextBox name="txtEditLname" ID="txtEditLname" runat="server" MaxLength="100" />
                </td>
            </tr>
            <tr>
                <td class="txtrht">Your Organisation
                </td>
                <td>
                    <asp:DropDownList ID="ddlEditAgency" CssClass="styled" runat="server">
                        <asp:ListItem Value="0">Your Organisation</asp:ListItem>
                            <asp:ListItem Value="1">Attorney-Generals Department</asp:ListItem>
                            <asp:ListItem Value="2">Courts Administration Authority</asp:ListItem>
                            <asp:ListItem Value="3">Department for Communities and Social Inclusion</asp:ListItem>
                            <asp:ListItem Value="4">Department for Correctional Services</asp:ListItem>
                            <asp:ListItem Value="5">Department for Education and Child Development</asp:ListItem>
                            <asp:ListItem Value="6">Department of Environment Water and Natural Resources</asp:ListItem>
                            <asp:ListItem Value="7">Department of Planning Transport and Infrastructure</asp:ListItem>
                            <asp:ListItem Value="8">Department of State Development</asp:ListItem>
                            <asp:ListItem Value="9">Department of the Premier and Cabinet </asp:ListItem>
                            <asp:ListItem Value="10">Department of Treasury and Finance</asp:ListItem>
                            <asp:ListItem Value="11">Primary Industries and Regions SA</asp:ListItem>
                            <asp:ListItem Value="12">SA Fire and Emergency Services Commission</asp:ListItem>
                            <asp:ListItem Value="13">SA Health</asp:ListItem>
                            <asp:ListItem Value="14">South Australia Police</asp:ListItem>
                            <asp:ListItem Value="15">South Australian Tourism Commission</asp:ListItem>
                            <asp:ListItem Value="16">TAFE SA</asp:ListItem> 
                    </asp:DropDownList>
                </td>
                <td class="txtrht">Email Address
                </td>
                <td>
                    <asp:TextBox name="txtEditEmailAddress" ID="txtEditEmailAddress" ReadOnly="true"
                        runat="server" MaxLength="250" />
                </td>
            </tr>
            <tr>
                <td class="txtrht">Job Role
                </td>
                <td>
                    <asp:DropDownList ID="ddlEditJobRole" CssClass="styled" runat="server">
                         <asp:ListItem Value="0">Please select</asp:ListItem>
                            <asp:ListItem Value="1">Purchasing Officer</asp:ListItem>
                            <asp:ListItem Value="2">Procurement/ Purchasing Support</asp:ListItem>
                            <asp:ListItem Value="3">Procurement/ Purchasing Analyst</asp:ListItem>
                            <asp:ListItem Value="4">Procurement Officer/ Advisor</asp:ListItem>
                            <asp:ListItem Value="5">Procurement Specialist</asp:ListItem>
                            <asp:ListItem Value="6">Contract Manager</asp:ListItem>
                            <asp:ListItem Value="7">Category Manager</asp:ListItem>
                            <asp:ListItem Value="8">Procurement Manager/ Director</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="txtrht">Experience
                </td>
                <td>
                   <asp:DropDownList ID="ddlExperience" class="styled" runat="server">
                            <asp:ListItem Value="0">Please select ---</asp:ListItem>
                            <asp:ListItem Value="1">Less than 1 year</asp:ListItem>
                            <asp:ListItem Value="2">1 to 3 years</asp:ListItem>
                            <asp:ListItem Value="3">3 to 5 years</asp:ListItem>
                            <asp:ListItem Value="4">5 to 10 years</asp:ListItem>
                            <asp:ListItem Value="5">10 to 15 years</asp:ListItem>
                            <asp:ListItem Value="6">15 to 20 years</asp:ListItem>
                            <asp:ListItem Value="7">More than 20 years</asp:ListItem>
                        </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td class="txtrht">Password
                </td>
                <td>
                    <asp:TextBox name="txtEditPassword" ID="txtEditPassword" runat="server" MaxLength="20" />
                </td>
                <td class="txtrht">Phone
                </td>
                <td>
                    <asp:TextBox name="txtEditPhone" ID="txtEditPhone" runat="server" MaxLength="100" />
                </td>
            </tr>            
            <tr>
                <td colspan="2">What is the nature of the goods/ services that you most commonly procure, or manage contracts for?
                for?
                </td>
                <td colspan="2">
                   <asp:DropDownList ID="ddlNature" class="styled" runat="server">
                            <asp:ListItem Value="0">Please select ---</asp:ListItem>
                            <asp:ListItem Value="1">General Goods and Services</asp:ListItem>
                            <asp:ListItem Value="2">ICT</asp:ListItem>
                            <asp:ListItem Value="3">Medical / Health</asp:ListItem>
                            <asp:ListItem Value="4">Social Services</asp:ListItem>
                            <asp:ListItem Value="5">Transport Infrastructure & Services</asp:ListItem>
                            <asp:ListItem Value="6">Building Construction and Maintenance</asp:ListItem>
                        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Expiry Date
                </td>
                <td colspan="2" class="ssec">
                    <asp:TextBox ID="txtEditExiryDate" runat="server" />
                    <asp:ImageButton ID="ImageButton12" runat="server" Height="16px" ImageUrl="~/Images/cal.gif"
                        Width="16px" ImageAlign="Bottom" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server" PopupButtonID="ImageButton7"
                        TargetControlID="txtEditExiryDate" Format="dd-MM-yyyy">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td colspan="3">Status:&nbsp;
                <asp:Label ID="lblEditStatus" runat="server"></asp:Label><br>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:ImageButton runat="server" ID="btnEditSaveProfile" OnClick="btnEditSaveProfile_Click"
                        ImageUrl="images/save.png" Width="96" Height="37" />
                    <asp:ImageButton runat="server" ID="btnEditProfileClose" ImageUrl="images/close.png"
                        Width="96" Height="37" />
                    <asp:ImageButton runat="server" ID="btnEditProfileExpire" OnClick="btnEditProfileExpire_Click"
                        ImageUrl="images/expired.png" Width="192" Height="37" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
            <tr>
                <td colspan="4">
                    <b>Test Permission</b>
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="4"></td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:DataGrid ID="dtgPermission" runat="server" AllowPaging="false" AllowSorting="false"
                        AutoGenerateColumns="False" CssClass="grdMain" Width="100%" GridLines="None"
                        PageSize="20">
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle Mode="NumericPages" CssClass="pager" HorizontalAlign="Center" />
                        <ItemStyle CssClass="gridItem" />
                        <Columns>
                            <asp:BoundColumn DataField="fullName" ItemStyle-Width="12%" HeaderText="Name" HeaderStyle-Width="12%"
                                SortExpression="fullName"></asp:BoundColumn>
                            <asp:BoundColumn DataField="email" HeaderText="Email" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="20%" HeaderStyle-Width="20%" SortExpression="email"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="View PKE Result" Visible="false" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <input type="checkbox" id="chkPke" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("pkeResult") %>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="View TNA Result" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <input type="checkbox" id="chkTna" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("tnaResult") %>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="View CMK Result" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <input type="checkbox" id="chkCmk" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("cmkResult") %>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="View CAA Result" Visible="true" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <input type="checkbox" id="chkCaa" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("caaResult") %>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="View CMA Result" Visible="false" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <input type="checkbox" id="chkCma" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("cmaResult") %>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="PKE Test" Visible="false" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <input type="checkbox" id="chkPketest" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("takePKE") %>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="TNA Test" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <input type="checkbox" id="chkTnatest" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("takeTNA") %>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CMK Test" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <input type="checkbox" id="chkCmktest" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("takeCMK") %>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CAA Test" Visible="true" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <input type="checkbox" id="chkCaatest" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("takeCAA") %>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CMA Test" Visible="false" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <input type="checkbox" id="chkCmatest" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("takeCMA") %>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:ImageButton runat="server" ID="ibtnUpdate" OnClick="ibtnUpdate_Click" ImageUrl="images/save.png"
                        Width="96" Height="37" />
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="4"></td>
            </tr>
        </table>
        <br />
        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
            <tr>
                <td colspan="4">
                    <b>Procurement Assessment</b>
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="4"></td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="4">
                    <asp:DataGrid ID="grdSSA" runat="server" AllowPaging="false" AllowSorting="false"
                        AutoGenerateColumns="False" CssClass="grdMain" OnItemDataBound="grdSSA_ItemDataBound"
                        OnItemCommand="grdSSA_ItemCommand" Width="100%" GridLines="None" PageSize="20">
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle Mode="NumericPages" CssClass="pager" HorizontalAlign="Center" />
                        <ItemStyle CssClass="gridItem" />
                        <Columns>
                            <asp:BoundColumn DataField="name" ItemStyle-Width="18%" HeaderText="Name" HeaderStyle-Width="18%"
                                SortExpression="name"></asp:BoundColumn>
                            <asp:TemplateColumn ItemStyle-Width="10%" HeaderStyle-Width="10%" SortExpression="Marks"
                                HeaderText="Marks">
                                <ItemTemplate>
                                    <%#Eval("marks")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-Width="20%" HeaderStyle-Width="20%" SortExpression="testdate"
                                HeaderText="Assesment Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssesmentDate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-Width="24%" HeaderStyle-Width="24%" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center" HeaderText="Action">
                                <ItemTemplate>
                                    <asp:ImageButton ID="iBtnDelete" runat="server" CausesValidation="false" AlternateText="Delete"
                                        Style="height: 25px; width: 25px;" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                        CommandArgument='<%#Eval("testId") %>' CommandName="Delete" ToolTip="Delete"
                                        ImageUrl="~/webadmin/images/disapprove_icon.png" />
                                  
                                    &nbsp;
                                <asp:ImageButton ID="iBtnGraph" runat="server" CausesValidation="false" AlternateText="Graph"
                                    Style="height: 25px; width: 25px;" CommandArgument='<%#Eval("testId") %>' CommandName="Graph"
                                    ToolTip="Graph" ImageUrl="~/webadmin/images/img-graph-icon.gif" />
                                    &nbsp;
                                <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" AlternateText="Edit"
                                    Style="height: 25px; width: 25px;" CommandArgument='<%#Eval("testId") %>' CommandName="Edit"
                                    ToolTip="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                    &nbsp;
                                <asp:ImageButton ID="iBtnDrill" runat="server" CausesValidation="false" AlternateText="Graph"
                                    Style="height: 30px; width: 30px;" CommandArgument='<%#Eval("testId") %>' CommandName="drilldown"
                                    ToolTip="drilldown" ImageUrl="~/webadmin/images/drilldown.png" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="4"></td>
            </tr>
        </table>
        <br />
        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
            <tr>
                <td colspan="6">
                    <b>Procurement Evaluation</b>
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="4"></td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:DataGrid ID="grdSga" runat="server" AllowPaging="false" AllowSorting="false"
                        AutoGenerateColumns="False" CssClass="grdMain" OnItemDataBound="grdSga_ItemDataBound"
                        OnItemCommand="grdSga_ItemCommand" Width="100%" GridLines="None" PageSize="20">
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle Mode="NumericPages" CssClass="pager" HorizontalAlign="Center" />
                        <ItemStyle CssClass="gridItem" />
                        <Columns>
                            <asp:BoundColumn DataField="name" ItemStyle-Width="18%" HeaderText="Name" HeaderStyle-Width="18%"
                                SortExpression="name"></asp:BoundColumn>
                            <asp:TemplateColumn ItemStyle-Width="10%" HeaderStyle-Width="10%" SortExpression="Marks"
                                HeaderText="Marks">
                                <ItemTemplate>
                                    <%#Eval("marks")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-Width="20%" HeaderStyle-Width="20%" SortExpression="testdate"
                                HeaderText="Assesment Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssesmentDate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-Width="24%" HeaderStyle-Width="24%" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center" HeaderText="Action">
                                <ItemTemplate>
                                    <asp:ImageButton ID="iBtnDelete" runat="server" CausesValidation="false" AlternateText="Delete"
                                        Style="height: 25px; width: 25px;" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                        CommandArgument='<%#Eval("testId") %>' CommandName="Delete" ToolTip="Delete"
                                        ImageUrl="~/webadmin/images/disapprove_icon.png" />
                                   
                                    &nbsp;
                                <asp:ImageButton ID="iBtnGraph" runat="server" CausesValidation="false" AlternateText="Graph"
                                    Style="height: 25px; width: 25px;" CommandArgument='<%#Eval("testId") %>' CommandName="Graph"
                                    ToolTip="Graph" ImageUrl="~/webadmin/images/img-graph-icon.gif" />
                                    &nbsp;
                                <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" AlternateText="Edit"
                                    Style="height: 25px; width: 25px;" CommandArgument='<%#Eval("testId") %>' CommandName="Edit"
                                    ToolTip="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                    &nbsp;
                                <asp:ImageButton ID="iBtnDrill" runat="server" CausesValidation="false" AlternateText="Graph"
                                    Style="height: 30px; width: 30px;" CommandArgument='<%#Eval("testId") %>' CommandName="drilldown"
                                    ToolTip="drilldown" ImageUrl="~/webadmin/images/drilldown.png" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="4"></td>
            </tr>
        </table>
        <br />
        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
            <tr>
                <td colspan="4">
                    <b>Contract Management Assessment</b>
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="4"></td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:DataGrid ID="grdCMA" runat="server" AllowPaging="false" AllowSorting="false"
                        AutoGenerateColumns="False" CssClass="grdMain" OnItemDataBound="grdCMA_ItemDataBound"
                        OnItemCommand="grdCMA_ItemCommand" Width="100%" GridLines="None" PageSize="20">
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle Mode="NumericPages" CssClass="pager" HorizontalAlign="Center" />
                        <ItemStyle CssClass="gridItem" />
                        <Columns>
                            <asp:BoundColumn DataField="name" ItemStyle-Width="18%" HeaderText="Name" HeaderStyle-Width="18%"
                                SortExpression="name"></asp:BoundColumn>
                            <asp:TemplateColumn ItemStyle-Width="12%" HeaderStyle-Width="12%" SortExpression="Marks"
                                HeaderText="Marks">
                                <ItemTemplate>
                                    <%#Eval("marks")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-Width="20%" HeaderStyle-Width="20%" SortExpression="testdate"
                                HeaderText="Assesment Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssesmentDate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-Width="24%" HeaderStyle-Width="24%" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center" HeaderText="Action">
                                <ItemTemplate>
                                    <asp:ImageButton ID="iBtnDelete" runat="server" CausesValidation="false" AlternateText="Delete"
                                        Style="height: 25px; width: 25px;" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                        CommandArgument='<%#Eval("testId") %>' CommandName="Delete" ToolTip="Delete"
                                        ImageUrl="~/webadmin/images/disapprove_icon.png" />
                                 &nbsp;
                                <asp:ImageButton ID="iBtnGraph" runat="server" CausesValidation="false" AlternateText="Graph"
                                    Style="height: 25px; width: 25px;" CommandArgument='<%#Eval("testId") %>' CommandName="Graph"
                                    ToolTip="Graph" ImageUrl="~/webadmin/images/img-graph-icon.gif" />
                                    &nbsp;
                                <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" AlternateText="Edit"
                                    Style="height: 25px; width: 25px;" CommandArgument='<%#Eval("testId") %>' CommandName="Edit"
                                    ToolTip="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                    &nbsp;
                                <asp:ImageButton ID="iBtnDrill" runat="server" CausesValidation="false" AlternateText="Graph"
                                    Style="height: 30px; width: 30px;" CommandArgument='<%#Eval("testId") %>' CommandName="drilldown"
                                    ToolTip="drilldown" ImageUrl="~/webadmin/images/drilldown.png" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="4"></td>
            </tr>
        </table>
        <br />
        <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
            <tr>
                <td colspan="4">
                    <b>Contract Management Evaluation</b>
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="4"></td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:DataGrid ID="grdCMK" runat="server" AllowPaging="false" AllowSorting="false"
                        AutoGenerateColumns="False" CssClass="grdMain" OnItemDataBound="grdCMK_ItemDataBound"
                        OnItemCommand="grdCMK_ItemCommand" Width="100%" GridLines="None" PageSize="20">
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle Mode="NumericPages" CssClass="pager" HorizontalAlign="Center" />
                        <ItemStyle CssClass="gridItem" />
                        <Columns>
                            <asp:BoundColumn DataField="name" ItemStyle-Width="18%" HeaderText="Name" HeaderStyle-Width="18%"
                                SortExpression="name"></asp:BoundColumn>
                            <asp:TemplateColumn ItemStyle-Width="12%" HeaderStyle-Width="12%" SortExpression="Marks"
                                HeaderText="Marks">
                                <ItemTemplate>
                                    <%#Eval("marks")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-Width="20%" HeaderStyle-Width="20%" SortExpression="testdate"
                                HeaderText="Assesment Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssesmentDate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-Width="24%" HeaderStyle-Width="24%" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center" HeaderText="Action">
                                <ItemTemplate>
                                    <asp:ImageButton ID="iBtnDelete" runat="server" CausesValidation="false" AlternateText="Delete"
                                        Style="height: 25px; width: 25px;" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                        CommandArgument='<%#Eval("testId") %>' CommandName="Delete" ToolTip="Delete"
                                        ImageUrl="~/webadmin/images/disapprove_icon.png" />
                                 &nbsp;
                                <asp:ImageButton ID="iBtnGraph" runat="server" CausesValidation="false" AlternateText="Graph"
                                    Style="height: 25px; width: 25px;" CommandArgument='<%#Eval("testId") %>' CommandName="Graph"
                                    ToolTip="Graph" ImageUrl="~/webadmin/images/img-graph-icon.gif" />
                                    &nbsp;
                                <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" AlternateText="Edit"
                                    Style="height: 25px; width: 25px;" CommandArgument='<%#Eval("testId") %>' CommandName="Edit"
                                    ToolTip="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                    &nbsp;
                                <asp:ImageButton ID="iBtnDrill" runat="server" CausesValidation="false" AlternateText="Graph"
                                    Style="height: 30px; width: 30px;" CommandArgument='<%#Eval("testId") %>' CommandName="drilldown"
                                    ToolTip="drilldown" ImageUrl="~/webadmin/images/drilldown.png" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="4"></td>
            </tr>
        </table>
        <br />
         <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
            <tr>
                <td colspan="4">
                    <b>Commercial Awareness Assessment</b>
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="4"></td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="4">
                    <asp:DataGrid ID="grdCAA" runat="server" AllowPaging="false" AllowSorting="false"
                        AutoGenerateColumns="False" CssClass="grdMain" OnItemDataBound="grdCAA_ItemDataBound"
                        OnItemCommand="grdCAA_ItemCommand" Width="100%" GridLines="None" PageSize="20">
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle Mode="NumericPages" CssClass="pager" HorizontalAlign="Center" />
                        <ItemStyle CssClass="gridItem" />
                        <Columns>
                            <asp:BoundColumn DataField="name" ItemStyle-Width="18%" HeaderText="Name" HeaderStyle-Width="18%"
                                SortExpression="name"></asp:BoundColumn>
                            <asp:TemplateColumn ItemStyle-Width="10%" HeaderStyle-Width="10%" SortExpression="Marks"
                                HeaderText="Marks">
                                <ItemTemplate>
                                    <%#Eval("marks")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-Width="20%" HeaderStyle-Width="20%" SortExpression="testdate"
                                HeaderText="Assesment Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssesmentDate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-Width="24%" HeaderStyle-Width="24%" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center" HeaderText="Action">
                                <ItemTemplate>
                                    <asp:ImageButton ID="iBtnDelete" runat="server" CausesValidation="false" AlternateText="Delete"
                                        Style="height: 25px; width: 25px;" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                        CommandArgument='<%#Eval("testId") %>' CommandName="Delete" ToolTip="Delete"
                                        ImageUrl="~/webadmin/images/disapprove_icon.png" />
                                  
                                    &nbsp;
                                <asp:ImageButton ID="iBtnGraph" runat="server" CausesValidation="false" AlternateText="Graph"
                                    Style="height: 25px; width: 25px;" CommandArgument='<%#Eval("testId") %>' CommandName="Graph"
                                    ToolTip="Graph" ImageUrl="~/webadmin/images/img-graph-icon.gif" />
                                    &nbsp;
                                <asp:ImageButton ID="iBtnEdit" runat="server" CausesValidation="false" AlternateText="Edit"
                                    Style="height: 25px; width: 25px;" CommandArgument='<%#Eval("testId") %>' CommandName="Edit"
                                    ToolTip="Edit" ImageUrl="~/webadmin/images/edit.png" />
                                    &nbsp;
                                <asp:ImageButton ID="iBtnDrill" runat="server" CausesValidation="false" AlternateText="Graph"
                                    Style="height: 30px; width: 30px;" CommandArgument='<%#Eval("testId") %>' CommandName="drilldown"
                                    ToolTip="drilldown" ImageUrl="~/webadmin/images/drilldown.png" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="4"></td>
            </tr>
        </table>
        <br />
    </form>
</body>
</html>
