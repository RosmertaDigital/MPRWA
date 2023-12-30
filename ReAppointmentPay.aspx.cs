using BMHSRPv2.Models;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2
{
    public partial class ReAppointmentPay : System.Web.UI.Page
    {
        //string Oemid = string.Empty;
        //string VehicleClass = string.Empty;
        //string VehicleType = string.Empty;
        //string VehicleCategoryID = string.Empty;
        //string FulType = string.Empty;
        //string AppointmentType = string.Empty;
        //string DealerAffixationCenterid = string.Empty;

        string CnnString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
 


                lblOrderNo.Text = Session["Re_OrderNo"].ToString();
                LitPreviousDate.Text = Session["OldAppointmentDate"].ToString();
                LitPreviousTime.Text = Session["OldAppointmentSlot"].ToString();
                LitNewDate.Text = Session["Re_SelectedSlotDate"].ToString();
                LitNewTime.Text = Session["Re_SelectedSlotTime"].ToString();

                if (Session["Re_DeliveryPoint"].ToString()== "Home")
                {
                    //start Before Home Delivery Free
                    //BtnSubmit.Visible = false;

                    //btnPayNow.Visible = true;
                    //DivPaymentSummary.Visible = true;
                    //end Before Home Delivery Free


                    //start Home delivery Free
                    btnPayNow.Visible = false;
                    BtnSubmit.Visible = true;       
                    DivPaymentSummary.Visible = false;
                    //end Home delivery free

                }
                else
                {
                    btnPayNow.Visible = false;
                    BtnSubmit.Visible = true;
                   // trDeliveryCharge.Visible = false;
                    DivPaymentSummary.Visible = false;
                }

                //start Before Home Delivery Free
                //PaymentDescription();
                //end Before Home Delivery Free

                //Only for display not used ant where
                // lblOrderNo.Text = "BMHSRP" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + GetRandomNumber();
            }
        }

        private void PaymentDescription()
        {
            string CheckOemRateQuery = "GetReAppointmentRate_Final '" + Session["Re_StateId"] + "', '"+ Session["Re_StateName"].ToString()+ "',"+ Session["Re_VehicleTypeid"].ToString() + ",'"+Session["plateSticker"].ToString()+"'"; 

            DataTable dtOemRate = BMHSRPv2.Models.Utils.GetDataTable(CheckOemRateQuery, CnnString);
            if (dtOemRate.Rows.Count > 0)
            {
              

              
               #region

                  
                    decimal GstBasic_Amt = Convert.ToDecimal(dtOemRate.Rows[0]["GstBasic_Amt"]) + Convert.ToDecimal(dtOemRate.Rows[0]["FittmentCharges"]);
                    decimal FittmentCharges = Convert.ToDecimal(dtOemRate.Rows[0]["FittmentCharges"]);
                    decimal BMHConvenienceCharges = Convert.ToDecimal(dtOemRate.Rows[0]["BMHConvenienceCharges"]);
                    decimal BMHHomeCharges = Convert.ToDecimal(dtOemRate.Rows[0]["BMHHomeCharges"]);
                    decimal GrossTotal = Convert.ToDecimal(dtOemRate.Rows[0]["GrossTotal"]);
                    decimal GSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["GSTAmount"]);
                    decimal IGSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["IGSTAmount"]);
                    decimal CGSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["CGSTAmount"]);
                    decimal SGSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["SGSTAmount"]);
                    decimal GstRate = Convert.ToDecimal(dtOemRate.Rows[0]["gst"]);
                    decimal TotalAmount = Convert.ToDecimal(dtOemRate.Rows[0]["TotalAmount"]);


                    lblBasicAmt.Text = "Rs. " + string.Format("{0:0.00}", GstBasic_Amt);
                    //lblFitmentCharge.Text = "Rs. " + string.Format("{0:0.00}", FittmentCharges);
                   // lblConvenienceFee.Text = "Rs. " + string.Format("{0:0.00}", BMHConvenienceCharges);
                    //lblDeliveryCharge.Text = "Rs. " + string.Format("{0:0.00}", BMHHomeCharges);
                    //lblTotalBasicAmt.Text = "Rs. " + string.Format("{0:0.00}", GrossTotal);
                    lblGST.Text = "Rs. " + string.Format("{0:0.00}", GSTAmount);
                    lblIGST.Text = "Rs. " + string.Format("{0:0.00}", IGSTAmount);
                    lblCGST.Text = "Rs. " + string.Format("{0:0.00}", CGSTAmount);
                    lblSGST.Text = "Rs. " + string.Format("{0:0.00}", SGSTAmount);
                    lblGrandTotal.Text = "Rs. " + string.Format("{0:0.00}", TotalAmount);
                    divtrGST.Visible = true;

                divtrIGST.Visible = false;
                divtrCGST.Visible = false;

                divtrSGST.Visible = false;
                if (IGSTAmount == 0)
                    {
                        divtrIGST.Visible = false;
                    }

                    if (CGSTAmount == 0)
                    {
                        divtrCGST.Visible = false;
                    }

                    if (SGSTAmount == 0)
                    {
                        divtrSGST.Visible = false;
                    }
                    #endregion
                
             
            }
            else
            {

                lblMessage.Text = "Rate not found.!!!";
                lblMessage.Visible = true;
                return;
            }

            try
            {
                //lblOwnerName.Text = Session["Re_SessionOwnerName"].ToString();
                //lblMobile.Text =    Session["Re_SessionMobileNo"].ToString();
                //lblAddress.Text =   Session["Re_SessionBillingAddress"].ToString();
                //lblEmailID.Text =   Session["Re_SessionEmailID"].ToString();
                //lblPinCode.Text = "";
            }
            catch(Exception ev)
            {

            }
        }

       
        
        protected void btnPayNow_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = false;

            //if (!cbAgree.Checked)
            //{
            //    lblMessage.Text = "You agree. Please click on the button to go to the next page.";
            //    lblMessage.Visible = true;
            //    return ;
            //}

            //if user coming again then failed previous order and start new order.
            if (!string.IsNullOrWhiteSpace(hdnMyOrderID.Value))
            {
                try
                {
                    var order_status = "Failed";
                    var failure_message = "User started new transaction.";
                    var payment_gateway_type = "razorpay";
                    string paymentFailedQuery = "RePaymentConfirmation '" + hdnMyOrderID.Value + "','','','" + order_status + "','" + failure_message + "','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','" + payment_gateway_type + "'";
                    DataTable dtPaymentConfirmation = BMHSRPv2.Models.Utils.GetDataTable(paymentFailedQuery, CnnString);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    //need to add in logger
                }
            }

            string AppointmentBlockedDatesQuery = "CheckECAppointmentBlockedDates '" + Session["Re_SelectedSlotDate"].ToString() + "', '" + Session["Re_DealerAffixationCenterid"].ToString() + "','" + Session["Re_DeliveryPoint"].ToString() + "','" + HttpContext.Current.Session["plateSticker"].ToString() + "' ";
            System.Data.DataTable dtAppointmentBlockedDates = BMHSRPv2.Models.Utils.GetDataTable(AppointmentBlockedDatesQuery, CnnString);

            if (dtAppointmentBlockedDates.Rows.Count > 0 && dtAppointmentBlockedDates.Rows[0]["status"].ToString() == "1")
            {
                lblMessage.Text = "Slot booked. Choose another Slot ";
                lblMessage.Visible = true;
                return;
            }
            else if (dtAppointmentBlockedDates.Rows.Count == 0)
            {
                lblMessage.Text = "Slot booked. Choose another Slot !!!! ";
                lblMessage.Visible = true;
                return;
            }


          
            string NewSlotId =  Session["Re_SelectedSlotID"].ToString();
            string NewSlotTime =  Session["Re_SelectedSlotTime"].ToString();
            string NewSlotBookingDate = Session["Re_SelectedSlotDate"].ToString(); //StateId
            string HSRP_StateID = Session["Re_StateId"].ToString();

            string OEMID = Session["Re_Oemid"].ToString();
       
            string AppointmentType = Session["Re_DeliveryPoint"].ToString();


            // old string Qstr = "EXEC [RescheduledAppointmentlog]  '" + Session["Re_OrderNo"].ToString() + "'," + Session["Re_DealerAffixationCenterid"].ToString() + ",'" + NewSlotBookingDate.ToString() + "','" + NewSlotTime.ToString() + "','"+ Session["VehicleRegNo"].ToString()+ "','','"+ AppointmentType + "'";
            string Qstr = "EXEC [RescheduledAppointmentInsert]  '" + Session["Re_OrderNo"].ToString() + "'," + Session["Re_DealerAffixationCenterid"].ToString() + ",'" + NewSlotBookingDate.ToString() + "','" + NewSlotTime.ToString() + "','','" + Session["VehicleRegNo"].ToString() + "','" + AppointmentType + "','" + NewSlotId + "'";
            DataTable DL = BMHSRPv2.Models.Utils.GetDataTable(Qstr, CnnString);
            if (DL.Rows.Count > 0)
            {

                if (DL.Rows[0]["Status"].ToString() == "1")
                {

                    StringBuilder Sbooking = new StringBuilder();
                    //Sbooking.Append("Your Order has been Rescheduled successfully <br/>");
                    //Sbooking.Append(DL.Rows[0]["Message"].ToString());
                    string OrderNo = DL.Rows[0]["RecheduleOrderNo"].ToString();

                    //string CheckOemRateQuery = "GetReAppointmentRate '" + Session["Re_StateId"] + "', '" + Session["Re_StateName"].ToString() + "'," + Session["Re_VehicleTypeid"].ToString() + "";
                    string CheckOemRateQuery = "GetReAppointmentRate_Final '" + Session["Re_StateId"] + "', '" + Session["Re_StateName"].ToString() + "'," + Session["Re_VehicleTypeid"].ToString() + ",'" + Session["plateSticker"].ToString() + "'";

                    DataTable dtOemRate = BMHSRPv2.Models.Utils.GetDataTable(CheckOemRateQuery, CnnString);
                    if (dtOemRate.Rows.Count > 0)
                    {



                        #region


                        decimal GstBasic_Amt = Convert.ToDecimal(dtOemRate.Rows[0]["GstBasic_Amt"]) + Convert.ToDecimal(dtOemRate.Rows[0]["FittmentCharges"]);
                        decimal FittmentCharges = Convert.ToDecimal(dtOemRate.Rows[0]["FittmentCharges"]);
                        decimal BMHConvenienceCharges = Convert.ToDecimal(dtOemRate.Rows[0]["BMHConvenienceCharges"]);
                        decimal BMHHomeCharges = Convert.ToDecimal(dtOemRate.Rows[0]["BMHHomeCharges"]);
                        decimal GrossTotal = Convert.ToDecimal(dtOemRate.Rows[0]["GrossTotal"]);
                        decimal GSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["GSTAmount"]);
                        decimal IGSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["IGSTAmount"]);
                        decimal CGSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["CGSTAmount"]);
                        decimal SGSTAmount = Convert.ToDecimal(dtOemRate.Rows[0]["SGSTAmount"]);
                        decimal GstRate = Convert.ToDecimal(dtOemRate.Rows[0]["gst"]);
                        decimal TotalAmount = Convert.ToDecimal(dtOemRate.Rows[0]["TotalAmount"]);


                        string RazorPayOrderIDUpdate = "update [BookMyHSRP].dbo.RescheduledAppointment set BasicAmt='"+ GstBasic_Amt + "',TotalAmt='"+ TotalAmount + "',GrandTotalAmt='"+ TotalAmount + "',GstAmt='"+ GSTAmount + "',FitmentCharge='"+ FittmentCharges + "',convenienceCharge='"+ BMHConvenienceCharges + "' where RescheduleOrderNo ='" + OrderNo + "'  ";
                        int updateCout = BMHSRPv2.Models.Utils.ExecNonQuery(RazorPayOrderIDUpdate, CnnString);

                        RazorPay(OrderNo, TotalAmount.ToString(), Session["Re_SessionOwnerName"].ToString(), Session["Re_SessionBillingAddress"].ToString(), "", Session["Re_StateName"].ToString(), "", Session["Re_SessionMobileNo"].ToString(), Session["Re_SessionEmailID"].ToString(), (NewSlotBookingDate + " " + NewSlotTime), Session["Re_DealerAffixationCenterid"].ToString(), Session["Re_SessionBillingAddress"].ToString(), Session["VehicleRegNo"].ToString(), NewSlotId);
                        StringBuilder RazorPayScript = new StringBuilder();
                        RazorPayScript.Append("<script language='javascript'>");
                        RazorPayScript.Append("ValidatePayForm();");
                        RazorPayScript.Append("</script>");
                        this.Page.Controls.Add(new LiteralControl(RazorPayScript.ToString()));

                        lblMessage.Text = Sbooking.ToString().ToString();
                        lblMessage.Visible = true;
                        return;

                        #endregion


                    }
                    else
                    {

                        lblMessage.Text = "Rate not found.!!!";
                        lblMessage.Visible = true;
                        return;
                    }

                  
                }
                else
                {
                    StringBuilder Sbooking = new StringBuilder();

                    Sbooking.Append(DL.Rows[0]["Message"].ToString());

                    lblMessage.Text = Sbooking.ToString().ToString();
                    lblMessage.Visible = true;
                    return;
                    
                }


            }
            else
            {
                lblMessage.Text = "Order Reschedule Not Completed!";
                lblMessage.Visible = true;
                return;
         

            }

        }

        public string GetRandomNumber()
        {
            Random r = new Random();
            var x = r.Next(0, 9);
            return x.ToString("0");
        }

        private void CCAvenueForm( string orderno,string GrandTotal,string OwnerName,string Address,
            string CityName,string StateName,string PinCode,
            string MobileNo,string Emailid,string AppointmentDateTime,string DealerAffaxtionCenterID,
            string DealerAffaxtionAddress,string Regno,string TimeSlotID)
        {

            string Host = ConfigurationManager.AppSettings["Host"].ToString();
            //string ccavResponseHandler = @"http://localhost:55098//plate/PaymentReceipt.aspx";
            string ccavResponseHandler = Host + "plate/PaymentReceipt.aspx";

            StringBuilder sbTable = new StringBuilder();
            sbTable.Clear();

            GrandTotal = "1"; // on live plz comment or remove this line.

            sbTable.Append("<form id='customerData' name='customerData' method='post' action='https://rosmertahsrp.com/ccvbookmyhsrp/ccavRequestHandler.aspx' autocomplete='off'>");
            sbTable.Append("<div class='row'>");
            sbTable.Append("<input type='hidden' name='tid' id='tid' readonly />");
            sbTable.Append("<input type='hidden' name='merchant_id' id='merchant_id' value='212112' runat='server' />");
            sbTable.Append("<input type='hidden' name='order_id' id='order_id' value='" + orderno + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='amount'   id='amount'   value='" + GrandTotal + "'  runat='server' />");
            sbTable.Append("<input type='hidden' name='currency' id='currency' value='INR' runat='server' />");
            sbTable.Append("<input type='hidden' name='redirect_url' id='redirect_url' value='" + ccavResponseHandler + "' />");
            sbTable.Append("<input type='hidden' name='cancel_url' id='cancel_url' value='" + ccavResponseHandler + "' />");

            sbTable.Append("<input type='hidden' name='billing_name' id='billing_name'          value='" + OwnerName.ToString() + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='billing_address' id='billing_address'    value='" + Address.ToString() + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='billing_city' id='billing_city' value='" + CityName.ToString() + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='billing_state' id='billing_state' value='" + StateName.ToString() + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='billing_zip' id='billing_zip' value='" + PinCode.ToString() + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='billing_country' id='billing_country' value='India' runat='server' />");
            sbTable.Append("<input type='hidden' name='billing_tel' id='billing_tel' value='" + MobileNo.ToString() + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='billing_email' id='billing_email' value='" + Emailid.ToString() + "' runat='server' />");

            sbTable.Append("<input type='hidden' name='delivery_name' id='delivery_name' runat='server' />");
            sbTable.Append("<input type='hidden' name='delivery_address' id='delivery_address' runat='server' />");
            sbTable.Append("<input type='hidden' name='delivery_city' id='delivery_city' runat='server' />");
            sbTable.Append("<input type='hidden' name='delivery_state' id='delivery_state' runat='server' />");
            sbTable.Append("<input type='hidden' name='delivery_zip' id='delivery_zip' runat='server' />");
            sbTable.Append("<input type='hidden' name='delivery_country' id='delivery_country' value='India' runat='server' />");
            sbTable.Append("<input type='hidden' name='delivery_tel' id='delivery_tel' runat='server' />");

            sbTable.Append("<input type='hidden' name='merchant_param1' id='merchant_param1' value='" + AppointmentDateTime.ToString().Replace(":", ".") + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='merchant_param2' id='merchant_param2' value='" + DealerAffaxtionCenterID.ToString() + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='merchant_param3' id='merchant_param3' value='" + DealerAffaxtionAddress.ToString() + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='merchant_param4' id='merchant_param4' value='" + Regno.ToString() + "' runat='server' />");
            sbTable.Append("<input type='hidden' name='merchant_param5' id='merchant_param5' value='" + TimeSlotID.ToString() + "' runat='server'  />");
            sbTable.Append("<input type='hidden' name='promo_code' />");
            sbTable.Append("<input type='hidden' name='customer_identifier' id='customer_identifier' runat='server' />");

            //sbTable.Append("<input id='paynow' type='Submit' name='Submit' />");
            sbTable.Append("</div>");
            sbTable.Append("</form>");
            Literal1.Text = sbTable.ToString();

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "PostData()", true);

        }


        private void RazorPay(string orderno, string GrandTotal, string OwnerName, string Address,
            string CityName, string StateName, string PinCode,
            string MobileNo, string Emailid, string AppointmentDateTime, string DealerAffaxtionCenterID,
            string DealerAffaxtionAddress, string Regno, string TimeSlotID)
        {
            string Order_No = string.Empty;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            decimal payableAmount = Convert.ToDecimal(GrandTotal) * 100;

            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", payableAmount); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", orderno);
            input.Add("payment_capture", 1);
            //input.Add("name", "Dheerendra Singh");
            //input.Add("prefill","{ 'email':'dheerendra786@gmail.com','contact':'8882359687'}");

            string key = ConfigurationManager.AppSettings["key"].ToString();
            string secret = ConfigurationManager.AppSettings["secret"].ToString();

            RazorpayClient client = new RazorpayClient(key, secret);

            try
            {
                Razorpay.Api.Order order = client.Order.Create(input);
                Order_No = order["id"].ToString();

                try
                {
                    string RazorPayOrderIDUpdate = "update [BookMyHSRP].dbo.RescheduledAppointment set Razorpay_Order_id = '" + Order_No + "' where RescheduleOrderNo ='" + orderno + "'  ";
                    int updateCout = BMHSRPv2.Models.Utils.ExecNonQuery(RazorPayOrderIDUpdate, CnnString);
                    hdnMyOrderID.Value = orderno;
                    hdnGatewayOrderID.Value = Order_No;
                }
                catch (Exception ev)
                {

                }
            }
            catch (Exception ex)
            {
                Order_No = RandomString(10);
                Console.WriteLine(ex.Message);
            }


            string Host = ConfigurationManager.AppSettings["Host"].ToString();
            //string ccavResponseHandler = @"http://localhost:55098//RePaymentReceipt.aspx";
            string ccavResponseHandler = Host + "RePaymentReceipt.aspx";

            StringBuilder sbTable = new StringBuilder();
            sbTable.Clear();
            sbTable.Append("<form id='customerData' name='customerData' action='" + ccavResponseHandler + "' method='post'>");
            sbTable.Append("<script ");
            sbTable.Append("src='https://checkout.razorpay.com/v1/checkout.js' ");
            sbTable.Append("data-key='" + key + "' ");
            sbTable.Append("data-amount='0' ");
            sbTable.Append("data-name='Razorpay' ");
            sbTable.Append("data-description='BookMyHSRP' ");
            sbTable.Append("data-order_id='" + Order_No + "' ");
            sbTable.Append("data-image='https://razorpay.com/favicon.png' ");
            sbTable.Append("data-prefill.name='"+OwnerName.ToString()+"' ");
            sbTable.Append("data-prefill.email='"+ Emailid + "' ");
            sbTable.Append("data-prefill.contact='"+ MobileNo + "' ");
            sbTable.Append("data-theme.color='#F37254' ");
            sbTable.Append("data-modal.confirm_close=true ");
            sbTable.Append("data-modal.escape=false ");
            //sbTable.Append($"data-modal.ondismiss=function(){{payment_checkoutClosed('{orderno}','{Order_No}');}} ");
            sbTable.Append("></script>");

            sbTable.Append("<input type='hidden' value='Hidden Element' name='hidden'>");
            sbTable.Append("<input type='hidden' value='" + orderno + "' name='generated_order_id'>");
            sbTable.Append("</form>");
            Literal1.Text = sbTable.ToString();
        }

        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                
                    lblMessage.Visible = false;

                    string AppointmentBlockedDatesQuery = "CheckECAppointmentBlockedDates '" + Session["Re_SelectedSlotDate"].ToString() + "', '" + Session["Re_DealerAffixationCenterid"].ToString() + "','" + Session["Re_DeliveryPoint"].ToString() + "','" + HttpContext.Current.Session["plateSticker"].ToString() + "' ";
                    System.Data.DataTable dtAppointmentBlockedDates = BMHSRPv2.Models.Utils.GetDataTable(AppointmentBlockedDatesQuery, CnnString);

                    if (dtAppointmentBlockedDates.Rows.Count > 0 && dtAppointmentBlockedDates.Rows[0]["status"].ToString() == "1")
                    {
                        lblMessage.Text = "Slot booked. Choose another Slot ";
                        lblMessage.Visible = true;
                        return;
                    }
                    else if (dtAppointmentBlockedDates.Rows.Count == 0)
                    {
                        lblMessage.Text = "Slot booked. Choose another Slot !!!! ";
                        lblMessage.Visible = true;
                        return;
                    }

                    string NewSlotId = Session["Re_SelectedSlotID"].ToString();
                    string NewSlotTime = Session["Re_SelectedSlotTime"].ToString();
                    string NewSlotBookingDate = Session["Re_SelectedSlotDate"].ToString(); //StateId
                    string HSRP_StateID = Session["Re_StateId"].ToString();

                    string OEMID = Session["Re_Oemid"].ToString();

                    string AppointmentType = Session["Re_DeliveryPoint"].ToString();
                
                    //old  string Qstr = "EXEC [RescheduledAppointmentlog]  '" + Session["Re_OrderNo"].ToString() + "'," + Session["Re_DealerAffixationCenterid"].ToString() + ",'" + NewSlotBookingDate.ToString() + "','" + NewSlotTime.ToString() + "','','" + Session["VehicleRegNo"].ToString() + "','" + AppointmentType + "'";

                //start Code before Home Delivery free
                    //string Qstr = "EXEC [RescheduledAppointmentInsert]  '" + Session["Re_OrderNo"].ToString() + "'," + Session["Re_DealerAffixationCenterid"].ToString() + ",'" + NewSlotBookingDate.ToString() + "','" + NewSlotTime.ToString() + "','','" + Session["VehicleRegNo"].ToString() + "','" + AppointmentType + "','"+ NewSlotId + "'";

                // end Code before Home Delivery free

                // start Home Delivery Free
                string Qstr = "EXEC [RescheduledAppointmentInsert_backoffice]  '" + Session["Re_OrderNo"].ToString() + "'," + Session["Re_DealerAffixationCenterid"].ToString() + ",'" + NewSlotBookingDate.ToString() + "','" + NewSlotTime.ToString() + "','','" + Session["VehicleRegNo"].ToString() + "','" + AppointmentType + "','" + NewSlotId + "'";
                // end Home Delivery Free
                DataTable DL = BMHSRPv2.Models.Utils.GetDataTable(Qstr, CnnString);
                    if (DL.Rows.Count > 0)
                    {

                        if (DL.Rows[0]["Status"].ToString() == "1")
                        {

                        hdnGatewayOrderID.Value = DL.Rows[0]["RecheduleOrderNo"].ToString();
                        StringBuilder Sbooking = new StringBuilder();
                            Sbooking.Append("Your Order has been Rescheduled successfully <br/>");
                            Sbooking.Append(DL.Rows[0]["Message"].ToString());
                            string Smstext = DL.Rows[0]["SmsText"].ToString();
                        
                            
                        string SMSResponse = BMHSRPv2.Models.Utils.SMSSend(Session["Re_SessionMobileNo"].ToString(), Smstext);
                        string SMSLogSaveQuery = "insert into [BookMyHSRP].dbo.Appointment_SMSDetails (OwnerName,VehicleRegNo,MobileNo,SMSText,SentResponseCode,SentDateTime) values " +
                                                 " ('" + Session["Re_SessionOwnerName"].ToString() + "','" + Session["VehicleRegNo"].ToString() + "','" + Session["Re_SessionMobileNo"].ToString() + "','" + Smstext + "','" + SMSResponse + "',getdate()) ";
                        BMHSRPv2.Models.Utils.ExecNonQuery(SMSLogSaveQuery, CnnString);
                        BtnSubmit.Visible = false;

                        #region New Receipt Genereated 
                        if (Session["plateSticker"].ToString().ToUpper() == "STICKER")
                        {
                            GenerateStickerInvoice(Session["Re_OrderNo"].ToString());
                        }
                        if (Session["plateSticker"].ToString().ToUpper() == "PLATE")
                        {
                            GeneratePlateInvoice(Session["Re_OrderNo"].ToString());

                        }

                        #endregion
                        lblMessage.Text = Sbooking.ToString().ToString();
                            lblMessage.Visible = true;
                            return;
                        }
                        else
                        {
                            StringBuilder Sbooking = new StringBuilder();

                            Sbooking.Append(DL.Rows[0]["Message"].ToString());

                            lblMessage.Text = Sbooking.ToString().ToString();
                            lblMessage.Visible = true;
                            return;

                        }


                    }
                    else
                    {
                        lblMessage.Text = "Order Reschedule Not Completed!";
                        lblMessage.Visible = true;
                        return;


                    }



                }
            catch (Exception ex)
            {

            }

        }

        private void GeneratePlateInvoice(string OrderNo)
        {
            try
            {
                string checkReceiptQuery = "PaymentPlateReceipt '" + OrderNo + "' ";
                DataTable dtReceipt = BMHSRPv2.Models.Utils.GetDataTable(checkReceiptQuery, CnnString);
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
                        string folderpath = ConfigurationManager.AppSettings["ReceiptPath"].ToString() + "/"+ Session["plateSticker"].ToString()+ "/" + MonthYears + "/";

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


                        #region Commented because home delivery Free

                        //if (OrderStatus.ToString().Equals("Success") || OrderStatus.ToString().Equals("Shipped"))
                        //{
                        //    sbTable.Append("<tr>");
                        //    sbTable.Append("<td>HSRP set including Fitment charges :</td>");
                        //    sbTable.Append("<td>" + string.Format("{0:0.00}", GstBasic_Amt) + " INR</td>");
                        //    sbTable.Append("</tr>");

                        //    //sbTable.Append("<tr>");
                        //    //sbTable.Append("<td>Fitment Charges :</td>");
                        //    //sbTable.Append("<td>" + FitmentCharge + " INR</td>");
                        //    //sbTable.Append("</tr>");

                        //    sbTable.Append("<tr>");
                        //    sbTable.Append("<td>Convenience Fee :</td>");
                        //    sbTable.Append("<td>" + ConvenienceFee + " INR</td>");
                        //    sbTable.Append("</tr>");

                        //    if (AppointmentType.ToString().Equals("Home"))
                        //    {
                        //        sbTable.Append("<tr>");
                        //        sbTable.Append("<td>Home Delivery :</td>");
                        //        sbTable.Append("<td>" + HomeDeliveryCharge + " INR</td>");
                        //        sbTable.Append("</tr>");
                        //    }


                        //    sbTable.Append("<tr>");
                        //    sbTable.Append("<td>Gross Total :</td>");
                        //    sbTable.Append("<td>" + TotalAmount + " INR</td>");
                        //    sbTable.Append("</tr>");

                        //    //sbTable.Append("<tr>");
                        //    //sbTable.Append("<td>GST Rate :</td>");
                        //    //sbTable.Append("<td>18% </td>");
                        //    //sbTable.Append("</tr>");

                        //    //sbTable.Append("<tr>");
                        //    //sbTable.Append("<td>GST Amount :</td>");
                        //    //sbTable.Append("<td>" + GSTAmount + " INR</td>");
                        //    //sbTable.Append("</tr>");
                        //    if (IGSTAmount != 0)
                        //    {
                        //        //sbTable.Append("<tr>");
                        //        //sbTable.Append("<td>IGST :</td>");
                        //        //sbTable.Append("<td>18% </td>");
                        //        //sbTable.Append("</tr>");

                        //        sbTable.Append("<tr>");
                        //        sbTable.Append("<td>IGST Amount :</td>");
                        //        sbTable.Append("<td>" + string.Format("{0:0.00}", IGSTAmount) + " INR</td>");
                        //        sbTable.Append("</tr>");
                        //    }

                        //    if (CGSTAmount != 0)
                        //    {
                        //        sbTable.Append("<tr>");
                        //        sbTable.Append("<td>CGST Amount :</td>");
                        //        sbTable.Append("<td>" + string.Format("{0:0.00}", CGSTAmount) + " INR</td>");
                        //        sbTable.Append("</tr>");
                        //    }

                        //    if (SGSTAmount != 0)
                        //    {

                        //        sbTable.Append("<tr>");
                        //        sbTable.Append("<td>SGST Amount :</td>");
                        //        sbTable.Append("<td>" + string.Format("{0:0.00}", SGSTAmount) + " INR</td>");
                        //        sbTable.Append("</tr>");
                        //    }

                        //    sbTable.Append("<tr>");
                        //    sbTable.Append("<td>Grand Total :</td>");
                        //    sbTable.Append("<td>" + NetAmount + " INR</td>");
                        //    sbTable.Append("</tr>");
                        //}


                        #endregion

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
                            sbTable.Append("<td><b>Reschedule Appointment Date Time</b></td>");
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
                                //string updateReceiptQuery = "update Appointment_bookingHist set ReceiptPath = '" + filepathtosave + "' where BookingHistoryID = '" + BookingHistoryID + "' ";
                                int i = 1;// BMHSRPv2.Models.Utils.ExecNonQuery(updateReceiptQuery, CnnString);
                                if (i > 0)
                                {
                                    string updateQuery = "update top (1) BookMyHSRP.dbo.RescheduledAppointment set ReceiptPath = '" + filepathtosave + "' where RescheduledId IN ( SELECT TOP 1 RescheduledId FROM  BookMyHSRP.dbo.RescheduledAppointment WHERE OrderNo= '" + OrderNo + "'  order by RescheduledId desc) ";

                                    BMHSRPv2.Models.Utils.ExecNonQuery(updateQuery, CnnString);
                                    btnPrint.Visible = true;
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
                DataTable dtReceipt = BMHSRPv2.Models.Utils.GetDataTable(checkReceiptQuery, CnnString);
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

                        #region Commented because Home delivery free

                        //if (OrderStatus.ToString().Equals("Success") || OrderStatus.ToString().Equals("Shipped"))
                        //{
                        //    sbTable.Append("<tr>");
                        //    sbTable.Append("<td>3rd Sticker only including Fitment charges :</td>");
                        //    sbTable.Append("<td>" + string.Format("{0:0.00}", GstBasic_Amt) + " INR</td>");
                        //    sbTable.Append("</tr>");

                        //    //sbTable.Append("<tr>");
                        //    //sbTable.Append("<td>Fitment Charges :</td>");
                        //    //sbTable.Append("<td>" + FitmentCharge + " INR</td>");
                        //    //sbTable.Append("</tr>");

                        //    sbTable.Append("<tr>");
                        //    sbTable.Append("<td>Convenience Fee :</td>");
                        //    sbTable.Append("<td>" + ConvenienceFee + " INR</td>");
                        //    sbTable.Append("</tr>");

                        //    if (AppointmentType.ToString().Equals("Home"))
                        //    {
                        //        sbTable.Append("<tr>");
                        //        sbTable.Append("<td>Home Delivery :</td>");
                        //        sbTable.Append("<td>" + HomeDeliveryCharge + " INR</td>");
                        //        sbTable.Append("</tr>");
                        //    }

                        //    sbTable.Append("<tr>");
                        //    sbTable.Append("<td>Gross Total :</td>");
                        //    sbTable.Append("<td>" + TotalAmount + " INR</td>");
                        //    sbTable.Append("</tr>");

                        //    //sbTable.Append("<tr>");
                        //    //sbTable.Append("<td>GST Rate :</td>");
                        //    //sbTable.Append("<td>18% </td>");
                        //    //sbTable.Append("</tr>");

                        //    //sbTable.Append("<tr>");
                        //    //sbTable.Append("<td>GST Amount :</td>");
                        //    //sbTable.Append("<td>" + GSTAmount + " INR</td>");
                        //    //sbTable.Append("</tr>");
                        //    if (IGSTAmount != 0)
                        //    {
                        //        //sbTable.Append("<tr>");
                        //        //sbTable.Append("<td>IGST :</td>");
                        //        //sbTable.Append("<td>18% </td>");
                        //        //sbTable.Append("</tr>");

                        //        sbTable.Append("<tr>");
                        //        sbTable.Append("<td>IGST Amount :</td>");
                        //        sbTable.Append("<td>" + string.Format("{0:0.00}", IGSTAmount) + " INR</td>");
                        //        sbTable.Append("</tr>");
                        //    }

                        //    if (CGSTAmount != 0)
                        //    {
                        //        sbTable.Append("<tr>");
                        //        sbTable.Append("<td>CGST Amount :</td>");
                        //        sbTable.Append("<td>" + string.Format("{0:0.00}", CGSTAmount) + " INR</td>");
                        //        sbTable.Append("</tr>");
                        //    }

                        //    if (SGSTAmount != 0)
                        //    {

                        //        sbTable.Append("<tr>");
                        //        sbTable.Append("<td>SGST Amount :</td>");
                        //        sbTable.Append("<td>" + string.Format("{0:0.00}", SGSTAmount) + " INR</td>");
                        //        sbTable.Append("</tr>");
                        //    }

                        //    sbTable.Append("<tr>");
                        //    sbTable.Append("<td>Grand Total :</td>");
                        //    sbTable.Append("<td>" + NetAmount + " INR</td>");
                        //    sbTable.Append("</tr>");
                        //}


                        #endregion

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
                                //string updateReceiptQuery = "update BookMyHSRP.dbo.Appointment_BookingHist set ReceiptPath = '" + filepathtosave + "' where BookingHistoryID = '" + BookingHistoryID + "' ";
                                int i = 1; //BMHSRPv2.Models.Utils.ExecNonQuery(updateReceiptQuery, CnnString);
                                if (i > 0)
                                {
                                    HiddenlblRescheduledOrderid.Value = filepathtosave;
                                    string updateQuery = "update top (1) BookMyHSRP.dbo.RescheduledAppointment set ReceiptPath = '" + filepathtosave + "' where RescheduledId IN ( SELECT TOP 1 RescheduledId FROM  BookMyHSRP.dbo.RescheduledAppointment WHERE OrderNo= '" + OrderNo + "'  order by RescheduledId desc) ";
                                    BMHSRPv2.Models.Utils.ExecNonQuery(updateQuery, CnnString);
                                    btnPrint.Visible = true;
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            #region New Code

            string checkReceiptQuery = "select isnull(ReceiptPath,'') FileName from [BookMyHSRP].dbo.RescheduledAppointment where RescheduleOrderNo = '" + hdnGatewayOrderID.Value.ToString() + "'";
            DataTable dtFileName = Models.Utils.GetDataTable(checkReceiptQuery, CnnString);
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
    }
}