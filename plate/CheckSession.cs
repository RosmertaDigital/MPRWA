using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BMHSRPv2.Models;
using System.Linq;

namespace BMHSRPv2.plate
{
    static  class CheckSession
    {
        public static bool isSessionLive;


        public static bool Checksession1(int page, string model)
        {

            var allSession = HttpContext.Current.Session.Contents.Keys;
 
            DataTable _dt = Global.SessionTable;
            int dtcount = _dt.Rows.Count;
            int sescount = allSession.Count;
            string _deliveryPoint = string.Empty;
            DataView dv = new DataView(_dt);
            DataView _homeView = new DataView(_dt);
            DataView _dealerView = new DataView(_dt);

            DataTable _dts = Global.stickerSessionTable;
           
            DataView dvs = new DataView(_dts);
            DataView _homeViews = new DataView(_dts);
            DataView _dealerViews = new DataView(_dts);


            // string currentSessionList = HttpContext.Current.Session.Contents.Keys.ToString();
            if (model == "plate")
            {
               
                    dv.RowFilter = "pagerank <" + page + " and sessionflag =0 and pagecat = 0";

                    foreach (DataRowView rowView in dv)
                    {
                        DataRow row = rowView.Row;
                        string _sessionname = row["variableName"].ToString();
                        if (HttpContext.Current.Session[_sessionname].ToString() == "")
                        {
                            return false;
                        }
                    }


                    if (HttpContext.Current.Session["DeliveryPoint"].ToString() != "")
                    {
                        _deliveryPoint = HttpContext.Current.Session["DeliveryPoint"].ToString();
                        //if (_deliveryPoint == "DealerAppointment")
                        //{
                        //    _dealerView.RowFilter = "pagecat in (0,2)";
                        //    foreach (DataRowView rowView in _dealerView)
                        //    {
                        //        DataRow row = rowView.Row;
                        //        string _sessionname = row["variableName"].ToString();
                        //        if (HttpContext.Current.Session[_sessionname].ToString() == "")
                        //        {
                        //            return false;
                        //        }
                        //    }
                        //}

                        if (_deliveryPoint == "HomeDelivery")
                        {
                            _homeView.RowFilter = "pagecat in (0,1)  and sessionflag in =0";

                            foreach (DataRowView rowView in _homeView)
                            {
                                DataRow row = rowView.Row;
                                string _sessionname = row["variableName"].ToString();
                                if (HttpContext.Current.Session[_sessionname].ToString() == "")
                                {
                                    return false;
                                }
                            }


                        }
                    }
            }

            else if (model == "sticker")
            {
                dvs.RowFilter = "pagerank <" + page + " and sessionflag =0 and pagecat = 0";

                foreach (DataRowView rowView in dvs)
                {
                    DataRow row = rowView.Row;
                    string _sessionname = row["variableName"].ToString();
                    if (HttpContext.Current.Session[_sessionname].ToString() == "")
                    {
                        return false;
                    }
                }


                if (HttpContext.Current.Session["S_DeliveryPoint"].ToString() != "")
                {
                    _deliveryPoint = HttpContext.Current.Session["S_DeliveryPoint"].ToString();
                   
                    if (_deliveryPoint == "S_HomeDelivery")
                    {
                        _homeViews.RowFilter = "pagecat in (0,1)  and sessionflag in =0";

                        foreach (DataRowView rowView in _homeViews)
                        {
                            DataRow row = rowView.Row;
                            string _sessionname = row["variableName"].ToString();
                            if (HttpContext.Current.Session[_sessionname].ToString() == "")
                            {
                                return false;
                            }
                        }


                    }
                }




            }




            return true;
        }

        public static void ClearSession(int page, string model)
        {
            if (model == "plate")
            {
                var allSession = HttpContext.Current.Session.Contents.Keys;
                DataTable _dt = Global.SessionTable;
                _dt.TableName = "tblS";//all sessionname table
                DataView dv = new DataView(_dt);
                dv.RowFilter = "pagerank >" + page + " and sessionflag in (0,1)";

            foreach (DataRowView drv in dv)
            {
                DataRow row = drv.Row;
                string _currentrowSession = row["variableName"].ToString();
                HttpContext.Current.Session[_currentrowSession] = "";

                }
            }

            if (model == "sticker")
            {
                var allSession = HttpContext.Current.Session.Contents.Keys;
                DataTable _dt = Global.stickerSessionTable;
                _dt.TableName = "tblS";//all sessionname table
                DataView dv = new DataView(_dt);
                dv.RowFilter = "pagerank >" + page + " and sessionflag in (0,1)";

                foreach (DataRowView drv in dv)
                {
                    DataRow row = drv.Row;
                    string _currentrowSession = row["variableName"].ToString();
                    HttpContext.Current.Session[_currentrowSession] = "";

                }
            }



            #region oldcode
            //DataTable _currentValidSession = new DataTable();
            //_currentValidSession= dv.ToTable(false, "variableName");
            //_currentValidSession.TableName = "tblValid";
            //DataTable _currentSession = new DataTable();
            //_currentSession.TableName = "tblSS";//current sessions 
            //_currentSession.Columns.Add("SessionName");
            //foreach (var crntSession in HttpContext.Current.Session.Keys)
            //{
            //    DataRow row = _currentSession.NewRow();
            //    row["SessionName"] = crntSession;
            //    _currentSession.Rows.Add(row);
            //}

            ////DataSet _comapretables = new DataSet();
            ////_comapretables.Tables.Add(_dt);
            ////_comapretables.Tables.Add(_currentSession);

            ////DataColumn parentColumn = _comapretables.Tables["tblSS"].Columns["SessionName"];
            ////DataColumn childColumn = _comapretables.Tables["tblS"].Columns["variableName"];
            ////DataRelation rel;
            ////rel = new DataRelation("ValidSession", parentColumn, childColumn);
            ////_comapretables.Relations.Add(rel);

            ////DataRow[] _invalidSess = _comapretables.Tables["tblSS"].getChildrows(rel);

            ////var _filtrow = from a in _currentValidSession.Rows.Cast<DataRow>()
            ////               where _currentSession.Rows.Cast<DataRow>().Any(
            ////                   r => object.Equals(r["SessionName"], a["variableName"]))
            ////               select a;
            //List<string> NoContactCus = new List<string>();
            //foreach (DataRow Customer in _currentSession.Rows)
            //{
            //    DataRow[] contacts = _currentValidSession.Select("variableName='"+ Customer["SessionName"].ToString()+"'");
            //    if (contacts.Count() == 0)
            //        NoContactCus.Add(Customer["SessionName"].ToString());
            //}

            //if (NoContactCus.Count > 0)
            //{
            //    foreach (string _sessvalue in NoContactCus)
            //    {
            //        HttpContext.Current.Session.Remove(_sessvalue);

            //    }


            //}
            #endregion
        }

        public static void reInitSession()
        {
            DataTable _dt = Global.SessionTable;
            foreach (DataRow row in _dt.Rows)
            {
                string _currentrowSession = row["variableName"].ToString();
                HttpContext.Current.Session[_currentrowSession] = "";
            }
            DataTable _dtS = Global.stickerSessionTable;
            foreach (DataRow row in _dtS.Rows)
            {
                string _currentrowSession = row["variableName"].ToString();
                HttpContext.Current.Session[_currentrowSession] = "";
            }
        }


    }
}