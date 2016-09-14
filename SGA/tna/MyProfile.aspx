<%@ Page Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="SGA.tna.MyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Custom Form -->
    <script type="text/javascript" src="../js/custom-form-elements.js"></script>

    <!-- Accordion Menu -->
    <script type="text/javascript" src="../js/jquery.min.js"></script>
    <!-- Popup -->
    <script type="text/javascript" src="../Scripts/jquery.colorbox.js"></script>
    <script type="text/javascript" src="../js/custom.js"></script>
    <script type="text/javascript" language="javascript">


        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        var alertHtml = '';
        var lastpage = 'n';
        var redirect = 'y';
        function gup(name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }
        function FinalSubmit() {
            var backUrl = gup("id");
            if (backUrl == "1") {
                window.location.href = '/tna/procurement-knowledge-evaluation-test.aspx';
            } else if (backUrl == "2") {
                window.location.href = '/tna/skills-self-test.aspx';
            } else if (backUrl == "3") {
                window.location.href = '/tna/contract-management-assessment-test.aspx';
            } else if (backUrl == "4") {
                window.location.href = '/tna/department-maturity-test.aspx';
            } else if (backUrl == "5") {
                window.location.href = '/tna/cmk-relationsip-context.aspx';
            }
            else {
                window.location.href = '/tna/default.aspx';
            }
        }

        function sentBack() {

            //window.location.href = '/tna/default.aspx';
        }

        function setPassword(parameters) {
            $('#<%=password.ClientID %>').val(parameters);
            $('#<%=passwordplain.ClientID %>').val(parameters);

        }

        $(document).ready(function () {
            $('#edit').click(function () {
                $('#<%=password.ClientID %>').removeAttr("disabled");
            $('#<%=password.ClientID %>').hide();
            $('#<%=passwordplain.ClientID %>').show();
        })

        var backUrl = gup("id");
        $('#submitbutton,#submitbuttonnext').colorbox({
            href: "../Popup.aspx",
            width: "392px",
            height: "450px",
            onComplete: function () {
                if (lastpage == 'y') {

                   if (($("#<%= txtPhoneNo.ClientID%>").val() == 'Phone') && ($("#<%= txtBranch.ClientID%>").val() == 'branch') && ($("#<%=txtJobTitle.ClientID %>").val() == 'jobTitle') && ($("#<%=ddlAgency.ClientID %>").val() == 0) && ($("#<%=ddlCentral.ClientID %>").val() == 0) && ($("#<%=ddlContract.ClientID %>").val() == 0) && ($("#<%=ddlCurrentJobClassification.ClientID %>").val() == 0) && ($("#<%=ddlExperience.ClientID %>").val() == 0) && ($("#<%=ddlInfluence.ClientID %>").val() == 0) && ($("#<%=ddlNature.ClientID %>").val() == 0) && ($("#<%=ddlQualification.ClientID %>").val() == 0) && ($("#<%=ddlRange.ClientID %>").val() == 0) && ($("#<%=ddlTimeAlloc.ClientID %>").val() == 0)) {
                        redirect = "y";
                        $('#title').text("Confirmation");
                        $('#colorbox').css({ "display": "block" });
                        $('#alertMessage').html("We noticed you haven't completed all of the fields. Providing this information helps us to support your learning. <b>If you are unsure how to answer, then please choose the answer you think is the 'closest fit' for you. Would you like to complete the rest of the fields, or proceed to your training needs analysis?</b> ");
                        $('#btnCancel').css("display", "block");
                        $('#btnOk').removeClass("btn-yes");
                        $('#btnOk').addClass("btn-back");

                        $('#btnCancel').removeClass("btn-back");
                        $('#btnCancel').addClass("btn-proceed");
                    } else {
                        $('#colorbox').css({ "display": "none" });
                        window.location.href = '/tna/default.aspx';
                    }



                }
                else {
                    redirect = "n";
                    $('#colorbox').css({ "display": "block" });
                    $('#alertMessage').html(alertHtml);
                }

            }
        });
        $('#submitbutton,#submitbuttonnext').click(function () {
            var error = 0;
            var emptyFields = new Array();
            var password = $('#<%=passwordplain.ClientID %>').val();

            var name = $('#<%=fname.ClientID %>').val();
            if (name == '' || name == 'First name') {
                error = 1;
                emptyFields.push('First name');
            }
            var surname = $('#<%=lname.ClientID %>').val();
            if (surname == '' || surname == 'Last name') {
                error = 1;
                emptyFields.push('Last name');
            }

         <%--   var agency = $("#<%=ddlAgency.ClientID %>").val();
            if (agency == 0) {
                error = 1;
                emptyFields.push('Your Organisation');
            }--%>

            if (password == '') {
                error = 1;
                emptyFields.push('Password');
            }




          
            if (error) {
                $('#colorbox').css({ "display": "block" });
                alertHtml = 'Please select/enter ' + emptyFields.join(', ');
            }
            else {
                alertHtml = 's';
                lastpage = 'y';
                var emailpass = 0;



                var json =
                    $.ajax({
                        type: "POST",
                        async: false,
                        url: "MyProfile.aspx/UpdateProfile",
                        data: JSON.stringify({ 'fname': name, 'lname': surname, 'phone': $("#<%= txtPhoneNo.ClientID%>").val(), 'password': password,'department': $("#<%= ddlAgency.ClientID%>").val(),'central': $("#<%= ddlCentral.ClientID%>").val(),'classification': $("#<%= ddlCurrentJobClassification.ClientID%>").val(),'experience': $("#<%= ddlExperience.ClientID%>").val(),'qualification': $("#<%= ddlQualification.ClientID%>").val(),'time': $("#<%= ddlTimeAlloc.ClientID%>").val(),'nature': $("#<%= ddlNature.ClientID%>").val(),'size': $("#<%= ddlInfluence.ClientID%>").val(),'noOfContracts': $("#<%= ddlContract.ClientID%>").val(),'activities': $("#<%= ddlRange.ClientID%>").val(),'branch': $("#<%= txtBranch.ClientID%>").val(),'jobTitle': $("#<%= txtJobTitle.ClientID%>").val() }),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d == 's') {
                                alertHtml = 's';
                                lastpage = 'y';
                            }
                        }
                    });

                }
        });
    });
    </script>
    <article id="container">
        <section class="welcome-inner">
            <p class="title40-orange">
                <asp:Label ID="lblName" runat="server"></asp:Label>
            </p>
            <p class="title40">Here is your profile</p>
        </section>
        <section class="color-box">
            <article class="info-box-shdw-inner">
                <div class="my-pofile-form">
                    <p class="txt28 orange">We need a little more information before you begin!</p>
                    <p>&nbsp;</p>
                    <p>Providing this information helps us to support your learning. If you are unsure how to answer, then please choose the answer you think is the 'closest fit' for you. </p>
                    <p>&nbsp;</p>
                    <p class="txt18-bold">REGISTRATION DETAILS</p>
                    <span class="error"></span>&nbsp;&nbsp;<b>First Name</b><br />
                    <span class="error">*</span>&nbsp;<input type="text" id="fname" name="fname" maxlength="100" runat="server" class="text-box-2" />
                    <p>&nbsp;</p>
                    <span class="error"></span>&nbsp;&nbsp;<b>Last Name</b><br />
                    <span class="error">*</span>&nbsp;<input type="text" id="lname" name="lname" maxlength="100" runat="server" class="text-box-2" />
                    <p>&nbsp;</p>
                    <span class="error"></span>&nbsp;&nbsp;<b>Your Job Role</b><asp:HyperLink ID="hylJobRole" CssClass="infolock" runat="server"><span>If you require your Role or your Level to be changed please contact support@comprara.com.au before continuing. In your email, please let us know the reason why you require the change.</span></asp:HyperLink><br />
                    <div class="form-box1">
                        <span class="error"></span>&nbsp;&nbsp;<asp:DropDownList ID="ddlJobRole" class="styled" runat="server">
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

                    </div>                  

                    <p>&nbsp;</p>
                    <p class="txt18-bold">LOGIN DETAILS</p>

                    <span class="error"></span>&nbsp;&nbsp;<b>Your Email</b><br />
                    <span class="error">*</span>&nbsp;<input type="text" id="email" name="email" disabled="disabled" readonly="readonly" maxlength="250" runat="server" class="text-box-2" />
                    <p>&nbsp;</p>
                    <span class="error"></span>&nbsp;&nbsp;<a href="javascript:void('0')" id="edit">Edit Password</a>
                    <br />
                    <span class="error">*</span>&nbsp;<input type="text" id="passwordplain" name="passwordplain" maxlength="20" runat="server" style="display: none" class="text-box-2" /><input type="password" id="password" name="password" disabled="disabled" maxlength="20" runat="server" class="text-box-2" />
                    <p>&nbsp;</p>
                    <span class="error"></span>&nbsp;&nbsp;<b>Phone</b><br />
                    <span class="error">*</span>&nbsp;<input type="text" id="txtPhoneNo" name="txtPhoneNo" maxlength="20" runat="server" class="text-box-2" />
                   

                    <p>&nbsp;</p>
                    <p class="txt18-bold">MY DETAILS</p>
                    <span class="error"></span>&nbsp;&nbsp;<b>Your Department</b><br />
                    <div class="form-box1">
                        <span class="error"></span>&nbsp;<asp:DropDownList ID="ddlAgency" class="styled" runat="server">
                            <asp:ListItem Value="0">Please Select--</asp:ListItem>
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

                    </div>
                    <span class="error"></span>&nbsp;&nbsp;<b>Your Branch</b><br />
                    <span class="error">&nbsp;</span>&nbsp;<input type="text" id="txtBranch" name="txtBranch" title="Division" maxlength="250" runat="server" class="text-box-2" />
                    <p>&nbsp;</p>
                    <span class="error"></span>&nbsp;&nbsp;<b>Youe actual job title(position)</b><br />
                    <span class="error">&nbsp;</span>&nbsp;<input type="text" id="txtJobTitle" name="txtJobTitle" title="Phone" maxlength="250" runat="server" class="text-box-2" />
                     <p>&nbsp;</p>
                     <span class="error"></span>&nbsp;&nbsp;<b>Are you part of the Central Procurement function? </b><br />
                    <div class="form-box1">
                        <span class="error"></span>&nbsp;<asp:DropDownList ID="ddlCentral" class="styled" runat="server">
                            <asp:ListItem Value="0">Please select ---</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No- Operational</asp:ListItem>
                            <asp:ListItem Value="3">No- Regional</asp:ListItem>    
                            <asp:ListItem Value="4">No- Other</asp:ListItem>                           
                        </asp:DropDownList>

                    </div>
                     <p>&nbsp;</p>
                     <span class="error"></span>&nbsp;&nbsp;<b>Your current job classification </b><br />
                    <div class="form-box1">
                        <span class="error"></span>&nbsp;<asp:DropDownList ID="ddlCurrentJobClassification" class="styled" runat="server">
                            <asp:ListItem Value="0">Please select ---</asp:ListItem>
                            <asp:ListItem Value="1">ASO2</asp:ListItem>
                            <asp:ListItem Value="2">ASO3</asp:ListItem>
                            <asp:ListItem Value="3">ASO4</asp:ListItem>
                            <asp:ListItem Value="4">ASO5</asp:ListItem>
                            <asp:ListItem Value="5">ASO6/ MAS1</asp:ListItem>
                            <asp:ListItem Value="6">ASO7/ MAS2</asp:ListItem>
                            <asp:ListItem Value="7">ASO8/ MAS3</asp:ListItem>
                            <asp:ListItem Value="8">EX</asp:ListItem>
                            <asp:ListItem Value="9">Other (e.g. technical grades)</asp:ListItem>
                            
                        </asp:DropDownList>

                    </div>
                     <p>&nbsp;</p>
                     <span class="error"></span>&nbsp;&nbsp;<b>Years of procurement/ contract management experience </b><br />
                    <div class="form-box1">
                        <span class="error"></span>&nbsp;<asp:DropDownList ID="ddlExperience" class="styled" runat="server">
                            <asp:ListItem Value="0">Please select ---</asp:ListItem>
                            <asp:ListItem Value="1">Less than 1 year</asp:ListItem>
                            <asp:ListItem Value="2">1 to 3 years</asp:ListItem>
                            <asp:ListItem Value="3">3 to 5 years</asp:ListItem>
                            <asp:ListItem Value="4">5 to 10 years</asp:ListItem>
                            <asp:ListItem Value="5">10 to 15 years</asp:ListItem>
                            <asp:ListItem Value="6">15 to 20 years</asp:ListItem>
                            <asp:ListItem Value="7">More than 20 years</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                       <p>&nbsp;</p>
                     <span class="error"></span>&nbsp;&nbsp;<b>Please select your highest level of qualification that relates to the field of procurement / contract management </b><br />
                    <div class="form-box1">
                        <span class="error"></span>&nbsp;<asp:DropDownList ID="ddlQualification" class="styled" runat="server">
                            <asp:ListItem Value="0">Please select ---</asp:ListItem>
                            <asp:ListItem Value="1">Not applicable</asp:ListItem>
                            <asp:ListItem Value="2">Certificate in Procurement / Contract Management</asp:ListItem>
                            <asp:ListItem Value="3">Diploma in Procurement / Contract Management</asp:ListItem>
                            <asp:ListItem Value="4">Undergraduate degree in Procurement / Contract Management</asp:ListItem>
                            <asp:ListItem Value="5">Postgraduate diploma in Procurement / Contract Management</asp:ListItem>
                            <asp:ListItem Value="6">Postgraduate degree in Procurement / Contract Management</asp:ListItem>
                            <asp:ListItem Value="7">CIPS: Member (MCIPS)</asp:ListItem>
                            <asp:ListItem Value="8">CIPS: Fellow (FCIPS)</asp:ListItem>
                            <asp:ListItem Value="9">AAPCM: Member</asp:ListItem>
                            <asp:ListItem Value="10">AAPCM: Fellow</asp:ListItem>
                            <asp:ListItem Value="11">IACCM: Accreditation</asp:ListItem>
                            <asp:ListItem Value="12">IFPSM: Certified International Procurement Professional</asp:ListItem>
                            <asp:ListItem Value="13">IFPSM: Certified International Advanced Procurement Professional</asp:ListItem>
                            <asp:ListItem Value="14">Other</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                    <p>&nbsp;</p>
                     <span class="error"></span>&nbsp;&nbsp;<b>How much of your time is allocated to Procurement and/or Contract Management activities </b><br />
                    <div class="form-box1">
                        <span class="error"></span>&nbsp;<asp:DropDownList ID="ddlTimeAlloc" class="styled" runat="server">
                            <asp:ListItem Value="0">Please select ---</asp:ListItem>
                            <asp:ListItem Value="1">Full-time</asp:ListItem>
                            <asp:ListItem Value="2">Part-time less than 50%</asp:ListItem>
                            <asp:ListItem Value="3">Part-time greater than 50%</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                     <p>&nbsp;</p>
                        <span class="error"></span>&nbsp;&nbsp;<b>What is the nature of the goods/ services that you most commonly procure, or manage contracts for?  </b><br />
                    <div class="form-box1">
                        <span class="error"></span>&nbsp;<asp:DropDownList ID="ddlNature" class="styled" runat="server">
                            <asp:ListItem Value="0">Please select ---</asp:ListItem>
                            <asp:ListItem Value="1">General Goods and Services</asp:ListItem>
                            <asp:ListItem Value="2">ICT</asp:ListItem>
                            <asp:ListItem Value="3">Medical / Health</asp:ListItem>
                            <asp:ListItem Value="4">Social Services</asp:ListItem>
                            <asp:ListItem Value="5">Transport Infrastructure & Services</asp:ListItem>
                            <asp:ListItem Value="6">Building Construction and Maintenance</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                     <p>&nbsp;</p>
                        <span class="error"></span>&nbsp;&nbsp;<b>What is the size of spend under your influence? </b><br />
                    <div class="form-box1">
                        <span class="error"></span>&nbsp;<asp:DropDownList ID="ddlInfluence" class="styled" runat="server">
                            <asp:ListItem Value="0">Please select ---</asp:ListItem>
                            <asp:ListItem Value="1">$1 billion or more</asp:ListItem>
                            <asp:ListItem Value="2">$500 million to $999.9 million</asp:ListItem>
                            <asp:ListItem Value="3">$100 million to $499.9 million</asp:ListItem>
                            <asp:ListItem Value="4">$50 million to $99.9 million</asp:ListItem>
                            <asp:ListItem Value="5">$20 million to $49.9 million</asp:ListItem>
                            <asp:ListItem Value="6">$10 million to $19.9 million</asp:ListItem>
                            <asp:ListItem Value="7">$5 million to $9.9 million</asp:ListItem>
                            <asp:ListItem Value="8">$2.5 million to $4.9 million</asp:ListItem>
                            <asp:ListItem Value="9">$1 million to $2.49 million</asp:ListItem>
                            <asp:ListItem Value="10">$500,000 to $999.999</asp:ListItem>
                            <asp:ListItem Value="11">Less than $500,000</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                      <p>&nbsp;</p>
                        <span class="error"></span>&nbsp;&nbsp;<b>How many procurements/ contracts have you managed in the past 12 months? </b><br />
                    <div class="form-box1">
                        <span class="error"></span>&nbsp;<asp:DropDownList ID="ddlContract" class="styled" runat="server">
                            <asp:ListItem Value="0">Please select ---</asp:ListItem>
                            <asp:ListItem Value="1">1 to 5</asp:ListItem>
                            <asp:ListItem Value="2">5 to 10 </asp:ListItem>
                            <asp:ListItem Value="3">10 to 20</asp:ListItem>
                            <asp:ListItem Value="4">20 plus</asp:ListItem>
                            <asp:ListItem Value="5">Not relevant</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                      <p>&nbsp;</p>
                        <span class="error"></span>&nbsp;&nbsp;<b>Select one of the range of activities listed which most closely reflects the nature of your role </b><br />
                    <div class="form-box1">
                        <span class="error"></span>&nbsp;<asp:DropDownList ID="ddlRange" class="styled" runat="server">
                            <asp:ListItem Value="0">Please select ---</asp:ListItem>
                            <asp:ListItem Value="1">Operational tasks like routing non-matched invoices or other administrative tasks</asp:ListItem>
                            <asp:ListItem Value="2">Managing contracts and dealing with operational problems that arise </asp:ListItem>
                            <asp:ListItem Value="3">Operational procurement tasks like raising purchase orders for end users</asp:ListItem>
                            <asp:ListItem Value="4">Tactical procurement activities, like issuing and evaluating quotes for simple acquisitions</asp:ListItem>
                            <asp:ListItem Value="5">Strategic procurement activities, like managing procurement projects for complex and/or high value acquisitions</asp:ListItem>
                            <asp:ListItem Value="6">Managing significant contracts and ensuring that outcomes are realised</asp:ListItem>
                            <asp:ListItem Value="7">Policy, governance or other managerial tasks, such as supporting governance bodies and/or reporting </asp:ListItem>
                            <asp:ListItem Value="8">Procurement leadership, setting goals and direction and leading a team of procurement practitioners</asp:ListItem>                           
                        </asp:DropDownList>

                    </div>
                    <div class="clear"></div>
                    <p>&nbsp;</p>
                    <p class="txtRgt">
                        <input type="submit" id="submitbutton" value="BACK" class="btn-save" />
                        <input type="submit" id="submitbuttonnext" value="NEXT" class="btn-next" />
                    </p>
                    <p>&nbsp;</p>
                </div>
            </article>
        </section>
        <div class="dot-line">&nbsp;</div>
    </article>
</asp:Content>
