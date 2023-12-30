using BMHSRPv2.plate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2.sticker
{
    public partial class BookingSummary : System.Web.UI.Page
    {
        string CnnString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        string SQLString = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckSession.Checksession1(13, "sticker"))
            {
                Response.Redirect("../Error.aspx");
            }
            if (!IsPostBack)
            {
                SetSideBar();
                

                    //string BharatStage = Session["SessionBharatStage"].ToString();

                    //string RegDate = Session["SessionRegDate"].ToString();
                    //string RegNo = Session["SessionRegNo"].ToString();
                    //string Chasisno = Session["SessionChasisno"].ToString();
                    //string Engno = Session["SessionEngno"].ToString();
                    //string OwnerName = Session["SessionOwnerName"].ToString();
                    //string EmailID = Session["SessionEmailID"].ToString();
                   
                    AppDate.Text = Session["S_SelectedSlotDate"].ToString();
                    TimeSlot.Text = Session["S_SelectedSlotTime"].ToString();
                    BharatStages.Text = Session["S_SessionBharatStage"].ToString();
                    ltrlRest.Text = Session["S_SessionRegNo"].ToString();
                    EngineNo.Text = Session["S_SessionEngno"].ToString();
                    ChassisNo.Text = Session["S_SessionChasisno"].ToString();
                   // VehicleMake.Text = "Honda";
                    //VehicleModel.Text = "Car";
                    VehicleType.Text = Session["S_VehicleCat"].ToString();
                    if (Session["S_DeliveryPoint"].ToString() == "Home")
                    {


                    string CheckDealerAffixationQuery = "SELECT name FROM [HSRPOEM].[dbo].OEMMaster  where oemid='" + Session["S_Oemid"].ToString() + "'";
                    DataTable dtOem = BMHSRPv2.Models.Utils.GetDataTable(CheckDealerAffixationQuery, CnnString);
                    if (dtOem.Rows.Count > 0)
                    {
                        VehicleMake.Text = dtOem.Rows[0]["name"].ToString();
                    }

                    if (Session["S_DeliveryAddress1"] != null && Session["S_DeliveryAddress1"].ToString().Length > 0)
                        {

                            AppAddress.Text = Session["S_DeliveryAddress1"].ToString() + " " + Session["S_DeliveryAddress2"].ToString() + " " + Session["S_Deliverycity"].ToString() + " " + Session["S_DeliveryState"].ToString();


                        }
                        else if (Session["S_mapAddress1"] != null && Session["S_mapAddress1"].ToString().Length > 0)
                        {
                            AppAddress.Text = Session["S_mapAddress1"].ToString();
                        }


                    }
                    else
                    {
                        try
                        {
                            string CheckDealerAffixationQuery = "select a.OemID, c.Name OemName, a.DealerID, a.StateID, a.RTOLocationID,b.RTOLocationName,a.DealerAffixationCenterName, a.DealerAffixationCenterAddress  " +
                          "from [HSRPOEM].dbo.DealerAffixationCenter a, [HSRPOEM].dbo.rtolocation b, [HSRPOEM].dbo.OEMMaster c  " +
                          "where a.rtolocationid = b.RTOLocationID and a.OemID = c.OEMID " +
                          " and DealerAffixationID = '" + Session["S_DealerAffixationCenterid"].ToString() + "' ";
                            DataTable dtDealerAffixation = BMHSRPv2.Models.Utils.GetDataTable(CheckDealerAffixationQuery, CnnString);
                            if (dtDealerAffixation.Rows.Count > 0)
                            {

                                string ManufacturerName = dtDealerAffixation.Rows[0]["OemName"].ToString();
                                string DealerAffixationCenterName = dtDealerAffixation.Rows[0]["DealerAffixationCenterName"].ToString();
                                string DealerAffixationCenterAddress = dtDealerAffixation.Rows[0]["DealerAffixationCenterAddress"].ToString();

                                VehicleMake.Text = ManufacturerName;
                                AppAddress.Text = DealerAffixationCenterName + " " + DealerAffixationCenterAddress;
                            }

                        }
                        catch (Exception ex)
                        {

                        }
                     
                    }

               

            }
        }
        private void SetSideBar()
        {
            LiteralBookingTypeImage.Text = "<img src='" + Session["S_OrderType_imgPath"].ToString() + "' draggable='false'>";
            LiteralVehicleTypeImage.Text = "<img src='" + Session["S_VehicleType_imgPath"].ToString() + "' draggable='false'>";
            LiteralOemImage.Text = "<img src='" + Session["S_OEMImgPath"].ToString() + "' draggable='false'>";
            LiteralState.Text = "<p><span>" + Session["S_StateShortName"].ToString() + "</span>" + Session["S_StateName"].ToString() + "</p>";
            LiteralVehicleClassImage.Text = "<img src='" + Session["S_VehicleClass_imgPath"].ToString() + "' draggable='false'>" +
                                                           "<p> " + Session["S_VehicleClass"].ToString() + " Vehicle </p>";
            LiteralFuelType.Text = "<p><span>" + Session["S_VehicleFuelType"].ToString() + "</span></p>";

            try
            {
                if (Session["S_DeliveryPoint"] != null && Session["S_DeliveryPoint"].ToString() == "Dealer")
                {
                    string CheckDealerAffixationQuery = "select a.OemID, c.Name OemName, a.DealerID, a.StateID, a.RTOLocationID,b.RTOLocationName,a.DealerAffixationCenterName, a.DealerAffixationCenterAddress  " +
             "from [HSRPOEM].dbo.DealerAffixationCenter a, [HSRPOEM].dbo.rtolocation b, [HSRPOEM].dbo.OEMMaster c  " +
             "where a.rtolocationid = b.RTOLocationID and a.OemID = c.OEMID " +
             " and DealerAffixationID = '" + Session["S_DealerAffixationCenterid"].ToString() + "' ";
                    DataTable dtDealerAffixation = BMHSRPv2.Models.Utils.GetDataTable(CheckDealerAffixationQuery, CnnString);
                    if (dtDealerAffixation.Rows.Count > 0)
                    {

                        string ManufacturerName = dtDealerAffixation.Rows[0]["OemName"].ToString();
                        string DealerAffixationCenterName = dtDealerAffixation.Rows[0]["DealerAffixationCenterName"].ToString();
                        string DealerAffixationCenterAddress = dtDealerAffixation.Rows[0]["DealerAffixationCenterAddress"].ToString();

                        AffixationAddress.Text = "<b>" + DealerAffixationCenterName + "<br>" +
                                   "</b>" +
                               "<p>" + DealerAffixationCenterAddress + " </p>";

                    }
                }
                else if (Session["S_DeliveryAddress1"] != null && Session["S_DeliveryAddress1"].ToString().Length > 0)
                {

                    AffixationAddress.Text = Session["S_DeliveryAddress1"].ToString() + " " + Session["S_DeliveryAddress2"].ToString() + " " + Session["S_Deliverycity"].ToString() + " " + Session["S_DeliveryState"].ToString();


                }
                else if (Session["S_mapAddress1"] != null && Session["S_mapAddress1"].ToString().Length > 0)
                {
                    AffixationAddress.Text = Session["S_mapAddress1"].ToString();
                }



            }
            catch (Exception ex)
            {

            }



        }
    }

}