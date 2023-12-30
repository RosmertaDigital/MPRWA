using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Data;
using BMHSRPv2.plate;

namespace BMHSRPv2
{
    public class Global : HttpApplication
    {
        public static DataTable SessionTable = new DataTable();
        public static DataTable stickerSessionTable = new DataTable();
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            GlobalConfiguration.Configure(WebApiConfig.Register);

            string _sessionlist = System.Configuration.ConfigurationManager.AppSettings["SessionVaribleList"].ToString();
            string _stickerSessionlist = System.Configuration.ConfigurationManager.AppSettings["StickerSessionVaribleList"].ToString();
            List<string> _sessionList = new List<string>();
            string[] _sessionlistAll = _sessionlist.Split('|');
            //DataTable SessionTable = new DataTable();
            SessionTable.Columns.Add("page");
            SessionTable.Columns.Add("variableName");
            SessionTable.Columns.Add("pagerank",typeof(Int32));
            SessionTable.Columns.Add("pagecat", typeof(Int32));
            SessionTable.Columns.Add("sessionflag", typeof(Int32));//to determine whether session value will be tested or not
            foreach (string _sitem in _sessionlistAll)
            {
                string[] _splitedSessionList = _sitem.Split(';');
                DataRow row = SessionTable.NewRow();
                row["page"] = _splitedSessionList[0];
                row["variableName"] = _splitedSessionList[1];
                row["pagerank"] = _splitedSessionList[2];
                row["pagecat"] = _splitedSessionList[3];
                row["sessionflag"]= _splitedSessionList[4];
                SessionTable.Rows.Add(row);
            }

            //sticker session list
            List<string> _StickersessionList = new List<string>();
            string[] _StickersessionlistAll = _stickerSessionlist.Split('|');
            //DataTable SessionTable = new DataTable();
            stickerSessionTable.Columns.Add("page");
            stickerSessionTable.Columns.Add("variableName");
            stickerSessionTable.Columns.Add("pagerank", typeof(Int32));
            stickerSessionTable.Columns.Add("pagecat", typeof(Int32));
            stickerSessionTable.Columns.Add("sessionflag", typeof(Int32));//to determine whether session value will be tested or not
            foreach (string _sitems in _StickersessionlistAll)
            {
                string[] _splitedSessionList = _sitems.Split(';');
                DataRow rows = stickerSessionTable.NewRow();
                rows["page"] = _splitedSessionList[0];
                rows["variableName"] = _splitedSessionList[1];
                rows["pagerank"] = _splitedSessionList[2];
                rows["pagecat"] = _splitedSessionList[3];
                rows["sessionflag"] = _splitedSessionList[4];
                stickerSessionTable.Rows.Add(rows);
            }


            //initializing all session 

            //foreach (DataRow row in SessionTable.Rows)
            //{

            //    string _currentrowSession = row["variableName"].ToString();
            //    string defaultValue = "0";
            //    HttpContext.Current.Session[_currentrowSession] = defaultValue;

            //}
            //CheckSession.initSession();
            
        }

           


        
        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }


        protected void Session_Start(object sender, EventArgs e)
        {
            DataTable _dt = Global.SessionTable;
            foreach (DataRow row in _dt.Rows)
            {
                string _currentrowSession =  row["variableName"].ToString() ;
                HttpContext.Current.Session[_currentrowSession] = "";
            }

            //sticker part
            DataTable _dtS = Global.stickerSessionTable;
            foreach (DataRow row in _dtS.Rows)
            {
                string _currentrowSession = row["variableName"].ToString();
                HttpContext.Current.Session[_currentrowSession] = "";
            }

        }
    }
}