<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StateCheck.aspx.cs" Inherits="BMHSRPv2.plate.StateCheck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        // var state;

        $(document).ready(function () {
            $('#txtsubmit').click(function () {


                //var state = document.getElementById('txtHideSelectedStateID').value;
                var phoneno = $("#txtphoneno").val();

                // var StateShortName = $("#ContentPlaceHolder1_HiddenShortname").val();
                //var StateName = $("#ContentPlaceHolder1_HiddenStateName").val();

                //alert(state);
                //alert(StateShortName);
                //alert(StateName);

                if (phoneno == '' || phoneno == null || phoneno.length < 10) {
                    alert('Please Input Mobile Number');
                    return false;
                }

                jQuery.ajax({
                    type: "GET",
                    url: "api/Get/Setmobileno?mobileno=" + phoneno,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        data = response;
                        if (data == "Success") {
                            $("#divmessage").css("display", "");
                        } else {
                            alert('Something went wrong please try again!!');
                        }
                    },
                    failure: function (response) {
                        alert('Something went wrong please try again!!');
                    }
                });
                return false;
            });
        });



        //$(document).ready(function () {
        //    $('#btnstate').click(function () {
        //        alert("aa");
        //        var state = $("#ContentPlaceHolder1_HiddenVehicleCatID").val();
        //        if (state == '' || state == null) {
        //            alert('Please Select Your State');
        //            return false;
        //        }
        //        jQuery.ajax({
        //            type: "GET",
        //            url: "api/Get/Setstate?state=" + state,
        //            contentType: "application/json; charset=utf-8",
        //            dataType: "json",
        //            success: function (response) {
        //                data = response;
        //                //alert(data);
        //                $("#ContentPlaceHolder1_btnVehicleCategory").click();

        //            },
        //            failure: function (response) {
        //                alert(response);
        //            }

        //    });
        //});


        function StateSelectionFun(StateID, shortname, statename) {
            //  alert(StateID);
            $("#ContentPlaceHolder1_txtHideSelectedStateID").val(StateID);

            $("#ContentPlaceHolder1_HiddenShortname").val(shortname);
            $("#ContentPlaceHolder1_HiddenStateName").val(statename);

            // document.getElementById('txtHideSelectedStateID').value = StateID;
        }

    </script>
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
                <div class="backbtn" data-act="page2">
                    <a href="page1.html" class="gobacks">
                        <img src="../assets/img/back.svg"></a>
                </div>
            </div>
            <div class="view-data color5">
                <div class="in_pages page1 clearfix">
                    <div class="leftside">
                        <%--    <div class="head">
                            <img src="../assets/img/h1.png">
                        </div>
                        <div class="card">
                            <div class="label_card bikefill">
                                <img src="../assets/img/bike-fill.svg">
                            </div>
                        </div>
                        <div class="card">
                            <div class="label_card">
                                <img src="../assets/img/color0.svg">
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
                    </div>
                    <div class="rightside" style="margin-top: 0 !important;">
                        <div class="app inner-pages check-status-page clearfix " style="background-image: none !important;">

                            <div class="check-status-form addedspace">
                                <div class="alert alert-success" role="alert" id="divmessage" style="display: none">
                                    Thank you. You will be updated as soon as the appointment system is available.
                                </div>
                                <h4>Appointment for HSRP/Colour Coded stickers is not available.</h4>



                                <div class="form-data clearfix">
                                    <div class="control">
                                        <label for="orderno">Kindly Provide your Contact No. *</label>
                                        <input type="text" name="phoneno" id="txtphoneno" required="" maxlength="10"
                                            clientidmode="Static"
                                            autocomplete="off"
                                            onpaste="return false;"
                                            onkeypress="return isNumberKey(event)"
                                            ondrop="return false;">
                                    </div>
                                    <%-- <div class="control">
                    <label for="vehiclereg">VehicleReg No: *</label>
                    <input type="text" name="vehiclereg" id="txtvehiclereg" placeholder="DL01ABXXXX" required="">
                </div>--%>

                                    <%--  <div class="control">
                    <div class="captcha-input rightmargin-xs">
                        <label for="orderno">Captcha:</label>
                        <!--<input type="text" name="captchaorderno" id="txtcaptcha" placeholder="" required="">-->
                        <input type="text" name="Captcha" id="txtCaptcha" placeholder="Input Captcha" required="" />


                    </div>
                    <div class="captcha-img">
                        <%--<asp:Image ID="imgCaptcha" CssClass="topmargin-sm"/>
                        <!--<img  class="topmargin-sm" id="captcha" src="../assets/img/captcha.png" alt="">-->
                        <button type="button" id="btnReferesh" onclick="DrawCaptcha();">
                            <img src="../assets/img/reload-icon.png"></button>

                    </div>

                </div>--%>
                                    <%-- <div class="control">
                    <div class="captcha-input rightmargin-xs">
                        <label for="InputCaptcha">Input Captcha:</label>
                        <input type="text" id="txtInput" />
                    </div>
                </div>--%>
                                </div>



                                <div class="App clearfix">
                                    <div class="control">
                                        <p>We will update you over SMS once the appointment system is available.</p>
                                        <%--<button id ="txtSearch" onclick="return ValidCaptcha();">Search</button>--%>
                                        <button id="txtsubmit">Submit</button>

                                    </div>
                                </div>




                            </div>
                        </div>
                        <!--  <div class="head">
                        <img src="../assets/img/h1.png">
                        <p>ONLY<br>COLOUR STICKER</p>
                    </div>-->
                        <!--  <div class="head">
                        <img src="../assets/img/h1.png">
                    </div>-->
                        <%--<div class="details">
                            <h4>Select State</h4>
                            <div class="brand-selection">
                                <div class="owl-carousel owl-theme states">
                                    <div  class="item">
                                        <a href="#" id="DL" class="label stype" onclick="StateSelectionFun('37','DL','Delhi')">
                                            <p ><span class="plain">DL</span>Delhi</p>
                                        </a>
                                    </div>
                                    <div  class="item">
                                        <a href="#" id="UP" class="label stype" onclick="StateSelectionFun('31','UP','Uttar Pradesh')">
                                            <p><span class="plan1">UP</span>Uttar Pradesh</p>
                                        </a>
                                       
                                    </div>

                                </div>
                                <div class="text-right">
                                    
                                    <asp:HiddenField ID="txtHideSelectedStateID" runat="server" />

                                     <asp:HiddenField ID="HiddenShortname" runat="server" />
                                        <asp:HiddenField ID="HiddenStateName" runat="server" />
                              
                                    <a  id="btnstate" class="filled btn isforwardsummary ">Next</a>
                                   
                                
                                </div>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
