<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StateType.aspx.cs" Inherits="BMHSRPv2.plate.Statetype" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
       // var state;

        $(document).ready(function () {
            $('#btnstate').click(function () {

                //alert("aa");
                //var state = document.getElementById('txtHideSelectedStateID').value;
                var state = $("#ContentPlaceHolder1_txtHideSelectedStateID").val();

                var StateShortName = $("#ContentPlaceHolder1_HiddenShortname").val();
                var StateName = $("#ContentPlaceHolder1_HiddenStateName").val();
           
                //alert(state);
                //alert(StateShortName);
                //alert(StateName);
          
                if (state == '' || state == null) {
                    alert('Please Select Your State');
                    return false;
                }
               
                
                jQuery.ajax({
                    type: "GET",
                    url: "api/Get/Setstate?state=" + state + "&ShortName=" + StateShortName + "&statename=" + StateName,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        data = response;
                        // alert(data);
                        if (data.Status == "1") {

                            if (data.ResponseData =="Rosmerta" ) {
                                window.location.href = "VehicleClass.aspx";
                            }
                            else
                            {
                                window.location.href = data.ResponseData;
                            }
                           
                        }
                        else
                        {
                            alert(data.Status);
                        }
                       

                    },
                    failure: function (response) {
                        // alert();
                    }
                });
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


        function StateSelectionFun(StateID,shortname,statename) {
          //  alert(StateID);
            //$("#ContentPlaceHolder1_txtHideSelectedStateID").val(StateID);

           // $("#ContentPlaceHolder1_HiddenShortname").val(shortname);
            //$("#ContentPlaceHolder1_HiddenStateName").val(statename);

           

                //alert("aa");
                //var state = document.getElementById('txtHideSelectedStateID').value;
            var state = StateID;// $("#ContentPlaceHolder1_txtHideSelectedStateID").val();

            var StateShortName = shortname;   // $("#ContentPlaceHolder1_HiddenShortname").val();
            var StateName = statename;    //$("#ContentPlaceHolder1_HiddenStateName").val();

                //alert(state);
                //alert(StateShortName);
                //alert(StateName);

                if (state == '' || state == null) {
                    alert('Please Select Your State');
                    return false;
                }


                jQuery.ajax({
                    type: "GET",
                    url: "api/Get/Setstate?state=" + state + "&ShortName=" + StateShortName + "&statename=" + StateName,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        data = response;
                        if (data.Status == "1") {

                            if (data.Msg == "Rosmerta") {
                                window.location.href = "VehicleClass.aspx";
                            }

                            else {
                                window.location.href = data.ResponseData;
                            }

                        }
                        else {
                            alert(data.Msg);
                        }


                    },
                    failure: function (response) {
                         alert();
                    }
                });
       
            
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
        <a href="VehicleMake.aspx" class="gobacks"><img src="../assets/img/back.svg"></a>
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
                    </div>
                    <div class="rightside">
                  <!--  <div class="head">
                        <img src="../assets/img/h1.png">
                        <p>ONLY<br>COLOUR STICKER</p>
                    </div>-->
                  <!--  <div class="head">
                        <img src="../assets/img/h1.png">
                    </div>-->
                        <div class="details">
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
                                        <%-- <a href="VehicleClass.aspx" class="label">
                                            <p><span>HR</span>Haryana</p>
                                        </a>--%>
                                    </div>
                                    <div  class="item">
                                        <a href="#" id="HP" class="label stype" onclick="StateSelectionFun('3','HP','HIMACHAL PRADESH')">
                                            <p ><span class="plain">HP</span>HIMACHAL<br /> PRADESH</p>
                                        </a>
                                    </div>

                                </div>
                                <div class="text-right">
                                     <!--/******
                                Start Create hidden Field For store StateID
                                ********/-->
                               <%-- <asp:TextBox ID="txtHideSelectedStateID1" ClientIDMode="Static" runat="server" CssClass="form-control" Visible="true" Height="0" Style="height: 0px; display: none;"></asp:TextBox>--%>
                                    <asp:HiddenField ID="txtHideSelectedStateID" runat="server" />

                                     <asp:HiddenField ID="HiddenShortname" runat="server" />
                                        <asp:HiddenField ID="HiddenStateName" runat="server" />
                                <!--/******
                                End  Create hidden Field For store StateID
                                ********/-->
                                    <a style="visibility:hidden"  id="btnstate" class="filled btn isforwardsummary ">Next</a>
                                    <%--<asp:Button ID="btnStateSelection" runat="server" OnClick="btnStateSelection_Click" Text="Next" class="filled btn isforwardsummary" />--%>
                                
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
