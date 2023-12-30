<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookingSummary.aspx.cs" Inherits="BMHSRPv2.sticker.BookingSummary" %>

<%@ Register Src="~/sticker/ucStepBar.ascx" TagPrefix="uc1" TagName="ucStepBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app clearfix">
        <section class="after">
            <uc1:ucStepBar runat="server" id="ucStepBar" PageStep="9" BackPage="AppointmentSlot.aspx" />

              <div class="color2 view-data">
                <div class="in_pages page1 clearfix">
                    <div class="leftside color_blue" style="display:none">
<%--                        <div class="head">
                            <img src="assets/img/h1.png" draggable="false">
                        </div>
                        <div class="card">
                            <div class="label_card bikefill">
                                <img src="../assets/img/bike-fill1.svg" draggable="false">
                            </div>
                        </div>
                        <div class="card">
                            <div class="label_card">
                                <img src="../assets/img/color0-1.svg" draggable="false">
                            </div>
                        </div>
                        <div class="card">
                            <div class="state label_card">
                                <p><span>HR</span>Haryana</p>
                            </div>
                        </div>
                        <div class="card">
                            <div class="label_card bikefill">
                                <img src="../assets/img/car-fill2.svg" draggable="false">
                                <p>Private Vehicle</p>
                            </div>
                        </div>
                        <div class="card">
                            <div class="state label_card mb0">
                                <p><span>Petrol</span></p>
                            </div>
                        </div>--%>

                                                                       <div class="head">
                           <%-- <img src="../assets/img/h1.png" draggable="false">--%>
                             <asp:Literal ID="LiteralBookingTypeImage" runat="server"></asp:Literal>
                        </div>
                        <div class="card" style="display:none">
                            <div class="label_card bikefill">
                               <%-- <img src="../assets/img/bike-fill.svg" draggable="false">--%>
                                <asp:Literal ID="LiteralVehicleTypeImage" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="card">
                            <div class="label_card">
                                  <asp:Literal ID="LiteralOemImage" runat="server"></asp:Literal>
                              <%--  <img src="../assets/img/color0.svg" draggable="false">--%>
                            </div>
                        </div>
                        <div class="card">
                            <div class="state label_card">
                                <asp:Literal ID="LiteralState" runat="server"></asp:Literal>
                               <%-- <p><span>HR</span>Haryana</p>--%>
                            </div>
                        </div>
                        <div class="card">
                            <div class="label_card bikefill">
                                  <asp:Literal ID="LiteralVehicleClassImage" runat="server"></asp:Literal>
                              <%--  <img src="../assets/img/car-fill1.svg" draggable="false">
                                <p>Private Vehicle</p>--%>
                            </div>
                        </div>
                        <div class="card">
                            <div class="state label_card mb0">
                                 <asp:Literal ID="LiteralFuelType" runat="server"></asp:Literal>
                               <%-- <p><span>Petrol</span></p>--%>
                            </div>
                        </div>
                      <div class="card">
                            <div class="label_card address">
                                <asp:Literal ID="AffixationAddress" runat="server"></asp:Literal>
                            </div>
                        </div>
                       <%-- <div class="card">
                            <div class="label_card orderno"><b>ORDER NUMBER</b>
                                <p>DLHSRP2019</p>
                                <p>1228182008374</p>
                            </div>
                        </div>--%>
                    </div>
                    <div class="rightside">
                        <div class="addedspace clearfix morehsrp">
                            <h4>Booking Summary</h4>
                           <%-- <h5 class="is_bottom">Order number: DLHSRP20191228182008374</h5>--%>
                            <div class="summary clearfix">
                                <div class="width50s">
                                    <span>Appointment Address</span>
                                    <p><asp:Literal ID="AppAddress" runat="server"></asp:Literal></p>

                                    <span>Appointment Date</span>                           
                                    <p><asp:Literal iD="AppDate" runat="server"></asp:Literal></p>

                                    <span>Appointment Time Slot</span>
                                    <p><asp:Literal ID="TimeSlot" runat="server"></asp:Literal></p>

                                    <span>Bharat Stage (BS)</span>
                                    <p><asp:Literal ID="BharatStages" runat="server"></asp:Literal></p>

                                    <span>Reg No.</span>
                                    <p><asp:Literal ID="ltrlRest" runat="server"></asp:Literal></p>

                                    <span>Engine No.</span>
                                    <p><asp:Literal ID="EngineNo" runat="server"></asp:Literal></p>
                                    <span>Chassis No.</span>
                                    <p><asp:Literal ID="ChassisNo" runat="server"></asp:Literal></p>
                                    <span>Vehicle Make</span>
                                    <p><asp:Literal ID="VehicleMake" runat="server"></asp:Literal></p>
                                    <span style="display:none;">Vehicle Model</span>
                                    <p style="display:none;"><asp:Literal ID="VehicleModel" runat="server"></asp:Literal></p>
                                    <span>Vehicle Type</span>
                                    <p><asp:Literal ID="VehicleType" runat="server"></asp:Literal></p>
                                </div>
                            </div>
                        </div>
                        <div class="text-right width70">
                            <a href="VerifyDetailPayment.aspx" class="filled btn isforwardpay">Confirm & Proceed</a>
                        </div>
                    </div>
                </div>

            </div>
        </section>
    </div>
</asp:Content>

