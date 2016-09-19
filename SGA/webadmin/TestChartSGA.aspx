<%@ Page Title="" Language="C#" MasterPageFile="~/webadmin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="TestChartSGA.aspx.cs" Inherits="SGA.webadmin.TestChartSGA" %>
<%@ Register Src="~/controls/ctrlCMCGraph.ascx" TagName="cmcGraph" TagPrefix="sga" %>
<%@ Register TagName="left" TagPrefix="sga" Src="~/controls/ctrlLeftMenu.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                                    Procurement Knowledge Evaluation Report Graph
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <sga:cmcGraph id="graph1" runat="server"></sga:cmcGraph>
                                                    </td>
                                                </tr>
                                            </table>
 
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</asp:Content>
