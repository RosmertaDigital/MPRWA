<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReAppointmentSlot.aspx.cs" Inherits="BMHSRPv2.ReAppointmentSlot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
    <style type="text/css">
        .calendar-custom th.datepicker-switch {font-weight: bold !important;}
        .datepicker table tr td {font-weight:bold;}
        .datepicker table tr td.new, .datepicker table tr td.old {color: #a2988f !important; opacity: 1;;font-weight: normal !important;}
        .datepicker table tr td.new:hover, .datepicker table tr td.old:hover {color: #fff !important; background-color: #a2988f !important; opacity: 1;}
        .datepicker table tr td.day:hover, .datepicker table tr td.focused {background: #36425b; color: #fff;}
        .datepicker table tr td.disabled.holiday {color: red;font-weight: normal !important;}
        .datepicker table tr td.disabled.holiday:hover {background-color: transparent;}
        .calendar-btn span.plo2 {background: gray}
        .calendar-btn span.plo3 {background: red;}
        .datepicker table tr td.disabled {color: gray;font-weight: normal !important;}
        .datepicker table tr td.disabled:hover {background-color: transparent;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function ValidateSlot() {
            var TimeSlot = document.getElementById('txtHideSelectedTimeSlot').value
            if (TimeSlot == '') {
                alert('Please select Appointment Time Slot');
                return false;
            }
            else {
                return true;

            }

        }
        function CalenderDayRender(date) {
            //alert(date);
            var mDay, mMonth;
            if ((date.getMonth() + 1) < 10) {
                mMonth = "0" + (date.getMonth() + 1);
            } else {
                mMonth = (date.getMonth() + 1);
            }

            if ((date.getDate()) < 10) {
                mDay = "0" + date.getDate();
            } else {
                mDay = date.getDate();
            }

            var BindDates = date.getFullYear() + '-' + mMonth + '-' + mDay;

            if (jsArrayHolidaysDates.indexOf(BindDates) > -1) {
                return {
                    classes: 'holiday disabled'
                };
            }

            try {
                $(".hidebefore").fadeOut();
                document.getElementById('txtHideSelectedTimeSlot').value = "";
            } catch (e) {

            }

            var BindingDate = new Date(BindDates);
            var AvaiableSlotFromDate = new Date(jsArrayAvaiableSlotFromDates[0]);
            var AvaiableSlotToDate = new Date(jsArrayAvaiableSlotFromDates[1]);

            //alert(jsArrayAvaiableSlotFromDates[1]);
            if (BindingDate < AvaiableSlotFromDate) {
                return false;
            }

            if (BindingDate > AvaiableSlotToDate) {
                return false;
            }

           

            return true;
        }
        function calender_changeDate(date) {
            $('#txtHideSelectedDate').val(date);
            if (!date || date === "") {
                $(divTimeSlot).html("<div><span>Not available, please check other dates.</span></div>");
                $(".hidebefore").fadeIn();
                return;
            }
            $("body").loading();
            setTimeout(function () {
                timeSlotBinding(date).then(function (msg) {
                    $("body").loading("stop");
                    if (msg.d) {
                        $(divTimeSlot).html(msg.d);
                    }
                    else {
                        $(divTimeSlot).html("<div><span>Not available, please check other dates.</span></div>");
                    }
                    $(".hidebefore").fadeIn();
                    datePickerCtr.hideOtherMonthDays();
                });
            }, 100);
        }

        function calender_changeMonth(sDate) {

            var selectedDate = moment(sDate).format("yyyy-MM-DD");
           
            $("body").loading();
            setTimeout(function () {
                CheckECBlockedDate(selectedDate, jsArrayAvaiableSlotFromDates[0]).then(function (msg) {
                    $("body").loading("stop");
                    var blockedDates = msg.d;
                    $(".calendar-custom").datepicker("setDatesDisabled", blockedDates);

                    var currentAvailabeDate = setdate();
                    if (currentAvailabeDate.getMonth() == sDate.getMonth()) {
                        $(".calendar-custom").datepicker("setDate", setdate());
                    }
                    else {
                        for (var i = 1; i <= 31; i++) {
                            var iDate = moment(new Date(sDate.getFullYear() + "-" + (sDate.getMonth() + 1) + "-" + i));
                            var iDateStr = iDate.format("yyyy-MM-DD");
                            if (blockedDates.indexOf(iDateStr) == -1 && jsArrayHolidaysDates.indexOf(iDateStr) == -1) {
                                $(".calendar-custom").datepicker("setDate", iDate.toDate());
                                break;
                            }
                        }
                    }
                    datePickerCtr.hideOtherMonthDays();
                });
            }, 100);
        }
        function CheckECBlockedDate(BindDates, AvaiableSlotFromDates) {
           
            return jQuery.ajax({
                type: "POST",
                url: "ReAppointmentSlot.aspx/CheckECBlockedDates",
                data: "{'checkDate':'" + BindDates + "', 'avaiableSlotFrom':'" + AvaiableSlotFromDates + "' }", //Pass the parameter names and values
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                error: function (jqXHR, textStatus, errorThrown) {
                    $("body").loading("stop");
                    alert("Error- Status: " + textStatus + " jqXHR Status: " + jqXHR.status + " jqXHR Response Text:" + jqXHR.responseText)
                }
            });

            
        }


        function setdate() {
            //alert("123");
            //return new Date(jsArrayAvaiableSlotFromDates[0]);
            return new Date(moment(jsArrayAvaiableSlotFromDates[0]).toDate());
        }

        function timeSlotBinding(SelectedDate) {
            //var responseValue = "";
            return jQuery.ajax({
                type: "POST",
                url: "ReAppointmentSlot.aspx/BindingTimeSlot",
                data: "{'SelectedDate':'" + SelectedDate + "'}", //Pass the parameter names and values
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("Error- Status: " + textStatus + " jqXHR Status: " + jqXHR.status + " jqXHR Response Text:" + jqXHR.responseText)
                }
               
            });

            //return responseValue;
        }

        function myCallback(result) {
            return {
                classes: 'holiday disabled'
            };
        }

        function TimeSlotSelection(SelectedSlot) {
            //alert(SelectedSlot);
            document.getElementById('txtHideSelectedTimeSlot').value = SelectedSlot;
        }

    </script>

    <div class="app clearfix">
        <section class="after">
            <div class="steps bars color1" style="display:none">
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
                <div class="backbtn" data-act="page9">
                    <a style="visibility:hidden" href="#" class="gobacks">
                        <img src="../assets/img/back.svg"></a>
                </div>
            </div>
            <div class="color2 view-data">
                <div class="in_pages page1 clearfix">
                    <div class="leftside color_blue" style="display:none" >
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
                        <div class="card">
                            <div class="label_card address">
                                <asp:Literal ID="AffixationAddress" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                    <div class="rightside">
                        <div class="addedspace">
                            <style>
                                .ng-calendar{
                                    position: relative;
                                }
                                .ng-calendar .cal-loader{
                                    position: absolute;
    width: 100%;
    height: 100%;
    background: #ccebf5 url(/assets/img/load.svg) center no-repeat;
    z-index: 9;
    display: none;
                                }
                            </style>
                            <h4>Select Appointment Date & Time Slot</h4>
                            <asp:Literal ID="LiteralMessage" runat="server"></asp:Literal>

                            <div class="appointment clearfix">
                                <div class="width50">
                                    <div class="title">
                                        <img src="../assets/img/calendar.svg">
                                        <p>Confirm Date</p>
                                    </div>
                                    <div class="ng-calendar">
                                        <div class="cal-loader"></div>
                                    <div class="calendar-custom"></div>
                                    </div>
                                    <div class="calendar-btn">
                                        <table>
                                            <tr>
                                                <td><span class="plo1"></span>Slot Available</td>
                                                <td class="text-center"><span class="plo2"></span>Slot not Available</td>
                                                <td class="text-right"><span class="plo3"></span>Holidays</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="width50 hidebefore">
                                    <div class="title">
                                        <img src="../assets/img/clock.svg">
                                        <p>Confirm Time</p>
                                    </div>
                                    <p class="sub-title">Available time slots</p>
                                    <div class="tabs">
                                        <span data-view="tab1">PLEASE SELECT A TIME SLOT</span>
                                        
                                    </div>
                                    <div class="tab-content">
                                        <div class="tabview tab1" style="display: block" id="divTimeSlot">
                                            <!--<table>
                                                <tr>
                                                    <td><p>10:00am-12:00pm</p></td>
                                                    <td><a href="#">Available</a></td>
                                                </tr>
                                                <tr>
                                                    <td><p>12:00am-1:45pm</p></td>
                                                    <td><a href="#" class="disable">Not Available</a></td>
                                                </tr>
                                                <tr>
                                                    <td><p>02:15am-4:00pm</p></td>
                                                    <td><a href="#" class="booked">Time Slot on Hold</a></td>
                                                </tr>
                                                <tr>
                                                    <td><p>04:00am-6:00pm</p></td>
                                                    <td><a href="#">Available</a></td>
                                                </tr>
                                            </table>-->
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="clearfix iscoupyes addedspace">
                            <div class="width50 width60">
                                <p class="notes">
                                    Note :<br>
                                    1. Your appointment will be processed only upon Verifcation of Data.<br>
                                    2. Verify contact number & Email ID to receive timely updates.<br>
                                    <br>
                                    Technical issue or query please contact at: 18001200201 or Email at
                                online@bookmyhsrp.com
                                </p>
                            </div>
                            <div class="width50 width40">
                                   <!--/******
                                Start Create hidden Field For store selected Date & Timeslot
                                ********/-->
                                <asp:TextBox ID="txtHideSelectedDate" ClientIDMode="Static" runat="server" CssClass="form-control" Visible="true" Height="0" Style="height: 0px; display: none;"></asp:TextBox>
                                
                                <asp:TextBox ID="txtHideSelectedTimeSlot" ClientIDMode="Static" runat="server" CssClass="form-control" Visible="true" Height="0" Style="height: 0px; display: none;"></asp:TextBox>
                                

                                <!--/******
                                End Create hidden Field For store selected Date & Timeslot
                                ********/-->
                                <!--<a href="page12.html" class="filled btn isforwardsummary">Confirm & Proceed</a>-->
                                <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Button ID="btnTimeSlotSelection" runat="server" OnClick="btnTimeSlotSelection_Click" Text="Confirm & Proceed" class="filled btn isforwardsummary" />

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnTimeSlotSelection" />
                                    </Triggers>
                                </asp:UpdatePanel>--%>
                                <asp:Button ID="btnTimeSlotSelection" runat="server" OnClientClick="return ValidateSlot();"  OnClick="btnTimeSlotSelection_Click" Text="Confirm & Proceed" class="filled btn isforwardsummary" />
                                
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </section>
    </div>


</asp:Content>
