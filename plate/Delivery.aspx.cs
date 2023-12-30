using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2.plate
{
    public partial class Delivery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["DealerAffixationCenterid"] = "367";
            //if (!CheckSession.Checksession1(10, "plate"))
            //{
            //    Response.Redirect("../Error.aspx");
            //}

            if (Session["DeliveryPoint"] != null)
            {
                string value = Session["DeliveryPoint"].ToString();
                if (Session["DeliveryPoint"].ToString() == "Home")
                {

                        SetSideBar();
                    }
                    else
                    {
                        Response.Redirect("DeliveryPoint.aspx");
                    }
                }
                else
                {
                    Response.Redirect("DeliveryPoint.aspx");
                }
           
        }

        private void SetSideBar()
        {
            LiteralBookingTypeImage.Text = "<img src='" + Session["OrderType_imgPath"].ToString() + "' draggable='false'>";
            LiteralVehicleTypeImage.Text = "<img src='" + Session["VehicleType_imgPath"].ToString() + "' draggable='false'>";
            LiteralOemImage.Text = "<img src='" + Session["OEMImgPath"].ToString() + "' draggable='false'>";
            LiteralState.Text = "<p><span>" + Session["StateShortName"].ToString() + "</span>" + Session["StateName"].ToString() + "</p>";
            LiteralVehicleClassImage.Text = "<img src='" + Session["VehicleClass_imgPath"].ToString() + "' draggable='false'>" +
                                                           "<p> " + Session["VehicleClass"].ToString() + " Vehicle </p>";
            LiteralFuelType.Text = "<p><span>" + Session["VehicleFuelType"].ToString() + "</span></p>";
        }

        protected void btnRepincode_Click(object sender, EventArgs e)
        {



        }
    }
}