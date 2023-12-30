<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VahanBookingDetail.aspx.cs" Inherits="BMHSRPv2.sticker.VahanBookingDetail" %>

<%@ Register Src="~/sticker/ucStepBar.ascx" TagPrefix="uc1" TagName="ucStepBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
            $(document).ready(function () {
                DrawCaptcha();
            });
        </script>

<style>


.modal {display: none;position: fixed;z-index: 1;left: 0;top: 0;width: 100%;height: 100%;overflow: auto;background-color: rgba(0,0,0,0.75);}

.modal-content {background-color: #fefefe;margin: auto;padding: 20px;border: 1px solid #888;width: 60%;text-align: center;}

.close {font-size: 35px;opacity:1;}
.close:hover, .close:focus {color: #000;text-decoration: none;cursor: pointer;}
.helpbtn {font-weight: bold !important;padding:0 12px !important;background: rgba(255,177,177,0.55) !important;font-size: 12px !important;}

</style>

   

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
                $(document).ready(function () {
                    $('#StickerBDID').click(function () {
                        var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
                        var BSType = $('#ddlbs').val();
                        var RegDate = $('#ContentPlaceHolder1_txtRegDate').val();
                        var RegNo =    $('#txtRegNo').val();
                        var ChasisNo = $('#txtChasisNo').val();
                        var EngineNo = $('#txtEngineNo').val();
                        var ownername = $('#txtownername').val();
                        var EmailID = $('#txtEmailID').val();
                        var MobileNo = $('#txtMobileNo').val();
                        var filepath = $('#myfile').val();

                        var BillingAddress = $('#txtBillingAddress').val();
                        var State = $('#ddlState').val();
                        var City = $('#txtCity').val();
                        var GST = $('#txtGST').val();
                        var RcFileName = $('#ContentPlaceHolder1_HiddenRcFileName').val();
                        var FrontLaserFileName = $('#ContentPlaceHolder1_HiddenFrontFileName').val();
                        var RearLaserFileName = $('#ContentPlaceHolder1_HiddenRearFileName').val();
                        var File3 = $('#ContentPlaceHolder1_HiddenFile3').val();
                        var File4 = $('#ContentPlaceHolder1_HiddenFile4').val();
                        var FrontLaserCode = $('#txtflc').val();
                        var RearLaserCode = $('#txtrlc').val();
                        var Makervahan = $('#ContentPlaceHolder1_txtmakerVahan').val();
                        var VehicleTypeVahan = $('#ddlVehicleTypeVahan').val();// $('#ContentPlaceHolder1_txtVehicleTypeVahan').val();
                        var FuelTypeVahan = $('#ddlFuelTypeVahan').val();
                        var VehiceCatVahan = $('#ddlvchCatgVahan').val();
                        var Stateid = $('#ddlVehicleRegState').val();
                        //ddlbs
                        //alert(status);
                        // $("#MainContent_hdnVehicleClass").val(status);
                        if (BSType == '') {
                            alert('Please select Bharat Stage value');
                            return false;
                        }
                        else if (BSType == 'Select Bharat Stage')
                        {
                            alert('Please select Bharat Stage value');
                            return false;

                        }
                        else if (RegDate == '') {
                            alert('Please enter registration date');
                            return false;
                        }
                        else if (Makervahan == '') {
                            alert('Please provide maker');
                            return false;

                        }
                        else if (VehicleTypeVahan == '') {
                            alert('Please provide Vehicle Type');
                            return false;

                        }
                        else if (FuelTypeVahan == '') {
                            alert('Please provide Fuel Type');
                            return false;

                        }
                        else if (FuelTypeVahan == 'Select Fuel Type') {
                            alert('Please provide Fuel Type');
                            return false;

                        }

                        else if (VehiceCatVahan == '') {
                            alert('Please provide Fuel Type');
                            return false;

                        }
                        else if (VehiceCatVahan == 'Select Vehicle Category') {
                            alert('Please provide Vehicle Category');
                            return false;

                        }
              
                        else if (RegNo == '') {
                            alert('Please registration number');
                            return false;
                        }
                        else if (ChasisNo == '') {
                            alert('Please enter chasis number');
                            return false;
                        }
                        else if (EngineNo == '') {
                            alert('Please enter Engine number');
                            return false;
                        }
                        else if (ownername == '') {
                            alert('Please enter owner name');
                            return false;
                        }
                        else if (EmailID == '') {
                            var Stateid = $('#ddlVehicleRegState').val();
                            if (Stateid == '31') {
                                EmailID = 'bmhreceipt@rosmertahsrp.com';
                            }
                            else {
                                alert('Please enter Email Id');
                                return false;
                            }


                        }
                        
                      else  if (!expr.test(EmailID)) {
                            alert('Invalid Email id.');
                            return false;
                        }
                else if (MobileNo == '' || MobileNo.length < 10) {
                            alert('Please enter Mobile No');
                            return false;
                        }

                        else if (BillingAddress == '') {
                            alert('Please enter Billing Address ');
                            return false;
                        }
                        else if (State == '') {
                            alert('Please enter State');
                            return false;
                        }
                        else if (City == '') {
                            alert('Please enter City');
                            return false;
                        }
                    //else if (GST == '') {
                    //    alert('Please enter GST');
                    //    return false;
                    //}
                        else if (FrontLaserCode == '') {
                            alert('Please enter Front Laser Code');
                            return false;
                        }
                        else if (RearLaserCode == '') {
                            alert('Please enter Rear Laser Code');
                            return false;
                        }

                        
                        
                        jQuery.ajax({
                            type: "GET",
                            url: "api/Get/SetSessionBookingDetail?SessionBs=" + BSType + "&SessionRD=" + RegDate + "&SessionRN=" + RegNo + "&SessionCHN=" + ChasisNo + "&SessionEN=" + EngineNo + "&SessionON=" + ownername + "&SessionEID=" + EmailID + "&SessionMn=" + MobileNo + "&SessionFP=" + filepath + "&SessionBA=" + BillingAddress + "&SessionState=" + State + "&SessionCity=" + City + "&SessionGST=" + GST + "&SFLCode=" + FrontLaserCode + "&SRLCode=" + RearLaserCode + "&Maker=" + Makervahan + "&VehicleType=" + VehicleTypeVahan + "&FuelType=" + FuelTypeVahan + "&VehiceCat=" + VehiceCatVahan + "&Stateid=" + Stateid + "&FrontLaserFileName=" + FrontLaserFileName + "&RearLaserFileName=" + RearLaserFileName + "&filename3=" + File3 + "&filename4=" + File4,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                data = response;
                                 //alert(data);
                               
                                if (data == "Success") {
                                   // window.location.href = "Dealers.aspx";
                                    window.location.href = "DeliveryPoint.aspx";
                                }
                        else if (data == "unsuccessfulbooking") {
                            $("#divMessage").css("display", "");
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
                    //Reset Button
                    $('#btnVehicleDetailsReset').click(function () {

                        ClearFields();
                        $('#Div2WCategory').hide();
                        $('#DivVehicleDetails').hide();
                        $('#DivContactInfo').hide();
                        $('#txtRegNo').val('');
                        $('#txtChasisNo').val('');
                        $('#txtEngineNo').val('');
                        $('#txtflc').val('');
                        $('#txtrlc').val('');
                       

                    });
                    $('#btnreset').click(function () {
                        var BSType = $('#ddlbs').val();
                        var RegDate = $('#ContentPlaceHolder1_txtRegDate').val();
                        var RegNo = $('#txtRegNo').val();
                        var ChasisNo = $('#txtChasisNo').val();
                        var EngineNo = $('#txtEngineNo').val();
                        var ownername = $('#txtownername').val();
                        var EmailID = $('#txtEmailID').val();
                        var MobileNo = $('#txtMobileNo').val();

                        var BillingAddress = $('#txtBillingAddress').val();
                        var State = $('#ddlState').val();
                        var City = $('#txtCity').val();
                        var GST = $('#txtGST').val();

                        BSType = "";
                        RegDate = "";
                        RegNo = "";
                        ChasisNo = "";
                        EngineNo = "";
                        ownername = "";
                        EmailID = "";
                        MobileNo = "";
                        BillingAddress = "";
                        State = "";
                        City = "";
                        GST = "";
                        ClearFields();
                        $('#DivVehicleDetails').hide();
                        $('#DivContactInfo').hide();
                        $('#txtRegNo').removeAttr('readonly');
                        $('#txtChasisNo').removeAttr('readonly');
                        $('#txtEngineNo').removeAttr('readonly');
                        $('#txtflc').removeAttr('readonly');
                        $('#txtrlc').removeAttr('readonly');
                      
                        //ddlbs
                        //alert(status);
                        // $("#MainContent_hdnVehicleClass").val(status);
                    });  
                    function convertDate(inputFormat) {
                        function pad(s) { return (s < 10) ? '0' + s : s; }
                        var d = new Date(inputFormat)
                        return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/')
                    }
                    $('#btnValidatVahan').click(function () {

                       
                        var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
              
                        var RegNo =    $('#txtRegNo').val();
                        var ChasisNo = $('#txtChasisNo').val();
                        var EngineNo = $('#txtEngineNo').val();
                        var Stateid = $('#ddlVehicleRegState').val();
                        var FrontLaserCode = $('#txtflc').val();
                        var RearLaserCode =  $('#txtrlc').val();


                        if (Stateid == '') {
                            alert('Please Select  Vehicle Registration State');
                            return false;
                        }
                        if (Stateid == 'Select Vehicle Registration State') {
                            alert('Please Select  Vehicle Registration State');
                            return false;
                        }
                        if (Stateid == '31') {
                            $('#EmailLabel').hide();
                        }
                        else
                        {
                            $('#EmailLabel').show();
                        }

                        if (RegNo == '') {
                            alert('Please provide Registration Number');
                            return false;
                        }
                        if (ChasisNo == '') {
                            alert('Please provide Valid Chassis No');
                            return false;
                        }
                        if (EngineNo == '') {
                            alert('Please provide Valid Engine number');
                            return false;
                        }
                        else if (FrontLaserCode == '') {
                            alert('Please enter Front Laser Code');
                            return false;
                        }
                        else if (RearLaserCode == '') {
                            alert('Please enter Rear Laser Code');
                            return false;
                        }

                        if (!ValidCaptcha()) return false;
                       
                        $("body").loading();
                        jQuery.ajax({
                            type: "GET",
                            url: "api/Get/ValidateData?RegNumber=" + RegNo + "&ChassisNo=" + ChasisNo + "&EngineNo=" + EngineNo + "&Stateid=" + Stateid + "&FrontLaserCode=" + FrontLaserCode + "&RearLaserCode=" + RearLaserCode,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                data = response;
                                //alert(data);
                                $("body").loading("stop");
                                if (data.Status == "1")
                                {


                                    if (data.Msg == "Vehicle details available in Vahan")
                                    {
                                        $('#txtRegNo').attr('readonly', 'readonly');
                                        $('#txtChasisNo').attr('readonly', 'readonly');
                                        $('#txtEngineNo').attr('readonly', 'readonly');
                                        $('#txtflc').attr('readonly', 'readonly');
                                        $('#txtrlc').attr('readonly', 'readonly');

                                        $('#DivContactInfo').show();
                                        $('#DivVehicleDetails').show();


                                        $("#ContentPlaceHolder1_txtRegDate").val(convertDate(data.ResponseData.regnDate));
                                        $("#ContentPlaceHolder1_txtRegDate").attr('readonly', 'readonly');


                                        if (data.ResponseData.norms == 'Not Available') {
                                            $('#ddlbs').prop("disabled", false);
                                        }
                                        else {
                                            $('#ddlbs').val(data.ResponseData.norms);
                                            $('#ddlbs').prop("disabled", true);
                                        }


                                        if (data.ResponseData.fuel == 'NOT APPLICABLE') {
                                            $('#ddlFuelTypeVahan').prop("disabled", false);
                                        }
                                        else {
                                            $('#ddlFuelTypeVahan').val(data.ResponseData.fuel);
                                            $('#ddlFuelTypeVahan').prop("disabled", true);
                                        }

                                        if (data.UploadFlag == 'Y') {
                                            alert('UPLOAD YOUR FRONT REGISTRATION AND REAR REGISTRATION PLATE PHOTO');
                                            $('#ContentPlaceHolder1_divrcupload').show();
                                        }
                                        $('#ContentPlaceHolder1_txtmakerVahan').val(data.ResponseData.maker);
                                        $('#ContentPlaceHolder1_txtmakerVahan').attr('readonly', 'readonly');
                                        $('#ddlVehicleTypeVahan').val(data.ResponseData.vchType);
                                        $('#ddlVehicleTypeVahan').prop("disabled", true);
                                        $('#ContentPlaceHolder1_txtVehicleTypeVahan').val(data.ResponseData.vchType);
                                        $('#ContentPlaceHolder1_txtVehicleTypeVahan').attr('readonly', 'readonly');

                                        $('#ddlvchCatgVahan').val(data.ResponseData.vchCatg);
                                        $('#ddlvchCatgVahan').prop("disabled", true);
                                        $('#txtownername').focus();
                                        //alert(data.ResponseData.message);
                                    }
                                    

                                    else if (data.Msg.includes('Vehicle details available in Vahan but')) // "Vehicle Present Maker Not Present in HOMOLOGATION   and you are not authorized vendor for this vehicle" || data.Msg == "Vehicle Present and you are not authorized vendor for this vehicle")
                                    {
                                        
                                        ClearFields();
                                        $('#DivVehicleDetails').show();
                                        $('#DivContactInfo').show();

                                        if (data.UploadFlag == 'Y')
                                        {
                                            alert('UPLOAD YOUR DATA ALONG WITH FRONT REGISTRATION AND REAR REGISTRATION PLATE PHOTO ');
                                            $('#ContentPlaceHolder1_divrcupload').show();
                                        }
                                       // $('#ContentPlaceHolder1_divrcupload').show();
                                        if (data.ResponseData.maker != '')
                                        {
                                            $('#ContentPlaceHolder1_txtmakerVahan').val(data.ResponseData.maker);
                                            $('#ContentPlaceHolder1_txtmakerVahan').attr('readonly', 'readonly');

                                        }
                                        else {
                                            $('#ContentPlaceHolder1_txtmakerVahan').removeAttr('readonly');
                                        }
                                      
                                        $('#ContentPlaceHolder1_txtVehicleTypeVahan').removeAttr('readonly');
                                        $("#ContentPlaceHolder1_txtRegDate").removeAttr('readonly');
                                        $('#ddlVehicleTypeVahan').prop("disabled", false);
                                        $('#ddlbs').prop("disabled", false);
                                        $('#ddlFuelTypeVahan').prop("disabled", false);
                                        $('#ddlvchCatgVahan').prop("disabled", false);
                                    }

                                    else {
                                        alert(data.Msg);
                                        window.location.href = "VehicleMake.aspx";

                                         }
                                    //else
                                    //{
                                    //    //alert('UPLOAD YOUR FRONT REGISTRATION AND REAR REGISTRATION PLATE PHOTO');
                                    //    //ClearFields();
                                    //    //$('#DivVehicleDetails').show();
                                    //    //$('#DivContactInfo').show();

                                    //    //$('#ContentPlaceHolder1_divrcupload').show();

                                    //    //$('#ContentPlaceHolder1_txtmakerVahan').removeAttr('readonly');
                                    //    //$('#ContentPlaceHolder1_txtVehicleTypeVahan').removeAttr('readonly');
                                    //    //$("#ContentPlaceHolder1_txtRegDate").removeAttr('readonly');
                                    //    //$('#ddlVehicleTypeVahan').prop("disabled", false);
                                    //    //$('#ddlbs').prop("disabled", false);
                                    //    //$('#ddlFuelTypeVahan').prop("disabled", false);
                                    //    //$('#ddlvchCatgVahan').prop("disabled", false);
                                    //    ClearFields();

                                    //    $('#DivVehicleDetails').hide();
                                    //    $('#DivContactInfo').hide();
                                    //    alert(data.Msg);


                                    //}





                                }
                                else
                                {

                                   // window.location.href = "VehicleMake.aspx";

                                    if (data.Msg == "Vehicle details available in Vahan" || data.Msg.includes('Vehicle details available in Vahan but')) {
                                        window.location.href = "VehicleMake.aspx";
                                    }
                                    else
                                    {
                                        alert(data.Msg);
                                    }
                                    //window.location.href = "VehicleMake.aspx";
                                    //ClearFields();

                                    //$('#DivVehicleDetails').hide();
                                    //$('#DivContactInfo').hide();
                                    //alert(data.Msg);
                                }

                            },
                            failure: function (response) {
                                $("body").loading("stop");
                                alert(response);


                            }

                        });
                        return false;
                    });
                });
                //validation functions
                function ClearFields() {
                    $("#ContentPlaceHolder1_txtRegDate").val('');
                    $('#ddlbs').val('Select Bharat Stage');
                    $('#ddlFuelTypeVahan').val('Select Fuel Type');
                    $('#ddlvchCatgVahan').val('Select Vehicle Category');
                    $('#ddlVehicleTypeVahan').val('Select Vehicle Type');
                    $('#ContentPlaceHolder1_txtmakerVahan').val('');
                    $('#ContentPlaceHolder1_txtVehicleTypeVahan').val('');
                    $('#ContentPlaceHolder1_HiddenFrontFileName').val('');
                    $('#ContentPlaceHolder1_HiddenRearFileName').val('');
                    $('#txtInput').val('');
                    

                }
                function DateValidator() {

                    var Val_date = $('#ContentPlaceHolder1_txtRegDate').val();
                    var v = Val_date;

                    if (v.match(/^\d{2}$/) !== null) {
                        $("#ContentPlaceHolder1_txtRegDate").val(v + '/');
                    } else if (v.match(/^\d{2}\/\d{2}$/) !== null) {
                        $("#ContentPlaceHolder1_txtRegDate").val(v + '/');
                    }
                   

                    if (v.substring(5, 2).replace('/', '') > 12) {
                        alert("Invalid Month date format! " + v);
                        $("#ContentPlaceHolder1_txtRegDate").val('');
                        return false;
                    }

                    if (v.substring(0, 2).replace('/', '') > 31) {
                        alert("Invalid Day of date format! " + v);
                        $("#ContentPlaceHolder1_txtRegDate").val('');
                        return false;
                    }


                    if (v.substring(5, 2).replace('/', '') == "01") {
                        if (v.substring(0, 2) > 31) {
                            alert("Invalid date format! " + v);
                            $("#ContentPlaceHolder1_txtRegDate").val('');
                            return false;
                        }
                    } else if (v.substring(5, 2).replace('/', '') == "02") {
                        if (v.substring(0, 2) > 29) {
                            alert("Invalid date format! " + v);
                            $("#ContentPlaceHolder1_txtRegDate").val('');
                            return false;
                        }
                    } else if (v.substring(5, 2).replace('/', '') == "03") {
                        if (v.substring(0, 2) > 31) {
                            alert("Invalid date format! " + v);
                            $("#ContentPlaceHolder1_txtRegDate").val('');
                            return false;
                        }
                    } else if (v.substring(5, 2).replace('/', '') == "04") {
                        if (v.substring(0, 2) > 30) {
                            alert("Invalid date format! " + v);
                            $("#ContentPlaceHolder1_txtRegDate").val('');
                            return false;
                        }
                    } else if (v.substring(5, 2).replace('/', '') == "05") {
                        if (v.substring(0, 2) > 31) {
                            alert("Invalid date format! " + v);
                            $("#ContentPlaceHolder1_txtRegDate").val('');
                            return false;
                        }
                    } else if (v.substring(5, 2).replace('/', '') == "06") {
                        if (v.substring(0, 2) > 30) {
                            alert("Invalid date format! " + v);
                            $("#ContentPlaceHolder1_txtRegDate").val('');
                            return false;
                        }
                    } else if (v.substring(5, 2).replace('/', '') == "07") {
                        if (v.substring(0, 2) > 31) {
                            alert("Invalid date format! " + v);
                            $("#ContentPlaceHolder1_txtRegDate").val('');
                            return false;
                        }
                    } else if (v.substring(5, 2).replace('/', '') == "08") {
                        if (v.substring(0, 2) > 31) {
                            alert("Invalid date format! " + v);
                            $("#ContentPlaceHolder1_txtRegDate").val('');
                            return false;
                        }
                    } else if (v.substring(5, 2).replace('/', '') == "09") {
                        if (v.substring(0, 2) > 30) {
                            alert("Invalid date format! " + v);
                            $("#ContentPlaceHolder1_txtRegDate").val('');
                            return false;
                        }
                    } else if (v.substring(5, 2).replace('/', '') == "10") {
                        if (v.substring(0, 2) > 31) {
                            alert("Invalid date format! " + v);
                            $("#ContentPlaceHolder1_txtRegDate").val('');
                            return false;
                        }
                    } else if (v.substring(5, 2).replace('/', '') == "11") {
                        if (v.substring(0, 2) > 30) {
                            alert("Invalid date format! " + v);
                            $("#ContentPlaceHolder1_txtRegDate").val('');
                            return false;
                        }
                    } else if (v.substring(5, 2).replace('/', '') == "12") {
                        if (v.substring(0, 2) > 31) {
                            alert("Invalid date format! " + v);
                            $("#ContentPlaceHolder1_txtRegDate").val('');
                            return false;
                        }
                    }



                    //return false;
                }

                function isNumberKey(evt) { // Numbers only
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if (charCode > 31 && (charCode < 48 || charCode > 57))
                        return false;
                    return true;
                }
                

                function ValidateEmail() {
                    var Stateid = $('#ddlVehicleRegState').val();

                    if (Stateid == '31') {

                    }
                    else {
                        var email = $('#txtEmailID').val();
                        var lblError = document.getElementById("lblError");
                        lblError.innerHTML = "";
                        var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
                        if (!expr.test(email)) {
                            lblError.innerHTML = "Invalid email address.";
                        }
                    }
                }

                //sticker validation
                function capitalize(textboxid, str) {
                    // string with alteast one character
                    if (str && str.length >= 1) {
                        var firstChar = str.charAt(0);
                        var remainingStr = str.slice(1);
                        str = firstChar.toUpperCase() + remainingStr;
                    }
                    document.getElementById(textboxid).value = str;
                }
                //End sticker validation

                function blockSpecialCharforreg(e) {
                    var k;
                    document.all ? k = e.keyCode : k = e.which;
                    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || (k >= 48 && k <= 57));
                }
                //End validation functions
                function blockSpecialChar(e) {
                    var k;
                    document.all ? k = e.keyCode : k = e.which;
                    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
                }

                function blockSomeSpecialChar(e) {
                    var k;
                    document.all ? k = e.keyCode : k = e.which;
                    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 126 || k == 42 || k == 46 || (k >= 48 && k <= 57));
                }


            </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--JavaScript--%>
     <%--<script language="javascript" type="text/javascript">--%>
         <asp:HiddenField ID="hdnStateID" ClientIDMode="Static" runat="server" Value="" />
    <%--JavaScript--%>
     <div class="app clearfix">
        <section class="after">
            <uc1:ucStepBar runat="server" id="ucStepBar" PageStep="1" BackPage="../Index.aspx" />

            <div class="color2 view-data">
                <div class="in_pages page1 clearfix">
                    <div class="leftside color_blue" style="display:none">
                         <div class="head">
                           <%-- <img src="../assets/img/h1.png" draggable="false">--%>
                             <asp:Literal ID="LiteralBookingTypeImage" runat="server"></asp:Literal>
                        </div>
                        <div class="card" style="display: none">
                            <div class="label_card bikefill">
                               <%-- <img src="../assets/img/bike-fill.svg" draggable="false">--%>
                                <asp:Literal ID="LiteralVehicleTypeImage" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="card">
                            <div class="label_card">
                                  <asp:Literal ID="LiteralOemImage" runat="server"></asp:Literal>
                              <%--  <img src="../assets/img/color0.svg" draggable="false">--%>
                            </div>
                        </div>
                        <div class="card">
                            <div class="state label_card">
                                <asp:Literal ID="LiteralState" runat="server"></asp:Literal>
                               <%-- <p><span>HR</span>Haryana</p>--%>
                            </div>
                        </div>
                        <div class="card">
                            <div class="label_card bikefill">
                                  <asp:Literal ID="LiteralVehicleClassImage" runat="server"></asp:Literal>
                              <%--  <img src="../assets/img/car-fill1.svg" draggable="false">
                                <p>Private Vehicle</p>--%>
                            </div>
                        </div>
                        <div class="card">
                            <div class="state label_card mb0">
                                 <asp:Literal ID="LiteralFuelType" runat="server"></asp:Literal>
                               <%-- <p><span>Petrol</span></p>--%>
                            </div>
                        </div>
                    </div>
                    <div class="rightside">
                        <div class="addedspace">
                            <h4>Booking Details</h4>

                            <div class="clearfix ish5">
                                <div class="width50 width30">
                                    <h5>Vehicle Information</h5>
                                </div>
                                <div class="width50 width70">
                                    <div class="alert alert-danger form-data clearfix" role="alert" id="divMessage" style="display: none">
                                        <b>In case of Unsuccessful Booking. Kindly <a href="../Document/Only for 3rd Sticker.pdf" target="_blank">Click Here</a></b>
                                    </div>

                                     <div class="form-data clearfix">

                                           <div class="control" >
                                            <label>State*</label>
                                            <select id="ddlVehicleRegState">
                                                <option>Select Vehicle Registration State</option>
                                                <%--<option value="31">Uttar Pradesh</option>
                                                <option value="37">Delhi</option>--%>
                                                 <%--<option value="3">Himachal Pradesh</option>--%>
                                               <option value="5">Madhya Pradesh</option>
                                               
                                            </select>
                                        </div>
                                            <div class="control">
                                            <label>Registration Number*</label>
                                            <input type="text" id="txtRegNo"
                                             autocomplete="off"                                                 
                                                onkeyup="this.value = this.value.toUpperCase();" 
                                                   onkeypress="return blockSpecialCharforreg(event)"
                                                maxlength="10" />
                                            <span class="helper">(Reg No. Ex.- DL3CBV7501)</span>
                                        </div>
                                         </div>

                                     <div class="form-data clearfix">
                                        <div class="control">
                                          <label>Chassis No.* (XXXXX12345)</label>
                                            <input type="text" id="txtChasisNo"
                                                 autocomplete="off"                                                 
                                                onkeyup="this.value = this.value.toUpperCase();" 
                                                onkeypress="return blockSomeSpecialChar(event)"
                                                maxlength="25" />
                                             <span class="helper" style="color:black">(Kindly enter last 5 digits including special characters (eg *,~,-,/,.) </span>
                                        </div>
                                        <div class="control">
                                            <label>Engine No.* (XXXXX12345)</label>
                                            <input type="text" id="txtEngineNo" 
                                                autocomplete="off"                                                 
                                                onkeyup="this.value = this.value.toUpperCase();" 
                                                onkeypress="return blockSomeSpecialChar(event)"
                                                maxlength="25" />
                                             <span class="helper" style="color:black">(Kindly enter last 5 digits)</span>
                                        </div>
                                    </div>

                                    
                                    <%--add column front and rear laser code--%>
                                     <div class="form-data clearfix">
                                        <div class="control">
                                            <label>Front Laser Code*</label>
                                            <input type="text" id="txtflc"
                                                 autocomplete="off"                                                 
                                                onkeyup="javascript:capitalize(this.id, this.value);" 
                                                   onkeypress="return blockSpecialCharforreg(event)"
                                                maxlength="25" />
                                        </div>
                                        <div class="control">
                                            <label>Rear Laser Code*</label>
                                            <input type="text" id="txtrlc" 
                                                autocomplete="off"                                                 
                                                   onkeyup="javascript:capitalize(this.id, this.value);"  
                                                   onkeypress="return blockSpecialCharforreg(event)"
                                                maxlength="25" />
                                        </div>
                                           </div>
                                         <div class="form-data clearfix">
                                        <div class="control width50">
                                            <div class="captcha-input ">
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
                                                <input type="text" id="txtInput"
                                                       onkeypress="return isNumberKey(event)" />
                                            </div>
                                        </div>
                                    </div>
                                  

                                      <div class="form-data clearfix">
                                        <div class="control">
                                               <a id="btnValidatVahan" class="filled"  style="float:left">Click Here</a>
                                        </div>

                                           <div class="control" style="display:none">
                                               <a id="btnVehicleDetailsReset" class="filled"  style="float:right">Reset</a>
                                        </div>



                                    </div>

                                      </div>
                                 </div>

                                      <div id="DivVehicleDetails" style="display:none" class="clearfix ish5">
                                <div class="width50 width30">
                                    <h5>Vehicle Details</h5>
                                </div>
                                <div class="width50 width70">

                                       <div class="form-data clearfix">

                           
                                          <div class="control">
                                            <label>Bharat Stage (BS)*</label>
                                            <select id="ddlbs">
                                                <option>Select Bharat Stage</option>
                                                <option value='Bharat (Trem) Stage III B'>Bharat (Trem) Stage III B</option>
                                                <option value='Bharat (Trem) Stage IV'>Bharat (Trem) Stage IV</option>
                                                <option value='Bharat (Trem) Stage V'>Bharat (Trem) Stage V</option>
                                                <option value='BHARAT STAGE I'>BHARAT STAGE I</option>
                                                <option value='BHARAT STAGE II'>BHARAT STAGE II</option>
                                                <option value='BHARAT STAGE III'>BHARAT STAGE III</option>
                                                <option value='Bharat Stage III (CEV)'>Bharat Stage III (CEV)</option>
                                                <option value='BHARAT STAGE III/IV'>BHARAT STAGE III/IV</option>
                                                <option value='BHARAT STAGE IV'>BHARAT STAGE IV</option>
                                                <option value='Bharat Stage IV (CEV)'>Bharat Stage IV (CEV)</option>
                                                <option value='BHARAT STAGE IV/VI'>BHARAT STAGE IV/VI</option>
                                                <option value='Bharat Stage V (CEV)'>Bharat Stage V (CEV)</option>
                                                <option value='BHARAT STAGE VI'>BHARAT STAGE VI</option>
                                                <option value='EURO  6AD'>EURO  6AD</option>
                                                <option value='EURO 1'>EURO 1</option>
                                                <option value='EURO 2'>EURO 2</option>
                                                <option value='EURO 3'>EURO 3</option>
                                                <option value='EURO 4'>EURO 4</option>
                                                <option value='EURO 6'>EURO 6</option>
                                                <option value='EURO 6A'>EURO 6A</option>
                                                <option value='EURO 6B'>EURO 6B</option>
                                                <option value='EURO 6C'>EURO 6C</option>
                                                <option value='EURO 6D'>EURO 6D</option>
                                             
                                            </select>
                                        </div>

                                        <div class="control">
                                            <label>Vehicle Registration Date(DD/MM/YYYY)*</label>
                                            <%--<input type="text" id="txtRegDate">--%>
                                            <input type="text" ID="txtRegDate" runat="server"
                                                onkeypress="return isNumberKey(event)" 
                                                onkeyup="DateValidator();"
                                                autocomplete="off"
                                                
                                                maxlength="10" 
                                                placeholder="DD/MM/YYYY">

                                            <span id="spanRegDate" class="helper">(Vehicle Reg. date before 01 APR 2020 only)</span>
                                        </div>
                                    
                                    </div>

                                       <div class="form-data clearfix">

                                            <div class="control">
                                            <label>Fuel Type*</label>
                                            <select id="ddlFuelTypeVahan" >
                                                <option>Select Fuel Type</option>
                                               <option value='CNG ONLY'>CNG ONLY</option>
                                                <option value='DIESEL'>DIESEL</option>
                                                <option value='DIESEL/HYBRID'>DIESEL/HYBRID</option>
                                                <option value='ELECTRIC(BOV)'>ELECTRIC(BOV)</option>
                                                <option value='ETHANOL'>ETHANOL</option>
                                                <option value='LNG'>LNG</option>
                                                <option value='LPG ONLY'>LPG ONLY</option>
                                                <option value='METHANOL'>METHANOL</option>
                                                <option value='PETROL'>PETROL</option>
                                                <option value='PETROL/CNG'>PETROL/CNG</option>
                                                <option value='PETROL/ETHANOL'>PETROL/ETHANOL</option>
                                                <option value='PETROL/HYBRID'>PETROL/HYBRID</option>
                                                <option value='PETROL/LPG'>PETROL/LPG</option>
                                                <option value='PETROL/METHANOL'>PETROL/METHANOL</option>
                                                <option value='SOLAR'>SOLAR</option>
                                            </select>
                                        </div>
                                          
                                         
                                           <div class="control">
                                            <label>maker*</label>
                                            <%--<input type="text" id="txtRegDate">--%>
                                            <input type="text" ID="txtmakerVahan"  runat="server"
                                                autocomplete="off">
                                           
                                        </div>
                                    
                                    </div>

                                        <div class="form-data clearfix">

                                            <div class="control">
                                            <label>Vehicle Type*</label>
                                                <select id="ddlVehicleTypeVahan">
                                                <option>Select Vehicle Type</option>
                                                    <option value='Transport'>Transport</option>
                                                     <option value='Non-Transport'>Non-Transport</option>
                                                    </select>
                                             <input type="text" ID="txtVehicleTypeVahan" runat="server" style="display:none"
                                              
                                                autocomplete="off">
                                        </div>
                                          
                                         
                                           <div class="control">
                                            <label>Vehicle Category *</label>
                                            <%--<input type="text" id="txtRegDate">--%>
                                         <%--   <input type="text" ID="txtvchCatgVahan" readonly="readonly"  runat="server"
                                                autocomplete="off">--%>
                                                 <select id="ddlvchCatgVahan">
                                                      <option>Select Vehicle Category</option>
                                             
                                                <option value='3WN'>3WN</option>
                                                <option value='3WT'>3WT</option>
                                                <option value='LPV'>LPV</option>
                                                <option value='4WIC'>4WIC</option>
                                                <option value='LMV'>LMV</option>
                                                <option value='HPV'>HPV</option>
                                                <option value='MPV'>MPV</option>
                                                <option value='HMV'>HMV</option>
                                                <option value='MMV'>MMV</option>
                                                <option value='LGV'>LGV</option>
                                                <option value='MGV'>MGV</option>
                                                <option value='HGV'>HGV</option>
                                               
                                           </select>
                                        </div>
                                    
                                    </div>



                                                     
                                   

                                    <%--Uploader--%>
                                      <div  id="divrcupload" style="display:none;overflow:hidden;"  class="form-data clearfix" runat="server">
                                           
                                        <form id="form1" >
                                             <asp:HiddenField runat="server" ID="HiddenRcFileName" />
                                                 <asp:HiddenField runat="server" ID="HiddenFrontFileName" />
                                                 <asp:HiddenField runat="server" ID="HiddenRearFileName" />
                                                 <asp:HiddenField runat="server" ID="HiddenFile3" />
                                                 <asp:HiddenField runat="server" ID="HiddenFile4" />
                                             <div class="form-data clearfix">
                                                 <div class="control">
                                                     <label>Front Laser Photo * &nbsp;<button type="button" data-modal="photo1-modal" class="helpbtn">?</button></label>
                                                     <input type="file" id="fileid" name="postedFile" />
                                                 </div>

                                                 <%--Photo1 Hidden--%>
                                                 <div id="photo1-modal" class="modal">
                                                     <div class="modal-content"><span class="close">&times;</span><img src="../assets/img/for3rdsticker/Photo1.png" /></div>
                                                 </div>
                                                 
                                                 <div class="control">
                                                     <label>Rear Laser Photo * &nbsp;<button type="button" data-modal="photo2-modal" class="helpbtn">?</button></label>
                                                     <input type="file" id="fileid2" name="postedFile" />
                                                 </div>

                                                 <%--Photo2 Hidden--%>
                                                 <div id="photo2-modal" class="modal">
                                                     <div class="modal-content"><span class="close">&times;</span><img src="../assets/img/for3rdsticker/Photo2.png" /></div>
                                                 </div>

                                             </div>

                                             <div class="form-data clearfix">
                                                 <div class="control">
                                                     <label>Front Plate Photo * &nbsp;<button type="button" data-modal="photo3-modal" class="helpbtn">?</button></label>
                                                     <input type="file" id="fileid3" name="postedFile" />
                                                 </div>

                                                 <%--Photo3 Hidden--%>
                                                 <div id="photo3-modal" class="modal">
                                                     <div class="modal-content"><span class="close">&times;</span><img src="../assets/img/for3rdsticker/Photo3.png" /></div>
                                                 </div>

                                                 <div class="control">
                                                     <label>Rear Plate Photo * &nbsp;<button type="button" data-modal="photo4-modal" class="helpbtn">?</button></label>
                                                     <input type="file" id="fileid4" name="postedFile" />
                                                 </div>

                                                 <%--Photo4 Hidden--%>
                                                 <div id="photo4-modal" class="modal">
                                                     <div class="modal-content"><span class="close">&times;</span><img src="../assets/img/for3rdsticker/Photo4.png" /></div>
                                                 </div>

                                            </div>
                                                 
                                            <div class="form-data clearfix">
                                                 <div class="control">

                                                    <a id="btnUpload" class="filled"  style="float: left">Upload</a>
                                                    <progress id="fileProgress" style="display: none"></progress>
                                                 
                                                    <span  id="lblMessage" style="color: Green" class="helper"></span>
   
                                           </div>

                                            </div>
                                            <hr />  

                                               
                                            </div>
                                                

                                          
    
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnUpload", function () {

            $.ajax({
                url: 'FileUploadHandler.ashx',
                type: 'POST',
                data: new FormData($('form')[0]),
                cache: false,
                contentType: false,
                processData: false,
                success: function (file) {
                    $("#fileProgress").hide();
                    if (file.name.startsWith("Error!"))
                        alert(file.name);
                    //$("#lblMessage").html(file.name);
                    else {
                       // $("#lblMessage").html("<b>" + file.name + "</b> and <b> " + file.name2 + "</b >  has been uploaded.");
                        $("#lblMessage").html("Files has been uploaded.");
                        $('#ContentPlaceHolder1_HiddenRcFileName').val(file.name);
                        $('#ContentPlaceHolder1_HiddenFrontFileName').val(file.name);
                        $('#ContentPlaceHolder1_HiddenRearFileName').val(file.name2);
                        $('#ContentPlaceHolder1_HiddenFile3').val(file.name3);
                        $('#ContentPlaceHolder1_HiddenFile4').val(file.name4);
                        
                    }
                },
                xhr: function () {
                    var fileXhr = $.ajaxSettings.xhr();
                    if (fileXhr.upload) {
                        $("progress").show();
                        fileXhr.upload.addEventListener("progress", function (e) {
                            if (e.lengthComputable) {
                                $("#fileProgress").attr({
                                    value: e.loaded,
                                    max: e.total
                                });
                            }
                        }, false);
                    }
                    return fileXhr;
                }
            });
        });
    </script>
    </form>
                                    </div>

                                    
                                    
                                    <%-- End Uploader--%>


                                </div>
                            </div>
                            
                                     <div id="DivContactInfo" style="display:none" class="clearfix ish5">
                                <div class="width50 width30">
                                    <h5>Contact Information</h5>
                                </div>
                                <div class="width50 width70">
                                    <div class="form-data clearfix">
                                        <div class="control">
                                            <label>Owner Name*</label>
                                            <input type="text" id="txtownername"
                                                 autocomplete="off"                                                 
                                                onkeyup="this.value = this.value.toUpperCase();" 
                                                 onkeypress="return blockSpecialChar(event)"
                                                 />
                                            <span class="helper">Full Name</span>
                                        </div>
                                        <div class="control">

                                        </div>
                                    </div>
                                    <div class="form-data clearfix">
                                        <div class="control">
                                            <label>Email Id<span id="EmailLabel">*</span></label>
                                            <input type="text" ID="txtEmailID" 
                                                autocomplete="off"
                                                onkeyup="ValidateEmail();"
                                               />
                                                
                                                 <span  id="lblError" style="color: red" class="helper"></span>
                                        </div>
                                        <div class="control">
                                            <label>Owner No.*</label>
                                            <input type="text" id="txtMobileNo" MaxLength="10"                                                
                                                autocomplete="off"
                                                onkeypress="return isNumberKey(event)" 
                                                ondrop="return false;" >
                                        </div>
                                    </div>

                                        <div class="form-data clearfix">
                                        <div class="control">
                                            <label>BillingAddress*</label>
                                            <input type="text"  autocomplete="off" id="txtBillingAddress">
                                        </div>
                                       <div class="control">
                                            <label>State*</label>
                                            <%--<input type="text" id="txtState"
                                                 autocomplete="off"                                                 
                                                onkeyup="this.value = this.value.toUpperCase();" 
                                                 />--%>

                                               <asp:DropDownList CssClass="text" runat="server" ID="ddlState" ClientIDMode="Static"></asp:DropDownList>
                                        </div>
                                    </div>

                                         <div class="form-data clearfix">
                                        <div class="control">
                                            <label>City*</label>
                                            <input type="text" id="txtCity"
                                             autocomplete="off"                                                 
                                                onkeyup="this.value = this.value.toUpperCase();" 
                                                 onkeypress="return blockSpecialChar(event)"
                                                />
                                        </div>
                                        <div class="control">
                                            <label>GST No. (Optional)</label>
                                            <input type="text" id="txtGST"
                                                   autocomplete="off"                                                 
                                                onkeyup="this.value = this.value.toUpperCase();" 
                                                 onkeypress="return blockSpecialChar(event)"
                                                />
                                        </div>
                                    </div>
                                    
                                    <div class="form-data isforced clearfix" style="margin-top: 45px">
                                        <table style="border-collapse: separate; border-spacing: 10px;">
                                            <tr>
                                                <td>If any details provided by me for HSRP is found incorrect (Partially/Fully). The HSRP will not be affixed to my vehicle and HSRP Fee will not be Refundable. </td>
                                            </tr>
                                        </table>
                                     </div>

                                    <div class="form-data isforced clearfix" style="margin-top: 45px">
                                        <div class="control">
                                            &nbsp;
                                        </div>
                                        <div class="control center clearfix">
                                            <button type="reset" id="btnreset" style="float: left">Reset</button>
                                            <a id="StickerBDID" class="filled"  style="float: right">Next</a>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

            </div>
        </section>
    </div>
    <script>
        
        //$("#btnupload").("click", "#btnUpload", function ()
      
        </script>

    <%--ModalScript--%>

    <script>
        var modalBtns = [...document.querySelectorAll(".helpbtn")];
        modalBtns.forEach(function(btn){
            btn.onclick = function() {
                var modal = btn.getAttribute('data-modal');
                document.getElementById(modal).style.display = "block";
            }
        });

        var closeBtns = [...document.querySelectorAll(".close")];
        closeBtns.forEach(function(btn){
            btn.onclick = function() {
                var modal = btn.closest('.modal');
                modal.style.display = "none";
            }
        });

        window.onclick = function(event) {
            if (event.target.className === "modal") {
                event.target.style.display = "none";
            }
        }
    </script>


</asp:Content>

