<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderCancelCaptchaCheck.aspx.cs" Inherits="BMHSRPv2.OrderCancelCaptchaCheck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    

   
    <script type="text/javascript">
        $(document).ready(function () {
            DrawCaptcha();
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
         $(document).ready(function () {
             $('#txtSearch').click(function () {
                 if (!ValidCaptcha()) return false;
                 var Orderno = $("#<%=txtorder.ClientID%>").val();
                 var VehicleNo = $("#<%=txtVeh.ClientID%>").val();

                 
                 var url = "FinalCancelStep.aspx";
                // var url = "FinalCancelStep.aspx?ORDER_NUMBER=" + Orderno + "&REG_NUMBER=" + VehicleNo;
                 window.location.href = url;
                 return false;
             });
         });
     </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtCancel').click(function () {
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
            <h4>Order Cancel </h4>
            <div>
                <asp:Literal ID="LiteralMessage" runat="server"></asp:Literal>
            </div>


            <div class="form-data clearfix">

                <div class="control">
                      <div class="captcha-input rightmargin-xs">
                        <label for="orderno">Order:</label>
                        <!--<input type="text" name="captchaorderno" id="txtcaptcha" placeholder="" required="">-->
                        <input type="text" name="order" id="txtorder" runat="server"  placeholder="Input Captcha" required="" readonly="readonly" />
                   </div>

                    <div class="captcha-input rightmargin-xs">
                        <label for="orderno">VehicleNo:</label>
                        <!--<input type="text" name="captchaorderno" id="txtcaptcha" placeholder="" required="">-->
                        <input type="text" name="order" id="txtVeh" runat="server" placeholder="Input Captcha" required="" readonly="readonly" />
                   </div>
                    
                    <div class="captcha-input rightmargin-xs">
                        <label for="orderno">Captcha:</label>
                        <!--<input type="text" name="captchaorderno" id="txtcaptcha" placeholder="" required="">-->
                        <input type="text" name="Captcha" id="txtCaptcha"  placeholder="Input Captcha" required="" readonly="readonly" />

                  
                   </div>
                   
                    <div class="captcha-img">
                        <%--<asp:Image ID="imgCaptcha" CssClass="topmargin-sm"/>--%>
                        <!--<img  class="topmargin-sm" id="captcha" src="../assets/img/captcha.png" alt="">-->
                        <button type="button" id="btnReferesh" onclick="DrawCaptcha();">
                            <img src="../assets/img/reload-icon.png"></button>
                    </div>

                </div>
                <div>
                    
                </div>
                <div class="control">
                    <div class="captcha-input rightmargin-xs">
                        <label for="InputCaptcha">Input Captcha:</label>
                        <input type="text" id="txtInput"/>
                    </div>
                </div>

            </div>



            <div class="App clearfix">
                <div class="control">
                    <%--<button id ="txtSearch" onclick="return ValidCaptcha();">Search</button>--%>
                    <%--<button class="filled"  id="btnConfirm" onServerClick="btnConfirm_OnClick"  runat="server">Search</button>--%>
                    <button class="filled"  id="txtSearch">Search</button>

                    <button id="txtCancel">Cancel</button>
                   
                    
                </div>
            </div>




        </div>
    </div>
</asp:Content>
