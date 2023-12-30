using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BMHSRPv2.Models;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
//using iTextSharp.xmp.impl;

namespace BMHSRPv2
{
    public partial class AppointmentStatus : System.Web.UI.Page
    {
        //testing git changes : Removed
        string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        StringBuilder sbbuild = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string orderno = Request.QueryString["ORDER_NUMBER"];
                //string vehNo = Request.QueryString["REG_NUMBER"];
                //System.Uri.EscapeDataString(orderno);

                // string Qstr = "exec CheckStickerOrderDetails '" + orderno.Trim() + "','" + vehNo.Trim() + "'";
               string  orderno = Session["ORDER_NUMBER"].ToString();
               string   vehNo = Session["REG_NUMBER"].ToString();

                DataTable DtResult = Utils.GetDataTable("exec BookMyHSRP_TrackYourOrder  '" + orderno + "','" + vehNo + "'", constr);
                if (DtResult.Rows.Count > 0)
                {
                    sbbuild.Clear();
                    if (DtResult.Rows[0]["ReAppointmentStatus"].ToString() == "1")
                    {
                        

                        for(int i = 0; i <DtResult.Rows.Count; i++)
                        {

                             
                            sbbuild.Append("<div class='row' style='font-size:small'>" +
                           "<div class='col-sm-3'>" +
                               "<label>Previous Date</label>" +
                               "<p>"+ DtResult.Rows[i]["AppointmentDate"].ToString() + "</p>" +
                           "</div>" +
                           "<div class='col-sm-3'>" +
                               "<label>Rescheduled Date  </label>" +
                              "<p>" + DtResult.Rows[i]["RescheduledAppointmentDate"].ToString() + "</p>" +
                           "</div>" +

                            "<div class='col-sm-3'>" +
                               "<label>Rescheduled On  </label>" +
                              "<p>" + DtResult.Rows[i]["CreationDate"].ToString() + "</p>" +
                           "</div>" +
                           "<div class='col-sm-3' >" +
                               "<label >" + DtResult.Rows[i]["OrderNo"].ToString() + " </label>" +
                              "<a href='" + DtResult.Rows[i]["ReceiptPath"].ToString()  + "' target='_blank' >Download</a>" +
                           "</div>" +
                         "</div> ");

                        }


                        LitralAppointmentDates.Text = sbbuild.ToString();
                    }

                   
                }


                string Qstr = " select Emailid, OrderNo as 'ORDER_NUMBER',VehicleRegNo as 'REG_NUMBER',format(SlotBookingDate,'dd-MMM-yyyy')[SlotBookingDate]," +
                           "  ChassisNo as 'CHASSIS_NUMBER',EngineNo as 'ENGINE_NUMBER',OrderStatus,dealerid,ReceiptPath,OwnerName,AppointmentType,ShippingAddress1,ShippingAddress2,ShippingCity,ShippingState,ShippingPinCode " +
                           "  from Appointment_BookingHist where VehicleRegNo = '" + vehNo.Trim() + "' and OrderNo = '" + orderno.Trim() + "' ";
                
                string _AppointmentType = string.Empty;
                DataTable dt = Utils.GetDataTable(Qstr, constr);
                if (dt.Rows.Count > 0)
                {
                    lblorderno.InnerText = dt.Rows[0]["ORDER_NUMBER"].ToString();
                    lblVehicleNo.InnerText = dt.Rows[0]["REG_NUMBER"].ToString();
                    lblAppointmentdate.InnerText = dt.Rows[0]["SlotBookingDate"].ToString();
                    lblStatus.InnerText = dt.Rows[0]["OrderStatus"].ToString();
                    _AppointmentType = dt.Rows[0]["AppointmentType"].ToString();

                    if (dt.Rows[0]["ReceiptPath"].ToString().Length > 0)
                    {
                        string ReceiptPath = ConfigurationManager.AppSettings["ReceiptDirectory"].ToString() + dt.Rows[0]["ReceiptPath"].ToString();

                        dreceipt.Text = " <a href='" + ReceiptPath + "' class='filled'>Download Receipt</a>";
                    }

                    else {
                        try
                        {
                            GenerateInvoice(orderno);
                        }
                        catch(Exception ex) {
                            Page.Response.Write("<script>console.log('" + ex.ToString() + "');</script>");
                        }

                        string _Qsql = "select ReceiptPath from Appointment_BookingHist where OrderNo = '" + orderno + "'";

                        string GetReceiptPath = Utils.getScalarValue(_Qsql, constr);
                        if (GetReceiptPath.Length > 0)
                        { 
                        string _Receiptpath = ConfigurationManager.AppSettings["ReceiptDirectory"].ToString() + GetReceiptPath;

                            dreceipt.Text = " <a href='" + _Receiptpath + "' class='filled'>Download Receipt</a>";

                        }
                    }


                    if (_AppointmentType == "Home")
                    {
                        lblDealer.InnerText = dt.Rows[0]["OwnerName"].ToString();
                        //string FullAffress= dt.Rows[0]["OwnerName"].ToString()
                        string FullAffress = dt.Rows[0]["ShippingAddress1"].ToString() + " " + dt.Rows[0]["ShippingAddress2"].ToString() + " " + dt.Rows[0]["ShippingCity"].ToString() + " " + dt.Rows[0]["ShippingState"].ToString() + " " + dt.Rows[0]["ShippingPinCode"].ToString();
                        lblAddress.InnerText = FullAffress.ToString();
                    }
                    else
                    {


                        string Dealerid = dt.Rows[0]["Dealerid"].ToString();
                        Qstr = " SELECT dealername , DealerAffixationCenterName ,DealerAffixationCenterAddress from" +
                              " DealerAffixationCenter where DealerID = '" + Dealerid + "'";
                        DataTable _dealer = Utils.GetDataTable(Qstr, constr);

                        if (_dealer.Rows.Count > 0)
                        {

                            lblDealer.InnerText = _dealer.Rows[0]["DealerAffixationCenterName"].ToString();
                            lblAddress.InnerText = _dealer.Rows[0]["DealerAffixationCenterAddress"].ToString();
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/TrackOrder.aspx");
                }
            }

        }

        private void GenerateInvoice(string OrderNo)
        {
            try
            {
                string checkReceiptQuery = "PaymentPlateReceipt '" + OrderNo + "' ";
                DataTable dtReceipt = Utils.GetDataTable(checkReceiptQuery, constr);
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
                                Utils.ExecNonQuery(updateReceiptQuery, constr);
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