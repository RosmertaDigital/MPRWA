<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delivery.aspx.cs" Inherits="BMHSRPv2.plate.Delivery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            $('#btnRepincode').click(function () {
                var pincode = $('#txtpincode').val();
                var mobile = $('#txtremobile').val();
                $.ajax({

                    type: "GET",
                    url: "api/Get/insertPincodequery?pincode=" + pincode + "&mobile="+mobile,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        data = response;
                        //alert(data);
                        if (data == "1") {
                            $('#txtpincode').val('');
                            $('#txtremobile').val('');
                            $('#CheckAvailabilityMsg2').show();
                            $('#CheckAvailabilityMsg').hide();



                        }

                    }


                })



            });


            $('#CheckAvailability').click(function () {

                var pincode = $('#txtpincode').val();
                // var status = $(this).attr('id');
                // alert(pincode);
                // $("#MainContent_hdnVehicleClass").val(status);
                if (pincode == '') {
                    alert('Please Enter Pincode');
                    return false;
                }

                $.ajax({
                    type: "GET",
                    url: "api/Get/CheckAvailability?SessionValue=" + pincode,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        data = response;
                        //alert(data);
                        if (data.Status == "1") {
                            $('#DivCheckAvailability').hide();
                            //$('#remobile').hide();
                            $('#reentry').hide();
                            $('#DivDeliveryAvailabe').show();
                            $('#txtdeliverypincode').val(pincode);
                            $('#txtdeliveryCity').val(data.DeliveryCity);
                            $('#txtdeliveryState').val(data.DeliveryState);
                           

                        }
                        else {
                            $('#DivOR').hide();
                            $('#DivUseMyLocation').hide();
                            $('#CheckAvailabilityMsg').show();
                            $('#btnDealerAppointment').show();
                            $('#reentry').show();

                        }



                    },
                    failure: function (response) {

                        alert(response);
                    }
                });
            });


            $('#btnHomeDeliveryNext').click(function () {


                //var status = jQuery(this).attr('id');
                //alert(status);

                var Deliverypincode = $('#txtpincode').val();
               
                var DeliveryAddress1 = $('#txtdeliveryaddress1').val();
               
                var DeliveryAddress2 = $('#txtdeliveryaddress2').val();
               
                var deliveryCity = $('#txtdeliveryCity').val();
                var deliveryState = $('#txtdeliveryState').val();
                var DeliveryLandmark = $('#txtDeliveryLandmark').val();

                if (Deliverypincode == '')
                {
                    alert('Please Enter Pincode');
                    return false;
                }
                else if (DeliveryAddress1 == '')
                {
                    alert('Please Enter Address1');
                    return false;
                }
                else if (DeliveryAddress2 == '')
                {
                    alert('Please Enter Address2');
                    return false;
                }
                else if (deliveryCity == '') {
                    alert('Please Enter City');
                    return false;
                }
                else if (deliveryState == '') {
                    alert('Please Enter State');
                    return false;
                }
                jQuery.ajax({
                    type: "GET",
                    url: "api/Get/DeliveryInfo?Pincode=" + Deliverypincode + "&Address1=" + DeliveryAddress1 + "&Address2=" + DeliveryAddress2 + "&city=" + deliveryCity + "&State=" + deliveryState + "&Landmark=" + DeliveryLandmark,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        data = response;
                        if (data == "Success") {
                            window.location.href = "AppointmentSlot.aspx";
                        }
                        else
                        {
                            alert(data);
                        }
                       


                    },
                    failure: function (response) {
                        alert(response);
                    }
                });
            });
        });



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app clearfix">
        <section class="after">
       <%--     <div class="steps bars color1">
    <ul>
        <li><a href="#"><span>Step 1</span> Vehicle Make</a></li>
        <li><a href="#"><span>Step 2</span> State</a></li>
        <li><a href="#"><span>Step 3</span> Vehicle Class</a></li>
        <li><a href="#"><span>Step 4</span> Fuel Type</a></li>
        <li><a href="#"><span>Step 5</span> Vehicle Type</a></li>
        <li><a href="#"><span>Step 6</span> Booking Details</a></li>
        <li><a href="#"><span>Step 7</span> Delivery</a></li>
        <li><a href="#"><span>Step 8</span> Dealer Information</a></li>
        <li><a href="#"><span>Step 9</span> Appointment Slot</a></li>
        <li><a href="#"><span>Step 10</span> Booking Summary</a></li>
        <li><a href="#"><span>Step 11</span> Payment</a></li>
        <li><a href="#"><span>Step 12</span> Customer Receipt</a></li>
    </ul>
        <div class="backbtn" data-act="page7">
        <a href="DeliveryPoint.aspx" class="gobacks"><img src="../assets/img/back.svg"></a>
    </div>
</div>    --%>      
                                               <div class="steps bars color1">
                <ul>
        <li><a href="#"><span>Step 1</span> Booking Details</a></li>
        <li><a href="#"><span>Step 2</span> Fitment Location</a></li>
        <li><a href="#"><span>Step 3</span> Appointment Slot</a></li>
        <li><a href="#"><span>Step 4</span> Booking Summary</a></li>
        <li><a href="#"><span>Step 5</span> Verify Details & Pay</a></li>
        <li><a href="#"><span>Step 6</span> Download Receipt </a></li>
    </ul>
                <div class="backbtn" data-act="page2">
                    <a href="DeliveryPoint.aspx"  class="gobacks">
                        <img src="../assets/img/back.svg"></a>
                </div>
            </div>

            <div class="color5 view-data">
                <div class="in_pages page1 clearfix">
                    <div class="leftside color_yellow" style="display:none">
                        <div class="head">
                           <%-- <img src="../assets/img/h1.png" draggable="false">--%>
                             <asp:Literal ID="LiteralBookingTypeImage" runat="server"></asp:Literal>
                        </div>
                        <div class="card">
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
                    </div>
                    <div id="DivCheckAvailability" class="rightside">
                        <div class="addedspace">
                            <h4>Home Delivery</h4>

                            <div class="form-data clearfix">
                                <div class="control width40">
                                    <label>Pincode</label>
                                    <input id="txtpincode" maxlength="6"  onkeypress="return isNumberKey(event)" 
                                                autocomplete="off" type="text">
                                </div>
                                <div class="control">
                                    <label> &nbsp; </label>
                                    <a id="CheckAvailability" class="filled btn isforwardsummary small">Check Availability</a>
                                </div>
                            </div>

                            <h5 id="DivOR" class="orname" style="display:none;">- OR -</h5>

                            <a id="DivUseMyLocation" href="dealerinformation.aspx" class="filled btn isforwardsummary" style="display:none;">Use My Location</a>

                             <%--<h4  id="CheckAvailabilityMsg"  style="display:none"  class="orname">The service is not Available in this area!</h4>--%>
                            <h4  id="CheckAvailabilityMsg"  style="display:none"  class="orname">Currentaly service is not Available in this area.Please Enter your mobile number,So when service resume on this pincode we ping you back !</h4>
                             <h4  id="CheckAvailabilityMsg2"   style="display:none; color:green"  class="orname">Thank you. You will be updated as soon as the appointment system is available. !</h4>
                             <div class="form-data clearfix" id="reentry" style="display:none">
                                <div class="control width40">
                                    <label >MobileNo.</label>
                                    <input id="txtremobile" maxlength="10"  onkeypress="return isNumberKey(event)"
                                                autocomplete="off" type="text">
                                     <a id="btnRepincode"  class="filled btn isforwardsummary">Submit</a>
                                </div>
                             
                            </div>

<%--

                             <asp:TextBox ID="remobile" style="display:none" ></asp:TextBox> 
                            <asp:Button ID="btnRepincode" style="display:none"  Text="Submit" />--%>

                            <br/>

                            <a id="btnDealerAppointment" style="display:none" href="Dealers.aspx" class="filled btn isforwardsummary">Dealer Appointment</a>

                        </div>
                    </div>

                      <div id="DivDeliveryAvailabe" style="display:none"  class="rightside">
                        <div class="addedspace">
                            <h4>Home Delivery</h4>

                            <div class="form-data clearfix">
                                <div class="control">
                                    <label>Pincode</label>
                                    <input readonly="readonly" id="txtdeliverypincode" type="text" >
                                </div>
                                <div class="control">

                                </div>
                            </div>


                            <div class="form-data clearfix">
                                <div class="control">
                                    <label>Address Line 1*</label>
                                    <input id="txtdeliveryaddress1" type="text" placeholder="Click to Enter">
                                </div>
                                <div class="control">
                                    <label>Address Line 2*</label>
                                    <input id="txtdeliveryaddress2" type="text" placeholder="Click to Enter">
                                </div>
                            </div>
                            <div class="form-data clearfix">
                                <div class="control">
                                    <label>City</label>
                                    <input  readonly="readonly"  id="txtdeliveryCity" type="text" >
                                </div>
                                <div class="control">
                                    <label>State</label>
                                    <input  readonly="readonly"  id="txtdeliveryState" type="text" >
                                </div>
                            </div>
                            <div class="form-data clearfix">
                                <div class="control">
                                    <label>Landmark</label>
                                    <input id="txtDeliveryLandmark" type="text" placeholder="Click to Enter">
                                </div>
                                <div class="control">

                                </div>
                            </div>


                            <h4 class="orname"> &nbsp; </h4>

                            <div class="text-right">
                                <a id="btnHomeDeliveryNext"   class="filled btn isforwardsummary">Next</a>
                            </div>


                        </div>
                    </div>
                </div>

            </div>
        </section>
    </div>
</asp:Content>
