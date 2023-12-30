<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dealers.aspx.cs" Inherits="BMHSRPv2.plate.Dealers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            $('.DealerClick').click(function () {


                var DealerAffixationid = jQuery(this).attr('id');

                // alert(DealerAffixationid);

                if (DealerAffixationid == '' || DealerAffixationid == null) {
                    alert('Please Select Your Dealer');
                    return false;
                }

                jQuery.ajax({
                    type: "GET",
                    url: "api/Get/SetDealerAffixationCenter?SessionValue=" + DealerAffixationid,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        data = response;
                        if (data == "Success") {
                            window.location.href = "AppointmentSlot.aspx";
                        } else {
                            alert(data);
                        }
                    },
                    failure: function (response) {
                        alert(response);
                    }
                });
            });




            $('#btnsearch').click(function () {

                var selectedCity = $('#ContentPlaceHolder1_ddlcity').children("option:selected").val().toLowerCase();

                //alert(selectedCity);
                if (selectedCity == 'all') {
                    selectedCity = '';
                }
                $(".dealer").filter(function () {

                    $(this).toggle($(this).text().toLowerCase().indexOf(selectedCity) > -1)
                });


            });

            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $(".dealer ").filter(function () {
                    // $(this).toggle($(this).attr('title').toLowerCase().indexOf(value) > -1)
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="app clearfix">
        <section class="after">
           <%-- <div class="steps bars color1">
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
                <div class="backbtn" data-act="page8">
                    <a href="DeliveryPoint.aspx" class="gobacks">
                        <img src="../assets/img/back.svg"></a>
                </div>
            </div>--%>
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
                    <a href="BookingDetail.aspx" style="display:none" class="gobacks">
                        <img src="../assets/img/back.svg"></a>
                </div>
            </div>
            <div class="color2 view-data">
                <div class="in_pages page1 clearfix">
                    <style>
                        p.waiting {
                            text-align: center;
                            position: absolute;
                            left: 0;
                            width: 100%;
                            height: 100%;
                            background: #ccebf5;
                            padding-top: 45px;
                            z-index: 9;
                        }

                        .app .bars {
                            vertical-align: top;
                        }
                    </style>
                    <div class="leftside color_blue" style="display:none">
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
                    <div class="rightside">
                        <div class="addedspace">
                            <h4>Dealer Appointment</h4>

                            <div class="form-data dealer_page clearfix">
                                 <div class="control width30">
                                    <label>&nbsp; </label>
                                    <asp:DropDownList CssClass="text" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged" runat="server" ID="ddlstate">
                                        <asp:ListItem Value ="31" Text="UP"></asp:ListItem>
                                        <asp:ListItem Value ="37" Text="DL"></asp:ListItem>
                                        <asp:ListItem Value ="3" Text="HP"></asp:ListItem>

                                    </asp:DropDownList>
                                  
                                </div>
                                <div class="control width30">
                                    <label>&nbsp; </label>
                                    <asp:DropDownList CssClass="text" runat="server" ID="ddlcity"></asp:DropDownList>
                                    <%--<select type="text">
                                        <option>Select City</option>
                                        <option>City 1</option>
                                        <option>City 2</option>
                                    </select>--%>
                                </div>
                                <div class="control width50">
                                    <label>&nbsp; </label>
                                    <input id="myInput" placeholder="Search Dealer/Pincode" type="text">
                                </div>
                                <div class="control width10">
                                    <label>&nbsp; </label>
                                    <a href="#" id="btnsearch" class="filled btn isforwardsummary small">GO</a>
                                </div>
                            </div>

                            <asp:Literal runat="server" ID="LitralDealer"></asp:Literal>
                            <%-- <div class="dealer">
                                    <div class="top clearfix">
                                        <p>Shiv Ganga Automobiles</p>
                                        <span><b>Distance: </b>5 KM</span>

                                        <div class="arrow"></div>
                                    </div>
                                    <div class="subdata">
                                        <div class="arrow"></div>
                                        <p class="cmp">Shiv Ganga Automobiles</p>
                                        <div class="clearfix">
                                            <div class="width50 width30">
                                                <span><b>Distance: </b>5 KM</span>
                                            </div>
                                            <div class="width50 width30">
                                                <span><b>City: </b>NEW DELHI</span>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                            <div class="width50 widthfull">
                                                <span><b>Address: </b><br>
                                                A-1/2, Paschim Vihar, Main Rohtak Road, Near Peera Garhi Chowk,<br>New Delhi - 110063</span>
                                            </div>
                                        </div>

                                        <div class="ticks clearfix">
                                            <p class="fg1"><span>HSRP & Colour Sticker Cost:</span>Rs. 354</p>
                                            <p class="fg2"><span>Earliest Date Available:</span>14-11-2020</p>
                                            <p class="fg3"><span>Earliest Time Slot Available:</span>10:00am-11:00am</p>

                                            <a href="page10.html" class="btn filled">Confirm Dealer</a>
                                        </div>
                                    </div>
                                </div>--%>
                            <%--  <div class="dealer">
                                    <div class="top clearfix">
                                        <p>Shiv Ganga Automobiles</p>
                                        <span><b>Distance: </b>5 KM</span>

                                        <div class="arrow"></div>
                                    </div>
                                    <div class="subdata">
                                        <div class="arrow"></div>
                                        <p class="cmp">Shiv Ganga Automobiles</p>
                                        <div class="clearfix">
                                            <div class="width50 width30">
                                                <span><b>Distance: </b>5 KM</span>
                                            </div>
                                            <div class="width50 width30">
                                                <span><b>City: </b>NEW DELHI</span>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                            <div class="width50 widthfull">
                                                <span><b>Address: </b><br>
                                                A-1/2, Paschim Vihar, Main Rohtak Road, Near Peera Garhi Chowk,<br>New Delhi - 110063</span>
                                            </div>
                                        </div>

                                        <div class="ticks clearfix">
                                            <p class="fg1"><span>HSRP & Colour Sticker Cost:</span>Rs. 354</p>
                                            <p class="fg2"><span>Earliest Date Available:</span>14-11-2020</p>
                                            <p class="fg3"><span>Earliest Time Slot Available:</span>10:00am-11:00am</p>

                                            <a href="page10.html" class="btn filled">Confirm Dealer</a>
                                        </div>
                                    </div>
                                </div>
                                                                <div class="dealer">
                                    <div class="top clearfix">
                                        <p>Shiv Ganga Automobiles</p>
                                        <span><b>Distance: </b>5 KM</span>

                                        <div class="arrow"></div>
                                    </div>
                                    <div class="subdata">
                                        <div class="arrow"></div>
                                        <p class="cmp">Shiv Ganga Automobiles</p>
                                        <div class="clearfix">
                                            <div class="width50 width30">
                                                <span><b>Distance: </b>5 KM</span>
                                            </div>
                                            <div class="width50 width30">
                                                <span><b>City: </b>NEW DELHI</span>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                            <div class="width50 widthfull">
                                                <span><b>Address: </b><br>
                                                A-1/2, Paschim Vihar, Main Rohtak Road, Near Peera Garhi Chowk,<br>New Delhi - 110063</span>
                                            </div>
                                        </div>

                                        <div class="ticks clearfix">
                                            <p class="fg1"><span>HSRP & Colour Sticker Cost:</span>Rs. 354</p>
                                            <p class="fg2"><span>Earliest Date Available:</span>14-11-2020</p>
                                            <p class="fg3"><span>Earliest Time Slot Available:</span>10:00am-11:00am</p>

                                            <a href="page10.html" class="btn filled">Confirm Dealer</a>
                                        </div>
                                    </div>
                                </div>
                                                                <div class="dealer">
                                    <div class="top clearfix">
                                        <p>Shiv Ganga Automobiles</p>
                                        <span><b>Distance: </b>5 KM</span>

                                        <div class="arrow"></div>
                                    </div>
                                    <div class="subdata">
                                        <div class="arrow"></div>
                                        <p class="cmp">Shiv Ganga Automobiles</p>
                                        <div class="clearfix">
                                            <div class="width50 width30">
                                                <span><b>Distance: </b>5 KM</span>
                                            </div>
                                            <div class="width50 width30">
                                                <span><b>City: </b>NEW DELHI</span>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                            <div class="width50 widthfull">
                                                <span><b>Address: </b><br>
                                                A-1/2, Paschim Vihar, Main Rohtak Road, Near Peera Garhi Chowk,<br>New Delhi - 110063</span>
                                            </div>
                                        </div>

                                        <div class="ticks clearfix">
                                            <p class="fg1"><span>HSRP & Colour Sticker Cost:</span>Rs. 354</p>
                                            <p class="fg2"><span>Earliest Date Available:</span>14-11-2020</p>
                                            <p class="fg3"><span>Earliest Time Slot Available:</span>10:00am-11:00am</p>

                                            <a href="page10.html" class="btn filled">Confirm Dealer</a>
                                        </div>
                                    </div>
                                </div>
                                                                <div class="dealer">
                                    <div class="top clearfix">
                                        <p>Shiv Ganga Automobiles</p>
                                        <span><b>Distance: </b>5 KM</span>

                                        <div class="arrow"></div>
                                    </div>
                                    <div class="subdata">
                                        <div class="arrow"></div>
                                        <p class="cmp">Shiv Ganga Automobiles</p>
                                        <div class="clearfix">
                                            <div class="width50 width30">
                                                <span><b>Distance: </b>5 KM</span>
                                            </div>
                                            <div class="width50 width30">
                                                <span><b>City: </b>NEW DELHI</span>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                            <div class="width50 widthfull">
                                                <span><b>Address: </b><br>
                                                A-1/2, Paschim Vihar, Main Rohtak Road, Near Peera Garhi Chowk,<br>New Delhi - 110063</span>
                                            </div>
                                        </div>

                                        <div class="ticks clearfix">
                                            <p class="fg1"><span>HSRP & Colour Sticker Cost:</span>Rs. 354</p>
                                            <p class="fg2"><span>Earliest Date Available:</span>14-11-2020</p>
                                            <p class="fg3"><span>Earliest Time Slot Available:</span>10:00am-11:00am</p>

                                            <a href="page10.html" class="btn filled">Confirm Dealer</a>
                                        </div>
                                    </div>
                                </div>
                                                                <div class="dealer">
                                    <div class="top clearfix">
                                        <p>Shiv Ganga Automobiles</p>
                                        <span><b>Distance: </b>5 KM</span>

                                        <div class="arrow"></div>
                                    </div>
                                    <div class="subdata">
                                        <div class="arrow"></div>
                                        <p class="cmp">Shiv Ganga Automobiles</p>
                                        <div class="clearfix">
                                            <div class="width50 width30">
                                                <span><b>Distance: </b>5 KM</span>
                                            </div>
                                            <div class="width50 width30">
                                                <span><b>City: </b>NEW DELHI</span>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                            <div class="width50 widthfull">
                                                <span><b>Address: </b><br>
                                                A-1/2, Paschim Vihar, Main Rohtak Road, Near Peera Garhi Chowk,<br>New Delhi - 110063</span>
                                            </div>
                                        </div>

                                        <div class="ticks clearfix">
                                            <p class="fg1"><span>HSRP & Colour Sticker Cost:</span>Rs. 354</p>
                                            <p class="fg2"><span>Earliest Date Available:</span>14-11-2020</p>
                                            <p class="fg3"><span>Earliest Time Slot Available:</span>10:00am-11:00am</p>

                                            <a href="page10.html" class="btn filled">Confirm Dealer</a>
                                        </div>
                                    </div>
                                </div>
                                                                <div class="dealer">
                                    <div class="top clearfix">
                                        <p>Shiv Ganga Automobiles</p>
                                        <span><b>Distance: </b>5 KM</span>

                                        <div class="arrow"></div>
                                    </div>
                                    <div class="subdata">
                                        <div class="arrow"></div>
                                        <p class="cmp">Shiv Ganga Automobiles</p>
                                        <div class="clearfix">
                                            <div class="width50 width30">
                                                <span><b>Distance: </b>5 KM</span>
                                            </div>
                                            <div class="width50 width30">
                                                <span><b>City: </b>NEW DELHI</span>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                            <div class="width50 widthfull">
                                                <span><b>Address: </b><br>
                                                A-1/2, Paschim Vihar, Main Rohtak Road, Near Peera Garhi Chowk,<br>New Delhi - 110063</span>
                                            </div>
                                        </div>

                                        <div class="ticks clearfix">
                                            <p class="fg1"><span>HSRP & Colour Sticker Cost:</span>Rs. 354</p>
                                            <p class="fg2"><span>Earliest Date Available:</span>14-11-2020</p>
                                            <p class="fg3"><span>Earliest Time Slot Available:</span>10:00am-11:00am</p>

                                            <a href="page10.html" class="btn filled">Confirm Dealer</a>
                                        </div>
                                    </div>
                                </div>
                                                                <div class="dealer">
                                    <div class="top clearfix">
                                        <p>Shiv Ganga Automobiles</p>
                                        <span><b>Distance: </b>5 KM</span>

                                        <div class="arrow"></div>
                                    </div>
                                    <div class="subdata">
                                        <div class="arrow"></div>
                                        <p class="cmp">Shiv Ganga Automobiles</p>
                                        <div class="clearfix">
                                            <div class="width50 width30">
                                                <span><b>Distance: </b>5 KM</span>
                                            </div>
                                            <div class="width50 width30">
                                                <span><b>City: </b>NEW DELHI</span>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                            <div class="width50 widthfull">
                                                <span><b>Address: </b><br>
                                                A-1/2, Paschim Vihar, Main Rohtak Road, Near Peera Garhi Chowk,<br>New Delhi - 110063</span>
                                            </div>
                                        </div>

                                        <div class="ticks clearfix">
                                            <p class="fg1"><span>HSRP & Colour Sticker Cost:</span>Rs. 354</p>
                                            <p class="fg2"><span>Earliest Date Available:</span>14-11-2020</p>
                                            <p class="fg3"><span>Earliest Time Slot Available:</span>10:00am-11:00am</p>

                                            <a href="page10.html" class="btn filled">Confirm Dealer</a>
                                        </div>
                                    </div>
                                </div>
                                                                <div class="dealer">
                                    <div class="top clearfix">
                                        <p>Shiv Ganga Automobiles</p>
                                        <span><b>Distance: </b>5 KM</span>

                                        <div class="arrow"></div>
                                    </div>
                                    <div class="subdata">
                                        <div class="arrow"></div>
                                        <p class="cmp">Shiv Ganga Automobiles</p>
                                        <div class="clearfix">
                                            <div class="width50 width30">
                                                <span><b>Distance: </b>5 KM</span>
                                            </div>
                                            <div class="width50 width30">
                                                <span><b>City: </b>NEW DELHI</span>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                            <div class="width50 widthfull">
                                                <span><b>Address: </b><br>
                                                A-1/2, Paschim Vihar, Main Rohtak Road, Near Peera Garhi Chowk,<br>New Delhi - 110063</span>
                                            </div>
                                        </div>

                                        <div class="ticks clearfix">
                                            <p class="fg1"><span>HSRP & Colour Sticker Cost:</span>Rs. 354</p>
                                            <p class="fg2"><span>Earliest Date Available:</span>14-11-2020</p>
                                            <p class="fg3"><span>Earliest Time Slot Available:</span>10:00am-11:00am</p>

                                            <a href="page10.html" class="btn filled">Confirm Dealer</a>
                                        </div>
                                    </div>
                                </div>
                                                                <div class="dealer">
                                    <div class="top clearfix">
                                        <p>Shiv Ganga Automobiles</p>
                                        <span><b>Distance: </b>5 KM</span>

                                        <div class="arrow"></div>
                                    </div>
                                    <div class="subdata">
                                        <div class="arrow"></div>
                                        <p class="cmp">Shiv Ganga Automobiles</p>
                                        <div class="clearfix">
                                            <div class="width50 width30">
                                                <span><b>Distance: </b>5 KM</span>
                                            </div>
                                            <div class="width50 width30">
                                                <span><b>City: </b>NEW DELHI</span>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                            <div class="width50 widthfull">
                                                <span><b>Address: </b><br>
                                                A-1/2, Paschim Vihar, Main Rohtak Road, Near Peera Garhi Chowk,<br>New Delhi - 110063</span>
                                            </div>
                                        </div>

                                        <div class="ticks clearfix">
                                            <p class="fg1"><span>HSRP & Colour Sticker Cost:</span>Rs. 354</p>
                                            <p class="fg2"><span>Earliest Date Available:</span>14-11-2020</p>
                                            <p class="fg3"><span>Earliest Time Slot Available:</span>10:00am-11:00am</p>

                                            <a href="page10.html" class="btn filled">Confirm Dealer</a>
                                        </div>
                                    </div>
                                </div>--%>
                        </div>
                    </div>
                </div>

            </div>
        </section>
    </div>
</asp:Content>
