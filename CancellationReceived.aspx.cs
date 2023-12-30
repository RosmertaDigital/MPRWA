using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BMHSRPv2.Models;

namespace BMHSRPv2
{
    public partial class CancellationReceived : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //string OemConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            CheckOrder();

        }
        private void CheckOrder()
        {
            lblorderno.InnerText = Session["CANCEL_ORDER_NO"].ToString();
            lblVehicleNo.InnerText = Session["REG_NUMBER"].ToString();


            if (string.IsNullOrEmpty(lblorderno.InnerText))
            {
                Response.Redirect("OrderCancel.aspx");
                return;
            }
            if (string.IsNullOrEmpty(lblVehicleNo.InnerText))
            {
                Response.Redirect("OrderCancel.aspx");
                return;
            }
            // lblorderno.InnerText = Session["CancelOrderNo"].ToString(); // Session genrate in FinalCancelStep.aspx form
           

            {

                //Qstr = "exec jsp_DEV_GET_ORDER_STATUS_Test '" + orderno.Trim() + "','" + vehNo.Trim() + "'"; //Test
                string Qstr = "  select top 1 HSRPRecord_CreationDate," +
                        " case when getdate() Between HSRPRecord_CreationDate And DATEADD(HOUR, 24, HSRPRecord_CreationDate) then 'Y' else 'N' end isAbleToCancelled, " +
                        "  OrderNo,OrderStatus,SlotTime,SlotBookingDate,EmailID,ChassisNo," +
                        " EngineNo,VehicleRegNo,Dealerid,OrderStatus,VehicleClass,VehicleType,ManufacturerModel,fuelType,ManufacturerName from Appointment_BookingHist where OrderNo='" + lblorderno.InnerText.Trim() + "' and VehicleRegNo='" + lblVehicleNo.InnerText + "' ";
                DataTable dt = Utils.GetDataTable(Qstr, constr);
                if (dt.Rows.Count > 0)
                {
                    lblorderno.InnerText = dt.Rows[0]["OrderNo"].ToString();
                    lblVehicleNo.InnerText = dt.Rows[0]["VehicleRegNo"].ToString();
                    lblVehicleclass.InnerText = dt.Rows[0]["VehicleClass"].ToString();
                    lblFuelType.InnerText = dt.Rows[0]["fuelType"].ToString();
                    lblorderstatus.InnerText = dt.Rows[0]["OrderStatus"].ToString();
                }
                else
                {
                    Console.WriteLine("OrderNo Not Cancel");
                }
            }

            Console.WriteLine("OrderNo Not Cancel");
            //Response.Redirect();
        }
    }
}