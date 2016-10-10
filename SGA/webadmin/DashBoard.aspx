<%@ Page  Language="C#" MasterPageFile="~/webadmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="SGA.webadmin.DashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <tr>
        <td>
            <table width="1280" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="cat">
                        <h1 style="font-weight: normal">
                            > MEMBERS</h1>
                        <div style="border: 1px solid #ed1165; margin-left: 30px; width: 98%;">
                        </div>
                        <br />
                        <ul>
                            <li><a href="ListUsers.aspx">
                                <div class="fl num_dashboard">
                                    <asp:Label ID="lblRegistered" runat="server"></asp:Label></div>
                                <div class="fl hdtxt pt15">
                                    Registered<br />
                                    User</div>
                                <div class="clr">
                                </div>
                            </a></li>
                            <li><a href="ListUsers.aspx?id=1">
                                <div class="fl num_dashboard">
                                    <asp:Label ID="lblUnApproved" runat="server"></asp:Label></div>
                                <div class="fl hdtxt pt15">
                                    UnApproved<br />
                                    User</div>
                                <div class="clr">
                                </div>
                            </a></li>
                            <li><a href="ListUsers.aspx?id=4">
                                <div class="fl num_dashboard">
                                    <asp:Label ID="lblDeactive" runat="server"></asp:Label></div>
                                <div class="fl hdtxt pt15">
                                    Expired<br />
                                    User</div>
                                <div class="clr">
                                </div>
                            </a></li>
                            <li><a href="ListUsers.aspx?id=5">
                                <div class="fl num_dashboard">
                                    <asp:Label ID="lblTotalUserByAdmin" runat="server"></asp:Label></div>
                                <div class="fl hdtxt pt15">
                                    Total user<br />
                                    created by admin</div>
                                <div class="clr">
                                </div>
                            </a></li>
                            <li><a href="ListUsers.aspx?id=6">
                                <div class="fl num_dashboard">
                                    <asp:Label ID="lblFromFront" runat="server"></asp:Label></div>
                                <div class="fl hdtxt pt15">
                                    Total user<br />
                                    registered by<br />
                                    front site</div>
                                <div class="clr">
                                </div>
                            </a></li>
                        </ul>
                        <br />
                    </td>
                </tr>
                <tr>
                <td class="cat">
                    <h1 style="font-weight: normal">
                        > ASSESSMENTS</h1>
                    <div style="border: 1px solid #ed1165; margin-left: 30px; width: 98%;">
                    </div>
                    <br />
                    <ul>  
                        <li><a href="ListUsers.aspx?tabId=3">
                            <div class="fl num_dashboard">
                                <asp:Label ID="lblSSA" runat="server"></asp:Label></div>
                            <div class="fl hdtxt pt15">
                               Procurement Skills Self Assessment taken</div>
                            <div class="clr">
                            </div>
                        </a></li>
                         <li><a href="ListUsers.aspx?tabId=4">
                            <div class="fl num_dashboard">
                                <asp:Label ID="lblSga" runat="server"></asp:Label></div>
                            <div class="fl hdtxt pt15">
                               Procurement Knowledge Evaluation taken</div>
                            <div class="clr">
                            </div>
                        </a></li>
                        <li><a href="ListUsers.aspx?tabId=6">
                            <div class="fl num_dashboard">
                                <asp:Label ID="lblCMA" runat="server"></asp:Label></div>
                            <div class="fl hdtxt pt15">
                                Contract Management Self Assessment taken</div>
                            <div class="clr">
                            </div>
                        </a></li>
                         <li><a href="ListUsers.aspx?tabId=5">
                            <div class="fl num_dashboard">
                                <asp:Label ID="lblCmk" runat="server"></asp:Label></div>
                            <div class="fl hdtxt pt15">
                                Contract Management Knowledge Evaluation taken</div>
                            <div class="clr">
                            </div>
                        </a></li>
                          <li><a href="ListUsers.aspx?tabId=7">
                            <div class="fl num_dashboard">
                                <asp:Label ID="lblCAA" runat="server"></asp:Label></div>
                            <div class="fl hdtxt pt15">
                                Commercial Awareness Assessment taken</div>
                            <div class="clr">
                            </div>
                        </a></li>
                       
                    </ul>
                    <br />
                </td>
                </tr>
                <tr>
                    <td class="cat">
                        <h1 style="font-weight: normal">
                            > OPPORTUNITIES</h1>
                        <div style="border: 1px solid #ed1165; margin-left: 30px; width: 98%;">
                        </div>
                        <br />
                        <ul>
                            <li><a href="AllContactUs.aspx">
                                <div class="fl num_dashboard">
                                    <asp:Label ID="lblContact" runat="server"></asp:Label></div>
                                <div class="fl hdtxt pt15">
                                    Total Standing <br />
                                    offer arrangement
                                </div>
                                <div class="clr">
                                </div>
                            </a></li>
                            <li><a href="ManageEmailTemplates.aspx">
                                <div class="fl num_dashboard">
                                    <asp:Label ID="lblEmailTemplate" runat="server"></asp:Label></div>
                                <div class="fl hdtxt pt15">
                                    Total email
                                    <br />
                                    templates</div>
                                <div class="clr">
                                </div>
                            </a></li>
                            
                            <li><a href="ListUsers.aspx?id=3">
                                <div class="fl num_dashboard">
                                    <asp:Label ID="lblLoggedin" runat="server"></asp:Label></div>
                                <div class="fl hdtxt pt15">
                                    Total loggedin<br />
                                    users (till date)
                                </div>
                                <div class="clr">
                                </div>
                            </a></li>
                            <li><a href="ListUsers.aspx?id=2">
                                <div class="fl num_dashboard">
                                    <asp:Label ID="lblNotLoggenin" runat="server"></asp:Label></div>
                                <div class="fl hdtxt pt15">
                                    Total users not<br />
                                    loggedin till<br />
                                    today</div>
                                <div class="clr">
                                </div>
                            </a></li>
                            <li><a href="ShareChallenge.aspx">
                                <div class="fl num_dashboard">
                                    <asp:Label ID="lblChallenge" runat="server"></asp:Label></div>
                                <div class="fl hdtxt pt15">
                                    Total Challenge<br />
                                    Shared</div>
                                <div class="clr">
                                </div>
                            </a></li>
                        </ul>
                        <br />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</asp:Content>
