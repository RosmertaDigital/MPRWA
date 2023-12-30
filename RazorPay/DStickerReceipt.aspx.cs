using BMHSRPv2.Models;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2.RazorPay
{
    public partial class DStickerReceipt : System.Web.UI.Page
    {
        static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["OrderNo"] != null && Request.QueryString["OrderNo"].ToString() != "")
                {
                    string OrderNo = Request.QueryString["OrderNo"].ToString();
                    GenerateInvoice(OrderNo);
                }

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


    }
}