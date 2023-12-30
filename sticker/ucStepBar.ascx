<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucStepBar.ascx.cs" Inherits="BMHSRPv2.sticker.ucStepBar" %>

<div class="steps bars color1" style="vertical-align:top !important">
    <ul>
        <li><a href="#"><span>Step 1</span> Booking Details</a></li>
        <li><a href="#"><span>Step 2</span> Fitment Location</a></li>
        <li><a href="#"><span>Step 3</span> Appointment Slot</a></li>
        <li><a href="#"><span>Step 4</span> Booking Summary</a></li>
        <li><a href="#"><span>Step 5</span> Verify Details & Pay</a></li>
        <li><a href="#"><span>Step 6</span> Download Receipt </a></li>
    </ul>
    <asp:Literal ID="ltBack" runat="server" Text="<div class='backbtn' data-act='#dataact'> <a href='#page' class='gobacks'> <img src='../assets/img/back.svg'></a></div>"></asp:Literal>
</div>



