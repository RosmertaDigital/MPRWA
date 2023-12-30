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

namespace BMHSRPv2
{
    public partial class RePaymentReceipt : System.Web.UI.Page
    {
        static string ReceiptCompletePath = string.Empty;
        static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                    }


                    #region
                    

                    try
                    {

                        string PaymentConfirmationQuery = "RePaymentConfirmation '" + order_id + "','" + tracking_id + "','" + bank_ref_no + "','" + order_status + "','" + failure_message + "','" + payment_mode + "','" + card_name + "','" + status_code + "','" + status_message + "','" + amount + "','" + billing_name + "','" + billing_address + "','" + billing_state + "','" + billing_zip + "','" + billing_country + "','" + billing_tel + "','" + billing_email + "','" + delivery_name + "','" + delivery_address + "','" + delivery_city + "','" + delivery_state + "','" + delivery_zip + "','" + delivery_country + "','" + delivery_tel + "','" + merchant_param1 + "','" + merchant_param2 + "','" + merchant_param3 + "','" + merchant_param4 + "','" + merchant_param5 + "','" + vault + "','" + offer_type + "','" + offer_code + "','" + discount_value + "','" + mer_amount + "','" + eci_value + "','" + retry + "','" + response_code + "','" + billing_notes + "','" + trans_date + "','" + bin_country + "','" + trans_fee + "','" + service_tax + "','" + razor_payment_id + "','" + payment_gateway_type + "'";

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
                                //lblFitmentPersonName.Text = dtPaymentConfirmation.Rows[0]["FitmentPersonName"].ToString();
                                //lblFitmentPersonMobile.Text = dtPaymentConfirmation.Rows[0]["FitmentPersonMobile"].ToString();


                                //string Fullurl = "/PaymentReceiptPdf.aspx?OrderNo=" + order_id;
                                //frame1.Visible = true;
                                //frame1.Attributes["src"] = Fullurl;

                                
                                if(order_status == "Success" || order_status == "Shipped")
                                {
                                    ReceiptGeneration(dtPaymentConfirmation.Rows[0]["ReScheduleOrderNo"].ToString());

                                    try
                                    {
                                        #region new Receipt Generation block

                                        //if (Session["plateSticker"].ToString().ToUpper() == "STICKER")
                                        //{
                                        //    GenerateStickerInvoice(dtPaymentConfirmation.Rows[0]["OrderNo"].ToString());
                                        //}
                                        //if (Session["plateSticker"].ToString().ToUpper() == "PLATE")
                                        //{
                                        //    GeneratePlateInvoice(dtPaymentConfirmation.Rows[0]["OrderNo"].ToString());

                                        //}

                                        #endregion

                                        string SMSText = "Order ID is "+ dtPaymentConfirmation.Rows[0]["OrderNo"].ToString() + ", Vehicle Reg. No. "+ dtPaymentConfirmation.Rows[0]["VehicleRegNo"].ToString() + " has been rescheduled by you.";
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
            HiddenlblRescheduledOrderid.Value = OrderNo;
            string checkReceiptQuery = "select isnull(ReceiptPath,'') FileName from [BookMyHSRP].dbo.RescheduledAppointment where RescheduleOrderNo = '" + OrderNo + "'";
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
                string checkReceiptQuery = "RePaymentPlateReceipt '" + OrderNo + "' ";
                DataTable dtReceipt = Utils.GetDataTable(checkReceiptQuery, ConnectionString);
                if (dtReceipt.Rows.Count > 0)
                {
                    string Status = dtReceipt.Rows[0]["status"].ToString();
                    if (Status == "1")
                    {
                        #region
                        string BookingHistoryID = dtReceipt.Rows[0]["BookingHistoryID"].ToString();
                        string OrderDate = dtReceipt.Rows[0]["OrderDate"].ToString();
                        string VehicleRegNo = dtReceipt.Rows[0]["VehicleRegNo"].ToString();
                        string fuelType = dtReceipt.Rows[0]["fuelType"].ToString();
                        string VehicleClass = dtReceipt.Rows[0]["VehicleClass"].ToString();
                        string VehicleType = dtReceipt.Rows[0]["VehicleType"].ToString();
                        OrderNo = dtReceipt.Rows[0]["OrderNo"].ToString();
                        string TrackingID = dtReceipt.Rows[0]["TrackingID"].ToString();
                        string AppointmentType = dtReceipt.Rows[0]["AppointmentType"].ToString();
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

                        if (!Directory.Exists(folderpath))
                        {
                            Directory.CreateDirectory(folderpath);
                        }

                        string filename = OrderNo + DateTime.Now.ToString("ddMMyyyyHHmmssfff") + ".pdf";
                        string PdfFolder = folderpath + filename;
                        string filepathtosave = "Plate/" + MonthYears + "/" + filename; //for saving into database

                        string ReceiptPathQRCode = "https://chart.googleapis.com/chart?chs=80x80&cht=qr&chl=" + ConfigurationManager.AppSettings["ReceiptDirectory"].ToString() + filepathtosave + "&chld=L|1&choe=UTF-8";

                        StringBuilder sbTable = new StringBuilder();
                        sbTable.Clear();

                        sbTable.Append("<table>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td valign='bottom' width='50%' ><b><h4>Receipt of Payment & Re-Appointment</h4></b></td>");
                        sbTable.Append("<td rowspan=2><img src='" + ReceiptPathQRCode + "' id='img_qrcode'></td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr><td><p>HSRP Online Re-Appointment Transaction Reciept <br> Rosmerta Safety System Pvt.Ltd. <br> https://bookmyhsrp.com </p></td></tr>");
                        sbTable.Append("</table>");

                        sbTable.Append("<table>");
                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Order Date :</td>");
                        sbTable.Append("<td>" + OrderDate + "</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Vehicle Reg No :</td>");
                        sbTable.Append("<td><b>" + VehicleRegNo + "</b></td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Vehicle Fuel :</td>");
                        sbTable.Append("<td>" + fuelType + "</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Vehicle Class :</td>");
                        sbTable.Append("<td>" + VehicleClass + "</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Vehicle Type :</td>");
                        sbTable.Append("<td>" + VehicleType + "</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Order ID :</td>");
                        sbTable.Append("<td>" + OrderNo + "</td>");
                        sbTable.Append("</tr>");

                        //sbTable.Append("<tr>");
                        //sbTable.Append("<td>Bank Tracking ID :</td>");
                        //sbTable.Append("<td>" + TrackingID + "</td>");
                        //sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Order Status :</td>");
                        sbTable.Append("<td><b>" + OrderStatus + "</b></td>");
                        sbTable.Append("</tr>");

                        //sbTable.Append("<tr>");
                        //sbTable.Append("<td>Payment/Order Status/status_massage :</td>");
                        //sbTable.Append("<td>" + StatusMessage + "</td>");
                        //sbTable.Append("</tr>");


                        if (OrderStatus.ToString().Equals("Success") || OrderStatus.ToString().Equals("Shipped"))
                        {
                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Reschedule Charges :</td>");
                            sbTable.Append("<td>" + string.Format("{0:0.00}", GstBasic_Amt) + " INR</td>");
                            sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>Fitment Charges :</td>");
                            //sbTable.Append("<td>" + FitmentCharge + " INR</td>");
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


                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Gross Total :</td>");
                            sbTable.Append("<td>" + string.Format("{0:0.00}", GstBasic_Amt) + " INR</td>");
                            sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>GST Rate :</td>");
                            //sbTable.Append("<td>18% </td>");
                            //sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>GST Amount :</td>");
                            //sbTable.Append("<td>" + GSTAmount + " INR</td>");
                            //sbTable.Append("</tr>");
                            sbTable.Append("<tr>");
                            sbTable.Append("<td>GST Amount :</td>");
                            sbTable.Append("<td>" + string.Format("{0:0.00}", SGSTAmount) + " INR</td>");
                            sbTable.Append("</tr>");

                            //if (IGSTAmount != 0)
                            //{
                            //    //sbTable.Append("<tr>");
                            //    //sbTable.Append("<td>IGST :</td>");
                            //    //sbTable.Append("<td>18% </td>");
                            //    //sbTable.Append("</tr>");

                            //    sbTable.Append("<tr>");
                            //    sbTable.Append("<td>IGST Amount :</td>");
                            //    sbTable.Append("<td>" + string.Format("{0:0.00}", IGSTAmount) + " INR</td>");
                            //    sbTable.Append("</tr>");
                            //}

                            //if (CGSTAmount != 0)
                            //{
                            //    sbTable.Append("<tr>");
                            //    sbTable.Append("<td>CGST Amount :</td>");
                            //    sbTable.Append("<td>" + string.Format("{0:0.00}", CGSTAmount) + " INR</td>");
                            //    sbTable.Append("</tr>");
                            //}

                            //if (SGSTAmount != 0)
                            //{

                            //    sbTable.Append("<tr>");
                            //    sbTable.Append("<td>SGST Amount :</td>");
                            //    sbTable.Append("<td>" + string.Format("{0:0.00}", SGSTAmount) + " INR</td>");
                            //    sbTable.Append("</tr>");
                            //}

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Grand Total :</td>");
                            sbTable.Append("<td>" + NetAmount + " INR</td>");
                            sbTable.Append("</tr>");
                        }


                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Billing Name :</td>");
                        sbTable.Append("<td>" + OwnerName + "</td>");
                        sbTable.Append("</tr>");
                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Billing Mobile</td>");
                        sbTable.Append("<td>" + MobileNo + "</td>");
                        sbTable.Append("</tr>");
                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Billing Email ID:</td>");
                        sbTable.Append("<td>" + EmailID + "</td>");
                        sbTable.Append("</tr>");

                        if (OrderStatus.ToString().Equals("Success") || OrderStatus.ToString().Equals("Shipped"))
                        {
                            sbTable.Append("<tr>");
                            sbTable.Append("<td><b>Appointment Date Time</b></td>");
                            sbTable.Append("<td><b>" + SlotBookingDate + " " + SlotTime + "</b></td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Receipt is valid till </td>");
                            sbTable.Append("<td><b>" + ReceiptValidUpTo + "</b></td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Fitment Location :</td>");
                            sbTable.Append("<td><b><h4>" + DealerAffixationCenterName + "</b></h4></td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Fitment Address :</td>");
                            sbTable.Append("<td><b><h4>" + DealerAffixationCenterAddress + "</b></h4></td>");
                            sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>Contact Person at Fitment Center:</td>");
                            //sbTable.Append("<td><b><h4>" + FitmentPersonName + "</b></h4></td>");
                            //sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>Mobile No. :</td>");
                            //sbTable.Append("<td><b><h4>" + FitmentPersonMobile + "</b></h4></td>");
                            //sbTable.Append("</tr>");
                        }


                        sbTable.Append("</table>");
                        //sbTable.Append("<br><br><p align='left'><b>Note:</b><br><font color='red'>1. Carry this receipt, SMS and RC copy at the time of appointment.</font></br>");

                        sbTable.Append("<table>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>1. Carry this receipt and RC copy at the time of appointment.</td>");
                        sbTable.Append("</tr>");

                        //sbTable.Append("<tr>");
                        //sbTable.Append("<td>2. If payment is not successful Process Booking Again.</td>");
                        //sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>2. For queries, Please contact Toll Free 18001200201, Email ID : online@bookmyhsrp.com </td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>3. Calling time (9:30 AM to 6:00 PM) and day (Monday to Saturday) or Email at online@bookmyhsrp.com</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>4. Re-Appointment (if any) will only avaiable for future dates. </td>");
                        sbTable.Append("</tr>");

                        //sbTable.Append("<tr>");
                        //sbTable.Append("<td><b>5. This slip is valid for 15 days from the date of appointment.<b></td>");
                        //sbTable.Append("</tr>");

                        sbTable.Append("</table>");

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
                            var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(sbTable.ToString()), null);
                            foreach (var htmlElement in parsedHtmlElements)
                                pdfDoc.Add(htmlElement as IElement);
                            pdfDoc.Close();


                            ReceiptCompletePath = PdfFolder;
                            //ReceiptCompletePath = ConfigurationManager.AppSettings["ReceiptDirectory"].ToString() + filepathtosave;


                            try
                            {
                                string updateReceiptQuery = "update RescheduledAppointment set ReceiptPath = '" + filepathtosave + "' where RescheduledId = '" + BookingHistoryID + "' ";
                                Utils.ExecNonQuery(updateReceiptQuery, ConnectionString);
                            }
                            catch (Exception ev)
                            {

                            }
                            string ReceiptPathForQRCode = ConfigurationManager.AppSettings["ReceiptDirectory"].ToString() + filepathtosave;

                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BarCodeDisplay('" + ReceiptPathForQRCode + "')", true);

                            #endregion
                        }
                        catch (Exception ev)
                        {

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
            catch (Exception ex)
            {

                lblMessage.Text = "Receipt : Your Session has been expired, Please fill the details again.";
                lblMessage.Visible = true;
                return;
            }



        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //string folderpath = ConfigurationManager.AppSettings["InvoiceFolder"].ToString();
            //ReceiptCompletePath 
            #region old code 

            //if (ReceiptCompletePath.Length > 0 && File.Exists(ReceiptCompletePath))
            //    {

            //        string[] arr = ReceiptCompletePath.ToString().Split('/');
            //        int i = arr.Length;

            //        //string PdfSignedFolder = signFolderPath + arr[i - 1].ToString();
            //        string filename = arr[i - 1].ToString();
            //        #region
            //        try
            //        {
            //            HttpContext context = HttpContext.Current;
            //            context.Response.ContentType = "Application/pdf";
            //            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            //            context.Response.WriteFile(ReceiptCompletePath);
            //            context.Response.End();
            //        }
            //        catch (Exception ev)
            //        {
            //            string temp = ev.Message;
            //        }
            //        #endregion
            //    }

            #endregion


            #region New Code
           
            string checkReceiptQuery = "select isnull(ReceiptPath,'') FileName from [BookMyHSRP].dbo.RescheduledAppointment where RescheduleOrderNo = '" + HiddenlblRescheduledOrderid.Value.ToString() + "'";
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

            #endregion
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

        private void GeneratePlateInvoice(string OrderNo)
        {
            try
            {
                string checkReceiptQuery = "PaymentPlateReceipt '" + OrderNo + "' ";
                DataTable dtReceipt = BMHSRPv2.Models.Utils.GetDataTable(checkReceiptQuery, ConnectionString);
                if (dtReceipt.Rows.Count > 0)
                {
                    string Status = dtReceipt.Rows[0]["status"].ToString();
                    if (Status == "1")
                    {
                        #region
                        string BookingHistoryID = dtReceipt.Rows[0]["BookingHistoryID"].ToString();
                        string OrderDate = dtReceipt.Rows[0]["OrderDate"].ToString();
                        string VehicleRegNo = dtReceipt.Rows[0]["VehicleRegNo"].ToString();
                        string fuelType = dtReceipt.Rows[0]["fuelType"].ToString();
                        string VehicleClass = dtReceipt.Rows[0]["VehicleClass"].ToString();
                        string VehicleType = dtReceipt.Rows[0]["VehicleType"].ToString();
                        OrderNo = dtReceipt.Rows[0]["OrderNo"].ToString();
                        string TrackingID = dtReceipt.Rows[0]["TrackingID"].ToString();
                        string AppointmentType = dtReceipt.Rows[0]["AppointmentType"].ToString();
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
                        string folderpath = ConfigurationManager.AppSettings["ReceiptPath"].ToString() + "/" + Session["plateSticker"].ToString() + "/" + MonthYears + "/";

                        if (!Directory.Exists(folderpath))
                        {
                            Directory.CreateDirectory(folderpath);
                        }

                        string filename = OrderNo + DateTime.Now.ToString("ddMMyyyyHHmmssfff") + ".pdf";
                        string PdfFolder = folderpath + filename;
                        string filepathtosave = "Plate/" + MonthYears + "/" + filename; //for saving into database

                        string ReceiptPathQRCode = "https://chart.googleapis.com/chart?chs=80x80&cht=qr&chl=" + ConfigurationManager.AppSettings["ReceiptDirectory"].ToString() + filepathtosave + "&chld=L|1&choe=UTF-8";

                        StringBuilder sbTable = new StringBuilder();
                        sbTable.Clear();

                        sbTable.Append("<table>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td valign='bottom' width='50%' ><b><h4>Receipt of Payment & Appointment</h4></b></td>");
                        sbTable.Append("<td rowspan=2><img src='" + ReceiptPathQRCode + "' id='img_qrcode'></td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr><td><p>HSRP Online Appointment Transaction Reciept <br> Rosmerta Safety System Pvt.Ltd. <br> https://bookmyhsrp.com </p></td></tr>");
                        sbTable.Append("</table>");

                        sbTable.Append("<table>");
                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Order Date :</td>");
                        sbTable.Append("<td>" + OrderDate + "</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Vehicle Reg No :</td>");
                        sbTable.Append("<td><b>" + VehicleRegNo + "</b></td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Vehicle Fuel :</td>");
                        sbTable.Append("<td>" + fuelType + "</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Vehicle Class :</td>");
                        sbTable.Append("<td>" + VehicleClass + "</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Vehicle Type :</td>");
                        sbTable.Append("<td>" + VehicleType + "</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Order ID :</td>");
                        sbTable.Append("<td>" + OrderNo + "</td>");
                        sbTable.Append("</tr>");

                        //sbTable.Append("<tr>");
                        //sbTable.Append("<td>Bank Tracking ID :</td>");
                        //sbTable.Append("<td>" + TrackingID + "</td>");
                        //sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Order Status :</td>");
                        sbTable.Append("<td><b>" + OrderStatus + "</b></td>");
                        sbTable.Append("</tr>");

                        //sbTable.Append("<tr>");
                        //sbTable.Append("<td>Payment/Order Status/status_massage :</td>");
                        //sbTable.Append("<td>" + StatusMessage + "</td>");
                        //sbTable.Append("</tr>");


                        if (OrderStatus.ToString().Equals("Success") || OrderStatus.ToString().Equals("Shipped"))
                        {
                            sbTable.Append("<tr>");
                            sbTable.Append("<td>HSRP set including Fitment charges :</td>");
                            sbTable.Append("<td>" + string.Format("{0:0.00}", GstBasic_Amt) + " INR</td>");
                            sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>Fitment Charges :</td>");
                            //sbTable.Append("<td>" + FitmentCharge + " INR</td>");
                            //sbTable.Append("</tr>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Convenience Fee :</td>");
                            sbTable.Append("<td>" + ConvenienceFee + " INR</td>");
                            sbTable.Append("</tr>");

                            if (AppointmentType.ToString().Equals("Home"))
                            {
                                sbTable.Append("<tr>");
                                sbTable.Append("<td>Home Delivery :</td>");
                                sbTable.Append("<td>" + HomeDeliveryCharge + " INR</td>");
                                sbTable.Append("</tr>");
                            }


                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Gross Total :</td>");
                            sbTable.Append("<td>" + TotalAmount + " INR</td>");
                            sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>GST Rate :</td>");
                            //sbTable.Append("<td>18% </td>");
                            //sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>GST Amount :</td>");
                            //sbTable.Append("<td>" + GSTAmount + " INR</td>");
                            //sbTable.Append("</tr>");
                            if (IGSTAmount != 0)
                            {
                                //sbTable.Append("<tr>");
                                //sbTable.Append("<td>IGST :</td>");
                                //sbTable.Append("<td>18% </td>");
                                //sbTable.Append("</tr>");

                                sbTable.Append("<tr>");
                                sbTable.Append("<td>IGST Amount :</td>");
                                sbTable.Append("<td>" + string.Format("{0:0.00}", IGSTAmount) + " INR</td>");
                                sbTable.Append("</tr>");
                            }

                            if (CGSTAmount != 0)
                            {
                                sbTable.Append("<tr>");
                                sbTable.Append("<td>CGST Amount :</td>");
                                sbTable.Append("<td>" + string.Format("{0:0.00}", CGSTAmount) + " INR</td>");
                                sbTable.Append("</tr>");
                            }

                            if (SGSTAmount != 0)
                            {

                                sbTable.Append("<tr>");
                                sbTable.Append("<td>SGST Amount :</td>");
                                sbTable.Append("<td>" + string.Format("{0:0.00}", SGSTAmount) + " INR</td>");
                                sbTable.Append("</tr>");
                            }

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Grand Total :</td>");
                            sbTable.Append("<td>" + NetAmount + " INR</td>");
                            sbTable.Append("</tr>");
                        }


                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Billing Name :</td>");
                        sbTable.Append("<td>" + OwnerName + "</td>");
                        sbTable.Append("</tr>");
                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Billing Mobile</td>");
                        sbTable.Append("<td>" + MobileNo + "</td>");
                        sbTable.Append("</tr>");
                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Billing Email ID:</td>");
                        sbTable.Append("<td>" + EmailID + "</td>");
                        sbTable.Append("</tr>");

                        if (OrderStatus.ToString().Equals("Success") || OrderStatus.ToString().Equals("Shipped"))
                        {
                            sbTable.Append("<tr>");
                            sbTable.Append("<td><b>Appointment Date Time</b></td>");
                            sbTable.Append("<td><b>" + SlotBookingDate + " " + SlotTime + "</b></td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Receipt is valid till </td>");
                            sbTable.Append("<td><b>" + ReceiptValidUpTo + "</b></td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Fitment Location :</td>");
                            sbTable.Append("<td><b><h4>" + DealerAffixationCenterName + "</b></h4></td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Fitment Address :</td>");
                            sbTable.Append("<td><b><h4>" + DealerAffixationCenterAddress + "</b></h4></td>");
                            sbTable.Append("</tr>");

                            if (AppointmentType.ToString().Equals("Dealer"))
                            {
                                sbTable.Append("<tr>");
                                sbTable.Append("<td>Contact Person at Fitment Center:</td>");
                                sbTable.Append("<td><b><h4>" + FitmentPersonName + "</b></h4></td>");
                                sbTable.Append("</tr>");

                                sbTable.Append("<tr>");
                                sbTable.Append("<td>Mobile No. :</td>");
                                sbTable.Append("<td><b><h4>" + FitmentPersonMobile + "</b></h4></td>");
                                sbTable.Append("</tr>");
                            }
                        }


                        sbTable.Append("</table>");
                        //sbTable.Append("<br><br><p align='left'><b>Note:</b><br><font color='red'>1. Carry this receipt, SMS and RC copy at the time of appointment.</font></br>");

                        sbTable.Append("<table>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>1. Carry this receipt and RC copy at the time of appointment.</td>");
                        sbTable.Append("</tr>");

                        //sbTable.Append("<tr>");
                        //sbTable.Append("<td>2. If payment is not successful Process Booking Again.</td>");
                        //sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>2. For queries, Please contact Toll Free 18001200201, Email ID : online@bookmyhsrp.com </td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>3. Calling time (9:30 AM to 6:00 PM) and day (Monday to Saturday) or Email at online@bookmyhsrp.com</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>4. Re-Appointment (if any) will only available for future dates. </td>");
                        sbTable.Append("</tr>");

                        //sbTable.Append("<tr>");
                        //sbTable.Append("<td><b>5. This slip is valid for 15 days from the date of appointment.<b></td>");
                        //sbTable.Append("</tr>");

                        sbTable.Append("</table>");

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
                            var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(sbTable.ToString()), null);
                            foreach (var htmlElement in parsedHtmlElements)
                                pdfDoc.Add(htmlElement as IElement);
                            pdfDoc.Close();


                            //ReceiptCompletePath = PdfFolder; //comment by dhiru
                            //ReceiptCompletePath = ConfigurationManager.AppSettings["ReceiptDirectory"].ToString() + filepathtosave;


                            try
                            {
                                string updateReceiptQuery = "update Appointment_bookingHist set ReceiptPath = '" + filepathtosave + "' where BookingHistoryID = '" + BookingHistoryID + "' ";
                                int i = BMHSRPv2.Models.Utils.ExecNonQuery(updateReceiptQuery, ConnectionString);
                                if (i > 0)
                                {
                                    string updateQuery = "update top (1) BookMyHSRP.dbo.RescheduledAppointment set ReceiptPath = '" + filepathtosave + "' where RescheduledId IN ( SELECT TOP 1 RescheduledId FROM  BookMyHSRP.dbo.RescheduledAppointment WHERE OrderNo= '" + OrderNo + "'  order by RescheduledId desc) ";

                                    BMHSRPv2.Models.Utils.ExecNonQuery(updateQuery, ConnectionString);
                                }
                            }
                            catch (Exception ev)
                            {

                            }
                            string ReceiptPathForQRCode = ConfigurationManager.AppSettings["ReceiptDirectory"].ToString() + filepathtosave;

                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BarCodeDisplay('" + ReceiptPathForQRCode + "')", true);

                            #endregion
                        }
                        catch (Exception ev)
                        {

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
            catch (Exception ex)
            {

                lblMessage.Text = "Receipt : Your Session has been expired, Please fill the details again.";
                lblMessage.Visible = true;
                return;
            }



        }

        private void GenerateStickerInvoice(string OrderNo)
        {
            try
            {
                string checkReceiptQuery = "PaymentPlateReceipt '" + OrderNo + "' ";
                DataTable dtReceipt = BMHSRPv2.Models.Utils.GetDataTable(checkReceiptQuery, ConnectionString);
                if (dtReceipt.Rows.Count > 0)
                {
                    string Status = dtReceipt.Rows[0]["status"].ToString();
                    if (Status == "1")
                    {
                        #region
                        string BookingHistoryID = dtReceipt.Rows[0]["BookingHistoryID"].ToString();
                        string OrderDate = dtReceipt.Rows[0]["OrderDate"].ToString();
                        string VehicleRegNo = dtReceipt.Rows[0]["VehicleRegNo"].ToString();
                        string fuelType = dtReceipt.Rows[0]["fuelType"].ToString();
                        string VehicleClass = dtReceipt.Rows[0]["VehicleClass"].ToString();
                        string VehicleType = dtReceipt.Rows[0]["VehicleType"].ToString();
                        OrderNo = dtReceipt.Rows[0]["OrderNo"].ToString();
                        string TrackingID = dtReceipt.Rows[0]["TrackingID"].ToString();
                        string AppointmentType = dtReceipt.Rows[0]["AppointmentType"].ToString();
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
                        string folderpath = ConfigurationManager.AppSettings["ReceiptPath"].ToString() + "/Sticker/" + MonthYears + "/";

                        if (!Directory.Exists(folderpath))
                        {
                            Directory.CreateDirectory(folderpath);
                        }

                        string filename = OrderNo + DateTime.Now.ToString("ddMMyyyyHHmmssfff") + ".pdf";
                        string PdfFolder = folderpath + filename;
                        string filepathtosave = "Sticker/" + MonthYears + "/" + filename; //for saving into database

                        string ReceiptPathQRCode = "https://chart.googleapis.com/chart?chs=80x80&cht=qr&chl=" + ConfigurationManager.AppSettings["ReceiptDirectory"].ToString() + filepathtosave + "&chld=L|1&choe=UTF-8";

                        StringBuilder sbTable = new StringBuilder();
                        sbTable.Clear();

                        sbTable.Append("<table>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td valign='bottom' width='50%' ><b><h4>Receipt of Payment & Appointment</h4></b></td>");
                        sbTable.Append("<td rowspan=2><img src='" + ReceiptPathQRCode + "' id='img_qrcode'></td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr><td><p>HSRP Online Appointment Transaction Reciept <br> Rosmerta Safety System Pvt.Ltd. <br> https://bookmyhsrp.com </p></td></tr>");
                        sbTable.Append("</table>");

                        sbTable.Append("<table>");
                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Order Date :</td>");
                        sbTable.Append("<td>" + OrderDate + "</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Vehicle Reg No :</td>");
                        sbTable.Append("<td><b>" + VehicleRegNo + "</b></td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Vehicle Fuel :</td>");
                        sbTable.Append("<td>" + fuelType + "</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Vehicle Class :</td>");
                        sbTable.Append("<td>" + VehicleClass + "</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Vehicle Type :</td>");
                        sbTable.Append("<td>" + VehicleType + "</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Order ID :</td>");
                        sbTable.Append("<td>" + OrderNo + "</td>");
                        sbTable.Append("</tr>");

                        //sbTable.Append("<tr>");
                        //sbTable.Append("<td>Bank Tracking ID :</td>");
                        //sbTable.Append("<td>" + TrackingID + "</td>");
                        //sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Order Status :</td>");
                        sbTable.Append("<td><b>" + OrderStatus + "</b></td>");
                        sbTable.Append("</tr>");

                        //sbTable.Append("<tr>");
                        //sbTable.Append("<td>Payment/Order Status/status_massage :</td>");
                        //sbTable.Append("<td>" + StatusMessage + "</td>");
                        //sbTable.Append("</tr>");


                        if (OrderStatus.ToString().Equals("Success") || OrderStatus.ToString().Equals("Shipped"))
                        {
                            sbTable.Append("<tr>");
                            sbTable.Append("<td>3rd Sticker only including Fitment charges :</td>");
                            sbTable.Append("<td>" + string.Format("{0:0.00}", GstBasic_Amt) + " INR</td>");
                            sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>Fitment Charges :</td>");
                            //sbTable.Append("<td>" + FitmentCharge + " INR</td>");
                            //sbTable.Append("</tr>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Convenience Fee :</td>");
                            sbTable.Append("<td>" + ConvenienceFee + " INR</td>");
                            sbTable.Append("</tr>");

                            if (AppointmentType.ToString().Equals("Home"))
                            {
                                sbTable.Append("<tr>");
                                sbTable.Append("<td>Home Delivery :</td>");
                                sbTable.Append("<td>" + HomeDeliveryCharge + " INR</td>");
                                sbTable.Append("</tr>");
                            }

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Gross Total :</td>");
                            sbTable.Append("<td>" + TotalAmount + " INR</td>");
                            sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>GST Rate :</td>");
                            //sbTable.Append("<td>18% </td>");
                            //sbTable.Append("</tr>");

                            //sbTable.Append("<tr>");
                            //sbTable.Append("<td>GST Amount :</td>");
                            //sbTable.Append("<td>" + GSTAmount + " INR</td>");
                            //sbTable.Append("</tr>");
                            if (IGSTAmount != 0)
                            {
                                //sbTable.Append("<tr>");
                                //sbTable.Append("<td>IGST :</td>");
                                //sbTable.Append("<td>18% </td>");
                                //sbTable.Append("</tr>");

                                sbTable.Append("<tr>");
                                sbTable.Append("<td>IGST Amount :</td>");
                                sbTable.Append("<td>" + string.Format("{0:0.00}", IGSTAmount) + " INR</td>");
                                sbTable.Append("</tr>");
                            }

                            if (CGSTAmount != 0)
                            {
                                sbTable.Append("<tr>");
                                sbTable.Append("<td>CGST Amount :</td>");
                                sbTable.Append("<td>" + string.Format("{0:0.00}", CGSTAmount) + " INR</td>");
                                sbTable.Append("</tr>");
                            }

                            if (SGSTAmount != 0)
                            {

                                sbTable.Append("<tr>");
                                sbTable.Append("<td>SGST Amount :</td>");
                                sbTable.Append("<td>" + string.Format("{0:0.00}", SGSTAmount) + " INR</td>");
                                sbTable.Append("</tr>");
                            }

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Grand Total :</td>");
                            sbTable.Append("<td>" + NetAmount + " INR</td>");
                            sbTable.Append("</tr>");
                        }


                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Billing Name :</td>");
                        sbTable.Append("<td>" + OwnerName + "</td>");
                        sbTable.Append("</tr>");
                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Billing Mobile</td>");
                        sbTable.Append("<td>" + MobileNo + "</td>");
                        sbTable.Append("</tr>");
                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Billing Email ID:</td>");
                        sbTable.Append("<td>" + EmailID + "</td>");
                        sbTable.Append("</tr>");

                        if (OrderStatus.ToString().Equals("Success") || OrderStatus.ToString().Equals("Shipped"))
                        {
                            sbTable.Append("<tr>");
                            sbTable.Append("<td><b>Appointment Date Time</b></td>");
                            sbTable.Append("<td><b>" + SlotBookingDate + " " + SlotTime + "</b></td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Receipt is valid till </td>");
                            sbTable.Append("<td><b>" + ReceiptValidUpTo + "</b></td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Fitment Location :</td>");
                            sbTable.Append("<td><b><h4>" + DealerAffixationCenterName + "</b></h4></td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Fitment Address :</td>");
                            sbTable.Append("<td><b><h4>" + DealerAffixationCenterAddress + "</b></h4></td>");
                            sbTable.Append("</tr>");

                            if (AppointmentType.ToString().Equals("Dealer"))
                            {
                                sbTable.Append("<tr>");
                                sbTable.Append("<td>Fitment Person Name :</td>");
                                sbTable.Append("<td><b><h4>" + FitmentPersonName + "</b></h4></td>");
                                sbTable.Append("</tr>");

                                sbTable.Append("<tr>");
                                sbTable.Append("<td>Mobile No. :</td>");
                                sbTable.Append("<td><b><h4>" + FitmentPersonMobile + "</b></h4></td>");
                                sbTable.Append("</tr>");
                            }

                        }


                        sbTable.Append("</table>");
                        //sbTable.Append("<br><br><p align='left'><b>Note:</b><br><font color='red'>1. Carry this receipt, SMS and RC copy at the time of appointment.</font></br>");

                        sbTable.Append("<table>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>1. Carry this receipt and RC copy at the time of appointment.</td>");
                        sbTable.Append("</tr>");

                        //sbTable.Append("<tr>");
                        //sbTable.Append("<td>2. If payment is not successful Process Booking Again.</td>");
                        //sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>2. For queries, Please contact Toll Free 18001200201, Email ID : online@bookmyhsrp.com </td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>3. Calling time (9:30 AM to 6:00 PM) and day (Monday to Saturday) or Email at online@bookmyhsrp.com</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>4. Re-Appointment (if any) will only available for future dates. </td>");
                        sbTable.Append("</tr>");

                        //sbTable.Append("<tr>");
                        //sbTable.Append("<td><b>5. This slip is valid for 15 days from the date of appointment.<b></td>");
                        //sbTable.Append("</tr>");

                        sbTable.Append("</table>");

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
                            var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(sbTable.ToString()), null);
                            foreach (var htmlElement in parsedHtmlElements)
                                pdfDoc.Add(htmlElement as IElement);
                            pdfDoc.Close();


                            //ReceiptCompletePath = PdfFolder;
                            //ReceiptCompletePath = ConfigurationManager.AppSettings["ReceiptDirectory"].ToString() + filepathtosave;


                            try
                            {
                                string updateReceiptQuery = "update BookMyHSRP.dbo.Appointment_BookingHist set ReceiptPath = '" + filepathtosave + "' where BookingHistoryID = '" + BookingHistoryID + "' ";
                                int i = BMHSRPv2.Models.Utils.ExecNonQuery(updateReceiptQuery, ConnectionString);
                                if (i > 0)
                                {
                                    string updateQuery = "update top (1) BookMyHSRP.dbo.RescheduledAppointment set ReceiptPath = '" + filepathtosave + "' where RescheduledId IN ( SELECT TOP 1 RescheduledId FROM  BookMyHSRP.dbo.RescheduledAppointment WHERE OrderNo= '" + OrderNo + "'  order by RescheduledId desc) ";
                                    BMHSRPv2.Models.Utils.ExecNonQuery(updateQuery, ConnectionString);
                                }
                            }
                            catch (Exception ev)
                            {

                            }
                            string ReceiptPathForQRCode = ConfigurationManager.AppSettings["ReceiptDirectory"].ToString() + filepathtosave;

                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "BarCodeDisplay('" + ReceiptPathForQRCode + "')", true);

                            #endregion
                        }
                        catch (Exception ev)
                        {

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
            catch (Exception ex)
            {

                lblMessage.Text = "Receipt : Your Session has been expired, Please fill the details again.";
                lblMessage.Visible = true;
                return;
            }



        }

    }
}