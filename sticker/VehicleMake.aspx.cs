using BMHSRPv2.Models;
using System;using BMHSRPv2.plate;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2.sticker
{
    public partial class VehicleMake : System.Web.UI.Page
    {
        String ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        StringBuilder sbTable = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            string twmp = Session["S_OrderType"].ToString();
            Session["S_VehicleType_imgPath"] = @"..\assets\img\car-w.svg";
            if (!CheckSession.Checksession1(2, "sticker"))
            {
                Response.Redirect("Error.aspx");
            }

            if (!IsPostBack)
            { 
                
                LiteralBookingTypeImage.Text = "<img src='"+ Session["S_OrderType_imgPath"].ToString()+"'>";
                BindVehicleCat();
               
            }

        }

        private void BindVehicleCat()
        {
            try
            {
                string Qstr = "execute BMHSRP_GetVehicleCategory";
                DataTable dtVehicleCat = Utils.GetDataTable(Qstr, ConnectionString);
                if (dtVehicleCat.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtVehicleCat.Rows)
                    {
                        if (dr["Vehiclecategoryid"].ToString() != "1")
                        {
                            sbTable.Append("<div id='" + dr["Vehiclecategoryid"].ToString() + "'  class='label s_active Vehicletype'>" +
                               "<img src = '" + dr["icon"].ToString() + "' >" +
                               "<p> " + dr["Vehiclename"].ToString() + " </p>" +
                           "</div>");
                        }
                       
                    }
                       

                    LitVehicleCat.Text = sbTable.ToString();
                }
            }

            catch (Exception)
            {

            }
        }

        private void Bindoem()
        {
            try
            {
                sbTable.Clear();
                string Qstr = "execute [HSRPOEM].[dbo].BMHSRPGetAllOem";
                DataTable dtVehicleCat = Utils.GetDataTable(Qstr, ConnectionString);
                if (dtVehicleCat.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dtVehicleCat.Rows)
                    {
                        if (i % 2 == 0)
                        {
                            sbTable.Append("<div class=' item Oem " + dr["BMHvehiclecategory"].ToString().Replace(',', ' ') + "'>" +

                                                 "<a href = '#' title= '" + dr["name"].ToString() + "' class='label '>" +
                                                     "<img src = '" + dr["oem_logo"].ToString() + "' alt='" + dr["name"].ToString() + "' class='simpleview' longdesc='" + dr["nonhomooem"].ToString() + "'> " +


                                                 "</a>"

                                                 ); 
                        }
                        else
                        {
                            sbTable.Append("<a href = 'page2.html' class='label '>" +
                                                 "<img src = '" + dr["oem_logo"].ToString() + "' alt='" + dr["name"].ToString() + "'class='simpleview'  longdesc='" + dr["nonhomooem"].ToString() + "'> " +


                                             "</a>" +
                                         "</div>");
                        }

                        i++;
                    }


                    Literal1.Text = sbTable.ToString();
                }
            }

            catch (Exception)
            {

            }
        }

        protected void btnVehicleCategory_Click(object sender, EventArgs e)
        {
            try
            {
                Literal1.Text = "";
                sbTable.Clear();
                string Catid = HiddenVehicleCatID.Value.ToString();

                string Qstr = "execute [BookMyHSRP].[dbo].BMHSRPGetAllOemByid '" + Catid + "','sticker'";
                DataTable dtVehicleCat = Utils.GetDataTable(Qstr, ConnectionString);
                if (dtVehicleCat.Rows.Count > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:SetVehicleCatSelected('" + Catid + "'); ", true);
                    int i = 0;
                    foreach (DataRow dr in dtVehicleCat.Rows)
                    {
                        if (Catid == "2")
                        {
                            if (dr["oemid"].ToString() != "22")
                            {

                                if (i % 2 == 0)
                                {
                                    sbTable.Append("<div class=' item'>" +

                                                         "<a href = '#' id='" + dr["oemid"].ToString() + "' title= '" + dr["name"].ToString() + "' class='label OemClick' > " +
                                                             "<img src = '" + dr["oem_logo"].ToString() + "' alt='" + dr["name"].ToString() + "' class='simpleview'  longdesc='" + dr["nonhomooem"].ToString() + "'> " +


                                                         "</a>"

                                                         );

                                    if (dtVehicleCat.Rows.Count == i + 1)
                                    {
                                        sbTable.Append("</div>");
                                    }
                                }
                                else
                                {
                                    sbTable.Append("<a href = '#' id='" + dr["oemid"].ToString() + "' title= '" + dr["name"].ToString() + "' class='label OemClick'>" +
                                                         "<img src = '" + dr["oem_logo"].ToString() + "' alt='" + dr["name"].ToString() + "'class='simpleview'  longdesc='" + dr["nonhomooem"].ToString() + "'> " +


                                                     "</a>" +
                                                 "</div>");
                                }

                                i++;
                            }
                        }
                        else 
                        {

                            if (i % 2 == 0)
                            {
                                sbTable.Append("<div class=' item'>" +

                                                     "<a href = '#' id='" + dr["oemid"].ToString() + "' title= '" + dr["name"].ToString() + "' class='label OemClick' > " +
                                                         "<img src = '" + dr["oem_logo"].ToString() + "' alt='" + dr["name"].ToString() + "' class='simpleview'   longdesc='" + dr["nonhomooem"].ToString() + "'> " +


                                                     "</a>"

                                                     );

                                if (dtVehicleCat.Rows.Count == i + 1)
                                {
                                    sbTable.Append("</div>");
                                }
                            }
                            else
                            {
                                sbTable.Append("<a href = '#' id='" + dr["oemid"].ToString() + "' title= '" + dr["name"].ToString() + "' class='label OemClick'>" +
                                                     "<img src = '" + dr["oem_logo"].ToString() + "' alt='" + dr["name"].ToString() + "'class='simpleview'    longdesc='" + dr["nonhomooem"].ToString() + "'> " +


                                                 "</a>" +
                                             "</div>");
                            }

                            i++;
                        }
                    }


                   // sbTable.Append("<script>");
                   // sbTable.Append("$('.type-selection .label').removeClass('s_active')");
                   // sbTable.Append("$(this).addClass('s_active')");
                   // sbTable.Append("$(this).parent().addClass('s_active')");
                   // sbTable.Append("$('.brand-selection').hide().fadeIn()");
                   // sbTable.Append("return false");

                   //sbTable.Append("</script>");


                    Literal1.Text = sbTable.ToString();
                }


            }
            catch (Exception ex)
            {
                return;
            }
        }

        protected void btnSearchOEM_Click(object sender, EventArgs e)
        {
          
        }

        protected void btnSearchOEM_Click1(object sender, EventArgs e)
        {
            try
            {
                Literal1.Text = "";
                sbTable.Clear();
                string COEMSearch = HiddenOemsearch.Value.ToString();

                string Qstr = "execute [BookMyHSRP].[dbo].BMHSRPGetAllOemByname '" + COEMSearch + "','sticker' ";
                DataTable dtVehicleCat = Utils.GetDataTable(Qstr, ConnectionString);
                if (dtVehicleCat.Rows.Count > 0)
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:SetVehicleCatSelected('" + Catid + "'); ", true);
                    int i = 0;
                    foreach (DataRow dr in dtVehicleCat.Rows)
                    {
                        if (i % 2 == 0)
                        {
                            sbTable.Append("<div class=' item'>" +

                                                 "<a href = '#' id='" + dr["oemid"].ToString() + "' title= '" + dr["name"].ToString() + "' class='label OemClick' > " +
                                                     "<img src = '" + dr["oem_logo"].ToString() + "' alt='" + dr["name"].ToString() + "' class='simpleview'  longdesc='" + dr["nonhomooem"].ToString() + "'> " +
                                                "</a>"

                                                 );

                            if (dtVehicleCat.Rows.Count == i + 1)
                            {
                                sbTable.Append("</div>");
                            }
                        }
                        else
                        {
                            sbTable.Append("<a href = '#' id='" + dr["oemid"].ToString() + "' title= '" + dr["name"].ToString() + "' class='label OemClick'>" +
                                                 "<img src = '" + dr["oem_logo"].ToString() + "' alt='" + dr["name"].ToString() + "'class='simpleview'   longdesc='" + dr["nonhomooem"].ToString() + "'> " +


                                             "</a>" +
                                         "</div>");
                        }

                        i++;
                    }


                    // sbTable.Append("<script>");
                    // sbTable.Append("$('.type-selection .label').removeClass('s_active')");
                    // sbTable.Append("$(this).addClass('s_active')");
                    // sbTable.Append("$(this).parent().addClass('s_active')");
                    // sbTable.Append("$('.brand-selection').hide().fadeIn()");
                    // sbTable.Append("return false");

                    //sbTable.Append("</script>");


                    Literal1.Text = sbTable.ToString();
                }


            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}