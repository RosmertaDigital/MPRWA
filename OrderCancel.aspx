<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderCancel.aspx.cs" Inherits="BMHSRPv2.OrderCancel" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
$( document ).ready(function() {
    DrawCaptcha();
});
</script>      

    <script type="text/javascript">
        $(document).ready(function () {
           
            $('#txtSearch').click(function () {
                if (!ValidCaptcha()) { return false; }

                var Orderno = $('#txtorderno').val();
                var VehicleNo = $('#txtvehiclereg').val();

                if (Orderno == '' || VehicleNo == '')
                {
                    alert('Orderno and VehicleNo should not be Blank!!');
                    return false;
                }
                
                $.ajax({
                    type: "GET",
                    url: "api/Get/OrderNoCancel?orderno=" + Orderno + " &vehicleRegno= " + VehicleNo,

                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response != '') 
                        {
                            var user = JSON.parse(response);
                            if (user.ORDER_NUMBER == '' || user.REG_NUMBER == '') { return false; }
                            //if (user.OrderStatus != 'Success') { alert("this order is already cancel"); return false;}
                            if (user.isAbleToCancelled == 'Y')
                            {
                                //var url = "OrderCancleConfirm.aspx?ORDER_NUMBER=" + Orderno + "&REG_NUMBER=" + VehicleNo;
                                var url = "OrderCancleConfirm.aspx";
                                window.location.href = url;
                            }
                            else
                            {
                                alert("You will not be able to cancel this order ");
                                window.location.href = "OrderCancel.aspx";
                            }
                        }
                        else {
                            
                            alert("Please check OrderNo and Vehicle RegistrationNo or it May be already CANCEL !!");
                        }
                    },
                    error: function (response) {
                        
                        //alert(response.responseText);
                    }
                });
                return false;
            });
        });
    </script>

    <script type="text/javascript">

        //Created / Generates the captcha function    
        function DrawCaptcha() {
            var a = Math.ceil(Math.random() * 10) + '';
            var b = Math.ceil(Math.random() * 10) + '';
            var c = Math.ceil(Math.random() * 10) + '';
            var d = Math.ceil(Math.random() * 10) + '';
            //var e = Math.ceil(Math.random() * 10)+ '';  
            //var f = Math.ceil(Math.random() * 10)+ '';  
            //var g = Math.ceil(Math.random() * 10)+ '';  
            //var code = a + ' ' + b + ' ' + ' ' + c + ' ' + d + ' ' + e + ' '+ f + ' ' + g;
            var code = a + ' ' + b + ' ' + ' ' + c + ' ' + d;
            document.getElementById("txtCaptcha").value = code
        }

        // Validate the Entered input aganist the generated security code function   
        function ValidCaptcha() {
            var str1 = removeSpaces(document.getElementById('txtCaptcha').value);
            var str2 = removeSpaces(document.getElementById('txtInput').value);
            if (str2 == '') {
                alert("Captcha can't be blank!!");
                return false;
            }
            else if (str1 == str2) {
                return true;
            }
            else {
                alert("Please Input Correct Captcha");
                return false;
            }




        }

        // Remove the spaces from the entered and generated code
        function removeSpaces(string) {
            return string.split(' ').join('');
        }


    </script>

    <script type="text/javascript">
        $(window).load(function () {
            DrawCaptcha();
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function ()
        {
            $('#txtCancel').click(function ()
            {
                var url = "Index.aspx";
                window.location.href = url;
                return false;
            });
        });
    </script>

 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script type="text/javascript">
        $(window).load(function () {
            DrawCaptcha();
        });
    </script>

    <div class="app inner-pages check-status-page clearfix color1">
        <div class="check-status-form addedspace">
            <h4> Order Cancellation & Refund </h4>
            <asp:Literal ID="LiteralMessage" runat="server"></asp:Literal>

                              
      <div class="form-data clearfix">
                <div class="control">
                    <label for="orderno">Order No: *</label>
                    <input type="text" name="orderno" id="txtorderno"  placeholder="DLHSRP1221XXXXXX" maxlength="28" required="">
                </div>
                <div class="control">
                    <label for="vehiclereg">VehicleReg No: *</label>
                    <input type="text" name="vehiclereg" id="txtvehiclereg"  placeholder="DL01ABXXXX" maxlength="14" required="">
                </div>

                <div class="control">
                    <div class="captcha-input rightmargin-xs">
                        <label for="orderno">Captcha:</label>
                        <!--<input type="text" name="captchaorderno" id="txtcaptcha" placeholder="" required="">-->
                        <input type="text" name="Captcha" id="txtCaptcha" readonly="readonly" placeholder="Input Captcha" required="" />


                    </div>
                    <div class="captcha-img">
                        <%--<asp:Image ID="imgCaptcha" CssClass="topmargin-sm"/>--%>
                        <!--<img  class="topmargin-sm" id="captcha" src="../assets/img/captcha.png" alt="">-->
                        <button type="button" id="btnReferesh" onclick="DrawCaptcha();">
                            <img src="../assets/img/reload-icon.png"></button>

                    </div>

                </div>
                <div class="control">
                    <div class="captcha-input rightmargin-xs">
                        <label for="InputCaptcha" >Input Captcha:</label>
                        <input type="text" id="txtInput"  maxlength="12" />
                    </div>
                </div>

            </div>
                               
                                
                               <div class="form-data isforced clearfix">
                                      <div class="control">
                                          <button id="txtSearch">Search</button>
                                          <button id="txtCancel">Cancel</button>
                                      </div>

                                </div>
 


               



        
           
        </div>
    </div>
</asp:Content>
