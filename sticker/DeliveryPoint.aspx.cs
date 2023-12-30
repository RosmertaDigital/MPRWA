using System;using BMHSRPv2.plate;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace BMHSRPv2.sticker
{
    public partial class DeliveryPoint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!CheckSession.Checksession1(8, "sticker"))
            {
                Response.Redirect("Error.aspx");
            }

           
                Literal1.Text = "Only Additional 100 Rs.";
          
           
            if (Session["StateId"].ToString() == "31")
            {
                LiteralComingSoon.Text = "Coming Soon";
                Literal1.Text = "";
            }
            else
            {
                LiteralComingSoon.Text = "";
            }
        }

    }
}