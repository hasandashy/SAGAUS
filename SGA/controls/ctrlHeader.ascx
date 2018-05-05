<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlHeader.ascx.cs" Inherits="SGA.controls.ctrlHeader" %>
<!-- Header start -->
<header>
	<!-- Logo start -->
	<div id="logo">
		<a href="#" title="skillsGAP - Analysis">skillsGAP - Analysis</a>
	</div>
	<!-- Logo end // -->
	<!-- Top Navigation start -->
    <asp:Panel ID="pnlTopMenu" runat="server">
	<nav>
		<ul>
			<li><a href="/tna/default.aspx">Home</a></li>
			<li><a href="/tna/MyProfile.aspx">My Profile</a></li>
            <li><a href="/tna/my-cma-reports.aspx">My Results</a></li>
			<li><a href="/tna/help.aspx">Help</a></li>
			<li><asp:LinkButton ID="lnkLogout" runat="server" CausesValidation="false" 
                  Text="Log out" onclick="lnkLogout_Click"  ></asp:LinkButton></li>
		</ul>
	</nav>
    </asp:Panel>
	<!-- Top Navigation end // -->
</header>
<!-- Header end // -->