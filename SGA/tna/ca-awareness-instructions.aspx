<%@ Page Title="" Language="C#" MasterPageFile="~/tnaMaster.Master" AutoEventWireup="true" CodeBehind="ca-awareness-instructions.aspx.cs" Inherits="SGA.tna.ca_awareness_instructions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Custom Form -->
    <script type="text/javascript" src="../js/custom-form-elements.js"></script>

    <!-- Accordion Menu -->
    <script type="text/javascript" src="../js/jquery.min.js"></script>
    <script type="text/javascript" src="../js/ddaccordion.js"></script>
    <script type="text/javascript" src="../js/ddaccordion-menu.js"></script>
    <!-- Popup -->
    <script type="text/javascript" src="../Scripts/jquery.colorbox.js"></script>
    <script type="text/javascript" src="../js/custom.js"></script>

    <script type="text/javascript" language="javascript">
        var alertHtml = '';
        var lastpage = 'n';
        var redirect = 'y';
        function sentBack() {
            window.location.href = '/tna/default.aspx';
        }
        function FinalSubmit() {
            $('#colorbox').css({ "display": "none" });
            window.location.href = '/tna/commercial-awareness-assessment-test.aspx';
        }

        $(document).ready(function () {
            $('.my-profile').colorbox({
                href: "../Popup.aspx",
                width: "392px",
                height: "450px",
                onComplete: function () {
                    if (lastpage == 'y') {
                        $('#title').text("Confirmation");
                        if (alertHtml.length > 1) {
                            $('#colorbox').css({ "display": "block" });
                            $('#alertMessage').text(alertHtml);
                            $('#btnCancel').css("display", "block");
                            $('#btnOk').removeClass("btn-yes");
                            $('#btnOk').addClass("btn-back");
                            $('#btnCancel').removeClass("btn-back");
                            $('#btnCancel').addClass("btn-yes");
                        }
                    } else {
                        $('#colorbox').css({ "display": "none" });
                        window.location.href = '/tna/myprofile.aspx?id=1';
                    }
                }
            });

            $('.my-profile').click(function () {
                var value = "<%=directSend %>";
            if (value == 0) {
                // send to profile page
                lastpage = 'n';
            } else {
                // show confirmation
                lastpage = 'y';
                alertHtml = 'You are about to begin your Commercial Awareness Assessment. This is a timed event and must be taken in a single sitting. Are you ready to begin?';
            }
        });
    });
    </script>

    <!-- Content Area start -->
    <article id="container">
        <section class="welcome">
            <p class="title40-orange" style="text-align:center;">
                <asp:Label ID="lblName" runat="server"></asp:Label></p>
            <p class="title40" style="text-align:center;">Welcome to the Commercial Awareness Assessment</p>
        </section>
        <div class="dot-line">&nbsp;</div>
        <section class="color-box">
            <article class="info-box-shdw-inner">
                <p class="title40">Instructions</p>
                <p>&nbsp;</p>
               <%-- <p class="txt16-bold">You are about to complete an evaluation of your commercial cwareness.</p>--%>
                <p>&nbsp;</p>
                <p class="txt30">01</p>
                <p>This assessment focuses on five dimensions that underpin commercial awareness.</p>
                <p>&nbsp;</p>
                <p class="txt16-bold">The five dimensions are:</p>
                <div class="step1">
                    <p>1. Stakeholder engagement</p>
                    <p>2. Outcome focus</p>
                    <p>3. Market steward</p>
                    <p>4. Risk management</p>
                    <p>5. Creating public value</p>
                </div>
                 <div class="clear"></div>
                <p>&nbsp;</p>
                <p>As you work through the assessment you may find themes that are not directly relevant to your current role. This is likely to happen for many people as this diagnostic considers a broad range of commercial awareness themes. It has purposefully been designed not to be role specific, instead it seeks to understand South Australian Government commercial awareness as a whole.</p>
                <div class="clear"></div>
                <hr class="divider-line" />
                <p class="txt30">02</p>
                <p>This assessment is scenario based. You will be given five different scenarios to consider. The five scenarios do not directly correspond with the five dimensions stated above.</p>
                <hr class="divider-line" />
                <p class="txt30">03</p>
                <p>This is a multiple-choice assessment. You will be presented with four questions for each scenario (20 questions in total) and each question has four answer options for you to choose from. Choose which ever you think is the most correct answer. </p>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <div class="floatR">
                    <%--<a href="MyProfile.aspx?id=1" class="update-profile">UPDATE PROFILE</a>--%>
                    <a id="hylProfile" runat="server" href="#" class="my-profile">BEGIN NOW</a>
                </div>
                <div class="clear"></div>
                <p>&nbsp;</p>
                <div class="timed-task">
                    <p class="title"><span>THIS IS A<br />
                        <font class="orange">TIMED TASK!</font></span></p>
                    <p>&nbsp;</p>
                    <p>
                       You are given 60 minutes to complete the evaluation. There are five sections with four scenario based questions in each. You will see the clock throughout the evaluation in the top right hand side of the page in orange and you are given a guide of your completion progress as you go. 
                    </p>
                </div>
            </article>
        </section>
        <div class="dot-line">&nbsp;</div>
    </article>
    <!-- Content Area end // -->
</asp:Content>
