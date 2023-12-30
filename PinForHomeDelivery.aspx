<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PinForHomeDelivery.aspx.cs" Inherits="BMHSRPv2.PinForHomeDelivery" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="app inner-pages check-status-page clearfix color1">
        <div class="check-status-form addedspace">
            <h4>Home Delivery Pincode </h4>
            <span class="helper">last updated 27 Nov 2020 </span>
            <asp:Literal ID="LiteralMessage" runat="server"></asp:Literal>

                              
      <div class="form-data clearfix">
      
            </div>
                               
                                
                               <div class="form-data isforced clearfix">
                                      <div class="control">
                                           <a href="../Document/PinCode-Delhi.pdf" target="_blank" class="filled btn isforwardpay">Delhi State</a>
                                          <a href="../Document/PinCode-UPv2.pdf" target="_blank"  class="filled btn isforwardpay">UP State</a>
                                         
                                      </div>

                                </div>
 


               



        
           
        </div>
    </div>
</asp:Content>
