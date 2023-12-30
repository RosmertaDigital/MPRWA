using BMHSRPv2.Models;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2.sticker
{
    public partial class VerifyDetailPayment : System.Web.UI.Page
    {

        string CnnString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetSideBar();
                if (Session["S_DeliveryPoint"].ToString()== "Home" || Session["S_DeliveryPoint"].ToString() == "rwa")
                {
                    trDeliveryCharge.Visible = true;
                }
                else
                {
                    trDeliveryCharge.Visible = false;
                }

                PaymentDescription();

            }
        }

        private void PaymentDescription()
        {
            string CheckOemRateQuery = "CheckOrdersRates '" + Session["S_OEMId"].ToString() + "', 'OB','" + Session["S_VehicleClass"].ToString() + "', '" + Session["S_VehicleType"].ToString() + "','" + Session["S_Vehiclecategoryid"].ToString() + "','" + Session["S_VehicleFuelType"].ToString() + "','" + Session["S_DeliveryPoint"].ToString() + "','" + Session["S_StateId"].ToString() + "','" + Session["S_SessionState"].ToString() + "','sticker'"; 

            DataTable dtOemRate = BMHSRPv2.Models.Utils.GetDataTable(CheckOemRateQuery, CnnString);
            if (dtOemRate.Rows.Count > 0)
            {
                string status = dtOemRate.Rows[0]["status"].ToString();

                #region
                if (status == "1")
                {
                    #region

                    //string FrontPlateSize = dtOemRate.Rows[0]["FrontPlateSize"].ToString();
                    //string RearPlateSize = dtOemRate.Rows[0]["RearPlateSize"].ToString();

                    //decimal GstBasic_Amt = Convert.ToDecimal(dtOemRate.Rows[0]["GstBasic_Amt"]) + Convert.ToDecimal(dtOemRate.Rows[0]["FittmentCharges"]);
                    //decimal FittmentCharges = Convert.ToDecimal(dtOemRate.Rows[0]["FittmentCharges"]);
                    //decimal BMHConvenienceCharges = Convert.ToDecimal(dtOemRate.Rows[0]["BMHConvenienceCharges"]);
                    //decimal BMHHomeCharges = Convert.ToDecimal(dtOemRate.Rows[0]["BMHHomeCharges"]);
                    //decimal GrossTotal = Convert.ToDecimal(dtOemRate.Rows[0]["GrossTotal"]);
                    //decimal GSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["GSTAmount"]);
                    //decimal IGSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["IGSTAmount"]);
                    //decimal CGSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["CGSTAmount"]);
                    //decimal SGSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["SGSTAmount"]);
                    //decimal GstRate = Convert.ToDecimal(dtOemRate.Rows[0]["gst"]);
                    //decimal TotalAmount = Convert.ToDecimal(dtOemRate.Rows[0]["TotalAmount"]);


                    //lblBasicAmt.Text = "Rs. " + string.Format("{0:0.00}", GstBasic_Amt);
                    //lblFitmentCharge.Text = "Rs. " + string.Format("{0:0.00}", FittmentCharges);
                    //lblConvenienceFee.Text = "Rs. " + string.Format("{0:0.00}", BMHConvenienceCharges);
                    //lblDeliveryCharge.Text = "Rs. " + string.Format("{0:0.00}", BMHHomeCharges);
                    //lblTotalBasicAmt.Text = "Rs. " + string.Format("{0:0.00}", GrossTotal);
                    //lblGST.Text = "Rs. " + string.Format("{0:0.00}", GSTAmount);
                    //lblIGST.Text = "Rs. " + string.Format("{0:0.00}", IGSTAmount);
                    //lblCGST.Text = "Rs. " + string.Format("{0:0.00}", CGSTAmount);
                    //lblSGST.Text = "Rs. " + string.Format("{0:0.00}", SGSTAmount);
                    //lblGrandTotal.Text = "Rs. " + string.Format("{0:0.00}", TotalAmount); string FrontPlateSize = dtOemRate.Rows[0]["FrontPlateSize"].ToString();
                    string RearPlateSize = dtOemRate.Rows[0]["RearPlateSize"].ToString();

                    decimal GstBasic_Amt = Convert.ToDecimal(dtOemRate.Rows[0]["GstBasic_Amt"]) + Convert.ToDecimal(dtOemRate.Rows[0]["FittmentCharges"]);
                    decimal FittmentCharges = Convert.ToDecimal(dtOemRate.Rows[0]["FittmentCharges"]);
                    // decimal BMHConvenienceCharges = Convert.ToDecimal(dtOemRate.Rows[0]["BMHConvenienceCharges"]);
                    // decimal BMHHomeCharges = Convert.ToDecimal(dtOemRate.Rows[0]["BMHHomeCharges"]);
                    decimal GrossTotal = GstBasic_Amt; //Convert.ToDecimal(dtOemRate.Rows[0]["GrossTotal"]);
                    //decimal GSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["GSTAmount"]);
                    //decimal IGSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["IGSTAmount"]);
                    //decimal CGSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["CGSTAmount"]);
                    //decimal SGSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["SGSTAmount"]);
                    //  decimal GstRate = Convert.ToDecimal(dtOemRate.Rows[0]["gst"]);
                    // decimal TotalAmount = Convert.ToDecimal(dtOemRate.Rows[0]["TotalAmount"]);
                    decimal _calculatedGst = GstBasic_Amt * Convert.ToDecimal(0.09);

                    decimal CGSTAmount = _calculatedGst;
                    decimal SGSTAmount = _calculatedGst;
                    decimal TotalAmount = GstBasic_Amt + CGSTAmount + SGSTAmount;

                    lblBasicAmt.Text = "Rs. " + string.Format("{0:0.00}", GstBasic_Amt);
                    lblFitmentCharge.Text = "Rs. " + string.Format("{0:0.00}", FittmentCharges);
                    //lblConvenienceFee.Text = "Rs. " + string.Format("{0:0.00}", BMHConvenienceCharges);
                    // lblDeliveryCharge.Text = "Rs. " + string.Format("{0:0.00}", BMHHomeCharges);
                    lblTotalBasicAmt.Text = "Rs. " + string.Format("{0:0.00}", GrossTotal);
                    //lblGST.Text = "Rs. " + string.Format("{0:0.00}", GSTAmount);
                    // lblIGST.Text = "Rs. " + string.Format("{0:0.00}", IGSTAmount);
                    lblCGST.Text = "Rs. " + string.Format("{0:0.00}", CGSTAmount);
                    lblSGST.Text = "Rs. " + string.Format("{0:0.00}", SGSTAmount);
                    lblGrandTotal.Text = "Rs. " + string.Format("{0:0.00}", TotalAmount);

                    //if (IGSTAmount == 0)
                    //{
                    //    divtrIGST.Visible = false;
                    //}

                    if (CGSTAmount == 0)
                    {
                        divtrCGST.Visible = false;
                    }

                    if (SGSTAmount == 0)
                    {
                        divtrSGST.Visible = false;
                    }
                    #endregion
                }
                else
                {
                    lblMessage.Text = dtOemRate.Rows[0]["message"].ToString();
                    lblMessage.Visible = true;
                    return;
                }
                #endregion
            }
            else
            {

                lblMessage.Text = "Rate not found.!!!";
                lblMessage.Visible = true;
                return;
            }

            try
            {
                lblOwnerName.Text = Session["S_SessionOwnerName"].ToString();
                lblMobile.Text = Session["S_SessionMobileNo"].ToString();
                lblAddress.Text = Session["S_SessionBillingAddress"].ToString();
                lblEmailID.Text = Session["S_SessionEmailID"].ToString();
                lblPinCode.Text = "";
            }
            catch(Exception ev)
            {

            }
        }

        private void SetSideBar()
        {
            LiteralBookingTypeImage.Text = "<img src='" + Session["S_OrderType_imgPath"].ToString() + "' draggable='false'>";
            LiteralVehicleTypeImage.Text = "<img src='" + Session["S_VehicleType_imgPath"].ToString() + "' draggable='false'>";
            LiteralOemImage.Text = "<img src='" + Session["S_OEMImgPath"].ToString() + "' draggable='false'>";
            LiteralState.Text = "<p><span>" + Session["S_StateShortName"].ToString() + "</span>" + Session["S_StateName"].ToString() + "</p>";
            LiteralVehicleClassImage.Text = "<img src='" + Session["S_VehicleClass_imgPath"].ToString() + "' draggable='false'>" +
                                                           "<p> " + Session["S_VehicleClass"].ToString() + " Vehicle </p>";
            LiteralFuelType.Text = "<p><span>" + Session["S_VehicleFuelType"].ToString() + "</span></p>";

            try
            {
                string CheckDealerAffixationQuery = "select a.OemID, c.Name OemName, a.DealerID, a.StateID, a.RTOLocationID,b.RTOLocationName,a.DealerAffixationCenterName, a.DealerAffixationCenterAddress  " +
              "from [HSRPOEM].dbo.DealerAffixationCenter a, [HSRPOEM].dbo.rtolocation b, [HSRPOEM].dbo.OEMMaster c  " +
              "where a.rtolocationid = b.RTOLocationID and a.OemID = c.OEMID " +
              " and DealerAffixationID = '" + Session["S_DealerAffixationCenterid"].ToString() + "' ";
                DataTable dtDealerAffixation = BMHSRPv2.Models.Utils.GetDataTable(CheckDealerAffixationQuery, CnnString);
                if (dtDealerAffixation.Rows.Count > 0)
                {

                    string ManufacturerName = dtDealerAffixation.Rows[0]["OemName"].ToString();
                    string DealerAffixationCenterName = dtDealerAffixation.Rows[0]["DealerAffixationCenterName"].ToString();
                    string DealerAffixationCenterAddress = dtDealerAffixation.Rows[0]["DealerAffixationCenterAddress"].ToString();

                    AffixationAddress.Text = "<b>" + DealerAffixationCenterName + "<br>" +
                               "</b>" +
                           "<p>" + DealerAffixationCenterAddress + " </p>";

                }

            }
            catch (Exception ex)
            {

            }

        }
        
        protected void btnPayNow_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = false;

            if (!cbAgree.Checked)
            {
                lblMessage.Text = "Please agree to all the terms & conditions before placing the order.";
                lblMessage.Visible = true;
                return ;
            }

            if (txtMobileNo.Text.ToString().Length == 0)
            {
                lblMessage.Text = "Please fill mobile number before placing the order.";
                lblMessage.Visible = true;
                return;
            }

            //bypassing this in case of RWA

            if (HttpContext.Current.Session["S_DeliveryPoint"].ToString() != "rwa")
            {

                string AppointmentBlockedDatesQuery = "CheckECAppointmentBlockedDates '" + Session["S_SelectedSlotDate"].ToString() + "', '" + Session["S_DealerAffixationCenterid"].ToString() + "','" + Session["S_DeliveryPoint"].ToString() + "','Sticker' ";
                System.Data.DataTable dtAppointmentBlockedDates = BMHSRPv2.Models.Utils.GetDataTable(AppointmentBlockedDatesQuery, CnnString);

                if (dtAppointmentBlockedDates.Rows.Count > 0 && dtAppointmentBlockedDates.Rows[0]["status"].ToString() == "1")
                {
                    lblMessage.Text = "Slot booked. Choose another Slot.";
                    lblMessage.Visible = true;
                    return;
                }
                else if (dtAppointmentBlockedDates.Rows.Count == 0)
                {
                    lblMessage.Text = "Slot booked. Choose another Slot !!!! ";
                    lblMessage.Visible = true;
                    return;
                }

            }
            /*
                OrderNo,OrderType,OrderStatus,SlotId,SlotTime,SlotBookingDate,HSRP_StateID,RTOLocationID,RTOName,
             *  HSRPRecord_CreationDate,OwnerName,OwnerFatherName,
                Address1,State,City,Pin,MobileNo,EmailID,VehicleClass,VehicleType,ManufacturerName,ManufacturerModel,ChassisNo,EngineNo,
                VehicleRegNo,TotalAmount,NetAmount,BookingType,Booking_classType,fuelType,Dealerid,OEMID,BookedFrom,AppointmentType
             */

            //string DealerAffixationCenterid = Session["S_DealerAffixationCenterid"].ToString();

            string OrderType = "OB";
            //string OrderStatus = "Initiated";
            string SlotId =  Session["S_SelectedSlotID"].ToString();
            string SlotTime =  Session["S_SelectedSlotTime"].ToString();
            string SlotBookingDate = Session["S_SelectedSlotDate"].ToString(); //StateId
            string HSRP_StateID = Session["S_StateId"].ToString();
            string RTOLocationID = string.Empty;
            string RTOName = string.Empty;
            string OwnerName = Session["S_SessionOwnerName"].ToString();
            string OwnerFatherName = "";
            string Address1 = Session["S_SessionBillingAddress"].ToString();
            string State = Session["S_SessionState"].ToString();
            string City = Session["S_SessionCity"].ToString();
            string Pin = "";
            string MobileNo = Session["S_SessionMobileNo"].ToString();
            string LandlineNo = txtMobileNo.Text.ToString();
            string EmailID = Session["S_SessionEmailID"].ToString();
            string VehicleClass = Session["S_VehicleClass"].ToString();
            string VehicleType = Session["S_VehicleType"].ToString();
            string ManufacturerName = "";
            //string ManufacturerModel = "";
            string ChassisNo = Session["S_SessionChasisno"].ToString();
            string EngineNo = Session["S_SessionEngno"].ToString();
            string VehicleRegNo = Session["S_SessionRegNo"].ToString();
            string ManufacturingYear = Session["S_SessionRegDate"].ToString();
            string FrontPlateSize = "";
            string RearPlateSize = "";
            string TotalAmount = "";
            string NetAmount = "";
            string BookingType = Session["S_VehicleCat"].ToString();//4W, 2W
            string Booking_classType = VehicleClass;
            string fuelType = Session["S_VehicleFuelType"].ToString();
            string Dealerid = "";
            string OEMID = Session["S_Oemid"].ToString();
            string BookedFrom = "Website";
            string AppointmentType = Session["S_DeliveryPoint"].ToString();
            string BasicAamount = "";
            string FitmentCharge = "";
            string ConvenienceFee = "";
            string HomeDeliveryCharge = "";
            string GSTAmount = "";
            string IGSTAmount = "";
            string CGSTAmount = "";
            string SGSTAmount = "";
            string CustomerGSTNo =Session["S_SessionGST"] != null && Session["S_SessionGST"].ToString().Length > 0 ? Session["S_SessionGST"].ToString() : "";
            string VehicleRCImage = "";
            string BharatStage = Session["S_SessionBharatStage"].ToString();
            string ShippingAddress1 = "";
            string ShippingAddress2 = "";
            string ShippingCity = "";
            string ShippingState = "";
            string ShippingPinCode = "";
            string ShippingLandMark = "";
            string DealerAffixationAddress = "";
            string FrontLaserCode = Session["S_FrontLaserCode"] == null ? "" : HttpContext.Current.Session["S_FrontLaserCode"].ToString();
            string RearLaserCode = Session["S_RearLaserCode"] == null ? "" : HttpContext.Current.Session["S_RearLaserCode"].ToString();
            //DealerAffixationCenterid = "367";

            if (Session["S_DeliveryPoint"].ToString() == "Home")
            {
                if (Session["S_DeliveryAddress1"] != null && Session["S_DeliveryAddress1"].ToString().Length > 0)
                {
                    ShippingAddress1 = Session["S_DeliveryAddress1"].ToString();
                    ShippingAddress2 = Session["S_DeliveryAddress2"].ToString();
                    ShippingCity = Session["S_Deliverycity"].ToString();
                    ShippingState = Session["S_DeliveryState"].ToString();
                    ShippingPinCode = Session["S_DeliveryPincode"].ToString();
                    ShippingLandMark = Session["S_DeliveryLandmark"] == null ? "" : Session["S_DeliveryLandmark"].ToString();
                }
                else if (Session["S_mapAddress1"] != null && Session["S_mapAddress1"].ToString().Length > 0)
                {
                    ShippingAddress1 = Session["S_mapAddress1"].ToString();
                    ShippingAddress2 = Session["S_mapAddress2"].ToString();
                    ShippingCity = Session["S_mapCity"].ToString();
                    ShippingState = Session["S_mapStates"].ToString();
                    ShippingPinCode = Session["S_mapPincode"].ToString();
                    ShippingLandMark = Session["S_mapLandmark"] == null ? "" : Session["S_mapLandmark"].ToString();
                }
            }

            try
            {
                string CheckDealerAffixationQuery = "select a.OemID, c.Name OemName, a.DealerID, a.StateID, a.RTOLocationID,b.RTOLocationName  " +
             "from [HSRPOEM].dbo.DealerAffixationCenter a, [HSRPOEM].dbo.rtolocation b, [HSRPOEM].dbo.OEMMaster c  " +
             "where a.rtolocationid = b.RTOLocationID and a.OemID = c.OEMID " +
             " and DealerAffixationID = '" + Session["S_DealerAffixationCenterid"].ToString()+ "' ";
                DataTable dtDealerAffixation = BMHSRPv2.Models.Utils.GetDataTable(CheckDealerAffixationQuery, CnnString);
                if (dtDealerAffixation.Rows.Count > 0)
                {
                    RTOLocationID = dtDealerAffixation.Rows[0]["RTOLocationID"].ToString();
                    RTOName = dtDealerAffixation.Rows[0]["RTOLocationName"].ToString();
                    Dealerid = dtDealerAffixation.Rows[0]["DealerID"].ToString();
                    //OEMID = dtDealerAffixation.Rows[0]["OemID"].ToString();
                    //ManufacturerName = dtDealerAffixation.Rows[0]["OemName"].ToString();
                }


                string CheckOEMQuery = "select oemid, name OemName from [HSRPOEM].dbo.OEMMaster where oemid = '" + Session["S_OEMId"].ToString() + "'";
                DataTable dtOem = BMHSRPv2.Models.Utils.GetDataTable(CheckOEMQuery, CnnString);
                if (dtOem.Rows.Count > 0)
                {
                    OEMID = dtOem.Rows[0]["OemID"].ToString();
                    ManufacturerName = dtOem.Rows[0]["OemName"].ToString();
                }
            }
            catch (Exception ev)
            {
                lblMessage.Text = ev.Message;
                lblMessage.Visible = true;
            }
            

            //Oemid = "46";
            //VehicleClass = "Transport";
            //VehicleType = "Motor Cycle";
            //VehicleCategoryID = "1";
            //FulType = "oth";
            //AppointmentType = "Dealer";

            try
            {
                string CheckOemRateQuery = "CheckOrdersRates '" + Session["S_OEMId"].ToString() + "', 'OB','" + Session["S_VehicleClass"].ToString() + "', '" + Session["S_VehicleType"].ToString() + "','" + Session["S_Vehiclecategoryid"].ToString() + "','" + Session["S_VehicleFuelType"].ToString() + "','" + Session["S_DeliveryPoint"].ToString() + "','" + Session["S_StateId"].ToString() + "','" + Session["S_SessionState"].ToString() + "','sticker'"; 

                DataTable dtOemRate = BMHSRPv2.Models.Utils.GetDataTable(CheckOemRateQuery, CnnString);
                if (dtOemRate.Rows.Count > 0)
                {
                    string status = dtOemRate.Rows[0]["status"].ToString();
                    #region
                    if (status == "1")
                    {
                        //FrontPlateSize = dtOemRate.Rows[0]["FrontPlateSize"].ToString();
                        //RearPlateSize = dtOemRate.Rows[0]["RearPlateSize"].ToString();
                        //BasicAamount = dtOemRate.Rows[0]["GstBasic_Amt"].ToString();
                        //FitmentCharge = dtOemRate.Rows[0]["FittmentCharges"].ToString();
                        //ConvenienceFee = dtOemRate.Rows[0]["BMHConvenienceCharges"].ToString();
                        //HomeDeliveryCharge = dtOemRate.Rows[0]["BMHHomeCharges"].ToString();
                        //TotalAmount = dtOemRate.Rows[0]["GrossTotal"].ToString();
                        //GSTAmount = dtOemRate.Rows[0]["GSTAmount"].ToString();
                        //IGSTAmount = dtOemRate.Rows[0]["IGSTAmount"].ToString();
                        //CGSTAmount = dtOemRate.Rows[0]["CGSTAmount"].ToString();
                        //SGSTAmount = dtOemRate.Rows[0]["SGSTAmount"].ToString();
                        //// = dtOemRate.Rows[0]["gst"].ToString();
                        //NetAmount = dtOemRate.Rows[0]["TotalAmount"].ToString();
                        decimal GstBasic_Amt = Convert.ToDecimal(dtOemRate.Rows[0]["GstBasic_Amt"]) + Convert.ToDecimal(dtOemRate.Rows[0]["FittmentCharges"]);

                        FrontPlateSize = dtOemRate.Rows[0]["FrontPlateSize"].ToString();
                        RearPlateSize = dtOemRate.Rows[0]["RearPlateSize"].ToString();
                        BasicAamount = GstBasic_Amt.ToString(); //dtOemRate.Rows[0]["GstBasic_Amt"].ToString();
                        FitmentCharge = dtOemRate.Rows[0]["FittmentCharges"].ToString();
                        ConvenienceFee = "0.00"; //dtOemRate.Rows[0]["BMHConvenienceCharges"].ToString();
                        HomeDeliveryCharge = "0.00"; //dtOemRate.Rows[0]["BMHHomeCharges"].ToString();

                        decimal _calculatedGst = Convert.ToDecimal(BasicAamount) * Convert.ToDecimal(0.09);

                        decimal _CGSTAmount = _calculatedGst;
                        decimal _SGSTAmount = _calculatedGst;
                        decimal _TotalAmount = Convert.ToDecimal(BasicAamount) + _CGSTAmount + _SGSTAmount;


                        TotalAmount = BasicAamount;  //dtOemRate.Rows[0]["GrossTotal"].ToString();
                        GSTAmount = GSTAmount = (_CGSTAmount + _SGSTAmount).ToString(); //dtOemRate.Rows[0]["GSTAmount"].ToString();
                        IGSTAmount = "0.00";// dtOemRate.Rows[0]["IGSTAmount"].ToString();
                        CGSTAmount = _CGSTAmount.ToString(); //dtOemRate.Rows[0]["CGSTAmount"].ToString();
                        SGSTAmount = _SGSTAmount.ToString();// dtOemRate.Rows[0]["SGSTAmount"].ToString();
                        // = dtOemRate.Rows[0]["gst"].ToString();
                        NetAmount = _TotalAmount.ToString("0.##"); //dtOemRate.Rows[0]["TotalAmount"].ToString();
                    }
                    else
                    {
                        lblMessage.Text = dtOemRate.Rows[0]["message"].ToString();
                        lblMessage.Visible = true;
                    }
                    #endregion

                }
                else
                {

                    lblMessage.Text = "Rate not found.!!";
                    lblMessage.Visible = true;
                    return;
                }
            }
            catch (Exception ev)
            {
                lblMessage.Text = ev.Message;
                lblMessage.Visible = true;
            }


            try
            {
                string OrderNo = "BMHSRP" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + GetRandomNumber();

                //string insertQuery = "insert into [BookMyHSRP].dbo.Appointment_BookingHist_test (OrderNo,OrderType,OrderStatus,SlotId,SlotTime,SlotBookingDate,HSRP_StateID,RTOLocationID," +
                //"RTOName,HSRPRecord_CreationDate," +
                //"OwnerName,OwnerFatherName,Address1,State,City,Pin,MobileNo,LandlineNo,EmailID,VehicleClass,VehicleType,ManufacturerName," +
                //"ChassisNo,EngineNo,ManufacturingYear,VehicleRegNo," +
                //"FrontPlateSize,RearPlateSize,TotalAmount,NetAmount," +
                //"BookingType,Booking_classType,fuelType,Dealerid,OEMID,BookedFrom," +
                //"AppointmentType,BasicAamount,FitmentCharge,ConvenienceFee,HomeDeliveryCharge,GSTAmount,CustomerGSTNo," +
                //"VehicleRCImage,BharatStage,ShippingAddress1,ShippingAddress2,ShippingCity,ShippingState,ShippingPinCode," +
                //"ShippingLandMark) values " +
                //"('" + OrderNo + "','" + OrderType + "','" + OrderStatus + "','" + SlotId + "','" + SlotTime + "','" + SlotBookingDate + "','" + HSRP_StateID + "','" + RTOLocationID + "','" + RTOName + "',getdate(),'" + OwnerName + "','" + OwnerFatherName + "','" + Address1 + "','" + State + "','" + City + "','" + Pin + "','" + MobileNo + "','" + LandlineNo + "','" + EmailID + "','" + VehicleClass + "','" + VehicleType + "','" + ManufacturerName + "','" + ChassisNo + "','" + EngineNo + "','" + ManufacturingYear + "','" + VehicleRegNo + "','" + FrontPlateSize + "','" + RearPlateSize + "','" + TotalAmount + "','" + NetAmount + "','" + BookingType + "','" + Booking_classType + "','" + fuelType + "','" + Dealerid + "','" + OEMID + "','" + BookedFrom + "','" + AppointmentType + "','" + BasicAamount + "','" + FitmentCharge + "','" + ConvenienceFee + "','" + HomeDeliveryCharge + "','" + GSTAmount + "','" + CustomerGSTNo + "','" + VehicleRCImage + "','" + BharatStage + "','" + ShippingAddress1 + "','" + ShippingAddress2 + "','" + ShippingCity + "','" + ShippingState + "','" + ShippingPinCode + "','" + ShippingLandMark + "') ";

                

                string PaymentInitiatedQuery = "PaymentInitiated '" + Session["S_DealerAffixationCenterid"].ToString() + "', '" + OrderNo + "','" + OrderType + "','" + SlotId + "','" + SlotTime + "','" + SlotBookingDate + "','" + HSRP_StateID + "','" + RTOLocationID + "','" + RTOName + "','" + OwnerName + "','" + OwnerFatherName + "','" + Address1 + "','" + State + "','" + City + "','" + Pin + "','" + MobileNo + "','" + LandlineNo + "','" + EmailID + "','" + VehicleClass + "','" + VehicleType + "','" + ManufacturerName + "','" + ChassisNo + "','" + EngineNo + "','" + ManufacturingYear + "','" + VehicleRegNo + "','" + FrontPlateSize + "','" + RearPlateSize + "','" + TotalAmount + "','" + NetAmount + "','" + BookingType + "','" + Booking_classType + "','" + fuelType + "','" + Dealerid + "','" + OEMID + "','" + BookedFrom + "','" + AppointmentType + "','" + BasicAamount + "','" + FitmentCharge + "','" + ConvenienceFee + "','" + HomeDeliveryCharge + "','" + GSTAmount + "','" + CustomerGSTNo + "','" + VehicleRCImage + "','" + BharatStage + "','" + ShippingAddress1 + "','" + ShippingAddress2 + "','" + ShippingCity + "','" + ShippingState + "','" + ShippingPinCode + "','" + ShippingLandMark + "','" + IGSTAmount + "','" + CGSTAmount + "','" + SGSTAmount + "','Sticker','"+FrontLaserCode + "','" +RearLaserCode +"'" ;

                DataTable dtPaymentConfirmation = BMHSRPv2.Models.Utils.GetDataTable(PaymentInitiatedQuery, CnnString);
                if (dtPaymentConfirmation.Rows.Count > 0)
                {
                    string Status = dtPaymentConfirmation.Rows[0]["status"].ToString();
                    OrderNo = dtPaymentConfirmation.Rows[0]["OrderNo"].ToString();
                    if (Status == "1")
                    {
                        /****
                         * Start for ccavenue
                         */ 
                        //CCAvenueForm(OrderNo, NetAmount, OwnerName, Address1, City, State, Pin, MobileNo, EmailID, (SlotBookingDate + " " + SlotTime), Session["DealerAffixationCenterid"].ToString(), DealerAffixationAddress, VehicleRegNo, SlotId);

                        //StringBuilder strScript = new StringBuilder();
                        //strScript.Append("<script language='javascript'>");
                        //strScript.Append("ValidateForm();");
                        //strScript.Append("</script>");
                        //this.Page.Controls.Add(new LiteralControl(strScript.ToString()));
                        /****
                         * End for ccavenue
                         */

                        /****
                         * Start for Razorpay
                         */
                        RazorPay(OrderNo, NetAmount, OwnerName, Address1, City, State, Pin, MobileNo, EmailID, (SlotBookingDate + " " + SlotTime), Session["S_DealerAffixationCenterid"].ToString(), DealerAffixationAddress, VehicleRegNo, SlotId);
                        StringBuilder RazorPayScript = new StringBuilder();
                        RazorPayScript.Append("<script language='javascript'>");
                        RazorPayScript.Append("ValidatePayForm();");
                        RazorPayScript.Append("</script>");
                        this.Page.Controls.Add(new LiteralControl(RazorPayScript.ToString()));
                        /****
                         * End for Razorpay
                         */
                    }
                }
                else
                {
                    lblMessage.Text = dtPaymentConfirmation.Rows[0]["message"].ToString();
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ev)
            {
                lblMessage.Text = ev.Message;
                lblMessage.Visible = true;
            }
        }

        public string GetRandomNumber()
        {
            Random r = new Random();
            var x = r.Next(0, 9);
            return x.ToString("0");
        }

        private void CCAvenueForm( string orderno,string GrandTotal,string OwnerName,string Address,
            string CityName,string StateName,string PinCode,
            string MobileNo,string Emailid,string AppointmentDateTime,string DealerAffaxtionCenterID,
            string DealerAffaxtionAddress,string Regno,string TimeSlotID)
        {

            string Host = ConfigurationManager.AppSettings["Host"].ToString();
            //string ccavResponseHandler = @"http://localhost:55098//plate/PaymentReceipt.aspx";
            string ccavResponseHandler = Host + "sticker/PaymentReceipt.aspx";

            StringBuilder sbTable = new StringBuilder();
            sbTable.Clear();

            GrandTotal = "1"; // on live plz comment or remove this line.

            sbTable.Append("<form id='customerData' name='customerData' method='post' action='https://rosmertahsrp.com/ccvbookmyhsrp/ccavRequestHandler.aspx' autocomplete='off'>");
            sbTable.Append("<div class='row'>");
            sbTable.Append("<input type='hidden' name='tid' id='tid' readonly />");
            sbTable.Append("<input type='hidden' name='merchant_id' id='merchant_id' value='212112' runat='server' />");
            sbTable.Append("<input type='hidden' name='order_id' id='order_id' value='" + orderno + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='amount'   id='amount'   value='" + GrandTotal + "'  runat='server' />");
            sbTable.Append("<input type='hidden' name='currency' id='currency' value='INR' runat='server' />");
            sbTable.Append("<input type='hidden' name='redirect_url' id='redirect_url' value='" + ccavResponseHandler + "' />");
            sbTable.Append("<input type='hidden' name='cancel_url' id='cancel_url' value='" + ccavResponseHandler + "' />");

            sbTable.Append("<input type='hidden' name='billing_name' id='billing_name'          value='" + OwnerName.ToString() + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='billing_address' id='billing_address'    value='" + Address.ToString() + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='billing_city' id='billing_city' value='" + CityName.ToString() + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='billing_state' id='billing_state' value='" + StateName.ToString() + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='billing_zip' id='billing_zip' value='" + PinCode.ToString() + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='billing_country' id='billing_country' value='India' runat='server' />");
            sbTable.Append("<input type='hidden' name='billing_tel' id='billing_tel' value='" + MobileNo.ToString() + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='billing_email' id='billing_email' value='" + Emailid.ToString() + "' runat='server' />");

            sbTable.Append("<input type='hidden' name='delivery_name' id='delivery_name' runat='server' />");
            sbTable.Append("<input type='hidden' name='delivery_address' id='delivery_address' runat='server' />");
            sbTable.Append("<input type='hidden' name='delivery_city' id='delivery_city' runat='server' />");
            sbTable.Append("<input type='hidden' name='delivery_state' id='delivery_state' runat='server' />");
            sbTable.Append("<input type='hidden' name='delivery_zip' id='delivery_zip' runat='server' />");
            sbTable.Append("<input type='hidden' name='delivery_country' id='delivery_country' value='India' runat='server' />");
            sbTable.Append("<input type='hidden' name='delivery_tel' id='delivery_tel' runat='server' />");

            sbTable.Append("<input type='hidden' name='merchant_param1' id='merchant_param1' value='" + AppointmentDateTime.ToString().Replace(":", ".") + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='merchant_param2' id='merchant_param2' value='" + DealerAffaxtionCenterID.ToString() + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='merchant_param3' id='merchant_param3' value='" + DealerAffaxtionAddress.ToString() + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='merchant_param4' id='merchant_param4' value='" + Regno.ToString() + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='merchant_param5' id='merchant_param5' value='" + TimeSlotID.ToString() + "' runat='server'  />");
            sbTable.Append("<input type='hidden' name='promo_code' />");
            sbTable.Append("<input type='hidden' name='customer_identifier' id='customer_identifier' runat='server' />");

            //sbTable.Append("<input id='paynow' type='Submit' name='Submit' />");
            sbTable.Append("</div>");
            sbTable.Append("</form>");
            Literal1.Text = sbTable.ToString();

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "PostData()", true);

        }


        private void RazorPay(string orderno, string GrandTotal, string OwnerName, string Address,
            string CityName, string StateName, string PinCode,
            string MobileNo, string Emailid, string AppointmentDateTime, string DealerAffaxtionCenterID,
            string DealerAffaxtionAddress, string Regno, string TimeSlotID)
        {
            string Order_No = string.Empty;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            decimal payableAmount = Convert.ToDecimal(GrandTotal) * 100;

            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", payableAmount); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", orderno);
            input.Add("payment_capture", 1);
            //input.Add("name", "Dheerendra Singh");
            //input.Add("prefill","{ 'email':'dheerendra786@gmail.com','contact':'8882359687'}");

            //string key = "rzp_test_A8SlD8Ar6NexSY";  //rzp_test_Uy1r8Av2FjQdBP
            //string secret = "L657FHU7f3APTshth2yDhjgw";//iaRucYRkXy0nW2IvRrqPrMwj
            string key = ConfigurationManager.AppSettings["key"].ToString();
            string secret = ConfigurationManager.AppSettings["secret"].ToString();

            RazorpayClient client = new RazorpayClient(key, secret);

            try
            {
                Razorpay.Api.Order order = client.Order.Create(input);
                Order_No = order["id"].ToString();

                try
                {
                    string RazorPayOrderIDUpdate = "update [BookMyHSRP].dbo.Appointment_bookingHist set razorpay_order_id = '" + Order_No + "' where OrderNo ='" + orderno + "'  ";
                    int updateCout = BMHSRPv2.Models.Utils.ExecNonQuery(RazorPayOrderIDUpdate, CnnString);
                }
                catch (Exception ev)
                {

                }
            }
            catch (Exception ex)
            {
                Order_No = RandomString(10);
                Console.WriteLine(ex.Message);
            }


            string Host = ConfigurationManager.AppSettings["Host"].ToString();
            //string ccavResponseHandler = @"http://localhost:55098//plate/PaymentReceipt.aspx";
            string ccavResponseHandler = Host + "sticker/PaymentReceipt.aspx";

            StringBuilder sbTable = new StringBuilder();
            sbTable.Clear();
            sbTable.Append("<form id='customerData' name='customerData' action='" + ccavResponseHandler + "' method='post'>");
            sbTable.Append("<script ");
            sbTable.Append("src='https://checkout.razorpay.com/v1/checkout.js' ");
            sbTable.Append("data-key='" + key + "' ");
            sbTable.Append("data-amount='0' ");
            sbTable.Append("data-timeout=600");//in sec.
            sbTable.Append("data-name='Razorpay' ");
            sbTable.Append("data-description='BookMy HSRP' ");
            sbTable.Append("data-order_id='" + Order_No + "' ");
            sbTable.Append("data-image='https://razorpay.com/favicon.png' ");
            sbTable.Append("data-prefill.name='" + OwnerName + "' ");
            sbTable.Append("data-prefill.email='" + Emailid + "' ");
            sbTable.Append("data-prefill.contact='" + MobileNo + "' ");
            sbTable.Append("data-theme.color='#F37254' ");
            sbTable.Append("></script>");

            sbTable.Append("<input type='hidden' value='Hidden Element' name='hidden'>");
            sbTable.Append("<input type='hidden' value='" + orderno + "' name='generated_order_id'>");
            sbTable.Append("</form>");
            Literal1.Text = sbTable.ToString();
        }

        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}