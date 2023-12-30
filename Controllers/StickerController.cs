using BMHSRPv2.Models;
using BMHSRPv2.plate;
using BMHSRPv2.sticker;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using Org.BouncyCastle.Asn1.Crmf;
using System.Globalization;
using Newtonsoft.Json;

namespace BMHSRPv2.Controllers
{
    public class StickerController : ApiController
    {
        StringBuilder sbTable = new StringBuilder();
        String Msg = string.Empty;
        String ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        String DLConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DLConnectionString"].ToString();


        #region Booking Type (HSRP or Sticker Only) with Image path in session- Ankit Chaudhary use on page Index.aspx
        //[HttpGet]

        //[Route("api/Get/OrderType")]
        //public String OrderType(string OrderType, string OrderTypeImagePath)
        //{
        //    Msg = "";
        //    if (OrderType == "" || OrderType == null)
        //    {
        //        Msg = "Please Choose Booking Type";
        //        return Msg;
        //    }

        //    if (OrderTypeImagePath == "" || OrderTypeImagePath == null)
        //    {
        //        Msg = "Something went wrong when choosing booking type";
        //        return Msg;
        //    }
        //    HttpContext.Current.Session["OrderType_imgPath"] = OrderTypeImagePath;

        //    HttpContext.Current.Session["OrderType"] = OrderType;
        //    Msg = OrderType;
        //    return Msg;



        //}

        #endregion

        #region session[VehicleType_imgPath] Image path in session- Ankit Chaudhary use on page Vehiclemake.aspx
        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("sticker/api/Get/SetSessionVehicleTypeID")]
        public String SetSessionVehicleTypeID(string CategoryId, string VehicleCategoryImgPath)
        {
            //CheckSession.ClearSession(2, "plate");
            Msg = "";
            if (VehicleCategoryImgPath == "" || VehicleCategoryImgPath == null)
            {
                Msg = "Something went wrong when choosing Vehicle Category";
                return Msg;
            }

            HttpContext.Current.Session["S_CategoryId"] = CategoryId;

            HttpContext.Current.Session["S_VehicleType_imgPath"] = VehicleCategoryImgPath.Replace(".svg", "-w.svg");
            Msg = "Success";
            return Msg;

        }


        #endregion





        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("sticker/api/Get/SetOEM")]

        public String SetOEM(string OEMId, string OEMImgPath)
        {
            if (OEMId == "" || OEMId == null)
            {
                Msg = "Please Select Your Vehicle Make";
                return Msg;

            }
            else if (OEMImgPath == "" || OEMImgPath == null)
            {
                Msg = "Something went wrong when choosing Vehicle Make";
                return Msg;
            }

            HttpContext.Current.Session["S_OEMId"] = OEMId;

            HttpContext.Current.Session["S_OEMImgPath"] = OEMImgPath;
            Msg = "Success";
            return Msg;


        }



        #region Check Home Delivery by pincode needs to be updated sessions Ankit Chaudhary use on page delivery.aspx
        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("sticker/api/Get/CheckAvailability")]
        public ResCheCkAvailability CheckAvailability(string SessionValue)
        {
            ResCheCkAvailability ResAv = new ResCheCkAvailability();
            HttpContext.Current.Session["S_mapAddress1"] = "";
            //CheckSession.ClearSession(10, "plate");
            if (SessionValue.Trim() == "" || SessionValue == null)
            {
                ResAv.Status = "0";
                ResAv.DeliveryCity = "";
                ResAv.DeliveryState = "";
                ResAv.Msg = "Please Enter Delivery Pincode";

                return ResAv;
            }
            if (HttpContext.Current.Session["S_Oemid"] != null && HttpContext.Current.Session["S_StateId"] != null)
            {
                string Qstr = "Execute CheckHomeDeliveryAvailability '" + HttpContext.Current.Session["S_Oemid"].ToString() + "','" + HttpContext.Current.Session["S_StateId"].ToString() + "', '" + SessionValue + "'";
                DataTable dtCheckPincode = Utils.GetDataTable(Qstr, ConnectionString);

                if (dtCheckPincode.Rows.Count > 0)
                {
                    HttpContext.Current.Session["S_DealerAffixationCenterid"] = dtCheckPincode.Rows[0]["DealerAffixationID"].ToString();
                    ResAv.Status = "1";
                    ResAv.Msg = "Available";
                    ResAv.DeliveryCity = dtCheckPincode.Rows[0]["DealerAffixationCenterCity"].ToString();
                    ResAv.DeliveryState = dtCheckPincode.Rows[0]["hsrpstate"].ToString();
                    return ResAv;

                }
                else
                {

                    ResAv.Status = "0";
                    ResAv.Msg = "Not Available";
                    ResAv.DeliveryCity = "";
                    ResAv.DeliveryState = "";
                    return ResAv;

                }
            }
            else
            {

                ResAv.Status = "0";
                ResAv.Msg = "Session Expires..";
                ResAv.DeliveryCity = "";
                ResAv.DeliveryState = "";
                return ResAv;
            }




        }
        //public String CheckAvailability(string SessionValue)
        //{
        //    HttpContext.Current.Session["S_mapAddress1"] = "";
        //    CheckSession.ClearSession(10, "plate");
        //    if (SessionValue.Trim() == "" || SessionValue == null)
        //    {
        //        return "Please Enter Delivery Pincode";
        //    }
        //    if (HttpContext.Current.Session["S_Oemid"] != null && HttpContext.Current.Session["S_StateId"] != null)
        //    {
        //        string Qstr = "Execute CheckHomeDeliveryAvailability '" + HttpContext.Current.Session["S_Oemid"].ToString() + "','" + HttpContext.Current.Session["S_StateId"].ToString() + "', '" + SessionValue + "'";
        //        DataTable dtCheckPincode = Utils.GetDataTable(Qstr, ConnectionString);

        //        if (dtCheckPincode.Rows.Count > 0)
        //        {
        //            HttpContext.Current.Session["S_DealerAffixationCenterid"] = dtCheckPincode.Rows[0]["DealerAffixationID"].ToString();

        //            return "Available";
        //        }
        //        else
        //        {
        //            return "Not Available";
        //        }
        //    }
        //    else
        //    {
        //        return "Session Expires..";
        //    }
        //}



        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("sticker/api/Get/GetDeliveryPoint")]
        public String GetDeliveryPoint(string SessionValue)
        {
            try
            {
                CheckSession.Checksession1(8, "sticker");

                if (SessionValue == "" || SessionValue == null)
                {
                    Msg = "Failed";
                    return Msg;
                }


                if (SessionValue == "Home")
                {
                    if (HttpContext.Current.Session["S_StateId"].ToString() == "31")
                    {
                        Msg = "Coming Soon";
                        return Msg;
                    }

                }

                HttpContext.Current.Session["S_DeliveryPoint"] = SessionValue;
                Msg = SessionValue;

            }
            catch (Exception)
            {
                Msg = "Something went wrong please try after some time";
                return Msg;

            }
            return Msg;





        }


        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("sticker/api/Get/DeliveryInfo")]
        public String DeliveryInfo(string Pincode, string Address1, string Address2, string city, string State, string Landmark)
        {

            try
            {



                if (Pincode == "" || Pincode == null)
                {
                    Msg = "Please Enter Pincode";
                    return Msg;
                }

                else if (Address1 == "" || Address1 == null)
                {
                    Msg = "Please Enter Address1";
                    return Msg;
                }
                else if (Address2 == "" || Address2 == null)
                {
                    Msg = "Please Enter Address2";
                    return Msg;
                }
                else if (city == "" || city == null)
                {
                    Msg = "Please Enter City";
                    return Msg;
                }
                else if (State == "" || State == null)
                {
                    Msg = "Please Enter State";
                    return Msg;
                }

                #region Session
                HttpContext.Current.Session["S_DeliveryPincode"] = Pincode;

                HttpContext.Current.Session["S_DeliveryAddress1"] = Address1;

                HttpContext.Current.Session["S_DeliveryAddress2"] = Address2;


                HttpContext.Current.Session["S_Deliverycity"] = city;

                HttpContext.Current.Session["S_DeliveryState"] = State;

                HttpContext.Current.Session["S_DeliveryLandmark"] = Landmark;

                Msg = "Success";

                #endregion

            }
            catch (Exception ex)
            {
                Msg = "Something went wrong please try after some time";
                return Msg;
            }

            return Msg;
        }

        #endregion

        [System.Web.Http.HttpGet()]
        [Route("sticker/api/Get/GetVehicleClass")]
        public string GetVehicleClass(string VehicleClass, string VehicleClassImgPath)
        {
            if (VehicleClass == "" || VehicleClass == null)
            {
                Msg = "Please Select Vehicle Class";
                return Msg;

            }
            else if (VehicleClassImgPath == "" || VehicleClassImgPath == null)
            {
                Msg = "Something went wrong when choosing Vehicle Class";
                return Msg;
            }
            CheckSession.ClearSession(4, "sticker");
            HttpContext.Current.Session["S_VehicleClass"] = VehicleClass;
            HttpContext.Current.Session["S_VehicleClass_imgPath"] = VehicleClassImgPath.Replace(".svg", "-w.svg"); ;

            Msg = "Success";
            return Msg;
        }



        #region SetDealerAffixationcenterid by ankit chaudhary use on Dealers.aspx
        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("sticker/api/Get/SetDealerAffixationCenter")]
        public String SetDealerAffixationCenter(string SessionValue)
        {
            try
            {
                CheckSession.ClearSession(9, "sticker");

                if (SessionValue == "" || SessionValue == null)
                {
                    Msg = "Please Select Dealer First";
                    return Msg;
                }


                HttpContext.Current.Session["S_DealerAffixationCenterid"] = SessionValue;
                Msg = "Success";

            }
            catch (Exception)
            {
                Msg = "Something went wrong please try after some time";
                return Msg;

            }
            return Msg;





        }

        //[System.Web.Http.HttpGet()]
        //[System.Web.Http.Route("sticker/api/Get/Earliest_DateandSlot")]
        //public String Earliest_DateandSlot(string Affixationid)
        //{
        //    try
        //    {
        //        // Thread.Sleep(5000);

        //        if (Affixationid == "" || Affixationid == null)
        //        {
        //            Msg = "Please Select Dealer First";
        //            return Msg;
        //        }
        //        string Qstr = "execute BindEarliestDateAndSlot '" + Affixationid + "'";
        //        DataTable dtDateAndSlot = Utils.GetDataTable(Qstr, ConnectionString);
        //        StringBuilder sbTable = new StringBuilder();

        //        if (dtDateAndSlot.Rows.Count > 0)
        //        {

        //            sbTable.Append("<p class='fg2'><span>Earliest Date Available:</span>" + dtDateAndSlot.Rows[0]["EarliestDateAvailable"].ToString() + "</p>" +
        //                                          "<p class='fg3'><span>Earliest Time Slot Available:</span>" + dtDateAndSlot.Rows[0]["EarliestTimeSlotAvailable"].ToString() + "</p>");
        //            Msg = sbTable.ToString();
        //        }
        //        else
        //        {
        //            Msg = "No Earliest Date Available";
        //        }





        //    }
        //    catch (Exception)
        //    {
        //        Msg = "Something went wrong please try after some time";
        //        return Msg;

        //    }
        //    return Msg;





        //}

        public class Response
        {
            public string Status { set; get; }
            public string Msg { set; get; }

            public string ResponseData { set; get; }

        }

        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("sticker/api/Get/Earliest_DateandSlot")]
        public Response Earliest_DateandSlot(string Affixationid)
        {
            Response res = new Response();
            try
            {
                // Thread.Sleep(5000);

                if (Affixationid == "" || Affixationid == null)
                {
                    res.Status = "0";
                    res.Msg = "Please Select Dealer First";
                    res.ResponseData = "";
                    return res;
                }
                string Qstr = "execute BindEarliestDateAndSlot_Sticker_temp '" + Affixationid + "' ,'" + HttpContext.Current.Session["S_OEMId"].ToString() + "','" + HttpContext.Current.Session["S_DeliveryPoint"].ToString() + "','" + HttpContext.Current.Session["S_StateId"].ToString() + "','" + HttpContext.Current.Session["S_VehicleTypeid"].ToString() + "'";
                DataTable dtDateAndSlot = Utils.GetDataTable(Qstr, ConnectionString);
                StringBuilder sbTable = new StringBuilder();

                if (dtDateAndSlot.Rows.Count > 0)
                {

                    sbTable.Append("<p class='fg2'><span>Earliest Date Available:</span>" + dtDateAndSlot.Rows[0]["EarliestDateAvailable"].ToString() + "</p>");

                    #region Bind Earliest Time Slot

                    CultureInfo provider = CultureInfo.InvariantCulture;
                    try
                    {
                        string SelectedDateTime = dtDateAndSlot.Rows[0]["EarliestDateAvailable"].ToString();

                        string paramDate = dtDateAndSlot.Rows[0]["EarliestDateAvailableParam"].ToString();

                        if (true)
                        {
                            string sqlQuery = string.Empty;

                            //sqlQuery = "EXEC [dbo].[jsp_BHSRP_GET_TIME_SLOT] @VehicleTypeID=" + HttpContext.Current.Session["VehicleTypeid"].ToString() + ",@RTOCodeID=" + Affixationid + "";

                            if (HttpContext.Current.Session["S_DeliveryPoint"].ToString() == "Dealer")
                            {
                                sqlQuery = "CheckApointmentTimeSlot '" + paramDate + "','" + HttpContext.Current.Session["S_VehicleTypeid"].ToString() + "','" + Affixationid + "','" + HttpContext.Current.Session["S_DeliveryPoint"].ToString() + "','" + HttpContext.Current.Session["S_StateId"].ToString() + "','Sticker' "; //Session["StateId"].ToString()
                            }
                            else if (HttpContext.Current.Session["S_DeliveryPoint"].ToString() == "Home")
                            {
                                sqlQuery = "CheckApointmentTimeSlot_Home '" + paramDate + "','" + HttpContext.Current.Session["S_VehicleTypeid"].ToString() + "','" + Affixationid + "','" + HttpContext.Current.Session["S_DeliveryPoint"].ToString() + "','" + HttpContext.Current.Session["S_StateId"].ToString() + "',Sticker "; //Session["StateId"].ToString()
                            }

                            DataTable dtTimeSlot = Utils.GetDataTable(sqlQuery, ConnectionString);
                            if (dtTimeSlot.Rows.Count > 0)
                            {

                                foreach (DataRow dr in dtTimeSlot.Rows)
                                {
                                    string SlotIDWithTimeSlot = dr["TimeSlotID"].ToString() + "`" + dr["SlotName"].ToString();

                                    if (dr["AvaiableStatus"].ToString().ToString() == "N")
                                    {

                                        res.Status = "0";
                                    }
                                    else
                                    {
                                        sbTable.Append("<p class='fg3'><span>Earliest Time Slot Available:</span>" + dr["SlotName"].ToString() + "</p>");
                                        res.Status = "1";
                                        res.Msg = "Success";
                                        break;

                                }


                            }

                                if (res.Status == "0")
                                {
                                    sbTable.Append("<p class='fg3'><span>Earliest Time Slot Available:</span>Not Availabe</p>");
                                    res.Msg = "Not Availabe";
                                }

                            }
                            else
                            {
                                //sbTable.Append("<p class='fg2'><span>Earliest Date Available:</span>Not Availabe</p>");
                                sbTable.Append("<p class='fg3'><span>Earliest Time Slot Available:</span>Not Availabe</p>");
                                Msg = sbTable.ToString();
                                res.Status = "0";
                                res.Msg = "Not Availabe";
                                res.ResponseData = sbTable.ToString();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        res.Status = "0";
                        res.Msg = ex.ToString();
                        res.ResponseData = "";

                        return res;
                    }
                    #endregion



                    res.ResponseData = sbTable.ToString();

                }
                else
                {
                    sbTable.Append("<p class='fg2'><span>Earliest Date Available:</span>Not Availabe</p>");
                    sbTable.Append("<p class='fg3'><span>Earliest Time Slot Available:</span>Not Availabe</p>");
                    Msg = sbTable.ToString();
                    res.Status = "0";
                    res.Msg = "Not Availabe";
                    res.ResponseData = sbTable.ToString();
                }





            }
            catch (Exception ex)
            {
                res.Status = "0";
                res.Msg = ex.ToString();
                res.ResponseData = "";
                return res;

            }
            return res;


        }




        #endregion


        // Brij

        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("sticker/api/Get/SetMapAddress")]
        public string SetMapAddress(string _mapaddress)
        {
            HttpContext.Current.Session["S_DeliveryAddress1"] = "";
            CheckSession.ClearSession(11, "sticker");
            string[] alladdressValue = _mapaddress.Split('@');
            Msg = "";
            //Msg = CheckAvailability(alladdressValue[4]);

            string Qstr = "Execute CheckHomeDeliveryAvailability '" + HttpContext.Current.Session["S_Oemid"].ToString() + "','" + HttpContext.Current.Session["S_StateId"].ToString() + "', '" + alladdressValue[4] + "'";
            DataTable dtCheckPincode = Utils.GetDataTable(Qstr, ConnectionString);

            if (dtCheckPincode.Rows.Count > 0)
            {
                HttpContext.Current.Session["S_DealerAffixationCenterid"] = dtCheckPincode.Rows[0]["S_DealerAffixationID"].ToString();

                Msg = "Available";
            }
            else
            {
                Msg = "Not Available";
            }
            if (Msg == "Available")
            {
                HttpContext.Current.Session["S_mapAddress1"] = alladdressValue[0];
                HttpContext.Current.Session["S_mapAddress2"] = alladdressValue[1];
                HttpContext.Current.Session["S_mapCity"] = alladdressValue[2];
                HttpContext.Current.Session["S_mapStates"] = alladdressValue[3];
                HttpContext.Current.Session["S_mapPincode"] = alladdressValue[4];
                HttpContext.Current.Session["S_mapLandmark"] = alladdressValue[5];
                HttpContext.Current.Session["S_DeliveryAddress1"] = "";


            }

            return Msg;
        }


        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("sticker/api/Get/SetVehicleType")]
        public string SetVehicleType(string radiovehicletype)
        {
            CheckSession.ClearSession(6, "sticker");
            Msg = "";
            if (radiovehicletype.Trim() == "undefined" || radiovehicletype == null)
            {
                Msg = "Please select Vehicle Type";
                return Msg;
            }
            else
            {
                string[] allVehicleType = radiovehicletype.Split('@');
                HttpContext.Current.Session["S_VehicleCat"] = allVehicleType[2];
                HttpContext.Current.Session["S_VehicleType"] = allVehicleType[1];
                HttpContext.Current.Session["S_VehicleTypeid"] = allVehicleType[0];
                HttpContext.Current.Session["S_Vehiclecategoryid"] = allVehicleType[3];
                HttpContext.Current.Session["S_VehicleCat"].ToString();

                Msg = "Success";

            }
            return Msg;

        }

        // Brij


        // Anand Code

        [System.Web.Http.HttpGet()]
        [Route("sticker/api/Get/Setstate")]
        public Response Setstate(string state, string ShortName, string statename)
        {
            Response Res = new Response();
            string Url = string.Empty;
            if (state == "" || state == null)
            {
                Res.Status = "0";
                Res.Msg = "Please Select State";
                Res.ResponseData = null;
                return Res;
            }
            #region Check Url OemWise
            string Qstr = "execute GetOemUrl '" + state + "','" + HttpContext.Current.Session["S_OEMId"].ToString() + "'";
            DataTable DtUrl = Utils.GetDataTable(Qstr, ConnectionString);
            if (DtUrl.Rows.Count > 0)
            {
                Res.Msg = DtUrl.Rows[0]["Vendor"].ToString();
                Url = DtUrl.Rows[0]["Url"].ToString();
            }
            else
            {
                Res.Msg = "Rosmerta";
                Url = "Rosmerta";
            }

            #endregion


           
            CheckSession.ClearSession(3, "sticker");
            HttpContext.Current.Session["S_StateId"] = state;
            HttpContext.Current.Session["S_StateShortName"] = ShortName;
            HttpContext.Current.Session["S_StateName"] = statename;

            Res.Status = "1";

            Res.ResponseData = Url;
            return Res;
        }

        [System.Web.Http.HttpGet()]
        [Route("sticker/api/Get/Setfueltype")]
        public string Setfueltype(string fueltype)
        {
            if (fueltype == "" || fueltype == null)
            {
                Msg = "Please Select Your Fuel Type";
                return Msg;

            }
            CheckSession.ClearSession(5, "sticker");
            HttpContext.Current.Session["S_VehicleFuelType"] = fueltype;
            Msg = "Success";
            return Msg;
        }
        //Anannd

        //created By Tek Chand

        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("sticker/api/Get/SessionBookingDetail")]
        public String SessionBookingDetail(string SessionBs, string SessionRD, string SessionRN, string SessionCHN, string SessionEN, string SessionON, string SessionEID, string SessionMn, string SessionFP, string SessionBA, string SessionState, string SessionCity, string SessionGST, string SFLCode, string SRLCode)
        {

            try
            {
                CheckSession.ClearSession(7, "sticker");
                if (SessionBs == "" || SessionBs == null)
                {
                    Msg = "Please select Bharat Stage value";
                    return Msg;
                }
                else if (SessionRD == "" || SessionRD == null)
                {
                    Msg = "Please enter registration date";
                    return Msg;
                }
                else if (SessionRN == "" || SessionRN == null)
                {
                    Msg = "Please registration number";
                    return Msg;
                }

                else if (SessionCHN == "" || SessionCHN == null)
                {
                    Msg = "Please enter Chasis No.";
                    return Msg;
                }
                else if (SessionCHN.Length < 5 )
                {
                    Msg = "Please enter valid Chasis No.";
                    return Msg;
                }
                else if (SessionEN == "" || SessionEN == null)
                {
                    Msg = "Please enter Engine number";
                    return Msg;
                }
                else if (SessionEN.Length < 5)
                {
                    Msg = "Please enter valid Engine number";
                    return Msg;
                }
                else if (SessionON == "" || SessionON == null)
                {
                    Msg = "Please enter owner name";
                    return Msg;
                }
                else if (SessionEID == "" || SessionEID == null)
                {
                    Msg = "Please enter Email Id";
                    return Msg;
                }
                else if (SessionMn == "" || SessionMn == null || SessionMn.Length <10)
                {
                    Msg = "Please enter Mobile No";
                    return Msg;
                }

                else if (SessionBA == "" || SessionBA == null)
                {
                    Msg = "Please enter Billing Address";
                    return Msg;
                }
                else if (SessionState == "" || SessionState == null)
                {
                    Msg = "Please enter State";
                    return Msg;
                }
                else if (SessionCity == "" || SessionCity == null)
                {
                    Msg = "Please enter City";
                    return Msg;
                }
                //else if (SessionGST == "" || SessionGST == null)
                //{
                //    Msg = "Please enter GST";
                //    return Msg;
                //}

                // Check Fron and rear laser code empty or not
                else if (SFLCode == "" || SFLCode == null)
                {
                    Msg = "Please enter Front Laser Code";
                    return Msg;
                }
                else if (SRLCode == "" || SRLCode == null)
                {
                    Msg = "Please enter Rear Laser Code";
                    return Msg;
                }

                else if (SFLCode.ToLower().StartsWith("aa") == false)
                {

                    Msg = "Please enter valid Front Laser Code";
                    return Msg;
                }
                else if (SRLCode.ToLower().StartsWith("aa") == false)
                {

                    Msg = "Please enter valid Rear Laser Code";
                    return Msg;
                }



                if (SessionRN.ToLower().StartsWith("up") == true || SessionRN.ToLower().StartsWith("dl") == true || SessionRN.ToLower().StartsWith("hp") == true || SessionRN.ToLower().StartsWith("d") == true)
                {



                }
                else
                {
                    Msg = "Please input Correct Registration Number of MP State";
                    return Msg;

                }
                #region CheckRegDate Validation
                try
                {
                    IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-GB", true);
                    DateTime from_date = DateTime.ParseExact(SessionRD, "dd/MM/yyyy", theCultureInfo);
                    DateTime to = DateTime.ParseExact("01/04/2020", "dd/MM/yyyy", theCultureInfo);
                    string txt_total_days = ((from_date - to).TotalDays).ToString();
                    int diffResult = int.Parse(txt_total_days.ToString());
                    //if (HttpContext.Current.Session["StateId"].ToString() == "37")
                    //{
                    if (diffResult >= 0)
                    {
                        Msg = "Vehicle Registration date should only be before 01/04/2020, Format is DD/MM/YYYY";
                        return Msg;
                    }
                    //}


                }
                catch (Exception ex)
                {
                    Msg = "Check Registration date Format DD/MM/YYYY";
                    return Msg;
                }

                #endregion 

                if (HttpContext.Current.Session["S_Oemid"] != null)
                {
                    string APIresponse = rosmerta_API(SessionRN, SessionCHN, SessionEN, "5UwoklBqiW");
                    bool ValidatedData = false;

                    bool CheckedStatus = false;
                    CheckedStatus = checkVehicleForSticker(SessionRN, SessionCHN, SessionEN, SFLCode, SRLCode);

                    if (HttpContext.Current.Session["Oemid"].ToString() == "191" && APIresponse.Contains("Vehicle Present Maker Present"))
                    {
                        if (CheckedStatus == true)
                        {

                            HttpContext.Current.Session["S_SessionBharatStage"] = SessionBs;
                            HttpContext.Current.Session["S_SessionRegDate"] = SessionRD;
                            HttpContext.Current.Session["S_SessionRegNo"] = SessionRN;
                            HttpContext.Current.Session["S_SessionChasisno"] = SessionCHN;
                            HttpContext.Current.Session["S_SessionEngno"] = SessionEN;
                            HttpContext.Current.Session["S_SessionOwnerName"] = SessionON;
                            HttpContext.Current.Session["S_SessionEmailID"] = SessionEID;
                            HttpContext.Current.Session["S_SessionMobileNo"] = SessionMn;

                            HttpContext.Current.Session["S_SessionBillingAddress"] = SessionBA;
                            HttpContext.Current.Session["S_SessionState"] = SessionState;
                            HttpContext.Current.Session["S_SessionCity"] = SessionCity;
                            HttpContext.Current.Session["S_SessionGST"] = SessionGST;

                            HttpContext.Current.Session["S_FrontLaserCode"] = SFLCode;
                            HttpContext.Current.Session["S_RearLaserCode"] = SRLCode;
                            Msg = "Success";
                            ValidatedData = true;
                        }
                        else
                        {
                            //Msg = "HSRP Fitment Not Found Kindly Get HSRP Fitted.";
                            Msg = "Kindly update details from your registration certificate. (" + SessionRN+")";
                            //return Msg;
                        }
                    }
                    else if (APIresponse.Contains("Vehicle Present Maker Present in HOMOLOGATION  and you are authorized vendor for this vehicle"))
                    {
                       // CheckedStatus = checkVehicleForSticker(SessionRN, SessionCHN, SessionEN, SFLCode, SRLCode);

                        if (CheckedStatus == true)
                        {

                            HttpContext.Current.Session["S_SessionBharatStage"] = SessionBs;
                            HttpContext.Current.Session["S_SessionRegDate"] = SessionRD;
                            HttpContext.Current.Session["S_SessionRegNo"] = SessionRN;
                            HttpContext.Current.Session["S_SessionChasisno"] = SessionCHN;
                            HttpContext.Current.Session["S_SessionEngno"] = SessionEN;
                            HttpContext.Current.Session["S_SessionOwnerName"] = SessionON;
                            HttpContext.Current.Session["S_SessionEmailID"] = SessionEID;
                            HttpContext.Current.Session["S_SessionMobileNo"] = SessionMn;

                            HttpContext.Current.Session["S_SessionBillingAddress"] = SessionBA;
                            HttpContext.Current.Session["S_SessionState"] = SessionState;
                            HttpContext.Current.Session["S_SessionCity"] = SessionCity;
                            HttpContext.Current.Session["S_SessionGST"] = SessionGST;

                            HttpContext.Current.Session["S_FrontLaserCode"] = SFLCode;
                            HttpContext.Current.Session["S_RearLaserCode"] = SRLCode;
                            Msg = "Success";
                            ValidatedData = true;
                        }
                        else
                        {
                            //Msg = "HSRP Fitment Not Found Kindly Get HSRP Fitted.";
                            //Msg = "Kindly update details from your registration certificate.";
                            //Msg = "Kindly update details from your registration certificate. (" + SessionRN + ")";
                            Msg = "unsuccessfulbooking";
                            //return Msg;
                        }
                    }
                    else
                    {
                        //Msg = "Kindly update details from your registration certificate.";
                        Msg = "Kindly update details from your registration certificate. (" + SessionRN + ")";
                        //return Msg;
                    }

                    try
                    {
                        string StickerValidation = "N";
                        string vahanStatus = "N";

                        #region
                        if (CheckedStatus)
                        {
                            StickerValidation = "Y";
                        }
                        if (APIresponse.Contains("Vehicle Present Maker Present in HOMOLOGATION  and you are authorized vendor for this vehicle"))
                        {
                            vahanStatus = "Y";
                        }

                        string InsertVahanLogQuery = " insert into [BookMyHSRP].dbo.VehiclePlateEntryLog " +
                         "(BharatStatge, VehicleRegNo, RegistrationDate,ChassisNo,EngineNo,OwnerName,EmailID, MobileNo, " +
                         "CustomerAddress,StateName,OwnerCity,GSTNO,VahanValidation,VahanResponse,Ordertype, " +
                         "VehicleType,VehicleClass,Created_Date, " +
                         "HSRP_StateID,OEMID,StickerValidation,FLaserCode,RLaserCode) values " +
                         "('" + SessionBs + "','" + SessionRN + "','" + SessionRD + "','" + SessionCHN + "','" + SessionEN + "','" + SessionON + "','" + SessionEID + "','" + SessionMn + "'," +
                         "'" + SessionBA + "','" + SessionState + "','" + SessionCity + "','" + SessionGST + "','" + vahanStatus + "','" + APIresponse + "','" + HttpContext.Current.Session["S_OrderType"].ToString() + "'," +
                         "'" + HttpContext.Current.Session["S_VehicleType"].ToString() + "','" + HttpContext.Current.Session["S_VehicleClass"].ToString() + "',getdate()," +
                         "'" + HttpContext.Current.Session["S_StateId"].ToString() + "','" + HttpContext.Current.Session["S_Oemid"].ToString() + "','" + StickerValidation + "','" + SFLCode + "','" + SRLCode + "') ";
                        Utils.ExecNonQuery(InsertVahanLogQuery, ConnectionString);

                        #endregion
                    }
                    catch (Exception ev)
                    {

                    }

                    if (!ValidatedData)
                    {
                        return Msg;
                    }
                   
                    //if (APIresponse.Contains("Vehicle Present Maker Present in HOMOLOGATION  and you are authorized vendor for this vehicle"))
                   
                }

                return Msg;

            }
            catch (Exception ex)
            {

                throw;
            }


        }
        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("sticker/api/Get/SetSessionBookingDetail")]
        public String SetSessionBookingDetail(string SessionBs, string SessionRD, string SessionRN, string SessionCHN, string SessionEN, string SessionON, string SessionEID, string SessionMn, string SessionFP, string SessionBA, string SessionState, string SessionCity, string SessionGST, string SFLCode, string SRLCode, string Maker, string VehicleType, string FuelType, string VehiceCat, string Stateid, string FrontLaserFileName, string RearLaserFileName,string filename3,string filename4)
        {

            try
            {
                CheckSession.ClearSession(7, "sticker");

                if (SessionBs == "" || SessionBs == null)
                {
                    Msg = "Please select Bharat Stage value";
                    return Msg;
                }
                else if (SessionRD == "" || SessionRD == null)
                {
                    Msg = "Please enter registration date";
                    return Msg;
                }
                else if (SessionRN == "" || SessionRN == null)
                {
                    Msg = "Please registration number";
                    return Msg;
                }

                else if (SessionCHN == "" || SessionCHN == null)
                {
                    Msg = "Please enter Chasis No.";
                    return Msg;
                }
                else if (SessionCHN.Length < 5)
                {
                    Msg = "Please enter valid Chasis No.";
                    return Msg;
                }
                else if (SessionEN == "" || SessionEN == null)
                {
                    Msg = "Please enter Engine number";
                    return Msg;
                }
                else if (SessionEN.Length < 5)
                {
                    Msg = "Please enter valid Engine number";
                    return Msg;
                }
                else if (SessionON == "" || SessionON == null)
                {
                    Msg = "Please enter owner name";
                    return Msg;
                }
                else if (SessionEID == "" || SessionEID == null)
                {
                    Msg = "Please enter Email Id";
                    return Msg;
                }
                else if (SessionMn == "" || SessionMn == null || SessionMn.Length < 10)
                {
                    Msg = "Please enter Mobile No";
                    return Msg;
                }

                else if (SessionBA == "" || SessionBA == null)
                {
                    Msg = "Please enter Billing Address";
                    return Msg;
                }
                else if (SessionState == "" || SessionState == null)
                {
                    Msg = "Please enter State";
                    return Msg;
                }
                else if (SessionCity == "" || SessionCity == null)
                {
                    Msg = "Please enter City";
                    return Msg;
                }
                //else if (SessionGST == "" || SessionGST == null)
                //{
                //    Msg = "Please enter GST";
                //    return Msg;
                //}

                // Check Fron and rear laser code empty or not
                else if (SFLCode == "" || SFLCode == null)
                {
                    Msg = "Please enter Front Laser Code";
                    return Msg;
                }
                else if (SRLCode == "" || SRLCode == null)
                {
                    Msg = "Please enter Rear Laser Code";
                    return Msg;
                }
                else if (SFLCode.ToLower().StartsWith("aa") == false)
                {

                    Msg = "Please enter valid Front Laser Code";
                    return Msg;
                }
                else if (SRLCode.ToLower().StartsWith("aa") == false)
                {

                    Msg = "Please enter valid Rear Laser Code";
                    return Msg;
                }



                else if (Maker == "" || Maker == null)
                {
                    Msg = "Please enter Maker";
                    return Msg;
                }

                else if (VehicleType == "" || VehicleType == null)
                {
                    Msg = "Please enter Vehicle Type";
                    return Msg;
                }

                else if (FuelType == "" || FuelType == null)
                {
                    Msg = "Please enter Fuel Type";
                    return Msg;
                }

                else if (VehiceCat == "" || VehiceCat == null)
                {
                    Msg = "Please enter Vehicle Category";
                    return Msg;
                }

               

                if (SessionRN.ToLower().StartsWith("up") == true || SessionRN.ToLower().StartsWith("dl") == true || SessionRN.ToLower().StartsWith("hp") == true || SessionRN.ToLower().StartsWith("d") == true || SessionRN.ToLower().StartsWith("mp") == true)
                {



                }
                else
                {
                    Msg = "Please input Correct Registration Number of MP State";
                    return Msg;

                }
                //HttpContext.Current.Session["S_VehicleFuelType"] = FuelType;
                //HttpContext.Current.Session["S_VehicleClass"] = VehicleType;
                HttpContext.Current.Session["DeliveryPoint"] = "Dealer";
                #region CheckRegDate Validation
                try
                {
                    IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-GB", true);
                    DateTime from_date = DateTime.ParseExact(SessionRD, "dd/MM/yyyy", theCultureInfo);
                    DateTime to = DateTime.ParseExact("01/04/2020", "dd/MM/yyyy", theCultureInfo);
                    string txt_total_days = ((from_date - to).TotalDays).ToString();
                    int diffResult = int.Parse(txt_total_days.ToString());
                    //if (HttpContext.Current.Session["StateId"].ToString() == "37")
                    //{
                    if (diffResult >= 0)
                    {
                        Msg = "Vehicle Registration date should only be before 01/04/2020, Format is DD/MM/YYYY";
                        return Msg;
                    }
                    //}


                }
                catch (Exception ex)
                {
                    Msg = "Check Registration date Format DD/MM/YYYY";
                    return Msg;
                }

                #endregion

                #region Session set 

                HttpContext.Current.Session["S_VehicleClass_imgPath"] = "WWW";
                HttpContext.Current.Session["S_VehicleFuelType"] = FuelType;
                HttpContext.Current.Session["S_VehicleClass"] = VehicleType;
                HttpContext.Current.Session["S_VehicleType_imgPath"] = "WWW";
                 HttpContext.Current.Session["S_VehicleCat"] = "4W";
                 HttpContext.Current.Session["S_Vehicletype"] = "LMV";
                 HttpContext.Current.Session["S_Vehicletypeid"] = "1";
                HttpContext.Current.Session["S_Vehiclecategoryid"] = "3";
           
                //string Qstr = "select HSRPHRVehicleType,VehicleTypeid,VehicleCategory from [hsrpoem].[dbo].[VahanVehicleType] where VahanVehicleType='" + VehiceCat.Trim() + "'";
                //DataTable DtVehicleSesssion = Utils.GetDataTable(Qstr, ConnectionString);
                //if (DtVehicleSesssion.Rows.Count > 0)
                //{
                //    HttpContext.Current.Session["S_VehicleType"] = DtVehicleSesssion.Rows[0]["HSRPHRVehicleType"].ToString();

                //    HttpContext.Current.Session["S_VehicleClass_imgPath"] = "www";
                //    HttpContext.Current.Session["S_VehicleCat"] = DtVehicleSesssion.Rows[0]["VehicleCategory"].ToString();
                //    HttpContext.Current.Session["S_VehicleTypeid"] = DtVehicleSesssion.Rows[0]["VehicleTypeid"].ToString();
                //    HttpContext.Current.Session["S_Vehiclecategoryid"] = "3";





                //}
                //else
                //{
                //    Msg = "Vehicle Details didn't match";
                //    return Msg;
                //}




                #endregion
                #region New Code For lASER UPLOAD

                int flag = 0;
                if (HttpContext.Current.Session["UploadFlag"].ToString() == "Y")
                {

                    if (FrontLaserFileName == "" || FrontLaserFileName == null || RearLaserFileName == "" || RearLaserFileName == null  || filename3=="" || filename3==null || filename4=="" || filename4==null)
                    {
                        flag = 0;
                        Msg = "Please Upload Front/Rear and other File";
                        return Msg;
                    }



                    else
                    {
                        flag = 1;

                        HttpContext.Current.Session["S_FrontLaserFileName"] = FrontLaserFileName;
                        HttpContext.Current.Session["S_RearLaserFileName"] = RearLaserFileName;
                        HttpContext.Current.Session["S_File3"] = filename3;
                        HttpContext.Current.Session["S_File4"] = filename4;
                    }
                }
                #endregion
                #region Vehicle Plate Entry Log
                string InsertVahanLogQuery = " insert into [BookMyHSRP].dbo.VehiclePlateEntryLog " +
                  "(vahanbsstage, VehicleRegNo, RegistrationDate,ChassisNo,EngineNo,OwnerName,EmailID, MobileNo, " +
                  "CustomerAddress,StateName,OwnerCity,GSTNO,VahanValidation,VahanResponse,Ordertype, " +
                  "VehicleType,VehicleClass,Created_Date, " +
                  "HSRP_StateID,OEMID,StickerValidation,FLaserCode,RLaserCode,fuelType,vahanfuel) values " +
                  "('" + SessionBs + "','" + SessionRN + "','" + SessionRD + "','" + SessionCHN + "','" + SessionEN + "','" + SessionON + "','" + SessionEID + "','" + SessionMn + "'," +
                  "'" + SessionBA + "','" + SessionState + "','" + SessionCity + "','" + SessionGST + "','','','" + HttpContext.Current.Session["S_OrderType"].ToString() + "'," +
                  "'" + VehiceCat.Trim() + "','" + VehicleType + "',getdate()," +
                  "'" + HttpContext.Current.Session["S_StateId"].ToString() + "','" + HttpContext.Current.Session["S_Oemid"].ToString() + "','N','" + SFLCode + "','" + SRLCode + "','" + FuelType + "','" + FuelType + "') ";
                Utils.ExecNonQuery(InsertVahanLogQuery, ConnectionString);

                #endregion



                #region old Code Session Binding
                //if (HttpContext.Current.Session["S_Oemid"] != null)
                //{
                //    string APIresponse =  rosmerta_API(SessionRN, SessionCHN, SessionEN, "5UwoklBqiW");
                //    bool ValidatedData = false;

                //    bool CheckedStatus = false;
                //    CheckedStatus =  checkVehicleForSticker(SessionRN, SessionCHN, SessionEN, SFLCode, SRLCode);

                //    if (HttpContext.Current.Session["Oemid"].ToString() == "191" && APIresponse.Contains("Vehicle Present Maker Present"))
                //    {
                //        if (CheckedStatus == true)
                //        {

                //            HttpContext.Current.Session["S_SessionBharatStage"] = SessionBs;
                //            HttpContext.Current.Session["S_SessionRegDate"] = SessionRD;
                //            HttpContext.Current.Session["S_SessionRegNo"] = SessionRN;
                //            HttpContext.Current.Session["S_SessionChasisno"] = SessionCHN;
                //            HttpContext.Current.Session["S_SessionEngno"] = SessionEN;
                //            HttpContext.Current.Session["S_SessionOwnerName"] = SessionON;
                //            HttpContext.Current.Session["S_SessionEmailID"] = SessionEID;
                //            HttpContext.Current.Session["S_SessionMobileNo"] = SessionMn;

                //            HttpContext.Current.Session["S_SessionBillingAddress"] = SessionBA;
                //            HttpContext.Current.Session["S_SessionState"] = SessionState;
                //            HttpContext.Current.Session["S_SessionCity"] = SessionCity;
                //            HttpContext.Current.Session["S_SessionGST"] = SessionGST;

                //            HttpContext.Current.Session["S_FrontLaserCode"] = SFLCode;
                //            HttpContext.Current.Session["S_RearLaserCode"] = SRLCode;
                //            Msg = "Success";
                //            ValidatedData = true;
                //        }
                //        else
                //        {
                //            //Msg = "HSRP Fitment Not Found Kindly Get HSRP Fitted.";
                //            Msg = "Kindly update details from your registration certificate. (" + SessionRN+")";
                //            //return Msg;
                //        }
                //    }
                //    else if (APIresponse.Contains("Vehicle Present Maker Present in HOMOLOGATION  and you are authorized vendor for this vehicle"))
                //    {
                //       // CheckedStatus = checkVehicleForSticker(SessionRN, SessionCHN, SessionEN, SFLCode, SRLCode);

                //        if (CheckedStatus == true)
                //        {

                //            HttpContext.Current.Session["S_SessionBharatStage"] = SessionBs;
                //            HttpContext.Current.Session["S_SessionRegDate"] = SessionRD;
                //            HttpContext.Current.Session["S_SessionRegNo"] = SessionRN;
                //            HttpContext.Current.Session["S_SessionChasisno"] = SessionCHN;
                //            HttpContext.Current.Session["S_SessionEngno"] = SessionEN;
                //            HttpContext.Current.Session["S_SessionOwnerName"] = SessionON;
                //            HttpContext.Current.Session["S_SessionEmailID"] = SessionEID;
                //            HttpContext.Current.Session["S_SessionMobileNo"] = SessionMn;

                //            HttpContext.Current.Session["S_SessionBillingAddress"] = SessionBA;
                //            HttpContext.Current.Session["S_SessionState"] = SessionState;
                //            HttpContext.Current.Session["S_SessionCity"] = SessionCity;
                //            HttpContext.Current.Session["S_SessionGST"] = SessionGST;

                //            HttpContext.Current.Session["S_FrontLaserCode"] = SFLCode;
                //            HttpContext.Current.Session["S_RearLaserCode"] = SRLCode;
                //            Msg = "Success";
                //            ValidatedData = true;
                //        }
                //        else
                //        {
                //            //Msg = "HSRP Fitment Not Found Kindly Get HSRP Fitted.";
                //            //Msg = "Kindly update details from your registration certificate.";
                //            Msg = "Kindly update details from your registration certificate. (" + SessionRN + ")";
                //            //return Msg;
                //        }
                //    }
                //    else
                //    {
                //        //Msg = "Kindly update details from your registration certificate.";
                //        Msg = "Kindly update details from your registration certificate. (" + SessionRN + ")";
                //        //return Msg;
                //    }

                //    try
                //    {
                //        string StickerValidation = "N";
                //        string vahanStatus = "N";

                //        #region
                //        if (CheckedStatus)
                //        {
                //            StickerValidation = "Y";
                //        }
                //        if (APIresponse.Contains("Vehicle Present Maker Present in HOMOLOGATION  and you are authorized vendor for this vehicle"))
                //        {
                //            vahanStatus = "Y";
                //        }

                //        string InsertVahanLogQuery = " insert into [BookMyHSRP].dbo.VehiclePlateEntryLog " +
                //         "(BharatStatge, VehicleRegNo, RegistrationDate,ChassisNo,EngineNo,OwnerName,EmailID, MobileNo, " +
                //         "CustomerAddress,StateName,OwnerCity,GSTNO,VahanValidation,VahanResponse,Ordertype, " +
                //         "VehicleType,VehicleClass,Created_Date, " +
                //         "HSRP_StateID,OEMID,StickerValidation,FLaserCode,RLaserCode) values " +
                //         "('" + SessionBs + "','" + SessionRN + "','" + SessionRD + "','" + SessionCHN + "','" + SessionEN + "','" + SessionON + "','" + SessionEID + "','" + SessionMn + "'," +
                //         "'" + SessionBA + "','" + SessionState + "','" + SessionCity + "','" + SessionGST + "','" + vahanStatus + "','" + APIresponse + "','" + HttpContext.Current.Session["S_OrderType"].ToString() + "'," +
                //         "'" + HttpContext.Current.Session["S_VehicleType"].ToString() + "','" + HttpContext.Current.Session["S_VehicleClass"].ToString() + "',getdate()," +
                //         "'" + HttpContext.Current.Session["S_StateId"].ToString() + "','" + HttpContext.Current.Session["S_Oemid"].ToString() + "','" + StickerValidation + "','" + SFLCode + "','" + SRLCode + "') ";
                //        Utils.ExecNonQuery(InsertVahanLogQuery, ConnectionString);

                //        #endregion
                //    }
                //    catch (Exception ev)
                //    {

                //    }

                //    if (!ValidatedData)
                //    {
                //        return Msg;
                //    }

                //    //if (APIresponse.Contains("Vehicle Present Maker Present in HOMOLOGATION  and you are authorized vendor for this vehicle"))

                //}
                #endregion


                #region New Code Session Binding
                HttpContext.Current.Session["S_SessionBharatStage"] = SessionBs;
                HttpContext.Current.Session["S_SessionRegDate"] = SessionRD;
                HttpContext.Current.Session["S_SessionRegNo"] = SessionRN;
                HttpContext.Current.Session["S_SessionChasisno"] = SessionCHN;
                HttpContext.Current.Session["S_SessionEngno"] = SessionEN;
                HttpContext.Current.Session["S_SessionOwnerName"] = SessionON;
                HttpContext.Current.Session["S_SessionEmailID"] = SessionEID;
                HttpContext.Current.Session["S_SessionMobileNo"] = SessionMn;

                HttpContext.Current.Session["S_SessionBillingAddress"] = SessionBA;
                HttpContext.Current.Session["S_SessionState"] = SessionState;
                HttpContext.Current.Session["S_SessionCity"] = SessionCity;
                HttpContext.Current.Session["S_SessionGST"] = SessionGST;

                HttpContext.Current.Session["S_FrontLaserCode"] = SFLCode;
                HttpContext.Current.Session["S_RearLaserCode"] = SRLCode;
                Msg = "Success";
                #endregion
                return Msg;

            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public static string rosmerta_API(string vehRegNo, string chasiNo, string EngineNo, string Key)
        {
            string html = string.Empty;


            string decryptedString = string.Empty;

            try
            {

                string url = @"" + ConfigurationManager.AppSettings["VehicleStatusAPI"].ToString() + "?VehRegNo=" + vehRegNo + "&ChassisNo=" + chasiNo + "&EngineNo=" + EngineNo + "&X=" + Key + "";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();

                }

            }
            catch (Exception ev)
            {
                html = "Error While Calling Deaprtment Service- " + ev.Message;
            }


            return html;
        }

        public  bool checkVehicleForSticker(string RegNo,string Chassisno,string EngineNO, string FrontLaser,string RearLaser)
        {
            bool checker = false;

            string Sql = "select top 1 Vehicleregno from [hsrpoem].dbo.hsrprecords WITH (NOLOCK) where Vehicleregno ='" + RegNo + "' " +
                       //"and right(trim(Chassisno),5) = '" + Chassisno.Substring(Chassisno.Length - 5) + "' and right(trim(Engineno),5) ='" + EngineNO.Substring(EngineNO.Length - 5) + "' " +
                       "and (hsrp_front_lasercode='" + FrontLaser + "' or hsrp_rear_lasercode='" + FrontLaser + "') " +
                       "and (hsrp_rear_lasercode='" + RearLaser + "' or hsrp_front_lasercode='" + RearLaser + "') and Vehicletype not in ('Scooter','Motor Cycle')";
            DataTable dt = Utils.GetDataTable(Sql, ConnectionString);


            Sql = "select top 1 Vehicleregno from hsrprecords WITH (NOLOCK) where Vehicleregno ='" + RegNo + "' " +
                        //"and right(trim(Chassisno),5)='" + Chassisno.Substring(Chassisno.Length - 5) + "' and right(trim(Engineno),5) ='" + EngineNO.Substring(EngineNO.Length - 5) + "' " +
                        "and (hsrp_front_lasercode='" + FrontLaser + "' or hsrp_rear_lasercode='" + FrontLaser + "') " +
                        "and (hsrp_rear_lasercode='" + RearLaser + "' or hsrp_front_lasercode='" + RearLaser + "') and Vehicletype not in ('Scooter','Motor Cycle')";
            //DataTable dt = Utils.GetDataTable(Sql, DLConnectionString);
            if (dt.Rows.Count == 0)
            {
                dt = Utils.GetDataTable(Sql, DLConnectionString);
            }

            if (dt.Rows.Count > 0)
            {
                checker = true;
            
            }



            return checker;

        }

        // This api is used in TrackOrder.aspx form 
        //Ashish Code


        //This api is used for OrderCancel.aspx Form 
        //[HttpGet()]
        //[Route("api/Get/OrderNoCancel")]
        //public string OrderNoCancel(string orderno, string vehicleRegno)
        //{
        //    string jsonData = "";
        //    using (SqlConnection con = new SqlConnection(ConnectionString))
        //    {
        //        string Msg = string.Empty;
        //        // string Qstr = "exec jsp_DEV_GET_ORDER_STATUS_Test '" + orderno.Trim() + "','" + vehicleRegno.Trim() + "'";
        //        if (string.IsNullOrEmpty(orderno))
        //        {
        //            Msg = "OrderNo Not Found";
        //            return Msg;
        //        }
        //        if (string.IsNullOrEmpty(vehicleRegno))
        //        {
        //            Msg = "VehicleRegNo Not Found";
        //            return Msg;
        //        }

        //        string Qstr = "  select top 1 HSRPRecord_CreationDate," +
        //         "  case when cast(HSRPRecord_CreationDate as date) = cast(getdate() as date) then 'Y' else 'N' end isAbleToCancelled, " +
        //         "  OrderNo,OrderStatus,SlotTime,SlotBookingDate,EmailID,ChassisNo," +
        //         " EngineNo,VehicleRegNo,Dealerid,OrderStatus from Appointment_BookingHist where OrderNo='" + orderno.Trim() + "' and VehicleRegNo='" + vehicleRegno.Trim() + "' ";
        //        using (SqlCommand cmd = new SqlCommand(Qstr, con))
        //        {
        //            con.Open();
        //            SqlDataReader sdr = cmd.ExecuteReader();
        //            if (sdr.Read())
        //            {
        //                var input = new
        //                {
        //                    isAbleToCancelled = sdr["isAbleToCancelled"],
        //                    ORDER_NUMBER = sdr["OrderNo"],
        //                    REG_NUMBER = sdr["VehicleRegNo"]
        //                };
        //                HttpContext.Current.Session["CANCEL_ORDER_NO"] = input.ORDER_NUMBER.ToString();
        //                HttpContext.Current.Session["REG_NUMBER"] = input.REG_NUMBER.ToString();
        //                jsonData = (new JavaScriptSerializer()).Serialize(input);
        //            }
        //            con.Close();
        //        }
        //    }
        //    return jsonData;



        //}


        [HttpGet()]
        [Route("sticker/api/Get/ValidateData")]
        public VahanResponse ValidateData(string RegNumber, string ChassisNo, string EngineNo, string Stateid, string FrontLaserCode, string RearLaserCode)
        {
            VahanResponse Ores = new VahanResponse();
            try
            {

                if (string.IsNullOrEmpty(Stateid))
                {
                    Ores.Status = "0";
                    Ores.Msg = "Please Select  Vehicle Registration State";
                    Ores.ResponseData = null;
                    return Ores;
                }
                if (Stateid == "Select Vehicle Registration State")
                {
                    Ores.Status = "0";
                    Ores.Msg = "Please Select  Vehicle Registration State";
                    Ores.ResponseData = null;
                    return Ores;
                }

                if (Stateid == "37")
                {
                    HttpContext.Current.Session["S_StateId"] = Stateid;
                    HttpContext.Current.Session["S_StateShortName"] = "Delhi";
                    HttpContext.Current.Session["S_StateName"] = "DL";

                }

                if (Stateid == "31")
                {
                    HttpContext.Current.Session["S_StateId"] = Stateid;
                    HttpContext.Current.Session["S_StateShortName"] = "Uttar Pradesh";
                    HttpContext.Current.Session["S_StateName"] = "UP";

                }
                if (Stateid == "3")
                {
                    HttpContext.Current.Session["S_StateId"] = Stateid;
                    HttpContext.Current.Session["S_StateShortName"] = "HIMACHAL PRADESH";
                    HttpContext.Current.Session["S_StateName"] = "HP";

                }
                if (Stateid == "5")
                {
                    HttpContext.Current.Session["S_StateId"] = Stateid;
                    HttpContext.Current.Session["S_StateShortName"] = "MADHYA PRADESH";
                    HttpContext.Current.Session["S_StateName"] = "MP";

                }



                if (string.IsNullOrEmpty(ChassisNo))
                {
                    Ores.Status = "0";
                    Ores.Msg = "Please Provide Chassisno";
                    Ores.ResponseData = null;
                    return Ores;
                }
                if (ChassisNo.Length < 5)
                {

                    Ores.Status = "0";
                    Ores.Msg = "Please provide Valid Chassis No";
                    Ores.ResponseData = null;
                    return Ores;
                }
                if (string.IsNullOrEmpty(RegNumber))
                {
                    Ores.Status = "0";
                    Ores.Msg = "Please Provide Vehilce No";
                    Ores.ResponseData = null;
                    return Ores;
                }
                if (RegNumber.Length < 5)
                {

                    Ores.Status = "0";
                    Ores.Msg = "Please provide Valid RegNumber No";
                    Ores.ResponseData = null;
                    return Ores;
                }

                if (RegNumber.ToLower().StartsWith("up") == true || RegNumber.ToLower().StartsWith("dl") == true || RegNumber.ToLower().StartsWith("hp") == true || RegNumber.ToLower().StartsWith("d") == true || RegNumber.ToLower().StartsWith("mp") == true)
                {



                }
                else
                {

                    Ores.Status = "0";
                    Ores.Msg = "Please input Correct Registration Number of UP/DL/HP State";
                    Ores.ResponseData = null;
                    return Ores;

                }

                if (string.IsNullOrEmpty(EngineNo))
                {
                    Ores.Status = "0";
                    Ores.Msg = "Please Provide EngineNo No";
                    Ores.ResponseData = null;
                    return Ores;
                }
                if (EngineNo.Length < 5)
                {

                    Ores.Status = "0";
                    Ores.Msg = "Please provide Valid Engine No";
                    Ores.ResponseData = null;
                    return Ores;
                }

                if (string.IsNullOrEmpty(FrontLaserCode))
                {
                    Ores.Status = "0";
                    Ores.Msg = "Please Provide FrontLaserCode";
                    Ores.ResponseData = null;
                    return Ores;
                }
                if (string.IsNullOrEmpty(RearLaserCode))
                {
                    Ores.Status = "0";
                    Ores.Msg = "Please Provide RearLaserCode";
                    Ores.ResponseData = null;
                    return Ores;
                }

                 if (FrontLaserCode.ToLower().StartsWith("aa") == true)
                {
                   
                    
                }
                else
                {
                    Ores.Status = "0";
                    Ores.Msg = "Please enter valid Front Laser Code";
                    Ores.ResponseData = null;
                    return Ores;

                }
                 if (RearLaserCode.ToLower().StartsWith("aa") == true)
                {
                   
                }
                else
                {

                    Ores.Status = "0";
                    Ores.Msg = "Please enter valid Rear Laser Code";
                    Ores.ResponseData = null;
                    return Ores;
                }
                if(RearLaserCode.ToLower().Trim()==FrontLaserCode.ToLower().Trim())
                {

                    Ores.Status = "0";
                    Ores.Msg = "Front Laser and Rear Laser Code cann't be same";
                    Ores.ResponseData = null;
                    return Ores;
                }

                HttpContext.Current.Session["S_FrontLaserFileName"] = "";
                HttpContext.Current.Session["S_RearLaserFileName"] = "";
                HttpContext.Current.Session["S_File3"] = "";
                HttpContext.Current.Session["S_File4"] = "";

                #region Check Order Exists or not
                //string Qstr = string.Empty;

                //Qstr = "select BookingHistoryID from [BookMyHSRP].dbo.Appointment_BookingHist " +
                //"where VehicleRegNo = '" + RegNumber.Trim().ToUpper() + "' and right(trim(Chassisno),5) = '" + ChassisNo.Substring(ChassisNo.Length - 5) + "' " +
                //"and right(trim(Engineno),5) = '" + EngineNo.Substring(EngineNo.Length - 5) + "' and OrderStatus in ('Success','Shipped','Success-Test')"; // and PlateSticker='plate' 
                //DataTable DL = Utils.GetDataTable(Qstr, ConnectionString);

                //if (DL.Rows.Count > 0)
                //{

                //    Ores.Status = "0";
                //    Ores.Msg = "Order for this registration number already exists. For any query kindly mail to online@bookmyhsrp.com";
                //    Ores.ResponseData = null;
                //    return Ores;

                //}

                #endregion

                string responseJson = rosmerta_API_2(RegNumber.ToUpper().Trim(), ChassisNo.ToUpper().Trim(), EngineNo.ToUpper().Trim(), "5UwoklBqiW");


                VehicleDetails _vd = JsonConvert.DeserializeObject<VehicleDetails>(responseJson);
                HttpContext.Current.Session["NonHomo"] = "N";
                HttpContext.Current.Session["UploadFlag"] = "N";
                if (_vd != null)
                {

                    if (_vd.message == "Vehicle details available in Vahan")
                    {


                        bool CheckedStatus = false;
                        CheckedStatus = checkVehicleForSticker(RegNumber.ToUpper().Trim(), ChassisNo.ToUpper().Trim(), EngineNo.ToUpper().Trim(), FrontLaserCode.ToUpper(), RearLaserCode.ToUpper());

                       
                        string InsertVahanLogQuery = "insert into [BookMyHSRP].dbo.VahanResponseLog " +
                         "([VehicleRegNo] ,[ChassisNo],[EngineNo],[Fuel],[BharatState],[VehicleClass],[VehicleType],[Maker],[VahanRespose],[RegDate],[PlateSticker],[FrontLaserCode],[RearLaserCode],[VahanFrontLaserCode],[VahanRearLaserCode] ) values " +
                         "( '" + RegNumber.ToUpper() + "','" + ChassisNo.ToUpper() + "','" + EngineNo.ToUpper() + "','" + _vd.fuel + "','" + _vd.norms + "','" + _vd.vchCatg + "','" + _vd.vchType + "','" + _vd.maker + "','" + responseJson + "'," +
                         "  '" + _vd.regnDate + "','Sticker','" + FrontLaserCode.ToUpper() + "','" + RearLaserCode.ToUpper() + "','"+_vd.hsrpFrontLaserCode+"','"+_vd.hsrpRearLaserCode+"') ";
                        Utils.ExecNonQuery(InsertVahanLogQuery, ConnectionString);
                        if (_vd.vchCatg.ToUpper() == "2WN" || _vd.vchCatg.ToUpper() == "2WIC" || _vd.vchCatg.ToUpper() == "2WT")
                        {
                            Ores.Status = "0";
                            Ores.Msg = "Your vehicle is not mapped for sticker";
                            Ores.UploadFlag = "N";
                            Ores.ResponseData = null;
                            return Ores;
                        }
                        string Qstr =" select Oemid,'https://bookmyhsrp.com/OEMLOGO'+REPLACE(replace(oem_logo,'.png','.jpg'),'images/brands','') as oem_logo from [hsrpoem].dbo.oemmaster  where vahanoemname='" + _vd.maker.Trim() + "' " +
                                 " union " +
                                " select Oemid,'https://bookmyhsrp.com/OEMLOGO' + REPLACE(replace(oem_logo, '.png', '.jpg'), 'images/brands', '') as oem_logo from[hsrpoem].[dbo].[OEMMasterNameMapping]  where vahanoemname = '" + _vd.maker.Trim() + "' ";

                        DataTable Dt = Utils.GetDataTable(Qstr, ConnectionString);
                        if (Dt.Rows.Count > 0)
                        {

                            HttpContext.Current.Session["S_OEMImgPath"] = Dt.Rows[0]["oem_logo"].ToString();

                            HttpContext.Current.Session["S_Oemid"] = Dt.Rows[0]["Oemid"].ToString();
                        }
                        else
                        {
                            Ores.Status = "0";
                            Ores.Msg = _vd.message;
                            Ores.UploadFlag = "N";
                            Ores.ResponseData = null;
                            return Ores;
                        }
                      
                        if (CheckedStatus == true)
                        {
                            HttpContext.Current.Session["NonHomo"] = "N";
                            HttpContext.Current.Session["UploadFlag"] = "N";
                            Ores.Status = "1";
                            Ores.Msg = _vd.message;
                            Ores.UploadFlag = "N";
                            Ores.ResponseData = _vd;
                            return Ores;
                        }
                        else
                        {
                            HttpContext.Current.Session["NonHomo"] = "N";
                            HttpContext.Current.Session["UploadFlag"] = "Y";
                            Ores.Status = "1";
                            Ores.Msg = _vd.message;
                            Ores.UploadFlag = "Y";
                            Ores.ResponseData = _vd;
                            return Ores;

                        }


                    }
                    else if (_vd.message.Contains("Vehicle details available in Vahan but")) // "Vehicle Present Maker Not Present in HOMOLOGATION   and you are not authorized vendor for this vehicle" || _vd.message == "Vehicle Present and you are not authorized vendor for this vehicle")
                    {

                      
                        bool CheckedStatus = false;
                        CheckedStatus = checkVehicleForSticker(RegNumber.ToUpper(), ChassisNo.ToUpper(), EngineNo.ToUpper(), FrontLaserCode.ToUpper(), RearLaserCode.ToUpper());



                        string InsertVahanLogQuery = "insert into [BookMyHSRP].dbo.VahanResponseLog " +
                         "([VehicleRegNo] ,[ChassisNo],[EngineNo],[Fuel],[BharatState],[VehicleClass],[VehicleType],[Maker],[VahanRespose],[RegDate],[PlateSticker],[FrontLaserCode],[RearLaserCode],[VahanFrontLaserCode],[VahanRearLaserCode] ) values " +
                         "( '" + RegNumber.ToUpper() + "','" + ChassisNo.ToUpper() + "','" + EngineNo.ToUpper() + "','" + _vd.fuel + "','" + _vd.norms + "','" + _vd.vchCatg + "','" + _vd.vchType + "','" + _vd.maker + "','" + responseJson + "'," +
                         "  '" + _vd.regnDate + "','Sticker','" + FrontLaserCode.ToUpper() + "','" + RearLaserCode.ToUpper() + "','" + _vd.hsrpFrontLaserCode + "','" + _vd.hsrpRearLaserCode + "') ";
                        Utils.ExecNonQuery(InsertVahanLogQuery, ConnectionString);

                        #region Check Vahan Oem Mapping

                        string Qstr = " select Oemid,'https://bookmyhsrp.com/OEMLOGO'+REPLACE(replace(oem_logo,'.png','.jpg'),'images/brands','') as oem_logo from [hsrpoem].dbo.oemmaster  where vahanoemname='" + _vd.maker.Trim() + "' " +
                                " union " +
                               " select Oemid,'https://bookmyhsrp.com/OEMLOGO' + REPLACE(replace(oem_logo, '.png', '.jpg'), 'images/brands', '') as oem_logo from[hsrpoem].[dbo].[OEMMasterNameMapping]  where vahanoemname = '" + _vd.maker.Trim() + "' ";

                        DataTable Dt = Utils.GetDataTable(Qstr, ConnectionString);
                        if (Dt.Rows.Count > 0)
                        {


                            HttpContext.Current.Session["S_OEMImgPath"] = Dt.Rows[0]["oem_logo"].ToString();

                            HttpContext.Current.Session["S_Oemid"] = Dt.Rows[0]["Oemid"].ToString();
                        }
                        else
                        {
                            Ores.Status = "0";
                            Ores.Msg = _vd.message;
                            Ores.UploadFlag = "N";
                            Ores.ResponseData = null;
                            return Ores;
                        }

                        #endregion


                        if (CheckedStatus == true)
                        {
                            HttpContext.Current.Session["NonHomo"] = "N";
                            HttpContext.Current.Session["UploadFlag"] = "N";
                            Ores.Status = "1";
                            Ores.Msg = _vd.message;
                            Ores.UploadFlag = "N";
                            Ores.ResponseData = _vd;
                            return Ores;
                        }
                        else
                        {
                            HttpContext.Current.Session["NonHomo"] = "N";
                            HttpContext.Current.Session["UploadFlag"] = "Y";
                            Ores.Status = "1";
                            Ores.Msg = _vd.message;
                            Ores.UploadFlag = "Y";
                            Ores.ResponseData = _vd;
                            return Ores;

                        }


                    }

                    else
                    {
                        HttpContext.Current.Session["NonHomo"] = "N";
                    
                        string InsertVahanLogQuery = "insert into [BookMyHSRP].dbo.VahanResponseLog " +
                         "([VehicleRegNo] ,[ChassisNo],[EngineNo],[Fuel],[BharatState],[VehicleClass],[VehicleType],[Maker],[VahanRespose],[RegDate],[PlateSticker],[FrontLaserCode],[RearLaserCode],[VahanFrontLaserCode],[VahanRearLaserCode] ) values " +
                         "( '" + RegNumber.ToUpper() + "','" + ChassisNo.ToUpper() + "','" + EngineNo.ToUpper() + "','" + _vd.fuel + "','" + _vd.norms + "','" + _vd.vchCatg + "','" + _vd.vchType + "','" + _vd.maker + "','" + responseJson + "'," +
                         "  '" + _vd.regnDate + "','Sticker','" + FrontLaserCode.ToUpper() + "','" + RearLaserCode.ToUpper() + "','" + _vd.hsrpFrontLaserCode + "','" + _vd.hsrpRearLaserCode + "') ";
                        Utils.ExecNonQuery(InsertVahanLogQuery, ConnectionString);

                        Ores.Status = "0";
                        Ores.Msg = "Your vehicle detail didn't match with vahan service";
                        Ores.UploadFlag = "N";
                        Ores.ResponseData = null;
                        return Ores;
                    }





                }
                else
                {

                    Ores.Status = "0";
                    Ores.Msg = "Your Vehicle Data Not Pulled From Vahan Please Try After Some Time. ";
                    Ores.UploadFlag = "N";
                    Ores.ResponseData = null;
                    return Ores;

                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Ores.Status = "0";
                Ores.Msg = "Error While Calling Your Vehicle Details From Vahan Please Try After Some Time";
                Ores.ResponseData = null;
                return Ores;
            }

        }
        public static string rosmerta_API_2(string vehRegNo, string chasiNo, string EngineNo, string Key)
        {
            string html = string.Empty;


            string decryptedString = string.Empty;

            try
            {

                string url = @"" + ConfigurationManager.AppSettings["VehicleStatusAPI2"].ToString() + "?VehRegNo=" + vehRegNo + "&ChassisNo=" + chasiNo + "&EngineNo=" + EngineNo + "&X=" + Key + "";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();

                }

            }
            catch (Exception ev)
            {
                html = "Error While Calling Vahan Service - " + ev.Message;
            }


            return html;
        }


        #region Check RWA code 
        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("sticker/api/Get/CheckCodeAvailability")]
        public ResCheCkAvailability CheckCodeAvailability(string SessionValue)
        {
            ResCheCkAvailability ResAv = new ResCheCkAvailability();
            HttpContext.Current.Session["S_mapAddress1"] = "";
            //CheckSession.ClearSession(10, "plate");
            if (SessionValue.Trim() == "" || SessionValue == null)
            {
                ResAv.Status = "0";
                ResAv.DeliveryCity = "";
                ResAv.DeliveryState = "";
                ResAv.Msg = "Please Enter Code to check Availability.";

                return ResAv;
            }
            if (HttpContext.Current.Session["S_Oemid"] != null && HttpContext.Current.Session["S_StateId"] != null)
            {

                string Qstr = "Execute CheckRwaCodeAvailability '" + HttpContext.Current.Session["S_Oemid"].ToString() + "','" + HttpContext.Current.Session["S_StateId"].ToString() + "', '" + SessionValue + "'";
                DataTable dtCheckPincode = Utils.GetDataTable(Qstr, ConnectionString);

                if (dtCheckPincode.Rows.Count > 0)
                {
                    HttpContext.Current.Session["S_DealerAffixationCenterid"] = dtCheckPincode.Rows[0]["DealerAffixationID"].ToString();
                    HttpContext.Current.Session["S_SelectedSlotID"] = "1";// SlotID;
                  

                    Qstr = "select distinct CONVERT(VARCHAR(20), cast(blockDate as date), 120) blockDate from [HSRPOEM].[dbo].[HolidayDateTime] " +
                    "where cast(blockDate as date) between getdate() and cast(DATEADD(DAY, +6, GETDATE()) as date) and ([Desc] = 'Holiday' or [Desc] = 'Sunday') ";
                    DataTable dtHoliday = BMHSRPv2.Models.Utils.GetDataTable(Qstr, ConnectionString);
                   
                    HttpContext.Current.Session["S_SelectedSlotDate"] = DateTime.Today.AddDays(6+ dtHoliday.Rows.Count).ToString("yyyy-MM-dd"); // DateTime.Now.Date.ToString("yyyy-MM-dd");//   SlotDate;
                    HttpContext.Current.Session["S_SelectedSlotTime"] = "10:00AM to 06:00PM";// SlotTime;
                    HttpContext.Current.Session["S_DealerAffixationCenterAdd"] = dtCheckPincode.Rows[0]["DealerAffixationCenterAddress"].ToString();
                    //BookingDetail
                    // Response.Redirect("BookingSummary.aspx");
                    ResAv.Status = "1";
                    ResAv.Msg = "Date:" + HttpContext.Current.Session["S_SelectedSlotDate"].ToString() + " Your available Slot is between " + HttpContext.Current.Session["S_SelectedSlotTime"].ToString() + " at " + HttpContext.Current.Session["S_DealerAffixationCenterAdd"].ToString();
                    ResAv.DeliveryCity = dtCheckPincode.Rows[0]["DealerAffixationCenterCity"].ToString();
                    ResAv.DeliveryState = dtCheckPincode.Rows[0]["hsrpstate"].ToString();
                    return ResAv;

                }
                else
                {

                    ResAv.Status = "0";
                    ResAv.Msg = "Not Available";
                    ResAv.DeliveryCity = "";
                    ResAv.DeliveryState = "";
                    return ResAv;

                }
            }
            else
            {

                ResAv.Status = "0";
                ResAv.Msg = "Session Expires..";
                ResAv.DeliveryCity = "";
                ResAv.DeliveryState = "";
                return ResAv;
            }




        }
        #endregion



        public class ResCheCkAvailability
        {
            public string Status { set; get; }
            public string Msg { set; get; }

            public string DeliveryCity { set; get; }
            public string DeliveryState { set; get; }

        }


        // VehicleDetails myDeserializedClass = JsonConvert.DeserializeObject<VehicleDetails>(myJsonResponse); 
        public class VehicleDetails
        {
            public string message { get; set; }
            public string fuel { get; set; }
            public string maker { get; set; }
            public string vchType { get; set; }
            public string norms { get; set; }
            public string vchCatg { get; set; }
            public string regnDate { get; set; }
            public string hsrpFrontLaserCode { set; get; }
            public string hsrpRearLaserCode { set; get; }
        }

        public class VahanResponse
        {
            public string Status { set; get; }
            public string Msg { set; get; }
            public string UploadFlag { set; get; }

            public VehicleDetails ResponseData { set; get; }


        }

    }
}
