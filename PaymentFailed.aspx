<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentFailed.aspx.cs" Inherits="BMHSRPv2.PaymentFailed" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="app inner-pages check-status-page clearfix color1 errorpage">
        <div class="check-status-form addedspace">
            <div class="control">
                <h4 style="color:red !important">Payment Faild!</h4>
                <h6>Trasaction Status - Uncompleted</h6>

                <a href="index.aspx" class="filled">Home</a>
            </div>
        </div>
    </div>

</asp:Content>
