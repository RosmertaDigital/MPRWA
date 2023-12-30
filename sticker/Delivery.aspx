<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delivery.aspx.cs" Inherits="BMHSRPv2.sticker.Delivery" %>

<%@ Register Src="~/sticker/ucStepBar.ascx" TagPrefix="uc1" TagName="ucStepBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script type="text/javascript">
        $(document).ready(function () {
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
                        if (data == "Available") {
                            $('#DivCheckAvailability').hide();
                            $('#DivDeliveryAvailabe').show();
                            $('#txtdeliverypincode').val(pincode);


                        } else {
                            $('#DivOR').hide();
                            $('#DivUseMyLocation').hide();
                            $('#CheckAvailabilityMsg').show();
                            $('#btnDealerAppointment').show();

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



    </script>--%>

    <script type="text/javascript">
        $(document).ready(function () {
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

                if (Deliverypincode == '') {
                    alert('Please Enter Pincode');
                    return false;
                }
                else if (DeliveryAddress1 == '') {
                    alert('Please Enter Address1');
                    return false;
                }
                else if (DeliveryAddress2 == '') {
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
                        else {
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
            <uc1:ucStepBar runat="server" id="ucStepBar" PageStep="6" BackPage="DeliveryPoint.aspx" />
            
             <div class="color5 view-data">
                <div class="in_pages page1 clearfix">
                    <div class="leftside color_yellow" style="display:none">
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

                             <h4  id="CheckAvailabilityMsg"  style="display:none"  class="orname">The service is not Available in this area!</h4>

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
                                    <input id="txtdeliveryCity" readonly="readonly" type="text" value="">
                                </div>
                                <div class="control">
                                    <label>State</label>
                                    <input id="txtdeliveryState" readonly="readonly" type="text"  value=""> <%--readonly="readonly"--%>
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
