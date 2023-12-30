<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VehicleMake.aspx.cs" Inherits="BMHSRPv2.sticker.VehicleMake" %>

<%@ Register Src="~/sticker/ucStepBar.ascx" TagPrefix="uc1" TagName="ucStepBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
/*        .hide{
  display: none;
}
.project{
  border: 1px solid #eaeaea;
  margin: 20px;
  padding: 20px;
}*/


    </style>
    <script type="text/javascript">

        window.onload = function () {
            //SetVehicleCatSelected('1')
        };
        function SetVehicleCatSelected(id) {
            // alert(id);
            // $('#1').addClass('s_active');
            $('.type-selection .label').css('opacity', '0.3');
            $('#' + id).css('opacity', '1');
            //$(this).addClass('s_active');
           // $('.type-selection .label').removeClass('s_active');
            //$(this).addClass('s_active');
           // $(this).parent().addClass('s_active');
            return false;
            
        }


        //function showProjectsbyCat(cat) {
        //    if (cat == 'all') {
        //        $('#projects-hidden .project').each(function () {
        //            var owl = $(".owl-carousel").data('owlCarousel');
        //            elem = $(this).parent().html();

        //            owl.addItem(elem);
        //            $(this).parent().remove();
        //        });
        //    } else {
        //        $('#projects-hidden .project.' + cat).each(function () {
        //            var owl = $(".owl-carousel").data('owlCarousel');
        //            elem = $(this).parent().html();

        //            owl.addItem(elem);
        //            $(this).parent().remove();
        //        });

        //        $('#projects-carousel .project:not(.project.' + cat + ')').each(function () {
        //            var owl = $(".owl-carousel").data('owlCarousel');
        //            targetPos = $(this).parent().index();
        //            elem = $(this).parent();

        //            $(elem).clone().appendTo($('#projects-hidden'));
        //            owl.removeItem(targetPos);
        //        });
        //    }
        //}

        //$(window).load(function () {

        //    //Click event for filters
        //    $('#project-terms a').click(function (e) {
        //        e.preventDefault();
        //        $('#project-terms a').removeClass('active');

        //        cat = $(this).attr('ID');
        //        $(this).addClass('active');
        //        showProjectsbyCat(cat);
        //        //alert('filtering'+ cat);
        //    });

        //    //Initialize owl carousel
        //    $("#projects-carousel").owlCarousel({

        //        // Most important owl features
        //        items: 3
        //    }
        //    );
        //});
       

        // Include script file
        function addScript(filename) {
            var head = document.getElementsByTagName('head')[0];

            var script = document.createElement('script');
            script.src = filename;
            script.type = 'text/javascript';

            head.append(script);
        }



        function VehicleTypeSearch() {
            var oemname = $("#myInput").val().toLowerCase();
            $("#ContentPlaceHolder1_HiddenOemsearch").val(oemname);
            $("#ContentPlaceHolder1_btnSearchOEM").click();

            //alert(oemname);
            // var src = $(this).children('img').first().attr('src');
            //var ImagePath = $('#' + status).find('.OemClick').find('img').map(function () { return this.src; }).get();

            //$("#ContentPlaceHolder1_HiddenOemImagePath").val(src);
            //alert(src);
            /*
            jQuery.ajax({
                type: "GET",
                url: "api/Get/searchoem?oemname=" + oemname,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    data = response;
                    //alert(data);


                },
                failure: function (response) {
                    alert(response);
                }
            });
            */
        }

        $(document).ready(function () {
            $('.Vehicletype').click(function () {


                var status = jQuery(this).attr('id');
                if (status == '') {
                    alert('Please Choose Vehicle Category');
                    return false;
                }

                $("#ContentPlaceHolder1_HiddenVehicleCatID").val(status);
               
               // alert(status);
                // $("#MainContent_hdnVehicleClass").val(status);

                var ImagePath =  $('#' + status).find('img').map(function () { return this.src; }).get();

                // alert(ImagePath);
                jQuery.ajax({
                    type: "GET",
                    url: "api/Get/SetSessionVehicleTypeID?CategoryId=" + status + "&VehicleCategoryImgPath=" + ImagePath,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        data = response;
                        //alert(data);
                        if (data == "Success") {
                            $("#ContentPlaceHolder1_btnVehicleCategory").click();
                        }
                        else {
                            alert(data);
                        }
                      
                      //  $(".brand-selection").hide().fadeIn();
                      //  $('#DivSearchOem').show();
                       
                       // $('#btnNextOem').show();
                        //$('.Oem').css('display', 'none');

                     // $('.' + status).css('display', 'block');
                        
                       // $('.brands').append(data);
                        //addScript('../assets/js/app.js');
                        //$("<link/>", {
                        //    rel: "stylesheet",
                        //    type: "text/css",
                        //    href: "~/assets/css/owl.carousel.min.css"
                        //}).appendTo("head");
                    },
                    failure: function (response) {
                        alert(response);
                    }
                });
            });

            $('#btnNextOem').click(function () {


                var status = jQuery(this).attr('id');


                var oemimg = $("#ContentPlaceHolder1_HiddenOemImagePath").val();
                var oemid = $("#ContentPlaceHolder1_HiddenOemid").val();
                //if (oemid == 13 || oemid == 46 || oemid == 6 || oemid == 22) {
                //    window.location.href = "https://www.makemyhsrp.com/";
                //    return false;
                //}

                


                //alert(oemid);
                if (oemid == '' || oemid == null) {
                    alert('Please Select Your Vehicle Make');
                    return false;
                }
                //alert(oemid);
                // $("#MainContent_hdnVehicleClass").val(status);
                //var ImagePath = $('#' + status).find('img').map(function () { return this.src; }).get();
                // alert(ImagePath);
                jQuery.ajax({
                    type: "GET",
                    url: "api/Get/SetOEM?OEMId=" + oemid + "&OEMImgPath=" + oemimg,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        data = response;
                        if (data == "Success") {
                            window.location.href = "StateType.aspx";
                        }
                        else {
                            alert(data);
                        }
                      
                       
                    },
                    failure: function (response) {
                        alert(response);
                    }
                });
            });


            $('.OemClick').click(function () {


                var status = jQuery(this).attr('id');


                //$("#ContentPlaceHolder1_HiddenOemid").val(status);

               //alert(status);
                var src = $(this).children('img').first().attr('src');
                //var ImagePath = $('#' + status).find('.OemClick').find('img').map(function () { return this.src; }).get();

                //$("#ContentPlaceHolder1_HiddenOemImagePath").val(src);



                var oemimg = src;// $("#ContentPlaceHolder1_HiddenOemImagePath").val();
                var oemid = status;// $("#ContentPlaceHolder1_HiddenOemid").val();
                //if (oemid == 13 || oemid == 46 || oemid == 6 || oemid == 22) {
                //    window.location.href = "https://www.makemyhsrp.com/";
                //    return false;
                //}


                if (oemid == '' || oemid == null) {
                    alert('Please Select Your Vehicle Make');
                    return false;
                }

                jQuery.ajax({
                    type: "GET",
                    url: "api/Get/SetOEM?OEMId=" + oemid + "&OEMImgPath=" + oemimg,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        data = response;
                        if (data == "Success") {
                            window.location.href = "StateType.aspx";
                        }
                        else {
                            alert(data);
                        }


                    },
                    failure: function (response) {
                        alert(response);
                    }
                });


            });

            $('#search').click(function () {
                VehicleTypeSearch();
                return false;
            });

            $('#myInput').keypress(function (event) {
                var keycode = event.keyCode ? event.keyCode : event.which;
                //alert("test");
                if (keycode == 13) {
                    VehicleTypeSearch();
                    return false;
                }

            });
            /*
            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $(".brands a").filter(function () {
                    $(this).toggle($(this).attr('title').toLowerCase().indexOf(value) > -1)
                });
            });
            */
        });

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app clearfix">
        <section class="after">
            <uc1:ucStepBar runat="server" id="ucStepBar" PageStep="1" BackPage="../index.aspx"  />

            <div class="view-data color5">
                <div class="in_pages page1">
                    <!--<div class="head">
                        <img src="../assets/img/h1.png">
                        <p>ONLY<br>COLOUR STICKER</p>
                    </div>-->
                    <div  class="head">
                          <asp:Literal ID="LiteralBookingTypeImage" runat="server"></asp:Literal>
                       <%-- <img src="../assets/img/h1.png">--%>
                    </div>
                    <div class="details">
                        <h4>Select Vehicle Make</h4>
                        <div class="type-selection clearfix">
                            <asp:Literal runat="server" ID="LitVehicleCat"></asp:Literal>
                           <%-- <div id="2WHEELERS"  class="label Vehicletype">
                                <img src="../assets/img/S1-01.svg">
                                <p>Two Wheelers</p>
                            </div>
                            <div id ="Motorcycle" class="label Vehicletype">
                                <img src="../assets/img/s2-01.svg">
                                <p>Motorcycle</p>
                            </div>
                            <div id="Three_Wheeler" class="label Vehicletype">
                                <img src="../assets/img/R1-01.svg">
                                <p>Three Wheelers</p>
                            </div>
                            <div id="Four_Wheeler" class="label Vehicletype">
                                <img src="../assets/img/car.svg">
                                <p>Four Wheelers</p>
                            </div>
                            <div id="Heavy" class="label Vehicletype">
                                <img src="../assets/img/truck-big.svg">
                                <p>Other Vehicles</p>
                            </div>
                            <div id="Other" class="label Vehicletype">
                                <img src="../assets/img/truck.svg">
                                <p>Other Vehicles</p>
                            </div>--%>
                            <asp:HiddenField ID="HiddenVehicleCatID" runat="server" />
                             <asp:HiddenField ID="HiddenOemid" runat="server" />
                             <asp:HiddenField ID="HiddenOemImagePath" runat="server" />
                            <asp:HiddenField ID="HiddenOemsearch" runat="server" />
                             <asp:Button ID="btnSearchOEM" runat="server" OnClick="btnSearchOEM_Click1" Text="" BackColor="Transparent" BorderStyle="None" />
                             <asp:Button ID="btnVehicleCategory" runat="server" OnClick="btnVehicleCategory_Click"  Text="" BackColor="Transparent" BorderStyle="None" />
                        </div>

                        <div class="brand-selection ">
                            <div class="left-aligns" id="DivSearchOem" >
                                <div>
                                    <input id="myInput" type="text" class="form-input cities" placeholder="Search your Vehicle Make">
                                    <button type="button" id="search"><img src="../assets/img/search.svg"></button>
                                </div>
                            </div>
                            <div class="owl-carousel owl-theme brands">
                                 <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                               <%--<div class='item'>
                                   <a href = 'page2.html'  title="Ashok Leyland" class='label'>
                                       <img src = 'https://bookmyhsrp.com/OEMLOGO/ahok leyland.jpg' alt='Ashok Leyland' class='simpleview'>
                                       </a>
                                   <a href = 'page2.html' title="Tata" class='label'>
                                       <img src = 'https://bookmyhsrp.com/OEMLOGO/tata.jpg' alt='Ashok Leyland'class='simpleview'>
                                       

                                   </a>
                               </div>--%>

                                 <%-- <div class='item'>
                                   <a href = 'page2.html'  title="HYUNDAI" class='label'>
                                       <img src = 'https://bookmyhsrp.com/OEMLOGO/HYUNDAI.jpg' alt='Ashok Leyland' class='simpleview'>
                                       </a>
                                   <a href = 'page2.html' title="Audi" class='label'>
                                       <img src = 'https://bookmyhsrp.com/OEMLOGO/Audi.jpg' alt='Ashok Leyland'class='simpleview'>
                                       

                                   </a>
                               </div>--%>
                                    <%--<div class="item">
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/bajaj-01.svg" class="simpleview">
                                            <img src="../assets/img/bajaj1.svg" class="hoverview">
                                        </a>
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/Royal-01.svg" class="simpleview">
                                            <img src="../assets/img/hero1.svg" class="hoverview">
                                        </a>
                                    </div>
                                    <div class="item">
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/Hero-01.svg" class="simpleview">
                                            <img src="../assets/img/honda1.svg" class="hoverview">
                                        </a>
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/Yamaha-01.svg" class="simpleview">
                                            <img src="../assets/img/royal1.svg" class="hoverview">
                                        </a>
                                    </div>
                                    <div class="item">
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/Honda-01.svg" class="simpleview">
                                            <img src="../assets/img/hero1.svg" class="hoverview">
                                        </a>
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/TVS-01.svg" class="simpleview">
                                            <img src="../assets/img/yamaha1.svg" class="hoverview">
                                        </a>
                                    </div>
                                    <div class="item">
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/bajaj-01.svg" class="simpleview">
                                            <img src="../assets/img/bajaj1.svg" class="hoverview">
                                        </a>
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/Royal-01.svg" class="simpleview">
                                            <img src="../assets/img/hero1.svg" class="hoverview">
                                        </a>
                                    </div>
                                    <div class="item">
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/bajaj-01.svg" class="simpleview">
                                            <img src="../assets/img/bajaj1.svg" class="hoverview">
                                        </a>
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/Royal-01.svg" class="simpleview">
                                            <img src="../assets/img/hero1.svg" class="hoverview">
                                        </a>
                                    </div>
                                    <div class="item">
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/Hero-01.svg" class="simpleview">
                                            <img src="../assets/img/honda1.svg" class="hoverview">
                                        </a>
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/Yamaha-01.svg" class="simpleview">
                                            <img src="../assets/img/royal1.svg" class="hoverview">
                                        </a>
                                    </div>
                                    <div class="item">
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/Honda-01.svg" class="simpleview">
                                            <img src="../assets/img/hero1.svg" class="hoverview">
                                        </a>
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/TVS-01.svg" class="simpleview">
                                            <img src="../assets/img/yamaha1.svg" class="hoverview">
                                        </a>
                                    </div>
                                    <div class="item">
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/bajaj-01.svg" class="simpleview">
                                            <img src="../assets/img/bajaj1.svg" class="hoverview">
                                        </a>
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/Royal-01.svg" class="simpleview">
                                            <img src="../assets/img/hero1.svg" class="hoverview">
                                        </a>
                                    </div>
                                    <div class="item">
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/bajaj-01.svg" class="simpleview">
                                            <img src="../assets/img/bajaj1.svg" class="hoverview">
                                        </a>
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/Royal-01.svg" class="simpleview">
                                            <img src="../assets/img/hero1.svg" class="hoverview">
                                        </a>
                                    </div>
                                    <div class="item">
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/Hero-01.svg" class="simpleview">
                                            <img src="../assets/img/honda1.svg" class="hoverview">
                                        </a>
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/Yamaha-01.svg" class="simpleview">
                                            <img src="../assets/img/royal1.svg" class="hoverview">
                                        </a>
                                    </div>
                                    <div class="item">
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/Honda-01.svg" class="simpleview">
                                            <img src="../assets/img/hero1.svg" class="hoverview">
                                        </a>
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/TVS-01.svg" class="simpleview">
                                            <img src="../assets/img/yamaha1.svg" class="hoverview">
                                        </a>
                                    </div>
                                    <div class="item">
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/bajaj-01.svg" class="simpleview">
                                            <img src="../assets/img/bajaj1.svg" class="hoverview">
                                        </a>
                                        <a href="page2.html" class="label">
                                            <img src="../assets/img/Royal-01.svg" class="simpleview">
                                            <img src="../assets/img/hero1.svg" class="hoverview">
                                        </a>
                                    </div>--%>
                              </div>

                              <%--  <div class="text-right">
                                <a href="page2.html" class="filled btn isforwardsummary">Next</a>
                            </div>--%>

                            <div class="text-right">
                                <a style="visibility:hidden"  id="btnNextOem"  class="filled btn isforwardsummary">Next</a>
                            </div>
                        </div>

                                                     <%--   <div class="text-right">
                                <a href="#" id="btnNextOem"  class="filled btn isforwardsummary">Next</a>
                            </div>--%>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
