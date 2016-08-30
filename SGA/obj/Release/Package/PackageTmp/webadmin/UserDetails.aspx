﻿<%@ Page  Language="C#" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="SGA.webadmin.UserDetails" %>
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
            <td class="txtrht">
                First Name
            </td>
            <td>
                <asp:TextBox name="txtEditFname" ID="txtEditFname" runat="server" MaxLength="100" />
            </td>
            <td class="txtrht">
                Last Name
            </td>
            <td>
                <asp:TextBox name="txtEditLname" ID="txtEditLname" runat="server" MaxLength="100" />
            </td>
        </tr>
        <tr>
            <td class="txtrht">
                Your Organisation
            </td>
            <td>
                <asp:DropDownList ID="ddlEditAgency" CssClass="styled" runat="server">
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
            </td>
            <td class="txtrht">
                Email Address
            </td>
            <td>
                <asp:TextBox name="txtEditEmailAddress" ID="txtEditEmailAddress" ReadOnly="true"
                    runat="server" MaxLength="250" />
            </td>
        </tr>
        <tr>
            <td class="txtrht">
                Job Role
            </td>
            <td>
                <asp:DropDownList ID="ddlEditJobRole" CssClass="styled" runat="server">
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
            </td>
            <td class="txtrht">
                Job level
            </td>
            <td>
                <asp:DropDownList ID="ddlEditJobLevel" CssClass="styled" runat="server">
                    <asp:ListItem Value="0">- Level of role best described as -</asp:ListItem>
                    <asp:ListItem Value="1">Graduate</asp:ListItem>
                    <asp:ListItem Value="2">Officer</asp:ListItem>
                    <asp:ListItem Value="3">Advisor</asp:ListItem>
                    <asp:ListItem Value="4">Senior advisor</asp:ListItem>
                    <asp:ListItem Value="5">Operational Leader</asp:ListItem>
                    <asp:ListItem Value="6">Director</asp:ListItem>
                    <asp:ListItem Value="7">Executive Level</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="txtrht">
                Manager First name
            </td>
            <td>
                <asp:TextBox name="MEditfirstName" ID="MEditfirstName" runat="server" MaxLength="250" />
            </td>
            <td class="txtrht">
                Manager Last name
            </td>
            <td>
                <asp:TextBox name="MEditlastName" ID="MEditlastName" runat="server" MaxLength="250" />
            </td>
        </tr>
        <tr>
            <td class="txtrht">
                Manager Email
            </td>
            <td>
                <asp:TextBox name="MEditemail" ID="MEditemail" runat="server" MaxLength="250" />
            </td>
            <td class="txtrht">
                Division
            </td>
            <td>
                <asp:TextBox name="txtDivision" ID="txtDivision" runat="server" MaxLength="100" />
            </td>
        </tr>
        <tr>
            <td class="txtrht">
                Password
            </td>
            <td>
                <asp:TextBox name="txtEditPassword" ID="txtEditPassword" runat="server" MaxLength="20" />
            </td>
            <td class="txtrht">
                Phone
            </td>
            <td>
                <asp:TextBox name="txtEditPhone" ID="txtEditPhone" runat="server" MaxLength="100" />
            </td>
        </tr>
        <tr>
            <td class="txtrht">
                Location
            </td>
            <td>
                <asp:DropDownList ID="ddlEditLocation" class="styled" runat="server">
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
                </asp:DropDownList>
            </td>
            <td class="txtrht">
                Position
            </td>
            <td>
                <input type="text" id="txtEditPosition" name="txtEditPosition" title="Position" maxlength="250"
                    runat="server" class="text-box-2" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Nature of the goods/services that you most commonly procure, or manage contracts
                for?
            </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlEditGoods" class="styled" runat="server">
                    <asp:ListItem Value="0">Nature of the goods/services that you most commonly procure, or manage contracts for?</asp:ListItem>
                    <asp:ListItem Value="1">Building Construction and Maintenance</asp:ListItem>
                    <asp:ListItem Value="2">General Goods and Services</asp:ListItem>
                    <asp:ListItem Value="3">ICT</asp:ListItem>
                    <asp:ListItem Value="4">Medical </asp:ListItem>
                    <asp:ListItem Value="5">Social Services</asp:ListItem>
                    <asp:ListItem Value="6">Transport Infrastructure & Services</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Expiry Date
            </td>
            <td colspan="2" class="ssec">
                <asp:TextBox ID="txtEditExiryDate" runat="server" />
                <asp:ImageButton ID="ImageButton12" runat="server" Height="16px" ImageUrl="~/Images/cal.gif"
                    Width="16px" ImageAlign="Bottom" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server" PopupButtonID="ImageButton7"
                    TargetControlID="txtEditExiryDate" Format="dd-MM-yyyy">
                </ajaxToolkit:CalendarExtender>
            </td>
            <td colspan="3">
                Status:&nbsp;
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
            <td style="height: 10px" colspan="4">
            </td>
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
                        <asp:TemplateColumn HeaderText="View CMC Result" Visible="false" ItemStyle-Width="10%"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <input type="checkbox" id="chkSga" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("sgaResult") %>' />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="View Procurement Result" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <input type="checkbox" id="chkTna" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("tnaResult") %>' />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="View Leadership Result" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <input type="checkbox" id="chkPmp" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("pmpResult") %>' />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="View NP Result" Visible="true" ItemStyle-Width="10%"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <input type="checkbox" id="chkNP" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("npResult") %>' />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="View MP Result" Visible="false" ItemStyle-Width="10%"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <input type="checkbox" id="chkDmp" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("dmpResult") %>' />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="View CMA Result" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <input type="checkbox" id="chkCMA" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("cmaResult") %>' />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="CMC Test" Visible="false" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <input type="checkbox" id="chkSgatest" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("takeSGT") %>' />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Procurement Test" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <input type="checkbox" id="chkTnatest" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("takeTNA") %>' />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Leadership Test" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <input type="checkbox" id="chkPmptest" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("takePMP") %>' />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="NP Test" Visible="true" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <input type="checkbox" id="chkNPtest" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("takeNP") %>' />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MP Test" Visible="false" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <input type="checkbox" id="chkDmptest" runat="server" value='<%#Eval("Id") %>' checked='<%#Eval("takeDMP") %>' />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="CMA Test" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
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
            <td style="height: 10px" colspan="4">
            </td>
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
            <td style="height: 10px" colspan="4">
            </td>
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
                                &nbsp; <a target="_blank" href="ShowSSAPdf.aspx?id=<%#Eval("emailLink") %>">
                                    <img src="../innerimages/icon-pdf.gif" style="height: 25px; width: 25px;" alt="" /></a>
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
            <td style="height: 10px" colspan="4">
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
        <tr>
            <td colspan="6">
                <b>Leadership Assessment </b>
            </td>
        </tr>
        <tr>
            <td style="height: 10px" colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:DataGrid ID="grdBA" runat="server" AllowPaging="false" AllowSorting="false"
                    AutoGenerateColumns="False" CssClass="grdMain" OnItemDataBound="grdBA_ItemDataBound"
                    OnItemCommand="grdBA_ItemCommand" Width="100%" GridLines="None" PageSize="20">
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
                                &nbsp; <a target="_blank" href="ShowBAPdf.aspx?id=<%#Eval("emailLink") %>">
                                    <img src="../innerimages/icon-pdf.gif" style="height: 25px; width: 25px;" alt="" /></a>
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
            <td style="height: 10px" colspan="4">
            </td>
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
            <td style="height: 10px" colspan="4">
            </td>
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
                                &nbsp; <a target="_blank" href="ShowCMAPdf.aspx?id=<%#Eval("emailLink") %>">
                                    <img src="../innerimages/icon-pdf.gif" style="height: 25px; width: 25px;" alt="" /></a>&nbsp;
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
            <td style="height: 10px" colspan="4">
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellspacing="1" cellpadding="1" class="tform">
        <tr>
            <td colspan="4">
                <b>Negotiation Profile Assessment</b>
            </td>
        </tr>
        <tr>
            <td style="height: 10px" colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:DataGrid ID="grdNP" runat="server" AllowPaging="false" AllowSorting="false"
                                                AutoGenerateColumns="False" CssClass="grdMain" 
                                                OnItemDataBound="grdNP_ItemDataBound" OnItemCommand="grdNP_ItemCommand" 
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
        <tr>
            <td style="height: 10px" colspan="4">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>