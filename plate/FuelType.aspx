<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FuelType.aspx.cs" Inherits="BMHSRPv2.plate.FuelType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <script type="text/javascript">
       // var state;

          $(document).ready(function () {
              $('#btnstate').click(function () {

                  //alert("aa");
                  //var state = document.getElementById('txtHideSelectedStateID').value;
                  var fueltype = $("#ContentPlaceHolder1_txtHideSelectedStateID").val();
                  //alert(state);
                  if (fueltype == '' || fueltype == null) {
                      alert('Please Select Your Fuel Type');
                      return false;
                  }


                 // alert(fueltype);
                
                  // alert(ImagePath);
                  jQuery.ajax({
                      type: "GET",
                      url: "api/Get/Setfueltype?fueltype=" + fueltype,
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      success: function (response) {
                          data = response;
                          if (data == "Success") {
                              window.location.href = "Vehicletype.aspx";
                          }
                          else {
                              alert(data);
                          }
                           
                         

                      },
                      failure: function (response) {
                          // alert();
                      }
                  });
              });
          });




         
        function StateSelectionFun(fuelID) {
           
            $("#ContentPlaceHolder1_txtHideSelectedStateID").val(fuelID);
           // document.getElementById('txtHideSelectedStateID').value = fuelID;

        

                //alert("aa");
                //var state = document.getElementById('txtHideSelectedStateID').value;
            var fueltype = fuelID;// $("#ContentPlaceHolder1_txtHideSelectedStateID").val();
           // alert(fueltype);
                if (fueltype == '' || fueltype == null) {
                    alert('Please Select Your Fuel Type');
                    return false;
                }


                // alert(fueltype);

                // alert(ImagePath);
                jQuery.ajax({
                    type: "GET",
                    url: "api/Get/Setfueltype?fueltype=" + fueltype,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        data = response;
                        if (data == "Success") {
                            window.location.href = "Vehicletype.aspx";
                        }
                        else {
                            alert(data);
                        }



                    },
                    failure: function (response) {
                        // alert();
                    }
                });
        
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
        <div class="backbtn" data-act="page4">
        <a href="VehicleClass.aspx" class="gobacks"><img src="../assets/img/back.svg"></a>
    </div>
</div>            <div class="view-data">
                <div class="in_pages page1 clearfix">
                    <div class="leftside">
                          <%-- <div class="head">
                         <img src="../assets/img/h1.png">
                        </div>
                        <div class="card">
                            <div class="label_card bikefill">
                                <img src="../assets/img/bike-fill1.svg">
                            </div>
                        </div>
                        <div class="card">
                            <div class="label_card">
                                <img src="../assets/img/color0-fill.svg">
                            </div>
                        </div>
                        <div class="card">
                            <div class="state label_card">
                                <p><span>HR</span>Haryana</p>
                            </div>
                        </div>
                        <div class="card">
                            <div class="label_card bikefill">
                                <img src="../assets/img/car-fill.svg">
                                <p>Private Vehicle</p>
                            </div>
                        </div>--%>
                         <div class="head">
                          
                             <asp:Literal ID="LiteralBookingTypeImage" runat="server"></asp:Literal>
                        </div>
                        <div class="card" style="display:none">
                            <div class="label_card bikefill">
                             
                                <asp:Literal ID="LiteralVehicleTypeImage" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="card">
                            <div class="label_card">
                                  <asp:Literal ID="LiteralOemImage" runat="server"></asp:Literal>
                            
                            </div>
                        </div>
                        <div class="card">
                            <div class="state label_card">
                                <asp:Literal ID="LiteralState" runat="server"></asp:Literal>
                              
                            </div>
                        </div>
                        <div class="card">
                            <div class="label_card bikefill">
                                  <asp:Literal ID="LiteralVehicleClassImage" runat="server"></asp:Literal>
                            
                            </div>
                        </div>
                    </div>
                    <div class="rightside">
                    <div class="details">
                        <h4>Select Fuel Type</h4>
                        <div class="fuel-selection clearfix">
                            <a href="#" class="f_type iss1" onclick="StateSelectionFun('Petrol')">
                                <p>Petrol</p>
                                
                            </a>
                            <a href="#" class="f_type iss2"  onclick="StateSelectionFun('Diesel')">
                                <p>Diesel</p>
                                
                            </a>
                            <a href="$" class="f_type iss3"  onclick="StateSelectionFun('CNG')">
                                <p>CNG</p>
                               
                            </a>
                        </div>
                        <div class="fuel-selection clearfix">
                            <a href="#" class="f_type widthdbl iss4"  onclick="StateSelectionFun('CNG/Petrol')">
                                <p>CNG/LPG + Petrol</p>
                                
                            </a>
                            <a href="#" class="f_type widthdbls iss5"  onclick="StateSelectionFun('Electric')">
                                <p>Electric V.</p>
                                
                            </a>
                        </div>
                        <div class="fuel-selection clearfix">
                        <div class="text-right">
                          <!--/******
                                Start Create hidden Field For store StateID
                                ********/-->
                                <%--<asp:TextBox ID="txtHideSelectedStateID" ClientIDMode="Static" runat="server" CssClass="form-control" Visible="true" Height="0" Style="height: 0px; display: none;"></asp:TextBox>--%>
                                <asp:HiddenField ID="txtHideSelectedStateID" runat="server" />
                                <!--/******
                                End  Create hidden Field For store StateID
                                ********/-->
                                    <a style="visibility:hidden"  id="btnstate" class="filled btn isforwardsummary ">Next</a>
                                   <%-- <asp:Button ID="btnFuelSelection" runat="server" OnClick="btnFuelSelection_Click" Text="Next" class="filled btn isforwardsummary" />--%>
                        
                        </div>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
