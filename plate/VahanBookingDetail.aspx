<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VahanBookingDetail.aspx.cs" Inherits="BMHSRPv2.plate.VahanBookingDetail" %>

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
        $(document).ready(function () {
            if (document.getElementById('<%= hdnStateID.ClientID%>').value == '31')
                $('#spanRegDate').css('display', 'none');
            else
                $('#spanRegDate').css('display', '');

            $('#BDID').click(function () {
                var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
                var BSType = $('#ddlbs').val();
                var RegDate = $('#ContentPlaceHolder1_txtRegDate').val();
                var RegNo = $('#txtRegNo').val();
                var ChasisNo = $('#txtChasisNo').val();
                var EngineNo = $('#txtEngineNo').val();
                var ownername = $('#txtownername').val();
                var EmailID = $('#txtEmailID').val();
                var MobileNo = $('#txtMobileNo').val();
                var filepath = '';

                var BillingAddress = $('#txtBillingAddress').val();
                var State = $('#ContentPlaceHolder1_ddlState').val();

                var City = $('#txtCity').val();
                var GST = $('#txtGST').val();
                var RcFileName = $('#ContentPlaceHolder1_HiddenRcFileName').val();
                //alert(RcFileName);

                var Makervahan = $('#ContentPlaceHolder1_txtmakerVahan').val();
                var VehicleTypeVahan = $('#ddlVehicleTypeVahan').val();// $('#ContentPlaceHolder1_txtVehicleTypeVahan').val();
                var FuelTypeVahan = $('#ddlFuelTypeVahan').val();

                var VehiceCatVahan = $('#ddlvchCatgVahan').val();

                if (VehiceCatVahan == '2WN' || VehiceCatVahan == '2WIC' || VehiceCatVahan == '2WT') {
                    VehiceCatVahan = $('#ddlVehicleType2W').val();
                    if (VehiceCatVahan == '') {
                        alert('Please select Vehicle Category');
                        return false;
                    }
                    else if (VehiceCatVahan == 'Select Vehicle Category') {
                        alert('Please select Vehicle Category');
                        return false;

                    }


                }
                var Stateid = $('#ddlVehicleRegState').val();
                //ddlbs

                // $("#MainContent_hdnVehicleClass").val(status);


                if (BSType == '') {
                    alert('Please select Bharat Stage value');
                    return false;
                }
                else if (BSType == 'Select Bharat Stage') {
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
                else if (VehicleTypeVahan == 'Select Vehicle Type') {
                    alert('Please Select Vehicle Type');
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
                    alert('Please provide Registration Number');
                    return false;
                }
                else if (ChasisNo == '') {
                    alert('Please provide Valid Chassis No');
                    return false;
                }
                else if (EngineNo == '') {
                    alert('Please provide Valid Engine number');
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

                else if (!expr.test(EmailID)) {
                    alert('Invalid Email id.');
                    return false;
                }
                else if (MobileNo == '') {
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
                else if (State == '--Select State--') {
                    alert('Please Select State');
                    return false;
                }
                else if (City == '') {
                    alert('Please enter City');
                    return false;
                }
                else if (Stateid == '') {
                    alert('Please Select  Vehicle Registration State');
                    return false;
                }
                else if (Stateid == 'Select Vehicle Registration State') {
                    alert('Please Select  Vehicle Registration State');
                    return false;
                }



                jQuery.ajax({
                    type: "GET",
                    url: "api/Get/SetSessionBookingDetail?SessionBs=" + BSType + "&SessionRD=" + RegDate + "&SessionRN=" + RegNo + "&SessionCHN=" + ChasisNo + "&SessionEN=" + EngineNo + "&SessionON=" + ownername + "&SessionEID=" + EmailID + "&SessionMn=" + MobileNo + "&SessionFP=" + filepath + "&SessionBA=" + BillingAddress + "&SessionState=" + State + "&SessionCity=" + City + "&SessionGST=" + GST + "&RcFileName=" + RcFileName + "&Maker=" + Makervahan + "&VehicleType=" + VehicleTypeVahan + "&FuelType=" + FuelTypeVahan + "&VehiceCat=" + VehiceCatVahan + "&Stateid=" + Stateid,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        data = response;
                        //alert(data);

                        if (data == "Success") {
                            window.location.href = "DeliveryPoint.aspx";

                        }
                        else {
                            alert(data);
                        }

                    },
                    failure: function (response) {
                        alert(response);


                    }

                });
                return false;
            });
            //Reset Button


            $('#btnVehicleDetailsReset').click(function () {

                $('#txtRegNo').val('');
                $('#txtChasisNo').val('');
                $('#txtEngineNo').val('');
                $('#txtRegNo').removeAttr('readonly');
                $('#txtChasisNo').removeAttr('readonly');
                $('#txtEngineNo').removeAttr('readonly');

                ClearFields();
                $('#Div2WCategory').hide();
                $('#DivVehicleDetails').hide();
                $('#DivContactInfo').hide();


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
                var State = $('#ContentPlaceHolder1_ddlState').val();
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
                $('#Div2WCategory').hide();
                $('#DivVehicleDetails').hide();
                $('#DivContactInfo').hide();

                $('#txtRegNo').removeAttr('readonly');
                $('#txtChasisNo').removeAttr('readonly');
                $('#txtEngineNo').removeAttr('readonly');

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
                //var BSType = $('#ddlbs').val();
                //var RegDate = $('#ContentPlaceHolder1_txtRegDate').val();
                var RegNo = $('#txtRegNo').val();
                var ChasisNo = $('#txtChasisNo').val();
                var EngineNo = $('#txtEngineNo').val();
                var Stateid = $('#ddlVehicleRegState').val();


                //alert(Stateid);
                //var ownername = $('#txtownername').val();
                //var EmailID = $('#txtEmailID').val();
                //var MobileNo = $('#txtMobileNo').val();
                //var filepath = '';

                //var BillingAddress = $('#txtBillingAddress').val();
                //var State = $('#ContentPlaceHolder1_ddlState').val();

                //var City = $('#txtCity').val();
                //var GST = $('#txtGST').val();
                //var RcFileName = $('#ContentPlaceHolder1_HiddenRcFileName').val();
                //ddlbs
                //alert(status);
                // $("#MainContent_hdnVehicleClass").val(status);
                //if (BSType == '') {
                //    alert('Please select Bharat Stage value');
                //    return false;
                //}
                //else if (BSType == 'Select Bharat Stage') {
                //    alert('Please select Bharat Stage value');
                //    return false;

                //}
                //else if (RegDate == '') {
                //    alert('Please enter registration date');
                //    return false;
                //}

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
                else {
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

                //else if (ownername == '') {
                //    alert('Please enter owner name');
                //    return false;
                //}
                //else if (EmailID == '') {
                //    var Stateid = $('#hdnStateID').val();
                //    if (Stateid == '31') {
                //        EmailID = 'bmhreceipt@rosmertahsrp.com';
                //    }
                //    else {
                //        alert('Please enter Email Id');
                //        return false;
                //    }


                //}

                //else if (!expr.test(EmailID)) {
                //    alert('Invalid Email id.');
                //    return false;
                //}
                //else if (MobileNo == '') {
                //    alert('Please enter Mobile No');
                //    return false;
                //}

                //else if (BillingAddress == '') {
                //    alert('Please enter Billing Address ');
                //    return false;
                //}
                //else if (State == '') {
                //    alert('Please enter State');
                //    return false;
                //}
                //else if (State == '--Select State--') {
                //    alert('Please Select State');
                //    return false;
                //}
                //else if (City == '') {
                //    alert('Please enter City');
                //    return false;
                //}
                //else if (GST == '') {
                //    alert('Please enter GST');
                //    return false;
                //}

                if (!ValidCaptcha()) return false;

                $("body").loading();
                jQuery.ajax({
                    type: "GET",
                    url: "api/Get/ValidateData?RegNumber=" + RegNo + "&ChassisNo=" + ChasisNo + "&EngineNo=" + EngineNo + "&Stateid=" + Stateid,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        data = response;
                        $("body").loading("stop");

                        if (data.Status == "1") {


                            if (data.Msg == "Vehicle details available in Vahan") {
                                $('#txtRegNo').attr('readonly', 'readonly');
                                $('#txtChasisNo').attr('readonly', 'readonly');
                                $('#txtEngineNo').attr('readonly', 'readonly');
                                $('#DivContactInfo').show();
                                $('#DivVehicleDetails').show();
                                $('#ContentPlaceHolder1_divrcupload').hide();

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

                                $('#ContentPlaceHolder1_txtmakerVahan').val(data.ResponseData.maker);
                                $('#ContentPlaceHolder1_txtmakerVahan').attr('readonly', 'readonly');
                                $('#ddlVehicleTypeVahan').val(data.ResponseData.vchType);
                                if (data.ResponseData.vchCatg == '2WN' || data.ResponseData.vchCatg == '2WIC' || data.ResponseData.vchCatg == '2WT') {
                                    $('#Div2WCategory').show();
                                }
                                else {
                                    $('#Div2WCategory').hide();
                                }
                                $('#ddlVehicleTypeVahan').prop("disabled", true);
                                //if (data.ResponseData.vchCatg == 'OTH')
                                //{
                                //    window.location.href = "VehicleMake.aspx";
                                //}
                                $('#ContentPlaceHolder1_txtVehicleTypeVahan').val(data.ResponseData.vchType);
                                $('#ContentPlaceHolder1_txtVehicleTypeVahan').attr('readonly', 'readonly');

                                $('#ddlvchCatgVahan').val(data.ResponseData.vchCatg);
                                $('#ddlvchCatgVahan').prop("disabled", true);
                                $('#txtownername').focus();
                                //alert(data.ResponseData.message);
                            }



                            else if (data.Msg.includes('Vehicle details available in Vahan but Maker of this vehicle Not Present in HOMOLOGATION') || data.Msg == "Vehicle details available in Vahan but OEM/Manufacturer (Homologation) of this vehicle have not authorized you for the State/RTO of this vehicle, please contact respective OEM.") // "Vehicle Present Maker Not Present in HOMOLOGATION   and you are not authorized vendor for this vehicle")   // == "Vehicle Present Maker Not Present in HOMOLOGATION   and you are not authorized vendor for this vehicle")
                            {

                                alert('UPDATE DETAILS FROM YOUR REGISTRATION CERTIFICATE');
                                ClearFields();
                                $('#txtRegNo').attr('readonly', 'readonly');
                                $('#txtChasisNo').attr('readonly', 'readonly');
                                $('#txtEngineNo').attr('readonly', 'readonly');
                                $('#DivVehicleDetails').show();
                                $('#DivContactInfo').show();

                                $('#ContentPlaceHolder1_divrcupload').show();
                                if (data.ResponseData.maker != '') {
                                    $('#ContentPlaceHolder1_txtmakerVahan').val(data.ResponseData.maker);
                                    $('#ContentPlaceHolder1_txtmakerVahan').attr('readonly', 'readonly');

                                }
                                else {
                                    $('#ContentPlaceHolder1_txtmakerVahan').removeAttr('readonly');
                                }

                                $('#ddlVehicleTypeVahan').prop("disabled", false);
                                $('#ContentPlaceHolder1_txtVehicleTypeVahan').removeAttr('readonly');
                                $("#ContentPlaceHolder1_txtRegDate").removeAttr('readonly');

                                $('#ddlbs').prop("disabled", false);
                                $('#ddlFuelTypeVahan').prop("disabled", false);
                                $('#ddlvchCatgVahan').prop("disabled", false);
                            }

                            else {
                                window.location.href = "VehicleMake.aspx";

                            }






                        }
                        else {


                            if (data.Msg == "Vehicle details available in Vahan" || data.Msg.includes('Vehicle details available in Vahan but Maker of this vehicle Not Present in HOMOLOGATION')) {
                                ClearFields();
                                $('#DivVehicleDetails').hide();
                                $('#DivContactInfo').hide();
                                window.location.href = "VehicleMake.aspx";

                            }
                            else if (data.Msg == "Vehicle details available in Vahan but OEM/Manufacturer (Homologation) of this vehicle have not authorized you for the State/RTO of this vehicle, please contact respective OEM.") {
                                ClearFields();
                                $('#DivVehicleDetails').hide();
                                $('#DivContactInfo').hide();
                                window.location.href = "VehicleMake.aspx";
                            }
                            else {
                                ClearFields();
                                $('#DivVehicleDetails').hide();
                                $('#DivContactInfo').hide();
                                alert(data.Msg);
                            }





                            //alert(data.Msg);
                        }

                    },
                    failure: function (response) {
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
            $('#ContentPlaceHolder1_txtmakerVahan').val('');
            $('#ddlVehicleTypeVahan').val('Select Vehicle Type');
            $('#ContentPlaceHolder1_txtVehicleTypeVahan').val('');
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
        //function chkRegDate(obj)
        //{
        //    //validation
        //    if( obj.value.length<10)
        //    {
        //        obj.value = '';
        //        alert('Registration date Should be in MM/DD/YYYY');
        //    return false;
        //    }
        //    //edit by tek
        //    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/; //Declare Regex
        //    var dtArray = currVal.match(rxDatePattern); // is format OK?

        //    if (dtArray == null)
        //        return false;

        //    //Checks for mm/dd/yyyy format.
        //    dtMonth = dtArray[1];
        //    dtDay = dtArray[3];
        //    dtYear = dtArray[5];

        //    if (dtMonth < 1 || dtMonth > 12)
        //        return false;
        //    else if (dtDay < 1 || dtDay > 31)
        //        return false;
        //    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
        //        return false;
        //    else if (dtMonth == 2) {
        //        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        //        if (dtDay > 29 || (dtDay == 29 && !isleap))
        //            return false;
        //    }
        //    //End edit by tek
        //    return true;
        //}

        function GetVehicleCat() {
            var VehiceCatVahan = $('#ddlvchCatgVahan').val();

            if (VehiceCatVahan == '2WN' || VehiceCatVahan == '2WIC' || VehiceCatVahan == '2WT') {
                $('#Div2WCategory').show();
            }
            else {
                $('#Div2WCategory').hide();
            }


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

        function blockSpecialChar(e) {
            var k;
            document.all ? k = e.keyCode : k = e.which;
            return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
        }

        function blockSpecialCharforreg(e) {
            var k;
            document.all ? k = e.keyCode : k = e.which;
            return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || (k >= 48 && k <= 57));
        }

        function blockSomeSpecialChar(e) {
            var k;
            document.all ? k = e.keyCode : k = e.which;
            return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 126 || k == 42 || k == 46 || (k >= 48 && k <= 57));
        }

        //End validation functions

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--JavaScript--%>
    <%--<script language="javascript" type="text/javascript">--%>

    <asp:HiddenField ID="hdnStateID" ClientIDMode="Static" runat="server" Value="" />

    <%--JavaScript--%>
    <div class="app clearfix">
        <section class="after">
            <div class="steps bars color1" style="vertical-align: top !important">
                <%--<ul>
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
    </ul>--%>

                <ul>
                    <li><a href="#"><span>Step 1</span> Booking Details</a></li>
                    <li><a href="#"><span>Step 2</span> Fitment Location</a></li>
                    <li><a href="#"><span>Step 3</span> Appointment Slot</a></li>
                    <li><a href="#"><span>Step 4</span> Booking Summary</a></li>
                    <li><a href="#"><span>Step 5</span> Verify Details & Pay</a></li>
                    <li><a href="#"><span>Step 6</span> Download Receipt </a></li>
                </ul>
                <div class="backbtn" data-act="page1">
                    <a href="../Index.aspx" class="gobacks">
                        <img src="../assets/img/back.svg"></a>
                </div>
            </div>
            <div class="color2 view-data">
                <div class="in_pages page1 clearfix">
                    <div class="leftside color_blue" style="display: none">
                        <div class="head">
                            <%-- <img src="../assets/img/h1.png" draggable="false">--%>
                            <asp:Literal ID="LiteralBookingTypeImage" runat="server"></asp:Literal>
                        </div>
                        <div class="card">
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
                            <%--<h4>Booking Details</h4>--%>

                            <div class="row">
                                <div class="col-sm-8">
                                    <h4>Booking Details</h4>
                                </div>
                                <div class="col-sm-4">
                                    <asp:Button ID="btnBulkUpload" runat="server" Text="Bulk Upload" class="btn btn-primary" ForeColor="Black" OnClick="btnBulkUpload_Click"/>
                                </div>
                            </div>


                            <div class="clearfix ish5">
                                <div class="width50 width30">
                                    <h5>Vehicle Information</h5>
                                </div>
                                <div class="width50 width70">

                                    <div class="form-data clearfix">

                                        <div class="control">
                                            <label>State*</label>
                                            <select id="ddlVehicleRegState">
                                                <option>Select Vehicle Registration State</option>
                                                <%--<option value="31">Uttar Pradesh</option>
                                                <option value="37">Delhi</option>
                                                 <option value="3">Himachal Pradesh</option>
                                                 <option value="32">West Bengal</option>--%>
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
                                            <span class="helper" style="color: black">(Reg No. Ex.- DLXXXXX01)</span>
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
                                            <span class="helper" style="color: black">(Kindly enter last 5 digits including special characters (eg *,~,-,/,.) </span>
                                        </div>
                                        <div class="control">
                                            <label>Engine No.* (XXXXX12345)</label>
                                            <input type="text" id="txtEngineNo"
                                                autocomplete="off"
                                                onkeyup="this.value = this.value.toUpperCase();"
                                                onkeypress="return blockSomeSpecialChar(event)"
                                                maxlength="25" />
                                            <span class="helper" style="color: black">(Kindly enter last 5 digits)</span>
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
                                            <a id="btnValidatVahan" class="filled" style="float: left">Click Here</a>
                                        </div>
                                        <div class="control" style="display: none">
                                            <a id="btnVehicleDetailsReset" class="filled" style="float: right">Reset</a>
                                        </div>


                                    </div>
                                </div>
                            </div>

                            <div id="DivVehicleDetails" style="display: none" class="clearfix ish5">
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
                                            <input type="text" id="txtRegDate" runat="server"
                                                onkeypress="return isNumberKey(event)"
                                                onkeyup="DateValidator();"
                                                autocomplete="off"
                                                maxlength="10"
                                                placeholder="DD/MM/YYYY">

                                            <span id="spanRegDate" class="helper">(Vehicle Reg. date before 25 Nov 2019 only)</span>
                                        </div>

                                    </div>

                                    <div class="form-data clearfix">

                                        <div class="control">
                                            <label>Fuel Type*</label>
                                            <select id="ddlFuelTypeVahan">
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
                                            <input type="text" id="txtmakerVahan" runat="server"
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
                                            <input type="text" id="txtVehicleTypeVahan" runat="server" style="display: none"
                                                autocomplete="off">
                                        </div>


                                        <div class="control">
                                            <label>Vehicle Category *</label>
                                            <%--<input type="text" id="txtRegDate">--%>
                                            <%--   <input type="text" ID="txtvchCatgVahan" readonly="readonly"  runat="server"
                                                autocomplete="off">--%>
                                            <select id="ddlvchCatgVahan" onchange="GetVehicleCat()">
                                                <option>Select Vehicle Category</option>
                                                <option value='2WN'>2WN</option>
                                                <option value='2WT'>2WT</option>
                                                <option value='2WIC'>2WIC</option>
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

                                    <div class="form-data clearfix" id="Div2WCategory" style="display: none">
                                        <div class="control">
                                            <label>Vehicle Category *</label>
                                            <select id="ddlVehicleType2W">
                                                <option>Select Vehicle Category</option>
                                                <option value='Scooter'>Scooter</option>
                                                <option value='Motor Cycle'>Motor Cycle</option>
                                            </select>
                                        </div>

                                    </div>





                                    <%--Uploader--%>
                                    <div id="divrcupload" style="display: none" class="form-data clearfix" runat="server">

                                        <form id="form1">
                                            <div class="control">
                                                <label>Upload RC Here *</label>
                                                <input type="file" id="fileid" name="postedFile" />

                                                <asp:HiddenField runat="server" ID="HiddenRcFileName" />
                                            </div>
                                            <div class="control">
                                                <%--    <input type="button" style="background-color: #39435b; border-radius: 25px;"  id="btnUpload" value="Upload" />--%>
                                                <%-- <button type="reset"  id="btnUpload" style="float: left">Upload</button>--%>
                                                <a id="btnUpload" class="filled" style="float: right">Upload</a>
                                                <progress id="fileProgress" style="display: none"></progress>

                                                <span id="lblMessage" style="color: Green" class="helper"></span>

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
                                                                $("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                                                                $('#ContentPlaceHolder1_HiddenRcFileName').val(file.name);
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

                            <div id="DivContactInfo" style="display: none" class="clearfix ish5">
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
                                                onkeypress="return blockSpecialChar(event)" />
                                            <span class="helper">Full Name</span>
                                        </div>
                                        <div class="control">
                                        </div>
                                    </div>
                                    <div class="form-data clearfix">
                                        <div class="control">
                                            <label>Email Id<span id="EmailLabel">*</span></label>
                                            <input type="text" id="txtEmailID"
                                                autocomplete="off"
                                                onkeyup="ValidateEmail();" />

                                            <span id="lblError" style="color: red" class="helper"></span>
                                        </div>
                                        <div class="control">
                                            <label>Owner No.*</label>
                                            <input type="text" id="txtMobileNo" maxlength="10"
                                                autocomplete="off"
                                                onkeypress="return isNumberKey(event)"
                                                ondrop="return false;">
                                        </div>
                                    </div>

                                    <div class="form-data clearfix">
                                        <div class="control">
                                            <label>BillingAddress*</label>
                                            <textarea id="txtBillingAddress" name="txtBillingAddress" cols="40" rows="5"></textarea>
                                            <%--  <input type="text" aria-multiline="true"  autocomplete="off" id="txtBillingAddress">--%>
                                        </div>
                                        <div class="control">
                                            <label>State*</label>
                                            <%--<input type="text" id="txtState"
                                                 autocomplete="off"                                                 
                                                onkeyup="this.value = this.value.toUpperCase();" 
                                                 />--%>

                                            <asp:DropDownList CssClass="text" runat="server" ID="ddlState"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-data clearfix">
                                        <div class="control">
                                            <label>City*</label>
                                            <input type="text" id="txtCity"
                                                autocomplete="off"
                                                onkeyup="this.value = this.value.toUpperCase();"
                                                onkeypress="return blockSpecialChar(event)" />
                                        </div>
                                        <div class="control">
                                            <label>GST No. (Optional)</label>
                                            <input type="text" id="txtGST"
                                                autocomplete="off"
                                                onkeyup="this.value = this.value.toUpperCase();"
                                                onkeypress="return blockSpecialChar(event)" />
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
                                            <a id="BDID" class="filled" style="float: right">Next</a>
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
</asp:Content>

