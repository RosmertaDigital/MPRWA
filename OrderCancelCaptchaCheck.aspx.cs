using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Data;
using BMHSRPv2.Models;
using System.Configuration;
using System.Web.Services;

namespace BMHSRPv2
{
    public partial class OrderCancelCaptchaCheck : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //string OemConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string orderno = string.Empty;
        string vehicleNo = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckOrder();
        }

        protected void btnConfirm_OnClick(object sender, EventArgs e)
        {
           

            //Response.Redirect(String.Format("~/FinalCancelStep.aspx?ORDER_NUMBER={0}&REG_NUMBER={1}", Server.UrlEncode(txtorder.Value), Server.UrlEncode(txtVeh.Value)));
           // Response.Redirect("~/FinalCancelStep.aspx");

        }


        private void CheckOrder()
        {

            txtorder.Value = Session["CANCEL_ORDER_NO"].ToString();
            txtVeh.Value = Session["REG_NUMBER"].ToString();


            if (string.IsNullOrEmpty(txtorder.Value))
            {
                Response.Redirect("OrderCancel.aspx");
                return;
            }
            if (string.IsNullOrEmpty(txtVeh.Value))
            {
                Response.Redirect("OrderCancel.aspx");
                return;
            }

            try
            {
                

                string Qstr = "  select top 1 HSRPRecord_CreationDate," +
                       " case when getdate() Between HSRPRecord_CreationDate And DATEADD(HOUR, 24, HSRPRecord_CreationDate) then 'Y' else 'N' end isAbleToCancelled, " +
                       "  OrderNo,OrderStatus,SlotTime,SlotBookingDate,EmailID,ChassisNo," +
                       " EngineNo,VehicleRegNo,Dealerid,OrderStatus from Appointment_BookingHist where OrderNo='" + txtorder.Value + "' and VehicleRegNo='" + txtVeh.Value + "' ";

                DataTable dt = Utils.GetDataTable(Qstr, constr);

                if (dt.Rows.Count > 0)
                {

                    string isAbleToCancelled = dt.Rows[0]["isAbleToCancelled"].ToString();
                    if (isAbleToCancelled == "N")
                    {
                        //LiteralMessage.Text = "<Alert> Order Number " + txtorderno.Value.ToString() + " Not successful Created </Alert>";
                        LiteralMessage.Text = "<Alert>Your order cannot be cancelled Now. </Alert>";


                    }
                }
                else
                {
                    LiteralMessage.Text = "<Alert>Your order cannot be cancelled Now. </Alert>";
                    Response.Redirect("OrderCancleConfirm.aspx");
                }
            }
            catch(Exception ex)
            {
                return;
            }

            }
            
        

    }
}