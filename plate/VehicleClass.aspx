<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VehicleClass.aspx.cs" Inherits="BMHSRPv2.plate.VehicleClass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.ftype').click(function () {
                var VehicleClass = $(this).attr('id');

                if (VehicleClass == '' || VehicleClass==null)
                {
                    alert('Please Select Vehicle Class');
                    return false;

                }

                var ImagePath = $('#' + VehicleClass).find('.style1 img').map(function () { return this.src; }).get();
                //alert(ImagePath);
                $.ajax({
                    type: "GET",
                    url: "api/Get/GetVehicleClass?VehicleClass=" + VehicleClass + "&VehicleClassImgPath=" + ImagePath,

                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        data = response;
                        if (data == "Success") {
                            window.location.href = "FuelType.aspx";
                        } else
                        {
                            alert(data);
                        }
                   
                      
                    },
                    failure: function (response) {
                        //alert(response);
                    }
                });
            });


        });

    </script>
       <div class="app clearfix page3">
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
        <div class="backbtn" data-act="page3">
        <a href="StateType.aspx" class="gobacks"><img src="../assets/img/back.svg"></a>
    </div>
</div>
            <a id="Non-Transport" class="bars pages color3 ftype">
                <div class="short">
                    <div class="img style1" style="display:none;"><img src="../assets/img/car.svg" ></div>
                    <div class="img style2 hidetemp" style="display:none;"><img src="../assets/img/car.svg" ></div>
                    <p><b>PRIVATE VEHICLE<br>
                        (NON-TRANSPORT) - WHITE BACKGROUNG PLATE</b></p>
                </div>
            </a>
            <a id="Transport" class="bars pages color5 ftype">
                <div class="short">
                    <div class="img style1" style="display:none;"><img src="../assets/img/truck-big.svg"></div>
                    <div class="img style2 hidetemp" style="display:none;"><img src="../assets/img/truck-big.svg"></div>
                    <p><b>COMMERCIAL VEHICLE<br>
                        (TRANSPORT) - YELLOW BACKGROUNG PLATE</b></p>
                </div>
            </a>
        </section>
    </div>

</asp:Content>
