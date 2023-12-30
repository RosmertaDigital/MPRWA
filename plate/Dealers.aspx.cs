using BMHSRPv2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2.plate
{
    public partial class Dealers : System.Web.UI.Page
    {
       
        String ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        StringBuilder sbTable = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["DeliveryPoint"] = "Dealer";
            if (!CheckSession.Checksession1(9, "plate"))
            {
                Response.Redirect("../Error.aspx");
            }
            if (!IsPostBack)
            {
                SetSideBar();
                BindDealers();
                if (Session["StateId"].ToString() == "31")
                {
                    ddlcity.Visible = true;
                    FillCity();
                }
                else
                {
                    ddlcity.Visible = false;
                }

            }
           

            //LitralDealer.Text = " <div class='dealer'>" +
            //                         "<div class='top clearfix'>" +
            //                            "<p>Shiv Ganga Automobiles</p>" +
            //                            "<span><b>Distance: </b>5 KM</span>" +

            //                            "<div class='arrow'></div>" +
            //                        "</div>" +
            //                        "<div class='subdata'>" +
            //                            "<div class='arrow'></div>" +
            //                            "<p class='cmp'>Shiv Ganga Automobiles</p>" +
            //                            "<div class='clearfix'>" +
            //                                "<div class='width50 width30'>" +
            //                                    "<span><b>Distance: </b>5 KM</span>" +
            //                                "</div>" +
            //                                "<div class='width50 width30'>" +
            //                                    "<span><b>City: </b>NEW DELHI</span>" +
            //                                "</div>" +
            //                            "</div>" +
            //                            "<div class='clearfix'>" +
            //                                "<div class='width50 widthfull'>" +
            //                                    "<span><b>Address: </b><br>A-1/2, Paschim Vihar, Main Rohtak Road, Near Peera Garhi Chowk,<br>New Delhi - 110063</span>" +
            //                                "</div>" +
            //                            "</div>" +

            //                            "<div class='ticks clearfix'>" +
            //                                "<p class='fg1'><span>HSRP & Colour Sticker Cost:</span>Rs. 354</p>" +
            //                                "<p class='fg2'><span>Earliest Date Available:</span>14-11-2020</p>" +
            //                                "<p class='fg3'><span>Earliest Time Slot Available:</span>10:00am-11:00am</p>" +

            //                                "<a href = 'page10.html' class='btn filled'>Confirm Dealer</a>" +
            //                            "</div>" +
            //                        "</div>" +
            //                    "</div>";
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
        private void BindDealers()
        {
            LitralDealer.Text = "";

            ddlstate.SelectedValue = Session["StateId"].ToString();
            if (Session["Oemid"]!=null && Session["StateId"]!=null && Session["VehicleCat"] != null && Session["Vehicletype"] != null && Session["VehicleClass"]!=null)
            {
                string  TotalAmountWithGST="0.00";

                try
                 {
                    string Qstr = string.Empty;
                    if (HttpContext.Current.Session["Oemid"].ToString() == "272" && HttpContext.Current.Session["VehicleType"].ToString() == "Scooter_2W")
                    {
                        Qstr = "execute BMHSRP_GET_DEALERS  '" + Session["Oemid"].ToString() + "','" + Session["StateId"].ToString() + "','','" + Session["VehicleCat"].ToString() + "','Scooter_Hero','" + Session["VehicleClass"].ToString() + "'";
                    }
                    else
                    {
                        Qstr = "execute BMHSRP_GET_DEALERS  '" + Session["Oemid"].ToString() + "','" + Session["StateId"].ToString() + "','','" + Session["VehicleCat"].ToString() + "','" + Session["Vehicletype"].ToString() + "','" + Session["VehicleClass"].ToString() + "'";
                    }

                    
                        DataTable dtVehicleCat = Utils.GetDataTable(Qstr, ConnectionString);
                        if (dtVehicleCat.Rows.Count > 0)
                        {
                            string CheckOemRateQuery = "CheckOrdersRates '" + Session["OEMId"].ToString() + "', 'OB','" + Session["VehicleClass"].ToString() + "', '" + Session["VehicleType"].ToString() + "','" + Session["Vehiclecategoryid"].ToString() + "','" + Session["VehicleFuelType"].ToString() + "','" + Session["DeliveryPoint"].ToString() + "','" + Session["Stateid"].ToString() + "','" + Session["SessionState"].ToString() + "'";
                            DataTable dtOemrates = Utils.GetDataTable(CheckOemRateQuery, ConnectionString);
                            if (dtOemrates.Columns.Count > 2)
                            {
                                if (dtOemrates.Rows.Count > 0)
                                {
                                    decimal TotalAmount = Convert.ToDecimal(dtOemrates.Rows[0]["TotalAmount"]);
                                    TotalAmountWithGST = TotalAmount.ToString();
                                }
                            else
                            {
                                TotalAmountWithGST = "Rate Not Found";
                            }
                        }
                        else
                        {
                            TotalAmountWithGST = "Rate Not Found";
                        }

                        sbTable = new StringBuilder();
                        foreach (DataRow dr in dtVehicleCat.Rows)
                             {
                              sbTable.Append(" <div class='dealer'>" +
                                         "<div class='top clearfix'>" +
                                            "<p id='" + dr["DealerAffixationID"].ToString() + "' class='expand' >" + dr["DealerAffixationCenterName"].ToString() + "</p>" +  //-" + dr["City"].ToString() + "
                                            "" +
                                          "<div id='" + dr["DealerAffixationID"].ToString() + "' class='arrow'></div>" +

                                        "</div>" +
                                        "<div class='subdata'>" +
                                            "<div class='arrow'></div>" +
                                            "" + //-" + dr["City"].ToString() + "
                                            "<div class='clearfix'>" +
                                                "<div class='width50 width30'>" +
                                                    "" +
                                                "</div>" +
                                                "<div class='width50 width30'>" +
                                                    "<span><b>City: </b>" + dr["City"].ToString() + "</span>" +
                                                "</div>" +
                                            "</div>" +
                                            "<div class='clearfix'>" +
                                                "<div class='width50 widthfull'>" +
                                                    "<span><b>Address: </b><br>" + dr["Address"].ToString() + "<br>"+ dr["StateName"].ToString() + " - " + dr["Pincode"].ToString() + "</span>" +
                                                "</div>" +
                                            "</div>" +
                                            
                                            "<div class='ticks clearfix'>" +
                                            "<p class='fg1'><span>HSRP Set </span>Rs." + TotalAmountWithGST.ToString() + "</p>" +
                                                //"<p class='fg1'><span>HSRP Set:</span>Rs."+ dr["RoundOff_netamount"].ToString() +"</p>" +
                                                //"<p class='fg2'><span>Earliest Date Available:</span>" + dr["EarliestDateAvailable"].ToString() + "</p>" +
                                                //"<p class='fg3'><span>Earliest Time Slot Available:</span>" + dr["EarliestTimeSlotAvailable"].ToString() + "</p>" +

                                                "<a href = '#' style='display: none'  id='" + dr["DealerAffixationID"].ToString() + "' class='btn filled DealerClick'>Confirm Dealer</a>" +
                                            "</div>" +
                                        "</div>" +
                                    "</div>");

                             }



                            LitralDealer.Text = sbTable.ToString();
                        }
                 }

            catch (Exception ex)
            {

            }

            }
            else
            {
                Response.Redirect("~/Index.aspx");
            }
        }
        
        private void FillCity()
        {
            try
            {
               
             
                string Qstr = "exec BindCity " + Session["StateId"].ToString().Trim() + "";
                DataTable dtCity = Utils.GetDataTable(Qstr, ConnString.ConString());
                if (dtCity.Rows.Count > 0)
                {


                    ddlcity.DataTextField = "District";
                    ddlcity.DataValueField = "District";

                    ddlcity.DataSource = dtCity;
                    ddlcity.DataBind();
                    //ddlcity.Items.Insert(-1, "--Select City--");
                    ddlcity.Items.Insert(0, "All");




                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                ddlcity.Items.Clear();
                string Stateid=  ddlstate.SelectedValue.ToString();
                if (Stateid == "37")
                {
                    HttpContext.Current.Session["StateId"] = Stateid;
                    HttpContext.Current.Session["StateShortName"] = "Delhi";
                    HttpContext.Current.Session["StateName"] = "DL";

                }

                if (Stateid == "31")
                {
                    HttpContext.Current.Session["StateId"] = Stateid;
                    HttpContext.Current.Session["StateShortName"] = "Uttar Pradesh";
                    HttpContext.Current.Session["StateName"] = "UP";

                }
                if (Stateid == "3")
                {
                    HttpContext.Current.Session["StateId"] = Stateid;
                    HttpContext.Current.Session["StateShortName"] = "HIMACHAL PRADESH";
                    HttpContext.Current.Session["StateName"] = "HP";

                }

                BindDealers();
                if (Session["StateId"].ToString() == "31")
                {
                    ddlcity.Visible = true;
                    FillCity();
                }
                else
                {
                    ddlcity.Visible = false;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}