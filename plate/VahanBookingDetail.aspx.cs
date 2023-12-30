using BMHSRPv2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2.plate
{
    public partial class VahanBookingDetail : System.Web.UI.Page
    {
        String ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        string SQLString = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            Session["VehicleType_imgPath"] = "www";
            Session["OEMImgPath"] = "www";

            //if (!CheckSession.Checksession1(7, "plate"))
            //{
            //    Response.Redirect("../Error.aspx");
            //}
            if (!IsPostBack)
            {
                //hdnStateID.Value = Session["StateId"].ToString();
                //if(Session["StateId"].ToString()== "31")
                //{
                //    EmailLabel.Visible = false;
                //}
                //else
                //{
                //    EmailLabel.Visible = true;
                //}
                SetSideBar();
                FillState();
                string[] oemarray;
                string oemid = ConfigurationManager.AppSettings["OemID"].ToString();

                //string _nonhomo = Session["NonHomo"].ToString();

                //if (_nonhomo == "Y")
                //{

                //    divrcupload.Visible = true;

                //}

                //if (oemid.Contains(','))
                //{
                //    oemarray = oemid.Split(',');
                //    foreach (string var in oemarray)
                //    {
                //        if (var == Session["Oemid"].ToString())
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
            LiteralBookingTypeImage.Text = "<img src='" + Session["OrderType_imgPath"].ToString() + "' draggable='false'>";
            LiteralVehicleTypeImage.Text = "<img src='" + Session["VehicleType_imgPath"].ToString() + "' draggable='false'>";
            LiteralOemImage.Text = "<img src='" + Session["OEMImgPath"].ToString() + "' draggable='false'>";
            LiteralState.Text = "<p><span>" + Session["StateShortName"].ToString() + "</span>" + Session["StateName"].ToString() + "</p>";
            LiteralVehicleClassImage.Text = "<img src='" + Session["VehicleClass_imgPath"].ToString() + "' draggable='false'>" +
                                                           "<p> " + Session["VehicleClass"].ToString() + " Vehicle </p>";
            LiteralFuelType.Text = "<p><span>" + Session["VehicleFuelType"].ToString() + "</span></p>";
        }

        protected void btnBulkUpload_Click(object sender, EventArgs e)
        {
            Response.Redirect("BulkUpload.aspx");
        }
    }
}