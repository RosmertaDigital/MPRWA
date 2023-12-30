<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="BMHSRPv2.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            $('.BookinTypeClick').click(function () {


                var status = jQuery(this).attr('id');
                //alert(status);
                // $("#MainContent_hdnVehicleClass").val(status);
                if (status == '')
                {
                    alert('Please Choose Booking Type');
                    return false;
                }

                var ImagePath = $('#' + status).find('.style1 img').map(function () { return this.src; }).get();
                // alert(ImagePath);
                jQuery.ajax({
                    type: "GET",
                    url: "api/Get/OrderType?OrderType=" + status + "&OrderTypeImagePath=" + ImagePath,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        data = response;
                        if (data == "HSRP") {
                            window.location.href = "plate/VahanBookingDetail.aspx";
                        }
                        else if (data == "Sticker") {
                            //alert(data);
                            window.location.href = "sticker/VahanBookingDetail.aspx";
                        }
                        else
                        {
                             alert(data);

                        }
                       

                    },
                    failure: function (response) {
                       // alert();
                    }
                });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="app clearfix homepage">
        <section class="before ishomepages">
            <a href="#" id="HSRP" class="bars pages color5 BookinTypeClick">
                <div class="short platnim">
                    <div class="img style1"><img src="assets/img/home11.png"></div>
                    <div class="img style2 hidetemp"><img src="assets/img/car.svg"></div>
                    <p>HIGH SECURITY REGISTRATION PLATE<br>WITH<br>COLOUR STICKER</p>
                </div>
            </a>

            

            <a href="#" id="Sticker" class="bars pages color4 BookinTypeClick">
                <div class="short">
                    <div class="img style1"><img src="assets/img/home12.png"></div>
                    <div class="img style2 hidetemp"><img src="assets/img/truck-big.svg"></div>
                    <p>ONLY<br>COLOUR STICKER<br>&nbsp;</p>
                </div>
            </a>
        </section>
    </div>
</asp:Content>
