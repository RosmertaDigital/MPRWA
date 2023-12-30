using BMHSRPv2.Models;
using BMHSRPv2.plate;
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
using System.Globalization;
using Newtonsoft.Json;

namespace BMHSRPv2.Controllers
{
    public class WebController : ApiController
    {
        StringBuilder sbTable = new StringBuilder();
        String Msg = string.Empty;
        String ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

        #region Booking Type (HSRP or Sticker Only) with Image path in session- Ankit Chaudhary use on page Index.aspx
        [HttpGet]
       
        [Route("api/Get/OrderType")]
        public String OrderType(string OrderType, string OrderTypeImagePath)
        {
            Msg = "";
            if(OrderType=="" || OrderType == null)
            {
                Msg = "Please Choose Booking Type";
                return Msg;
            }

            if(OrderTypeImagePath=="" || OrderTypeImagePath == null)
            {
                Msg = "Something went wrong when choosing booking type";
                return Msg;
            }

            if (OrderType == "HSRP")
            {
                HttpContext.Current.Session["OrderType_imgPath"] = OrderTypeImagePath;

                HttpContext.Current.Session["OrderType"] = OrderType;
                Msg = OrderType;
                // return Msg;
            }
            else if (OrderType == "Sticker")
            {
                HttpContext.Current.Session["S_OrderType_imgPath"] = OrderTypeImagePath;

                HttpContext.Current.Session["S_OrderType"] = OrderType;
                Msg = OrderType;
                // return Msg;
            }

            return Msg;



        }

        #endregion

        #region session[VehicleType_imgPath] Image path in session- Ankit Chaudhary use on page Vehiclemake.aspx
        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("plate/api/Get/SetSessionVehicleTypeID")]
        public String SetSessionVehicleTypeID(string CategoryId, string VehicleCategoryImgPath)
        {
            CheckSession.ClearSession(2, "plate");
            Msg = "";
            if(VehicleCategoryImgPath=="" || VehicleCategoryImgPath == null)
            {
                Msg = "Something went wrong when choosing Vehicle Category";
                return Msg;
            }

            HttpContext.Current.Session["CategoryId"] = CategoryId;
       
            HttpContext.Current.Session["VehicleType_imgPath"] = VehicleCategoryImgPath.Replace(".svg","-w.svg");
            Msg = "Success";
            return Msg;

        }


        #endregion





        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("plate/api/Get/SetOEM")]

        public String SetOEM(string OEMId, string OEMImgPath ,string nonhomo)
        {
            if(OEMId== "" || OEMId==null)
            {
                Msg = "Please Select Your Vehicle Make";
                return Msg;

            }
            else if(OEMImgPath=="" || OEMImgPath == null)
            {
                Msg = "Something went wrong when choosing Vehicle Make";
                return Msg;
            }

            HttpContext.Current.Session["OEMId"] = OEMId;

            HttpContext.Current.Session["OEMImgPath"] = OEMImgPath;
            HttpContext.Current.Session["NonHomo"] = nonhomo;
            Msg = "Success";
            return Msg;


        }



        #region Check Home Delivery by pincode needs to be updated sessions Ankit Chaudhary use on page delivery.aspx
        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("plate/api/Get/CheckAvailability")]
        public ResCheCkAvailability CheckAvailability(string SessionValue)
        {
            ResCheCkAvailability ResAv = new ResCheCkAvailability();
            HttpContext.Current.Session["mapAddress1"] = "";
            //CheckSession.ClearSession(10, "plate");
            if (SessionValue.Trim() == "" || SessionValue == null)
            {
                ResAv.Status = "0";
                ResAv.DeliveryCity = "";
                ResAv.DeliveryState = "";
                ResAv.Msg = "Please Enter Delivery Pincode";

                return ResAv;
            }
            if (HttpContext.Current.Session["Oemid"] != null && HttpContext.Current.Session["StateId"] != null)
            {
                string Qstr = "Execute CheckHomeDeliveryAvailability '" + HttpContext.Current.Session["Oemid"].ToString() + "','" + HttpContext.Current.Session["StateId"].ToString() + "', '" + SessionValue + "'";
                DataTable dtCheckPincode = Utils.GetDataTable(Qstr, ConnectionString);

                if (dtCheckPincode.Rows.Count > 0)
                {
                    HttpContext.Current.Session["DealerAffixationCenterid"] = dtCheckPincode.Rows[0]["DealerAffixationID"].ToString();
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

        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("plate/api/Get/GetDeliveryPoint")]
        public String GetDeliveryPoint(string SessionValue)
        {
            try
            {
                CheckSession.ClearSession(8, "plate");

                if (SessionValue == "" || SessionValue == null)
                {
                    Msg = "Failed";
                    return Msg;
                }

                //if (SessionValue == "Home")
                //{
                //    if (HttpContext.Current.Session["StateId"].ToString() == "31")
                //    {
                //        Msg = "Coming Soon";
                //        return Msg;
                //    }

                //}


                HttpContext.Current.Session["DeliveryPoint"] = SessionValue;
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
        [System.Web.Http.Route("plate/api/Get/DeliveryInfo")]
        public String DeliveryInfo(string Pincode,string Address1,string Address2,string city,string State,string Landmark)
        {
          
            try
            {

          
           
            if (Pincode == "" || Pincode==null)
            {
                Msg = "Please Enter Pincode";
                return Msg;
            }

           else  if (Address1 == "" || Address1 == null)
            {
                Msg = "Please Enter Address1";
                return Msg;
            }
           else  if (Address2 == "" || Address2 == null)
            {
                Msg = "Please Enter Address2";
                return Msg;
            }
           else   if (city == "" || city == null)
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
                  HttpContext.Current.Session["DeliveryPincode"] = Pincode;
          
                 HttpContext.Current.Session["DeliveryAddress1"] = Address1;

                HttpContext.Current.Session["DeliveryAddress2"] = Address2;
          
           
                HttpContext.Current.Session["Deliverycity"] = city;
            
                HttpContext.Current.Session["DeliveryState"] = State;

                HttpContext.Current.Session["DeliveryLandmark"] = Landmark == null || Landmark == "null" ? "" : Landmark;

                 Msg = "Success";

                #endregion
               
            }
            catch(Exception ex)
            {
                Msg = "Something went wrong please try after some time";
               return Msg;
            }

            return Msg;
        }

        #endregion

        [System.Web.Http.HttpGet()]
        [Route("plate/api/Get/GetVehicleClass")]
        public string GetVehicleClass(string VehicleClass,string VehicleClassImgPath)
        {
            if(VehicleClass =="" || VehicleClass == null)
            {
                Msg = "Please Select Vehicle Class";
                return Msg;

            }
            else if (VehicleClassImgPath == "" || VehicleClassImgPath == null)
            {
                Msg = "Something went wrong when choosing Vehicle Class";
                return Msg;
            }
            CheckSession.ClearSession(4, "plate");
            HttpContext.Current.Session["VehicleClass"] = VehicleClass;
            HttpContext.Current.Session["VehicleClass_imgPath"] = VehicleClassImgPath.Replace(".svg", "-w.svg"); ;

            Msg = "Success";
            return Msg;
        }
       


        #region SetDealerAffixationcenterid by ankit chaudhary use on Dealers.aspx
        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("plate/api/Get/SetDealerAffixationCenter")]
        public String SetDealerAffixationCenter(string SessionValue)
        {
            try
            {
                CheckSession.ClearSession(9, "plate");

                if (SessionValue == "" || SessionValue == null)
                {
                    Msg = "Please Select Dealer First";
                    return Msg;
                }


                HttpContext.Current.Session["DealerAffixationCenterid"] = SessionValue;
                Msg = "Success";

            }
            catch (Exception)
            {
                Msg = "Something went wrong please try after some time";
                return Msg;

            }
            return Msg;





        }

        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("plate/api/Get/Earliest_DateandSlot")]
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
                string Qstr = "execute BindEarliestDateAndSlot_temp '" + Affixationid + "' ,'" + HttpContext.Current.Session["OEMId"].ToString() + "','" + HttpContext.Current.Session["DeliveryPoint"].ToString() + "','" + HttpContext.Current.Session["StateId"].ToString() + "','" + HttpContext.Current.Session["VehicleTypeid"].ToString() + "'";
                DataTable dtDateAndSlot = Utils.GetDataTable(Qstr, ConnectionString);
                StringBuilder sbTable = new StringBuilder();

                try
                {
                    //string JSONresult = JsonConvert .SerializeObject(dtDateAndSlot);  
                    //string SQLInsertLog = "insert into BMHSRPECCapacityCheckingLog () ";
                }
                catch (Exception ev)
                {
                    string error = ev.Message;
                }

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

                            if (HttpContext.Current.Session["DeliveryPoint"].ToString() == "Dealer")
                            {
                                sqlQuery = "CheckApointmentTimeSlot '" + paramDate + "','" + HttpContext.Current.Session["VehicleTypeid"].ToString() + "','" + Affixationid + "','" + HttpContext.Current.Session["DeliveryPoint"].ToString() + "','" + HttpContext.Current.Session["StateId"].ToString() + "','Plate' "; //Session["StateId"].ToString()
                            }
                            else if (HttpContext.Current.Session["DeliveryPoint"].ToString() == "Home")
                            {
                                sqlQuery = "CheckApointmentTimeSlot_Home '" + paramDate + "','" + HttpContext.Current.Session["VehicleTypeid"].ToString() + "','" + Affixationid + "','" + HttpContext.Current.Session["DeliveryPoint"].ToString() + "','" + HttpContext.Current.Session["StateId"].ToString() + "',Plate "; //Session["StateId"].ToString()
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
        [System.Web.Http.Route("plate/api/Get/SetMapAddress")]
        public string SetMapAddress(string _mapaddress)
        {
            HttpContext.Current.Session["DeliveryAddress1"] = "";
            //CheckSession.ClearSession(11, "plate");
            string[] alladdressValue = _mapaddress.Split('@');
            Msg = "";
            //Msg = CheckAvailability(alladdressValue[4]);

            string Qstr = "Execute CheckHomeDeliveryAvailability '" + HttpContext.Current.Session["Oemid"].ToString() + "','" + HttpContext.Current.Session["StateId"].ToString() + "', '" + alladdressValue[4] + "'";
            DataTable dtCheckPincode = Utils.GetDataTable(Qstr, ConnectionString);

            if (dtCheckPincode.Rows.Count > 0)
            {
                HttpContext.Current.Session["DealerAffixationCenterid"] = dtCheckPincode.Rows[0]["DealerAffixationID"].ToString();

                Msg= "Available";
            }
            else
            {
                Msg= "Not Available";
            }
            if (Msg == "Available")
            {
                HttpContext.Current.Session["mapAddress1"] = alladdressValue[0];
                HttpContext.Current.Session["mapAddress2"] = alladdressValue[1];
                HttpContext.Current.Session["mapCity"] = alladdressValue[2];
                HttpContext.Current.Session["mapStates"] = alladdressValue[3];
                HttpContext.Current.Session["mapPincode"] = alladdressValue[4];
                HttpContext.Current.Session["mapLandmark"] = alladdressValue[5];
                HttpContext.Current.Session["DeliveryAddress1"] = "";


            }
           
            return Msg;
        }


        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("plate/api/Get/SetVehicleType")]
        public string SetVehicleType(string radiovehicletype,string VehicleTypeImage)
        {
            CheckSession.ClearSession(6, "plate");
            Msg = "";
            if(radiovehicletype.Trim()== "undefined" || radiovehicletype == null)
            {
                Msg = "Please select Vehicle Type";
                return Msg;
            }

           else if (VehicleTypeImage == null || VehicleTypeImage == "")
                {
                Msg = "Something Went Wrong While Selecting Vehicle Type";
                return Msg;
                }
            else
            {
                HttpContext.Current.Session["VehicleType_imgPath"] = VehicleTypeImage;
                string[] allVehicleType = radiovehicletype.Split('@');
                HttpContext.Current.Session["VehicleCat"] = allVehicleType[2];
                HttpContext.Current.Session["VehicleType"] = allVehicleType[1];
                HttpContext.Current.Session["VehicleTypeid"] = allVehicleType[0];
                HttpContext.Current.Session["Vehiclecategoryid"] = allVehicleType[3];
                HttpContext.Current.Session["VehicleCat"].ToString();

                Msg = "Success";
               
            }
            return Msg;
          
        }

        // Brij


        // Anand Code
        [System.Web.Http.HttpGet()]
        [Route("plate/api/Get/Setmobileno")]
        public string Setmobileno(string mobileno)
        {
            Msg = "";
            if (mobileno == "" || mobileno == null || mobileno.Length < 10)
            {
                Msg = "Please Input Mobile Number";
                return Msg;
            }

            try
            {
                string storeMobileQuery = "insert into AppointmentMobile (MobileNo) values ('" + mobileno + "') ";
                int atemp = Utils.ExecNonQuery(storeMobileQuery, ConnString.ConString());

                if (atemp > 0)
                {

                    //Literal10MessageError.Text = "<Alert><font color='green'>Thank you. You will be updated as soon as the appointment system is available.</font></Alert>";

                    Msg = "Success";
                    return Msg;
                }

            }
            catch (Exception ev)
            {

            }
            Msg = "Fail";
            return Msg;
        }


        [System.Web.Http.HttpGet()]
        [Route("plate/api/Get/Setstate")]
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
            string Qstr = "execute GetOemUrl '" + state + "','" + HttpContext.Current.Session["OEMId"].ToString() + "'";
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


            CheckSession.ClearSession(3, "plate");
            HttpContext.Current.Session["StateId"] = state;
            HttpContext.Current.Session["StateShortName"] = ShortName;
            HttpContext.Current.Session["StateName"] = statename;

            Res.Status = "1";

            Res.ResponseData = Url;
            return Res;
        }

        [System.Web.Http.HttpGet()]
        [Route("plate/api/Get/Setfueltype")]
        public string Setfueltype(string fueltype)
        {
            if(fueltype=="" || fueltype == null)
            {
                Msg = "Please Select Your Fuel Type";
                return Msg;

            }
            CheckSession.ClearSession(5, "plate");
            HttpContext.Current.Session["VehicleFuelType"] = fueltype;
            Msg = "Success";
            return Msg;
        }
        //Anannd

        //created By Tek Chand

        //[System.Web.Http.HttpGet()]
        //[System.Web.Http.Route("plate/api/Get/SetSessionBookingDetail")]
        //public String SetSessionBookingDetail(string SessionBs, string SessionRD, string SessionRN, string SessionCHN, string SessionEN, string SessionON, string SessionEID, string SessionMn, string SessionFP, string SessionBA, string SessionState, string SessionCity, string SessionGST, string RcFileName)
        //{

        //    try
        //    {
        //        CheckSession.ClearSession(7, "plate");
        //        if (SessionBs == "" || SessionBs == null)
        //        {
        //            Msg = "Please select Bharat Stage value";
        //            return Msg;
        //        }
        //        else if (SessionRD == "" || SessionRD == null)
        //        {
        //            Msg = "Please provide registration date";
        //            return Msg;
        //        }
        //        else if (SessionRN == "" || SessionRN == null)
        //        {
        //            Msg = "Please provide Registration Number";
        //            return Msg;
        //        }
        //        else if (HttpContext.Current.Session["StateId"].ToString()=="31"  && SessionRN.ToLower().StartsWith("up") == false)
        //        {
        //            Msg = "Please input Correct Registration Number of UP State";
        //            return Msg;
        //        }
        //        else if (HttpContext.Current.Session["StateId"].ToString() == "37" && SessionRN.ToLower().StartsWith("dl") == false)
        //        {
        //            Msg = "Please input Correct Registration Number of DL State";
        //            return Msg;
        //        }

        //        else if (SessionCHN == "" || SessionCHN == null)
        //        {
        //            Msg = "Please provide Valid Chassis No";
        //            return Msg;
        //        }
        //        else if (SessionEN == "" || SessionEN == null)
        //        {
        //            Msg = "Please provide Valid Engine number";
        //            return Msg;
        //        }
        //        else if (SessionON == "" || SessionON == null)
        //        {
        //            Msg = "Please enter owner name";
        //            return Msg;
        //        }
        //        else if (SessionEID == "" || SessionEID == null)
        //        {
        //            Msg = "Please enter Email Id";
        //            return Msg;
        //        }
        //        else if (SessionMn == "" || SessionMn == null)
        //        {
        //            Msg = "Please enter Mobile No";
        //            return Msg;
        //        }

        //        else if (SessionBA == "" || SessionBA == null)
        //        {
        //            Msg = "Please enter Billing Address";
        //            return Msg;
        //        }
        //        else if (SessionState == "" || SessionState == null)
        //        {
        //            Msg = "Please enter State";
        //            return Msg;
        //        }
        //        else if (SessionCity == "" || SessionCity == null)
        //        {
        //            Msg = "Please enter City";
        //            return Msg;
        //        }
        //        //else if (SessionGST == "" || SessionGST == null)
        //        //{
        //        //    Msg = "Please enter GST";
        //        //    return Msg;
        //        //}

        //        if (HttpContext.Current.Session["StateId"].ToString() == "37")
        //        {
        //            try
        //            {

        //                IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-GB", true);
        //                DateTime from_date = DateTime.ParseExact(SessionRD, "dd/MM/yyyy", theCultureInfo);
        //                DateTime to = DateTime.ParseExact("01/04/2019", "dd/MM/yyyy", theCultureInfo);
        //                string txt_total_days = ((from_date - to).TotalDays).ToString();
        //                int diffResult = int.Parse(txt_total_days.ToString());

        //                if (diffResult > 0)
        //                {
        //                    Msg = "Vehicle Registration date should only be before 01/04/2019, Format is DD/MM/YYYY";
        //                    return Msg;
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                Msg = "Check Registration date Format DD/MM/YYYY";
        //                return Msg;
        //            }
        //        }
        //        // string omid= Session
        //        #region Check Order Exists or not 
        //        string Qstr = string.Empty;

        //        //Qstr = "EXEC [dbo].[jsp_GET_EXISTING_ORDER_NUMBER] @REG_NUMBER='" + SessionRN + "',@CHASSIS_NUMBER='" + SessionCHN + "',@ENGINE_NUMBER='" + SessionEN + "'";

        //        Qstr = "select BookingHistoryID from [BookMyHSRP].dbo.Appointment_BookingHist "+
        //        "where VehicleRegNo = '" + SessionRN + "' and ChassisNo = '" + SessionCHN + "' "+
        //        "and EngineNo = '" + SessionEN + "' and OrderStatus='Success' and PlateSticker='plate' ";
        //        DataTable DL = Utils.GetDataTable(Qstr, ConnectionString);
        //        if (DL.Rows.Count > 0)
        //        {

        //            Msg = "ORDER Already Created !";
        //            return Msg;
        //        }

        //        #endregion



        //        string oemid = ConfigurationManager.AppSettings["OemID"].ToString();
        //        string[] oemarray;
        //        int flag = 0; 
        //        if (oemid.Contains(','))
        //        {
        //            oemarray = oemid.Split(',');
        //            foreach (string var in oemarray)
        //            {
        //                if (var == HttpContext.Current.Session["Oemid"].ToString())
        //                {
        //                    if(RcFileName=="" || RcFileName == null)
        //                    {
        //                        flag = 1;
        //                        Msg = "Please Upload Rc File";
        //                        return Msg;
        //                    }
        //                }
        //            }

        //        }
        //        else
        //        {
        //            if (HttpContext.Current.Session["Oemid"].ToString() == oemid)
        //            {

        //                if (RcFileName.Trim() == "" || RcFileName == null)
        //                {
        //                    flag = 1;
        //                    Msg = "Please Upload Rc File";
        //                    return Msg;
        //                }
        //            }


        //        }

        //        if (HttpContext.Current.Session["Oemid"] != null)
        //        {
        //            string APIresponse = rosmerta_API(SessionRN, SessionCHN, SessionEN, "5UwoklBqiW");

        //            try
        //            {
        //                string vahanStatus = "N";
        //                if (APIresponse.Contains("Vehicle Present Maker Present in HOMOLOGATION  and you are authorized vendor for this vehicle"))
        //                {
        //                    vahanStatus = "Y";
        //                }

        //                string InsertVahanLogQuery = " insert into [BookMyHSRP].dbo.VehiclePlateEntryLog "+
        //                 "(BharatStatge, VehicleRegNo, RegistrationDate,ChassisNo,EngineNo,OwnerName,EmailID, MobileNo, "+
        //                 "CustomerAddress,StateName,OwnerCity,GSTNO,VahanValidation,VahanResponse,Ordertype, "+
        //                 "VehicleType,VehicleClass,Created_Date,VahanDateTime, "+
        //                 "HSRP_StateID,OEMID) values " +
        //                 "('" + SessionBs + "','" + SessionRN + "','" + SessionRD + "','" + SessionCHN + "','" + SessionEN + "','" + SessionON + "','" + SessionEID + "','" + SessionMn + "',"+
        //                 "'" + SessionBA + "','" + SessionState + "','" + SessionCity + "','" + SessionGST + "','" + vahanStatus + "','" + APIresponse + "','" + HttpContext.Current.Session["OrderType"].ToString() + "'," +
        //                 "'" + HttpContext.Current.Session["VehicleType"].ToString() + "','" + HttpContext.Current.Session["VehicleClass"].ToString() + "',getdate(),getdate(),"+
        //                 "'" + HttpContext.Current.Session["StateId"].ToString() + "','" + HttpContext.Current.Session["Oemid"].ToString() + "') ";
        //                Utils.ExecNonQuery(InsertVahanLogQuery, ConnectionString);
        //            }
        //            catch (Exception ev)
        //            {

        //            }

        //            if (APIresponse.Contains("Vehicle Present Maker Present in HOMOLOGATION  and you are authorized vendor for this vehicle"))
        //            {

        //                HttpContext.Current.Session["SessionBharatStage"] = SessionBs;
        //                HttpContext.Current.Session["SessionRegDate"] = SessionRD;
        //                HttpContext.Current.Session["SessionRegNo"] = SessionRN;
        //                HttpContext.Current.Session["SessionChasisno"] = SessionCHN;
        //                HttpContext.Current.Session["SessionEngno"] = SessionEN;
        //                HttpContext.Current.Session["SessionOwnerName"] = SessionON;
        //                HttpContext.Current.Session["SessionEmailID"] = SessionEID;
        //                HttpContext.Current.Session["SessionMobileNo"] = SessionMn;
        //                HttpContext.Current.Session["SessionFilePath"] = RcFileName;
        //                HttpContext.Current.Session["SessionBillingAddress"] = SessionBA;
        //                HttpContext.Current.Session["SessionState"] = SessionState;
        //                HttpContext.Current.Session["SessionCity"] = SessionCity;
        //                HttpContext.Current.Session["SessionGST"] = SessionGST;
        //                Msg = "Success";
        //            }
        //            else
        //            {
        //                if (APIresponse.Contains("Vehicle Present Maker Present in HOMOLOGATION  and you are not authorized vendor for this vehicle"))
        //                {
        //                    if (flag==1)
        //                    {
        //                        HttpContext.Current.Session["SessionBharatStage"] = SessionBs;
        //                        HttpContext.Current.Session["SessionRegDate"] = SessionRD;
        //                        HttpContext.Current.Session["SessionRegNo"] = SessionRN;
        //                        HttpContext.Current.Session["SessionChasisno"] = SessionCHN;
        //                        HttpContext.Current.Session["SessionEngno"] = SessionEN;
        //                        HttpContext.Current.Session["SessionOwnerName"] = SessionON;
        //                        HttpContext.Current.Session["SessionEmailID"] = SessionEID;
        //                        HttpContext.Current.Session["SessionMobileNo"] = SessionMn;

        //                        HttpContext.Current.Session["SessionFilePath"] = RcFileName;
        //                        HttpContext.Current.Session["SessionBillingAddress"] = SessionBA;
        //                        HttpContext.Current.Session["SessionState"] = SessionState;
        //                        HttpContext.Current.Session["SessionCity"] = SessionCity;
        //                        HttpContext.Current.Session["SessionGST"] = SessionGST;
        //                        Msg = "Success";
        //                    }
        //                    else
        //                    {
        //                        Msg = "Kindly update details from your registration certificate.";
        //                        return Msg;

        //                    }
        //                }

        //                Msg = "Kindly update details from your registration certificate";
        //                return Msg;
        //            }

        //        }

        //        return Msg;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }


        //}
        ////End API created By Tek Chand

        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("plate/api/Get/SetSessionBookingDetail")]
        public String SetSessionBookingDetail(string SessionBs, string SessionRD, string SessionRN, string SessionCHN, string SessionEN, string SessionON, string SessionEID, string SessionMn, string SessionFP, string SessionBA, string SessionState, string SessionCity, string SessionGST, string RcFileName, string Maker, string VehicleType, string FuelType, string VehiceCat, string Stateid)
        {
            try
            {
                CheckSession.ClearSession(7, "plate");
                if (SessionBs == "" || SessionBs == null)
                {
                    Msg = "Please select Bharat Stage value";
                    return Msg;
                }
                else if (SessionRD == "" || SessionRD == null)
                {
                    Msg = "Please provide registration date";
                    return Msg;
                }
                else if (SessionRN == "" || SessionRN == null)
                {
                    Msg = "Please provide Registration Number";
                    return Msg;
                }
                //else if (HttpContext.Current.Session["StateId"].ToString() == "31" && SessionRN.ToLower().StartsWith("up") == false)
                //{
                //    Msg = "Please input Correct Registration Number of UP State";
                //    return Msg;
                //}
                //else if (HttpContext.Current.Session["StateId"].ToString() == "37" && SessionRN.ToLower().StartsWith("dl") == false)
                //{
                //    Msg = "Please input Correct Registration Number of DL State";
                //    return Msg;
                //}



                else if (SessionCHN == "" || SessionCHN == null)
                {
                    Msg = "Please provide Valid Chassis No";
                    return Msg;
                }
                else if (SessionCHN.Length < 5)
                {
                    Msg = "Please provide Valid Chassis No";
                    return Msg;
                }
                else if (SessionEN == "" || SessionEN == null)
                {
                    Msg = "Please provide Valid Engine number";
                    return Msg;
                }
                else if (SessionEN.Length < 5)
                {
                    Msg = "Please provide Valid Engine number";
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
                else if (SessionMn == "" || SessionMn == null)
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


                if (SessionMn.Length == 10)
                {

                }
                else
                {
                    Msg = "Please enter valid mobile no";
                    return Msg;

                }


                //if (true)
                //{
                if (SessionRN.ToLower().StartsWith("up") == true || SessionRN.ToLower().StartsWith("dl") == true || SessionRN.ToLower().StartsWith("hp") == true || SessionRN.ToLower().StartsWith("d") == true || SessionRN.ToLower().StartsWith("wb") == true || SessionRN.ToLower().StartsWith("mp") == true)
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
                    DateTime to = DateTime.ParseExact("25/11/2019", "dd/MM/yyyy", theCultureInfo);
                    string txt_total_days = ((from_date - to).TotalDays).ToString();
                    int diffResult = int.Parse(txt_total_days.ToString());
                    //if (HttpContext.Current.Session["StateId"].ToString() == "37")
                    //{
                        if (diffResult > 0)
                        {
                        //Msg = "Vehicle Registration date should only be before 25/11/2019, Format is DD/MM/YYYY";
                        Msg = "Vehicle owner's with vehicles manufactured after 1st April 2019, should contact their respective Automobile Dealers for HSRP affixation.";
                        return Msg;
                        }
                   //}


                }
                catch (Exception ex)
                {
                    Msg = "Check Registration date Format DD/MM/YYYY";
                    return Msg;
                }


                #region VehiclePlateEntry Log
                //string InsertVahanLogQuery = " insert into [BookMyHSRP].dbo.VehiclePlateEntryLog " +
                //        "(BharatStatge, VehicleRegNo, RegistrationDate,ChassisNo,EngineNo,OwnerName,EmailID, MobileNo, " +
                //        "CustomerAddress,StateName,OwnerCity,GSTNO,VahanValidation,VahanResponse,Ordertype, " +
                //        "VehicleType,VehicleClass,Created_Date,VahanDateTime, " +
                //        "HSRP_StateID,OEMID,NonHomologVehicle) values " +
                //        "('" + SessionBs + "','" + SessionRN + "','" + SessionRD + "','" + SessionCHN + "','" + SessionEN + "','" + SessionON + "','" + SessionEID + "','" + SessionMn + "'," +
                //        "'" + SessionBA + "','" + SessionState + "','" + SessionCity + "','" + SessionGST + "','','','" + HttpContext.Current.Session["OrderType"].ToString() + "'," +
                //        "'" + HttpContext.Current.Session["VehicleType"].ToString() + "','" + HttpContext.Current.Session["VehicleClass"].ToString() + "',getdate(),getdate()," +
                //        "'" + HttpContext.Current.Session["StateId"].ToString() + "','" + HttpContext.Current.Session["Oemid"].ToString() + "','" + HttpContext.Current.Session["NonHomo"].ToString() + "') ";
                //Utils.ExecNonQuery(InsertVahanLogQuery, ConnectionString);
                #endregion

                #endregion
                //}
                // string omid= Session
                #region Check Order Exists or not
                string Qstr = string.Empty;

                //Qstr = "EXEC [dbo].[jsp_GET_EXISTING_ORDER_NUMBER] @REG_NUMBER='" + SessionRN + "',@CHASSIS_NUMBER='" + SessionCHN + "',@ENGINE_NUMBER='" + SessionEN + "'";

                Qstr = "select BookingHistoryID from [BookMyHSRP].dbo.Appointment_BookingHist " +
                "where VehicleRegNo = '" + SessionRN + "' and right(trim(Chassisno),5) = '" + SessionCHN.Substring(SessionCHN.Length - 5) + "' " +
                "and right(trim(Engineno),5) = '" + SessionEN.Substring(SessionEN.Length - 5) + "' and OrderStatus in ('Success','Shipped','Success-Test')"; // and PlateSticker='plate' 
                DataTable DL = Utils.GetDataTable(Qstr, ConnectionString);

                //if (DL.Rows.Count == 0)
                //{
                //    //Msg = "ORDER Already Booked!";
                //    Qstr = "select HSRPRecordID from [HSRPOEM].dbo.hsrprecords where VehicleRegNo = '" + SessionRN + "' " +
                //    "and right(trim(Chassisno),5) = '" + SessionCHN.Substring(SessionCHN.Length - 5) + "' " +
                //    "and right(trim(Engineno),5) ='" + SessionEN.Substring(SessionEN.Length - 5) + "'";
                //    DL = Utils.GetDataTable(Qstr, ConnectionString);
                //}


                if (DL.Rows.Count > 0)
                {
                    //Msg = "ORDER Already Created !";
                    string InsertVahanLogQuery = " insert into [BookMyHSRP].dbo.VehiclePlateEntryLog " +
                       "(vahanbsstage, VehicleRegNo, RegistrationDate,ChassisNo,EngineNo,OwnerName,EmailID, MobileNo, " +
                       "CustomerAddress,StateName,OwnerCity,GSTNO,VahanValidation,VahanResponse,Ordertype, " +
                       "VehicleType,VehicleClass,Created_Date,VahanDateTime, " +
                       "HSRP_StateID,OEMID,NonHomologVehicle) values " +
                       "('" + SessionBs + "','" + SessionRN + "','" + SessionRD + "','" + SessionCHN + "','" + SessionEN + "','" + SessionON + "','" + SessionEID + "','" + SessionMn + "'," +
                       "'" + SessionBA + "','" + SessionState + "','" + SessionCity + "','" + SessionGST + "','','ORDER Already Created !','" + HttpContext.Current.Session["OrderType"].ToString() + "'," +
                       "'" + VehiceCat + "','" + VehicleType + "',getdate(),getdate()," +
                       "'" + HttpContext.Current.Session["StateId"].ToString() + "','" + HttpContext.Current.Session["Oemid"].ToString() + "','" + HttpContext.Current.Session["NonHomo"].ToString() + "') ";
                    Utils.ExecNonQuery(InsertVahanLogQuery, ConnectionString);

                    Msg = "Order for this registration number already exists. For any query kindly mail to online@bookmyhsrp.com";
                    return Msg;
                }
                else
                {
                    #region VehiclePlateEntry Log
                    string InsertVahanLogQuery = " insert into [BookMyHSRP].dbo.VehiclePlateEntryLog " +
                            "(vahanbsstage, VehicleRegNo, RegistrationDate,ChassisNo,EngineNo,OwnerName,EmailID, MobileNo, " +
                            "CustomerAddress,StateName,OwnerCity,GSTNO,VahanValidation,VahanResponse,Ordertype, " +
                            "VehicleType,VehicleClass,Created_Date,VahanDateTime, " +
                            "HSRP_StateID,OEMID,NonHomologVehicle) values " +
                            "('" + SessionBs + "','" + SessionRN + "','" + SessionRD + "','" + SessionCHN + "','" + SessionEN + "','" + SessionON + "','" + SessionEID + "','" + SessionMn + "'," +
                            "'" + SessionBA + "','" + SessionState + "','" + SessionCity + "','" + SessionGST + "','','','" + HttpContext.Current.Session["OrderType"].ToString() + "'," +
                            "'" + VehiceCat + "','" + VehicleType + "',getdate(),getdate()," +
                            "'" + HttpContext.Current.Session["StateId"].ToString() + "','" + HttpContext.Current.Session["Oemid"].ToString() + "','" + HttpContext.Current.Session["NonHomo"].ToString() + "') ";
                    Utils.ExecNonQuery(InsertVahanLogQuery, ConnectionString);
                    #endregion


                }

                #endregion





                #region old Code For HOMOLOGO

                string oemid = ConfigurationManager.AppSettings["OemID"].ToString();
                string[] oemarray;
                int flag = 0;

               

                #endregion

                #region New Code For HOMOLOGO
                if (HttpContext.Current.Session["NonHomo"].ToString() == "Y")
                {

                    if (RcFileName == "" || RcFileName == null)
                    {
                        flag = 0;
                        Msg = "Please Upload Rc File";
                        return Msg;
                    }
                    else
                    {
                        flag = 1;

                    }
                }
                #endregion

             


                #region New code For Session Binding
                try
                {





                    Qstr = "select HSRPHRVehicleType,VehicleTypeid,VehicleCategory from [hsrpoem].[dbo].[VahanVehicleType] where VahanVehicleType='" + VehiceCat.Trim() + "'";
                    DataTable DtVehicleSesssion = Utils.GetDataTable(Qstr, ConnectionString);
                    if (DtVehicleSesssion.Rows.Count > 0)
                    {
                        //HttpContext.Current.Session["VehicleType"] = DtVehicleSesssion.Rows[0]["HSRPHRVehicleType"].ToString();
                        string GetOemVehicleType = "GetOEMvehicleType '" + DtVehicleSesssion.Rows[0]["HSRPHRVehicleType"].ToString() + "','OB','" + VehicleType.Trim() + "','" + HttpContext.Current.Session["Oemid"].ToString() + "'";
                        DataTable DtOemVehicleType = Utils.GetDataTable(GetOemVehicleType, ConnectionString);
                        if (DtOemVehicleType.Rows.Count > 0)
                        {
                            HttpContext.Current.Session["VehicleType"] = DtOemVehicleType.Rows[0]["vehicleType"].ToString();
                            
                           
                        }
                        else
                        {
                            Msg = "Vehicle Type Details didn't match";
                            return Msg;

                        }

                        HttpContext.Current.Session["VehicleClass_imgPath"] = "www";
                        HttpContext.Current.Session["VehicleCat"] = DtVehicleSesssion.Rows[0]["VehicleCategory"].ToString();
                        HttpContext.Current.Session["VehicleTypeid"] = DtVehicleSesssion.Rows[0]["VehicleTypeid"].ToString();
                        HttpContext.Current.Session["Vehiclecategoryid"] = "3";

                    }
                    else
                    {
                        Msg = "Vehicle Details didn't match";
                        return Msg;
                    }




                    HttpContext.Current.Session["VehicleFuelType"] = FuelType;
                    HttpContext.Current.Session["VehicleClass"] = VehicleType;
                    HttpContext.Current.Session["SessionBharatStage"] = SessionBs;
                    HttpContext.Current.Session["SessionRegDate"] = SessionRD;
                    HttpContext.Current.Session["SessionRegNo"] = SessionRN;
                    HttpContext.Current.Session["SessionChasisno"] = SessionCHN;
                    HttpContext.Current.Session["SessionEngno"] = SessionEN;
                    HttpContext.Current.Session["SessionOwnerName"] = SessionON;
                    HttpContext.Current.Session["SessionEmailID"] = SessionEID;
                    HttpContext.Current.Session["SessionMobileNo"] = SessionMn;

                    HttpContext.Current.Session["SessionFilePath"] = RcFileName;
                    HttpContext.Current.Session["SessionBillingAddress"] = SessionBA;
                    HttpContext.Current.Session["SessionState"] = SessionState;
                    HttpContext.Current.Session["SessionCity"] = SessionCity;
                    HttpContext.Current.Session["SessionGST"] = SessionGST;
                    Msg = "Success";

                    //string vahanStatus = "";
                    //string APIresponse = "";

                    //string InsertVahanLogQuery = " insert into [BookMyHSRP].dbo.VehiclePlateEntryLog " +
                    // "(BharatStatge, VehicleRegNo, RegistrationDate,ChassisNo,EngineNo,OwnerName,EmailID, MobileNo, " +
                    // "CustomerAddress,StateName,OwnerCity,GSTNO,VahanValidation,VahanResponse,Ordertype, " +
                    // "VehicleType,VehicleClass,Created_Date,VahanDateTime, " +
                    // "HSRP_StateID,OEMID,NonHomologVehicle) values " +
                    // "('" + SessionBs + "','" + SessionRN + "','" + SessionRD + "','" + SessionCHN + "','" + SessionEN + "','" + SessionON + "','" + SessionEID + "','" + SessionMn + "'," +
                    // "'" + SessionBA + "','" + SessionState + "','" + SessionCity + "','" + SessionGST + "','" + vahanStatus + "','" + APIresponse + "','" + HttpContext.Current.Session["OrderType"].ToString() + "'," +
                    // "'" + HttpContext.Current.Session["VehicleType"].ToString() + "','" + HttpContext.Current.Session["VehicleClass"].ToString() + "',getdate(),getdate()," +
                    // "'" + HttpContext.Current.Session["StateId"].ToString() + "','" + HttpContext.Current.Session["Oemid"].ToString() + "','" + HttpContext.Current.Session["NonHomo"].ToString() + "') ";
                    //Utils.ExecNonQuery(InsertVahanLogQuery, ConnectionString);
                }
                catch (Exception ev)
                {

                }




                #endregion




                return Msg;

            }
            catch (Exception ex)
            {

                throw;
            }


            

        }


        [System.Web.Http.HttpGet()]
        [System.Web.Http.Route("plate/api/Get/SessionBookingDetail")]
        public String SessionBookingDetail(string SessionBs, string SessionRD, string SessionRN, string SessionCHN, string SessionEN, string SessionON, string SessionEID, string SessionMn, string SessionFP, string SessionBA, string SessionState, string SessionCity, string SessionGST, string RcFileName)
        {

            try
            {
                CheckSession.ClearSession(7, "plate");
                if (SessionBs == "" || SessionBs == null)
                {
                    Msg = "Please select Bharat Stage value";
                    return Msg;
                }
                else if (SessionRD == "" || SessionRD == null)
                {
                    Msg = "Please provide registration date";
                    return Msg;
                }
                else if (SessionRN == "" || SessionRN == null)
                {
                    Msg = "Please provide Registration Number";
                    return Msg;
                }
                //else if (HttpContext.Current.Session["StateId"].ToString() == "31" && SessionRN.ToLower().StartsWith("up") == false)
                //{
                //    Msg = "Please input Correct Registration Number of UP State";
                //    return Msg;
                //}
                //else if (HttpContext.Current.Session["StateId"].ToString() == "37" && SessionRN.ToLower().StartsWith("dl") == false)
                //{
                //    Msg = "Please input Correct Registration Number of DL State";
                //    return Msg;
                //}

                

                else if (SessionCHN == "" || SessionCHN == null)
                {
                    Msg = "Please provide Valid Chassis No";
                    return Msg;
                }
                else if (SessionCHN.Length < 5)
                {
                    Msg = "Please provide Valid Chassis No";
                    return Msg;
                }
                else if (SessionEN == "" || SessionEN == null)
                {
                    Msg = "Please provide Valid Engine number";
                    return Msg;
                }
                else if (SessionEN.Length < 5)
                {
                    Msg = "Please provide Valid Engine number";
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
                else if (SessionMn == "" || SessionMn == null)
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

                //if (true)
                //{

                if (SessionMn.Length == 10)
                {

                }
                else
                {
                    Msg = "Please enter valid mobile no";
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
                        DateTime to = DateTime.ParseExact("01/04/2019", "dd/MM/yyyy", theCultureInfo);
                        string txt_total_days = ((from_date - to).TotalDays).ToString();
                        int diffResult = int.Parse(txt_total_days.ToString());
                        //if (HttpContext.Current.Session["StateId"].ToString() == "37")
                        //{
                            if (diffResult > 0)
                            {
                            Msg = "Vehicle owner's with vehicles manufactured after 1st April 2019, should contact their respective Automobile Dealers for HSRP affixation.";
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
                //}
                // string omid= Session
                #region Check Order Exists or not
                string Qstr = string.Empty;

                //Qstr = "EXEC [dbo].[jsp_GET_EXISTING_ORDER_NUMBER] @REG_NUMBER='" + SessionRN + "',@CHASSIS_NUMBER='" + SessionCHN + "',@ENGINE_NUMBER='" + SessionEN + "'";

                Qstr = "select BookingHistoryID from [BookMyHSRP].dbo.Appointment_BookingHist " +
                "where VehicleRegNo = '" + SessionRN + "' and right(trim(Chassisno),5) = '" + SessionCHN.Substring(SessionCHN.Length - 5) + "' " +
                "and right(trim(Engineno),5) = '" + SessionEN.Substring(SessionEN.Length - 5) + "' and OrderStatus in ('Success','Shipped','Success-Test')"; // and PlateSticker='plate' 
                DataTable DL = Utils.GetDataTable(Qstr, ConnectionString);

                //if (DL.Rows.Count == 0)
                //{
                //    //Msg = "ORDER Already Booked!";
                //    Qstr = "select HSRPRecordID from [HSRPOEM].dbo.hsrprecords where VehicleRegNo = '" + SessionRN + "' " +
                //    "and right(trim(Chassisno),5) = '" + SessionCHN.Substring(SessionCHN.Length - 5) + "' " +
                //    "and right(trim(Engineno),5) ='" + SessionEN.Substring(SessionEN.Length - 5) + "'";
                //    DL = Utils.GetDataTable(Qstr, ConnectionString);
                //}
                

                if (DL.Rows.Count > 0)
                {
                    //Msg = "ORDER Already Created !";
                    string InsertVahanLogQuery = " insert into [BookMyHSRP].dbo.VehiclePlateEntryLog " +
                         "(BharatStatge, VehicleRegNo, RegistrationDate,ChassisNo,EngineNo,OwnerName,EmailID, MobileNo, " +
                         "CustomerAddress,StateName,OwnerCity,GSTNO,VahanValidation,VahanResponse,Ordertype, " +
                         "VehicleType,VehicleClass,Created_Date,VahanDateTime, " +
                         "HSRP_StateID,OEMID,NonHomologVehicle) values " +
                         "('" + SessionBs + "','" + SessionRN + "','" + SessionRD + "','" + SessionCHN + "','" + SessionEN + "','" + SessionON + "','" + SessionEID + "','" + SessionMn + "'," +
                         "'" + SessionBA + "','" + SessionState + "','" + SessionCity + "','" + SessionGST + "','','ORDER Already Created !','" + HttpContext.Current.Session["OrderType"].ToString() + "'," +
                         "'" + HttpContext.Current.Session["VehicleType"].ToString() + "','" + HttpContext.Current.Session["VehicleClass"].ToString() + "',getdate(),getdate()," +
                         "'" + HttpContext.Current.Session["StateId"].ToString() + "','" + HttpContext.Current.Session["Oemid"].ToString() + "','"+ HttpContext.Current.Session["NonHomo"].ToString()+ "') ";
                    Utils.ExecNonQuery(InsertVahanLogQuery, ConnectionString);

                    Msg = "Order for this registration number already exists. For any query kindly mail to online@bookmyhsrp.com";
                    return Msg;
                }

                #endregion



                string oemid = ConfigurationManager.AppSettings["OemID"].ToString();
                string[] oemarray;
                int flag = 0;

                if (oemid.Contains(','))
                {
                    oemarray = oemid.Split(',');
                    foreach (string var in oemarray)
                    {
                        if (var == HttpContext.Current.Session["Oemid"].ToString())
                        {
                            if (RcFileName == "" || RcFileName == null)
                            {
                                flag = 0;
                                Msg = "Please Upload Rc File";
                                return Msg;
                            }
                            else
                            {
                                flag = 1;
                            }
                        }
                    }

                }
                else
                {
                    if (HttpContext.Current.Session["Oemid"].ToString() == oemid)
                    {

                        if (RcFileName== "" || RcFileName == null)
                        {
                            flag = 0;
                            Msg = "Please Upload Rc File";
                            return Msg;
                        }
                        else
                        {
                            flag = 1;

                        }
                    }


                }

                if (HttpContext.Current.Session["Oemid"] != null)
                {
                    string APIresponse = rosmerta_API(SessionRN, SessionCHN, SessionEN, "5UwoklBqiW");

                    try
                    {
                        string vahanStatus = "N";
                        if (APIresponse.Contains("Vehicle Present Maker Present in HOMOLOGATION  and you are authorized vendor for this vehicle"))
                        {
                            vahanStatus = "Y";
                        }

                        string InsertVahanLogQuery = " insert into [BookMyHSRP].dbo.VehiclePlateEntryLog " +
                         "(BharatStatge, VehicleRegNo, RegistrationDate,ChassisNo,EngineNo,OwnerName,EmailID, MobileNo, " +
                         "CustomerAddress,StateName,OwnerCity,GSTNO,VahanValidation,VahanResponse,Ordertype, " +
                         "VehicleType,VehicleClass,Created_Date,VahanDateTime, " +
                         "HSRP_StateID,OEMID,NonHomologVehicle) values " +
                         "('" + SessionBs + "','" + SessionRN + "','" + SessionRD + "','" + SessionCHN + "','" + SessionEN + "','" + SessionON + "','" + SessionEID + "','" + SessionMn + "'," +
                         "'" + SessionBA + "','" + SessionState + "','" + SessionCity + "','" + SessionGST + "','" + vahanStatus + "','" + APIresponse + "','" + HttpContext.Current.Session["OrderType"].ToString() + "'," +
                         "'" + HttpContext.Current.Session["VehicleType"].ToString() + "','" + HttpContext.Current.Session["VehicleClass"].ToString() + "',getdate(),getdate()," +
                         "'" + HttpContext.Current.Session["StateId"].ToString() + "','" + HttpContext.Current.Session["Oemid"].ToString() + "','" + HttpContext.Current.Session["NonHomo"].ToString() + "') ";
                        Utils.ExecNonQuery(InsertVahanLogQuery, ConnectionString);
                    }
                    catch (Exception ev)
                    {

                    }

                    if (HttpContext.Current.Session["Oemid"].ToString() == "191" && APIresponse.Contains("Vehicle Present Maker Present"))
                    {
                        HttpContext.Current.Session["SessionBharatStage"] = SessionBs;
                        HttpContext.Current.Session["SessionRegDate"] = SessionRD;
                        HttpContext.Current.Session["SessionRegNo"] = SessionRN;
                        HttpContext.Current.Session["SessionChasisno"] = SessionCHN;
                        HttpContext.Current.Session["SessionEngno"] = SessionEN;
                        HttpContext.Current.Session["SessionOwnerName"] = SessionON;
                        HttpContext.Current.Session["SessionEmailID"] = SessionEID;
                        HttpContext.Current.Session["SessionMobileNo"] = SessionMn;
                        HttpContext.Current.Session["SessionFilePath"] = RcFileName;
                        HttpContext.Current.Session["SessionBillingAddress"] = SessionBA;
                        HttpContext.Current.Session["SessionState"] = SessionState;
                        HttpContext.Current.Session["SessionCity"] = SessionCity;
                        HttpContext.Current.Session["SessionGST"] = SessionGST;
                        Msg = "Success";

                    }else if (APIresponse.Contains("Vehicle Present Maker Present in HOMOLOGATION  and you are authorized vendor for this vehicle"))
                    {

                        HttpContext.Current.Session["SessionBharatStage"] = SessionBs;
                        HttpContext.Current.Session["SessionRegDate"] = SessionRD;
                        HttpContext.Current.Session["SessionRegNo"] = SessionRN;
                        HttpContext.Current.Session["SessionChasisno"] = SessionCHN;
                        HttpContext.Current.Session["SessionEngno"] = SessionEN;
                        HttpContext.Current.Session["SessionOwnerName"] = SessionON;
                        HttpContext.Current.Session["SessionEmailID"] = SessionEID;
                        HttpContext.Current.Session["SessionMobileNo"] = SessionMn;
                        HttpContext.Current.Session["SessionFilePath"] = RcFileName;
                        HttpContext.Current.Session["SessionBillingAddress"] = SessionBA;
                        HttpContext.Current.Session["SessionState"] = SessionState;
                        HttpContext.Current.Session["SessionCity"] = SessionCity;
                        HttpContext.Current.Session["SessionGST"] = SessionGST;
                        Msg = "Success";
                    }
                    else
                    {
                        //Msg = "Kindly update details from your registration certificate";
                        Msg = "Kindly update details from your registration certificate. (" + SessionRN + ")";

                     // if (APIresponse.Contains("Vehicle Present Maker Not Present in HOMOLOGATION   and you are not authorized vendor for this vehicle"))
                        if (APIresponse.Contains("Vehicle Present Maker Not Present in HOMOLOGATION   and you are not authorized vendor for this vehicle") || APIresponse.Contains("Vehicle Present Maker Present in HOMOLOGATION  and you are not authorized vendor for this vehicle"))
                        {
                            if (flag == 1)
                            {
                                HttpContext.Current.Session["SessionBharatStage"] = SessionBs;
                                HttpContext.Current.Session["SessionRegDate"] = SessionRD;
                                HttpContext.Current.Session["SessionRegNo"] = SessionRN;
                                HttpContext.Current.Session["SessionChasisno"] = SessionCHN;
                                HttpContext.Current.Session["SessionEngno"] = SessionEN;
                                HttpContext.Current.Session["SessionOwnerName"] = SessionON;
                                HttpContext.Current.Session["SessionEmailID"] = SessionEID;
                                HttpContext.Current.Session["SessionMobileNo"] = SessionMn;

                                HttpContext.Current.Session["SessionFilePath"] = RcFileName;
                                HttpContext.Current.Session["SessionBillingAddress"] = SessionBA;
                                HttpContext.Current.Session["SessionState"] = SessionState;
                                HttpContext.Current.Session["SessionCity"] = SessionCity;
                                HttpContext.Current.Session["SessionGST"] = SessionGST;
                                Msg = "Success";
                            }
                            else
                            {
                                //Msg = "Kindly update details from your registration certificate.";
                                Msg = "Kindly update details from your registration certificate. (" + SessionRN + ")";
                                return Msg;

                            }
                        }


                    }

                }

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
                html = "Error While Calling Vahan Service - " + ev.Message;
            }


            return html;
        }


        // This api is used in TrackOrder.aspx form 
        //Ashish Code

        [HttpGet()]
        [Route("api/Get/TrackOrderNo")]
        public string TrackOrderNo(string orderno, string vehicleRegno)
        {
            string jsonData = "";
            // string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string Msg = string.Empty;
                if (string.IsNullOrEmpty(orderno))
                {
                    Msg = "OrderNo Not Found";
                    return Msg;
                }
                if (string.IsNullOrEmpty(vehicleRegno))
                {
                    Msg = "VehicleRegNo Not Found";
                    return Msg;

                }
                //string Qstr = "exec CheckStickerOrderDetails '" + orderno.Trim() + "','" + vehicleRegno.Trim() + "'";
                string Qstr = " select Emailid, OrderNo as 'ORDER_NUMBER',VehicleRegNo as 'REG_NUMBER',SlotBookingDate," +
                " ChassisNo as 'CHASSIS_NUMBER',EngineNo as 'ENGINE_NUMBER',OrderStatus " +
                " from Appointment_BookingHist where VehicleRegNo = '" + vehicleRegno.Trim() + "' and OrderNo = '" + orderno.Trim() + "'";

                using (SqlCommand cmd = new SqlCommand(Qstr, con))
                {
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        var input = new
                        {

                            Emailid = sdr["Emailid"],
                            ORDER_NUMBER = sdr["ORDER_NUMBER"],
                            REG_NUMBER = sdr["REG_NUMBER"],
                            SlotBookingDate = sdr["SlotBookingDate"],
                            CHASSIS_NUMBER = sdr["CHASSIS_NUMBER"],
                            ENGINE_NUMBER = sdr["ENGINE_NUMBER"],
                            OrderStatus = sdr["OrderStatus"]
                        };
                        HttpContext.Current.Session["ORDER_NUMBER"] = input.ORDER_NUMBER.ToString();
                        HttpContext.Current.Session["REG_NUMBER"] = input.REG_NUMBER.ToString();
                        jsonData = (new JavaScriptSerializer()).Serialize(input);
                    }


                    con.Close();
                }
            }

            return jsonData;
        }




        [HttpGet()]
        [Route("api/Get/ReceiptValidity")]
        public string ReceiptValidity(string vehicleRegno)
        {
            string jsonData = "";
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    string Msg = string.Empty;

                    if (string.IsNullOrEmpty(vehicleRegno))
                    {
                        Msg = "VehicleRegNo Not Found";
                        return Msg;
                    }

                    /*
                    string Qstr = " SELECT case when DATEDIFF(day, HSRPRecord_CreationDate, Getdate())<= 15" +
                    " then 'NotExpire' else 'Expire' end DayCount,VehicleRegNo from Appointment_BookingHist " +
                    " where VehicleRegNo = '" + vehicleRegno.Trim() + "' ";
                    */

                    string Qstr = " SELECT VehicleRegNo from Appointment_BookingHist" +
                    " where VehicleRegNo = '" + vehicleRegno.Trim() + "'";

                    using (SqlCommand cmd = new SqlCommand(Qstr, con))
                    {
                        con.Open();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            var input = new
                            {

                                REG_NUMBER = sdr["VehicleRegNo"],
                            };

                            HttpContext.Current.Session["REG_NUMBER"] = input.REG_NUMBER.ToString();
                            jsonData = (new JavaScriptSerializer()).Serialize(input);
                        }
                        con.Close();
                    }
                }
                return jsonData;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "DataNotFound";
            }
        }



        //This api is used for OrderCancel.aspx Form 
        [HttpGet()]
        [Route("api/Get/OrderNoCancel")]
        public string OrderNoCancel(string orderno, string vehicleRegno)
        {
            string jsonData = "";
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    string Msg = string.Empty;
                    // string Qstr = "exec jsp_DEV_GET_ORDER_STATUS_Test '" + orderno.Trim() + "','" + vehicleRegno.Trim() + "'";
                    if (string.IsNullOrEmpty(orderno))
                    {
                        Msg = "OrderNo Not Found";
                        return Msg;
                    }
                    if (string.IsNullOrEmpty(vehicleRegno))
                    {
                        Msg = "VehicleRegNo Not Found";
                        return Msg;
                    }

                    //string Qstr = " select top 1 HSRPRecord_CreationDate," +
                    //" case when cast(HSRPRecord_CreationDate as date) = cast(getdate() as date) then 'Y' else 'N' end isAbleToCancelled, " +
                    //" OrderNo,OrderStatus,SlotTime,SlotBookingDate,EmailID,ChassisNo," +
                    //" EngineNo,VehicleRegNo,Dealerid,OrderStatus from Appointment_BookingHist where OrderStatus='Success' and OrderNo='" + orderno.Trim() + "' and VehicleRegNo='" + vehicleRegno.Trim() + "' ";

                    string Qstr = "  select top 1 HSRPRecord_CreationDate," +
                    "  case when getdate() Between HSRPRecord_CreationDate And DATEADD(HOUR, 24, HSRPRecord_CreationDate) then 'Y' else 'N' end isAbleToCancelled, " +
                 "  OrderNo,OrderStatus,SlotTime,convert(date,SlotBookingDate)as SlotBookingDate ,EmailID,ChassisNo,Replace(Convert(varchar,SlotBookingDate,106),' ','-') as SlotBookingDateNew," +
                 " EngineNo,VehicleRegNo,Dealerid,OrderStatus from Appointment_BookingHist where OrderStatus='Success' and OrderNo='" + orderno.Trim() + "' and VehicleRegNo='" + vehicleRegno.Trim() + "' ";

                    using (SqlCommand cmd = new SqlCommand(Qstr, con))
                    {
                        con.Open();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            var input = new
                            {
                                isAbleToCancelled = sdr["isAbleToCancelled"],
                                ORDER_NUMBER = sdr["OrderNo"],
                                REG_NUMBER = sdr["VehicleRegNo"],
                                OrderStatus = sdr["OrderStatus"]
                            };
                            HttpContext.Current.Session["CANCEL_ORDER_NO"] = input.ORDER_NUMBER.ToString();
                            HttpContext.Current.Session["REG_NUMBER"] = input.REG_NUMBER.ToString();
                            jsonData = (new JavaScriptSerializer()).Serialize(input);
                        }
                        con.Close();
                    }
                }
                return jsonData;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "NotFound";
            }

        }


        //[HttpGet()]
        //[Route("api/Get/OrderDetails")]
        //public OrderResponse OrderDetails(string orderno, string vehicleRegno)
        //{
        //    OrderResponse Ores = new OrderResponse();
        //    try
        //    {
        //        if (string.IsNullOrEmpty(orderno))
        //        {
        //            Ores.Status = "0";
        //            Ores.Msg = "Please Provide OrderNo";
        //            Ores.ResponseData = null;
        //            return Ores;
        //        }
        //        if (string.IsNullOrEmpty(vehicleRegno))
        //        {
        //            Ores.Status = "0";
        //            Ores.Msg = "Please Provide Vehilce No";
        //            Ores.ResponseData = null;
        //            return Ores;
        //        }


        //        string Qstr = "Execute GET_ORDER_Details '" + orderno.Trim() + "','" + vehicleRegno.Trim() + "' ";
        //        DataTable dtOrderDetails = Utils.GetDataTable(Qstr, ConnectionString);
        //        if (dtOrderDetails.Rows.Count > 0)
        //        {
        //            if (dtOrderDetails.Columns.Count == 2)
        //            {
        //                Ores.Status = "0";
        //                Ores.Msg = dtOrderDetails.Rows[0]["Msg"].ToString();
        //                Ores.ResponseData = null;
        //                return Ores;
        //            }

        //            Ores.Status = "1";
        //            Ores.Msg = "";
        //            Ores.ResponseData = null;
        //            ResponseData RD = new ResponseData();
        //            RD.OrderNo = dtOrderDetails.Rows[0]["orderno"].ToString();
        //            HttpContext.Current.Session["OrderNo"] = dtOrderDetails.Rows[0]["orderno"].ToString();
        //            RD.VehicleRegNo = dtOrderDetails.Rows[0]["VehicleRegNo"].ToString();
        //            Ores.ResponseData = RD;
        //            HttpContext.Current.Session["OEMId"] = dtOrderDetails.Rows[0]["oemid"].ToString();
        //            HttpContext.Current.Session["DealerAffixationCenterid"] = dtOrderDetails.Rows[0]["affix_id"].ToString();
        //             HttpContext.Current.Session["SessionOwnerName"]= dtOrderDetails.Rows[0]["OwnerName"].ToString();
        //            HttpContext.Current.Session["SessionMobileNo"]= dtOrderDetails.Rows[0]["MobileNo"].ToString();
        //            HttpContext.Current.Session["SessionBillingAddress"]= dtOrderDetails.Rows[0]["Address1"].ToString();
        //            HttpContext.Current.Session["SessionEmailID"]= dtOrderDetails.Rows[0]["EmailID"].ToString();
        //            HttpContext.Current.Session["StateName"] = dtOrderDetails.Rows[0]["State"].ToString();
        //            if (dtOrderDetails.Rows[0]["BookingType"].ToString().Replace("-", "").ToUpper() == "4W")
        //            {
        //                HttpContext.Current.Session["VehicleTypeid"] = "1";
        //            }
        //            else if (dtOrderDetails.Rows[0]["BookingType"].ToString().Replace("-", "").ToUpper() == "2W")
        //            {
        //                HttpContext.Current.Session["VehicleTypeid"] = "2";
        //            }
        //            else if (dtOrderDetails.Rows[0]["BookingType"].ToString().Replace("-", "").ToUpper() == "3W")
        //            {
        //                HttpContext.Current.Session["VehicleTypeid"] = "2";
        //            }
        //            else
        //            {
        //                HttpContext.Current.Session["VehicleTypeid"] = "";
        //            }


        //            HttpContext.Current.Session["DeliveryPoint"] = dtOrderDetails.Rows[0]["AppointmentType"].ToString();
        //            HttpContext.Current.Session["StateId"] = dtOrderDetails.Rows[0]["HSRP_StateID"].ToString();


        //            return Ores;
        //        }
        //        else
        //        {
        //            Ores.Status = "0";
        //            Ores.Msg = "No Data Found";
        //            Ores.ResponseData = null;
        //            return Ores;

        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Ores.Status = "0";
        //        Ores.Msg = "No Data Found";
        //        Ores.ResponseData = null;
        //        return Ores;
        //    }

        //}

        [HttpGet()]
        [Route("api/Get/OrderDetails")]
        public OrderResponse OrderDetails(string orderno, string vehicleRegno)
        {
            OrderResponse Ores = new OrderResponse();
            try
            {
                if (string.IsNullOrEmpty(orderno))
                {
                    Ores.Status = "0";
                    Ores.Msg = "Please Provide OrderNo";
                    Ores.ResponseData = null;
                    return Ores;
                }
                if (string.IsNullOrEmpty(vehicleRegno))
                {
                    Ores.Status = "0";
                    Ores.Msg = "Please Provide Vehilce No";
                    Ores.ResponseData = null;
                    return Ores;
                }


                string Qstr = "Execute GET_ORDER_Details '" + orderno.Trim() + "','" + vehicleRegno.Trim() + "' ";
                DataTable dtOrderDetails = Utils.GetDataTable(Qstr, ConnectionString);
                if (dtOrderDetails.Rows.Count > 0)
                {
                    if (dtOrderDetails.Columns.Count == 2)
                    {
                        Ores.Status = "0";
                        Ores.Msg = dtOrderDetails.Rows[0]["Msg"].ToString();
                        Ores.ResponseData = null;
                        return Ores;
                    }

                    Ores.Status = "1";
                    Ores.Msg = "";
                    Ores.ResponseData = null;
                    ResponseData RD = new ResponseData();
                    RD.OrderNo = dtOrderDetails.Rows[0]["orderno"].ToString();
                    HttpContext.Current.Session["Re_OrderNo"] = dtOrderDetails.Rows[0]["orderno"].ToString();
                    RD.VehicleRegNo = dtOrderDetails.Rows[0]["VehicleRegNo"].ToString();
                    Ores.ResponseData = RD;
                    HttpContext.Current.Session["plateSticker"] = dtOrderDetails.Rows[0]["PlateSticker"].ToString();
                    HttpContext.Current.Session["VehicleRegNo"] = dtOrderDetails.Rows[0]["VehicleRegNo"].ToString();
                    HttpContext.Current.Session["OldAppointmentDate"] = dtOrderDetails.Rows[0]["oldDate"].ToString();
                    HttpContext.Current.Session["OldAppointmentSlot"] = dtOrderDetails.Rows[0]["SlotTime"].ToString();
                    HttpContext.Current.Session["Re_OEMId"] = dtOrderDetails.Rows[0]["oemid"].ToString();
                    HttpContext.Current.Session["Re_DealerAffixationCenterid"] = dtOrderDetails.Rows[0]["affix_id"].ToString();
                    HttpContext.Current.Session["Re_SessionOwnerName"] = dtOrderDetails.Rows[0]["OwnerName"].ToString();
                    HttpContext.Current.Session["Re_SessionMobileNo"] = dtOrderDetails.Rows[0]["MobileNo"].ToString();
                    HttpContext.Current.Session["Re_SessionBillingAddress"] = dtOrderDetails.Rows[0]["Address1"].ToString();
                    HttpContext.Current.Session["Re_SessionEmailID"] = dtOrderDetails.Rows[0]["EmailID"].ToString();
                    HttpContext.Current.Session["Re_StateName"] = dtOrderDetails.Rows[0]["State"].ToString();
                    if (dtOrderDetails.Rows[0]["BookingType"].ToString().Replace("-", "").ToUpper() == "4W")
                    {
                        HttpContext.Current.Session["Re_VehicleTypeid"] = "1";
                    }
                    else if (dtOrderDetails.Rows[0]["BookingType"].ToString().Replace("-", "").ToUpper() == "2W")
                    {
                        HttpContext.Current.Session["Re_VehicleTypeid"] = "2";
                    }
                    else if (dtOrderDetails.Rows[0]["BookingType"].ToString().Replace("-", "").ToUpper() == "3W")
                    {
                        HttpContext.Current.Session["Re_VehicleTypeid"] = "2";
                    }
                    else
                    {
                        HttpContext.Current.Session["Re_VehicleTypeid"] = "";
                    }


                    HttpContext.Current.Session["Re_DeliveryPoint"] = dtOrderDetails.Rows[0]["AppointmentType"].ToString();
                    HttpContext.Current.Session["Re_StateId"] = dtOrderDetails.Rows[0]["HSRP_StateID"].ToString();


                    return Ores;
                }
                else
                {
                    Ores.Status = "0";
                    Ores.Msg = "No Data Found";
                    Ores.ResponseData = null;
                    return Ores;

                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Ores.Status = "0";
                Ores.Msg = "No Data Found";
                Ores.ResponseData = null;
                return Ores;
            }

        }

        [HttpGet()]
        [Route("plate/api/Get/insertPincodequery")]
        public string insertPincodequery(string pincode, string mobile)
        {
            string status = string.Empty;

            string insertQuery = @"INSERT INTO [dbo].[DeliveryPincodeCheckLog] ([deliveryPincode],[vehicleRegNo],[ChassisNo] ,[EngineNo],[OwnerName],[emailid],[ownerno],[billingAddress] ,[state],[city],[deliverymobile])
     VALUES
           ('" + pincode + "','" + HttpContext.Current.Session["SessionRegNo"].ToString() + "','"
           + HttpContext.Current.Session["SessionChasisno"].ToString() + "','"
           + HttpContext.Current.Session["SessionEngno"].ToString() + "','"
           + HttpContext.Current.Session["SessionOwnerName"].ToString() + "','"
           + HttpContext.Current.Session["SessionEmailID"].ToString() + "','"
            + HttpContext.Current.Session["SessionMobileNo"].ToString() + "','"
           + HttpContext.Current.Session["SessionBillingAddress"].ToString() + "','"
            + HttpContext.Current.Session["SessionState"].ToString() + "','"
            + HttpContext.Current.Session["SessionCity"].ToString() + "','"
            + mobile + "')";

            int result = Utils.ExecNonQuery(insertQuery, ConnectionString);

            status = result.ToString();

            return status;
        }


        [HttpGet()]
        [Route("plate/api/Get/ValidateData")]
        public VahanResponse ValidateData(string RegNumber, string ChassisNo, string EngineNo, string Stateid)
        {
            HttpContext.Current.Session.Remove("Type");
            VahanResponse Ores = new VahanResponse();
            try
            {
                #region block for Validation of Data 

                if (string.IsNullOrEmpty(Stateid))
                {
                    Ores.Status = "0";
                    Ores.Msg = "Please Select  Vehicle Registration State";
                    Ores.ResponseData = null;
                    return Ores;
                }
                if (Stateid== "Select Vehicle Registration State")
                {
                    Ores.Status = "0";
                    Ores.Msg = "Please Select  Vehicle Registration State";
                    Ores.ResponseData = null;
                    return Ores;
                }
                
                if (Stateid == "37")
                { 
                    HttpContext.Current.Session["StateId"] = Stateid;
                    HttpContext.Current.Session["StateShortName"] = "Delhi";
                    HttpContext.Current.Session["StateName"] = "DL";

                }

                if (Stateid == "31")
                {
                    HttpContext.Current.Session["StateId"] = Stateid;
                    HttpContext.Current.Session["StateShortName"] = "Uttar Pradesh";
                    HttpContext.Current.Session["StateName"] = "UP";

                }
                if (Stateid == "3")
                {
                    HttpContext.Current.Session["StateId"] = Stateid;
                    HttpContext.Current.Session["StateShortName"] = "HIMACHAL PRADESH";
                    HttpContext.Current.Session["StateName"] = "HP";

                }
                if (Stateid == "32")
                {
                    HttpContext.Current.Session["StateId"] = Stateid;
                    HttpContext.Current.Session["StateShortName"] = "West Bengal";
                    HttpContext.Current.Session["StateName"] = "WB";

                }

                if (Stateid == "5")
                {
                    HttpContext.Current.Session["StateId"] = Stateid;
                    HttpContext.Current.Session["StateShortName"] = "Madhya Pradesh";
                    HttpContext.Current.Session["StateName"] = "MP";

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

                if (RegNumber.ToLower().StartsWith("up") == true || RegNumber.ToLower().StartsWith("dl") == true || RegNumber.ToLower().StartsWith("hp")== true || RegNumber.ToLower().StartsWith("d") == true || RegNumber.ToLower().StartsWith("wb") || RegNumber.ToLower().StartsWith("mp"))
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


                #endregion

                #region block for Check Order already Exists or not   
                
                string Qstr = string.Empty;

                Qstr = "select BookingHistoryID from [BookMyHSRP].dbo.Appointment_BookingHist " +
                "where VehicleRegNo = '" + RegNumber.Trim().ToUpper() + "' and right(trim(Chassisno),5) = '" + ChassisNo.Substring(ChassisNo.Length - 5) + "' " +
                "and right(trim(Engineno),5) = '" + EngineNo.Substring(EngineNo.Length - 5) + "' and OrderStatus in ('Success','Shipped','Success-Test')"; // and PlateSticker='plate' 
                DataTable DL = Utils.GetDataTable(Qstr, ConnectionString);

                if (DL.Rows.Count > 0)
                {

                    Ores.Status = "0";
                    Ores.Msg = "Order for this registration number already exists. For any query kindly mail to online@bookmyhsrp.com";
                    Ores.ResponseData = null;
                    return Ores;

                }

                #endregion

                #region Block where Calling of Vahan API and Apply Cases on Order Booking According to Response from Vahan API

                string responseJson = rosmerta_API_2(RegNumber.ToUpper().Trim(), ChassisNo.ToUpper().Trim(), EngineNo.ToUpper().Trim(), "5UwoklBqiW");

                HttpContext.Current.Session["SessionFilePath"] = "";
                VehicleDetails _vd = JsonConvert.DeserializeObject<VehicleDetails>(responseJson);

                if (_vd != null)
                {
                    HttpContext.Current.Session["NonHomo"] = "N";

                    if (_vd.message == "Vehicle details available in Vahan")
                    {
                        #region Vehicle details available in Vahan

                        HttpContext.Current.Session["NonHomo"] = "N";

                        #region Insert VahanLog in Case of Vehicle details available in Vahan with Data
                        string InsertVahanLogQuery = "insert into [BookMyHSRP].dbo.VahanResponseLog " +
                "([VehicleRegNo] ,[ChassisNo],[EngineNo],[Fuel],[BharatState],[VehicleClass],[VehicleType],[Maker],[VahanRespose],[RegDate],[PlateSticker],[VahanFrontLaserCode],[VahanRearLaserCode] ) values " +
                "( '" + RegNumber.ToUpper() + "','" + ChassisNo.ToUpper() + "','" + EngineNo.ToUpper() + "','" + _vd.fuel + "','" + _vd.norms + "','" + _vd.vchCatg + "','" + _vd.vchType + "','" + _vd.maker + "','" + responseJson + "'," +
                "  '" + _vd.regnDate + "','Plate','" + _vd.hsrpFrontLaserCode + "','" + _vd.hsrpRearLaserCode + "' ) ";
                        Utils.ExecNonQuery(InsertVahanLogQuery, ConnectionString);
                        #endregion

                        #region Check Oem Vahan Mapping in Hsrp Oem

                        //Qstr = "select Oemid,BMHvehiclecategory,Name,'https://bookmyhsrp.com/OEMLOGO'+REPLACE(replace(oem_logo,'.png','.jpg'),'images/brands','') as oem_logo from [hsrpoem].dbo.oemmaster  where vahanoemname ='" + _vd.maker.Trim() + "'";
                        Qstr = " select Oemid,'https://bookmyhsrp.com/OEMLOGO'+REPLACE(replace(oem_logo,'.png','.jpg'),'images/brands','') as oem_logo from [hsrpoem].dbo.oemmaster  where vahanoemname='" + _vd.maker.Trim() + "' " +
                                 " union " +
                                " select Oemid,'https://bookmyhsrp.com/OEMLOGO' + REPLACE(replace(oem_logo, '.png', '.jpg'), 'images/brands', '') as oem_logo from[hsrpoem].[dbo].[OEMMasterNameMapping]  where vahanoemname = '" + _vd.maker.Trim() + "' ";

                        DataTable Dt = Utils.GetDataTable(Qstr, ConnectionString);
                        if (Dt.Rows.Count > 0)
                        {
                            #region When Vahan Oem Mapping True

                            HttpContext.Current.Session["OEMImgPath"] = Dt.Rows[0]["oem_logo"].ToString();

                            HttpContext.Current.Session["Oemid"] = Dt.Rows[0]["Oemid"].ToString();
                            if (HttpContext.Current.Session["Oemid"].ToString() == "20")
                            {
                                HttpContext.Current.Session["Oemid"] = "272";
                            }

                            #endregion
                        }
                        else
                        {
                            #region Code Before Homologo Disabled
                                #region When Vahan Oem Mapping False

                                //Ores.Status = "0";
                                //Ores.Msg = _vd.message;
                                //Ores.ResponseData = null;
                                //return Ores;
                            #endregion
                            #endregion
                            #region Code After Homologo Disabled
                            HttpContext.Current.Session["NonHomo"] = "N";
                            Ores.Status = "0";
                            Ores.Msg = "As a Vendor we are not authorised for this vehicle Please visit www.siam.in for respective HSRP Maker.";
                            Ores.ResponseData = null;
                            return Ores;
                            #endregion

                        }

                        #endregion

                        Ores.Status = "1";
                        Ores.Msg = _vd.message;
                        Ores.ResponseData = _vd;
                        return Ores;

                        #endregion
                    }
                    

                    else if (_vd.message.Contains("Vehicle details available in Vahan but Maker of this vehicle Not Present in HOMOLOGATION") || _vd.message == "Vehicle details available in Vahan but OEM/Manufacturer (Homologation) of this vehicle have not authorized you for the State/RTO of this vehicle, please contact respective OEM.") // "Vehicle Present Maker Not Present in HOMOLOGATION   and you are not authorized vendor for this vehicle")
                    {



                        #region Insert Vahan Log when message contains Vehicle details available in Vahan but Maker of this vehicle Not Present in HOMOLOGATION or Message equal to Vehicle details available in Vahan but OEM/Manufacturer (Homologation) of this vehicle have not authorized you for the State/RTO of this vehicle, please contact respective OEM.") // "Vehicle Present Maker Not Present in HOMOLOGATION   and you are not authorized vendor for this vehicle

                        string InsertVahanLogQuery = "insert into [BookMyHSRP].dbo.VahanResponseLog " +
                  "([VehicleRegNo] ,[ChassisNo],[EngineNo],[Fuel],[BharatState],[VehicleClass],[VehicleType],[Maker],[VahanRespose],[RegDate],[PlateSticker],[VahanFrontLaserCode],[VahanRearLaserCode] ) values " +
                  "( '" + RegNumber.ToUpper() + "','" + ChassisNo.ToUpper() + "','" + EngineNo.ToUpper() + "','" + _vd.fuel + "','" + _vd.norms + "','" + _vd.vchCatg + "','" + _vd.vchType + "','" + _vd.maker + "','" + responseJson + "'," +
                  "  '" + _vd.regnDate + "','Plate','" + _vd.hsrpFrontLaserCode + "','" + _vd.hsrpRearLaserCode + "' ) ";
                        Utils.ExecNonQuery(InsertVahanLogQuery, ConnectionString);



                        #region Check Oem Vahan Mapping in Hsrp Oem

                        
                        Qstr = " select Oemid,'https://bookmyhsrp.com/OEMLOGO'+REPLACE(replace(oem_logo,'.png','.jpg'),'images/brands','') as oem_logo from [hsrpoem].dbo.oemmaster  where vahanoemname='" + _vd.maker.Trim() + "' " +
                                 " union " +
                                " select Oemid,'https://bookmyhsrp.com/OEMLOGO' + REPLACE(replace(oem_logo, '.png', '.jpg'), 'images/brands', '') as oem_logo from[hsrpoem].[dbo].[OEMMasterNameMapping]  where vahanoemname = '" + _vd.maker.Trim() + "' ";

                        DataTable Dt = Utils.GetDataTable(Qstr, ConnectionString);
                        if (Dt.Rows.Count > 0)
                        {
                            #region When Vahan Oem Mapping True
                            HttpContext.Current.Session["NonHomo"] = "Y";
                            HttpContext.Current.Session["OEMImgPath"] = Dt.Rows[0]["oem_logo"].ToString();

                            HttpContext.Current.Session["Oemid"] = Dt.Rows[0]["Oemid"].ToString();

                            #region Check OemRtoMapping

                            DataTable DtOemRtoMapping = Utils.GetDataTable("execute CheckOemMappedOrNot '"+ Dt.Rows[0]["Oemid"].ToString()+ "', '" + RegNumber.ToUpper().Trim()+ "'", ConnectionString);
                            if (HttpContext.Current.Session["Oemid"].ToString() == "20")
                            {
                                HttpContext.Current.Session["Oemid"] = "272";
                            }
                            if (DtOemRtoMapping.Rows.Count > 0)
                            {
                                if (DtOemRtoMapping.Rows[0]["IsOemRtoMapped"].ToString() == "Y")
                                {
                                    #region Code Before Homologo Disabled
                                        #region When Oem Rto Mapping True 
                                        //HttpContext.Current.Session["NonHomo"] = "Y";
                                        //Ores.Status = "1";
                                        //Ores.Msg = _vd.message;
                                        //Ores.ResponseData = _vd;
                                        //return Ores;
                                        #endregion
                                    #endregion

                                    #region Code After Homologo Disabled
                                    HttpContext.Current.Session["NonHomo"] = "N";
                                    Ores.Status = "0";
                                    Ores.Msg = "As a Vendor we are not authorised for this vehicle Please visit www.siam.in for respective HSRP Maker.";
                                    Ores.ResponseData = null;
                                    return Ores;
                                    #endregion
                                }
                                else
                                {
                                    #region Code Before Homologo Disabled
                                        #region When Oem Rto Mapping False
                                        //HttpContext.Current.Session["NonHomo"] = "N";
                                        //Ores.Status = "0";
                                        //Ores.Msg = DtOemRtoMapping.Rows[0]["Message"].ToString();
                                        //Ores.ResponseData = null;
                                        //return Ores;
                                    #endregion
                                    #endregion

                                    #region Code After Homologo Disabled
                                    HttpContext.Current.Session["NonHomo"] = "N";
                                    Ores.Status = "0";
                                    Ores.Msg = "As a Vendor we are not authorised for this vehicle Please visit www.siam.in for respective HSRP Maker.";
                                    Ores.ResponseData = null;
                                    return Ores;
                                    #endregion
                                }


                            }
                            else
                            {
                                #region When Oem Rto Mapping False
                                HttpContext.Current.Session["NonHomo"] = "N";
                                Ores.Status = "0";
                                Ores.Msg = "As a Vendor we are not authorised for this vehicle Please visit www.siam.in for respective HSRP Maker.";
                                Ores.ResponseData = null;
                                return Ores;
                                #endregion

                            }

                            #endregion

                            #endregion
                        }
                        else
                        {
                            #region Code Before Homologo Disabled
                            #region When Vahan Oem Mapping False
                            //HttpContext.Current.Session["NonHomo"] = "N";
                            //Ores.Status = "0";
                            //Ores.Msg = _vd.message;
                            //Ores.ResponseData = null;
                            //return Ores;
                            #endregion
                            #endregion

                            #region Code After Homologo Disabled
                            HttpContext.Current.Session["NonHomo"] = "N";
                            Ores.Status = "0";
                            Ores.Msg = "As a Vendor we are not authorised for this vehicle Please visit www.siam.in for respective HSRP Maker.";
                            Ores.ResponseData = null;
                            return Ores;
                            #endregion

                        }

                        #endregion


                       
                        #endregion
                    }

                    //else if (_vd.message == "Vehicle details available in Vahan but OEM/Manufacturer (Homologation) of this vehicle have not authorized you for the State/RTO of this vehicle, please contact respective OEM.")
                    //{
                    //    #region block when message is Vehicle details available in Vahan but OEM/Manufacturer (Homologation) of this vehicle have not authorized you for the State/RTO of this vehicle, please contact respective OEM.

                    //    Ores.Status = "0";
                    //    Ores.Msg = "As a Vendor we are not authorised for this vehicle Please visit www.siam.in for respective HSRP Maker.";
                    //    Ores.ResponseData = null;
                    //    return Ores;

                    //    #endregion

                    //}


                         #region Commented Because of New Vahan Api Response
                    //else if (_vd.message == "Vehicle Present and you are not authorized vendor for this vehicle")
                    //{
                    //    HttpContext.Current.Session["NonHomo"] = "N";

                    //    string InsertVahanLogQuery = "insert into [BookMyHSRP].dbo.VahanResponseLog " +
                    //    "([VehicleRegNo] ,[ChassisNo],[EngineNo],[Fuel],[BharatState],[VehicleClass],[VehicleType],[Maker],[VahanRespose],[RegDate],[PlateSticker] ) values " +
                    //    "( '" + RegNumber.ToUpper() + "','" + ChassisNo.ToUpper() + "','" + EngineNo.ToUpper() + "','" + _vd.fuel + "','" + _vd.norms + "','" + _vd.vchCatg + "','" + _vd.vchType + "','" + _vd.maker + "','" + responseJson + "'," +
                    //    "  '" + _vd.regnDate + "','Plate' ) ";
                    //    Utils.ExecNonQuery(InsertVahanLogQuery, ConnectionString);

                    //    Ores.Status = "0";
                    //    Ores.Msg = "As a Vendor we are not authorised for this vehicle Please visit www.siam.in for respective HSRP Maker.";
                    //    Ores.ResponseData = null;
                    //    return Ores;
                    //}
                    #endregion
                    else
                    {
                        #region Case When Message Contains Other than Above cases

                        HttpContext.Current.Session["NonHomo"] = "N";
                       
                        string InsertVahanLogQuery = "insert into [BookMyHSRP].dbo.VahanResponseLog " +
                   "([VehicleRegNo] ,[ChassisNo],[EngineNo],[Fuel],[BharatState],[VehicleClass],[VehicleType],[Maker],[VahanRespose],[RegDate],[PlateSticker],[VahanFrontLaserCode],[VahanRearLaserCode] ) values " +
                   "( '" + RegNumber.ToUpper() + "','" + ChassisNo.ToUpper() + "','" + EngineNo.ToUpper() + "','" + _vd.fuel + "','" + _vd.norms + "','" + _vd.vchCatg + "','" + _vd.vchType + "','" + _vd.maker + "','" + responseJson + "'," +
                   "  '" + _vd.regnDate + "','Plate','" + _vd.hsrpFrontLaserCode + "','" + _vd.hsrpRearLaserCode + "' ) ";
                        Utils.ExecNonQuery(InsertVahanLogQuery, ConnectionString);

                        Ores.Status = "0";
                        Ores.Msg = "Your vehicle detail didn't match with vahan service";
                        Ores.ResponseData = null;
                        return Ores;

                        #endregion

                    }





                }
                else
                {
                    #region Case When Data Not Coming from Vahan Service

                    Ores.Status = "0";
                    Ores.Msg = "Your Vehicle Data Not Pulled From Vahan Please Try After Some Time. ";
                    Ores.ResponseData = null;
                    return Ores;

                    #endregion

                }

                #endregion

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
        [System.Web.Http.Route("plate/api/Get/CheckCodeAvailability")]
        public ResCheCkAvailability CheckCodeAvailability(string SessionValue)
        {
            ResCheCkAvailability ResAv = new ResCheCkAvailability();
            HttpContext.Current.Session["mapAddress1"] = "";
            //CheckSession.ClearSession(10, "plate");
            if (SessionValue.Trim() == "" || SessionValue == null)
            {
                ResAv.Status = "0";
                ResAv.DeliveryCity = "";
                ResAv.DeliveryState = "";
                ResAv.Msg = "Please Enter Code to check Availability.";

                return ResAv;
            }
            if (HttpContext.Current.Session["Oemid"] != null && HttpContext.Current.Session["StateId"] != null)
            {

                string Qstr = "Execute CheckRwaCodeAvailability '" + HttpContext.Current.Session["Oemid"].ToString() + "','" + HttpContext.Current.Session["StateId"].ToString() + "', '" + SessionValue + "'";
                DataTable dtCheckPincode = Utils.GetDataTable(Qstr, ConnectionString);

                if (dtCheckPincode.Rows.Count > 0)
                {
                    HttpContext.Current.Session["DealerAffixationCenterid"] = dtCheckPincode.Rows[0]["DealerAffixationID"].ToString();
                    HttpContext.Current.Session["SelectedSlotID"] = "1";// SlotID;
                    Qstr = "select distinct CONVERT(VARCHAR(20), cast(blockDate as date), 120) blockDate from [HSRPOEM].[dbo].[HolidayDateTime] " +
                   "where cast(blockDate as date) between getdate() and cast(DATEADD(DAY, +6, GETDATE()) as date) and ([Desc] = 'Holiday' or [Desc] = 'Sunday') ";
                    DataTable dtHoliday = BMHSRPv2.Models.Utils.GetDataTable(Qstr, ConnectionString);
                    HttpContext.Current.Session["SelectedSlotDate"] = DateTime.Now.ToString("yyyy-MM-dd");//DateTime.Today.AddDays(6+ dtHoliday.Rows.Count).ToString("yyyy-MM-dd"); // DateTime.Now.Date.ToString("yyyy-MM-dd");//   SlotDate;
                    HttpContext.Current.Session["SelectedSlotTime"] = "10:00AM to 06:00PM";// SlotTime;
                    HttpContext.Current.Session["DealerAffixationCenterAdd"] = dtCheckPincode.Rows[0]["DealerAffixationCenterAddress"].ToString();
                    //BookingDetail
                    // Response.Redirect("BookingSummary.aspx");
                    ResAv.Status = "1";
                    ResAv.Msg = "Date:" + HttpContext.Current.Session["SelectedSlotDate"].ToString() + " Your available Slot is between " + HttpContext.Current.Session["SelectedSlotTime"].ToString() + " at " + HttpContext.Current.Session["DealerAffixationCenterAdd"].ToString();
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

        public class Response {
       public string Status { set; get; }
       public string Msg { set; get; }

       public string ResponseData { set; get; }

        }

        public class ResCheCkAvailability
        {
            public string Status { set; get; }
            public string Msg { set; get; }

            public string DeliveryCity { set; get; }
            public string DeliveryState { set; get; }

        }
        public class OrderResponse
        {
            public string Status { set; get; }
            public string Msg { set; get; }

            public ResponseData ResponseData { set; get; }


        }

        public class ResponseData

        {
            public string OrderNo { set; get; }
            public string VehicleRegNo { set; get; }

        }

        public class VahanResponse
        {
            public string Status { set; get; }
            public string Msg { set; get; }

            public VehicleDetails ResponseData { set; get; }


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

    }
}
