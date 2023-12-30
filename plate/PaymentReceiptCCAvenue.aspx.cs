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
    public partial class PaymentReceiptCCAvenue : System.Web.UI.Page
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
                    StringBuilder sb = new StringBuilder();
                    #endregion

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

                    #region
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

                    try
                    {

                        string PaymentConfirmationQuery = "PaymentConfirmation '" + order_id + "','" + tracking_id + "','" + bank_ref_no + "','" + order_status + "','" + failure_message + "','" + payment_mode + "','" + card_name + "','" + status_code + "','" + status_message + "','" + amount + "','" + billing_name + "','" + billing_address + "','" + billing_state + "','" + billing_zip + "','" + billing_country + "','" + billing_tel + "','" + billing_email + "','" + delivery_name + "','" + delivery_address + "','" + delivery_city + "','" + delivery_state + "','" + delivery_zip + "','" + delivery_country + "','" + delivery_tel + "','" + merchant_param1 + "','" + merchant_param2 + "','" + merchant_param3 + "','" + merchant_param4 + "','" + merchant_param5 + "','" + vault + "','" + offer_type + "','" + offer_code + "','" + discount_value + "','" + mer_amount + "','" + eci_value + "','" + retry + "','" + response_code + "','" + billing_notes + "','" + trans_date + "','" + bin_country + "','" + trans_fee + "','" + service_tax + "'";

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
                                lblvalidity.Text = "Validity upto " + dtPaymentConfirmation.Rows[0]["ReceiptValidUpTo"].ToString();
                                lblFitmentCenterName.Text = dtPaymentConfirmation.Rows[0]["DealerAffixationCenterName"].ToString();
                                lblFitmentAddress.Text = dtPaymentConfirmation.Rows[0]["DealerAffixationCenterAddress"].ToString();


                                //string Fullurl = "/PaymentReceiptPdf.aspx?OrderNo=" + order_id;
                                //frame1.Visible = true;
                                //frame1.Attributes["src"] = Fullurl;

                                
                                if(order_status == "Success" || order_status == "Shipped")
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
                string checkReceiptQuery = "PaymentPlateReceipt '" + OrderNo + "' ";
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
                        string BasicAamount = dtReceipt.Rows[0]["BasicAamount"].ToString();
                        string FitmentCharge = dtReceipt.Rows[0]["FitmentCharge"].ToString();
                        string ConvenienceFee = dtReceipt.Rows[0]["ConvenienceFee"].ToString();
                        string HomeDeliveryCharge = dtReceipt.Rows[0]["HomeDeliveryCharge"].ToString();
                        string TotalAmount = dtReceipt.Rows[0]["TotalAmount"].ToString();
                        string GSTAmount = dtReceipt.Rows[0]["GSTAmount"].ToString();
                        string NetAmount = dtReceipt.Rows[0]["NetAmount"].ToString();
                        string OrderStatus = dtReceipt.Rows[0]["OrderStatus"].ToString();
                        string StatusMessage = dtReceipt.Rows[0]["StatusMessage"].ToString();
                        string OwnerName = dtReceipt.Rows[0]["OwnerName"].ToString();
                        string MobileNo = dtReceipt.Rows[0]["MobileNo"].ToString();
                        string EmailID = dtReceipt.Rows[0]["EmailID"].ToString();
                        string SlotBookingDate = dtReceipt.Rows[0]["SlotBookingDate"].ToString();
                        string SlotTime = dtReceipt.Rows[0]["SlotTime"].ToString();
                        string DealerAffixationCenterName = dtReceipt.Rows[0]["DealerAffixationCenterName"].ToString();
                        string DealerAffixationCenterAddress = dtReceipt.Rows[0]["DealerAffixationCenterAddress"].ToString();

                        StringBuilder sbTable = new StringBuilder();
                        sbTable.Clear();

                        sbTable.Append("<table>");
                        sbTable.Append("<tr><td colspan=2><b><h4>Receipt of Payment & Appointment</h4></b></td></tr>");
                        sbTable.Append("<tr><td colspan=2><p>HSRP Online Appointment Transaction Reciept <br> Rosmerta Safety System Pvt.Ltd. <br> https://bookmyhsrp.com </p></td></tr>");
                        sbTable.Append("</table>");

                        sbTable.Append("<table>");
                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Order Date :</td>");
                        sbTable.Append("<td>" + OrderDate + "</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Vehicle Reg No :</td>");
                        sbTable.Append("<td>" + VehicleRegNo + "</td>");
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

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Bank Tracking ID :</td>");
                        sbTable.Append("<td>" + TrackingID + "</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Order Status :</td>");
                        sbTable.Append("<td>" + OrderStatus + "</td>");
                        sbTable.Append("</tr>");

                        sbTable.Append("<tr>");
                        sbTable.Append("<td>Payment/Order Status/status_massage :</td>");
                        sbTable.Append("<td>" + StatusMessage + "</td>");
                        sbTable.Append("</tr>");


                        if (StatusMessage.ToString().Equals("Success") || StatusMessage.ToString().Equals("Shipped"))
                        {
                            sbTable.Append("<tr>");
                            sbTable.Append("<td>HSRP cost :</td>");
                            sbTable.Append("<td>" + BasicAamount + " INR</td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Fitment Charges :</td>");
                            sbTable.Append("<td>" + FitmentCharge + " INR</td>");
                            sbTable.Append("</tr>");

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

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>GST Rate :</td>");
                            sbTable.Append("<td>18% </td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>GST Amount :</td>");
                            sbTable.Append("<td>" + GSTAmount + " INR</td>");
                            sbTable.Append("</tr>");

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

                        if (StatusMessage.ToString().Equals("Success") || StatusMessage.ToString().Equals("Shipped"))
                        {
                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Appointment Date Time</td>");
                            sbTable.Append("<td>" + SlotBookingDate + " " + SlotTime + "</td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Fitment Center Name :</td>");
                            sbTable.Append("<td><b><h4>" + DealerAffixationCenterName + "</b></h4></td>");
                            sbTable.Append("</tr>");

                            sbTable.Append("<tr>");
                            sbTable.Append("<td>Fitment Center Address :</td>");
                            sbTable.Append("<td><b><h4>" + DealerAffixationCenterAddress + "</b></h4></td>");
                            sbTable.Append("</tr>");
                        }


                        sbTable.Append("</table>");
                        sbTable.Append("<br><br><p align='left'><b>Note:</b><br><font color='red'>1. Carry this receipt, SMS and RC copy at the time of appointment.</font></br>");
                        sbTable.Append("2. If payment is not successful Process Booking Again. </br>");
                        sbTable.Append("3. If you have queries, Please contact Toll Free 18001200201, Email ID : online@bookmyhsrp.com </br>");
                        sbTable.Append("4. calling time (9:30 AM to 6:00 PM) and day (Monday to Saturday) <br> or Email at online@bookmyhsrp.com </br>");
                        sbTable.Append("5. If you miss to visit at fitment center at Appointment Date Time you can take Re-Appointment Online using above mentioned successful Order ID </br></p>");

                        //createpdfreceipt(sbTable.ToString(), BookingHistoryID);
                        try
                        {
                            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
                            #region
                            string MonthYears = DateTime.Now.ToString("MMM-yyyy");
                            string folderpath = ConfigurationManager.AppSettings["ReceiptPath"].ToString() + "/Plate/" + MonthYears + "/";

                            if (!Directory.Exists(folderpath))
                            {
                                Directory.CreateDirectory(folderpath);
                            }

                            string filename = OrderNo + DateTime.Now.ToString("ddMMyyyyHHmmssfff") + ".pdf";
                            string PdfFolder = folderpath + filename;
                            string filepathtosave = "Plate/" + MonthYears + "/" + filename; //for saving into database

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
                                string updateReceiptQuery = "update Appointment_bookingHist set ReceiptPath = '" + filepathtosave + "' where BookingHistoryID = '" + BookingHistoryID + "' ";
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

                if (ReceiptCompletePath.Length > 0 && File.Exists(ReceiptCompletePath))
                {

                    string[] arr = ReceiptCompletePath.ToString().Split('/');
                    int i = arr.Length;

                    //string PdfSignedFolder = signFolderPath + arr[i - 1].ToString();
                    string filename = arr[i - 1].ToString();
                    #region
                    try
                    {
                        HttpContext context = HttpContext.Current;
                        context.Response.ContentType = "Application/pdf";
                        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                        context.Response.WriteFile(ReceiptCompletePath);
                        context.Response.End();
                    }
                    catch (Exception ev)
                    {
                        string temp = ev.Message;
                    }
                    #endregion
                }
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