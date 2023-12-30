<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AppointmentStatus.aspx.cs" Inherits="BMHSRPv2.AppointmentStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnclose').click(function () {
                var url = "Index.aspx";
                window.location.href = url;
                return false;
            });
        });
    </script>
    <style>
        .check-status-page .check-status-appointment {
    max-width: 1000px;
}
    </style>
     <div class="app inner-pages check-status-page clearfix color1">
        <div class="check-status-appointment check-status-form addedspace">
            <h4>Appointment Status</h4>
            <div class="form-data clearfix">
                    <div class="control" style='font-size:small'>
                      <div class="row">
                        <div class="col-sm-3">
                            <label for="orderno">Order No.</label>
                            <p id="lblorderno" runat="server"></p>
                        </div>
                        <div class="col-sm-3">
                            <label for="VehicleNo">Vehicle Registration No.</label>
                           <p id="lblVehicleNo" runat="server"></p>
                        </div>
                      </div>
                        <asp:Literal runat="server" ID="LitralAppointmentDates"></asp:Literal>
                      <div class="row">
                        <div class="col-sm-3">
                            <label for="Appointmentdate">Final Appointment Date</label>
                            <p id="lblAppointmentdate" runat="server"></p>
                        </div>
                        <div class="col-sm-3">
                            <label for="Status">Order Status</label>
                            <p id ="lblStatus" runat="server"></p>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-sm-3">
                            <label for="Dealer" >Name</label>
                            <p id="lblDealer" runat="server"></p>
                        </div>
                        <div class="col-sm-3">
                            <label for="Address">Fitment Address</label>
                            <p id="lblAddress" runat="server"></p>
                        </div>
                      </div>
                  </div>
                  <div class="control">
                    <button class="filled" id="btnclose">Close</button>
                    <!--<button>Email Receipt</button>-->
                      <%--<a href="https://bmhsrpreciept.rosmertahsrp.com//Plate/Oct-2020/BMHSRP20201024193642645624102020193946499.pdf" class="filled">Download Recipt</a>--%>
                      <asp:Literal ID="dreceipt" runat="server"></asp:Literal>
                       <asp:Label ID="lblMessage" runat="server" Text="" style="font-size: 12.86pt;color: red;font-weight: 300;" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>

     
                                      
                                
</asp:Content>
