using System;using BMHSRPv2.plate;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BMHSRPv2.Models;

namespace BMHSRPv2.sticker
{
    public partial class VerifyDetailPay : System.Web.UI.Page
    {
        string CnnString = string.Empty;
        string SQLString = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["S_DeliveryPincode"] = "121004";
            Session["S_DeliveryAddress1"]="Ram Chowk Udyog Vihar";
                if (Session["S_OrderType"] != null && Session["S_DeliveryAddress1"] != null && Session["S_SessionBharatStage"] != null && Session["S_SessionRegDate"] != null && Session["S_SessionRegNo"] != null && Session["S_SessionChasisno"] != null && Session["S_SessionEngno"] != null && Session["S_SessionOwnerName"] != null && Session["S_SessionEmailID"] != null && Session["S_DeliveryPincode"] != null && Session["S_SessionMobileNo"] != null)
                {
                    
                    string OwnerName=Session["S_SessionOwnerName"].ToString();
                    string MobileNo=Session["S_SessionMobileNo"].ToString();
                    string Address=Session["S_DeliveryAddress1"].ToString();
                    string emailid=Session["S_SessionEmailID"].ToString();
                    string PinCode=Session["S_DeliveryPincode"].ToString();

                    ltrlON.Text = OwnerName;
                    ltrlMN.Text = MobileNo;
                    ltrlAddress.Text = Address;
                    ltrlEmail.Text = emailid;
                    ltrlPinCode.Text = PinCode;

                } 
                else
                {
                   Response.Redirect("Index.aspx");
                }
                    CnnString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                    if (!IsPostBack)
                    {
                        SQLString = "select top 1 GstBasic_Amt,FittmentCharges,cgstper,roundoff_netamount from HSRPOEM.dbo.OemRates";
                        DataTable dt = Utils.GetDataTable(SQLString, CnnString);
                        if (dt.Rows.Count > 0)
                        {
                            ltlCost.Text = dt.Rows[0]["GstBasic_Amt"].ToString();
                            ltlFCharge.Text = dt.Rows[0]["FittmentCharges"].ToString();
                            ltlgst.Text = dt.Rows[0]["cgstper"].ToString();
                            ltltcost.Text = dt.Rows[0]["roundoff_netamount"].ToString();                          
                        }
                    }
                }

            }
        }
   