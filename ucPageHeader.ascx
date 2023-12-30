<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPageHeader.ascx.cs" Inherits="BMHSRPv2.ucPageHeader" %>
<!-- Global site tag (gtag.js) - Google Analytics -->
<script async src="https://www.googletagmanager.com/gtag/js?id=G-XDKVQBG5DN"></script>
<script>
  window.dataLayer = window.dataLayer || [];
  function gtag(){dataLayer.push(arguments);}
  gtag('js', new Date());

  gtag('config', 'G-XDKVQBG5DN');
</script>
<style>
    .Bar {
        font-weight: 600;
        color: #231f20;
        font-size: 13.87pt;
        /*padding: 15px 13px;*/
        padding: 15px 13px 15px 0px;
        position: relative;
        text-align: center;
        display: table-cell;
        vertical-align: middle;
    }
</style>
<header>
    <a href="../index.aspx">
        <div class="logo font4">BOOK-MY-HSRP<i></i><i></i><i></i><i></i></div>
    </a>
    <%--<span style="font-weight: 600; color: #231f20; font-size: 13.87pt; padding: 15px 13px; position: relative; text-align: center; display: table-cell; vertical-align: middle;"></span>--%>
    <%--<span class="Bar"></span>--%>
    <%--<img src="../assets/img/rosmerta_hsrp_logo.png" style="height: 60px;" />--%>
    <%--<img src="../assets/img/rosmerta_hsrp_logo.png" style="height: 50px;" />--%>
    <%--<p class="sub-line-more font3">Made easy!</p>--%>

    <span style="font-weight: 600;color: #231f20;font-size: 13.87pt;padding: 15px 13px;position: relative;text-align: center;display: table-cell;vertical-align: middle;"></span>
        <img src="../assets/img/rosmerta_hsrp_logo.png" style="height:60px;"/>
    <ul class="menu">
  <%--     <li><a href="../PinForHomeDelivery.aspx" style="color:black"><b>CHECK FOR HOME DELIVERY</b></a></li>--%>
        <li><a href="../TrackOrder.aspx" class="active1">Track Your Order</a></li>
<%--        <li><a href="../ReAppointment.aspx">Reschedule Appointment</a></li>--%>
        <li><a href="../OrderCancel.aspx">Cancel Order</a></li>
        <li><a href="../ReceiptValidity.aspx" style="color:black"><b>Receipt Validity</b></a></li>

        <li><a href="../BookMyHSRP_ContactUs.aspx" style="color:black"><b>Contact Us</b></a></li>
        <%--<li style="vertical-align: top;"><a href="#">Corporate Bookings<br /><span class="blink blink-one">(Coming Soon)</span></a></li>--%>
        
       <%-- <li><a href="#"><span style="color:red">(coming soon)</span> <br />&nbsp;&nbsp;Corporate Bookings </a></li>--%>
    </ul>
    <div class="menubutton">
        <span></span>
        <span></span>
        <span></span>
    </div>
</header>
<div class="megamenu"></div>
