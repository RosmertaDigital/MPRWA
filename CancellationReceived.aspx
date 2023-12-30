<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CancellationReceived.aspx.cs" Inherits="BMHSRPv2.CancellationReceived" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="app inner-pages check-status-page clearfix color1">
        <div class="check-status-appointment check-status-form addedspace">
            <h4>Order Cancellation Request Received </h4>
            <h5>Cancelled and payment refund initiated as per your order details are below:</h5>

            <div class="row">
                        <div class="text-center">
                           <!-- <label for="orderno">Order No.</label>-->
                            <h5>Order No</h5>
                            <p id="lblorderno" runat="server"></p>
                        </div>
                    </div>

            <div class="form-data clearfix">
                <div class="control">
                    
                  
                    
                    <div class="row">
                        <div class="col-sm-6">
                            <label for="Status">OrderStatus</label>
                            <p id="lblorderstatus" runat="server"></p>
                        </div>

                        <div class="col-sm-6">
                            <label for="VehicleNo">Vehicle Registration No.</label>
                            <p id="lblVehicleNo" runat="server"></p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <label for="FuelType">Fuel Type</label>
                            <p id="lblFuelType" runat="server"></p>
                        </div>
                        <div class="col-sm-6">
                            <label for="Vehicleclass">Vehicle class</label>
                            <p id="lblVehicleclass" runat="server"></p>
                        </div>
                    </div>

                </div>
               
            
            </div>
        </div>
    </div>
    
</asp:Content>
