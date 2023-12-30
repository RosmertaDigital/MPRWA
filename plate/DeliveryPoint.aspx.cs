using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace BMHSRPv2.plate
{
    public partial class DeliveryPoint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!CheckSession.Checksession1(8, "plate"))
            {
                Response.Redirect("../Error.aspx");
            }


            if (Session["VehicleCat"].ToString() == "2W" || Session["VehicleCat"].ToString() == "3W")
            {
                Literal1.Text = "Only Additional 125 Rs.";
            }
            else
            {
                Literal1.Text = "Only Additional 250 Rs.";
            }
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