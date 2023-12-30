using System;using BMHSRPv2.plate;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2.sticker
{
    public partial class Statetype : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            
            if (!CheckSession.Checksession1(3, "sticker"))
            {
                Response.Redirect("Error.aspx");
            }

            if (!IsPostBack)
            {

                SetSideBar();

            }
        }

        private void SetSideBar()
        {
            LiteralBookingTypeImage.Text = "<img src='" + Session["S_OrderType_imgPath"].ToString() + "' draggable='false'>";
            LiteralVehicleTypeImage.Text = "<img src='" + Session["S_VehicleType_imgPath"].ToString() + "' draggable='false'>";
            LiteralOemImage.Text = "<img src='" + Session["S_OEMImgPath"].ToString() + "' draggable='false'>";
        }

        //protected void btnStateSelection_Click(object sender, EventArgs e)
        //{
        //    string SelectedStateID = txtHideSelectedStateID.Value.ToString();
        //    if (SelectedStateID == "" || SelectedStateID == null)
        //    {
        //        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:SetVehicleCatSelected('Please Select Your State'); ", true);
        //    }
            

        //    Session["S_SelectedStateID"] = SelectedStateID;
        //    Response.Redirect("VehicleClass.aspx");
        //}

    }
}