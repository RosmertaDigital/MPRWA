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

namespace BMHSRPv2.plate
{
    public partial class VerifyDetailPayment : System.Web.UI.Page
    {

        string CnnString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetSideBar();
                if (Session["DeliveryPoint"].ToString()== "Home" || Session["DeliveryPoint"].ToString() == "rwa")
                {
                    trDeliveryCharge.Visible = true;
                }
                else
                {
                    trDeliveryCharge.Visible = false;
                }

                if (Session["Type"] == null)
                {
                    PaymentDescription();
                }
                else
                {
                    chkPassport.Visible = false;
                    txtMobileNo.Visible = false;
                    decimal GstBasic_Amt = Convert.ToDecimal(Session["_GstBasic_Amt"]);
                    decimal FittmentCharges = Convert.ToDecimal(Session["_FittmentCharges"]);
                    decimal BMHConvenienceCharges = Convert.ToDecimal(Session["_BMHConvenienceCharges"]);
                    decimal BMHHomeCharges = Convert.ToDecimal(Session["_BMHHomeCharges"]);
                    decimal GrossTotal = GstBasic_Amt + BMHConvenienceCharges + BMHHomeCharges; //Convert.ToDecimal(dtOemRate.Rows[0]["GrossTotal"]);
                    decimal _calculatedGst = GrossTotal * Convert.ToDecimal(0.09);

                    decimal CGSTAmount = _calculatedGst;
                    decimal SGSTAmount = _calculatedGst;
                    decimal TotalAmount = GstBasic_Amt + BMHConvenienceCharges + BMHHomeCharges + CGSTAmount + SGSTAmount;
                    Session["TotalAmt"] = TotalAmount;

                    lblBasicAmt.Text = "Rs. " + string.Format("{0:0.00}", GstBasic_Amt);
                    lblFitmentCharge.Text = "Rs. " + string.Format("{0:0.00}", FittmentCharges);
                    lblConvenienceFee.Text = "Rs. " + string.Format("{0:0.00}", BMHConvenienceCharges);
                    lblDeliveryCharge.Text = "Rs. " + string.Format("{0:0.00}", BMHHomeCharges);
                    //lblDeliveryCharge.Text = "0.00";
                    lblTotalBasicAmt.Text = "Rs. " + string.Format("{0:0.00}", GrossTotal);
                    decimal IGSTAmount = GrossTotal * Convert.ToDecimal("0.00");
                    //lblGST.Text = "Rs. " + string.Format("{0:0.00}", GSTAmount);
                    lblIGST.Text = "Rs. " + string.Format("{0:0.00}", IGSTAmount);
                    lblCGST.Text = "Rs. " + string.Format("{0:0.00}", CGSTAmount);
                    lblSGST.Text = "Rs. " + string.Format("{0:0.00}", SGSTAmount);
                    lblGrandTotal.Text = "Rs. " + string.Format("{0:0.00}", (GrossTotal + CGSTAmount + SGSTAmount));

                    if (CGSTAmount == 0)
                    {
                        divtrCGST.Visible = false;
                    }

                    if (SGSTAmount == 0)
                    {
                        divtrSGST.Visible = false;
                    }
                }

            }
        }

        private void PaymentDescription()
        {
            string CheckOemRateQuery = "CheckOrdersRates '" + Session["OEMId"].ToString() + "', 'OB','" + Session["VehicleClass"].ToString() + "', '" + Session["VehicleType"].ToString() + "','" + Session["Vehiclecategoryid"].ToString() + "','" + Session["VehicleFuelType"].ToString() + "','" + Session["DeliveryPoint"].ToString() + "','" + Session["StateId"].ToString() + "','" + Session["SessionState"].ToString() + "'"; 

            DataTable dtOemRate = BMHSRPv2.Models.Utils.GetDataTable(CheckOemRateQuery, CnnString);
            if (dtOemRate.Rows.Count > 0)
            {
                string status = dtOemRate.Rows[0]["status"].ToString();

                #region
                if (status == "1")
                {
                    #region

                    string FrontPlateSize = dtOemRate.Rows[0]["FrontPlateSize"].ToString();
                    string RearPlateSize = dtOemRate.Rows[0]["RearPlateSize"].ToString();

                    decimal GstBasic_Amt = Convert.ToDecimal(dtOemRate.Rows[0]["GstBasic_Amt"]) + Convert.ToDecimal(dtOemRate.Rows[0]["FittmentCharges"]);
                    decimal FittmentCharges = Convert.ToDecimal(dtOemRate.Rows[0]["FittmentCharges"]);
                    decimal BMHConvenienceCharges = Convert.ToDecimal(dtOemRate.Rows[0]["BMHConvenienceCharges"]);
                    decimal BMHHomeCharges = Convert.ToDecimal(dtOemRate.Rows[0]["BMHHomeCharges"]);
                    //decimal GrossTotal = GstBasic_Amt; //Convert.ToDecimal(dtOemRate.Rows[0]["GrossTotal"]);
                    //decimal GrossTotal = GstBasic_Amt + BMHHomeCharges + BMHConvenienceCharges; //Convert.ToDecimal(dtOemRate.Rows[0]["GrossTotal"]);
                    //decimal GSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["GSTAmount"]);
                    //decimal IGSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["IGSTAmount"]);
                    //decimal CGSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["CGSTAmount"]);
                    //decimal SGSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["SGSTAmount"]);
                    //  decimal GstRate = Convert.ToDecimal(dtOemRate.Rows[0]["gst"]);
                    // decimal TotalAmount = Convert.ToDecimal(dtOemRate.Rows[0]["TotalAmount"]);
                    decimal GrossTotal = GstBasic_Amt + BMHConvenienceCharges + BMHHomeCharges; //Convert.ToDecimal(dtOemRate.Rows[0]["GrossTotal"]);
                    decimal _calculatedGst = GrossTotal * Convert.ToDecimal(0.09);
                   

                    decimal CGSTAmount = _calculatedGst;
                    decimal SGSTAmount = _calculatedGst;
                    //decimal TotalAmount = GstBasic_Amt + CGSTAmount + SGSTAmount;
                    //decimal TotalAmount = GstBasic_Amt + BMHHomeCharges + BMHConvenienceCharges + CGSTAmount + SGSTAmount;
                    decimal TotalAmount = GstBasic_Amt + BMHConvenienceCharges + BMHHomeCharges + CGSTAmount + SGSTAmount;

                    lblBasicAmt.Text = "Rs. " + string.Format("{0:0.00}", GstBasic_Amt);
                    lblFitmentCharge.Text = "Rs. " + string.Format("{0:0.00}", FittmentCharges);
                    lblConvenienceFee.Text = "Rs. " + string.Format("{0:0.00}", BMHConvenienceCharges);
                    lblDeliveryCharge.Text = "Rs. " + string.Format("{0:0.00}", BMHHomeCharges);
                    //lblDeliveryCharge.Text = "0.00";
                    lblTotalBasicAmt.Text = "Rs. " + string.Format("{0:0.00}", GrossTotal);
                    decimal IGSTAmount = GrossTotal * Convert.ToDecimal("0.00");
                    //lblGST.Text = "Rs. " + string.Format("{0:0.00}", GSTAmount);
                    lblIGST.Text = "Rs. " + string.Format("{0:0.00}", IGSTAmount);
                    lblCGST.Text = "Rs. " + string.Format("{0:0.00}", CGSTAmount);
                    lblSGST.Text = "Rs. " + string.Format("{0:0.00}", SGSTAmount);
                    lblGrandTotal.Text = "Rs. " + string.Format("{0:0.00}", (GrossTotal + CGSTAmount + SGSTAmount));

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
                lblOwnerName.Text = Session["SessionOwnerName"].ToString();
                lblMobile.Text = Session["SessionMobileNo"].ToString();
                lblAddress.Text = Session["SessionBillingAddress"].ToString();
                lblEmailID.Text = Session["SessionEmailID"].ToString();
                lblPinCode.Text = "";
            }
            catch(Exception ev)
            {

            }
        }

        private void SetSideBar()
        {
            LiteralBookingTypeImage.Text = "<img src='" + Session["OrderType_imgPath"].ToString() + "' draggable='false'>";
            LiteralVehicleTypeImage.Text = "<img src='" + Session["VehicleType_imgPath"].ToString() + "' draggable='false'>";
            LiteralOemImage.Text = "<img src='" + Session["OEMImgPath"].ToString() + "' draggable='false'>";
            LiteralState.Text = "<p><span>" + Session["StateShortName"].ToString() + "</span>" + Session["StateName"].ToString() + "</p>";
            LiteralVehicleClassImage.Text = "<img src='" + Session["VehicleClass_imgPath"].ToString() + "' draggable='false'>" +
                                                           "<p> " + Session["VehicleClass"].ToString() + " Vehicle </p>";
            LiteralFuelType.Text = "<p><span>" + Session["VehicleFuelType"].ToString() + "</span></p>";

            try
            {
                string CheckDealerAffixationQuery = "select a.OemID, c.Name OemName, a.DealerID, a.StateID, a.RTOLocationID,b.RTOLocationName,a.DealerAffixationCenterName, a.DealerAffixationCenterAddress  " +
              "from [HSRPOEM].dbo.DealerAffixationCenter a, [HSRPOEM].dbo.rtolocation b, [HSRPOEM].dbo.OEMMaster c  " +
              "where a.rtolocationid = b.RTOLocationID and a.OemID = c.OEMID " +
              " and DealerAffixationID = '" + Session["DealerAffixationCenterid"].ToString() + "' ";
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

            if (ViewState["OrderNos"] != null)
            {
                string orderNos = ViewState["OrderNos"].ToString();

                if (!string.IsNullOrEmpty(orderNos))
                {
                    string[] vs = orderNos.Split(',');
                    if (vs.Length > 0)
                    {
                        foreach (var v in vs)
                        {

                            try
                            {
                                var order_status = "Failed";
                                var failure_message = "User started new transaction.";
                                var payment_gateway_type = "razorpay";
                                string paymentFailedQuery = "PaymentConfirmation '" + v + "','','','" + order_status + "','" + failure_message + "','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','" + payment_gateway_type + "'";
                                DataTable dtPaymentConfirmation = BMHSRPv2.Models.Utils.GetDataTable(paymentFailedQuery, CnnString);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                                //need to add in logger
                            }

                        }
                    }
                }
                ViewState.Remove("OrderNos");
            }

            //bypassing this in case of RWA

            if (HttpContext.Current.Session["DeliveryPoint"].ToString() !="rwa")
            {

                string AppointmentBlockedDatesQuery = "CheckECAppointmentBlockedDates '" + Session["SelectedSlotDate"].ToString() + "', '" + Session["DealerAffixationCenterid"].ToString() + "','" + Session["DeliveryPoint"].ToString() + "' ";
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
            if (Session["Type"] != null)
            {
                #region Properties
                string OrderType = "OB";
                string SlotId = string.Empty;
                string SlotTime = string.Empty;
                string SlotBookingDate = string.Empty;
                string HSRP_StateID = string.Empty;
                string RTOLocationID = string.Empty;
                string RTOName = string.Empty;
                string OwnerName = string.Empty;
                string OwnerFatherName = string.Empty;
                string Address1 = string.Empty;
                string State = string.Empty;
                string City = string.Empty;
                string Pin = string.Empty;
                string MobileNo = string.Empty;
                string LandlineNo = string.Empty;
                string EmailID = string.Empty;
                string VehicleClass = string.Empty;
                string VehicleType = string.Empty;
                string ManufacturerName = string.Empty;
                //string ManufacturerModel = "";
                string ChassisNo = string.Empty;
                string EngineNo = string.Empty;
                string VehicleRegNo = string.Empty;
                string ManufacturingYear = string.Empty;
                string FrontPlateSize = string.Empty;
                string RearPlateSize = string.Empty;
                string TotalAmount = string.Empty;
                string NetAmount = string.Empty;
                string BookingType = string.Empty;
                string Booking_classType = string.Empty;
                string fuelType = string.Empty;
                string Dealerid = string.Empty;
                string OEMID = string.Empty;
                string BookedFrom = string.Empty;
                string AppointmentType = string.Empty;
                string BasicAamount = string.Empty;
                string FitmentCharge = string.Empty;
                string ConvenienceFee = string.Empty;
                string HomeDeliveryCharge = string.Empty;
                string GSTAmount = string.Empty;
                string IGSTAmount = string.Empty;
                string CGSTAmount = string.Empty;
                string SGSTAmount = string.Empty;
                string CustomerGSTNo = string.Empty;
                string VehicleRCImage = string.Empty;
                string BharatStage = string.Empty;
                string ShippingAddress1 = string.Empty;
                string ShippingAddress2 = string.Empty;
                string ShippingCity = string.Empty;
                string ShippingState = string.Empty;
                string ShippingPinCode = string.Empty;
                string ShippingLandMark = string.Empty;
                string DealerAffixationAddress = string.Empty;
                string NonHomologVehicle = string.Empty;
                string BillingAddress = string.Empty;

               string Qstr = "select distinct CONVERT(VARCHAR(20), cast(blockDate as date), 120) blockDate from [HSRPOEM].[dbo].[HolidayDateTime] " +
                "where cast(blockDate as date) between getdate() and cast(DATEADD(DAY, +6, GETDATE()) as date) and ([Desc] = 'Holiday' or [Desc] = 'Sunday') ";
                DataTable dtHoliday = BMHSRPv2.Models.Utils.GetDataTable(Qstr, CnnString);

                //HttpContext.Current.Session["SelectedSlotID"] = "1";// SlotID;
                //HttpContext.Current.Session["SelectedSlotDate"] = DateTime.Today.AddDays(6 + dtHoliday.Rows.Count).ToString("yyyy-MM-dd"); // DateTime.Now.Date.ToString("yyyy-MM-dd");//   SlotDate;
                //HttpContext.Current.Session["SelectedSlotTime"] = "10:00AM to 06:00PM";// SlotTime;
                //HttpContext.Current.Session["DealerAffixationCenterAdd"] = 11;//dtCheckPincode.Rows[0]["DealerAffixationCenterAddress"].ToString();

                SlotId= "1";
                //SlotBookingDate = DateTime.Today.AddDays(6 + dtHoliday.Rows.Count).ToString("yyyy-MM-dd"); // DateTime.Now.Date.ToString("yyyy-MM-dd");//   SlotDate;
                //SlotBookingDate = DateTime.Today.AddDays(dtHoliday.Rows.Count).ToString("yyyy-MM-dd"); // DateTime.Now.Date.ToString("yyyy-MM-dd");//   SlotDate;
                SlotBookingDate = DateTime.Now.ToString("yyyy-MM-dd"); // DateTime.Now.Date.ToString("yyyy-MM-dd");//   SlotDate;
                SlotTime = HttpContext.Current.Session["SelectedSlotTime"].ToString();

                string CheckDealerAffixationQuery = "select a.OemID, c.Name OemName, a.DealerID, a.StateID, a.RTOLocationID,b.RTOLocationName  " +
                 "from [HSRPOEM].dbo.DealerAffixationCenter a, [HSRPOEM].dbo.rtolocation b, [HSRPOEM].dbo.OEMMaster c  " +
                 "where a.rtolocationid = b.RTOLocationID and a.OemID = c.OEMID " +
                 " and DealerAffixationID = '" + Session["DealerAffixationCenterid"].ToString() + "' ";
                DataTable dtDealerAffixation = BMHSRPv2.Models.Utils.GetDataTable(CheckDealerAffixationQuery, CnnString);
                if (dtDealerAffixation.Rows.Count > 0)
                {
                    RTOLocationID = dtDealerAffixation.Rows[0]["RTOLocationID"].ToString();
                    RTOName = dtDealerAffixation.Rows[0]["RTOLocationName"].ToString();
                    Dealerid = dtDealerAffixation.Rows[0]["DealerID"].ToString();
                    //OEMID = dtDealerAffixation.Rows[0]["OemID"].ToString();
                    //ManufacturerName = dtDealerAffixation.Rows[0]["OemName"].ToString();
                }

                #endregion

                //if (dtTemp.Rows.Count > 0)
                //{
                //    for (int j = 0; j < dtTemp.Rows.Count; j++)
                //    {
                //        string strInsert = "update [bookmyhsrp].dbo.Appointment_BookingHist set orderstatus='Failed' where orderno='" + dtTemp.Rows[j]["OrderNo"] + "'";
                //        BMHSRPv2.Models.Utils.ExecNonQuery(strInsert, CnnString);
                //    }
                //    dtTemp.Rows.Clear();
                //}

                DataTable dtTemp1 = new DataTable();
                string OrderNo = string.Empty;
                string str = "usp_Appointment_BookingHistStagingGet '" + Session["GroupOrderNo"] + "'";
                DataTable dtStaging = BMHSRPv2.Models.Utils.GetDataTable(str, CnnString);
                if(dtStaging.Rows.Count>0)
                {
                    for (int i=0; i<dtStaging.Rows.Count; i++)
                    {
                        OrderNo = "BMHSRP" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + GetRandomNumber();

                        //if (ClickCount > 1)
                        //{
                        //    if (dtTemp.Rows.Count > 0)
                        //    {
                        //        for (int j = 0; j < dtTemp.Rows.Count; j++)
                        //        {
                        //            string strInsert = "update [bookmyhsrp].dbo.Appointment_BookingHist set orderstatus='Failed' where orderno='" + dtTemp.Rows[j]["OrderNo"] + "'";
                        //            //BMHSRPv2.Utils.ExecNonQuery(strInsert, ConnectionString);
                        //            BMHSRPv2.Models.Utils.ExecNonQuery(strInsert, CnnString);
                        //        }
                        //        dtTemp.Rows.Clear();
                        //    }
                        //}

                        

                        VehicleRegNo = dtStaging.Rows[i]["VehicleRegNo"].ToString();
                        ChassisNo = dtStaging.Rows[i]["ChassisNo"].ToString();
                        EngineNo = dtStaging.Rows[i]["EngineNo"].ToString();
                        VehicleClass = dtStaging.Rows[i]["VehicleClass"].ToString();
                        VehicleType = dtStaging.Rows[i]["VehicleType"].ToString();
                        BharatStage = dtStaging.Rows[i]["BharatStage"].ToString();
                        OEMID= dtStaging.Rows[i]["OemId"].ToString();
                        BookingType = dtStaging.Rows[i]["VehicleCat"].ToString();
                        AppointmentType = dtStaging.Rows[i]["AppointmentType"].ToString();
                        HSRP_StateID = dtStaging.Rows[i]["StateId"].ToString();
                        State = dtStaging.Rows[i]["StateName"].ToString();
                        BasicAamount = dtStaging.Rows[i]["GstBasic_Amt"].ToString();
                        FitmentCharge = dtStaging.Rows[i]["FittmentCharges"].ToString();
                        ConvenienceFee = dtStaging.Rows[i]["BMHConvenienceCharges"].ToString();
                        HomeDeliveryCharge = dtStaging.Rows[i]["BMHHomeCharges"].ToString();
                        TotalAmount = dtStaging.Rows[i]["GrossTotal"].ToString();
                        GSTAmount = dtStaging.Rows[i]["GSTAmount"].ToString();
                        IGSTAmount = dtStaging.Rows[i]["IGSTAmount"].ToString();
                        CGSTAmount = dtStaging.Rows[i]["CGSTAmount"].ToString();
                        SGSTAmount = dtStaging.Rows[i]["SGSTAmount"].ToString();
                        NetAmount = dtStaging.Rows[i]["TotalAmount"].ToString();
                        Address1 = dtStaging.Rows[i]["BillingAddress"].ToString();
                        MobileNo = dtStaging.Rows[i]["MobileNo"].ToString();
                        EmailID = dtStaging.Rows[i]["EmailId"].ToString();
                        AppointmentType = "rwa";
                        LandlineNo= dtStaging.Rows[i]["MobileNo"].ToString();
                        ManufacturerName = dtStaging.Rows[i]["ManufactureName"].ToString();
                        ManufacturingYear = dtStaging.Rows[i]["ManufactureYear"].ToString();
                        FrontPlateSize = dtStaging.Rows[i]["FrontPlateSize"].ToString();
                        RearPlateSize = dtStaging.Rows[i]["RearPlateSize"].ToString();
                        Booking_classType= dtStaging.Rows[i]["VehicleClass"].ToString();
                        fuelType= dtStaging.Rows[i]["FuelType"].ToString();
                        NonHomologVehicle = "N";
                        BookedFrom = "Website";
                        City = Session["DeliveryCity"].ToString();
                        Dealerid = Session["DealerAffixationCenterid"].ToString();
                        OwnerName = dtStaging.Rows[i]["OwnerName"].ToString();
                        string PaymentInitiatedQuery = "PaymentInitiated '" + Session["DealerAffixationCenterid"].ToString() + "', '" + OrderNo + "','" + OrderType + "','" + SlotId + "','" + SlotTime + "','" + SlotBookingDate + "','" + HSRP_StateID + "','" + RTOLocationID + "','" + RTOName + "','" + OwnerName + "','" + OwnerFatherName + "','" + Address1 + "','" + State + "','" + City + "','" + Pin + "','" + MobileNo + "','" + LandlineNo + "','" + EmailID + "','" + VehicleClass + "','" + VehicleType + "','" + ManufacturerName + "','" + ChassisNo + "','" + EngineNo + "','" + ManufacturingYear + "','" + VehicleRegNo + "','" + FrontPlateSize + "','" + RearPlateSize + "','" + TotalAmount + "','" + NetAmount + "','" + BookingType + "','" + Booking_classType + "','" + fuelType + "','" + Dealerid + "','" + OEMID + "','" + BookedFrom + "','" + AppointmentType + "','" + BasicAamount + "','" + FitmentCharge + "','" + ConvenienceFee + "','" + HomeDeliveryCharge + "','" + GSTAmount + "','" + CustomerGSTNo + "','" + VehicleRCImage + "','" + BharatStage + "','" + ShippingAddress1 + "','" + ShippingAddress2 + "','" + ShippingCity + "','" + ShippingState + "','" + ShippingPinCode + "','" + ShippingLandMark + "','" + IGSTAmount + "','" + CGSTAmount + "','" + SGSTAmount + "','','','', '" + NonHomologVehicle + "'";

                        DataTable dtPaymentConfirmation = BMHSRPv2.Models.Utils.GetDataTable(PaymentInitiatedQuery, CnnString);
                        if (dtPaymentConfirmation.Rows.Count > 0)
                        {
                            string Status = dtPaymentConfirmation.Rows[0]["status"].ToString();
                            OrderNo = dtPaymentConfirmation.Rows[0]["OrderNo"].ToString();
                            if (Status == "1")
                            {
                                if (dtTemp1.Columns.Count == 0)
                                {
                                    dtTemp1.Columns.Add("OrderNo", typeof(string));
                                }
                                dtTemp1.Rows.Add(OrderNo);
                            }
                        }
                        else
                        {
                            lblMessage.Text = dtPaymentConfirmation.Rows[0]["message"].ToString();
                            lblMessage.Visible = true;
                        }



                    }
                    ViewState["OrderNos"] = string.Join(",", dtTemp1.AsEnumerable()
                        .Select(row => row.Field<string>("orderNo")));

                }
                //ClickCount = 0;

                string _NetAmount = Session["TotalAmt"].ToString();
                string _GroupOrderNo = Session["GroupOrderNo"].ToString();
                //RazorPay(OrderNo, _NetAmount, OwnerName, Address1, City, State, Pin, MobileNo, EmailID, (SlotBookingDate + " " + SlotTime), Session["DealerAffixationCenterid"].ToString(), DealerAffixationAddress, VehicleRegNo, SlotId);
                RazorPay(_GroupOrderNo, _NetAmount, OwnerName, Address1, City, State, Pin, MobileNo, EmailID, (SlotBookingDate + " " + SlotTime), Session["DealerAffixationCenterid"].ToString(), DealerAffixationAddress, VehicleRegNo, SlotId);
                StringBuilder RazorPayScript = new StringBuilder();
                RazorPayScript.Append("<script language='javascript'>");
                RazorPayScript.Append("ValidatePayForm();");
                RazorPayScript.Append("</script>");
                this.Page.Controls.Add(new LiteralControl(RazorPayScript.ToString()));


            }
            else
            {
                if (txtMobileNo.Text.ToString().Length != 10)
                {
                    lblMessage.Text = "Please fill mobile number before placing the order.";
                    lblMessage.Visible = true;
                    return;
                }

                /*
                    OrderNo,OrderType,OrderStatus,SlotId,SlotTime,SlotBookingDate,HSRP_StateID,RTOLocationID,RTOName,
                 *  HSRPRecord_CreationDate,OwnerName,OwnerFatherName,
                    Address1,State,City,Pin,MobileNo,EmailID,VehicleClass,VehicleType,ManufacturerName,ManufacturerModel,ChassisNo,EngineNo,
                    VehicleRegNo,TotalAmount,NetAmount,BookingType,Booking_classType,fuelType,Dealerid,OEMID,BookedFrom,AppointmentType
                 */

                //string DealerAffixationCenterid = Session["DealerAffixationCenterid"].ToString();

                string OrderType = "OB";
                //string OrderStatus = "Initiated";
                string SlotId = Session["SelectedSlotID"].ToString();
                string SlotTime = Session["SelectedSlotTime"].ToString();
                string SlotBookingDate = Session["SelectedSlotDate"].ToString(); //StateId
                string HSRP_StateID = Session["StateId"].ToString();
                string RTOLocationID = string.Empty;
                string RTOName = string.Empty;
                string OwnerName = Session["SessionOwnerName"].ToString();
                string OwnerFatherName = "";
                string Address1 = Session["SessionBillingAddress"].ToString();
                string State = Session["SessionState"].ToString();
                string City = Session["SessionCity"].ToString();
                string Pin = "";
                string MobileNo = Session["SessionMobileNo"].ToString();
                string LandlineNo = txtMobileNo.Text.ToString();
                string EmailID = Session["SessionEmailID"].ToString();
                string VehicleClass = Session["VehicleClass"].ToString();
                string VehicleType = Session["VehicleType"].ToString();
                string ManufacturerName = "";
                //string ManufacturerModel = "";
                string ChassisNo = Session["SessionChasisno"].ToString();
                string EngineNo = Session["SessionEngno"].ToString();
                string VehicleRegNo = Session["SessionRegNo"].ToString();
                string ManufacturingYear = Session["SessionRegDate"].ToString();
                string FrontPlateSize = "";
                string RearPlateSize = "";
                string TotalAmount = "";
                string NetAmount = "";
                string BookingType = Session["VehicleCat"].ToString();//4W, 2W
                string Booking_classType = VehicleClass;
                string fuelType = Session["VehicleFuelType"].ToString();
                string Dealerid = "";
                string OEMID = Session["Oemid"].ToString();
                string BookedFrom = "Website";
                string AppointmentType = Session["DeliveryPoint"].ToString();
                string BasicAamount = "";
                string FitmentCharge = "";
                string ConvenienceFee = "";
                string HomeDeliveryCharge = "";
                string GSTAmount = "";
                string IGSTAmount = "";
                string CGSTAmount = "";
                string SGSTAmount = "";
                string CustomerGSTNo = Session["SessionGST"] != null && Session["SessionGST"].ToString().Length > 0 ? Session["SessionGST"].ToString() : "";
                string VehicleRCImage = Session["SessionFilePath"] != null && Session["SessionFilePath"].ToString().Length > 0 ? Session["SessionFilePath"].ToString() : "";
                string BharatStage = Session["SessionBharatStage"].ToString();
                string ShippingAddress1 = "";
                string ShippingAddress2 = "";
                string ShippingCity = "";
                string ShippingState = "";
                string ShippingPinCode = "";
                string ShippingLandMark = "";
                string DealerAffixationAddress = "";
                string NonHomologVehicle = HttpContext.Current.Session["NonHomo"].ToString();
                //DealerAffixationCenterid = "367";

                if (Session["DeliveryPoint"].ToString() == "Home")
                {
                    if (Session["DeliveryAddress1"] != null && Session["DeliveryAddress1"].ToString().Length > 0)
                    {
                        ShippingAddress1 = Session["DeliveryAddress1"].ToString();
                        ShippingAddress2 = Session["DeliveryAddress2"].ToString();
                        ShippingCity = Session["Deliverycity"].ToString();
                        ShippingState = Session["DeliveryState"].ToString();
                        ShippingPinCode = Session["DeliveryPincode"].ToString();
                        ShippingLandMark = Session["DeliveryLandmark"] == null ? "" : Session["DeliveryLandmark"].ToString();
                    }
                    else if (Session["mapAddress1"] != null && Session["mapAddress1"].ToString().Length > 0)
                    {
                        ShippingAddress1 = Session["mapAddress1"].ToString();
                        ShippingAddress2 = Session["mapAddress2"].ToString();
                        ShippingCity = Session["mapCity"].ToString();
                        ShippingState = Session["mapStates"].ToString();
                        ShippingPinCode = Session["mapPincode"].ToString();
                        ShippingLandMark = Session["mapLandmark"] == null ? "" : Session["mapLandmark"].ToString();
                    }
                }

                try
                {
                    string CheckDealerAffixationQuery = "select a.OemID, c.Name OemName, a.DealerID, a.StateID, a.RTOLocationID,b.RTOLocationName  " +
                 "from [HSRPOEM].dbo.DealerAffixationCenter a, [HSRPOEM].dbo.rtolocation b, [HSRPOEM].dbo.OEMMaster c  " +
                 "where a.rtolocationid = b.RTOLocationID and a.OemID = c.OEMID " +
                 " and DealerAffixationID = '" + Session["DealerAffixationCenterid"].ToString() + "' ";
                    DataTable dtDealerAffixation = BMHSRPv2.Models.Utils.GetDataTable(CheckDealerAffixationQuery, CnnString);
                    if (dtDealerAffixation.Rows.Count > 0)
                    {
                        RTOLocationID = dtDealerAffixation.Rows[0]["RTOLocationID"].ToString();
                        RTOName = dtDealerAffixation.Rows[0]["RTOLocationName"].ToString();
                        Dealerid = dtDealerAffixation.Rows[0]["DealerID"].ToString();
                        //OEMID = dtDealerAffixation.Rows[0]["OemID"].ToString();
                        //ManufacturerName = dtDealerAffixation.Rows[0]["OemName"].ToString();
                    }

                    string CheckOEMQuery = "select oemid, name OemName from [HSRPOEM].dbo.OEMMaster where oemid = '" + Session["OEMId"].ToString() + "'";
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
                    string CheckOemRateQuery = "CheckOrdersRates '" + Session["OEMId"].ToString() + "', 'OB','" + Session["VehicleClass"].ToString() + "', '" + Session["VehicleType"].ToString() + "','" + Session["Vehiclecategoryid"].ToString() + "','" + Session["VehicleFuelType"].ToString() + "','" + Session["DeliveryPoint"].ToString() + "','" + Session["StateId"].ToString() + "','" + Session["SessionState"].ToString() + "'";

                    DataTable dtOemRate = BMHSRPv2.Models.Utils.GetDataTable(CheckOemRateQuery, CnnString);
                    if (dtOemRate.Rows.Count > 0)
                    {
                        string status = dtOemRate.Rows[0]["status"].ToString();
                        #region
                        if (status == "1")
                        {
                            decimal GstBasic_Amt = Convert.ToDecimal(dtOemRate.Rows[0]["GstBasic_Amt"]) + Convert.ToDecimal(dtOemRate.Rows[0]["FittmentCharges"]);

                            FrontPlateSize = dtOemRate.Rows[0]["FrontPlateSize"].ToString();
                            RearPlateSize = dtOemRate.Rows[0]["RearPlateSize"].ToString();
                            BasicAamount = GstBasic_Amt.ToString(); //dtOemRate.Rows[0]["GstBasic_Amt"].ToString();
                            FitmentCharge = dtOemRate.Rows[0]["FittmentCharges"].ToString();
                            ConvenienceFee = dtOemRate.Rows[0]["BMHConvenienceCharges"].ToString(); //"0.00";
                            HomeDeliveryCharge = dtOemRate.Rows[0]["BMHHomeCharges"].ToString();

                            //decimal _calculatedGst = Convert.ToDecimal(BasicAamount) * Convert.ToDecimal(0.09);
                            decimal _calculatedGst = (Convert.ToDecimal(BasicAamount) + Convert.ToDecimal(ConvenienceFee) + Convert.ToDecimal(HomeDeliveryCharge)) * Convert.ToDecimal(0.09);

                            decimal _CGSTAmount = _calculatedGst;
                            decimal _SGSTAmount = _calculatedGst;
                            decimal _TotalAmount = Convert.ToDecimal(BasicAamount) + Convert.ToDecimal(ConvenienceFee) + Convert.ToDecimal(HomeDeliveryCharge) + _CGSTAmount + _SGSTAmount;

                            TotalAmount = (Convert.ToDecimal(BasicAamount) + Convert.ToDecimal(ConvenienceFee) + Convert.ToDecimal(HomeDeliveryCharge)).ToString();  //dtOemRate.Rows[0]["GrossTotal"].ToString();
                            GSTAmount = GSTAmount = (_CGSTAmount + _SGSTAmount).ToString(); //dtOemRate.Rows[0]["GSTAmount"].ToString();
                            IGSTAmount = "0.00";//_calculatedGst.ToString();// dtOemRate.Rows[0]["IGSTAmount"].ToString();
                            CGSTAmount = _CGSTAmount.ToString(); //dtOemRate.Rows[0]["CGSTAmount"].ToString();
                            SGSTAmount = _SGSTAmount.ToString();// dtOemRate.Rows[0]["SGSTAmount"].ToString();
                                                                // = dtOemRate.Rows[0]["gst"].ToString();
                            NetAmount = (_TotalAmount).ToString("0.##"); //dtOemRate.Rows[0]["TotalAmount"].ToString();
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

                    FitmentCharge = "0";

                    string PaymentInitiatedQuery = "PaymentInitiated '" + Session["DealerAffixationCenterid"].ToString() + "', '" + OrderNo + "','" + OrderType + "','" + SlotId + "','" + SlotTime + "','" + SlotBookingDate + "','" + HSRP_StateID + "','" + RTOLocationID + "','" + RTOName + "','" + OwnerName + "','" + OwnerFatherName + "','" + Address1 + "','" + State + "','" + City + "','" + Pin + "','" + MobileNo + "','" + LandlineNo + "','" + EmailID + "','" + VehicleClass + "','" + VehicleType + "','" + ManufacturerName + "','" + ChassisNo + "','" + EngineNo + "','" + ManufacturingYear + "','" + VehicleRegNo + "','" + FrontPlateSize + "','" + RearPlateSize + "','" + TotalAmount + "','" + NetAmount + "','" + BookingType + "','" + Booking_classType + "','" + fuelType + "','" + Dealerid + "','" + OEMID + "','" + BookedFrom + "','" + AppointmentType + "','" + BasicAamount + "','" + FitmentCharge + "','" + ConvenienceFee + "','" + HomeDeliveryCharge + "','" + GSTAmount + "','" + CustomerGSTNo + "','" + VehicleRCImage + "','" + BharatStage + "','" + ShippingAddress1 + "','" + ShippingAddress2 + "','" + ShippingCity + "','" + ShippingState + "','" + ShippingPinCode + "','" + ShippingLandMark + "','" + IGSTAmount + "','" + CGSTAmount + "','" + SGSTAmount + "','','','', '" + NonHomologVehicle + "'";

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
                            RazorPay(OrderNo, NetAmount, OwnerName, Address1, City, State, Pin, MobileNo, EmailID, (SlotBookingDate + " " + SlotTime), Session["DealerAffixationCenterid"].ToString(), DealerAffixationAddress, VehicleRegNo, SlotId);
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
            string ccavResponseHandler = Host + "plate/PaymentReceipt.aspx";

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

            decimal payableAmount = Decimal.Round(Convert.ToDecimal(GrandTotal),2) * 100;

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
            string ccavResponseHandler = Host + "plate/PaymentReceipt.aspx";

            StringBuilder sbTable = new StringBuilder();
            sbTable.Clear();
            sbTable.Append("<form id='customerData' name='customerData' action='" + ccavResponseHandler + "' method='post'>");
            sbTable.Append("<script ");
            sbTable.Append("src='https://checkout.razorpay.com/v1/checkout.js' ");
            sbTable.Append("data-key='" + key + "' ");
            sbTable.Append("data-amount='0' ");
            sbTable.Append("data-timeout=600");//in sec.
            sbTable.Append("data-name='Razorpay' ");
            sbTable.Append("data-description='BookMyHSRP' ");
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