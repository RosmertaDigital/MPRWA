using BMHSRPv2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using CCA.Util;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

namespace BMHSRPv2.plate
{
    public partial class PaymentReceipt : System.Web.UI.Page
    {
        //static string ReceiptCompletePath = string.Empty;
        static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        static string HSRPOEMConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HSRPOEMConnectionString"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {

            if(Session["Type"] != null)
            {
                //lblMessage.Text = Session["Type"].ToString() + "if ";
                //lblMessage.Visible = true;
                divControlsRow.Visible = false;
                btnPrint.Visible = false;
                divRQCode.Visible = false;
            }
            else
            {
                //lblMessage.Text = Session["Type"].ToString() + "else ";
                //lblMessage.Visible = true;
                divControlsRow.Visible = true;
                btnPrint.Visible = true;
                divRQCode.Visible = true;
            }

            //lblMessage.Text = Session["Type"].ToString() + "if ";
            //lblMessage.Visible = true;

            if (!IsPostBack)
            {
                if (Session["Type"] != null)
                {
                    //lblMessage.Text = Session["Type"].ToString() + "if postback";
                    //lblMessage.Visible = true; 
                    divControlsRow.Visible = false;
                    btnPrint.Visible = false;
                    divRQCode.Visible = false;
                }
                else
                {
                    //lblMessage.Text = Session["Type"].ToString() + "else postback";
                    //lblMessage.Visible = true;
                    divControlsRow.Visible = true;
                    btnPrint.Visible = true;
                    divRQCode.Visible = true;
                }

                #region
                try
                {
                    HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
                    HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
                    HttpContext.Current.Response.AddHeader("Expires", "0");

                    #region
                    String order_id = String.Empty;
                    String tracking_id = String.Empty;
                    String bank_ref_no = String.Empty;
                    String order_status = String.Empty;
                    String failure_message = String.Empty;
                    String payment_mode = String.Empty;
                    String card_name = String.Empty;
                    String status_code = String.Empty;
                    String status_message = String.Empty;
                    String currency = String.Empty;
                    String amount = String.Empty;
                    String billing_name = String.Empty;
                    String billing_address = String.Empty;
                    String billing_city = String.Empty;
                    String billing_state = String.Empty;
                    String billing_zip = String.Empty;
                    String billing_country = String.Empty;
                    String billing_tel = String.Empty;
                    String billing_email = String.Empty;
                    String delivery_name = String.Empty;
                    String delivery_address = String.Empty;
                    String delivery_city = String.Empty;
                    String delivery_state = String.Empty;
                    String delivery_zip = String.Empty;
                    String delivery_country = String.Empty;
                    String delivery_tel = String.Empty;
                    String merchant_param1 = String.Empty;
                    String merchant_param2 = String.Empty;
                    String merchant_param3 = String.Empty;
                    String merchant_param4 = String.Empty;
                    String merchant_param5 = String.Empty;
                    String vault = String.Empty;
                    String offer_type = String.Empty;
                    String offer_code = String.Empty;
                    String discount_value = String.Empty;
                    String mer_amount = String.Empty;
                    String eci_value = String.Empty;
                    String retry = String.Empty;
                    String response_code = String.Empty;
                    String billing_notes = String.Empty;
                    String trans_date = String.Empty;
                    String bin_country = String.Empty;
                    String trans_fee = String.Empty;
                    String service_tax = String.Empty;
                    String razor_payment_id = String.Empty;
                    String payment_gateway_type = String.Empty;
                    StringBuilder sb = new StringBuilder();
                    #endregion

                    if (Request.Form["encResp"] != null)
                    {
                        payment_gateway_type = "ccavenue";
                        #region
                        string workingKey = "1CC0F7E9D8F6CD1646D36A789F20234C";//put in the 32bit alpha numeric key in the quotes provided here
                        CCACrypto ccaCrypto = new CCACrypto();

                        string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
                        NameValueCollection Params = new NameValueCollection();
                        string[] segments = encResponse.Split('&');
                        foreach (string seg in segments)
                        {
                            string[] parts = seg.Split('=');
                            if (parts.Length > 0)
                            {
                                string Key = parts[0].Trim();
                                string Value = parts[1].Trim();
                                Params.Add(Key, Value);
                                sb.Append(Key + " - " + Value + Environment.NewLine);
                            }
                        }

                        AddLog(encResponse);

                        try
                        {
                            #region
                            order_id = Params[0].ToString();
                            try
                            {
                                string logQuery = "CCavenueLog '" + order_id + "' ,'" + encResponse + "'";
                                DataTable dtPaymentConfirmation = Utils.GetDataTable(logQuery, ConnectionString);
                            }
                            catch (Exception ev)
                            {

                            }

                            tracking_id = Params[1].ToString() == "null" ? "" : Params[1].ToString();
                            bank_ref_no = Params[2].ToString() == "null" ? "" : Params[2].ToString();
                            order_status = Params[3].ToString() == "null" ? "" : Params[3].ToString();
                            failure_message = Params[4].ToString() == "null" ? "" : Params[4].ToString();
                            payment_mode = Params[5].ToString() == "null" ? "" : Params[5].ToString();
                            card_name = Params[6].ToString() == "null" ? "" : Params[6].ToString();
                            status_code = Params[7].ToString() == "null" ? "" : Params[7].ToString();
                            status_message = Params[8].ToString() == "null" ? "" : Params[8].ToString();
                            currency = Params[9].ToString() == "null" ? "" : Params[9].ToString();
                            amount = Params[10].ToString() == "null" ? "" : Params[10].ToString();
                            billing_name = Params[11].ToString() == "null" ? "" : Params[11].ToString();
                            billing_address = Params[12].ToString() == "null" ? "" : Params[12].ToString();
                            billing_city = Params[13].ToString() == "null" ? "" : Params[13].ToString();
                            billing_state = Params[14].ToString() == "null" ? "" : Params[14].ToString();
                            billing_zip = Params[15].ToString() == "null" ? "" : Params[15].ToString();
                            billing_country = Params[16].ToString() == "null" ? "" : Params[16].ToString();
                            billing_tel = Params[17].ToString() == "null" ? "" : Params[17].ToString();
                            billing_email = Params[18].ToString() == "null" ? "" : Params[18].ToString();
                            delivery_name = Params[19].ToString() == "null" ? "" : Params[19].ToString();
                            delivery_address = Params[20].ToString() == "null" ? "" : Params[20].ToString();
                            delivery_city = Params[21].ToString() == "null" ? "" : Params[21].ToString();
                            delivery_state = Params[22].ToString() == "null" ? "" : Params[22].ToString();
                            delivery_zip = Params[23].ToString() == "null" ? "" : Params[23].ToString();
                            delivery_country = Params[24].ToString() == "null" ? "" : Params[24].ToString();
                            delivery_tel = Params[25].ToString() == "null" ? "" : Params[25].ToString();
                            merchant_param1 = Params[26].ToString() == "null" ? "" : Params[26].ToString();
                            merchant_param2 = Params[27].ToString() == "null" ? "" : Params[27].ToString();


                            merchant_param3 = Params[28].ToString() == "null" ? "" : Params[28].ToString();
                            merchant_param4 = Params[29].ToString() == "null" ? "" : Params[29].ToString();
                            merchant_param5 = Params[30].ToString() == "null" ? "" : Params[30].ToString();
                            vault = Params[31].ToString() == "null" ? "" : Params[31].ToString();
                            offer_type = Params[32].ToString() == "null" ? "" : Params[32].ToString();
                            offer_code = Params[33].ToString() == "null" ? "" : Params[33].ToString();
                            discount_value = Params[34].ToString() == "null" ? "" : Params[34].ToString();
                            mer_amount = Params[35].ToString() == "null" ? "" : Params[35].ToString();
                            eci_value = Params[36].ToString() == "null" ? "" : Params[36].ToString();
                            retry = Params[37].ToString() == "null" ? "" : Params[37].ToString();
                            response_code = Params[38].ToString() == "null" ? "" : Params[38].ToString();
                            billing_notes = Params[39].ToString() == "null" ? "" : Params[39].ToString();
                            trans_date = Params[40].ToString() == "null" ? "" : Params[40].ToString();
                            bin_country = Params[41].ToString() == "null" ? "" : Params[41].ToString();
                            trans_fee = Params[42].ToString() == "null" ? "" : Params[42].ToString();
                            service_tax = Params[43].ToString() == "null" ? "" : Params[43].ToString();
                            #endregion
                        }
                        catch (Exception ev)
                        {

                        }
                        #endregion
                    }


                    if (Request.Form["razorpay_payment_id"] != null)
                    {
                        payment_gateway_type = "razorpay";
                        order_id = Request.Form["generated_order_id"];
                        razor_payment_id = Request.Form["razorpay_payment_id"];
                        merchant_param1 = Request.Form["razorpay_order_id"];
                        merchant_param2 = Request.Form["razorpay_signature"];

                        if (razor_payment_id.Length > 0)
                        {
                            order_status = "Success";
                        }
                        else
                        {
                            order_status = "Failed";
                        }

                        try
                        {
                            #region
                            var dict = new Dictionary<string, string>();
                            foreach (var key in Request.Form.AllKeys)
                            {
                                dict.Add(key, Request.Form[key]);

                            }

                            string logstring = string.Join(";", dict.Select(x => x.Key + "=" + x.Value));
                            AddLog(logstring);
                            #endregion

                            #region
                            try
                            {
                                if (logstring != null && logstring.Length > 0)
                                {
                                    string logQuery = "CCavenueLog '" + order_id + "' ,'" + logstring + "'";
                                    DataTable dtPaymentConfirmation = Utils.GetDataTable(logQuery, ConnectionString);
                                }
                            }
                            catch (Exception ev)
                            {

                            }
                            #endregion
                        }
                        catch (Exception ev)
                        {
                            AddLog(ev.Message);
                        }

                        


                    }


                    #region
                    

                    try
                    {
                        if (Session["Type"] != null)
                        {
                            lblMessage.Text = Session["Type"].ToString() + "if update ";
                            lblMessage.Visible = true;

                            string strStaging = "select VehicleRegNo, ChassisNo, EngineNo from [BookMyHSRP].dbo.Appointment_BookingHistStaging where GroupOrderNo='" + Session["GroupOrderNo"] + "' and OrderStatus='Success' and BookingType='OB'";
                            DataTable dtStaging = Utils.GetDataTable(strStaging, ConnectionString);
                            if (dtStaging.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtStaging.Rows.Count; i++)
                                {
                                    //string strInsert = "update [bookmyhsrp].dbo.Appointment_BookingHist set razorpay_order_id='"+ merchant_param1 + "',razorpay_payment_id='" + razor_payment_id + "', merchant_param1='"+ merchant_param1 + "', merchant_param2='"+ merchant_param2 + "' ,OrderStatus='Success', trans_date='"+DateTime.Now+"',oldOrderno='"+ Session["GroupOrderNo"] + "' where VehicleRegNo='" + dtStaging.Rows[i]["VehicleRegNo"] + "' and ChassisNo='" + dtStaging.Rows[i]["ChassisNo"] + "' and EngineNo='" + dtStaging.Rows[i]["EngineNo"] + "' and OrderType='OB' and OrderStatus='Initiated'";
                                    string strInsert = "update [bookmyhsrp].dbo.Appointment_BookingHist set razorpay_order_id='" + merchant_param1 + "',razorpay_payment_id='" + razor_payment_id + "', merchant_param1='" + merchant_param1 + "', merchant_param2='" + merchant_param2 + "' ,OrderStatus='Success', trans_date='" + Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd") + "',oldOrderno='" + Session["GroupOrderNo"] + "' where BookingHistoryID = (select max(BookingHistoryID) from [BookMyHSRP].dbo.Appointment_BookingHist where VehicleRegNo='" + dtStaging.Rows[i]["VehicleRegNo"] + "' and ChassisNo='" + dtStaging.Rows[i]["ChassisNo"] + "' and EngineNo='" + dtStaging.Rows[i]["EngineNo"] + "' and OrderStatus='Initiated' and OrderType='OB')";
                                    int j = BMHSRPv2.Models.Utils.ExecNonQuery(strInsert, ConnectionString);
                                }
                                if (ViewState["OrderNos"] != null)
                                {
                                    ViewState.Remove("OrderNos");
                                }
                                lblMessage.Text = "Payment Done Successfully";
                                lblMessage.Visible = true;
                            }
                            else
                            {
                                Response.Redirect("../Error.aspx");
                            }
                        }
                        else
                        {
                            //lblMessage.Text = Session["Type"].ToString() + "else update ";
                            //lblMessage.Visible = true;
                            string PaymentConfirmationQuery = "PaymentConfirmation '" + order_id + "','" + tracking_id + "','" + bank_ref_no + "','" + order_status + "','" + failure_message + "','" + payment_mode + "','" + card_name + "','" + status_code + "','" + status_message + "','" + amount + "','" + billing_name + "','" + billing_address + "','" + billing_state + "','" + billing_zip + "','" + billing_country + "','" + billing_tel + "','" + billing_email + "','" + delivery_name + "','" + delivery_address + "','" + delivery_city + "','" + delivery_state + "','" + delivery_zip + "','" + delivery_country + "','" + delivery_tel + "','" + merchant_param1 + "','" + merchant_param2 + "','" + merchant_param3 + "','" + merchant_param4 + "','" + merchant_param5 + "','" + vault + "','" + offer_type + "','" + offer_code + "','" + discount_value + "','" + mer_amount + "','" + eci_value + "','" + retry + "','" + response_code + "','" + billing_notes + "','" + trans_date + "','" + bin_country + "','" + trans_fee + "','" + service_tax + "','" + razor_payment_id + "','" + payment_gateway_type + "'";
                            DataTable dtPaymentConfirmation = Utils.GetDataTable(PaymentConfirmationQuery, ConnectionString);
                            if (dtPaymentConfirmation.Rows.Count > 0)
                            {
                                #region
                                string Status = dtPaymentConfirmation.Rows[0]["status"].ToString();
                                if (Status == "1")
                                {
                                    lblOrderNo.Text = dtPaymentConfirmation.Rows[0]["OrderNo"].ToString();
                                    lblOrderDate.Text = dtPaymentConfirmation.Rows[0]["OrderDate"].ToString();
                                    lblVehicleRegNo.Text = dtPaymentConfirmation.Rows[0]["VehicleRegNo"].ToString();
                                    lblOrderId.Text = dtPaymentConfirmation.Rows[0]["OrderNo"].ToString();
                                    lblTrackingID.Text = dtPaymentConfirmation.Rows[0]["TrackingID"].ToString();
                                    lblPaymentStatus.Text = dtPaymentConfirmation.Rows[0]["OrderStatus"].ToString();
                                    lblAmount.Text = dtPaymentConfirmation.Rows[0]["NetAmount"].ToString();
                                    lblName.Text = dtPaymentConfirmation.Rows[0]["OwnerName"].ToString();
                                    lblMobile.Text = dtPaymentConfirmation.Rows[0]["MobileNo"].ToString();
                                    lblEmailID.Text = dtPaymentConfirmation.Rows[0]["EmailID"].ToString();
                                    lblAppointmentDateTime.Text = dtPaymentConfirmation.Rows[0]["SlotBookingDate"].ToString() + " " + dtPaymentConfirmation.Rows[0]["SlotTime"].ToString();
                                    lblvalidity.Text = "Receipt Validity " + dtPaymentConfirmation.Rows[0]["ReceiptValidUpTo"].ToString();
                                    lblFitmentCenterName.Text = dtPaymentConfirmation.Rows[0]["DealerAffixationCenterName"].ToString();
                                    lblFitmentAddress.Text = dtPaymentConfirmation.Rows[0]["DealerAffixationCenterAddress"].ToString();
                                    lblFitmentPersonName.Text = dtPaymentConfirmation.Rows[0]["FitmentPersonName"].ToString();
                                    lblFitmentPersonMobile.Text = dtPaymentConfirmation.Rows[0]["FitmentPersonMobile"].ToString();


                                    //string Fullurl = "/PaymentReceiptPdf.aspx?OrderNo=" + order_id;
                                    //frame1.Visible = true;
                                    //frame1.Attributes["src"] = Fullurl;

                                    if (dtPaymentConfirmation.Rows[0]["AppointmentType"].ToString() == "Home")
                                    {
                                        trFitmentPerson.Visible = false;
                                        trFitmentMobile.Visible = false;
                                    }


                                    if (order_status == "Success" || order_status == "Shipped")
                                    {
                                        ReceiptGeneration(dtPaymentConfirmation.Rows[0]["OrderNo"].ToString());

                                        try
                                        {
                                            string SMSText = "Your HSRP Appointment Booking Order ID is " + dtPaymentConfirmation.Rows[0]["OrderNo"].ToString() + " for Vehicle Reg No " + dtPaymentConfirmation.Rows[0]["VehicleRegNo"].ToString() + ", Order Status is " + dtPaymentConfirmation.Rows[0]["OrderStatus"].ToString() + ". Bank Track ID " + dtPaymentConfirmation.Rows[0]["TrackingID"].ToString() + ", Schedule Date time :" + dtPaymentConfirmation.Rows[0]["SlotBookingDate"].ToString() + " " + dtPaymentConfirmation.Rows[0]["SlotTime"].ToString() + " For queries contact Toll Free:18001200201, Email ID:online@bookmyhsrp.com";
                                            string SMSResponse = Utils.SMSSend(dtPaymentConfirmation.Rows[0]["MobileNo"].ToString(), SMSText);

                                            string SMSLogSaveQuery = "insert into [BookMyHSRP].dbo.Appointment_SMSDetails (OwnerName,VehicleRegNo,MobileNo,SMSText,SentResponseCode,SentDateTime) values " +
                                             " ('" + dtPaymentConfirmation.Rows[0]["OwnerName"].ToString() + "','" + dtPaymentConfirmation.Rows[0]["VehicleRegNo"].ToString() + "','" + dtPaymentConfirmation.Rows[0]["MobileNo"].ToString() + "','" + SMSText + "','" + SMSResponse + "',getdate()) ";
                                            Utils.ExecNonQuery(SMSLogSaveQuery, ConnectionString);
                                        }
                                        catch (Exception ev)
                                        {

                                        }

                                    }
                                    else
                                    {
                                        Response.Redirect("../PaymentFailed.aspx");
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                Response.Redirect("../Error.aspx");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string logtext = string.Empty;
                        logtext = ex.Message.ToString();
                        logtext = logtext + Environment.NewLine;
                        logtext = logtext + System.DateTime.Now.ToString();
                        AddLog(logtext.ToString());
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    //Object reference not set to an instance of an object.
                    string error = ex.Message;
                    if (error == "Object reference not set to an instance of an object.")
                    {
                        Response.Redirect("../index.aspx");
                    }
                    else
                    {
                        string logtext = string.Empty;
                        logtext = ex.Message.ToString();
                        logtext = logtext + Environment.NewLine;
                        logtext = logtext + System.DateTime.Now.ToString();
                        AddLog(logtext.ToString());
                    }
                }
                #endregion
            }
        }

        private void ReceiptGeneration(string OrderNo)
        {
            string checkReceiptQuery = "select isnull(ReceiptPath,'') FileName from Appointment_bookingHist where OrderNo = '" + OrderNo + "'";
            DataTable dtFileName = Utils.GetDataTable(checkReceiptQuery, ConnectionString);
            if (dtFileName.Rows.Count > 0)
            {
                #region
                string FileName = dtFileName.Rows[0]["FileName"].ToString();
                string folderpath = ConfigurationManager.AppSettings["ReceiptPath"].ToString();
                string PdfFolder = folderpath + FileName;
                if (FileName.Length > 0 && File.Exists(PdfFolder))
                {
                    string[] arr = FileName.ToString().Split('/');
                    int i = arr.Length;

                    //string PdfSignedFolder = signFolderPath + arr[i - 1].ToString();
                    string filename = arr[i - 1].ToString();
                    #region
                    try
                    {
                        HttpContext context = HttpContext.Current;
                        context.Response.ContentType = "Application/pdf";
                        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                        context.Response.WriteFile(PdfFolder);
                        context.Response.End();
                    }
                    catch (Exception ev)
                    {
                        lblMessage.Text = ev.Message;
                        lblMessage.Visible = true;
                    }
                    #endregion
                }
                else
                {
                    GenerateInvoice(OrderNo);
                }
                #endregion
            }
            else
            {
                GenerateInvoice(OrderNo);
            }
        }

        private void GenerateInvoice(string OrderNo)
        {
            try
            {
                //ResourceManager rm = new ResourceManager("BMHSRPv2.Resource", Assembly.GetExecutingAssembly());
                //String strWebsite = rm.GetString("Website", CultureInfo.CurrentCulture);
                //String strName = rm.GetString("Name");
                //String strpt1 = rm.GetString("pt1", CultureInfo.CurrentCulture);
                //String strpt2 = rm.GetString("pt2");
                //String strpt3 = rm.GetString("pt3");
                //String strpt4 = rm.GetString("pt4");
                //String strpt5 = rm.GetString("pt5");

                string checkReceiptQuery = "PaymentPlateReceipt_MX '" + OrderNo + "' ";
                DataTable dtReceipt = Utils.GetDataTable(checkReceiptQuery, ConnectionString);
                if (dtReceipt.Rows.Count > 0)
                {
                    string Status = dtReceipt.Rows[0]["status"].ToString();
                    if (Status == "1")
                    {
                        #region
                        string BookingHistoryID = dtReceipt.Rows[0]["BookingHistoryID"].ToString();
                        string OrderType = dtReceipt.Rows[0]["ordertype"].ToString();
                        string BookingType = dtReceipt.Rows[0]["BookingType"].ToString();
                        string OrderDate = dtReceipt.Rows[0]["OrderDate"].ToString();
                        string VehicleRegNo = dtReceipt.Rows[0]["VehicleRegNo"].ToString();
                        string fuelType = dtReceipt.Rows[0]["fuelType"].ToString();
                        string VehicleClass = dtReceipt.Rows[0]["VehicleClass"].ToString();
                        string VehicleType = dtReceipt.Rows[0]["VehicleType"].ToString();
                        //string VehicleType = string.Empty;
                        //string GetOemVehicleType = "usp_VahanResponseLog_GetVehicleClass '" + VehicleRegNo + "'";
                        //DataTable DtOemVehicleType = BMHSRPv2.Models.Utils.GetDataTable(GetOemVehicleType, ConnectionString);
                        //if (DtOemVehicleType.Rows.Count > 0)
                        //{
                        //    if (HttpContext.Current.Session["NonHomo"].ToString() == "Y")
                        //    {
                        //        VehicleType = HttpContext.Current.Session["NonHomoVehicleType"].ToString();
                        //    }
                        //    else
                        //    {
                        //        VehicleType = DtOemVehicleType.Rows[0]["VahanVehType"].ToString();
                        //    }
                        //}

                        OrderNo = dtReceipt.Rows[0]["OrderNo"].ToString();
                        string TrackingID = dtReceipt.Rows[0]["TrackingID"].ToString();
                        string AppointmentType = dtReceipt.Rows[0]["AppointmentType"].ToString();
                        //    string SuperTagAmt = dtReceipt.Rows[0]["SuperTagAmount"].ToString();
                        //   string _isSuperTag = dtReceipt.Rows[0]["isSuperTag"].ToString();
                        decimal GstBasic_Amt = Convert.ToDecimal(dtReceipt.Rows[0]["BasicAamount"]) + Convert.ToDecimal(dtReceipt.Rows[0]["FitmentCharge"]);

                        string BasicAamount = dtReceipt.Rows[0]["BasicAamount"].ToString();
                        string FitmentCharge = dtReceipt.Rows[0]["FitmentCharge"].ToString();
                        string ConvenienceFee = dtReceipt.Rows[0]["ConvenienceFee"].ToString();
                        string HomeDeliveryCharge = dtReceipt.Rows[0]["HomeDeliveryCharge"].ToString();
                        string TotalAmount = dtReceipt.Rows[0]["TotalAmount"].ToString();
                        string GSTAmount = dtReceipt.Rows[0]["GSTAmount"].ToString();
                        decimal IGSTAmount = Convert.ToDecimal(dtReceipt.Rows[0]["IGSTAmount"]);
                        decimal CGSTAmount = Convert.ToDecimal(dtReceipt.Rows[0]["CGSTAmount"]);
                        decimal SGSTAmount = Convert.ToDecimal(dtReceipt.Rows[0]["SGSTAmount"]);
                        string NetAmount = dtReceipt.Rows[0]["NetAmount"].ToString();
                        string OrderStatus = dtReceipt.Rows[0]["OrderStatus"].ToString();
                        string StatusMessage = dtReceipt.Rows[0]["StatusMessage"].ToString();
                        string OwnerName = dtReceipt.Rows[0]["OwnerName"].ToString();
                        string MobileNo = dtReceipt.Rows[0]["MobileNo"].ToString();
                        string EmailID = dtReceipt.Rows[0]["EmailID"].ToString();
                        string SlotBookingDate = dtReceipt.Rows[0]["SlotBookingDate"].ToString();
                        string ReceiptValidUpTo = dtReceipt.Rows[0]["ReceiptValidUpTo"].ToString();
                        string SlotTime = dtReceipt.Rows[0]["SlotTime"].ToString();
                        string DealerAffixationCenterName = dtReceipt.Rows[0]["DealerAffixationCenterName"].ToString();
                        string DealerAffixationCenterAddress = dtReceipt.Rows[0]["DealerAffixationCenterAddress"].ToString();
                        string FitmentPersonName = dtReceipt.Rows[0]["FitmentPersonName"].ToString();
                        string FitmentPersonMobile = dtReceipt.Rows[0]["FitmentPersonMobile"].ToString();

                        string MonthYears = DateTime.Now.ToString("MMM-yyyy");

                        string folderpath = ConfigurationManager.AppSettings["ReceiptPath"].ToString() + "/Plate/" + MonthYears + "/";
                        string BILLEDADDRESS = dtReceipt.Rows[0]["BILLEDADDRESS"].ToString();
                        string SuperTagAmount = dtReceipt.Rows[0]["SuperTagAmount"].ToString();
                        string totalamount = dtReceipt.Rows[0]["SuperTagTotalAmount"].ToString();
                        string IGSTAmountST = dtReceipt.Rows[0]["IGSTAmountST"].ToString();
                        string CGSTAmountST = dtReceipt.Rows[0]["CGSTAmountST"].ToString();
                        string SGSTAmountST = dtReceipt.Rows[0]["SGSTAmountST"].ToString();

                        string FrameTagAmount = dtReceipt.Rows[0]["FrameTagAmount"].ToString();
                        string Frametotalamount = dtReceipt.Rows[0]["FrameTagTotalAmount"].ToString();
                        string IGSTAmountFrm = dtReceipt.Rows[0]["IGSTAmountFrm"].ToString();
                        string CGSTAmountFrm = dtReceipt.Rows[0]["CGSTAmountFrm"].ToString();
                        string SGSTAmountFrm = dtReceipt.Rows[0]["SGSTAmountFrm"].ToString();
                        string isSuperTag = dtReceipt.Rows[0]["isSuperTag"].ToString();
                        string isFramrTag = dtReceipt.Rows[0]["IsFrame"].ToString();

                        string CustomerAddress1 = dtReceipt.Rows[0]["CustomerAddress1"].ToString();
                        string CustomerCity = dtReceipt.Rows[0]["CustomerCity"].ToString();
                        string CustomerPin = dtReceipt.Rows[0]["CustomerPin"].ToString();
                        string MRDCharges = dtReceipt.Rows[0]["MRDCharges"].ToString();
                        string StateId = dtReceipt.Rows[0]["HSRP_StateID"].ToString();

                        string query = "select GSTIN from [HSRPOEM].dbo.HSRPState where HSRP_StateID=" + StateId + "";
                        DataTable dtGSTIN = Utils.GetDataTable(query, HSRPOEMConnectionString);

                        if (!Directory.Exists(folderpath))
                        {
                            Directory.CreateDirectory(folderpath);
                        }

                        string filename = OrderNo + DateTime.Now.ToString("ddMMyyyyHHmmssfff") + ".pdf";
                        string PdfFolder = folderpath + filename;
                        string filepathtosave = "Plate/" + MonthYears + "/" + filename; //for saving into database

                        //string ReceiptPathQRCode = "https://chart.googleapis.com/chart?chs=80x80&cht=qr&chl=" + ConfigurationManager.AppSettings["ReceiptDirectory"].ToString() + filepathtosave + "&chld=L|1&choe=UTF-8";
                        string ReceiptPathQRCode = "https://chart.googleapis.com/chart?chs=80x80&cht=qr&chl=https://bookmyhsrp.com/TrackOrder.aspx?oid=" + OrderNo + "%26vr=" + VehicleRegNo + "&chld=L|1&choe=UTF-8";

                        StringBuilder sbTable = new StringBuilder();
                        sbTable.Clear();

                        string ImgPath = Server.MapPath("~/assetsfile/images/logo");

                        sbTable.Append("<table style='width:100%;text-align:center;font-size: 10pt' border='1'>");
                        sbTable.Append("<tr>");
                        sbTable.Append("<td>");

                        sbTable.Append("<table style='width:100%;margin-top:0' border='0'>");
                        sbTable.Append("<tr><td align='center'><b>Receipt of Payment & Appointment</b></td></tr>");
                        sbTable.Append("</table>");

                        sbTable.Append("</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>");
                        sbTable.Append("<table style='width:100%;' border='0'>");
                        sbTable.Append("<tr>");
                        sbTable.Append("<td align='left'><img src='" + ImgPath + "/BookMyHSRP.png' height='25px'/></td>");
                        //sb.Append("<td><span><img src='" + ImgPath + "/BookMyHSRP.png' height='13px'/><span><b>Receipt of Payment & Appointment</b><span text-align:right><img src='" + ImgPath + "/logo.png' height='13px'/></span></td>");
                        sbTable.Append("<td></td>");
                        sbTable.Append("<td style='padding-left:90px;'><img src='" + ImgPath + "/logo.png' height='25px'/></td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("</table>");
                        sbTable.Append("</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>");

                        sbTable.Append("<table style='width:100%;' border='0'>");
                        sbTable.Append("<tr>");
                        sbTable.Append("<td><b>Rosmerta Safety Systems Pvt. Ltd.</b><br/>https://bookmyhsrp.com<br/>HSRP Online Appointment Transaction Receipt<br/>GST No.: " + dtGSTIN.Rows[0]["GSTIN"].ToString() + "</td>");
                        if (AppointmentType.ToString().Equals("Dealer"))
                        {
                            sbTable.Append("<td><b>Fitment Location:</b><br/>" + DealerAffixationCenterName + "<br/>" + DealerAffixationCenterAddress + "<br/>" + FitmentPersonName + "<br/>" + FitmentPersonMobile + "</td>");
                        }
                        else
                        {
                            sbTable.Append("<td><b>Fitment Location:</b><br/>" + DealerAffixationCenterAddress + "<br/>" + FitmentPersonName + "<br/>" + FitmentPersonMobile + "</td>");
                        }
                        sbTable.Append("</tr>");
                        sbTable.Append("</table>");

                        sbTable.Append("</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>");
                        sbTable.Append("<table style='width:100%;margin-top:0' border='0'>");
                        sbTable.Append("<tr margin-top:-10px;>");
                        sbTable.Append("<td>Order Date: <br/><span style='color: blue;'>" + OrderDate + "</span></td>");
                        sbTable.Append("<td>Order ID: <br/><span style='color: blue;'>" + OrderNo + "</span></td>");
                        sbTable.Append("<td>Order Status:<br/><span style='color: blue;'>" + OrderStatus + "</span></td>");
                        //sbTable.Append("<td>Appointment Date & Time: <br/><span style='color: blue;'>01-02-2023 09:00</span></td>");
                        //sbTable.Append("<td>Appointment Date & Time: <br/><span style='color: blue;'>" + SlotBookingDate + " " + SlotTime + "</span></td>");
                        //if (Session["Affix"].ToString() != "Affixval")
                        //{
                            sbTable.Append("<td>Appointment Date & Time: <br/><span style='color: blue;'>" + SlotBookingDate + " " + SlotTime + "</span></td>");
                        //}
                        //else
                        //{
                        //    sbTable.Append("<td>Appointment Date & Time: <br/> <b>Fitment Date will be informed later via SMS and on Call</b></td>");
                        //}

                        sbTable.Append("</tr>");

                        sbTable.Append("</table>");
                        sbTable.Append("</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>");

                        sbTable.Append("<table style='width:100%;' border='0'>");
                        sbTable.Append("<tr>");
                        sbTable.Append("<td><b><u>Billing Detail</u></b><br/><b>Billing Name:</b><br/>" + OwnerName + "</td>");
                        //sbTable.Append("<td><b>Billing Name:</b><br/>Ramesh Kumar</td>");
                        //sbTable.Append("<td><br/><b>Billing Mobile:</b><br/>" + MobileNo + "</td>");
                        //sbTable.Append("<td colspan = '2' margin-left: 100;><br/><b>Billing Email ID:</b><br/>" + EmailID + "</td>");
                        sbTable.Append("</tr>");
                        sbTable.Append("</table>");

                        sbTable.Append("</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>");

                        sbTable.Append("<table style='width:100%;' border='0'>");
                        sbTable.Append("<tr>");
                        sbTable.Append("<td><b><u>Vehicle Detail</u></b><br/><b>Vehicle Reg No: </b><br/>" + VehicleRegNo + "</td>");
                        //sbTable.Append("<td><b>Vehicle Reg No: </b><br/>DL10C1996</td>");
                        sbTable.Append("<td><br/><b>Vehicle Fuel:</b><br/>" + fuelType + "</td>");
                        sbTable.Append("<td><br/><b>Vehicle Class:</b><br/>" + VehicleClass + "</td>");
                        sbTable.Append("<td><br/><b>Vehicle Type:</b><br/>" + VehicleType + "</td>");
                        sbTable.Append("</tr>");
                        sbTable.Append("</table>");

                        sbTable.Append("</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>");

                        sbTable.Append("<table style='width:100%;' border='0'>");
                        sbTable.Append("<tr>");

                        if (OrderStatus.ToString().Equals("Success") || OrderStatus.ToString().Equals("Shipped"))
                        {
                            //sbTable.Append("<tr>");
                            if (OrderType.ToUpper().Trim() == "DR")
                            {
                                if (Session["Oemid"].ToString() == "46")
                                {
                                    sbTable.Append("<td><b><u>Payment Detail</u></b><br/><b>HSRP Cost (Rear Plate) (including Fitment Cost): </b></td>");
                                }
                                else
                                {
                                    sbTable.Append("<td><b><u>Payment Detail</u></b><br/><b>HSRP Cost (Rear Plate) (including Service & Fitment Cost): </b></td>");
                                }
                            }
                            else if (OrderType.ToUpper().Trim() == "DF")
                            {
                                if (Session["Oemid"].ToString() == "46")
                                {
                                    sbTable.Append("<td><b><u>Payment Detail</u></b><br/><b>HSRP Cost (Front Plate) (including Fitment Cost): </b></td>");
                                }
                                else
                                {
                                    sbTable.Append("<td><b><u>Payment Detail</u></b><br/><b>HSRP Cost (Front Plate) (including Service & Fitment Cost): </b></td>");
                                }

                            }
                            else if (OrderType.ToUpper().Trim() == "OB" || OrderType.ToUpper().Trim() == "DB")
                            {
                                if (Session["Oemid"].ToString() == "46")
                                {
                                    sbTable.Append("<td><b><u>Payment Detail</u></b><br/><b>HSRP Cost (Complete set) (including Fitment Cost): </b></td>");
                                }
                                else
                                {
                                    sbTable.Append("<td><b><u>Payment Detail</u></b><br/><b>HSRP Cost (Complete set) (including Service & Fitment Cost): </b></td>");
                                }

                            }
                            else
                            {
                                if (Session["Oemid"].ToString() == "46")
                                {
                                    sbTable.Append("<td><b><u>Payment Detail</u></b><br/><b>HSRP Cost (Complete set) (including Fitment Cost): </b></td>");
                                }
                                else
                                {
                                    sbTable.Append("<td><b><u>Payment Detail</u></b><br/><b>HSRP Cost (Complete set) (including Service & Fitment Cost): </b></td>");
                                }
                            }

                            sbTable.Append("<td><b>" + string.Format("{0:0.00}", GstBasic_Amt) + " INR</b></td>");
                            sbTable.Append("<td><img height='70px' width='70px' src='" + ReceiptPathQRCode + "' id='img_qrcode'></td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr style='line-height:80%;'>");
                            sbTable.Append("<td>Convenience Fee :</td>");
                            sbTable.Append("<td colspan='2'>" + ConvenienceFee + " INR</td>");
                            sbTable.Append("</tr>");

                            if (MRDCharges != "0.00")
                            {
                                sbTable.Append("<tr style='line-height:80%;'>");
                                sbTable.Append("<td>MRD Charges :</td>");
                                sbTable.Append("<td colspan='2'>" + MRDCharges + " INR</td>");
                                sbTable.Append("</tr>");
                            }

                            if (AppointmentType.ToString().Equals("rwa"))
                            {
                                sbTable.Append("<tr style='line-height:80%;'>");
                                sbTable.Append("<td>Home Delivery :</td>");
                                sbTable.Append("<td colspan='2'>" + HomeDeliveryCharge + " INR</td>");
                                sbTable.Append("</tr>");
                            }

                            sbTable.Append("<tr style='line-height:80%;'>");
                            sbTable.Append("<td>Gross Total :</td>");
                            sbTable.Append("<td>" + TotalAmount + " INR</td>");
                            //sbTable.Append("<td>Receipt is valid till: <b> " + ReceiptValidUpTo + "</b></td>");
                            //if (Session["Affix"].ToString() != "Affixval")
                            //{
                                sbTable.Append("<td>Receipt is valid till: <b> " + ReceiptValidUpTo + "</b></td>");
                            //}
                            //else
                            //{
                            //    sbTable.Append("<td></td>");
                            //}
                            sbTable.Append("</tr>");

                            if (IGSTAmount != 0)
                            {
                                sbTable.Append("<tr style='line-height:80%;'>");
                                sbTable.Append("<td>IGST Amount :</td>");
                                sbTable.Append("<td colspan='2'>" + string.Format("{0:0.00}", IGSTAmount) + " INR</td>");
                                sbTable.Append("</tr>");
                            }

                            if (CGSTAmount != 0)
                            {
                                sbTable.Append("<tr style='line-height:80%;'>");
                                sbTable.Append("<td>CGST Amount :</td>");
                                sbTable.Append("<td colspan='2'>" + string.Format("{0:0.00}", CGSTAmount) + " INR</td>");
                                sbTable.Append("</tr>");
                            }

                            if (SGSTAmount != 0)
                            {
                                sbTable.Append("<tr style='line-height:80%;'>");
                                sbTable.Append("<td>SGST Amount :</td>");
                                sbTable.Append("<td colspan='2'>" + string.Format("{0:0.00}", SGSTAmount) + " INR</td>");
                                sbTable.Append("</tr>");
                            }

                            sbTable.Append("<tr style='line-height:80%;'>");
                            sbTable.Append("<td><b>Grand Total :</b></td>");
                            sbTable.Append("<td colspan='2'><b>" + NetAmount + " INR</b></td>");
                            sbTable.Append("</tr>");
                        }

                        sbTable.Append("</table>");
                        string HindiImgPath = Server.MapPath("~/HindiLang");
                        //string HindiImgPath = ConfigurationManager.AppSettings["ReceiptDirectory"].ToString();

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>");
                        sbTable.Append("<table style='width:100%;' border='0'>");
                        sbTable.Append("<tr>");
                        sbTable.Append("<td><u><b>Instructions</b></u></td>");
                        sbTable.Append("</tr>");

                        if (VehicleRegNo.Trim().ToLower().StartsWith("dl") == true)
                        {
                            sbTable.Append("<tr><td>1. Please show this Receipt and R.C Copy at the time of fitment. </td></tr>");
                            sbTable.Append("<tr><td>2. Please ensure respective vehicle in which the HSRP has to be Affixed should be available. </td></tr>");
                            sbTable.Append("<tr><td>3. As per the M.V Act (Transport Ministry), The HSRP will be affixed in a respective vehicle only. The HSRP will not be Handed over to the Vehicle Owner. </td></tr>");
                            sbTable.Append("<tr><td>4. Changes in appointment date (if any) will only be available for a future date,on paid basis.</td></tr>");

                            if (AppointmentType.ToString().Equals("Dealer"))
                            {
                                sbTable.Append("<tr><td>5. For Dealer Point Grievance – please contact at +91 8929722203(Calling time 9.00 AM to 6 PM (Monday to Saturday) and Email ID is grievance@bookmyhsrp.com </td></tr>");
                            }
                            if (AppointmentType.ToString().Equals("Home"))
                            {
                                sbTable.Append("<tr><td>5. For Home Fixation Grievance – please contact at +91 8929722202(Calling time 9.00 AM to 6 PM (Monday to Saturday) and Email ID is homegrievance@bookmyhsrp.com </td></tr>");
                            }

                            sbTable.Append("<tr><td>6. Fitment charges have already been paid by you, no extra payment is required to be paid to the fitment person/dealer team.</td></tr>");
                            sbTable.Append("<tr><td>7. Please note that you can cancel your order within 4 hrs from Successful  booking. </td></tr>");
                            sbTable.Append("<tr><td>8. In case of fitment at the dealer's end, the responsibility of company would be to deliver the HSRP to the dealer's address as selected by the vehicle owner at the time of booking.</td></tr>");
                            sbTable.Append("<tr><td>9. It is the responsibility of the vehicle owner to bring the vehicle to the selected dealer to get the HSRP affixed on vehicle.</td></tr>");
                            sbTable.Append("<tr><td>10. If the HSRP is not affixed within six months from the original affixation date, it shall be destroyed and no refund shall be given in any circumstances.</td></tr>");
                        }
                        else if ((VehicleRegNo.Trim().ToLower().StartsWith("up") == true) || (VehicleRegNo.Trim().ToLower().StartsWith("mp") == true))
                        {
                            //string HindiImgPath = ConfigurationManager.AppSettings["ReceiptDirectory"].ToString();
                            //string HindiImgPath = Server.MapPath("~/HindiLang/dealer");

                            sbTable.Append("<tr><td style='line-height:11px;'>1. Please show this Receipt and R.C Copy at the time of fitment. <img src='" + HindiImgPath + "/dealer/pt1.jpg' height='10px'/></td></tr>");
                            sbTable.Append("<tr><td style='line-height:11px;'>2. Please ensure respective vehicle in which the HSRP has to be Affixed should be available.<br/> <img src='" + HindiImgPath + "/dealer/pt2.jpg' height='10px'/></td></tr>");
                            sbTable.Append("<tr><td style='line-height:11px;'>3. As per the M.V Act (Transport Ministry), The HSRP will be affixed in a respective vehicle only. The HSRP will not be Handed over to the Vehicle Owner.<br/> <img src='" + HindiImgPath + "/dealer/pt3.jpg' height='10px'/></td></tr>");
                            sbTable.Append("<tr><td style='line-height:11px;'>4. Changes in appointment date (if any) will only be available for a future date,on paid basis.<br/> <img src='" + HindiImgPath + "/dealer/pt4.jpg' height='10px'/></td></tr>");
                            if (AppointmentType.ToString().Equals("Dealer"))
                            {
                                sbTable.Append("<tr><td style='line-height:11px;'>5. For Dealer Point Grievance – please contact at +91 8929722203(Calling time 9.00 AM to 6 PM (Monday to Saturday) and Email ID is grievance@bookmyhsrp.com <br/> <img src='" + HindiImgPath + "/dealer/pt5.jpg' height='13px'/></td></tr>");
                            }
                            if (AppointmentType.ToString().Equals("Home") || AppointmentType.ToString().Equals("rwa"))
                            {
                                sbTable.Append("<tr><td style='line-height:11px;'>5. For Home Fixation Grievance – please contact at +91 8929722202(Calling time 9.00 AM to 6 PM (Monday to Saturday) and Email ID is homegrievance@bookmyhsrp.com<br/> <img src='" + HindiImgPath + "/home/pt5.jpg' height='13px'/></td></tr>");
                            }
                            sbTable.Append("<tr><td style='line-height:11px;'>6. Fitment charges have already been paid by you, no extra payment is required to be paid to the fitment person/dealer team. <img src='" + HindiImgPath + "/dealer/pt6.jpg' height='10px'/></td></tr>");
                            sbTable.Append("<tr><td style='line-height:11px;'>7. Please note that you can cancel your order within 4 hrs from Successful  booking. <img src='" + HindiImgPath + "/dealer/pt7.jpg' height='10px'/></td></tr>");
                            sbTable.Append("<tr><td style='line-height:11px;'>8. In case of fitment at the dealer's end, the responsibility of company would be to deliver the HSRP to the dealer's address as selected by the vehicle owner at the time of booking.<br/> <img src='" + HindiImgPath + "/dealer/pt8.jpg' height='10px'/></td></tr>");
                            sbTable.Append("<tr><td style='line-height:11px;'>9. It is the responsibility of the vehicle owner to bring the vehicle to the selected dealer to get the HSRP affixed on vehicle.  <br/><img src='" + HindiImgPath + "/dealer/pt9.jpg' height='14px'/></td></tr>");
                            sbTable.Append("<tr><td style='line-height:11px;'>10. If the HSRP is not affixed within six months from the original affixation date, it shall be destroyed and no refund shall be given in any circumstances. <img src='" + HindiImgPath + "/dealer/pt10.jpg' height='10px'/></td></tr>");
                        }
                        else
                        {
                            sbTable.Append("<tr><td>1. Carry this Receipt and RC Copy at the time of fitment.</td></tr>");
                            sbTable.Append("<tr><td>2. Bring you respective vehicle in which the HRSP has to be installed.</td></tr>");
                            sbTable.Append("<tr><td>3. Re-Appointment (if any) will only available for future date.</td></tr>");


                            if (StateId == "18")
                            {
                                sbTable.Append("<tr><td>4. For Dealer Point Grievance – please contact at +91 9205262323(Calling time 9.30AM - 6PM (07 days)) and Email ID is cssupport@rosmertasafety.com </td></tr>");

                            }
                            else
                            {
                                if (AppointmentType.ToString().Equals("Dealer"))
                                {
                                    sbTable.Append("<tr><td>4. For Dealer Point Grievance – please contact at +91 8929722203(Calling time 9.30AM - 6PM (07 days)) and Email ID is grievance@bookmyhsrp.com </td></tr>");
                                }
                                if (AppointmentType.ToString().Equals("Home"))
                                {
                                    sbTable.Append("<tr><td>4. For Home Fixation Grievance – please contact at +91 8929722202(Calling time 9.30 AM to 6 PM (07 days)) and Email ID is homegrievance@bookmyhsrp.com </td></tr>");
                                }
                            }



                            sbTable.Append("<tr><td>5. Fitment charges paid, no extra payment required to be paid to fitment person/dealer team.</td></tr>");
                            if (VehicleRegNo.Trim().ToLower().StartsWith("od") == true || VehicleRegNo.Trim().ToLower().StartsWith("or") == true)
                            {
                                sbTable.Append("<tr><td>6. Warranty for five years applicable in compliance to S.O.6052 dated 06.12.2018 and Rule 50 of CMVR 1989.</td></tr>");
                                sbTable.Append("<tr><td>7. In case of fitment at the dealer's end, the responsibility of company would be to deliver the HSRP to the dealer's address as selected by the vehicle owner at the time of booking.</td></tr>");
                                sbTable.Append("<tr><td>8. It is the responsibility of the vehicle owner to bring the vehicle to the selected dealer to get the HSRP affixed on vehicle.</td></tr>");
                                sbTable.Append("<tr><td>9. If the HSRP is not affixed within six months from the original affixation date, it shall be destroyed and no refund shall be given in any circumstances.</td></tr>");
                            }
                            else
                            {
                                sbTable.Append("<tr><td>6. In case of fitment at the dealer's end, the responsibility of company would be to deliver the HSRP to the dealer's address as selected by the vehicle owner at the time of booking.</td></tr>");
                                sbTable.Append("<tr><td>7. It is the responsibility of the vehicle owner to bring the vehicle to the selected dealer to get the HSRP affixed on vehicle.</td></tr>");
                                sbTable.Append("<tr><td>8. If the HSRP is not affixed within six months from the original affixation date, it shall be destroyed and no refund shall be given in any circumstances.</td></tr>");

                            }
                        }

                        sbTable.Append("</table>");

                        sbTable.Append("</td>");
                        sbTable.Append("</tr>");

                        //if ((VehicleRegNo.Trim().ToLower().StartsWith("up") == true) || (VehicleRegNo.Trim().ToLower().StartsWith("mp") == true))
                        //{
                        //    sbTable.Append("<br/>");
                        //    sbTable.Append("<tr>");
                        //    sbTable.Append("<td>");
                        //    sbTable.Append("<table style='width:100%;' border='0'>");
                        //    sbTable.Append("<tr><td>9. It is the responsibility of the vehicle owner to bring the vehicle to the selected dealer to get the HSRP affixed on vehicle.  <br/><img src='" + HindiImgPath + "/dealer/pt9.jpg' height='14px'/></td></tr>");
                        //    sbTable.Append("<tr><td>10. If the HSRP is not affixed within six months from the original affixation date, it shall be destroyed and no refund shall be given in any circumstances. <img src='" + HindiImgPath + "/dealer/pt10.jpg' height='10px'/></td></tr>");
                        //    sbTable.Append("</table>");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("</tr>");
                        //}

                        sbTable.Append("</table>");

                        if (isSuperTag == "Y")
                        {
                            sbTable.Append("<br/>"); sbTable.Append("<br/>");
                            sbTable.Append("<br/>"); sbTable.Append("<br/>");

                            if (VehicleRegNo.Trim().ToLower().StartsWith("dl") == true)
                            {
                                sbTable.Append("<br/>"); sbTable.Append("<br/>");
                                sbTable.Append("<br/>"); sbTable.Append("<br/>");
                                sbTable.Append("<br/>"); sbTable.Append("<br/>");
                                sbTable.Append("<br/>"); sbTable.Append("<br/>");
                                sbTable.Append("<br/>"); sbTable.Append("<br/>");
                                sbTable.Append("<br/>"); sbTable.Append("<br/>");
                            }
                            else if ((VehicleRegNo.Trim().ToLower().StartsWith("up") == true) || (VehicleRegNo.Trim().ToLower().StartsWith("mp") == true))
                            {
                                sbTable.Append("<br/>"); sbTable.Append("<br/>");
                                sbTable.Append("<br/>"); sbTable.Append("<br/>");
                                sbTable.Append("<br/>"); sbTable.Append("<br/>");
                                sbTable.Append("<br/>"); sbTable.Append("<br/>");
                            }
                            else
                            {
                                sbTable.Append("<br/>"); sbTable.Append("<br/>");
                                sbTable.Append("<br/>"); sbTable.Append("<br/>");
                                sbTable.Append("<br/>"); sbTable.Append("<br/>");
                                sbTable.Append("<br/>"); sbTable.Append("<br/>");
                                sbTable.Append("<br/>"); sbTable.Append("<br/>");
                                sbTable.Append("<br/>"); sbTable.Append("<br/>");
                            }


                            sbTable.Append("<table>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td valign='bottom' width='55%' ><b><h4>Receipt of Payment</h4></b></td>");
                            sbTable.Append("<td rowspan=2><img src='" + ReceiptPathQRCode + "' id='img_qrcode'></td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr><td><p> <br> Rosmerta Safety Systems Pvt.Ltd. <br> https://bookmyhsrp.com </p><br>GST No.: " + dtGSTIN.Rows[0]["GSTIN"].ToString() + "</td></tr>");
                            //sbTable.Append("<tr><td>GST No.:" + dtGSTIN.Rows[0]["GSTIN"].ToString() + "</td></tr>");
                            sbTable.Append("</table>");

                            sbTable.Append("<table style='font-size:medium;'>");
                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Order Date :</td>");
                            sbTable.Append("<td>" + OrderDate + "</td>");
                            sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>Vehicle Reg No :</td>");
                            //sbTable.Append("<td><b>" + VehicleRegNo + "</b></td>");
                            //sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>Vehicle Fuel :</td>");
                            //sbTable.Append("<td>" + fuelType + "</td>");
                            //sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>Vehicle Class :</td>");
                            //sbTable.Append("<td>" + VehicleClass + "</td>");
                            //sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>Vehicle Type :</td>");
                            //sbTable.Append("<td>" + VehicleType + "</td>");
                            //sbTable.Append("</tr>");

                            sbTable.Append("<tr style='line-height:80%;'>");
                            sbTable.Append("<td><b>Order ID :</b></td>");
                            sbTable.Append("<td><b>" + OrderNo + "</b></td>");
                            sbTable.Append("</tr>");



                            sbTable.Append("<tr style='line-height:80%;'>");
                            sbTable.Append("<td>Order Status :</td>");
                            sbTable.Append("<td><b>" + OrderStatus + "</b></td>");
                            sbTable.Append("</tr>");




                            if (OrderStatus.ToString().Equals("Success") || OrderStatus.ToString().Equals("Shipped"))
                            {
                                sbTable.Append("<tr>");
                                sbTable.Append("<td><b>Park+FasTag charges (Do it yourself) :</b></td>");
                                sbTable.Append("<td><b>" + string.Format("{0:0.00}", (Convert.ToDecimal(84.74)).ToString()) + " INR</b></td>");
                                sbTable.Append("</tr>");

                                sbTable.Append("<tr style='line-height:80%;'>");
                                sbTable.Append("<td width='55%'><b>Dispatch & Convenience Charge for Park+FasTag :</b></td>");
                                sbTable.Append("<td><b>" + string.Format("{0:0.00}", (Convert.ToDecimal(212.72)).ToString()) + " INR</b></td>");
                                sbTable.Append("</tr>");

                                sbTable.Append("<tr style='line-height:80%;'>");
                                sbTable.Append("<td><b>Gross Total :</b></td>");
                                sbTable.Append("<td><b>" + string.Format("{0:0.00}", (Convert.ToDecimal(SuperTagAmount) - 99).ToString()) + " INR</b></td>");
                                sbTable.Append("</tr>");

                                if (IGSTAmount != 0)
                                {

                                    sbTable.Append("<tr style='line-height:80%;'>");
                                    sbTable.Append("<td>IGST Amount :</td>");
                                    sbTable.Append("<td>" + string.Format("{0:0.00}", IGSTAmountST) + " INR</td>");
                                    sbTable.Append("</tr>");
                                }

                                if (CGSTAmount != 0)
                                {
                                    sbTable.Append("<tr style='line-height:80%;'>");
                                    sbTable.Append("<td>CGST Amount :</td>");
                                    sbTable.Append("<td>" + string.Format("{0:0.00}", CGSTAmountST) + " INR</td>");
                                    sbTable.Append("</tr>");
                                }

                                if (SGSTAmount != 0)
                                {

                                    sbTable.Append("<tr style='line-height:80%;'>");
                                    sbTable.Append("<td>SGST Amount :</td>");
                                    sbTable.Append("<td>" + string.Format("{0:0.00}", SGSTAmountST) + " INR</td>");
                                    sbTable.Append("</tr>");
                                }


                                sbTable.Append("<tr style='line-height:80%;'>");
                                sbTable.Append("<td>Sub Total :</td>");
                                sbTable.Append("<td>" + ((Convert.ToDecimal(SuperTagAmount) - 99) + Convert.ToDecimal(IGSTAmountST) + Convert.ToDecimal(CGSTAmountST) + Convert.ToDecimal(SGSTAmountST)).ToString() + " INR</td>");
                                sbTable.Append("</tr>");



                                sbTable.Append("<tr style='line-height:80%;'>");
                                sbTable.Append("<td><b>Park+FasTag Wallet Balance(Recharge):</b></td>");
                                sbTable.Append("<td><b>" + string.Format("{0:0.00}", "99") + " INR</b></td>");
                                sbTable.Append("</tr>");


                                //sbTable.Append("<tr>");
                                //sbTable.Append("<td>Convenience Fee :</td>");
                                //sbTable.Append("<td>" + ConvenienceFee + " INR</td>");
                                //sbTable.Append("</tr>");

                                //if (AppointmentType.ToString().Equals("Home"))
                                //{
                                //    sbTable.Append("<tr>");
                                //    sbTable.Append("<td>Home Delivery :</td>");
                                //    sbTable.Append("<td>" + HomeDeliveryCharge + " INR</td>");
                                //    sbTable.Append("</tr>");
                                //}




                                //sbTable.Append("<tr>");
                                //sbTable.Append("<td>Gross Total :</td>");
                                //sbTable.Append("<td>" + SuperTagAmount + " INR</td>");
                                //sbTable.Append("</tr>");





                                sbTable.Append("<tr style='line-height:80%;'>");
                                sbTable.Append("<td>Grand Total :</td>");
                                sbTable.Append("<td>" + totalamount + " INR</td>");
                                sbTable.Append("</tr>");
                            }


                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Billing Name :</td>");
                            sbTable.Append("<td>" + OwnerName + "</td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr style='line-height:80%;'>");
                            sbTable.Append("<td>Billing Mobile</td>");
                            sbTable.Append("<td>" + MobileNo + "</td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr style='line-height:80%;'>");
                            sbTable.Append("<td>Billing Email ID:</td>");
                            sbTable.Append("<td>" + EmailID + "</td>");
                            sbTable.Append("</tr>");

                            if (OrderStatus.ToString().Equals("Success") || OrderStatus.ToString().Equals("Shipped"))
                            {
                                sbTable.Append("<tr>");
                                sbTable.Append("<td><b>Delivery Time</b></td>");
                                sbTable.Append("<td><b>7 to 10 days</b></td>");
                                sbTable.Append("</tr>");

                                //sbTable.Append("<tr>");
                                //sbTable.Append("<td>Receipt is valid till </td>");
                                //sbTable.Append("<td><b>" + ReceiptValidUpTo + "</b></td>");
                                //sbTable.Append("</tr>")

                                //sbTable.Append("<tr>");
                                //sbTable.Append("<td>Fitment Location :</td>");
                                //sbTable.Append("<td><b><h4>" + DealerAffixationCenterName + "</b></h4></td>");
                                //sbTable.Append("</tr>");

                                sbTable.Append("<tr>");
                                sbTable.Append("<td>Shipping Address :</td>");
                                sbTable.Append("<td><b><h4>" + CustomerAddress1 + " " + CustomerCity + " " + CustomerPin + "</b></h4></td>");
                                sbTable.Append("</tr>");

                                //if (AppointmentType.ToString().Equals("Dealer"))
                                //{
                                //    sbTable.Append("<tr>");
                                //    sbTable.Append("<td>Contact Person at Fitment Center:</td>");
                                //    sbTable.Append("<td><b><h4>" + FitmentPersonName + "</b></h4></td>");
                                //    sbTable.Append("</tr>");

                                //    sbTable.Append("<tr>");
                                //    sbTable.Append("<td>Mobile No. :</td>");
                                //    sbTable.Append("<td><b><h4>" + FitmentPersonMobile + "</b></h4></td>");
                                //    sbTable.Append("</tr>");
                                //}
                            }


                            sbTable.Append("</table>");

                            sbTable.Append("<table>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>1.The <b>Park+FasTag will be shipped directly to the shipping address</b> and will not delivered with HSRP or Sticker.</td>");
                            sbTable.Append("</tr>");


                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>1. Carry this Receipt and RC Copy at the time of fitment.</td>");
                            //sbTable.Append("</tr>");

                            ////sbTable.Append("<tr>");
                            ////sbTable.Append("<td>2. If payment is not successful Process Booking Again.</td>");
                            ////sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>2. Re-Appointment (if any) will only available for future date </td>");
                            //sbTable.Append("</tr>");
                            //if (VehicleRegNo.Trim().ToLower().StartsWith("dl") == true)
                            //{


                            //    if (AppointmentType.ToString().Equals("Dealer"))
                            //    {
                            //        sbTable.Append("<tr>");
                            //        sbTable.Append("<td>3. For Dealer Point Grievance – please contact at +91 8929722201 (Calling time 9.30 AM to 6 PM (07 days) and Email ID is grievance@bookmyhsrp.com </td>");
                            //        sbTable.Append("</tr>");

                            //    }
                            //    if (AppointmentType.ToString().Equals("Home"))
                            //    {
                            //        sbTable.Append("<tr>");
                            //        sbTable.Append("<td>3. For Home Delivery Grievance – please contact at +91 8929722202 (Calling time 9.30AM - 6PM (07 days) and Email ID is homegrieviance@bookmyhsrp.com </td>");
                            //        sbTable.Append("</tr>");

                            //    }
                            //}
                            //else
                            //{


                            //    if (AppointmentType.ToString().Equals("Dealer"))
                            //    {
                            //        sbTable.Append("<tr>");
                            //        sbTable.Append("<td>3. For Dealer Point Grievance – please contact at +91 8929722203(Calling time 9.30AM - 6PM (07 days) and Email ID is grievance.up@bookmyhsrp.com. </td>");
                            //        sbTable.Append("</tr>");

                            //    }
                            //    if (AppointmentType.ToString().Equals("Home"))
                            //    {
                            //        sbTable.Append("<tr>");
                            //        sbTable.Append("<td>3. For Dealer Point Grievance – please contact at +91 892722204(Calling time 9.30 AM to 6 PM (07 days) and Email ID is homegrievance@bookmyhsrp.com. </td>");
                            //        sbTable.Append("</tr>");

                            //    }
                            //}


                            ////sbTable.Append("<tr>");
                            ////sbTable.Append("<td><b>5. This slip is valid for 15 days from the date of appointment.<b></td>");
                            ////sbTable.Append("</tr>");
                            sbTable.Append("</table>");
                        }

                        if (isFramrTag == "Y")
                        {
                            sbTable.Append("<br/>"); sbTable.Append("<br/>");
                            sbTable.Append("<br/>"); sbTable.Append("<br/>");
                            sbTable.Append("<br/>"); sbTable.Append("<br/>");
                            sbTable.Append("<br/>"); sbTable.Append("<br/>");
                            sbTable.Append("<br/>"); sbTable.Append("<br/>");
                            sbTable.Append("<br/>"); sbTable.Append("<br/>");
                            sbTable.Append("<br/>"); sbTable.Append("<br/>");
                            sbTable.Append("<br/>"); sbTable.Append("<br/>");
                            sbTable.Append("<br/>"); sbTable.Append("<br/>");
                            sbTable.Append("<br/>"); sbTable.Append("<br/>");
                            sbTable.Append("<br/>"); sbTable.Append("<br/>");
                            sbTable.Append("<br/>"); sbTable.Append("<br/>");
                            sbTable.Append("<br/>"); sbTable.Append("<br/>");




                            sbTable.Append("<table>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td valign='bottom' width='50%' ><b><h4>Receipt of Payment of Frame</h4></b></td>");
                            sbTable.Append("<td rowspan=2><img src='" + ReceiptPathQRCode + "' id='img_qrcode'></td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr><td><p> <br> Rosmerta Safety Systems Pvt.Ltd. <br> https://bookmyhsrp.com </p></br>GST No.: " + dtGSTIN.Rows[0]["GSTIN"].ToString() + "</td></tr>");
                            //sbTable.Append("<tr><td>GST No.:" + dtGSTIN.Rows[0]["GSTIN"].ToString() + "</td></tr>");
                            sbTable.Append("</table>");

                            sbTable.Append("<table style='font-size:medium;>");
                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Order Date :</td>");
                            sbTable.Append("<td>" + OrderDate + "</td>");
                            sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>Vehicle Reg No :</td>");
                            //sbTable.Append("<td><b>" + VehicleRegNo + "</b></td>");
                            //sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>Vehicle Fuel :</td>");
                            //sbTable.Append("<td>" + fuelType + "</td>");
                            //sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>Vehicle Class :</td>");
                            //sbTable.Append("<td>" + VehicleClass + "</td>");
                            //sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>Vehicle Type :</td>");
                            //sbTable.Append("<td>" + VehicleType + "</td>");
                            //sbTable.Append("</tr>");

                            sbTable.Append("<tr style='line-height:80%;'>");
                            sbTable.Append("<td><b>Order ID :</b></td>");
                            sbTable.Append("<td><b>" + OrderNo + "</b></td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr style='line-height:80%;'>");
                            sbTable.Append("<td>Order Status :</td>");
                            sbTable.Append("<td><b>" + OrderStatus + "</b></td>");
                            sbTable.Append("</tr>");

                            if (OrderStatus.ToString().Equals("Success") || OrderStatus.ToString().Equals("Shipped"))
                            {
                                sbTable.Append("<tr>");
                                sbTable.Append("<td><b>Frame charges  :</b></td>");
                                sbTable.Append("<td><b>" + string.Format("{0:0.00}", (Convert.ToDecimal(FrameTagAmount)).ToString()) + " INR</b></td>");
                                sbTable.Append("</tr>");

                                if (IGSTAmount != 0)
                                {

                                    sbTable.Append("<tr style='line-height:80%;'>");
                                    sbTable.Append("<td>IGST Amount :</td>");
                                    sbTable.Append("<td>" + string.Format("{0:0.00}", IGSTAmountFrm) + " INR</td>");
                                    sbTable.Append("</tr>");
                                }

                                if (CGSTAmount != 0)
                                {
                                    sbTable.Append("<tr style='line-height:80%;'>");
                                    sbTable.Append("<td>CGST Amount :</td>");
                                    sbTable.Append("<td>" + string.Format("{0:0.00}", CGSTAmountFrm) + " INR</td>");
                                    sbTable.Append("</tr>");
                                }

                                if (SGSTAmount != 0)
                                {

                                    sbTable.Append("<tr style='line-height:80%;'>");
                                    sbTable.Append("<td>SGST Amount :</td>");
                                    sbTable.Append("<td>" + string.Format("{0:0.00}", SGSTAmountFrm) + " INR</td>");
                                    sbTable.Append("</tr>");
                                }

                                //sbTable.Append("<tr>");
                                //sbTable.Append("<td>Sub Total :</td>");
                                //sbTable.Append("<td>" + ((Convert.ToDecimal(FrameTagAmount) ) + Convert.ToDecimal(IGSTAmountFrm) + Convert.ToDecimal(CGSTAmountFrm) + Convert.ToDecimal(SGSTAmountFrm)).ToString() + " INR</td>");
                                //sbTable.Append("</tr>");

                                //sbTable.Append("<tr>");
                                //sbTable.Append("<td>Convenience Fee :</td>");
                                //sbTable.Append("<td>" + ConvenienceFee + " INR</td>");
                                //sbTable.Append("</tr>");

                                //if (AppointmentType.ToString().Equals("Home"))
                                //{
                                //    sbTable.Append("<tr>");
                                //    sbTable.Append("<td>Home Delivery :</td>");
                                //    sbTable.Append("<td>" + HomeDeliveryCharge + " INR</td>");
                                //    sbTable.Append("</tr>");
                                //}

                                //sbTable.Append("<tr>");
                                //sbTable.Append("<td>Gross Total :</td>");
                                //sbTable.Append("<td>" + SuperTagAmount + " INR</td>");
                                //sbTable.Append("</tr>");

                                sbTable.Append("<tr style='line-height:80%;'>");
                                sbTable.Append("<td>Grand Total :</td>");
                                sbTable.Append("<td>" + Frametotalamount + " INR</td>");
                                sbTable.Append("</tr>");
                            }

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Billing Name :</td>");
                            sbTable.Append("<td>" + OwnerName + "</td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr style='line-height:80%;'>");
                            sbTable.Append("<td>Billing Mobile</td>");
                            sbTable.Append("<td>" + MobileNo + "</td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr style='line-height:80%;'>");
                            sbTable.Append("<td>Billing Email ID:</td>");
                            sbTable.Append("<td>" + EmailID + "</td>");
                            sbTable.Append("</tr>");

                            if (OrderStatus.ToString().Equals("Success") || OrderStatus.ToString().Equals("Shipped"))
                            {
                                //sbTable.Append("<tr>");
                                //sbTable.Append("<td>Receipt is valid till </td>");
                                //sbTable.Append("<td><b>" + ReceiptValidUpTo + "</b></td>");
                                //sbTable.Append("</tr>");

                                sbTable.Append("<tr style='line-height:80%;'>");
                                sbTable.Append("<td>Fitment Location :</td>");
                                sbTable.Append("<td><b>" + DealerAffixationCenterName + "</b></td>");
                                sbTable.Append("</tr>");

                                sbTable.Append("<tr style='line-height:80%;'>");
                                sbTable.Append("<td>Shipping Address :</td>");
                                sbTable.Append("<td><b>" + DealerAffixationCenterAddress + "</b></td>");
                                sbTable.Append("</tr>");
                            }

                            sbTable.Append("</table>");
                            sbTable.Append("<table>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>1.The <b>Frame will be shipped with HSRP to the shipping address</td>");
                            sbTable.Append("</tr>");


                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>1. Carry this Receipt and RC Copy at the time of fitment.</td>");
                            //sbTable.Append("</tr>");

                            ////sbTable.Append("<tr>");
                            ////sbTable.Append("<td>2. If payment is not successful Process Booking Again.</td>");
                            ////sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>2. Re-Appointment (if any) will only available for future date </td>");
                            //sbTable.Append("</tr>");
                            //if (VehicleRegNo.Trim().ToLower().StartsWith("dl") == true)
                            //{


                            //    if (AppointmentType.ToString().Equals("Dealer"))
                            //    {
                            //        sbTable.Append("<tr>");
                            //        sbTable.Append("<td>3. For Dealer Point Grievance – please contact at +91 8929722201 (Calling time 9.30 AM to 6 PM (07 days) and Email ID is grievance@bookmyhsrp.com </td>");
                            //        sbTable.Append("</tr>");

                            //    }
                            //    if (AppointmentType.ToString().Equals("Home"))
                            //    {
                            //        sbTable.Append("<tr>");
                            //        sbTable.Append("<td>3. For Home Delivery Grievance – please contact at +91 8929722202 (Calling time 9.30AM - 6PM (07 days) and Email ID is homegrieviance@bookmyhsrp.com </td>");
                            //        sbTable.Append("</tr>");

                            //    }
                            //}
                            //else
                            //{


                            //    if (AppointmentType.ToString().Equals("Dealer"))
                            //    {
                            //        sbTable.Append("<tr>");
                            //        sbTable.Append("<td>3. For Dealer Point Grievance – please contact at +91 8929722203(Calling time 9.30AM - 6PM (07 days) and Email ID is grievance.up@bookmyhsrp.com. </td>");
                            //        sbTable.Append("</tr>");

                            //    }
                            //    if (AppointmentType.ToString().Equals("Home"))
                            //    {
                            //        sbTable.Append("<tr>");
                            //        sbTable.Append("<td>3. For Dealer Point Grievance – please contact at +91 892722204(Calling time 9.30 AM to 6 PM (07 days) and Email ID is homegrievance@bookmyhsrp.com. </td>");
                            //        sbTable.Append("</tr>");

                            //    }
                            //}


                            ////sbTable.Append("<tr>");
                            ////sbTable.Append("<td><b>5. This slip is valid for 15 days from the date of appointment.<b></td>");
                            ////sbTable.Append("</tr>");
                            sbTable.Append("</table>");
                        }

                        #region supertag_INvoice
                        //if (_isSuperTag == "Y")
                        //{


                        //    sbTable.Append("<br/><br/><br/><br/><table width='100%' border='1' style =\"border: 1px solid black;border-collapse:collapse\">");
                        //    sbTable.Append("<tr>");
                        //    sbTable.Append("<td colspan=\"7\" valign='bottom' width='100%' ><b><h4 align='center'>Tax Invoice</h4></b></td>");
                        //    sbTable.Append("</tr>");
                        //    sbTable.Append("<tr>");
                        //    sbTable.Append("<td colspan=\"7\" valign='bottom' width='100%' ><b><h4  align='center'>Rule 46 of CGST Rules</h4></b></td>");
                        //    sbTable.Append("</tr>");

                        //    sbTable.Append("<tr>");
                        //    sbTable.Append("<td colspan=\"7\" style='text-align:center' align='center'><h2 align='center'>ABSOLUTE INFRACON PVT. LTD.</h2>");
                        //    sbTable.Append("<br />FIRST FLOOR,137 UDYOG VIHAR GURGAON");
                        //    sbTable.Append("<br />ARN NUMBER - 077AAACU4747P1ZR");
                        //    // sbTable.Append("<br />GSTIN - U31909DL2001PTC171519");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("</tr>");

                        //    sbTable.Append("<tr>");
                        //    //sbTable.Append("<td colspan=\"2\">Dispatch From -");
                        //    //sbTable.Append("<br />ROSMERTA SAFETY SYSTEMS PRIVATE LIMITED");
                        //    //sbTable.Append("<br />94/4 baba colony, Burari");
                        //    //sbTable.Append("</td>");

                        //    sbTable.Append("<td colspan=\"7\">Invoice  No - " + OrderNo + "");
                        //    sbTable.Append("<br />Date -" + OrderDate + "");
                        //    //sbTable.Append("<br />Reference No -");
                        //    //sbTable.Append("<br />Mode of Transport -");
                        //    //sbTable.Append("<br />Transport Name -");
                        //    //sbTable.Append("<br />GR/LR  No -");
                        //    //sbTable.Append("<br />Vehicle No -");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("</tr>");

                        //    sbTable.Append("<tr>");
                        //    sbTable.Append("<td colspan=\"2\">Bill to -");
                        //    sbTable.Append("<br />" + BILLEDADDRESS);
                        //    //  sbTable.Append("<br />M2-55 attam Vallabh CGHS sec 13 rohino DELHI");
                        //    sbTable.Append("</td>");

                        //    sbTable.Append("<td colspan=\"5\">Ship To - ");
                        //    sbTable.Append("<br /> " + CustomerAddress1 + " " + CustomerCity + " " + CustomerPin);
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("</tr>");

                        //    sbTable.Append("<tr>");
                        //    sbTable.Append("<td colspan=\"1\"><h3>SL. NO.</h3></td>");
                        //    sbTable.Append("<td colspan=\"2\"><h3>Descrition of Goods</h3></td>");
                        //    sbTable.Append("<td colspan=\"1\"><h3>HSN Code</h3></td>");
                        //    //sbTable.Append("<td ><h3>UOM</h3>");
                        //    // sbTable.Append("</td>");
                        //    sbTable.Append("<td colspan=\"1\"><h3>City</h3>");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("<td colspan=\"1\"><h3>Rate Per <br/> Unit</h3>");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("<td colspan=\"1\"><h3>Taxable <br/>Amount<br>(Rs.)</h3>");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("</tr>");

                        //    sbTable.Append("<tr>");
                        //    sbTable.Append("<td <td colspan=\"1\">1");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("<td  <td colspan=\"2\">Super Tag");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("<td <td colspan=\"1\"><h3>HSN Code</h3>");
                        //    sbTable.Append("</td>");
                        //    //sbTable.Append("<td style= 'width:10%'><h3>UOM</h3>");
                        //    //sbTable.Append("</td>");
                        //    sbTable.Append("<td <td colspan=\"1\">" + CustomerCity);
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("<td <td colspan=\"1\">" + SuperTagAmount);
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("<td <td colspan=\"1\"'>" + SuperTagAmount);
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("</tr>");

                        //    //sbTable.Append("<tr>");
                        //    //sbTable.Append("<td colspan=\"7\">");
                        //    //sbTable.Append("</td>");
                        //    //sbTable.Append("</tr>");

                        //    sbTable.Append("<tr >");
                        //    sbTable.Append("<td colspan=\"5\">");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("<td >Sub Total");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("<td >" + SuperTagAmount);
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("</tr>");

                        //    sbTable.Append("<tr >");
                        //    sbTable.Append("<td colspan=\"4\">");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("<td >IGST @");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("<td > 18.00%");
                        //    sbTable.Append("</td>");
                        //    if (IGSTAmount != 0)
                        //    {
                        //        sbTable.Append("<td >" + IGSTAmountST);
                        //    }
                        //    else
                        //    { sbTable.Append("<td >" + "0.00"); }
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("</tr>");

                        //    sbTable.Append("<tr >");
                        //    sbTable.Append("<td colspan=\"4\">");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("<td >CGST @");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("<td > 9%");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("<td >" + CGSTAmountST);
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("</tr>");

                        //    sbTable.Append("<tr >");
                        //    sbTable.Append("<td colspan=\"4\">");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("<td >UTGST/<br/>SGST @");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("<td > 9%");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("<td > " + SGSTAmountST);
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("</tr>");

                        //    sbTable.Append("<tr >");
                        //    //sbTable.Append("<td>");
                        //    //sbTable.Append("</td>");
                        //    //sbTable.Append("<td>Whether the payable on reverse charge mechanism- NO");
                        //    //sbTable.Append("</td>");
                        //    sbTable.Append("<td colspan=\"6\"><h3>Total Value<br/>Total Round off Value</h3>");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("<td >" + totalamount + "<br>" + totalamount);
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("</tr>");

                        //    //sbTable.Append("<tr>");
                        //    //sbTable.Append("<td colspan=\"7\"> Total amount in words : One thousand ");
                        //    //sbTable.Append("</td>");
                        //    //sbTable.Append("</tr>");


                        //    //sbTable.Append("<tr>");
                        //    //sbTable.Append("<td colspan=\"7\"> <h3>VehicleRegNo." + VehicleRegNo + "</h3> ");
                        //    //sbTable.Append("</td>");
                        //    //sbTable.Append("</tr>");

                        //    //sbTable.Append("<tr>");
                        //    //sbTable.Append("<td colspan=\"7\">");
                        //    //sbTable.Append("</td>");
                        //    //sbTable.Append("</tr>");

                        //    //sbTable.Append("<tr>");
                        //    //sbTable.Append("<td colspan=\"7\">");
                        //    //sbTable.Append("</td>");
                        //    //sbTable.Append("</tr>");

                        //    //sbTable.Append("<tr>");
                        //    //sbTable.Append("<td colspan=\"7\">");
                        //    //sbTable.Append("</td>");
                        //    //sbTable.Append("</tr>");
                        //    //sbTable.Append("<tr>");
                        //    sbTable.Append("<td colspan=\"4\">Declaration : certified that the particulers given above are true and correct and the amount indicated represents the price actualy charged and their is no additional flow of money either directaly or indirectaly from the recipient. <br/>" +
                        //        "The invoice for the super tag only is attached here.<br/><br/><br/>E & O E");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("<tr>");
                        //    sbTable.Append("<td colspan=\"3\">For ABSOLUTE INFRACON PVT. LTD.<br/><br/><br/><br/>Authorize Signature");
                        //    sbTable.Append("</td>");
                        //    sbTable.Append("</tr>");
                        //    sbTable.Append("</table>");


                        //}

                        #endregion
                        //createpdfreceipt(sbTable.ToString(), BookingHistoryID);
                        try
                        {
                            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
                            #region
                            //string MonthYears = DateTime.Now.ToString("MMM-yyyy");
                            //string folderpath = ConfigurationManager.AppSettings["ReceiptPath"].ToString() + "/Plate/" + MonthYears + "/";

                            //if (!Directory.Exists(folderpath))
                            //{
                            //    Directory.CreateDirectory(folderpath);
                            //}

                            //string filename = OrderNo + DateTime.Now.ToString("ddMMyyyyHHmmssfff") + ".pdf";
                            //string PdfFolder = folderpath + filename;
                            //string filepathtosave = "Plate/" + MonthYears + "/" + filename; //for saving into database

                            PdfWriter.GetInstance(pdfDoc, new FileStream(PdfFolder, FileMode.Create));
                            pdfDoc.Open();
#pragma warning disable CS0612 // 'HTMLWorker' is obsolete
                            var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(sbTable.ToString()), null);
#pragma warning restore CS0612 // 'HTMLWorker' is obsolete
                            foreach (var htmlElement in parsedHtmlElements)
                                pdfDoc.Add(htmlElement as IElement);


                            pdfDoc.Close();



                            //ReceiptCompletePath = PdfFolder; //comment by dhiru
                            //ReceiptCompletePath = ConfigurationManager.AppSettings["ReceiptDirectory"].ToString() + filepathtosave;
                            #region Nlog Variables
                            //Logger logger = LogManager.GetLogger("databaseLogger");
                            StringBuilder str = new StringBuilder();
                            string ip = string.Empty;
                            #endregion

                            try
                            {
                                #region Nlog
                                foreach (string var in Request.ServerVariables)
                                {
                                    str.Append(var + ":" + Request[var] + "@");
                                }

                           //     ip = Request.ServerVariables["REMOTE_ADDR"];
                           //     logger.WithProperty("RazorpayOrderid", "")
                           //    .WithProperty("Regno", VehicleRegNo)
                           //    .WithProperty("OrderNo", OrderNo)
                           //    .WithProperty("Email", EmailID)
                           //     .WithProperty("Remoteip", ip)
                           //      .WithProperty("OrderStatus", OrderStatus)
                           //       .WithProperty("Status", "Receipt Generated Successfully")
                           //.Info(str.ToString());
                                #endregion
                                string updateReceiptQuery = "update Appointment_bookingHist set ReceiptPath = N'" + filepathtosave + "' where BookingHistoryID = '" + BookingHistoryID + "' ";
                                Utils.ExecNonQuery(updateReceiptQuery, ConnectionString);
                            }
#pragma warning disable CS0168 // The variable 'ev' is declared but never used
                            catch (Exception ev)
#pragma warning restore CS0168 // The variable 'ev' is declared but never used
                            {
                                //Response.Write(ev.ToString());
                            }
                            string ReceiptPathForQRCode = ConfigurationManager.AppSettings["ReceiptDirectory"].ToString() + filepathtosave;

                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BarCodeDisplay('" + ReceiptPathForQRCode + "')", true);

                            #endregion
                        }
#pragma warning disable CS0168 // The variable 'ev' is declared but never used
                        catch (Exception ev)
#pragma warning restore CS0168 // The variable 'ev' is declared but never used
                        {
                            //Response.Write(ev.ToString());
                        }

                        #endregion

                    }
                    else
                    {
                        lblMessage.Text = "Error:: " + dtReceipt.Rows[0]["message"].ToString();
                        lblMessage.Visible = true;
                    }
                }
                else
                {
                    lblMessage.Text = "Error: Invalid OrderNo.";
                    lblMessage.Visible = true;
                }

            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                //Response.Write(ex.ToString());
                lblMessage.Text = "Receipt : Your Session has been expired, Please fill the details again.";
                lblMessage.Visible = true;
                return;
            }



        }


        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //string folderpath = ConfigurationManager.AppSettings["InvoiceFolder"].ToString();
            //ReceiptCompletePath 

            //string OrderNo = string.Empty;
            //OrderNo = lblOrderNo.Text.ToString();

            //string sqlQuery = "select ReceiptPath, BookingHistoryID from [BookMyHSRP].dbo.Appointment_BookingHist where OrderNo = '" + lblOrderNo.Text.ToString() + "' ";
            string checkReceiptQuery = "select isnull(ReceiptPath,'') FileName, BookingHistoryID from Appointment_bookingHist where OrderNo = '" + lblOrderId.Text.ToString() + "'";
            DataTable dtFileName = Utils.GetDataTable(checkReceiptQuery, ConnectionString);
            if (dtFileName.Rows.Count > 0)
            {
                #region
                string FileName = dtFileName.Rows[0]["FileName"].ToString();
                string folderpath = ConfigurationManager.AppSettings["ReceiptPath"].ToString();
                string PdfFolder = folderpath + FileName;
                if (FileName.Length > 0 && File.Exists(PdfFolder))
                {
                    string[] arr = FileName.ToString().Split('/');
                    int i = arr.Length;

                    //string PdfSignedFolder = signFolderPath + arr[i - 1].ToString();
                    string filename = arr[i - 1].ToString();
                    #region
                    try
                    {
                        HttpContext context = HttpContext.Current;
                        context.Response.ContentType = "Application/pdf";
                        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                        context.Response.WriteFile(PdfFolder);
                        context.Response.End();
                    }
                    catch (Exception ev)
                    {
                        lblMessage.Text = ev.Message;
                        lblMessage.Visible = true;
                    }
                    #endregion
                }
                else
                {
                    lblMessage.Text = "Receipt not Generated.";
                    lblMessage.Visible = true;
                }
                #endregion
            }

                //if (ReceiptCompletePath.Length > 0 && File.Exists(ReceiptCompletePath))
                //{

                //    string[] arr = ReceiptCompletePath.ToString().Split('/');
                //    int i = arr.Length;

                //    //string PdfSignedFolder = signFolderPath + arr[i - 1].ToString();
                //    string filename = arr[i - 1].ToString();
                //    #region
                //    try
                //    {
                //        HttpContext context = HttpContext.Current;
                //        context.Response.ContentType = "Application/pdf";
                //        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                //        context.Response.WriteFile(ReceiptCompletePath);
                //        context.Response.End();
                //    }
                //    catch (Exception ev)
                //    {
                //        string temp = ev.Message;
                //    }
                //    #endregion
                //}
        }



        static void AddLog(string logtext)
        {
            string pathx = "BHSRP-" + System.DateTime.Now.Day.ToString() + "-" + System.DateTime.Now.Month.ToString() + "-" + System.DateTime.Now.Year.ToString() + ".log";
            string savepath = ConfigurationManager.AppSettings["LogFilePath"].ToString();
            string PdfFolder = Utils.SetFolder(savepath);

            using (StreamWriter sw = File.AppendText(PdfFolder + pathx))
            {
                sw.WriteLine("-------------------" + System.DateTime.Now.ToString() + "--------------------");
                sw.WriteLine(logtext);
                sw.WriteLine("-----------------------------------------------------------------------------");
                sw.Close();
            }
        }

    }
}