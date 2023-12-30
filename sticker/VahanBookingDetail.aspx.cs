using BMHSRPv2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2.sticker
{
    public partial class VahanBookingDetail : System.Web.UI.Page
    {
        string CnnString = string.Empty;
        string SQLString = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["S_VehicleCat"] = "4W";
                Session["S_Vehicletype"] = "LMV";
                Session["S_Vehicletypeid"] = "1";
                Session["S_Vehiclecategoryid"] = "3";

                //hdnStateID.Value = Session["S_StateId"].ToString();
                //if (Session["S_StateId"].ToString() == "31")
                //{
                //    EmailLabel.Visible = false;
                //}
                //else
                //{
                //    EmailLabel.Visible = true;
                //}

                if (Session["S_OrderType"] != null && Session["S_Oemid"] != null)
                {

                    SetSideBar();
                    FillState();
                    // string[] oemarray;
                    //string oemid = ConfigurationManager.AppSettings["OemID"].ToString();
                    //if (oemid.Contains(','))
                    //{
                    //    oemarray= oemid.Split(',');
                    //    foreach(string var in oemarray)
                    //    {
                    //        if(var== Session["Oemid"].ToString())
                    //        {
                    //            divrcupload.Visible = true;
                    //        }
                    //    }

                    //}
                    //else
                    //{
                    //    if (Session["Oemid"].ToString() == oemid)
                    //    {

                    //        divrcupload.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        divrcupload.Visible = false;
                    //    }

                    //}

                }
                else
                {                  
                    CnnString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                    if (!IsPostBack)
                    {
                       
                        
                    }
                }

            }

           
        }
        private void FillState()
        {
            try
            {
                string Qstr = "select HSRP_StateID,HSRPStateName from hsrpstate order by HSRPStateName";
                DataTable dtState = Utils.GetDataTable(Qstr, ConnString.ConString());
                if (dtState.Rows.Count > 0)
                {
                    ddlState.DataTextField = "HSRPStateName";
                    ddlState.DataValueField = "HSRPStateName";
                    ddlState.DataSource = dtState;
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, "--Select State--");





                }
            }
            catch (Exception ex)
            {
                throw ex;
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
        }
    }
}