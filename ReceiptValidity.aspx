<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReceiptValidity.aspx.cs" Inherits="BMHSRPv2.ReceiptValidity" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   

    <script type="text/javascript">
        $(document).ready(function () {
            DrawCaptcha();
            $('#txtSearch').click(function () {
                if (!ValidCaptcha()) { return false; }

                //var Orderno = $('#txtorderno').val();
                var VehicleNo = $('#txtvehiclereg').val();
                if (VehicleNo == '')
                {
                    alert('VehicleNo should not be Blank!!');
                    return false;
                }
                var url = "ReceiptValidityDetail.aspx?REG_NUMBER=" + VehicleNo;
                window.location.href = url;
                /*
                $.ajax({
                    type: "GET",
                    url: "api/Get/ReceiptValidity?vehicleRegno=" + VehicleNo,

                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response != '') 
                        {
                            var user = JSON.parse(response);
                            if (user.REG_NUMBER == '') { return false; }
                            if (user.DayCount == '') { return false; }
                            if (user.DayCount <= 15)
                            {
                               
                               // var url = "ReceiptValidityDetail.aspx";
                                window.location.href = url;
                                //alert('Receipt is Not Expire')
                            }
                            else
                            {
                                alert('Receipt is Expire')
                                window.location.href = "OrderCancel.aspx";
                            }
                        }
                        else {
                            
                            alert("Please check Vehicle Registration No !!");
                        }
                    },
                    error: function (response) {
                        
                        //alert(response.responseText);
                    }
                });
                */
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
    
    <div class="app inner-pages check-status-page clearfix color1">
        <div class="check-status-form addedspace">
            <h4> Check Receipt Validity </h4>
            <asp:Literal ID="LiteralMessage" runat="server"></asp:Literal>

                              
      <div class="form-data clearfix">
                <%--<div class="control">
                    <label for="orderno">Order No: *</label>
                    <input type="text" name="orderno" id="txtorderno"  placeholder="DLHSRP1221XXXXXX" maxlength="28" required="">
                </div>--%>
                <div class="control">
                    <label for="vehiclereg">VehicleReg No: *</label>
                    <input type="text" name="vehiclereg" id="txtvehiclereg"  placeholder="DL01ABXXXX" maxlength="10" required="">
                </div>

                <div class="control">
                    <div class="captcha-input rightmargin-xs">
                        <label for="orderno">Captcha:</label>
                        <!--<input type="text" name="captchaorderno" id="txtcaptcha" placeholder="" required="">-->
                        <input type="text" name="Captcha" id="txtCaptcha" readonly="readonly" maxlength="10" placeholder="Input Captcha" required="" />


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
                               
                                
                               <div class="form-data isforced clearfix">
                                      <div class="control">
                                          <button id="txtSearch">Search</button>
                                          <button id="txtCancel">Cancel</button>
                                      </div>

                                </div>
 


               



        
           
        </div>
    </div>
</asp:Content>
