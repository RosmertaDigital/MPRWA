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

namespace BMHSRPv2.plate
{
    public partial class AppointmentSlot : System.Web.UI.Page
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

            if (!CheckSession.Checksession1(12, "plate"))
            {
                Response.Redirect("../Error.aspx");
            }
            if (!IsPostBack)
            {

                //VehicleTypeID = "2";
                //AffaxtionCenterID = "367";
                //DeliveryType = "Dealer"; // Home, Dealer
               SetSideBar();
                //if ( Session["OrderType"] != null && Session["OrderType"].ToString().Length > 0 &&
                //     Session["VehicleTypeID"] != null && Session["VehicleTypeID"].ToString().Length > 0)
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
                if (Session["DeliveryPoint"] != null && Session["DeliveryPoint"].ToString() == "Dealer")
                {
                    string CheckDealerAffixationQuery = "select a.OemID, c.Name OemName, a.DealerID, a.StateID, a.RTOLocationID,b.RTOLocationName,a.DealerAffixationCenterName, a.DealerAffixationCenterAddress  " +
             "from [HSRPOEM].dbo.DealerAffixationCenter a, [HSRPOEM].dbo.rtolocation b, [HSRPOEM].dbo.OEMMaster c  " +
             "where a.rtolocationid = b.RTOLocationID and a.OemID = c.OEMID " +
             " and DealerAffixationID = '" + Session["DealerAffixationCenterid"].ToString() + "' ";
                    DataTable dtDealerAffixation = BMHSRPv2.Models.Utils.GetDataTable(CheckDealerAffixationQuery, ConnectionString);
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
                else if (Session["DeliveryAddress1"] != null && Session["DeliveryAddress1"].ToString().Length > 0)
                {

                    AffixationAddress.Text = Session["DeliveryAddress1"].ToString() + " " + Session["DeliveryAddress2"].ToString() + " " + Session["Deliverycity"].ToString() + " " + Session["DeliveryState"].ToString();

                   
                }
                else if (Session["mapAddress1"] != null && Session["mapAddress1"].ToString().Length > 0)
                {
                    AffixationAddress.Text = Session["mapAddress1"].ToString();
                }



                }
            catch (Exception ex)
            {

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
            "where cast(blockDate as date) between getdate() and cast(DATEADD(MONTH, +5, GETDATE()) as date) and [Desc] = 'Holiday' )t";
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

            SQLAppointmentFromDate = "CheckAppointmentFromDate '" + Session["OEMId"].ToString() + "', '" + Session["DealerAffixationCenterid"].ToString() + "','" + Session["VehicleTypeid"].ToString() + "','" + Session["DeliveryPoint"].ToString() + "' ,'" + Session["StateId"].ToString() + "','Plate','"+Session["NonHomo"].ToString()+ "' ";
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
                   // AppointmentBlockedDatesQuery = "CheckECAppointmentBlockedDates '" + tempDate.ToString("yyyy-MM-dd") + "', '" + HttpContext.Current.Session["DealerAffixationCenterid"].ToString() + "','" + HttpContext.Current.Session["DeliveryPoint"].ToString() + "' ";
                    AppointmentBlockedDatesQuery = "CheckAppointmentBlockedDates_EcDealer '" + tempDate.ToString("yyyy-MM-dd") + "', '" + HttpContext.Current.Session["DealerAffixationCenterid"].ToString() + "','" + HttpContext.Current.Session["DeliveryPoint"].ToString() + "' ";
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

                string AppointmentBlockedDatesQuery = "CheckECAppointmentBlockedDates '" + SelectedDate + "', '" + HttpContext.Current.Session["DealerAffixationCenterid"].ToString() + "','" + HttpContext.Current.Session["DeliveryPoint"].ToString() + "' ";
                System.Data.DataTable dtAppointmentBlockedDates = BMHSRPv2.Models.Utils.GetDataTable(AppointmentBlockedDatesQuery, ConnectionString);

                if (dtAppointmentBlockedDates.Rows.Count > 0)
                {
                    string Status = dtAppointmentBlockedDates.Rows[0]["status"].ToString();

                    string sqlQuery = string.Empty;
                    StringBuilder sbTable = new StringBuilder();

                    if (Status == "0" && SelectedDateTime.DayOfWeek != DayOfWeek.Sunday && HttpContext.Current.Session["DeliveryPoint"].ToString() == "Dealer")
                    {
                        sqlQuery = "CheckApointmentTimeSlot'" + SelectedDate + "','" + HttpContext.Current.Session["VehicleTypeid"].ToString() + "','" + HttpContext.Current.Session["DealerAffixationCenterid"].ToString() + "','" + HttpContext.Current.Session["DeliveryPoint"].ToString() + "','" + HttpContext.Current.Session["StateId"].ToString() + "','Plate' "; //Session["S_StateId"].ToString()
                    }

                    if (Status == "0" && HttpContext.Current.Session["DeliveryPoint"].ToString() == "Home")
                    {
                        sqlQuery = "CheckApointmentTimeSlot_Home '" + SelectedDate + "','" + HttpContext.Current.Session["VehicleTypeid"].ToString() + "','" + HttpContext.Current.Session["DealerAffixationCenterid"].ToString() + "','" + HttpContext.Current.Session["DeliveryPoint"].ToString() + "','" + HttpContext.Current.Session["StateId"].ToString() + "','Plate' "; //Session["StateId"].ToString()
                    }

                    if (sqlQuery != null && sqlQuery.Length > 0)
                    {
                        
                        //sqlQuery = "EXEC [dbo].[jsp_BHSRP_GET_TIME_SLOT] @VehicleTypeID=" + HttpContext.Current.Session["VehicleTypeid"].ToString() + ",@RTOCodeID=" + HttpContext.Current.Session["DealerAffixationCenterid"].ToString() + "";
                        //DataTable dtTimeSlot = BMHSRPv2.Models.Utils.GetDataTable(sqlQuery, ConnectionString);
                        //sqlQuery = "CheckApointmentTimeSlot '" + SelectedDate + "','" + HttpContext.Current.Session["VehicleTypeid"].ToString() + "','" + HttpContext.Current.Session["DealerAffixationCenterid"].ToString() + "','" + HttpContext.Current.Session["DeliveryPoint"].ToString() + "','" + HttpContext.Current.Session["StateId"].ToString() + "' "; //Session["StateId"].ToString()
                        //if (HttpContext.Current.Session["DeliveryPoint"].ToString() == "Dealer")
                        //{
                        //    sqlQuery = "CheckApointmentTimeSlot '" + SelectedDate + "','" + HttpContext.Current.Session["VehicleTypeid"].ToString() + "','" + HttpContext.Current.Session["DealerAffixationCenterid"].ToString() + "','" + HttpContext.Current.Session["DeliveryPoint"].ToString() + "','" + HttpContext.Current.Session["StateId"].ToString() + "' "; //Session["StateId"].ToString()
                        //}
                        //else if (HttpContext.Current.Session["DeliveryPoint"].ToString() == "Home")
                        //{
                        //    sqlQuery = "CheckApointmentTimeSlot_Home '" + SelectedDate + "','" + HttpContext.Current.Session["VehicleTypeid"].ToString() + "','" + HttpContext.Current.Session["DealerAffixationCenterid"].ToString() + "','" + HttpContext.Current.Session["DeliveryPoint"].ToString() + "','" + HttpContext.Current.Session["StateId"].ToString() + "' "; //Session["StateId"].ToString()
                        //}
                        
                        DataTable dtTimeSlot = BMHSRPv2.Models.Utils.GetDataTable(sqlQuery, ConnectionString);

                        if (dtTimeSlot.Rows.Count > 0)
                        {
                            #region
                            sbTable.Append("<table>");
                            foreach (DataRow dr in dtTimeSlot.Rows)
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
            CheckSession.ClearSession(12, "plate");
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
            string AppointmentBlockedDatesQuery = "CheckECAppointmentBlockedDates '" + SlotDate + "', '" +Session["DealerAffixationCenterid"].ToString() + "','" + Session["DeliveryPoint"].ToString()+ "' ";
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
                    Session["SelectedSlotID"] = SlotID;
                    Session["SelectedSlotDate"] = SlotDate;
                    Session["SelectedSlotTime"] = SlotTime;

                    //BookingDetail
                    Response.Redirect("BookingSummary.aspx");
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