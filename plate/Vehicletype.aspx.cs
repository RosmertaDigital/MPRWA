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
    public partial class Vehicletype : System.Web.UI.Page
    {
        String ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        string _oemid = string.Empty;
        string _vehicleClass = string.Empty; //Session["VehicleCat"]
        string CategoryId = string.Empty; 
        string _vehicleClassImgPath = string.Empty;
        string _orderType = string.Empty;
        string _fuelType = string.Empty;
        StringBuilder forlitral = new StringBuilder();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckSession.Checksession1(6, "plate"))
            {
                Response.Redirect("../Error.aspx");
            }

            //HttpContext.Current.Session["OEMId"] = "22";
            _oemid = HttpContext.Current.Session["OEMId"].ToString();
            _vehicleClass = HttpContext.Current.Session["VehicleClass"].ToString();            
            _vehicleClassImgPath = HttpContext.Current.Session["VehicleClass_imgPath"].ToString();
            _fuelType = "Oth";
            if (HttpContext.Current.Session["VehicleFuelType"].ToString() == "electric")
            {
                _fuelType = "Elect";
            }

            if (Session["CategoryId"] != null)
            {
                CategoryId = HttpContext.Current.Session["CategoryId"].ToString();
            }

            _orderType = "OB";


           string queryString = @" select ora.OemId, ora.BMHVehicleType [vehicleType],ora.VehicleType [vehicleTypevalue],vehicleclass ,
				
				case when ora.BMHCategory =1 then '2W'
				when ora.BMHCategory =2 then '3W'
				when ora.BMHCategory in (3,4,5) then '4W'
				end [vehicleCategory],
                case when ora.BMHCategory in (1,2) and BMHFuleType != 'Elect' then 2  
                when ora.BMHFuleType != 'Elect' then 1
                when ora.BMHFuleType = 'Elect' then 3
                else 0 end [vehicletypeid],
				vc.icon,vc.Vehiclename,vc.Vehiclecategoryid
                from [HSRPOEM].dbo.[OemRates] ora
               
                left join [HSRPOEM].dbo.VehicleCategory vc on vc.Vehiclecategoryid=ora.BMHCategory
                where 
				OrderType='OB' and ora.BMHFuleType is not null 
				and oemid=" + _oemid+" and VehicleClass='"+_vehicleClass+"'   and BMHFuleType= '"+_fuelType+"'"+  
				@"group by ora.OemId, ora.Vehicletypenew,ora.VehicleType,vehicleclass ,BMHCategory,ora.BMHFuleType,ora.BMHVehicleType,icon,Vehiclename,vc.Vehiclecategoryid
				";


           if (CategoryId == "2" && _oemid == "22")
           {
               queryString = @" select ora.OemId, ora.BMHVehicleType [vehicleType],ora.VehicleType [vehicleTypevalue],vehicleclass ,
				
				case when ora.BMHCategory =1 then '2W'
				when ora.BMHCategory =2 then '3W'
				when ora.BMHCategory in (3,4,5) then '4W'
				end [vehicleCategory],
                case when ora.BMHCategory in (1,2) and BMHFuleType != 'Elect' then 2  
                when ora.BMHFuleType != 'Elect' then 1
                when ora.BMHFuleType = 'Elect' then 3
                else 0 end [vehicletypeid],
				vc.icon,vc.Vehiclename,vc.Vehiclecategoryid
                from [HSRPOEM].dbo.[OemRates] ora
               
                left join [HSRPOEM].dbo.VehicleCategory vc on vc.Vehiclecategoryid=ora.BMHCategory
                where 
				OrderType='OB' and ora.BMHFuleType is not null 
				and oemid=" + _oemid + " and VehicleClass='" + _vehicleClass + "'  and ora.BMHCategory = '2'  and BMHFuleType= '" + _fuelType + "'" +
                @"group by ora.OemId, ora.Vehicletypenew,ora.VehicleType,vehicleclass ,BMHCategory,ora.BMHFuleType,ora.BMHVehicleType,icon,Vehiclename,vc.Vehiclecategoryid
				";
           }

            DataTable _vehicleTypeDt = new DataTable();

            _vehicleTypeDt = Utils.GetDataTable(queryString, ConnectionString);


            if (_vehicleTypeDt.Rows.Count > 0)

            {
                string _vehicleType = string.Empty;
                string _vehicleTypeImgPath = string.Empty;
                string _divid = string.Empty; 
                string filterValue = string.Empty;
                DataView dv = new DataView(_vehicleTypeDt);
                dv.RowFilter = string.Empty;
                List<string> _vechCat = new List<string>();
                DataTable _vnameIcon = new DataTable();
                _vnameIcon = _vehicleTypeDt.DefaultView.ToTable(false, "Vehiclename", "icon", "vehicleCategory");
                DataView _vechIconName = new DataView(_vnameIcon);
                DataTable distinctVechIcon = _vechIconName.ToTable(true, "Vehiclename", "icon", "vehicleCategory");
                DataTable distinctmain = dv.ToTable(true, "OemId", "vehicleType", "vehicleTypevalue", "vehicleclass", "vehicleCategory", "vehicletypeid", "icon", "Vehiclename", "Vehiclecategoryid");
                foreach (DataRow _vehicon in distinctVechIcon.Rows)
                {

                    filterValue = "Vehiclename='" + _vehicon["Vehiclename"].ToString() + "'";
                    dv.RowFilter = filterValue;
                    //string[] _imagePathSplit = _vehicon["icon"].ToString().Trim().Split('.');
                    string _whiteImage = ".."+_vehicon["icon"].ToString().Trim().Split('.')[2] + "-w.svg";
                    HttpContext.Current.Session["VehicleType_imgPath"] = _whiteImage;
                    forlitral.Append(" <div class='table_3'> " +
                                        " <div class='bars pages color3'>"
                                     );
                    forlitral.Append("<div class='short'>" +
                              "<div id=" + _vehicon["vehicleCategory"].ToString().Trim() + " class='img style1'><img src = " + _vehicon["icon"].ToString().Trim() + " >" +
                         "<p>" + _vehicon["Vehiclename"].ToString().Trim() + "</p>" +
                          "</div>" +
                          "<div class='upper_view'>" +
                         "<img src = " +_whiteImage+ " >" +
                          "<div class='clearfix'>" +
                         " <div class='control'>"

                          );
                    string vehiclecategory = string.Empty;
                    foreach (DataRow row in distinctmain.Rows)
                    {

                        if (_vehicon["vehicleCategory"].ToString().Trim() == row["vehicleCategory"].ToString().Trim())
                        {
                            string passvalue = string.Empty;
                            passvalue = row["vehicletypeid"].ToString().Trim() + "@" + row["vehicleTypevalue"].ToString().Trim() + "@" + row["vehicleCategory"].ToString().Trim() + "@" + row["Vehiclecategoryid"].ToString().Trim();
                            forlitral.Append(

                                    "<div class='radio'>" +
                                        "<input type = 'radio' name='vehicle_Category' class='rdo1' value='" + passvalue + "'>" +
                                      "  <i></i>" +
                                        "<span>" + row["vehicleTypevalue"].ToString() + "</span>"
                                   + "   </div> "

                                );
                        }
                    }

                    forlitral.Append("<div class='control text-right'>" +
                                   "<a id='setvehicletype' class='btn vtype'>Next</a> " +

                               "</div>" +
                           " </div>" +
                           " </div> </div> " +
                           " </div>" +
                           "</div> </div>"
                           );
                }
          
            }

            showvehicle_cat.Text = forlitral.ToString();

        }
    }
}