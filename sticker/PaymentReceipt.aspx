<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentReceipt.aspx.cs" Inherits="BMHSRPv2.sticker.PaymentReceipt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <script>
       function BarCodeDisplay(receiptPath) {
         //  alert(receiptPath);
           //var QRPath = "https://chart.googleapis.com/chart?chs=150x150&cht=qr&chl=DLHSRP20191228182008374&chld=L|1&choe=UTF-8";
           var QRPath = "https://chart.googleapis.com/chart?chs=150x150&cht=qr&chl=" + receiptPath + "&chld=L|1&choe=UTF-8";
           $("#img_qrcode").attr("src", QRPath);
       }
   </script>
    
    <div class="app clearfix">
        <section class="after">
            <div class="color1 view-data">
                <div class="in_pages page1 clearfix">
                    <div class="addedspace width50 width40">
                        <div class="clearfix">
                            <div class="leftshow">
                                <h4>Receipt of Payment & Appointment</h4>
                                <h5>Order number: <asp:Label ID="lblOrderNo" runat="server" Text="..." ></asp:Label></h5></div>
                        </div>

                        <div class="qr">
                        <p>Scan to download the receipt.</p>
                            <img src="https://chart.googleapis.com/chart?chs=150x150&cht=qr&chl=DLHSRP20191228182008374&chld=L|1&choe=UTF-8" id="img_qrcode">
                        </div>

                        <h5 class="orname">- OR -</h5>

                        <!--<button class="filled btn printoption">Print</button>-->
                        <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" class="filled btn " />

                        <div class="rightshow"><p>HSRP Online Appointment Transaction Reciept<br>
                                Rosmerta Safety System Pvt. Ltd.<br>
                                www.rosmertahsrp.com</p></div>
                    </div>
                    <div class="addedspace printpage width50 width60">
                        <div class="printarea">
                            <div class="white">
                                <h5>HSRP Sticker Appointment</h5>
                                <asp:Label ID="lblMessage" runat="server" Text="" style="font-size: 12.86pt;color: red;font-weight: 300;" Visible="false"></asp:Label>
                                <div class="table-show">
                                    <table>
                                        <tr>
                                            <td>Order Date :</td>
                                            <td>
                                                <asp:Label ID="lblOrderDate" runat="server" Text="" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Vehicle Reg No :</td>
                                            <td>
                                                <asp:Label ID="lblVehicleRegNo" runat="server" Text="" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Order ID :</td>
                                            <td>
                                                <asp:Label ID="lblOrderId" runat="server" Text="" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Bank Tracking ID :</td>
                                            <td>
                                                <asp:Label ID="lblTrackingID" runat="server" Text="" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Payment/Order Status/Status Message :</td>
                                            <td>
                                                <asp:Label ID="lblPaymentStatus" runat="server" Text="" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Amount :</td>
                                            <td>
                                                <asp:Label ID="lblAmount" runat="server" Text="" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Billing Name :</td>
                                            <td>
                                                <asp:Label ID="lblName" runat="server" Text="" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Billing Mobile</td>
                                            <td>
                                                <asp:Label ID="lblMobile" runat="server" Text="" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Billing Email ID:</td>
                                            <td>
                                                <asp:Label ID="lblEmailID" runat="server" Text="" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Appointment Date Time</td>
                                            <td>
                                                <asp:Label ID="lblAppointmentDateTime" runat="server" Text="" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Fitment Center Name :</td>
                                            <td>
                                                <asp:Label ID="lblFitmentCenterName" runat="server" Text="" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Fitment Center Address :</td>
                                            <td>
                                                <asp:Label ID="lblFitmentAddress" runat="server" Text="" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trFitmentPerson" runat="server">
                                            <td>Fitment Person Name :</td>
                                            <td>
                                                <asp:Label ID="lblFitmentPersonName" runat="server" Text="" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trFitmentMobile" runat="server">
                                            <td>Mobile No. :</td>
                                            <td>
                                                <asp:Label ID="lblFitmentPersonMobile" runat="server" Text="" ></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="grey">
                                <p>Please note:</p>
                                <ul>
                                    <li>Carry this receipt and RC copy at the time of appointment.</li>
                                   <%-- <li>If payment is not seccessfull Process Booking Again.</li>--%>
                                    <li>For queries, Please contact Toll Free 18001200201, Email ID : online@bookmyhsrp.com
                                    </li>
                                    <li>Calling time (9:30 AM to 6:00 PM) and day (Monday to Saturday) or Email at online@bookmyhsrp.com</li>
                                    <li>Re-Appointment (if any) will only available for future dates.
                                    </li>
                                    <li> <asp:Label ID="lblvalidity" runat="server" Text="Validity upto 12/09/2020" ></asp:Label></li>
                                    <li>Fitment charges paid, no extra payment required to be paid to fitment person/dealer team.
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

    <iframe id="frame1" runat="server" name="frameMain" width="0px" height="0px" visible="false"></iframe>
</asp:Content>
