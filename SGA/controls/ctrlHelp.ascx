<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlHelp.ascx.cs" Inherits="SGA.controls.ctrlHelp" %>

<article id="container">
    <div class="col-660">
        <section class="cnt-rt-2 equal_height">
            <div class="form" style="width: 95%; background: none;">
                <h6>Please complete your enquiry below.</h6>
                <ul>
                    <li><span>*</span><input type="text" value="First name" class="txt-field2" id="fname" maxlength="250" runat="server" name="fname" /></li>
                    <li><span>*</span><input type="text" value="Last name" class="txt-field2" id="lname" maxlength="250" runat="server" name="lname" /></li>
                    <li><span>*</span><input type="text" value="Email address" class="txt-field2" id="email" runat="server" maxlength="250" name="email" /></li>
                    <li><span>*</span><asp:DropDownList ID="company" runat="server">
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
                        <asp:ListItem Value="17">Department for Child Protection</asp:ListItem>
                        <asp:ListItem Value="18">SA Water</asp:ListItem>
                    </asp:DropDownList></li>
                    <li><strong>What is the primary nature of enquiry?</strong></li>
                    <li>
                        <input type="radio" value="1" name="access" class="access" />
                        Navigating or logging into the website
                        <br />
                        <asp:DropDownList ID="ddlNavigating" runat="server" disabled="disabled" CssClass="helpSelect">
                            <asp:ListItem Value="0"> Please select </asp:ListItem>
                            <asp:ListItem Value="1">Navigation and accessibility</asp:ListItem>
                            <asp:ListItem Value="2">User registration</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <%--  <li>
                                <input type="radio" value="2" name="access" class="access"/>
                            	Workshop event enquiries and bookings <br/>
                                <asp:DropDownList ID="ddlWorkshopEvent" runat="server" disabled="disabled" cssclass="helpSelect">
                            <asp:ListItem Value="0"> Please select </asp:ListItem>
                            <asp:ListItem Value="1">Event inquiries and booking</asp:ListItem> 
<asp:ListItem Value="2">Registration process</asp:ListItem>
<asp:ListItem Value="3">Event Information</asp:ListItem>
<asp:ListItem Value="4">Post event</asp:ListItem> 
<asp:ListItem Value="5">General</asp:ListItem>
                            </asp:DropDownList>
                            </li>--%>
                    <li>
                        <input type="radio" value="3" name="access" class="access" />
                        Taking the assessments and accessing my bar-graph results
                        <br />
                        <asp:DropDownList ID="ddlTna" runat="server" disabled="disabled" CssClass="helpSelect">
                            <asp:ListItem Value="0"> Please select </asp:ListItem>
                            <asp:ListItem Value="1">Registration, username and password</asp:ListItem>
                            <asp:ListItem Value="2">Assessments</asp:ListItem>
                            <asp:ListItem Value="3">Results</asp:ListItem>
                            <asp:ListItem Value="4">Reporting</asp:ListItem>
                            <asp:ListItem Value="5">General</asp:ListItem>
                        </asp:DropDownList>

                    </li>
                    <%-- <li>
                            <input type="radio" value="4" name="access" class="access" />
                            
                            	 Accessing the e-Learning portal <br/>
                                <asp:DropDownList ID="ddlAccess" runat="server" disabled="disabled" cssclass="helpSelect">
                            <asp:ListItem Value="0"> Please select </asp:ListItem>
                            <asp:ListItem Value="1">Registration, username and password</asp:ListItem> 
<asp:ListItem Value="2">Learning plan</asp:ListItem>
<asp:ListItem Value="3">Reporting</asp:ListItem>
<asp:ListItem Value="4">General</asp:ListItem> 
                            </asp:DropDownList>
                            </li>
                            <%--<li>
                            	<input type="radio" value="5" name="access" class="access" /> Other <br/>
                                 
	  
                            </li>--%>


                    <li>
                        <strong>Please provide addtional supporting comments.</strong><br />
                        <textarea id="comments" runat="server" class="helpTextarea"></textarea>
                    </li>
                    <li class="txtRgt">
                        <input type="image" id="btnSend" src="/Images/icoSend.png" /></li>
                </ul>


            </div>
            <p>&nbsp;</p>
        </section>
    </div>
    <div class="col-340">
        <section class="cnt-lt-2 equal_height">
            <p>&nbsp;</p>
            <p>Technical support for the Skills for Procurement – Assess and Build project is available between the hours of 8.30am and 5.00pm CST, Monday to Friday.</p>
            <p>&nbsp;</p>

        </section>
    </div>
    <div class="clear"></div>

    <p class="hide">&nbsp;</p>
    <p>&nbsp;</p>
</article>
<script type="text/javascript" language="javascript">
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var loginHtml = '';


    $(document).ready(function () {
        $("input[name='access']").click(function () {
            var access = $("input[name='access']:checked").val();
            $('#<%=ddlNavigating.ClientID %>').attr("disabled", "disabled");
            $('#<%=ddlTna.ClientID %>').attr("disabled", "disabled");


            switch (parseInt(access)) {
                case 1:
                    $('#<%=ddlNavigating.ClientID %>').removeAttr("disabled");
                    break;
                case 3:
                    $('#<%=ddlTna.ClientID %>').removeAttr("disabled");
                    break;
            }
        });
        /*$("input[name='access']").on('change', function () {
        var form = document.getElementById("access");
        alert(form.elements["access"].value);
        });*/

        $('#btnSend').colorbox({
            href: "/Popup.aspx",
            width: "492px",
            height: "300px",
            onComplete: function () {
                if (loginHtml == 's') {
                    $('#title').text("Success");
                    $('#alertMessage').text('Thanks for contacting us, we will contact you very soon.');
                } else {
                    $('#title').text("Unsuccessful");
                    $('#alertMessage').text(loginHtml);
                }
            }
        });

        $('#btnSend').click(function () {

            var error = 0;
            var emptyFields = new Array();
            var fname = $('#<%=fname.ClientID %>').val();
            var lname = $('#<%=lname.ClientID %>').val();
            var email = $('#<%=email.ClientID %>').val();
            var company = $('#<%=company.ClientID %>').val();
            var access = $("input[name='access']:checked").val();
            var query = "";
            if (access == '' || access == null) {
                error = 1;
                emptyFields.push('What is the primary nature of enquiry?');
            } else {
                switch (parseInt(access)) {
                    case 1:
                        query = $('#<%=ddlNavigating.ClientID %>').val();
                        break;
                    case 3:
                        query = $('#<%=ddlTna.ClientID %>').val();
                        break;

                }
            }



            if (fname == '' || fname == 'First name') {
                error = 1;
                emptyFields.push('First name');
            }

            if (lname == '' || lname == 'Last name') {
                error = 1;
                emptyFields.push('Last name');
            }

            if (email == '' || email == 'Email address') {
                error = 1;
                emptyFields.push('Email address');
            }

            if (company == '' || company == '0') {
                error = 1;
                emptyFields.push('Organisation');
            }
            else {
                company = $('#<%=company.ClientID %>').text();
            }



            if (error) {
                $('#colorbox').css({ "display": "block" });
                loginHtml = 'Please enter/select ' + emptyFields.join(', ');
            }
            else if (email != '' && !filter.test(email)) {
                $('#colorbox').css({ "display": "block" });
                alertHtml = 'Please enter valid email id';
            }
            else {
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "/About_Us.aspx/Help",
                    data: JSON.stringify({ 'firstName': fname, 'lastName': lname, 'email': email, 'department': company, 'primaryEnquirytype': access, 'query': query, 'comments': $('#<%=comments.ClientID %>').val() }),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {

                        if (data.d == 's') {
                            $('#colorbox').css({ "display": "block" });
                            loginHtml = 's';
                        } else {
                            $('#colorbox').css({ "display": "block" });
                            loginHtml = 'There was some problem with the form and its not submitted, please try again!';
                        }
                    }
                });
            }
        });

    });
</script>
