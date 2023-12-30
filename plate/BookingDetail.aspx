<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookingDetail.aspx.cs" Inherits="BMHSRPv2.plate.BookingDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
           if (document.getElementById('<%= hdnStateID.ClientID%>').value == '31')
                $('#spanRegDate').css('display', 'none');
            else
                $('#spanRegDate').css('display', '')

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
                //ddlbs
                //alert(status);
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

                    var Stateid = $('#hdnStateID').val();
                  
                    if (Stateid == '31')
                    {
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
                //else if (GST == '') {
                //    alert('Please enter GST');
                //    return false;
                //}

                jQuery.ajax({
                    type: "GET",
                    url: "api/Get/SessionBookingDetail?SessionBs=" + BSType + "&SessionRD=" + RegDate + "&SessionRN=" + RegNo + "&SessionCHN=" + ChasisNo + "&SessionEN=" + EngineNo + "&SessionON=" + ownername + "&SessionEID=" + EmailID + "&SessionMn=" + MobileNo + "&SessionFP=" + filepath + "&SessionBA=" + BillingAddress + "&SessionState=" + State + "&SessionCity=" + City + "&SessionGST=" + GST + "&RcFileName=" + RcFileName,
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
                //ddlbs
                //alert(status);
                // $("#MainContent_hdnVehicleClass").val(status);
            });
        });
        //validation functions

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


        function ValidateEmail() {
            var Stateid = $('#hdnStateID').val();

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
            <div class="steps bars color1">
    <ul>
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
    </ul>
        <div class="backbtn" data-act="page6">
        <a href="VehicleType.aspx" class="gobacks"><img src="../assets/img/back.svg"></a>
    </div>
</div>            <div class="color2 view-data">
                <div class="in_pages page1 clearfix">
                    <div class="leftside color_blue">
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
                            <h4>Booking Details</h4>

                            <div class="clearfix ish5">
                                <div class="width50 width30">
                                    <h5>Vehicle Information</h5>
                                </div>
                                <div class="width50 width70">
                                    <div class="form-data clearfix">

                                            <div class="control">
                                            <label>Bharat Stage (BS)*</label>
                                            <select id="ddlbs">
                                                <option>Select Bharat Stage</option>
                                                <option>BS 3 & Others</option>
                                                <option>BS 4</option>
                                                <option>BS 6</option>
                                            </select>
                                        </div>
                                    
                                    </div>
                                    <div class="form-data clearfix">
                                        <div class="control">
                                            <label>Vehicle Registration Date(DD/MM/YYYY)*</label>
                                            <%--<input type="text" id="txtRegDate">--%>
                                            <input type="text" ID="txtRegDate" runat="server"
                                                onkeypress="return isNumberKey(event)" 
                                                onkeyup="DateValidator();"
                                                autocomplete="off"
                                                
                                                maxlength="10" 
                                                placeholder="DD/MM/YYYY">

                                            <span id="spanRegDate" class="helper">(Vehicle Reg. date before 01 APR 2019 only)</span>
                                        </div>
                                        <div class="control">
                                            <label>Registration Number*</label>
                                            <input type="text" id="txtRegNo"
                                             autocomplete="off"                                                 
                                                onkeyup="this.value = this.value.toUpperCase();" 
                                                maxlength="10" />
                                            <span class="helper">(Reg No. Ex.- DLXXXXX01)</span>
                                        </div>
                                    </div>
                                    <div class="form-data clearfix">
                                        <div class="control">
                                            <label>Chassis No.* (XXXXX12345)</label>
                                            <input type="text" id="txtChasisNo"
                                                 autocomplete="off"                                                 
                                                onkeyup="this.value = this.value.toUpperCase();" 
                                                maxlength="25" />
                                 <span class="helper" style="color:black">(Kindly enter last 5 digits including special characters (eg *,~,-,/,.) </span>
                                        </div>
                                        <div class="control">
                                            <label>Engine No.* (XXXXX12345)</label>
                                            <input type="text" id="txtEngineNo" 
                                                autocomplete="off"                                                 
                                                onkeyup="this.value = this.value.toUpperCase();" 
                                                maxlength="25" />
                                            <span class="helper" style="color:black">(Kindly enter last 5 digits)</span>
                                        </div>
                                    </div>

                                    <%--Uploader--%>
                                      <div  id="divrcupload"   visible="false" class="form-data clearfix" runat="server">
                                           
                                        <form id="form1" >
                                            <div class="control">
    <input type="file" id="fileid" name="postedFile" />
                                                <asp:HiddenField runat="server" ID="HiddenRcFileName" />
                                            </div>
                                           <div class="control">
<%--    <input type="button" style="background-color: #39435b; border-radius: 25px;"  id="btnUpload" value="Upload" />--%>
                                               <%-- <button type="reset"  id="btnUpload" style="float: left">Upload</button>--%>
                                                    <a id="btnUpload" class="filled"  style="float: right">Upload</a>
    <progress id="fileProgress" style="display: none"></progress>
                                                 
    <span  id="lblMessage" style="color: Green" class="helper"></span>
   
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
                            <div class="clearfix ish5">
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
                                                 />
                                            <span class="helper">Full Name</span>
                                        </div>
                                        <div class="control">

                                        </div>
                                    </div>
                                    <div class="form-data clearfix">
                                        <div class="control">
                                            <label>Email Id<span runat="server" id="EmailLabel">*</span></label>
                                            <input type="text" ID="txtEmailID" 
                                                autocomplete="off"
                                                onkeyup="ValidateEmail();"
                                               />
                                                
                                                 <span  id="lblError" style="color: red" class="helper"></span>
                                        </div>
                                        <div class="control">
                                            <label>Owner No. *</label>
                                            <input type="text" id="txtMobileNo" MaxLength="10"                                                
                                                autocomplete="off"
                                                onkeypress="return isNumberKey(event)" 
                                                ondrop="return false;" >
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
                                                />
                                        </div>
                                        <div class="control">
                                            <label>GST No. (Optional)</label>
                                            <input type="text" id="txtGST"
                                                   autocomplete="off"                                                 
                                                onkeyup="this.value = this.value.toUpperCase();" 
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
                                            <a id="BDID" class="filled"  style="float: right">Next</a>
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

