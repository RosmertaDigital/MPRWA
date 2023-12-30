<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReceiptValidityDetail.aspx.cs" Inherits="BMHSRPv2.ReceiptValidityDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="app inner-pages check-status-page clearfix color1">
        <div class="check-status-appointment check-status-form addedspace">
            <h4 class="center">Receipt Validity Detail</h4>
            <div class="form-data clearfix">
                <div class="control">
                    <div class="row">
                        <div class="center">
                            
                            <p id="lblorderstatus" runat="server"></p>
                        </div>

                        <%--<div class="col-sm-6">
                            <label for="VehicleNo">Vehicle Registration No.</label>
                            <p id="lblVehicleNo" runat="server"></p>
                        </div>--%>
                    </div>
                </div>
               
            
            </div>
        </div>
    </div>
    
</asp:Content>
