<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="dealerinformation.aspx.cs" Inherits="BMHSRPv2.plate.dealerinformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
   <%-- <script src="https://maps.googleapis.com/maps/api/js?signed_in=true&libraries=places&v=3.exp&key='your google api key here'"></script>--%>
    <%--<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?libraries=places&key=AIzaSyABiLCgMQ26APlMItwz4P-CHDovYgmT0kI"></script>--%>
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.4.0/dist/leaflet.css" integrity="sha512-puBpdR0798OZvTTbP4A8Ix/l+A4dHDD0DGqYW6RQ+9jxkRFclaxxQb/SJAWZfWAkuyeQUytO7+7N4QKrDh+drA==" crossorigin="" />
    <script src="https://unpkg.com/leaflet@1.4.0/dist/leaflet.js" integrity="sha512-QVftwZFqvtRNi0ZyCtsznlKSWOStnDORoefr1enyq5mVL4tmKB3S/EnC3rRJcxCPavG10IcrVGSmPh6Qw5lwrg==" crossorigin=""></script>


    <!-- Load Esri Leaflet from CDN -->
    <script src="https://unpkg.com/esri-leaflet@2.2.3/dist/esri-leaflet.js" integrity="sha512-YZ6b5bXRVwipfqul5krehD9qlbJzc6KOGXYsDjU9HHXW2gK57xmWl2gU6nAegiErAqFXhygKIsWPKbjLPXVb2g==" crossorigin=""></script>


    <!-- Load Esri Leaflet Geocoder from CDN -->
    <link rel="stylesheet" href="https://unpkg.com/esri-leaflet-geocoder@2.2.13/dist/esri-leaflet-geocoder.css" integrity="sha512-v5YmWLm8KqAAmg5808pETiccEohtt8rPVMGQ1jA6jqkWVydV5Cuz3nJ9fQ7ittSxvuqsvI9RSGfVoKPaAJZ/AQ==" crossorigin="">
    <script src="https://unpkg.com/esri-leaflet-geocoder@2.2.13/dist/esri-leaflet-geocoder.js" integrity="sha512-zdT4Pc2tIrc6uoYly2Wp8jh6EPEWaveqqD3sT0lf5yei19BC1WulGuh5CesB0ldBKZieKGD7Qyf/G0jdSe016A==" crossorigin=""></script>

  
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
        <div class="backbtn" data-act="page7">
        <a href="Delivery.aspx" class="gobacks"><img src="../assets/img/back.svg"></a>
    </div>
</div>            <div class="color5 view-data">
                <div class="in_pages page1 clearfix">
                    <div class="leftside color_yellow">
      <%--                  <div class="head">
                            <img src="../assets/img/h1.png" draggable="false">
                        </div>
                        <div class="card">
                            <div class="label_card bikefill">
                                <img src="../assets/img/bike-fill.svg" draggable="false">
                            </div>
                        </div>
                        <div class="card">
                            <div class="label_card">
                                <img src="../assets/img/color0.svg" draggable="false">
                            </div>
                        </div>
                        <div class="card">
                            <div class="state label_card">
                                <p><span>HR</span>Haryana</p>
                            </div>
                        </div>
                        <div class="card">
                            <div class="label_card bikefill">
                                <img src="../assets/img/car-fill1.svg" draggable="false">
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
                   
                    <div class="rightside clearfix databutton">
                        <div class="width50 addedspace inner_subpage width70" style="display:block;">
                            <h4>Home Delivery</h4>
                            <p class="map_frame">Select the nearest location on the map provided:</p>
                           <%-- <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m12!1m3!1d21009730.5997248!2d83.71702172776996!3d20.658923650525495!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!5e0!3m2!1sen!2sin!4v1602571954441!5m2!1sen!2sin" height="350" frameborder="0" style="border:0;" allowfullscreen="" aria-hidden="false" tabindex="0"></iframe>--%>
                            <%-- <div id="dvMap" style="width: 400px; height: 400px">
    </div>--%>
                            <div id="map"  style="width: 400px; height: 400px"></div>
                            <div class="form-data clearfix">
                                <div class="control">
                                     <h4  id="CheckAvailabilityMsg"  style="display:none"  class="orname">The service is not Available in this area!</h4>
                                </div>
                            </div>


                            <div class="form-data clearfix">
                                <div class="control">
                                    <label>Address Line 1*</label>
                                    <input id="add1" type="text" placeholder="Click to Enter">
                                </div>
                                <div class="control">
                                    <label>Address Line 2*</label>
                                    <input id="add2" type="text" placeholder="Click to Enter">
                                </div>
                            </div>
                            <div class="form-data clearfix">
                                <div class="control">
                                    <label>City</label>
                                    <input id="city" type="text" value="" disabled="disabled">
                                </div>
                                <div class="control">
                                    <label>State</label>
                                    <input id="states" type="text" value="" disabled="disabled">
                                </div>
                            </div>
                            <div class="form-data clearfix">
                                <div class="control">
                                    <label>Pincode</label>
                                    <input id="pincode" type="text" value=""  disabled="disabled">
                                </div>
                                <div class="control">
                                    <label>Landmark</label>
                                    <input id="landmark" type="text" placeholder="Click to Enter">
                                </div>
                            </div>

                            <div class="form-data clearfix">
                                <div class="control">
                                 <a id="btnDealerAppointment" style="display:none" href="Dealers.aspx" class="filled btn isforwardsummary">Dealer Appointment</a>
                                </div>
                                <div class="control">
                                    
                                </div>
                            </div>
                             




                        </div>
                        <div class="isbottom">
                            <a id="setadd" class="filled btn isforwardsummary">Next</a>
                        </div>
                    </div>
                </div>

            </div>
        </section>
    </div>
     <script>
         var mapAddress1, mapAddress2, mapCity, mapStates, mapPincode, mapLandmark;
         //comment due to google api key not working
         //window.onload = function () {
         //    var mapOptions = {
         //        center: new google.maps.LatLng(28.5904833, 77.3317246),
         //        zoom: 10,
         //        mapTypeId: google.maps.MapTypeId.ROADMAP
         //    };
         //    var infoWindow = new google.maps.InfoWindow();
         //    var latlngbounds = new google.maps.LatLngBounds();
         //    var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
         //    google.maps.event.addListener(map, 'click', function (e) {
         //        var latlng = new google.maps.LatLng(e.latLng.lat(), e.latLng.lng());
         //        var geocoder = geocoder = new google.maps.Geocoder();
         //        geocoder.geocode({ 'latLng': latlng }, function (results, status) {
         //            if (status == google.maps.GeocoderStatus.OK) {
         //                if (results[1]) {
         //                    alert("Location: " + results[1].formatted_address + "\r\nLatitude: " + e.latLng.lat() + "\r\nLongitude: " + e.latLng.lng());
         //                }
         //            }
         //        });
         //    });
         //}
         var map = L.map('map').setView([28.5904833, 77.3317246], 5);

         L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
             attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
         }).addTo(map);

         var geocodeService = L.esri.Geocoding.geocodeService();

         map.on('click', function (e) {
             geocodeService.reverse().latlng(e.latlng).run(function (error, result) {
                 console.log(result);
                 mapAddress1 = result.address.LongLabel;
                 mapAddress2 = result.address.Address;
                 mapCity = result.address.City;
                 mapStates = result.address.Region;
                 mapPincode = result.address.Postal;
                 mapLandmark = result.address.Subregion

                 $("#add1").val(mapAddress1);
                 $("#add2").val(mapAddress2);
                 $("#city").val(mapCity);
                 $("#states").val(mapStates);
                 $("#landmark").val(mapLandmark);
                 $("#pincode").val(mapPincode);
                 // L.marker(result.latlng).addTo(map).bindPopup(result.address.LongLabel).openPopup();
             });
         });

         var message;

         //message = geocodeService.reverse().latlng([40.725, -73.985]).run(function (error, result) {
         //    //alert(result.address.Match_addr); //this alert works here ok and can retur addrress
         //    return result.address.Match_addr;
         //});
         $(document).ready(function () {
             $('#setadd').click(function () {

                 var _mapaddress = mapAddress1 + '@' + mapAddress2 + '@' + mapCity + '@' + mapStates + '@' + mapPincode + '@' +mapLandmark;
                 //alert(_mapaddress);
                 // $("#MainContent_hdnVehicleClass").val(status);

                 jQuery.ajax({
                     type: "GET",
                     url: "api/Get/SetMapAddress?_mapaddress=" + _mapaddress,
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function (response) {
                         data = response;
                         if (data == "Available") {
                             $('#btnDealerAppointment').hide();
                             $('#CheckAvailabilityMsg').hide();
                             window.location.href = "AppointmentSlot.aspx";
                         }
                         else if (data == "Not Available") {
                             $('#CheckAvailabilityMsg').show();
                             $('#btnDealerAppointment').show();
                         }
                         else
                         {
                             $('#CheckAvailabilityMsg').hide();
                             $('#btnDealerAppointment').hide();
                             //alert(data);
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
