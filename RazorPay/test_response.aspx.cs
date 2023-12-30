using Razorpay.Api;
using Razorpay.Api.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2.RazorPay
{
    public partial class test_response : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            string paymentId = Request.Form["razorpay_payment_id"];

            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", 100); // this amount should be same as transaction amount


            string key = "rzp_test_Uy1r8Av2FjQdBP";   //rzp_test_Uy1r8Av2FjQdBP
            string secret = "iaRucYRkXy0nW2IvRrqPrMwj";//rzp_test_Uy1r8Av2FjQdBP
            RazorpayClient client = new RazorpayClient(key, secret);

            Dictionary<string, string> attributes = new Dictionary<string, string>();

            attributes.Add("razorpay_payment_id", paymentId);
            attributes.Add("razorpay_order_id", Request.Form["razorpay_order_id"]);
            attributes.Add("razorpay_signature", Request.Form["razorpay_signature"]);

            try
            {
                Utils.verifyPaymentSignature(attributes);
            }
            catch(SignatureVerificationError ev)
            {
                lblStatus.Text = "Error: "+ev.Message;
            }
             

            //             please use the below code to refund the payment 
            //             Refund refund = new Razorpay.Api.Payment((string) paymentId).Refund();

            if (paymentId.Length > 0)
            {
                lblStatus.Text = "Success";
            }
            else
            {
                lblStatus.Text = "Failed";
            }
            lblPaymentID.Text = paymentId;
            lblOrderID.Text = Request.Form["razorpay_order_id"];
            txtSignature.Text = Request.Form["razorpay_signature"];
        }
    }
}