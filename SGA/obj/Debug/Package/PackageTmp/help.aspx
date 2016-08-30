<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="help.aspx.cs" Inherits="SGA.help" %>
<%@ Register TagName="help" TagPrefix="sga" Src="~/controls/ctrlHelp.ascx"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<!-- Banner start -->
				
				<!-- Banner end // -->
				<div class="dot-line1 no-mrg">&nbsp;</div>
                <%--<p class="title28">Need Help</p>
                <p>Technical support: If your issue is technically related please complete the form below. This may include such things as password changes or difficulties with logging into the site.</p>
                --%>
				<!-- Content Area start -->
                <%--<article id="container">
					<div class="col-660">
						<section class="cnt-lt-2 equal_height" style="height: 334px;">
							<p class="title28">Need Help</p>
							<p>Support for the Critical Skills Boost Program is available between the hours of 9.00am and 5.00pm, Monday to Friday.</p>
							<p>&nbsp;</p>
							<p>Enquiries of a general nature should be sent to <a href="mailto:skills2procure@hpw.qld.gov.au">skills2procure@hpw.qld.gov.au</a></p>
							<p>&nbsp;</p>
							<p>If your issue is technically related please email Comprara at <a href="mailto:support@comprara.com.au">support@comprara.com.au</a></p>
							
							<p>This may include such things as password changes or difficulties with logging into the site.</p>
							<p>&nbsp;</p>
							<p>If you are in anyway unsure, please direct your query to the Skills2Procure team.</p>
							
							<p>&nbsp;</p>
						</section>
					</div>
					<div class="col-340">
						<section class="cnt-rt-2 equal_height" style="height: 334px;">
							<p class="title28">Key contacts</p>
                            <p>&nbsp;</p>
                            <p><b>PROGRAM ENQUIRIES &gt;</b></p>
                           <p>&nbsp;</p>
                            <p><a href="mailto:skills2procure@hpw.qld.gov.au">skills2procure@hpw.qld.gov.au</a></p>
                            <p>&nbsp;</p>
                            <p><b>TECHNICAL ENQUIRIES &gt;</b></p>
                            <p>&nbsp;</p>
                            <p><a href="mailto:support@comprara.com.au">support@comprara.com.au</a> </p>
							<p></p>
						</section>
					</div>
					<div class="clear"></div>
					
					<p class="hide">&nbsp;</p>
					<p>&nbsp;</p>
				</article>--%>
                <sga:help id="help1" runat="server"></sga:help>
				<!-- user control here -->
				<!-- Content Area end // -->
<script>
    $(document).ready(function () {
        $(".iphonNav ul li").removeClass("active");
        //alert($(".iphonNav ul li").eq(4));
        $(".iphonNav ul li:eq(20)").addClass("active");
    });
</script>
</asp:Content>

