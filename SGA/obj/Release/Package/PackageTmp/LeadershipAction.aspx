<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LeadershipAction.aspx.cs" Inherits="SGA.LeadershipAction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div>
<!-- Banner start -->
        
        <div class="inner-banner">
        <img src="images/leadership-banner.jpg"  alt="" /> </div>
        <!-- Banner end // --> 
        
        <!-- Content Area start -->
        <article id="container">
       
        <div class="heading-block">
   
      <h1>Leadership in Action</h1>
      <p>A key development program for our Procurement<br /> and Contract Management leaders.</p>
    
  </div>
        
        <div class="info-block lead-program">
       <p class="txt2">
       <span>This Leadership in Action program will further enhance procurement performance across Queensland Government in the way we lead and engage.</span> These workshops are dedicated to opening pathways so we can perform at our peak. By joining together in these highly focused workshops we will leverage our collective strengths to create a landscape of positive change and deliver ongoing sustainable results to the business.
       </p> 
        
        
        <div class="info-col-1 lineh1 ">
        <div class="leadership-action">
          <span class="tittle1">Procurement Leadership – in Action</span>
          
         <div>
         <span class="txt5">Credibility</span>
         <p>Develop credibility by engaging with confidence, verbalising insights into procurement and leading candid debate.</p>
         </div>
         
         
          <div>
         <span class="txt5">Vision</span>
         <p>Communicate a clear and compelling vision to key stakeholders demonstrating alignment of procurement strategy with business needs.</p>
         </div>
         
         <div>
         <span class="txt5">Status</span>
         <p>Earn the status of trusted adviser amongst key stakeholders by increasing influence and gaining buy-in for procurement strategies.</p>
         </div>
         
         <div>
         <span class="txt5">Ownership</span>
         <p>Exemplify and coach excellence in procurement by taking ownership to deliver better procurement outcomes.</p>
         </div>
         
         
           <div>
         <span class="txt5">Leadership</span>
         <p>Demonstrate leadership by consistently driving and delivering sustainable results.</p>
         </div>
         
         <div class="attend">
            <span class="tittle1">Who should attend?</span>
            
            <p>
            <span>The Queensland Government Leadership in Action Program is designed for individuals who are responsible for leading:</span>
            
<P>• Delivery of an organisation’s procurement function, and/or</P>
<p>• Teams that are responsible for procurement (including any part of the procurement lifecycle).</p>
            </p>
            
            </div>
          </div>
        
        </div>
        
        
        <div class="info-col-2 lineh1 ">
        
        <div class="procurement-books"><img alt="" src="images/leadership-action.png"></div>
        
        <div class="dw-bt"><a href="images/CSB-LeadershipInAction.pdf">DOWNLOAD <br />BROCHURE HERE >></a></div>
        </div>
        
        
        </div>
        
          
          
          
          
          
          <div class="dot-line clear ">&nbsp;</div>
          <div class="clear">&nbsp;</div>
          <div class="orange-bar">The Skills Boost you have been waiting for! </div>
          <p class="hide">&nbsp;</p>
          
          
        </article>
</div>
<script>
    $(document).ready(function () {
        $(".iphonNav ul li").removeClass("active");
        //alert($(".iphonNav ul li").eq(4));
        $(".iphonNav ul li:eq(16)").addClass("active");
    });
</script>
</asp:Content>
