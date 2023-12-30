<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vehicletype.aspx.cs" Inherits="BMHSRPv2.plate.Vehicletype" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="app clearfix">
        <section class="after">
            <div class="steps bars color1">
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
        <div class="backbtn" data-act="page5">
        <a href="FuelType.aspx" class="gobacks"><img src="../assets/img/back.svg"></a>
    </div>
</div>    
    <div class="page5_sec">
         <asp:Literal ID="showvehicle_cat" runat="server"></asp:Literal>

                <%--<div class="table_3">
                    <div class="bars pages color3">
                        <div class="short">
                            <div class="img style1"><img src="../assets/img/S1-01.svg"></div>
                            <p>Two Wheelers</p>
                        </div>
                        <div class="upper_view">
                            <img src="../assets/img/S1-01-w.svg">
                            <div class="clearfix">
                                <div class="control">
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="1">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="2">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                </div>
                                <div class="control">
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="1">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="2">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                </div>
                                <div class="control">
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="1">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="2">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                </div>
                                <div class="control text-right">
                                    <a href="page6.html" class="btn">Next</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="bars pages color1">
                        <div class="short">
                            <div class="img style1"><img src="../assets/img/s2-01.svg"></div>
                            <p>Motorcycle</p>
                        </div>
                        <div class="upper_view">
                            <img src="../assets/img/s2-01-w.svg">
                            <div class="clearfix">
                                <div class="control">
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="1">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="2">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                </div>
                                <div class="control">
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="1">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="2">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                </div>
                                <div class="control">
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="1">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="2">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                </div>
                                <div class="control text-right">
                                    <a href="page6.html" class="btn">Next</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="bars pages color3">
                        <div class="short">
                            <div class="img style1"><img src="../assets/img/R1-01.svg"></div>
                            <p>Three Wheelers</p>
                        </div>
                        <div class="upper_view">
                            <img src="../assets/img/R1-01-w.svg">
                            <div class="clearfix">
                                <div class="control">
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="1">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="2">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                </div>
                                <div class="control">
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="1">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="2">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                </div>
                                <div class="control">
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="1">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="2">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                </div>
                                <div class="control text-right">
                                    <a href="page6.html" class="btn">Next</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>
      

                <%--<div class="table_3">
                    <div class="bars pages color5">
                        <div class="short">
                            <div class="img style1"><img src="../assets/img/car.svg"></div>
                            <p>Four Wheelers</p>
                        </div>
                        <div class="upper_view">
                            <img src="../assets/img/car-w.svg">
                            <div class="clearfix">
                                <div class="control">
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="1">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="2">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                </div>
                                <div class="control">
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="1">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="2">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                </div>
                                <div class="control">
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="1">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="2">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                </div>
                                <div class="control text-right">
                                    <a href="page6.html" class="btn">Next</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="bars pages color2">
                        <div class="short">
                            <div class="img style1"><img src="../assets/img/truck-big.svg"></div>
                            <p>Other Vehicles</p>
                        </div>
                        <div class="upper_view">
                            <img src="../assets/img/truck-big-w.svg">
                            <div class="clearfix">
                                <div class="control">
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="1">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="2">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                </div>
                                <div class="control">
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="1">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="2">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                </div>
                                <div class="control">
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="1">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="2">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                </div>
                                <div class="control text-right">
                                    <a href="page6.html" class="btn">Next</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="bars pages color4">
                        <div class="short">
                            <div class="img style1"><img src="../assets/img/truck.svg"></div>
                            <p>Other Vehicles</p>
                        </div>
                        <div class="upper_view">
                            <img src="../assets/img/truck-w.svg">
                            <div class="clearfix">
                                <div class="control">
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="1">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="2">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                </div>
                                <div class="control">
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="1">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="2">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                </div>
                                <div class="control">
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="1">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                    <div class="radio">
                                        <input type="radio" name="dealer_info" class="rdo1" value="2">
                                        <i></i>
                                        <span>Lorem Ipsum</span>
                                    </div>
                                </div>
                                <div class="control text-right">
                                    <a href="page6.html" class="btn">Next</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>

            </div>
              </section>
    </div>
    <script>
   $(document).ready(function () {
     //  $('#setvehicletype').click(function () {
           $('.vtype').click(function () {
                 var sel_radio= $('input[name="vehicle_Category"]:checked').val();
                 //alert(sel_radio);
                 if (sel_radio == 'undefined') {
                     alert('Please Select Vehicle Type');
                     return false;
               }

               var Path = $(this).parent().parent().parent().parent().find('img').map(function () { return this.src; }).get();
               //alert(Path);
                 jQuery.ajax({
                     type: "GET",
                     url: "api/Get/SetVehicleType?radiovehicletype=" + sel_radio + "&VehicleTypeImage=" + Path,
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function (response) {
                         data = response;
                         if (data == "Success") {
                             window.location.href = "BookingDetail.aspx";
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
