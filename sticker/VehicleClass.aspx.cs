using System;using BMHSRPv2.plate;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2.sticker
{
    public partial class VehicleClass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!CheckSession.Checksession1(4, "sticker"))
            {
                Response.Redirect("../Error.aspx");
            }

           



        }
    }
}