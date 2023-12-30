<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderCancleConfirm.aspx.cs" Inherits="BMHSRPv2.OrderCancleConfirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
       <%-- $(document).ready(function () {
            $('#btnConfirm').click(function () {
                var Orderno = $('#lblorderno').val();
                // var VehicleNo = $('#lblVehicleNo').val();
                var Orderno = $("#<%=lblorderno.ClientID%>").text();// runat server
                var VehicleNo = $("#<%=lblVehicleNo.ClientID%>").text();

                if (Orderno != '' && VehicleNo != '')
                {
                    var url = "OrderCancelCaptchaCheck.aspx?ORDER_NUMBER=" + Orderno + "&REG_NUMBER=" + VehicleNo;
                    window.location.href = url;
                }
                else
                {
                    alert(VehicleNo);
                }
      
                return false;
            });
        });--%>
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnNotConfirm').click(function () {
                var url = "Index.aspx";
                window.location.href = url;
                return false;
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="app inner-pages check-status-page clearfix color1">
        <div class="check-status-appointment check-status-form addedspace">
            <h4>Order Cancellation </h4>
             <asp:Literal ID="LiteralMessage" runat="server"></asp:Literal>
            <div class="form-data clearfix">
                    <div class="control">
                      <div class="row">
                        <div class="col-sm-6">
                            <label for="orderno">Order No.</label>
                            <p id="lblorderno" runat="server"></p>
                        </div>
                        <div class="col-sm-6">
                            <label for="VehicleNo">Vehicle Registration No.</label>
                           <p id="lblVehicleNo" runat="server"></p>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-sm-6">
                            <label for="Appointmentdate">Appointment Date</label>
                            <p id="lblAppointmentdate" runat="server"></p>
                        </div>
                        <div class="col-sm-6">
                            <label for="Status">Order Status</label>
                            <p id ="lblStatus" runat="server"></p>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-sm-6">
                            <label for="Dealer">Name</label>
                            <p id="lblDealer" runat="server"></p>
                        </div>
                        <div class="col-sm-6">
                            <label for="Address">Fitment Address</label>
                            <p id="lblAddress" runat="server"></p>
                        </div>
                           
                      </div>
                  </div>
                  <div class="control">
                    <button class="filled" id="btnConfirm" onServerClick="btnConfirm_OnClick"  runat="server">Confirm</button>
                    <button class="filled" id ="btnNotConfirm">Not Confirm</button>
                </div>
            </div>
        </div>
    </div>

     
                                      
                                
</asp:Content>
