<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VehicleClass.aspx.cs" Inherits="BMHSRPv2.sticker.VehicleClass" %>

<%@ Register Src="~/sticker/ucStepBar.ascx" TagPrefix="uc1" TagName="ucStepBar" %>

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
            <uc1:ucStepBar runat="server" id="ucStepBar" PageStep="3" BackPage="StateType.aspx"  />

            <a id="Non-Transport" class="bars pages color3 ftype">
                <div class="short">
                    <div class="img style1" style="display: none;">
                        <img src="../assets/img/car.svg"></div>
                    <div class="img style2 hidetemp" style="display: none;">
                        <img src="../assets/img/car.svg"></div>
                    <p>
                        <b>PRIVATE VEHICLE<br>
                            (NON-TRANSPORT) - WHITE BACKGROUNG PLATE</b>
                    </p>
                </div>
            </a>
            <a id="Transport" class="bars pages color5 ftype">
                <div class="short">
                    <div class="img style1" style="display: none;">
                        <img src="../assets/img/truck-big.svg"></div>
                    <div class="img style2 hidetemp" style="display: none;">
                        <img src="../assets/img/truck-big.svg"></div>
                    <p>
                        <b>COMMERCIAL VEHICLE<br>
                            (TRANSPORT) - YELLOW BACKGROUNG PLATE</b>
                    </p>
                </div>
            </a>
        </section>
    </div>

</asp:Content>
