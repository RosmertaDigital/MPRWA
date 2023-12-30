<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vehicletype.aspx.cs" Inherits="BMHSRPv2.sticker.Vehicletype" %>

<%@ Register Src="~/sticker/ucStepBar.ascx" TagPrefix="uc1" TagName="ucStepBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="app clearfix">
        <section class="after">
            <uc1:ucStepBar runat="server" id="ucStepBar" PageStep="4" BackPage="VehicleClass.aspx" />

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
             $('#setvehicletype').click(function () {

                 var sel_radio= $('input[name="vehicle_Category"]:checked').val();
                 //alert(sel_radio);
                 if (sel_radio == 'undefined') {
                     alert('Please Select Vehicle Type');
                     return false;
                 }
                 jQuery.ajax({
                     type: "GET",
                     url: "api/Get/SetVehicleType?radiovehicletype=" + sel_radio,
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
