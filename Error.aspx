<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="BMHSRPv2.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="app inner-pages check-status-page clearfix color1 errorpage">
        <div class="check-status-form addedspace">
            <div class="control">
                <h4 style="color:red !important">Error!</h4>
                <h6>Your session has expired,<br>please fill the details again.</h6>
                <a href="index.aspx" class="filled">Home</a>
            </div>
        </div>
    </div>

</asp:Content>
