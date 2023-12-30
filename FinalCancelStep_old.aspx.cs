using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;
using BMHSRPv2.Models;
//using iTextSharp.xmp.impl;

namespace BMHSRPv2
{
    public partial class FinalCancelStep_old : System.Web.UI.Page
    {
        string Qstr = string.Empty;
        string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //string OemConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
         CheckOrder();
        }

        protected void BtnConfirm_OnClick123()
        {
            #region
            //string Check = CheckOrder();
            //if (Check == "success")
            //{
            //    string HsrpOrderStatus = string.Empty;
            //    //Check  OrderStatus in HSRPOEM if OrderStatus="New Order" then insert and delete
            //    string selectStatus = "Select top 1 isnull(OrderStatus,'') OrderStatus,OrderNo FROM HSRPOEM.dbo.hsrprecords  WHERE  OrderNo='" + lblorderno.InnerText.Trim() + "' and IsBookMyHsrpRecord ='Y' ";
            //    DataTable _status = Utils.GetDataTable(selectStatus, constr);
            //    if (_status.Rows.Count > 0)
            //    {
            //        HsrpOrderStatus = _status.Rows[0]["OrderStatus"].ToString();
            //        //string _order = _status.Rows[0]["OrderNo"].ToString();

            //        //if (_orderStatus.ToString() == "New Order" && _order != "")
            //        //{
            //        //    // Update in HSRPOEM.dbo.hsrprecords table if order no is exixts
            //        //    string UpdateHSRPRecord = "UPDATE HSRPOEM.dbo.hsrprecords SET OrderStatus= 'ORDER CANCELLED' WHERE OrderNo='" + lblorderno.InnerText.Trim() + "  and IsBookMyHsrpRecord ='Y' '";
            //        //    Utils.ExecNonQuery(UpdateHSRPRecord, constr);
            //        //}

            //    }

            //    if (HsrpOrderStatus == "New Order" || HsrpOrderStatus == "")
            //    {
            //        string Reason = ddlReason.Items[ddlReason.SelectedIndex].Text + " -- Remark: " + txtother.Value.ToString();
            //        try
            //        {
            //            string UpdateHSRPRecord = "UPDATE HSRPOEM.dbo.hsrprecords SET OrderStatus= 'ORDER CANCELLED' WHERE OrderNo='" + lblorderno.InnerText.Trim() + "  and IsBookMyHsrpRecord ='Y' '";
            //            Utils.ExecNonQuery(UpdateHSRPRecord, constr);

            //            string Query = "UPDATE Appointment_BookingHist SET OrderStatus='ORDER CANCELLED' WHERE OrderNo='" + lblorderno.InnerText.Trim() + "' and VehicleRegNo='" + lblVehicleNo.InnerText.Trim() + "'";
            //            Utils.ExecNonQuery(Query, constr);

            //            string checkBookApp = "select top 1 orderno from hsrpoem.dbo.BookMyHSRPAppointment WHERE OrderNo='" + lblorderno.InnerText.Trim() + "'";
            //            DataTable dtbookapp = Utils.GetDataTable(checkBookApp, constr);
            //            if (dtbookapp.Rows.Count > 0)
            //            {
            //                string updateBookApp = " UPDATE hsrpoem.dbo.BookMyHSRPAppointment SET OrderStatus = 'ORDER CANCELLED',Process = 'R',ProcessMessage = '" + Reason.ToString() + "' WHERE OrderNo='" + lblorderno.InnerText.Trim() + "'";
            //                Utils.ExecNonQuery(updateBookApp, constr);
            //            }

            //            string Smsqry = " select top 1 LandLineNo from Appointment_BookingHist WHERE OrderNo='" + lblorderno.InnerText.Trim() + "' and VehicleRegNo='" + lblVehicleNo.InnerText.Trim() + "'";
            //            DataTable dt = Utils.GetDataTable(Smsqry, constr);
            //            if (dt.Rows.Count > 0)
            //            {
            //                string qry = "Your order has been successfully cancelled, amount will be refunded with in 7 working days";
            //                Utils.SMSSend(dt.Rows[0]["LandLineNo"].ToString(), qry);
            //            }

            //            Response.Redirect("CancellationReceived.aspx");
            //        }
            //        catch (Exception ev)
            //        {

            //        }

            //    }
            //    else
            //    {

            //    }



                


                
            //    //------------------------------ Additional changes 31-10-2020 --------------------------------------------

            //    // insert in BHSRP_ORDER_CANCELLED table
            //    // string insertqry = " insert into dbo.BHSRP_ORDER_CANCELLED (BVINFO_ID,DealerAffixationID,OEMID,STATE_ID,REASON,ORDER_NUMBER,CREATEDON) " +
            //    //                   " values(@BVINFO_ID, @DealerAffixationID, @OEMID, @STATE_ID, @REASON, @ORDER_NUMBER, GETDATE())  ";

            //    // Update in BHSRP_VEHICLE_INFO table 
            //    /* Not used
            //    string infocheck = "Select top 1 * from BHSRP_VEHICLE_INFO WHERE OrderNo='" + lblorderno.InnerText.Trim() + "'";
            //    DataTable dtinfo = Utils.GetDataTable(infocheck, constr);
            //    if (dtinfo.Rows.Count > 0)
            //    {
            //        string infoupdate = "UPDATE BHSRP_VEHICLE_INFO SET ORDER_STATUS='ORDER CANCELLED' WHERE OrderNo='" + lblorderno.InnerText.Trim() + "'";
            //        Utils.ExecNonQuery(infoupdate, constr);
            //    }*/

            //    // Update in Appointment_HSRPRecord_API table
            //    /* Not used
            //    string checkapi = "select top 1 * from Appointment_HSRPRecord_API WHERE OrderNo='" + lblorderno.InnerText.Trim() + "'";
            //    DataTable dtbook = Utils.GetDataTable(checkapi, constr);
            //    if(dtbook.Rows.Count>0)
            //    {
            //        string apiupdate = "UPDATE Appointment_HSRPRecord_API SET OrderStatus='ORDER CANCELLED' WHERE OrderNo='" + lblorderno.InnerText.Trim() + "'";
            //        Utils.ExecNonQuery(apiupdate, constr);
            //    }*/

            //    // Update in hsrpoem.dbo.BookMyHSRPAppointment table if exixts            
            //    ///----string Reason = ddlReason.InnerText.ToString() + " Remark : " + txtother.Value.ToString();
            //    //string checkBookApp = "select top 1 orderno from hsrpoem.dbo.BookMyHSRPAppointment WHERE OrderNo='" + lblorderno.InnerText.Trim() + "'";
            //    //DataTable dtbookapp = Utils.GetDataTable(checkBookApp, constr);
            //    //if (dtbookapp.Rows.Count > 0)
            //    //{
            //    //    string updateBookApp = " UPDATE hsrpoem.dbo.BookMyHSRPAppointment SET OrderStatus = 'ORDER CANCELLED',Process = 'R',ProcessMessage = '" + Reason.ToString() + "' WHERE OrderNo='" + lblorderno.InnerText.Trim() + "'";
            //    //    Utils.ExecNonQuery(updateBookApp, constr);
            //    //}

            //    // Delete in HSRPOEM.dbo.hsrprecords table if order no is exixts
            //    /* Reworking
            //    string checkqry = "SELECT OrderNo from HSRPOEM.dbo.hsrprecords_concelled_orders WHERE OrderNo='" + lblorderno.InnerText.Trim() + "'";
            //    DataTable dtcheck = Utils.GetDataTable(checkqry, constr);
            //    if (dtcheck.Rows.Count > 0)
            //    {
            //        string deleteqry = " DELETE HSRPOEM.dbo.hsrprecords WHERE OrderNo='" + lblorderno.InnerText.Trim() + "'";
            //        Utils.ExecNonQuery(deleteqry, constr);
            //    }*/
                
            //    //--------------------------------------- end code---------------------------------------------


            //   // string Smsqry = " select top 1 LandLineNo from Appointment_BookingHist WHERE OrderNo='" + lblorderno.InnerText.Trim() + "' and VehicleRegNo='" + lblVehicleNo.InnerText.Trim() + "'";
            //   //DataTable dt= Utils.GetDataTable(Smsqry, constr);
            //   // if(dt.Rows.Count>0)
            //   // {
            //   //     string qry = "Your order has been successfully cancelled, amount will be refunded with in 7 working days";
            //   //     Utils.SMSSend(dt.Rows[0]["LandLineNo"].ToString(), qry);
            //   // }


            //   // Response.Redirect("CancellationReceived.aspx");

            //}
            #endregion

        }


        protected void BtnConfirm_OnClick(object Source, EventArgs e)
        {

            string Check = CheckOrder();
            if (Check == "success")
            {
                string HsrpOrderStatus = string.Empty;
                //Check  OrderStatus in HSRPOEM if OrderStatus="New Order" then insert and delete
                string selectStatus = "Select top 1 isnull(OrderStatus,'') OrderStatus,OrderNo FROM HSRPOEM.dbo.hsrprecords  WHERE  OrderNo='" + lblorderno.InnerText.Trim() + "' and IsBookMyHsrpRecord ='Y' ";
                DataTable _status = Utils.GetDataTable(selectStatus, constr);
                if (_status.Rows.Count > 0)
                {
                    HsrpOrderStatus = _status.Rows[0]["OrderStatus"].ToString();
                }

                if (HsrpOrderStatus == "New Order" || HsrpOrderStatus == "")
                {
                    string Reason = ddlReason.Items[ddlReason.SelectedIndex].Text + " -- Remark: " + txtother.Value.ToString();
                    try
                    {
                        string UpdateHSRPRecord = "UPDATE HSRPOEM.dbo.hsrprecords SET OrderStatus= 'ORDER CANCELLED' WHERE OrderNo='" + lblorderno.InnerText.Trim() + "'  and IsBookMyHsrpRecord ='Y' ";
                        Utils.ExecNonQuery(UpdateHSRPRecord, constr);

                        string Query = "UPDATE Appointment_BookingHist SET OrderStatus='ORDER CANCELLED' WHERE OrderNo='" + lblorderno.InnerText.Trim() + "' and VehicleRegNo='" + lblVehicleNo.InnerText.Trim() + "'";
                        Utils.ExecNonQuery(Query, constr);

                        string checkBookApp = "select top 1 orderno from hsrpoem.dbo.BookMyHSRPAppointment WHERE OrderNo='" + lblorderno.InnerText.Trim() + "'";
                        DataTable dtbookapp = Utils.GetDataTable(checkBookApp, constr);
                        if (dtbookapp.Rows.Count > 0)
                        {
                            string updateBookApp = " UPDATE hsrpoem.dbo.BookMyHSRPAppointment SET OrderStatus = 'ORDER CANCELLED',Process = 'R',ProcessMessage = '" + Reason.ToString() + "' WHERE OrderNo='" + lblorderno.InnerText.Trim() + "'";
                            Utils.ExecNonQuery(updateBookApp, constr);
                        }

                        string Smsqry = " select top 1 LandLineNo from Appointment_BookingHist WHERE OrderNo='" + lblorderno.InnerText.Trim() + "' and VehicleRegNo='" + lblVehicleNo.InnerText.Trim() + "'";
                        DataTable dt = Utils.GetDataTable(Smsqry, constr);
                        if (dt.Rows.Count > 0)
                        {
                            string qry = "Your order has been successfully cancelled, amount will be refunded with in 7 working days";
                            Utils.SMSSend(dt.Rows[0]["LandLineNo"].ToString(), qry);
                        }

                        Response.Redirect("CancellationReceived.aspx");
                    }
                    catch (Exception ev)
                    {

                    }

                }
                else
                {
                    lblMsg.InnerText = "Your are not able to Cancel this order";
                    lblMsg.Visible = true;

                }
            }
        }

        private string CheckOrder()
        {

            lblorderno.InnerText = Session["CANCEL_ORDER_NO"].ToString();
            lblVehicleNo.InnerText = Session["REG_NUMBER"].ToString();

            if (string.IsNullOrEmpty(lblorderno.InnerText))
            {
                Response.Redirect("OrderCancel.aspx");
                
            }
            if (string.IsNullOrEmpty(lblVehicleNo.InnerText))
            {
                Response.Redirect("OrderCancel.aspx");
                
            }

           

           

            //Qstr = "  select top 1 HSRPRecord_CreationDate," +
            //       " case when getdate() Between HSRPRecord_CreationDate And DATEADD(HOUR, 24, HSRPRecord_CreationDate) then 'Y' else 'N' end isAbleToCancelled, " +
            //       "  OrderNo,OrderStatus,SlotTime,SlotBookingDate,EmailID,ChassisNo," +
            //       " EngineNo,VehicleRegNo,Dealerid,OrderStatus,VehicleClass,VehicleType,ManufacturerModel,fuelType,ManufacturerName from Appointment_BookingHist where OrderNo='" + lblorderno.InnerText.Trim() + "' and VehicleRegNo='" + lblVehicleNo.InnerText.Trim() + "' ";
            
            Qstr = "  select top 1 HSRPRecord_CreationDate," +
                  " case when getdate() Between HSRPRecord_CreationDate And DATEADD(HOUR, 24, HSRPRecord_CreationDate) then 'Y' else 'N' end isAbleToCancelled, " +
                  "  OrderNo,OrderStatus,SlotTime,SlotBookingDate,EmailID,ChassisNo,Replace(Convert(varchar,SlotBookingDate,106),' ','-') as SlotBookingDateNew," +
                  " EngineNo,VehicleRegNo,Dealerid,OrderStatus,VehicleClass,VehicleType,ManufacturerModel,fuelType,ManufacturerName,DealerID,AppointmentType,ShippingAddress1,ShippingAddress2,ShippingCity,ShippingState,ShippingPinCode from Appointment_BookingHist where OrderNo='" + lblorderno.InnerText.Trim() + "' and VehicleRegNo='" + lblVehicleNo.InnerText.Trim() + "' ";


            DataTable dt = Utils.GetDataTable(Qstr, constr);
            if (dt.Rows.Count > 0)
            {
                lblorderno.InnerText = dt.Rows[0]["OrderNo"].ToString();
                lblVehicleNo.InnerText = dt.Rows[0]["VehicleRegNo"].ToString();
                //lblAppointmentdate.InnerText = dt.Rows[0]["SlotBookingDate"].ToString();
                lblAppointmentdate.InnerText = dt.Rows[0]["SlotBookingDateNew"].ToString();
                lblSlot.InnerText = dt.Rows[0]["SlotTime"].ToString();
                lblChassisNo.InnerText = dt.Rows[0]["ChassisNo"].ToString();
                lblEngineNo.InnerText = dt.Rows[0]["EngineNo"].ToString();
                lblVehicleclass.InnerText = dt.Rows[0]["VehicleClass"].ToString();
                lblVehicletype.InnerText = dt.Rows[0]["VehicleType"].ToString();
                
                lblFuelType.InnerText= dt.Rows[0]["fuelType"].ToString();
                lblVehicleMake.InnerText = dt.Rows[0]["ManufacturerName"].ToString();

                string _AppointmentType = string.Empty;
                _AppointmentType = dt.Rows[0]["AppointmentType"].ToString();
                if (_AppointmentType == "Home")
                {
                    string FullAffress = dt.Rows[0]["ShippingAddress1"].ToString() + " " + dt.Rows[0]["ShippingAddress2"].ToString() + " " + dt.Rows[0]["ShippingCity"].ToString() + " " + dt.Rows[0]["ShippingState"].ToString() + " " + dt.Rows[0]["ShippingPinCode"].ToString();
                    lblVehiclemodel.InnerText = FullAffress.ToString();
                }
                else
                {
                    string _dealerId = dt.Rows[0]["DealerID"].ToString();
                    string qry2 = "select DealerAffixationCenterAddress,DealerAffixationCenterName,DealerAffixationCenterCity,DealerAffixationCenterPinCode from [HSRPOEM].dbo.DealerAffixationCenter where DealerID= '" + _dealerId.ToString() + "'";

                    DataTable _dt = Utils.GetDataTable(qry2, constr);
                    if (_dt.Rows.Count > 0)
                    {
                        //string dname= _dt.Rows[0]["DealerName"].ToString();
                        string dAddress = _dt.Rows[0]["DealerAffixationCenterName"].ToString();
                        string dCity = _dt.Rows[0]["DealerAffixationCenterAddress"].ToString();
                        string dPincode = _dt.Rows[0]["DealerAffixationCenterPinCode"].ToString();
                        lblVehiclemodel.InnerText = dAddress.ToString() + " " + dCity.ToString() + " " + dPincode.ToString();
                    }
                }

                return "success";
            }
            else
            {
                return "Fail";
            }
        }


    }
}