using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2
{
    public partial class ReAppointmentSlot : System.Web.UI.Page
    {
        #region Private Variables
        protected DataTable dtCategories;
        #endregion

        //static string VehicleTypeID = string.Empty;
        //static string AffaxtionCenterID = string.Empty;
        //static string DeliveryType = string.Empty; // Home, Dealer
        //static string OemID = string.Empty; // for OEM Check Appointment From Date


        /****
         * Start Remove on live
         */

        /****
         * End Remove on live
         */

        static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

          
            if (!IsPostBack)
            {

                //VehicleTypeID = "2";
                //AffaxtionCenterID = "367";
                //DeliveryType = "Dealer"; // Home, Dealer
             
                //if ( Session["Re_OrderType"] != null && Session["Re_OrderType"].ToString().Length > 0 &&
                //     Session["Re_VehicleTypeID"] != null && Session["Re_VehicleTypeID"].ToString().Length > 0)
                //{

                //}
                //else
                //{
                //    Response.Redirect("Index.aspx");
                //}

                LoadDataTable();

                //btnTimeSlotSelection.Visible = false;
            }

        }

       

        #region Private Methods
        private void LoadDataTable()
        {
            /*****
             * Checking Holidays
             */ 
            string Qstr = string.Empty;
            //Qstr = "select distinct CONVERT(VARCHAR(20), cast(blockDate as date), 120) blockDate from [dbo].[_HolidayDateTime] where cast(blockDate as date) >= getdate() order by CONVERT(VARCHAR(20), cast(blockDate as date), 120)";
            
            Qstr = "select ''''+STRING_AGG(blockDate, ''',''')+''''   blockDate from ( "+
            "select distinct CONVERT(VARCHAR(20), cast(blockDate as date), 120) blockDate from [HSRPOEM].[dbo].[HolidayDateTime] " +
            "where cast(blockDate as date) between getdate() and cast(DATEADD(MONTH, +5, GETDATE()) as date) )t";
            DataTable dtHoliday = BMHSRPv2.Models.Utils.GetDataTable(Qstr, ConnectionString);
            if (dtHoliday.Rows.Count > 0)
            {
                string jsArrayHolidaysDates = dtHoliday.Rows[0]["blockDate"].ToString();
                this.ClientScript.RegisterArrayDeclaration("jsArrayHolidaysDates", jsArrayHolidaysDates);
            }

            /*****
             * Booking Start after 10 days from today.
             */ 
            //int noOfDays = 11;
            string SQLAppointmentFromDate = string.Empty;
            //SQLAppointmentFromDate = "select DateDiff(ww, getdate(), DATEADD(DAY, " + noOfDays + ", getdate())) as NumOfSundays";
            //SQLAppointmentFromDate = "select case when DateDiff(ww, getdate(), DATEADD(DAY, " + noOfDays + ", getdate())) > 0 " +
            //  "then CONVERT(VARCHAR(20), cast(DATEADD(DAY, " + noOfDays + "+(DateDiff(ww, getdate(), DATEADD(DAY, " + noOfDays + ", getdate()))), getdate()) as date), 120) " +
            //  "else CONVERT(VARCHAR(20), cast(DATEADD(DAY, " + noOfDays + ", getdate()) as date), 120)  end as FromDate";

            SQLAppointmentFromDate = "CheckAppointmentFromDate '" + Session["Re_OEMId"].ToString() + "', '" + Session["Re_DealerAffixationCenterid"].ToString() + "','" + Session["Re_VehicleTypeid"].ToString() + "','" + Session["Re_DeliveryPoint"].ToString() + "' ,'" + Session["Re_StateId"].ToString() + "','" + HttpContext.Current.Session["plateSticker"].ToString() + "' ";
            DataTable dtAppointmentFromDate = BMHSRPv2.Models.Utils.GetDataTable(SQLAppointmentFromDate, ConnectionString);
            if (dtAppointmentFromDate.Rows.Count > 0)
            {
                string jsArraySlotFromDate = "'" + dtAppointmentFromDate.Rows[0]["FromDate"].ToString() + "','" + dtAppointmentFromDate.Rows[0]["EndDate"].ToString() + "'";
                this.ClientScript.RegisterArrayDeclaration("jsArrayAvaiableSlotFromDates", jsArraySlotFromDate);
            }
            
        }
        #endregion


        [WebMethod, ScriptMethod]
        public static List<string> CheckECBlockedDates(string checkDate, string avaiableSlotFrom)
        {
            List<string> blockedDates = new List<string>();
            CultureInfo provider = CultureInfo.InvariantCulture; 
            try
            {
                DateTime checkDateTime = DateTime.ParseExact(checkDate, "yyyy-MM-dd", provider);
                DateTime avaiableSlotFromDateTime = DateTime.ParseExact(avaiableSlotFrom, "yyyy-MM-dd", provider);
                DateTime startDate = new DateTime(checkDateTime.Year, checkDateTime.Month, 1);
                if (checkDateTime.Month == avaiableSlotFromDateTime.Month)
                {
                    startDate = avaiableSlotFromDateTime;
                }
                else if (checkDateTime.Month < avaiableSlotFromDateTime.Month)
                {
                    return blockedDates;
                }

                DateTime endDate = new DateTime(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
                

                DateTime tempDate = startDate;
                while (tempDate <= endDate)
                {
                    string AppointmentBlockedDatesQuery = string.Empty;
                    //HolidayQuery = "select holidayid from _HolidayDateTime where cast(blockDate as date) = cast('" + e.Day.Date.ToShortDateString() + "' as date)";

                    AppointmentBlockedDatesQuery = "CheckAppointmentBlockedDates_EcDealer '" + checkDate + "', '" + HttpContext.Current.Session["Re_DealerAffixationCenterid"].ToString()+ "','" + HttpContext.Current.Session["Re_DeliveryPoint"].ToString()+ "','" + HttpContext.Current.Session["plateSticker"].ToString() + "' ";
                    System.Data.DataTable dtAppointmentBlockedDates = BMHSRPv2.Models.Utils.GetDataTable(AppointmentBlockedDatesQuery, ConnectionString);

                    if (dtAppointmentBlockedDates.Rows.Count > 0)
                    {
                        string Status = dtAppointmentBlockedDates.Rows[0]["status"].ToString();
                        if (Status == "1")
                        {
                            blockedDates.Add(tempDate.ToString("yyyy-MM-dd"));
                        }
                    }
                    tempDate = tempDate.AddDays(1);
                }
               
                return blockedDates;
            }
            catch (Exception ex)
            {
                return blockedDates;
            }
        }


        [WebMethod, ScriptMethod]
        public static string BindingTimeSlot(string SelectedDate)
        {
            string responseHtml = string.Empty;
            CultureInfo provider = CultureInfo.InvariantCulture;
            try
            {
                DateTime SelectedDateTime = DateTime.ParseExact(SelectedDate, "yyyy-MM-dd", provider);
                string DayOfWeekTemp =  SelectedDateTime.DayOfWeek.ToString();

                string AppointmentBlockedDatesQuery = "CheckECAppointmentBlockedDates '" + SelectedDate + "', '" + HttpContext.Current.Session["Re_DealerAffixationCenterid"].ToString() + "','" + HttpContext.Current.Session["Re_DeliveryPoint"].ToString() + "','" + HttpContext.Current.Session["plateSticker"].ToString() + "' ";
                System.Data.DataTable dtAppointmentBlockedDates = BMHSRPv2.Models.Utils.GetDataTable(AppointmentBlockedDatesQuery, ConnectionString);

                if (dtAppointmentBlockedDates.Rows.Count > 0)
                {
                    string Status = dtAppointmentBlockedDates.Rows[0]["status"].ToString();
                    if (Status == "0" && SelectedDateTime.DayOfWeek != DayOfWeek.Sunday)
                    {
                        string sqlQuery = string.Empty;
                        StringBuilder sbTable = new StringBuilder();
                        
                        //sqlQuery = "EXEC [dbo].[jsp_BHSRP_GET_TIME_SLOT] @VehicleTypeID=" + HttpContext.Current.Session["Re_VehicleTypeid"].ToString() + ",@RTOCodeID=" + HttpContext.Current.Session["Re_DealerAffixationCenterid"].ToString() + "";
                        //DataTable dtTimeSlot = BMHSRPv2.Models.Utils.GetDataTable(sqlQuery, ConnectionString);
                        //sqlQuery = "CheckApointmentTimeSlot '" + SelectedDate + "','" + HttpContext.Current.Session["Re_VehicleTypeid"].ToString() + "','" + HttpContext.Current.Session["Re_DealerAffixationCenterid"].ToString() + "','" + HttpContext.Current.Session["Re_DeliveryPoint"].ToString() + "','" + HttpContext.Current.Session["Re_StateId"].ToString() + "' "; //Session["Re_StateId"].ToString()
                        if (HttpContext.Current.Session["Re_DeliveryPoint"].ToString() == "Dealer")
                        {
                            sqlQuery = "CheckApointmentTimeSlot '" + SelectedDate + "','" + HttpContext.Current.Session["Re_VehicleTypeid"].ToString() + "','" + HttpContext.Current.Session["Re_DealerAffixationCenterid"].ToString() + "','" + HttpContext.Current.Session["Re_DeliveryPoint"].ToString() + "','','" + HttpContext.Current.Session["plateSticker"].ToString() + "' "; //Session["Re_StateId"].ToString()
                        }
                        else if (HttpContext.Current.Session["Re_DeliveryPoint"].ToString() == "Home")
                        {
                            sqlQuery = "CheckApointmentTimeSlot_Home '" + SelectedDate + "','" + HttpContext.Current.Session["Re_VehicleTypeid"].ToString() + "','" + HttpContext.Current.Session["Re_DealerAffixationCenterid"].ToString() + "','" + HttpContext.Current.Session["Re_DeliveryPoint"].ToString() + "','' ,'" + HttpContext.Current.Session["plateSticker"].ToString() + "' "; //Session["Re_StateId"].ToString()
                        }
                        
                        DataTable dtTimeSlot = BMHSRPv2.Models.Utils.GetDataTable(sqlQuery, ConnectionString);

                        if (dtTimeSlot.Rows.Count > 0)
                        {
                            #region
                            sbTable.Append("<table>");
                            int aind = 0;
                            foreach (DataRow dr in dtTimeSlot.Rows)
                            {
                                if (HttpContext.Current.Session["Re_DeliveryPoint"].ToString() == "Home")
                                {
                                    if (aind == 0)
                                    {
                                        string SlotIDWithTimeSlot = dr["TimeSlotID"].ToString() + "`" + "09:00 AM-01:00 PM";
                                        sbTable.Append("<tr>");
                                        sbTable.Append("<td><p>09:00 AM-01:00 PM</p></td>");
                                        if (dr["AvaiableStatus"].ToString() == "N")
                                        {
                                            sbTable.Append("<td><a href='#' class='disable'>Not Available</a></td>");
                                        }
                                        else
                                        {
                                            sbTable.Append("<td><a href='#' onclick='TimeSlotSelection(&#39;" + SlotIDWithTimeSlot + "&#39;);'>Available</a></td>");
                                        }
                                    }
                                    if (aind == 1)
                                    {
                                        string SlotIDWithTimeSlot = dr["TimeSlotID"].ToString() + "`" + "02:00 AM-06:00 PM";
                                        sbTable.Append("<tr>");
                                        sbTable.Append("<td><p>02:00 PM-06:00 PM</p></td>");
                                        if (dr["AvaiableStatus"].ToString() == "N")
                                        {
                                            sbTable.Append("<td><a href='#' class='disable'>Not Available</a></td>");
                                        }
                                        else
                                        {
                                            sbTable.Append("<td><a href='#' onclick='TimeSlotSelection(&#39;" + SlotIDWithTimeSlot + "&#39;);'>Available</a></td>");
                                        }
                                    }

                                    aind++;
                                }
                                else
                                {
                                    string SlotIDWithTimeSlot = dr["TimeSlotID"].ToString() + "`" + dr["SlotName"].ToString();
                                    sbTable.Append("<tr>");
                                    sbTable.Append("<td><p>" + dr["SlotName"].ToString() + "</p></td>");
                                    if (dr["AvaiableStatus"].ToString() == "N")
                                    {
                                        sbTable.Append("<td><a href='#' class='disable'>Not Available</a></td>");
                                    }
                                    else
                                    {
                                        sbTable.Append("<td><a href='#' onclick='TimeSlotSelection(&#39;" + SlotIDWithTimeSlot + "&#39;);'>Available</a></td>");
                                    }

                                }

                                sbTable.Append("</tr>");
                            }
                            sbTable.Append("</table>");
                            #endregion
                        }
                        responseHtml = sbTable.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                responseHtml = ex.Message;
            }

            return responseHtml;
        }


        protected void btnTimeSlotSelection_Click(object sender, EventArgs e)
        {
            
            string SlotDate = txtHideSelectedDate.Text.ToString();
            string SlotIDWithTimeSlot = txtHideSelectedTimeSlot.Text.ToString();
            string SlotID = string.Empty;
            string SlotTime = string.Empty;

            if (SlotDate.Length == 0)
            {
                LiteralMessage.Text = "<Alert> Please select Appointment date. </Alert>";
                return;
            }
            if (SlotIDWithTimeSlot.Length == 0)
            {
                LiteralMessage.Text = "<Alert> Please select Appointment Time Slot </Alert>";
                return;
            }

            if (SlotIDWithTimeSlot != null && SlotIDWithTimeSlot.Length > 0)
            {
                string[] stringSeparators = new string[] { "`" };
                string[] SlotIDWithTimeSlotArr = SlotIDWithTimeSlot.Split(stringSeparators, StringSplitOptions.None);

                if (SlotIDWithTimeSlotArr.Length == 2)
                {
                    SlotID = SlotIDWithTimeSlotArr[0];
                    SlotTime = SlotIDWithTimeSlotArr[1];
                }
            }

            //if (SelectedSlotDate == null || SelectedSlotDate.Length == 0)
            //{
            //    LiteralMessage.Text = "<Alert> Choose Appointment Date </Alert>";
            //    return;
            //}

            //if (SelectedSlotTime == null || SelectedSlotTime.Length == 0)
            //{
            //    LiteralMessage.Text = "<Alert> Choose Appointment Time </Alert>";
            //    return;
            //}

            //string ECBlockQuery = "CheckECBlockedDates '" + SelectedSlotDate + "', '" + AffaxtionCenterID + "' ";
            string AppointmentBlockedDatesQuery = "CheckECAppointmentBlockedDates '" + SlotDate + "', '" +Session["Re_DealerAffixationCenterid"].ToString() + "','" + Session["Re_DeliveryPoint"].ToString()+ "','" + HttpContext.Current.Session["plateSticker"].ToString() + "' ";
            System.Data.DataTable dtAppointmentBlockedDates = BMHSRPv2.Models.Utils.GetDataTable(AppointmentBlockedDatesQuery, ConnectionString);

            if (dtAppointmentBlockedDates.Rows.Count > 0)
            {
                //Response.Redirect("BookingSummary.aspx");
                string Status = dtAppointmentBlockedDates.Rows[0]["status"].ToString();
                if (Status == "1")
                {

                    LiteralMessage.Text = "<Alert> All Slots are Booked. Please select another date. </Alert>";
                    return;
                    
                }
                else
                {
                    Session["Re_SelectedSlotID"] = SlotID;
                    Session["Re_SelectedSlotDate"] = SlotDate;
                    Session["Re_SelectedSlotTime"] = SlotTime;

                    //BookingDetail
                    Response.Redirect("ReAppointmentPay.aspx");
                    //LiteralMessage.Text = "<Alert> Successfully Accepted " + SelectedSlotDate + " on " + SelectedSlotTime + " </Alert>";
                    return;
                }

                
            }
            else
            {
                LiteralMessage.Text = "<Alert> All Slots are Booked. Please select another date.  </Alert>";
                return;
            }

        }

    }

}