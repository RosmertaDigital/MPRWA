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
    public partial class ReceiptValidityDetail : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            string vehicle = Request.QueryString["REG_NUMBER"].ToString();
            //string ExpireQry = " SELECT case when DATEDIFF(day, HSRPRecord_CreationDate, Getdate())<= 15" +
            //                 " then 'NotExpire' else 'Expire' end ExpireDetail,VehicleRegNo from Appointment_BookingHist " +
            //                 " where VehicleRegNo = '" + vehicle.Trim() + "' and OrderStatus='Success' ";

            //string ExpireQry= " SELECT DATEDIFF(day, SlotBookingDate, Getdate())" +
            //                 " daycount,DATEADD(dd, 15, SlotBookingDate) as 'ExpireDate'," +
            //                 " VehicleRegNo from Appointment_BookingHist" +
            //                 " where VehicleRegNo = '" + vehicle.Trim() + "' and OrderStatus='Success' ";

            string ExpireQry = " SELECT DATEDIFF(day, SlotBookingDate, Getdate())" +
                             " daycount,Replace(Convert(varchar,DATEADD(dd, 15, SlotBookingDate),106),' ','-') as 'ExpireDate'," +
                             " VehicleRegNo from Appointment_BookingHist" +
                             " where VehicleRegNo = '" + vehicle.Trim() + "' and OrderStatus='Success' ";

            DataTable dt = Utils.GetDataTable(ExpireQry, constr);
            if(dt.Rows.Count>0)
            {
                int d = Convert.ToInt32( dt.Rows[0]["daycount"].ToString());
                string date = dt.Rows[0]["ExpireDate"].ToString();
                string VehicleNo = dt.Rows[0]["VehicleRegNo"].ToString();

                if (d <= 15)
                {
                    lblorderstatus.InnerText = "The Validity of your receipt is " + date + " of Vehicle No  " + VehicleNo;
                }
                else
                {
                    lblorderstatus.InnerText = "Record Not Found";
                }
                //if (d<15)
                //{
                //    lblorderstatus.InnerText = " your expire date of " + VehicleNo + " " + date; 
                //}
                //else
                //{

                //}

            }
            else
            {
                lblorderstatus.InnerText= "Record Not Found";
            }
            

        }
       
    }
}