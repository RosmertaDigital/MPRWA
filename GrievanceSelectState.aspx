<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GrievanceSelectState.aspx.cs" Inherits="BMHSRPv2.GrievanceSelectState" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="app inner-pages check-status-page clearfix color1 contdiv" style="padding: 5px 60px 0;">
        <div class="check-status-form addedspace cont">
            <h4>Select State for Grievance</h4>
            <div class="form-data isforced clearfix">
                <div class="control">
                    <a href="http://grievance.bookmyhsrp.com/grievance/index.php/home/grievance_dl" class="filled btn isforwardpay">Delhi</a>
                    <a href="http://grievance.bookmyhsrp.com/grievance/index.php/home/grievance_up" class="filled btn isforwardpay">Uttar Pradesh</a>
                </div>
            </div>
        </div>
        
        <div class="check-status-form addedspace cont" style="max-width:480px;">
            <h4>Track Ticket Status of Grievance</h4>
            <div class="form-data isforced clearfix">
                <div class="control">
                    <center><a href="http://grievance.bookmyhsrp.com/grievance/index.php/home/tracking_status" class="filled btn isforwardpay">Track</a></center>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
