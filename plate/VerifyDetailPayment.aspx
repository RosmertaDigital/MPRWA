<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerifyDetailPayment.aspx.cs" Inherits="BMHSRPv2.plate.VerifyDetailPayment" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0,minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <title>BOOK-MY-HSRP</title>
    <meta name="Keywords" content="">
    <meta name="Description" content="">
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate"/>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Expires" content="0"/>
    <meta name="theme-color" content="#000000"/>
    <meta name="msapplication-navbutton-color" content="#000000"/>
    <meta name="apple-mobile-web-app-status-bar-style" content="#000000"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/css/bootstrap-datepicker3.min.css">
    <link rel="stylesheet" href="../assets/css/owl.carousel.min.css">
    <link rel="stylesheet" href="../assets/css/app.css?1603186240">
    <link rel="shortcut icon" href="../assets/img/fav.png">

    <script type="text/javascript">
        function ValidateForm() {
            document.getElementById('customerData').submit();
            return true;
        }


        function ValidatePayForm() {
            //alert("hi...");
            var edit = document.querySelector(".razorpay-payment-button");

            edit.addEventListener("click", function () { console.log("clicked") });
            setTimeout(function () {
                edit.click();
            }, 1000);
        }

        function isNumberKey(evt) { // Numbers only
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="loader"></div>
            <div class="section">
                <header>
                    <a href="../index.aspx">
                        <div class="logo font4">BOOK-MY-HSRP<i></i><i></i><i></i><i></i></div>
                    </a>
                    
                    <span style="font-weight: 600;color: #231f20;font-size: 13.87pt;padding: 15px 13px;position: relative;text-align: center;display: table-cell;vertical-align: middle;"></span>
                    <img src="../assets/img/rosmerta_hsrp_logo.png" style="height:60px;"/>

                    <%--<p class="sub-line-more font3">Made easy!</p>--%>
                    <ul class="menu">
                        <li><a href="../TrackOrder.aspx" class="active1">Track Your Order</a></li>
             <%--           <li><a href="../ReAppointment.aspx">Reschedule Appointment</a></li>--%>
                        <li><a href="../OrderCancel.aspx">Cancel Order</a></li>
                        <li><a href="../ReceiptValidity.aspx">Receipt Validity</a></li>
                        <li><a href="#"><span style="color:red">(coming soon)</span> <br />&nbsp;&nbsp;Corporate Bookings</a></li>


                    </ul>
                    <div class="menubutton">
                        <span></span>
                        <span></span>
                        <span></span>
                    </div>
                </header>
                <div class="megamenu"></div>

                <div class="app clearfix">
                    <section class="after">
                        <div class="steps bars color1">
                             <ul>
        <li><a href="#"><span>Step 1</span> Booking Details</a></li>
        <li><a href="#"><span>Step 2</span> Fitment Location</a></li>
        <li><a href="#"><span>Step 3</span> Appointment Slot</a></li>
        <li><a href="#"><span>Step 4</span> Booking Summary</a></li>
        <li><a href="#"><span>Step 5</span> Verify Details & Pay</a></li>
        <li><a href="#"><span>Step 6</span> Download Receipt </a></li>
    </ul>
                            <div class="backbtn" data-act="page5">
                                <a href="BookingSummary.aspx" class="gobacks">
                                    <img src="../assets/img/back.svg"/></a>
                            </div>
                        </div>
                        <div class="color2 view-data">
                            <div class="in_pages page1 clearfix">
                                <div class="leftside color_blue" style="display:none;">
                                    <%--<div class="head">
                                        <img src="../assets/img/h1.png" draggable="false">
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
                                    </div>
                                    <div class="card">
                                        <div class="label_card address">
                                            <b>AMAN<br>
                                                MOTORS</b>
                                            <p>City: DELHI</p>
                                            <p>
                                                Address: J-128/1 PUSHTA
                                    3-1/2 KARTAR NAGAR
                                            </p>
                                            <p>pincode: 110053</p>
                                        </div>
                                    </div>--%>
                                    <%--   <div class="card">
                                        <div class="label_card orderno">
                                            <b>ORDER NUMBER</b>
                                            <p>DLHSRP2019</p>
                                            <p>1228182008374</p>
                                        </div>
                                    </div>--%>

                                    <div class="head" style="display:none;">
                                        <%-- <img src="../assets/img/h1.png" draggable="false">--%>
                                        <asp:Literal ID="LiteralBookingTypeImage" runat="server"></asp:Literal>
                                    </div>
                                    <div class="card" style="display:none;">
                                        <div class="label_card bikefill">
                                            <%-- <img src="../assets/img/bike-fill.svg" draggable="false">--%>
                                            <asp:Literal ID="LiteralVehicleTypeImage" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="card" style="display:none;">
                                        <div class="label_card">
                                            <asp:Literal ID="LiteralOemImage" runat="server"></asp:Literal>
                                            <%--  <img src="../assets/img/color0.svg" draggable="false">--%>
                                        </div>
                                    </div>
                                    <div class="card" style="display:none;">
                                        <div class="state label_card">
                                            <asp:Literal ID="LiteralState" runat="server"></asp:Literal>
                                            <%-- <p><span>HR</span>Haryana</p>--%>
                                        </div>
                                    </div>
                                    <div class="card" style="display:none;">
                                        <div class="label_card bikefill">
                                            <asp:Literal ID="LiteralVehicleClassImage" runat="server"></asp:Literal>
                                            <%--  <img src="../assets/img/car-fill1.svg" draggable="false">
                                <p>Private Vehicle</p>--%>
                                        </div>
                                    </div>
                                    <div class="card" style="display:none;">
                                        <div class="state label_card mb0">
                                            <asp:Literal ID="LiteralFuelType" runat="server"></asp:Literal>
                                            <%-- <p><span>Petrol</span></p>--%>
                                        </div>
                                    </div>
                                    <div class="card">
                                        <asp:Literal ID="AffixationAddress" runat="server"></asp:Literal>
                                        <%-- <div class="label_card address"><b>AMAN<br>
                                    MOTORS</b>
                                <p>City: DELHI</p>
                                <p>Address: J-128/1 PUSHTA
                                    3-1/2 KARTAR NAGAR
                                </p>
                                <p>pincode: 110053</p></div>--%>
                                    </div>
                                </div>
                                <div class="rightside">
                                    <div class="addedspace">
                                        <h4>Verify Details & Pay</h4>
                                        <asp:Label ID="lblMessage" runat="server" Text="" Style="font-size: 12.86pt; color: red; font-weight: 300;" Visible="false"></asp:Label>
                                        <div class="summary clearfix">
                                            <div class="width50 padd0">
                                                <%-- <h5>Order number: 
                                                    <asp:Label ID="lblOrderNo" runat="server" Text="DLHSRP20191228182008374" style="font-size: 12.86pt;color: #39435b;font-weight: 600;"></asp:Label>
                                                </h5>--%>
                                                <span>Owner Name
                                                </span>
                                                <p>
                                                    <asp:Label ID="lblOwnerName" runat="server" Text="" Style="font-size: 12.86pt; color: #39435b; font-weight: 300;"></asp:Label>
                                                </p>
                                                <span>Mobile No.</span>
                                                <p>
                                                    <asp:Label ID="lblMobile" runat="server" Text="" Style="font-size: 12.86pt; color: #39435b; font-weight: 300;"></asp:Label>
                                                </p>
                                                <span>Address</span>
                                                <p>
                                                    <asp:Label ID="lblAddress" runat="server" Text="" Style="font-size: 12.86pt; color: #39435b; font-weight: 300;"></asp:Label>

                                                </p>
                                                <span>Email ID</span>
                                                <p>
                                                    <asp:Label ID="lblEmailID" runat="server" Text="" Style="font-size: 12.86pt; color: #39435b; font-weight: 300;"></asp:Label></p>

                                                <span style="display:none;">Pin Code</span>
                                                <p style="display:none;">
                                                    <asp:Label ID="lblPinCode" runat="server" Text="" Style="font-size: 12.86pt; color: #39435b; font-weight: 300;"></asp:Label></p>
                                                <script>
                                                    function SamePreviousMobileNo(chkPassport) {
                                                        //var dvPassport = document.getElementById("dvPassport");
                                                        //dvPassport.style.display = chkPassport.checked ? "block" : "none";

                                                        var customerMobileNo = document.getElementById("<%=lblMobile.ClientID %>").innerText;

                                                        //alert(customerMobileNo);

                                                        if (chkPassport.checked) {
                                                            document.getElementById("<%=txtMobileNo.ClientID %>").value = customerMobileNo;
                                                        }
                                                    }
                                                </script>

                                                <label>
                                                    <%--<asp:CheckBox ID="cbPreviousMobileNo" runat="server"  onclick="SamePreviousMobileNo()"/>--%>
                                                    <%-- OnCheckedChanged="cbPreviousMobileNo_CheckedChanged" --%>
                                                    <%--<input type="checkbox" onclick="SamePreviousMobileNo()" >--%>
                                                    <asp:CheckBox ID="chkPassport" Text="Same as previous mobile" runat="server" onclick="SamePreviousMobileNo(this)" />

                                                </label>
                                                <span>Kindly ensure to update correct mobile number. All updates (SMS and OTP for the fitment) will shared over this number from time to time.  </span>
                                                <div class="clearfix ish5">
                                                    <div class="width80 width70">
                                                        <div class="form-data clearfix">
                                                            <div class="control" style="width: 100%;">
                                                                <label>Mobile No.*</label>
                                                                <asp:TextBox type="text" runat="server" TextMode="Number" ClientIDMode="Static"
                                                                    ID="txtMobileNo" MaxLength="10"
                                                                    autocomplete="off"
                                                                    onkeypress="return isNumberKey(event)"
                                                                    ondrop="return false;"> </asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <p class="notes" style="margin-top: 25px;">Note: Kindly Verify and pay HSRP Fee online.</p>
                                            </div>
                                            <div class="width50">
                                                <div class="right-pay">
                                                    <div class="popup">
                                                        <table>
                                                            <thead>
                                                                <tr>
                                                                    <td colspan="2" class="text-center">Description</td>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td><span>HSRP Cost (Included Fitment Cost)</span></td>
                                                                    <td>
                                                                        <asp:Label ID="lblBasicAmt" runat="server" Text="Rs. 0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr style="display: none;">
                                                                    <td><span>Handling and<br>
                                                                        Fitment Charges</span></td>
                                                                    <td>
                                                                        <asp:Label ID="lblFitmentCharge" runat="server" Text="Rs. 0"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><span>Convenience Fee</span></td>
                                                                    <td>
                                                                        <asp:Label ID="lblConvenienceFee" runat="server" Text="Rs. 0"></asp:Label></td>
                                                                </tr>
                                                                <%--<tr id="tr1" runat="server" style="display: none;">--%>
                                                                <tr id="trDeliveryCharge" runat="server">
                                                                    <td><span>Delivery Charge</span></td>
                                                                    <td>
                                                                        <%--<asp:Label ID="lblDeliveryCharge" runat="server" Text="Rs. 0" style="display: none;"></asp:Label></td>--%>
                                                                        <asp:Label ID="lblDeliveryCharge" runat="server" Text="Rs. 0"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><span>Gross Total</span></td>
                                                                    <td>
                                                                        <asp:Label ID="lblTotalBasicAmt" runat="server" Text="Rs. 0"></asp:Label></td>
                                                                </tr>
                                                                <tr class="removeline" id="divtrGST" style="display: none;">
                                                                    <td><span>GST @ 18%</span></td>
                                                                    <td>
                                                                        <asp:Label ID="lblGST" runat="server" Text="Rs. 0"></asp:Label></td>
                                                                </tr>
                                                                <tr class="removeline" id="divtrIGST" runat="server" style="display: none;">
                                                                    <td><span>IGST @ 18%</span></td>
                                                                    <td>
                                                                        <asp:Label ID="lblIGST" runat="server" Text="Rs. 0"></asp:Label></td>
                                                                </tr>
                                                                <tr class="removeline" id="divtrCGST" runat="server">
                                                                    <td><span>CGST @ 9%</span></td>
                                                                    <td>
                                                                        <asp:Label ID="lblCGST" runat="server" Text="Rs. 0"></asp:Label></td>
                                                                </tr>
                                                                <tr class="removeline" id="divtrSGST" runat="server">
                                                                    <td><span>SGST @ 9%</span></td>
                                                                    <td>
                                                                        <asp:Label ID="lblSGST" runat="server" Text="Rs. 0"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">&nbsp;</td>
                                                                </tr>
                                                            </tbody>
                                                            <tfoot>
                                                                <tr class="btold">
                                                                    <td>Total Cost</td>
                                                                    <td>
                                                                        <asp:Label ID="lblGrandTotal" runat="server" Text="Rs. 0" Style="font-size: 12.86pt; color: #000;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tfoot>
                                                        </table>
                                                    </div>
                                                    <div class="left" style="margin-bottom: 15px;">
                                                        <%--<label>
                                                            <asp:CheckBox ID="cbAgree" runat="server" />
                                                            I agree to the Terms & Condition.
                                                        </label>
                                                        <label>
                                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                                            I agree to the Terms & Condition.
                                                        </label>
                                                        <label>
                                                            <asp:CheckBox ID="CheckBox2" runat="server" />
                                                            I agree to the Terms & Condition.
                                                        </label>--%>

                                                        <table style="border-collapse: separate; border-spacing: 10px;">
                                                            <tr>
                                                                <td style="vertical-align: top;">a.</td>

                                                                <td>I am the registered owner of the above mentioned vehicle.</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top;">b.</td>

                                                                <td>I confirm that above details entered by me, has been verified from the vehicle registration certificate.</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top;">c.</td>
                                                                <td>Company will not be held responsible for any wrong inputs provided by me.</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top;">
                                                                    <asp:CheckBox ID="cbAgree" runat="server" />
                                                                </td>
                                                                <td>I agree to the Terms and Condition.</td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="center">
                                                        <%--<a href="#" style="float: left;" class="btn isforwardprint">Add More Products</a>--%>
                                                        <!--<a href="page15.html" style="float: right" class="filled btn isforwardprint">Pay Online</a>-->
                                                        <asp:Button ID="btnPayNow" runat="server" OnClick="btnPayNow_Click" Text="Pay Online" class="filled btn isforwardsummary" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>

                        </div>
                    </section>
                </div>

                
                <footer>
                    <ul>

                        <li><a href="../BookMyHSRP_ContactUs.aspx" style="color: black"><b>Contact Us</b></a></li>
                        <li><a href="../Document/Pincode.pdf" target="_blank" style="color: black"><b>PIN FOR HOME DELIVERY</b></a></li>
                        <%--<li><a href="#">Latest Update</a></li>--%>
                        <li><a href="../BookMyHSRP_HSRPFAQ.aspx" style="color: black"><b>FAQ</b></a></li>
                        <li><a href="../BookMyHSRP_privacypolicy.aspx">Privacy Policy</a></li>
                        <li><a href="../assets/pdf/Refund_cancellation.pdf" target="_blank">Refund & Cancellation Policy</a></li>
                        <li><a href="../BookMyHSRP_safe-secure-transaction.aspx">Safe & Secure Transaction</a></li>
                        <li><a href="../BookMyHSRP_termsofuse.aspx">Terms & Conditions</a></li>
                        <%--<li><a href="#">HSRP Rate</a></li>--%>
                        <li><a href="../GovNotification.aspx">Government Notifications</a></li>
                        <li><a href="../assets/pdf/HSRP_features.pdf" target="_blank">About HSRP</a></li>
                    </ul>
                    <p class="right" style="width: 100px;">Customer Care
                        <br />
                        1800 1200 201</p>
                    <p class="copyright" style="width: 160px;">© 2020 BOOKMYHSRP</p>
                </footer>
            </div>

            <script src="../assets/js/jquery.min.js"></script>
            <script src="../assets/js/bootstrap-datepicker.min.js"></script>
            <script src="../assets/js/owl.carousel.js"></script>
            <script src="../assets/js/app.js?1603186240"></script>

        </div>


    </form>

    <asp:Literal ID="Literal1" runat="server"></asp:Literal>


</body>
</html>
