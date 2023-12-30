using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BMHSRPv2.Models;
//using iTextSharp.xmp.impl;

namespace BMHSRPv2
{
    public partial class OrderCancleConfirm : System.Web.UI.Page
    {
        string Qstr = string.Empty;
        string erpassigndate = string.Empty;
        string Status = string.Empty;
        string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string OemConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckOrder();
        }

        private void CheckOrder()
        {
            lblorderno.InnerText = Session["CANCEL_ORDER_NO"].ToString();
            lblVehicleNo.InnerText = Session["REG_NUMBER"].ToString();

            if (lblorderno.InnerText == "undefined" || string.IsNullOrEmpty(lblorderno.InnerText))
            {
                Response.Redirect("OrderCancel.aspx");
                return;
            }
            if (lblVehicleNo.InnerText == "undefined" || string.IsNullOrEmpty(lblVehicleNo.InnerText))
            {
                Response.Redirect("OrderCancel.aspx");
                return;
            }
            if (string.IsNullOrEmpty(Session["CANCEL_ORDER_NO"].ToString()))
            {
                return;
            }
            if (string.IsNullOrEmpty(Session["REG_NUMBER"].ToString()))
            {
                return;
            }

            

            //if (lblorderno.InnerText == "undefined" || string.IsNullOrEmpty(lblorderno.InnerText)) 
            //{
            //    Response.Redirect("OrderCancel.aspx");
            //    return;
            //}
            //if (lblVehicleNo.InnerText == "undefined" || string.IsNullOrEmpty(lblVehicleNo.InnerText))
            //{
            //    Response.Redirect("OrderCancel.aspx");
            //    return;
            //}


            //Qstr = "exec jsp_DEV_GET_ORDER_STATUS_Test '" + orderno.Trim() + "','" + vehNo.Trim() + "'"; //Test
            //Qstr = "  select top 1 HSRPRecord_CreationDate," +
            //       "  case when getdate() Between HSRPRecord_CreationDate And DATEADD(HOUR, 24, HSRPRecord_CreationDate) then 'Y' else 'N' end isAbleToCancelled, " +
            //       "  OrderNo,OrderStatus,SlotTime,convert(date,SlotBookingDate)as SlotBookingDate ,EmailID,ChassisNo," +
            //       " EngineNo,VehicleRegNo,Dealerid,OrderStatus from Appointment_BookingHist where OrderNo='" + lblorderno.InnerText + "' and VehicleRegNo='" + lblVehicleNo.InnerText + "' ";
            
            Qstr = "  select top 1 HSRPRecord_CreationDate," +
                  "  case when getdate() Between HSRPRecord_CreationDate And DATEADD(HOUR, 24, HSRPRecord_CreationDate) then 'Y' else 'N' end isAbleToCancelled, " +
                  "  OrderNo,OrderStatus,SlotTime,convert(date,SlotBookingDate)as SlotBookingDate ,EmailID,ChassisNo,Replace(Convert(varchar,SlotBookingDate,106),' ','-') as SlotBookingDateNew," +
                  " EngineNo,VehicleRegNo,Dealerid,OrderStatus,OwnerName,AppointmentType,ShippingAddress1,ShippingAddress2,ShippingCity,ShippingState,ShippingPinCode from Appointment_BookingHist where OrderNo='" + lblorderno.InnerText + "' and VehicleRegNo='" + lblVehicleNo.InnerText + "' ";

            DataTable dt = Utils.GetDataTable(Qstr, constr);

            if (dt.Rows.Count > 0)
            {

                string isAbleToCancelled = dt.Rows[0]["isAbleToCancelled"].ToString();
                if (isAbleToCancelled == "N")
                {
                    LiteralMessage.Text = "<Alert>Your order cannot be cancelled Now. </Alert>";

                    return;
                }
                else
                {
                    lblorderno.InnerText = dt.Rows[0]["OrderNo"].ToString();
                    lblVehicleNo.InnerText = dt.Rows[0]["VehicleRegNo"].ToString();
                    string chassisno = dt.Rows[0]["ChassisNo"].ToString();
                    string EngineNo = dt.Rows[0]["EngineNo"].ToString();
                    lblStatus.InnerText = dt.Rows[0]["OrderStatus"].ToString();
                    //lblAppointmentdate.InnerText = dt.Rows[0]["SlotBookingDate"].ToString();
                    lblAppointmentdate.InnerText = dt.Rows[0]["SlotBookingDateNew"].ToString();
                    string Dealerid = dt.Rows[0]["Dealerid"].ToString();
                    string _AppointmentType = string.Empty;
                    _AppointmentType = dt.Rows[0]["AppointmentType"].ToString();

                    if (_AppointmentType == "Home")
                    {
                        lblDealer.InnerText = dt.Rows[0]["OwnerName"].ToString();
                        //string FullAffress= dt.Rows[0]["OwnerName"].ToString()
                        string FullAffress = dt.Rows[0]["ShippingAddress1"].ToString() + " " + dt.Rows[0]["ShippingAddress2"].ToString() + " " + dt.Rows[0]["ShippingCity"].ToString() + " " + dt.Rows[0]["ShippingState"].ToString() + " " + dt.Rows[0]["ShippingPinCode"].ToString();
                        lblAddress.InnerText = FullAffress.ToString();
                    }
                    else
                    {
                        Qstr = " SELECT dealername , DealerAffixationCenterName ,DealerAffixationCenterAddress from" +
                              " DealerAffixationCenter where DealerID = '" + Dealerid + "'";
                        DataTable _dealer = Utils.GetDataTable(Qstr, OemConnectionString);
                        lblDealer.InnerText = _dealer.Rows[0]["DealerAffixationCenterName"].ToString();
                        lblAddress.InnerText = _dealer.Rows[0]["DealerAffixationCenterAddress"].ToString();

                    }

                   
                }
            }
            else
            {

                LiteralMessage.Text = "<Alert>Your order Find</Alert>";
            }
        }

        private void CheckIfNotFound()
        {
            if (LiteralMessage.Text == "Your order cannot be cancelled Now.")
            {
                Response.Redirect("OrderCancleConfirm.aspx");
                return;
            }
            else if (LiteralMessage.Text == "Your order Find")
            {
                Response.Redirect("OrderCancleConfirm.aspx");
                return;
            }
            else
            {

                // Server.Transfer("OrderCancelCaptchaCheck.aspx", true);
                // Response.Redirect(String.Format("~/OrderCancelCaptchaCheck.aspx?ORDER_NUMBER={0}&REG_NUMBER={1}", lblorderno.InnerText, lblVehicleNo.InnerText));
                //Response.Redirect("OrderCancelCaptchaCheck.aspx");
                Response.Redirect("FinalCancelStep.aspx");
            }

        }

        protected void btnConfirm_OnClick(object sender, EventArgs e)
        {
            CheckIfNotFound();

        }

    }
}