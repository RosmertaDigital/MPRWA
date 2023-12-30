using BMHSRPv2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2.plate
{
    public partial class dealerinformation : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            //for indivisual page test
            //comment below session variable when merge 
            //if (!CheckSession.Checksession1(11, "plate"))
            //{
            //    Response.Redirect("../Error.aspx");
            //}

            SetSideBar();

            

            
                String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                string querystring = @"SELECT daf.[DealerAffixationID], daf.[DealerAffixationCenterName],daf.[DealerAffixationCenterAddress]
                                        ,daf.[DealerAffixationCenterCity],daf.[DealerAffixationCenterPinCode],daf.[DealerAffixationCenterLat]
                                        ,daf.[DealerAffixationCenterLon],hs.HSRPStateName,daf.[Landmark]
                                FROM[dbo].[DealerAffixationCenter] daf
                                inner join[dbo].[hsrpstate] hs on hs.HSRP_StateID=daf.StateID
                                    where DealerAffixationCenterLat is not null and isnumeric(DealerAffixationCenterLat )=1  and  DealerAffixationCenterLon not in ('0','-')";
                DataTable _dealerLocation = new DataTable();
              //  _dealerLocation = Utils.GetDataTable(querystring, connectionString);



            

            

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
    }
}