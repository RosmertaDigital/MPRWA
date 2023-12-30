<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TrackOrder.aspx.cs" Inherits="BMHSRPv2.TrackOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            DrawCaptcha();
        });
    </script>     

    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtSearch').click(function () {
                //if (user.ORDER_NUMBER == '' || user.REG_NUMBER == '') { return false; }
                if (!ValidCaptcha()) return false;

                var Orderno = $('#txtorderno').val();
                var VehicleNo = $('#txtvehiclereg').val();
                //alert(Orderno);
                $.ajax({
                    type: "GET",
                    url: "api/Get/TrackOrderNo?orderno=" + Orderno + " &vehicleRegno= " + VehicleNo,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response != '') {
                            var user = JSON.parse(response);
                            if (user.ORDER_NUMBER == '' || user.REG_NUMBER == '') { return false; }
                            //var url = "AppointmentStatus.aspx?ORDER_NUMBER=" + encodeURIComponent(user.ORDER_NUMBER) + "&REG_NUMBER=" + encodeURIComponent(user.REG_NUMBER);
                            var url = "AppointmentStatus.aspx";
                            //window.location.href = encodeURIComponent(url);
                            window.location.href = url;
                        }
                        else {
                            
                            alert("Data Not Found");
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app inner-pages check-status-page clearfix color1">
        <div class="check-status-form addedspace">
            <h4>Track Your Order</h4>



            <div class="form-data clearfix">
                <div class="control">
                    <label for="orderno">Order No: *</label>
                    <input type="text" name="orderno" id="txtorderno" placeholder="DLHSRP1221XXXXXX" required="">
                </div>
                <div class="control">
                    <label for="vehiclereg">VehicleReg No: *</label>
                    <input type="text" name="vehiclereg" id="txtvehiclereg" placeholder="DL01ABXXXX" required="">
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
                        <label for="InputCaptcha">Input Captcha:</label>
                        <input type="text" id="txtInput" />
                    </div>
                </div>

            </div>



            <div class="App clearfix">
                <div class="control">
                    <%--<button id ="txtSearch" onclick="return ValidCaptcha();">Search</button>--%>
                    <button id="txtSearch">Search</button>
                    <button id="txtCancel">Cancel</button>
                </div>
            </div>




        </div>
    </div>
</asp:Content>
