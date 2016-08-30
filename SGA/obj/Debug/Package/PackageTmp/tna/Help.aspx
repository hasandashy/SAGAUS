<%@ Page Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="SGA.tna.Help" %>
<%@ Register TagName="help" TagPrefix="sga" Src="~/controls/ctrlHelp.ascx"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!-- Custom Form -->
		<script type="text/javascript" src="../js/custom-form-elements.js"></script>
		
		<!-- Accordion Menu -->
		<script type="text/javascript" src="../js/jquery.min.js"></script>
		<!-- Popup -->
		<script type="text/javascript" src="../Scripts/jquery.colorbox.js"></script>
		<script type="text/javascript" src="../js/custom.js"></script>
<!-- Banner start -->
<style>
#container p{
	margin-bottom:8px;
}
.formArea h2 {
	color: #ea4320;
	font-family: "cleargothic-mediumregular", Arial, Helvetica, sans-serif;
	font-size: 36px;
	line-height: 25px;
	margin-bottom: 20px;
	text-align: center;
}
.form {
    float: left;
    width: 63%;
    height: auto;
    padding: 20px 40px 20px 20px;
    background: #e5e1d6;
}
.form h6{
	font-size:18px;
	color:#231f20;
	margin-bottom:10px;
}
.form > strong{
	margin-bottom:10px;
	display:block;
}
.form ul {
	float: left;
	width: 100%;
}
.form ul li {
	float: left;
	width: 100%;
	padding-bottom: 10px;
	font-size:14px;
}
.form ul li span {
	color: red;
	float: left;
	width: 10px;
}
.form ul li input[type=text], .form ul li select {
	float: left;
	width: 95%;
	font-size: 12px;
	border-radius: 5px;
	padding: 5px;
}
.form ul li select {
	float: left;
	width: 97%;
}
</style>
<script>
    if (document.documentElement.clientWidth >= 768) {
        $(window).load(function () {
            //$(document).ready(function(){
            //set the starting bigestHeight variable
            var biggestHeight = 0;
            //check each of them
            $('.equal_height').each(function () {
                //if the height of the current element is
                //bigger then the current biggestHeight value
                if ($(this).height() > biggestHeight) {
                    //update the biggestHeight with the
                    //height of the current elements
                    biggestHeight = $(this).height();
                }
            });
            //when checking for biggestHeight is done set that
            //height to all the elements
            $('.equal_height').height(biggestHeight);
        });
    }
</script>
				
				<!-- Banner end // -->
				<div class="dot-line1 no-mrg">&nbsp;</div>
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
</asp:Content>

