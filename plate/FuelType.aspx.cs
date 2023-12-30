using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2.plate
{
    public partial class FuelType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!CheckSession.Checksession1(5, "plate"))
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
            LiteralBookingTypeImage.Text = "<img src='" + Session["OrderType_imgPath"].ToString() + "' draggable='false'>";
            LiteralVehicleTypeImage.Text = "<img src='" + Session["VehicleType_imgPath"].ToString() + "' draggable='false'>";
            LiteralOemImage.Text = "<img src='" + Session["OEMImgPath"].ToString() + "' draggable='false'>";
            LiteralState.Text = "<p><span>"+ Session["StateShortName"].ToString()+ "</span>" + Session["StateName"].ToString() + "</p>";
            LiteralVehicleClassImage.Text = "<img src='" + Session["VehicleClass_imgPath"].ToString() + "' draggable='false'>" +
                                                           "<p> " + Session["VehicleClass"].ToString() + " Vehicle </p>";
        }
        //protected void btnFuelSelection_Click(object sender, EventArgs e)
        //{
        //    string SelectedStateID = txtHideSelectedStateID.Text.ToString();
        //    Session["SelectedStateID"] = SelectedStateID;
        //    Response.Redirect("FuelType.aspx");
        //}
    }
}