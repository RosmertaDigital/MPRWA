using System;using BMHSRPv2.plate;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2.sticker
{
    public partial class FuelType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!CheckSession.Checksession1(5, "sticker"))
            {
                Response.Redirect("../Error.aspx");
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
            LiteralState.Text = "<p><span>"+ Session["S_StateShortName"].ToString()+ "</span>" + Session["S_StateName"].ToString() + "</p>";
            LiteralVehicleClassImage.Text = "<img src='" + Session["S_VehicleClass_imgPath"].ToString() + "' draggable='false'>" +
                                                           "<p> " + Session["S_VehicleClass"].ToString() + " Vehicle </p>";
        }
        //protected void btnFuelSelection_Click(object sender, EventArgs e)
        //{
        //    string SelectedStateID = txtHideSelectedStateID.Text.ToString();
        //    Session["S_SelectedStateID"] = SelectedStateID;
        //    Response.Redirect("FuelType.aspx");
        //}
    }
}