using BMHSRPv2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2.plate
{
    public partial class BulkUpload : System.Web.UI.Page
    {
        string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        string HSRPOEMConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HSRPOEMConnectionString"].ToString();

        public static DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            div2.Visible = false;
            btnValidateVahan.Visible = false;
            btnPayNow.Visible = false;

            step2.Visible = false;
            step3.Visible = false;
            //btnValidateVahan.Enabled = false;
            //btnPayNow.Enabled = false;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            dt.Rows.Clear();
            dt.Columns.Clear();
            grdUploadFiles.DataSource = null;
            if (flUpload.PostedFile.ContentLength == 0)
            {
                lblMsg.Text = "Please Select File";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                div2.Visible = true;
                return;
            }
            string FileName = Path.GetFileName(flUpload.PostedFile.FileName);
            string Extension = Path.GetExtension(flUpload.PostedFile.FileName);

            if (!Directory.Exists(Server.MapPath("files")))
            {
                Directory.CreateDirectory(Server.MapPath("files"));
            }

            string FilePath = Server.MapPath("files\\" + FileName);
            flUpload.SaveAs(FilePath);

            Import_To_Grid(FilePath, Extension, "Yes");
            File.Delete(Server.MapPath("files\\" + FileName));

            if (dt.Rows.Count > 0)
            {
                div2.Visible = true;
                btnValidateVahan.Visible = true;
                step2.Visible = true;
            }
            else
            {
                div2.Visible = true;
                grdUploadFiles.Visible = false;
            }
        }


        private void Import_To_Grid(string FilePath, string Extension, string isHDR)
        {
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03  
                    conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties = 'Excel 8.0;HDR={1}'";
                    break;

                case ".xlsx": //Excel 07  
                    conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties = 'Excel 8.0;HDR={1}'";
                    break;
            }

            conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            //DataTable dt = new DataTable();
            dt.Clear();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet  
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet  
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            //dt.Columns.Add("VehicleType", typeof(string));
            dt.Columns.Add("VehicleClass", typeof(string));
            //dt.Columns.Add("BharatStage", typeof(string));
            dt.Columns.Add("Remarks", typeof(string));
            dt.Columns.Add("OemId", typeof(int));
            dt.Columns.Add("fuel", typeof(string));
            dt.Columns.Add("StateId", typeof(string));
            dt.Columns.Add("StateShortName", typeof(string));
            dt.Columns.Add("StateName", typeof(string));

            dt.Columns.Add("GstBasic_Amt", typeof(string));
            dt.Columns.Add("FittmentCharges", typeof(string));
            dt.Columns.Add("BMHConvenienceCharges", typeof(string));
            dt.Columns.Add("BMHHomeCharges", typeof(string));
            dt.Columns.Add("GrossTotal", typeof(string));
            dt.Columns.Add("GSTAmount", typeof(string));
            dt.Columns.Add("IGSTAmount", typeof(string));
            dt.Columns.Add("CGSTAmount", typeof(string));
            dt.Columns.Add("SGSTAmount", typeof(string));
            dt.Columns.Add("GstRate", typeof(string));
            dt.Columns.Add("TotalAmount", typeof(string));
            dt.Columns.Add("VehicleCat", typeof(string));

            //btnValidateVahan.Enabled = true;
            //btnPayNow.Enabled = true;

            //GridView1.Caption = Path.GetFileName(FilePath);
            grdUploadFiles.DataSource = dt;
            grdUploadFiles.DataBind();

        }

        protected void btnValidateVahan_Click(object sender, EventArgs e)
        {
            decimal _GstBasic_Amt = 0;
            decimal _FittmentCharges = 0;
            decimal _BMHConvenienceCharges = 0;
            decimal _BMHHomeCharges = 0;
            decimal _GrossTotal = 0;
            decimal _GSTAmount = 0;
            decimal _IGSTAmount = 0;
            decimal _CGSTAmount = 0;
            decimal _SGSTAmount = 0;
            decimal _GstRate = 0;
            decimal _TotalAmount = 0;
            string OrderNo = "BMHSRPGRP" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + GetRandomNumber();
            Session["GroupOrderNo"] = OrderNo;
            string[] _rwaStateId = ConfigurationManager.AppSettings["RwaStateId"].ToString().Split(',');

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow[] myResultSet = dt.Select("[VehicleRegNo] ='" + dt.Rows[i]["VehicleRegNo"].ToString() + "' and [ChassisNo] ='" + dt.Rows[i]["ChassisNo"].ToString() + "' and [EngineNo] ='" + dt.Rows[i]["EngineNo"].ToString() + "'");
                if (myResultSet.Length == 1)
                {

                    string ss = dt.Rows[i]["VehicleRegNo"].ToString();
                    string VehicleRegNo = dt.Rows[i]["VehicleRegNo"].ToString();
                    string ChassisNo = dt.Rows[i]["ChassisNo"].ToString().PadLeft(5, '0');
                    dt.Rows[i]["ChassisNo"] = ChassisNo;
                    string EngineNo = dt.Rows[i]["EngineNo"].ToString().PadLeft(5, '0');
                    dt.Rows[i]["EngineNo"] = EngineNo;
                    string responseJson = rosmerta_API_2(VehicleRegNo.ToUpper().Trim(), ChassisNo.ToUpper().Trim(), EngineNo.ToUpper().Trim(), "5UwoklBqiW");
                    VehicleDetails _vd = JsonConvert.DeserializeObject<VehicleDetails>(responseJson);
                    if (_vd != null)
                    {
                        if (_vd.message == "Vehicle details available in Vahan")
                        {
                            string query = "select top 1 1 from [bookmyhsrp].dbo.Appointment_BookingHist where VehicleRegNo='" + dt.Rows[i]["VehicleRegNo"].ToString() + "' and ChassisNo='" + dt.Rows[i]["ChassisNo"].ToString() + "' and EngineNo='" + dt.Rows[i]["EngineNo"].ToString() + "' and OrderStatus='Success' and OrderType='OB'";
                            DataTable dtExists = Utils.GetDataTable(query, ConnectionString);
                            if (dtExists.Rows.Count == 0)
                            {
                                //dt.Rows[i]["VehicleType"] = _vd.vchCatg;
                                dt.Rows[i]["VehicleClass"] = _vd.vchType;
                                if (_vd.norms != "Not Available")
                                {
                                    dt.Rows[i]["BharatStage"] = _vd.norms;
                                }
                                dt.Rows[i]["Remarks"] = "Success";
                                dt.Rows[i]["fuel"] = _vd.fuel;

                                string Qstr = " select Oemid,'https://bookmyhsrp.com/OEMLOGO'+REPLACE(replace(oem_logo,'.png','.jpg'),'images/brands','') as oem_logo, name  as oemname from [hsrpoem].dbo.oemmaster  where vahanoemname='" + _vd.maker.Trim() + "' " +
                                         " union " +
                                        " select Oemid,'https://bookmyhsrp.com/OEMLOGO' + REPLACE(replace(oem_logo, '.png', '.jpg'), 'images/brands', '') as oem_logo, vahanoemname as oemname from [hsrpoem].[dbo].[OEMMasterNameMapping]  where vahanoemname = '" + _vd.maker.Trim() + "' ";

                                DataTable dtOem = Utils.GetDataTable(Qstr, ConnectionString);
                                if (dtOem.Rows.Count > 0)
                                {
                                    #region When Vahan Oem Mapping True

                                    string _OemId = dtOem.Rows[0]["Oemid"].ToString();
                                    if (_OemId == "20")
                                    {
                                        _OemId = "272";
                                    }
                                    dt.Rows[i]["OemId"] = _OemId;

                                    string strPasscodeOemId = "select OemID from [HSRPOEM].dbo.DealerAffixationCenter where DailyPassCode='" + Session["Passcode"] + "'";
                                    DataTable dtPassCodeOem = Utils.GetDataTable(strPasscodeOemId, ConnectionString);
                                    if (dtPassCodeOem.Rows.Count > 0 && _OemId == dtPassCodeOem.Rows[0]["OemID"].ToString())
                                    {
                                        string VehicleCat = string.Empty;
                                        string QstrState = "select HSRP_StateID,HSRPStateName,StateCode as HSRPStateShortName from BMHSRPStates where HSRPStateShortName='" + VehicleRegNo.Substring(0, 2) + "'";
                                        DataTable dtState = Utils.GetDataTable(QstrState, HSRPOEMConnectionString);
                                        if (dtState.Rows.Count > 0)
                                        {
                                            dt.Rows[i]["StateId"] = dtState.Rows[0]["HSRP_StateID"].ToString();
                                            dt.Rows[i]["StateShortName"] = dtState.Rows[0]["HSRPStateShortName"].ToString();
                                            dt.Rows[i]["StateName"] = dtState.Rows[0]["HSRPStateName"].ToString();
                                        }

                                        if (_rwaStateId.Contains(dt.Rows[i]["StateId"]) || (_rwaStateId[0] == ""))
                                        {

                                            Qstr = "select HSRPHRVehicleType,VehicleTypeid,VehicleCategory from [hsrpoem].[dbo].[VahanVehicleType] where VahanVehicleType='" + _vd.vchCatg + "'";
                                            DataTable DtVehicleSesssion = Utils.GetDataTable(Qstr, ConnectionString);
                                            if (DtVehicleSesssion.Rows.Count > 0)
                                            {
                                                string GetOemVehicleType = "GetOEMvehicleType '" + DtVehicleSesssion.Rows[0]["HSRPHRVehicleType"].ToString() + "','OB','" + _vd.vchCatg + "','" + _OemId + "'";
                                                DataTable DtOemVehicleType = Utils.GetDataTable(GetOemVehicleType, ConnectionString);
                                                VehicleCat = DtVehicleSesssion.Rows[0]["VehicleCategory"].ToString();
                                            }

                                            decimal GstBasic_Amt;
                                            decimal FittmentCharges;
                                            decimal BMHConvenienceCharges;
                                            decimal BMHHomeCharges;
                                            decimal GrossTotal;
                                            decimal GSTAmount;
                                            decimal IGSTAmount;
                                            decimal CGSTAmount;
                                            decimal SGSTAmount;
                                            decimal GstRate;
                                            decimal TotalAmount;

                                            try
                                            {
                                                string[] vahandateArr = _vd.regnDate.Split('-');
                                                string vahandate = vahandateArr[2] + "/" + vahandateArr[1] + "/" + vahandateArr[0];

                                                //DateTime dtTodate = DateTime.ParseExact(_vd.regnDate, "dd/mm/yyyy", null);
                                                //var dtToDateValue = dtTodate.ToString("dd/MM/yyyy");
                                                //string dateValue = "07/08/2015"; //_vd.regnDate.Replace("-", "/");
                                                IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-GB", true);
                                                DateTime from_date = DateTime.ParseExact(vahandate, "dd/MM/yyyy", theCultureInfo);
                                                DateTime to = DateTime.ParseExact("25/11/2019", "dd/MM/yyyy", theCultureInfo);
                                                string txt_total_days = ((from_date - to).TotalDays).ToString();
                                                int diffResult = int.Parse(txt_total_days.ToString());
                                                //if (HttpContext.Current.Session["StateId"].ToString() == "37")
                                                //{
                                                if (diffResult > 0)
                                                {
                                                    //Msg = "Vehicle Registration date should only be before 25/11/2019, Format is DD/MM/YYYY";
                                                    dt.Rows[i]["Remarks"] = "Reg Date must be before 1st April 2019";
                                                }
                                                else
                                                {
                                                    //dt.Rows[i]["VehicleType"]
                                                    //string CheckOemRateQuery = "CheckOrdersRates '" + _OemId + "', 'OB','" + _vd.vchType + "', '" + _vd.vchCatg + "','3','" + _vd.fuel + "','rwa','" + dtState.Rows[0]["HSRP_StateID"].ToString() + "','" + dtState.Rows[0]["HSRPStateName"].ToString() + "'";
                                                    string CheckOemRateQuery = "CheckOrdersRates '" + _OemId + "', 'OB','" + _vd.vchType + "', '" + dt.Rows[i]["VehicleType"] + "','3','" + _vd.fuel + "','rwa','" + dtState.Rows[0]["HSRP_StateID"].ToString() + "','" + dtState.Rows[0]["HSRPStateName"].ToString() + "'";
                                                    DataTable dtOemRate = BMHSRPv2.Models.Utils.GetDataTable(CheckOemRateQuery, ConnectionString);
                                                    if (dtOemRate.Rows.Count > 0 && dtOemRate.Rows[0]["status"].ToString() != "0")
                                                    {
                                                        GstBasic_Amt = Convert.ToDecimal(dtOemRate.Rows[0]["GstBasic_Amt"]) + Convert.ToDecimal(dtOemRate.Rows[0]["FittmentCharges"]);
                                                        FittmentCharges = Convert.ToDecimal(dtOemRate.Rows[0]["FittmentCharges"]);
                                                        BMHConvenienceCharges = Convert.ToDecimal(dtOemRate.Rows[0]["BMHConvenienceCharges"]);
                                                        BMHHomeCharges = Convert.ToDecimal(dtOemRate.Rows[0]["BMHHomeCharges"]);
                                                        GrossTotal = Convert.ToDecimal(dtOemRate.Rows[0]["GrossTotal"]);
                                                        GSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["GSTAmount"]);
                                                        IGSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["IGSTAmount"]);
                                                        CGSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["CGSTAmount"]);
                                                        SGSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["SGSTAmount"]);
                                                        GstRate = Convert.ToDecimal(dtOemRate.Rows[0]["gst"]);
                                                        TotalAmount = Convert.ToDecimal(dtOemRate.Rows[0]["TotalAmount"]);

                                                        dt.Rows[i]["GstBasic_Amt"] = GstBasic_Amt;
                                                        dt.Rows[i]["FittmentCharges"] = FittmentCharges;
                                                        dt.Rows[i]["BMHConvenienceCharges"] = BMHConvenienceCharges;
                                                        dt.Rows[i]["BMHHomeCharges"] = BMHHomeCharges;
                                                        dt.Rows[i]["GrossTotal"] = GrossTotal;
                                                        dt.Rows[i]["GSTAmount"] = GSTAmount;
                                                        dt.Rows[i]["IGSTAmount"] = IGSTAmount;
                                                        dt.Rows[i]["CGSTAmount"] = CGSTAmount;
                                                        dt.Rows[i]["SGSTAmount"] = SGSTAmount;
                                                        dt.Rows[i]["GstRate"] = GstRate;
                                                        dt.Rows[i]["TotalAmount"] = TotalAmount;
                                                        dt.Rows[i]["fuel"] = _vd.fuel;
                                                        dt.Rows[i]["VehicleCat"] = VehicleCat;

                                                        _GstBasic_Amt = _GstBasic_Amt + GstBasic_Amt;
                                                        _FittmentCharges = _FittmentCharges + FittmentCharges;
                                                        _BMHConvenienceCharges = _BMHConvenienceCharges + BMHConvenienceCharges;
                                                        _BMHHomeCharges = _BMHHomeCharges + BMHHomeCharges;
                                                        _GrossTotal = _GrossTotal + GrossTotal;
                                                        _GSTAmount = _GSTAmount + GSTAmount;
                                                        _IGSTAmount = _IGSTAmount + IGSTAmount;
                                                        _CGSTAmount = _CGSTAmount + CGSTAmount;
                                                        _SGSTAmount = _SGSTAmount + SGSTAmount;
                                                        _GstRate = _GstRate + GstRate;
                                                        _TotalAmount = _TotalAmount + TotalAmount;

                                                        string RegDate = _vd.regnDate.Substring(8, 2) + "/" + _vd.regnDate.Substring(5, 2) + "/" + _vd.regnDate.Substring(0, 4);



                                                        string PaymentInitiatedQuery = "usp_Appointment_BookingHistStagingInsert '" + dt.Rows[i]["VehicleRegNo"] + "','" + dt.Rows[i]["ChassisNo"] + "','" + dt.Rows[i]["EngineNo"] + "','" + dt.Rows[i]["VehicleClass"] + "','" + dt.Rows[i]["VehicleType"] + "','" + dt.Rows[i]["BharatStage"] + "','" + dt.Rows[i]["OemId"] + "','OB','rwa','" + dt.Rows[i]["StateId"] + "','" + dt.Rows[i]["StateName"] + "','" + dt.Rows[i]["GstBasic_Amt"] + "','" + dt.Rows[i]["FittmentCharges"] + "','" + dt.Rows[i]["BMHConvenienceCharges"] + "','" + dt.Rows[i]["BMHHomeCharges"] + "','" + dt.Rows[i]["GrossTotal"] + "','" + dt.Rows[i]["GSTAmount"] + "','" + dt.Rows[i]["IGSTAmount"] + "','" + dt.Rows[i]["CGSTAmount"] + "','" + dt.Rows[i]["SGSTAmount"] + "','" + dt.Rows[i]["GstRate"] + "','" + dt.Rows[i]["TotalAmount"] + "','" + OrderNo + "','" + dt.Rows[i]["BillingAddress"] + "','" + dt.Rows[i]["MobileNo"] + "','" + dt.Rows[i]["EmailId"] + "','" + dtOem.Rows[0]["oemname"] + "','" + RegDate + "','" + dtOemRate.Rows[0]["FrontPlateSize"] + "','" + dtOemRate.Rows[0]["RearPlateSize"] + "','" + _vd.fuel + "','" + dt.Rows[i]["VehicleCat"] + "','" + dt.Rows[i]["OwnerName"] + "'";
                                                        DataTable dtPaymentConfirmation = BMHSRPv2.Models.Utils.GetDataTable(PaymentInitiatedQuery, ConnectionString);
                                                        if (dtPaymentConfirmation.Rows.Count > 0)
                                                        {
                                                            string Status = dtPaymentConfirmation.Rows[0]["status"].ToString();
                                                            //OrderNo = dtPaymentConfirmation.Rows[0]["OrderNo"].ToString();
                                                            if (Status == "1")
                                                            {

                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        dt.Rows[i]["Remarks"] = "Rate Not Proper";
                                                    }

                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                //dt.Rows[i]["Remarks"] = ex.Message.ToString();
                                                //dt.Rows[i]["Remarks"] = "Check Registration date Format DD/MM/YYYY";
                                                dt.Rows[i]["Remarks"] = "Error in Date Time Format";

                                            }
                                        }
                                        else
                                        {
                                            dt.Rows[i]["Remarks"] = "Booking not Allowed";
                                        }


                                        #endregion
                                    }
                                    else
                                    {
                                        dt.Rows[i]["Remarks"] = "Invalid OEM";
                                    }
                                }
                                else
                                {
                                    dt.Rows[i]["Remarks"] = "OEM Not Exist";
                                }
                            }
                            else
                            {
                                dt.Rows[i]["Remarks"] = "Already Exist";
                            }
                        }
                        else
                        {
                            dt.Rows[i]["Remarks"] = "Not Verified from Vahan";
                        }
                    }

                }
                else
                {
                    dt.Rows[i]["Remarks"] = "Duplicate Rows";
                }
            }

            Session["_GstBasic_Amt"] = _GstBasic_Amt;
            Session["_FittmentCharges"] = _FittmentCharges;
            Session["_BMHConvenienceCharges"] = _BMHConvenienceCharges;
            Session["_BMHHomeCharges"] = _BMHHomeCharges;
            Session["_GrossTotal"] = _GrossTotal;
            Session["_GSTAmount"] = _GSTAmount;
            Session["_IGSTAmount"] = _IGSTAmount;
            Session["_CGSTAmount"] = _CGSTAmount;
            Session["_SGSTAmount"] = _SGSTAmount;
            Session["_GstRate"] = _GstRate;
            Session["_TotalAmount"] = _TotalAmount;
            Session["Type"] = "Bulk";
            Session["GroupOrderNo"] = OrderNo;
            Session["DeliveryPoint"] = "rwa";

            grdUploadFiles.DataSource = dt;
            grdUploadFiles.DataBind();

            div2.Visible = true;
            btnUpload.Visible = true;
            btnValidateVahan.Visible = true;
            btnPayNow.Visible = true;

            step2.Visible = true;
            step3.Visible = true;

            grdUploadFiles.Visible = true;
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


        public string GetRandomNumber()
        {
            Random r = new Random();
            var x = r.Next(0, 9);
            return x.ToString("0");
        }

        protected void btnPayNow_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerifyDetailPayment.aspx");
        }

        protected void grdUploadFiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUploadFiles.PageIndex = e.NewPageIndex;
            grdUploadFiles.DataSource = dt;
            grdUploadFiles.DataBind();

            div2.Visible = true;
            btnValidateVahan.Visible = true;
            step2.Visible = true;
            div2.Visible = true;
            grdUploadFiles.Visible = true;

            btnValidateVahan.Visible = true;
            btnPayNow.Visible = false;

            step2.Visible = true;
            step3.Visible = true;

        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (string.IsNullOrEmpty(txtPasscode.Text))
            {
                lblMsg.Text = "Please Enter Passcode";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string Qstr = "Execute CheckRwaCodeAvailability '0','5', '" + txtPasscode.Text + "'";
            DataTable dtCheckPincode = Utils.GetDataTable(Qstr, ConnectionString);

            if (dtCheckPincode.Rows.Count > 0)
            {
                HttpContext.Current.Session["DealerAffixationCenterid"] = dtCheckPincode.Rows[0]["DealerAffixationID"].ToString();
                HttpContext.Current.Session["SelectedSlotID"] = "1";// SlotID;
                Qstr = "select distinct CONVERT(VARCHAR(20), cast(blockDate as date), 120) blockDate from [HSRPOEM].[dbo].[HolidayDateTime] " +
               "where cast(blockDate as date) between getdate() and cast(DATEADD(DAY, +6, GETDATE()) as date) and ([Desc] = 'Holiday' or [Desc] = 'Sunday') ";
                DataTable dtHoliday = BMHSRPv2.Models.Utils.GetDataTable(Qstr, ConnectionString);
                HttpContext.Current.Session["SelectedSlotDate"] = DateTime.Today.AddDays(6 + dtHoliday.Rows.Count).ToString("yyyy-MM-dd"); // DateTime.Now.Date.ToString("yyyy-MM-dd");//   SlotDate;
                HttpContext.Current.Session["SelectedSlotTime"] = "10:00AM to 06:00PM";// SlotTime;
                HttpContext.Current.Session["DealerAffixationCenterAdd"] = dtCheckPincode.Rows[0]["DealerAffixationCenterAddress"].ToString();
                //BookingDetail
                // Response.Redirect("BookingSummary.aspx");
                Session["Status"] = "1";
                Session["Slot"] = "Date:" + HttpContext.Current.Session["SelectedSlotDate"].ToString() + " Your available Slot is between " + HttpContext.Current.Session["SelectedSlotTime"].ToString() + " at " + HttpContext.Current.Session["DealerAffixationCenterAdd"].ToString();
                Session["DeliveryCity"] = dtCheckPincode.Rows[0]["DealerAffixationCenterCity"].ToString();
                Session["DeliveryState"] = dtCheckPincode.Rows[0]["hsrpstate"].ToString();

                Session["Passcode"] = txtPasscode.Text;
                div1.Visible = false;
                div2.Visible = true;
            }
            else
            {
                lblMsg.Text = "Invalid Passcode";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }




        }
    }
}

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
