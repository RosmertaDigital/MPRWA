<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FinalCancelStep_old.aspx.cs" Inherits="BMHSRPv2.FinalCancelStep" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


     

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<script type = "text/javascript">
        function SetSelectedText(ddlReason) {
            var selectedText = ddlReason.options[ddlReason.selectedIndex].innerHTML;
        document.getElementById("hfFruitName").value = selectedText;
    }
    </script>--%>

     <div class="app inner-pages check-status-page clearfix color1">
        <div class="check-status-appointment check-status-form addedspace">
            <h4>Booking Summary</h4>
            <div class="form-data clearfix">
                <label id="lblMsg" runat="server" visible="false"></label>
               </div>
            <div class="form-data clearfix">
                    <div class="control">
                         <div class="row">
                             <div class="col-sm-6">
                            <label for="orderno">Order No.</label>
                            <p id="lblorderno" runat="server"></p>
                            </div>
                         </div>
                      <div class="row">
                        <div class="col-sm-6">
                            <label for="VehicleNo">Vehicle Registration No.</label>
                           <p id="lblVehicleNo" runat="server"></p>
                        </div>
                          <div class="col-sm-6">
                            <label for="Appointmentdate">Appointment Date</label>
                            <p id="lblAppointmentdate" runat="server"></p>
                        </div>
                      </div>
                        <div class="row">
                            <div class="col-sm-6">
                            <label for="slot">Appointment Slot:</label>
                            <p id="lblSlot" runat="server"></p>
                        </div>
                            <div class="col-sm-6">
                            <label for="EngineNo">Engine No:</label>
                            <p id ="lblEngineNo" runat="server"></p>
                        </div>
                        </div>
                      <div class="row">
                        <div class="col-sm-6">
                            <label for="ChassisNo">Chassis No:</label>
                            <p id ="lblChassisNo" runat="server"></p>
                        </div>
                       <div class="col-sm-6">
                            <label for="VehicleMake">Vehicle Make:</label>
                            <p id="lblVehicleMake" runat="server"></p>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-sm-6">
                            <label for="FuelType">Fuel Type</label>
                            <p id="lblFuelType" runat="server"></p>
                        </div>
                          <div class="col-sm-6">
                            <label for="Vehiclemodel">Fitment Address</label>
                            <p id="lblVehiclemodel" runat="server"></p>
                        </div>
                          </div>
                        <div class="row">
                          <div class="col-sm-6">
                            <label for="Vehicletype">Vehicle Type</label>
                            <p id="lblVehicletype" runat="server"></p>
                        </div>
                            <div class="col-sm-6">
                            <label for="Vehicleclass">Vehicle class</label>
                            <p id="lblVehicleclass" runat="server"></p>
                        </div>
                     </div>

                        <div class="row">
                          <div class="col-sm-6">
                            <label for="Reason">Reason for Order Cancel</label>
                              <select id="ddlReason" name="reason" runat="server">
                                  <option value="Select Reason">Select Reason</option>
                                  <option value="Wrong Vehicle type Select">Wrong Vehicle type Select</option>
                                  <option value="Other">Other</option>
                              </select>
                        </div>
                              <div class="col-sm-6">
                               <label>Remark</label>
                                  <input id="txtother" type="text" runat="server"/>
                                  <textarea id="w3review" name="w3review" rows="4" cols="50"></textarea>
                               
                                </div>
                          </div>
                      </div>
              
                  <div class="control">
                    <button  class="filled" runat="server"  OnServerClick="BtnConfirm_OnClick" id="Confirm">Confirm&Proceed</button>
                </div>
            </div>
        </div>
    </div>

     
                                      
                                
</asp:Content>
