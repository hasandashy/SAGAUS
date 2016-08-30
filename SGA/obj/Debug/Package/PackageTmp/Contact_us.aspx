<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Contact_us.aspx.cs" Inherits="SGA.Contact_us" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var alertHtml = '';
    $(document).ready(function () {
        $('#btnSend').colorbox({
            href: "Popup.aspx",
            width: "492px",
            height: "300px",
            onComplete: function () {
                $('#alertMessage').text(alertHtml);
                $('#title').text('Success!');
            }
        });

        $('#btnSend').click(function () {
            var error = 0;
            var emptyFields = new Array();
            var name = $('#Firstname').val();
            if (name == '' || name == 'First name') {
                error = 1;
                emptyFields.push('First name');
            }
            var surname = $('#Lastname').val();
            if (surname == '' || surname == 'Last name') {
                error = 1;
                emptyFields.push('Last name');
            }
            var company = $('#CName').val();
            if (company == '' || company == 'Organisation') {
                error = 1;
                emptyFields.push('Organisation');
            }
            var email = $('#Email').val();
            if (email == '' || email == 'Email') {
                error = 1;
                emptyFields.push('Email');
            }

            var Department = $('#Department').val();
            if (Department == '' || Department == 'Department') {
                error = 1;
                emptyFields.push('Department');
            }

            var Position = $('#Position').val();
            if (Position == '' || Position == 'Position') {
                error = 1;
                emptyFields.push('Position');
            }

            var interest = "";

            $("input[name='interest']:checked").each(function () {
                interest += $(this).val() + ",";
            });

            if (($("#terms").is(':checked')) == false) {
                error = 1;
                emptyFields.push("\r\nPlease tick the checkbox to agree to the Terms and Conditions.");
            }

            if (interest == undefined) {
                interest = "";
            }


            if (error) {
                $('#colorbox').css({ "display": "block" });
                alertHtml = 'Please enter ' + emptyFields.join(', ');
            }
            else if (email != '' && !filter.test(email)) {
                $('#colorbox').css({ "display": "block" });
                alertHtml = 'Please enter valid email id';
            }
            else {

                var json =
                        $.ajax({
                            type: "POST",
                            async: false,
                            url: "About_us.aspx/SendMail",
                            data: JSON.stringify({ 'Firstname': name, 'Lastname': surname, 'Email': email, 'CName': company, 'Department': Department, 'Position': Position, 'interest': interest, 'comments': $('#comments').val() }),
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                if (data.d == 'success') {
                                    $('#colorbox').css({ "display": "block" });
                                    alertHtml = 'Thank you for your enquiry. \r\nA member of our Compara team will be in contact.';
                                    $('#Firstname').val('');
                                    $('#Lastname').val('');
                                    $('#CName').val('');
                                    $('#Email').val('');
                                    $('#Department').val('');
                                    $('#Position').val('');
                                    $('#comments').val('');
                                }
                            }
                        });
            }
        });

    });
</script>
    				
				 <div class="inner-banner">
                <img src="images/standing-banner.jpg"  alt="" /> </div>
				<!-- Content Area start -->
				<article id="container">


                    <div class="heading-block">
   
      <h1>Standing Offer of Agreement</h1>
      <p>Making it easier for you to access assessments, training and e-Learning at preferred pricing.</p>
    
  </div>
        
        
        
<div class="info-block ">

<div class="info-col-1 lineh1">
<div class="intro1"> <span class="tittle1">The Queensland Government has entered into a Standing Offer Arrangement (SOA) with Comprara to provide training services as part of the Critical Skills Boost program until 30 June 2018.</span>
<span class="tittle1 orange-color mtop2">Benefits</span>
<ul class="stand-offer">
<li>The SOA makes it easier for “eligible customers” to purchase assessments, training and e-Learning covering procurement, leadership and contract management</li>
<li>Contract terms are already agreed with Comprara, so there is no need for you to negotiate a new contract – saving you time and money</li>
<li> You have access to competitive pricing and terms that have been negotiated with Comprara on behalf of the Queensland Government.</li>
</ul>
</div>

<div class="intro1 role">
<span class="tittle1 orange-color">Eligible customers include:</span>
<p>1. Queensland Government Bodies (as defined in Definitions and rules of interpretation):</p>
<ul class="stand-offer">
<li>body corporate or an unincorporated body established or constituted for a public purpose by the State of Queensland legislation, or an 			instrument made under that legislation (including a local authority);</li>
<li>a body established by the State of Queensland through the Governor or a Minister; or</li>
<li>an incorporated or unincorporated body over which the State of Queensland exercises control</li>
</ul>

<p>2. Entities funded by the State of Queensland</p>
<p>3. Community based non-profit making organisations performing community services</p>
<p>4. Commonwealth Government, State Government or a Territory Government.</p>

<p class="tittle1 mtop2"><a href="Images/FactSheetSOA.pdf" title="FactSheet SOA">Download the full Standing Offer Agreement here</a></p>

</div>
</div>



<div class="info-col-2">
<div class="intro1">
<div class="comprara-design"> 
<span class="template"><img src="images/skills-procure.png" alt=""></span> 
<div class="dw-bt"><a href="Images/FactSheetSOA.pdf" title="FactSheet SOA">DOWNLOAD <br>BROCHURE HERE &gt;&gt;</a></div>
</div>
</div>
</div>


</div> 
        
        
        
        
        
        
          
          <div class="dot-line clear mar1">&nbsp;</div>

                    <div class="full">
     <div class="heading-block ptopnone">
   
      <h2>Want to know more?</h2>
      <p>Please complete the form below and we will get in touch.</p>
    
  </div>
                    
                    <p style="width:100%; float:left; height:20px;">&nbsp;</p>
                    <div class="full standing-form">
					<div class="cnt-bx">
						<section class="equal_height">
							<p class="title28-1">Tell us about yourself</p>
                            <p>&nbsp;</p>
							<div class="cnt-us-form">
								
								<p ><input type="text" class="txt-field1" title="First name" value="First name" maxlength="100" id="Firstname"/></p>
                               
								<p ><input type="text" class="txt-field1" title="Last name" value="Last name" maxlength="100" id="Lastname"/></p>
                               
								<p ><input type="text" title="Email" value="Email" class="txt-field1" maxlength="250" id="Email"/></p>
								
                                <p ><input type="text"  class="txt-field1" title="Organisation" value="Organisation" maxlength="100"  id="CName"/></p>
								
								<p ><input type="text" class="txt-field1" title="Department" value="Department" maxlength="100"  id="Department"/></p>
							
								<p ><input type="text" title="Position" value="Position" class="txt-field1" id="Position" maxlength="50" /></p>
								 
								
                                 <p ><textarea rows="8" class="txt-area"  id="comments">Comments</textarea></p>
                                
								
							</div>
						</section>
					</div>
					<div class="cnt-bx">
						<section class="cnt-rt equal_height">
                            <p>&nbsp;</p>
							<p class="title28-1">Tell us your primary area of interest</p>
                            <p>&nbsp;</p>
							<div class="cnt-us-form">
								<p class="radio-txt"><input type="checkbox" name="interest" class="styled" value="Training Needs Analysis" id="int1"/> <label for="int1" >Training Needs Analysis</label></p>
								
                                <p class="radio-txt"><input type="checkbox" name="interest" class="styled" value="Contract Management - Training Workshops" id="int2"/> <label for="int2" >Contract Management - Training Workshops</label></p>
								
                                <p class="radio-txt"><input type="checkbox" name="interest" class="styled" value="Category Management - Training Workshops" id="int3"/> <label for="int3" >Category Management - Training Workshops</label></p>
								
                                <p class="radio-txt"><input type="checkbox" name="interest" class="styled" value="Sourcing - Training Workshops" id="int4"/> <label for="int4" >Sourcing - Training Workshops</label></p>
								
                                <p class="radio-txt"><input type="checkbox" name="interest" class="styled" value="Procurement Leadership - Training Workshops" id="int5"/> <label for="int5" >Procurement Leadership - Training Workshops</label></p>
 								
                                <p class="radio-txt"><input type="checkbox" name="interest" class="styled" value="Access to e-Learning" id="int6"/> <label for="int6" >Access to e-Learning</label></p>
								
								<p class="txt13"><input type="checkbox" class="styled" name="terms" id="terms"/> By ticking this box, I acknowledge I will be contacted by Comprara. I agree to the <a href="/Terms" title="terms">terms of this website</a> and understand that the information (including my personal information) entered is held offshore.</p>
 								<p>&nbsp;</p>
								<p><input type="submit" value="" class="btn-submit floatR" id="btnSend" /></p>
							</div>
							<p>&nbsp;</p>
						</section>
					</div>
					<div class="clear"></div>
                    </div>
					<p>&nbsp;</p>
                    </div>
					<div class="dot-line">&nbsp;</div>
                    <div class="clear"></div>
					<div class="orange-bar">The Skills Boost you have been waiting for!</div>
					<p>&nbsp;</p>
				</article>
				<!-- Content Area end // -->
<script>
    $(document).ready(function () {
        $(".iphonNav ul li").removeClass("active");
        //alert($(".iphonNav ul li").eq(4));
        $(".iphonNav ul li:eq(19)").addClass("active");
    });
</script>
</asp:Content>

