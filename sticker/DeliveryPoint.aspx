<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeliveryPoint.aspx.cs" Inherits="BMHSRPv2.sticker.DeliveryPoint" %>

<%@ Register Src="~/sticker/ucStepBar.ascx" TagPrefix="uc1" TagName="ucStepBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function ()
        {
            $('.DealerPointClick').click(function ()
            {
                var DeliveryPoint =  $(this).attr('id');
                if (DeliveryPoint == '')
                {
                    alert('Please Choose Delivery Point');
                    return false;
                }
                //alert(DeliveryPoint);
                $.ajax({
                    type: "GET",
                    url: "api/Get/GetDeliveryPoint?SessionValue=" + DeliveryPoint,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        data = response;
                        //alert(data);
                        if (data == "Home")
                        {
                            //alert(data);
                            window.location.href = "Delivery.aspx";
                        }
                        else if (data == "Dealer")
                        {
                            window.location.href = "Dealers.aspx";
                        }
                        else if (data == "rwa") {
                            window.location.href = "Rwa.aspx";
                            // alert("Coming RWA");
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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="app clearfix page3 locations">
	
	
        <section class="after">
            <uc1:ucStepBar runat="server" id="ucStepBar" PageStep="6" BackPage="BookingDetail.aspx" />
            
            <a id="Home" style="display:none" class="bars pages color5 " >
                <div class="short">
                    <div class="img style1"><img src="../assets/img/ic1.svg"></div>
                    <p><span></span>Home Delivery</p>
                    <div class="control widthfull" style="margin-top: 15px;">
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                       <br /> <h4><asp:Literal ID="LiteralComingSoon" runat="server"></asp:Literal></h4>
                    </div>
                   
                </div>
                 
            </a>
            <a id="Dealer"  style="display:none" class="bars pages color2 ">
                <div class="short">
                    <div class="img style1" ><img src="../assets/img/ic2.svg"></div>
                    <p><span></span>Dealer Appointment</p>
                    
                </div>
            </a>
              <a id="rwa" class="bars pages color5 DealerPointClick">
                <div class="short">
                    <%--<div class="img style1" ><img src="../assets/img/home12.svg"></div>--%>
                    <p><span></span>RWA Booking</p>
                    
                </div>
            </a>
        </section>
    </div>
</asp:Content>
