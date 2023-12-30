using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2.RazorPay
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "ValidateForm()", true);
            RazorPay();
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("ValidateForm();");
            strScript.Append("</script>");
            this.Page.Controls.Add(new LiteralControl(strScript.ToString()));

        }





        private void RazorPay()
        {
            string OrderNo = string.Empty;

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", 1000000); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", "12121");
            input.Add("payment_capture", 1);
            //input.Add("notes", "Dheerendra");
            //input.Add("name", "Dheerendra Singh");
            //input.Add("prefill","{ 'email':'dheerendra786@gmail.com','contact':'8882359687'}");

            //string key = "rzp_test_A8SlD8Ar6NexSY";     //rzp_test_Uy1r8Av2FjQdBP
            //string secret = "L657FHU7f3APTshth2yDhjgw"; //iaRucYRkXy0nW2IvRrqPrMwj
            string key = "rzp_test_Uy1r8Av2FjQdBP";   //rzp_test_Uy1r8Av2FjQdBP
            string secret = "iaRucYRkXy0nW2IvRrqPrMwj";//rzp_test_Uy1r8Av2FjQdBP

            //string key = ConfigurationManager.AppSettings["key"].ToString();
            //string secret = ConfigurationManager.AppSettings["secret"].ToString();

            RazorpayClient client = new RazorpayClient(key, secret);

            try
            {
                Razorpay.Api.Order order = client.Order.Create(input);
                OrderNo = order["id"].ToString();
                //OrderNo = RandomString(10);
            }
            catch (Exception ex)
            {
                OrderNo = RandomString(10);
                Console.WriteLine(ex.Message);
            }


            string Host = ConfigurationManager.AppSettings["Host"].ToString();
            //string ccavResponseHandler = @"http://localhost:55098//plate/PaymentReceipt.aspx";
            string ccavResponseHandler = Host + "RazorPay/test_response.aspx";



            StringBuilder sbTable = new StringBuilder();
            sbTable.Clear();
            sbTable.Append("<form id='customerData' name='customerData' action='" + ccavResponseHandler + "' method='post'>");
            sbTable.Append("<script\n");
            sbTable.Append("src='https://checkout.razorpay.com/v1/checkout.js'\n");
            sbTable.Append("data-key='" + key + "'\n");
            sbTable.Append("data-amount='8000'\n");
            sbTable.Append("data-currency='INR'\n");
            sbTable.Append("data-retry=");
            sbTable.Append(false);
            sbTable.Append("\n");
            sbTable.Append("data-redirect="+true+"\n");

            //sbTable.Append("data-notes.order_id='" + OrderNo + "'");
            //sbTable.Append("data-notes.order='BMDLAC72D8C2-8703-4B-8703'");
            //sbTable.Append("data-notes.cust_name='Dheerendra Kumar Singh Patel'");
            //sbTable.Append("data-notes.cust_mobile='+91 8882359687'");
            //sbTable.Append("data-notes.vehicleregno='DL7CP0362'");
            //sbTable.Append("data-notes.slotdate='2020-11-09'");
            //sbTable.Append("data-notes.slottime='02:00 PM-06:00 PM'");
            //sbTable.Append("data-notes.slotid='4688'");
            //sbTable.Append("data-notes.appointmenttype='Dealer'");
            //sbTable.Append("data-notes.platesticker='plate'");

            sbTable.Append("data-description='Purchase Description'\n");
            //sbTable.Append("data-redirect=true ");
            //sbTable.Append("data-retry=false");
            sbTable.Append("data-order_id='" + OrderNo + "'\n");
            sbTable.Append("data-image='https://razorpay.com/favicon.png'\n");
            sbTable.Append("data-prefill.name='Gaurav Kumar'\n");
            sbTable.Append("data-prefill.email='gaurav.kumar@example.com'\n");
            sbTable.Append("data-prefill.contact='9123456789'\n");
            sbTable.Append("data-theme.color='#F37254'\n");
            sbTable.Append("></script>");

            sbTable.Append("<input type='hidden' value='Hidden Element' name='hidden'>");
            sbTable.Append("</form>");

            string twmp = sbTable.ToString();
            Literal1.Text = twmp;

            //StringBuilder strScript = new StringBuilder();
            //strScript.Append("<script language='javascript'>");
            //strScript.Append("ValidateForm();");
            //strScript.Append("</script>");
            //this.Page.Controls.Add(new LiteralControl(strScript.ToString()));

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "ValidateForm()", true);
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
    }
}