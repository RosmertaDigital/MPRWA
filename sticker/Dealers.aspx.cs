using BMHSRPv2.Models;
using BMHSRPv2.plate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2.sticker
{
    public partial class Dealers : System.Web.UI.Page
    {
       
        String ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        StringBuilder sbTable = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["S_DeliveryPoint"] = "Dealer";
            if (!CheckSession.Checksession1(9, "sticker"))
            {
                Response.Redirect("../Error.aspx");
            }
            SetSideBar();
            BindDealers();
            if (Session["S_StateId"].ToString()=="31")
            {
              
                FillCity();
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
            LiteralBookingTypeImage.Text = "<img src='" + Session["S_OrderType_imgPath"].ToString() + "' draggable='false'>";
            LiteralVehicleTypeImage.Text = "<img src='" + Session["S_VehicleType_imgPath"].ToString() + "' draggable='false'>";
            LiteralOemImage.Text = "<img src='" + Session["S_OEMImgPath"].ToString() + "' draggable='false'>";
            LiteralState.Text = "<p><span>" + Session["S_StateShortName"].ToString() + "</span>" + Session["S_StateName"].ToString() + "</p>";
            LiteralVehicleClassImage.Text = "<img src='" + Session["S_VehicleClass_imgPath"].ToString() + "' draggable='false'>" +
                                                           "<p> " + Session["S_VehicleClass"].ToString() + " Vehicle </p>";
            LiteralFuelType.Text = "<p><span>" + Session["S_VehicleFuelType"].ToString() + "</span></p>";
        }
        private void BindDealers()
        {
            LitralDealer.Text = "";

            
            if(Session["S_Oemid"]!=null && Session["S_StateId"]!=null && Session["S_VehicleCat"] != null && Session["S_Vehicletype"] != null && Session["S_VehicleClass"]!=null)
            {
                string  TotalAmountWithGST="0.00";

                try
                 {
                        string Qstr = "execute BMHSRP_GET_DEALERS  '"+ Session["S_Oemid"].ToString() + "','"+ Session["S_StateId"].ToString()+ "','','"+ Session["S_VehicleCat"].ToString() + "','"+ Session["S_Vehicletype"] .ToString()+ "','"+ Session["S_VehicleClass"].ToString()+ "'";
                        DataTable dtVehicleCat = Utils.GetDataTable(Qstr, ConnectionString);
                        if (dtVehicleCat.Rows.Count > 0)
                        {
                        string CheckOemRateQuery = "CheckOrdersRates '" + Session["S_OEMId"].ToString() + "', 'OB','" + Session["S_VehicleClass"].ToString() + "', '" + Session["S_VehicleType"].ToString() + "','" + Session["S_Vehiclecategoryid"].ToString() + "','" + Session["S_VehicleFuelType"].ToString() + "','" + Session["S_DeliveryPoint"].ToString() + "','" + Session["S_StateId"].ToString() + "','" + Session["S_SessionState"].ToString() + "','sticker'";
                        DataTable dtOemrates = Utils.GetDataTable(CheckOemRateQuery, ConnectionString);
                        if (dtOemrates.Columns.Count > 2)
                        {

                       
                            if (dtOemrates.Rows.Count > 0)
                            {
                                //decimal GstBasic_Amt = Convert.ToDecimal(dtOemrates.Rows[0]["GstBasic_Amt"]) + Convert.ToDecimal(dtOemrates.Rows[0]["FittmentCharges"]);
                                //decimal FittmentCharges = Convert.ToDecimal(dtOemrates.Rows[0]["FittmentCharges"]);
                                //decimal BMHConvenienceCharges = Convert.ToDecimal(dtOemrates.Rows[0]["BMHConvenienceCharges"]);
                                //decimal BMHHomeCharges = Convert.ToDecimal(dtOemrates.Rows[0]["BMHHomeCharges"]);
                                //decimal GrossTotal = Convert.ToDecimal(dtOemrates.Rows[0]["GrossTotal"]);
                                //decimal GSTAmount = Convert.ToDecimal(dtOemrates.Rows[0]["GSTAmount"]);
                                //decimal GstRate = Convert.ToDecimal(dtOemrates.Rows[0]["gst"]);
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
               
             
                string Qstr = "exec BindCity " + Session["S_StateId"].ToString().Trim() + "";
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

        
    }
}