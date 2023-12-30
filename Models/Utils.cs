using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace BMHSRPv2.Models
{
    public class Utils
    {

        public string strProvider = ConnString.ConString();
        public string sqlText;
        public int CommandTimeOut = 0;
        public SqlConnection objConnection;

        public static string SYS_GBL_QUOTE(string inputstring)
        {
            inputstring = inputstring.Replace("'", "");
            return inputstring;
        }

        public static string SetFolder(string strPath)
        {
            string folder = string.Empty;
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
                folder = strPath;
            }
            else
            {
                folder = strPath;
            }
            return folder;
        }

        public SqlDataReader GetReader()
        {
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 0;
            MakeConnection();
            OpenConnection();
            cmd = GetCommand();
            cmd.CommandTimeout = 0;
            dr = cmd.ExecuteReader();
            return dr;
        }
        SqlCommand GetCommand()
        {
            return new SqlCommand(sqlText, objConnection);
        }
        public void MakeConnection()
        {

            objConnection = new SqlConnection(strProvider);
            // objConnection.= 200000;
        }
        public void OpenConnection()
        {


            objConnection.Open();
        }
        public void CloseConnection()
        {
            objConnection.Close();
        }
        public static string ConvertDateTimeToString(DateTime oDatatime)
        {
            string str = "";
            str = oDatatime.Month.ToString() + "/" + oDatatime.Day.ToString() + "/" + oDatatime.Year.ToString() + " " + oDatatime.Hour.ToString() + ":" + oDatatime.Minute.ToString() + ":" + oDatatime.Second.ToString();
            return str;
        }
        public static int getScalarCount(string SQLString, string CnnString)
        {
            int ReturnValue = 0;
            using (SqlConnection conn = new SqlConnection(CnnString))
            {
                SqlCommand cmd = new SqlCommand(SQLString, conn);

                conn.Open();
                ReturnValue = (int)cmd.ExecuteScalar();
                conn.Close();
            }
            return ReturnValue;
        }

        public static string getScalarValue(string SQLString, string CnnString)
        {

            string ReturnValue = string.Empty;
            using (SqlConnection conn = new SqlConnection(CnnString))
            {
                SqlCommand cmd = new SqlCommand(SQLString, conn);
                conn.Open();
                ReturnValue = cmd.ExecuteScalar().ToString(); ;
            }
            return ReturnValue;
        }
        //============================ComponentArt Menu===NEW-------------------------------//
        //===============================Menu Component art
       
        public static int ExecNonQuery(string SQLString, string CnnString)
        {
            try
            {
                int count = 0;
                using (SqlConnection connection = new SqlConnection(CnnString))
                {
                    SqlCommand command = new SqlCommand(SQLString, connection);
                    command.Connection.Open();
                    count = command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                return count;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    return ex.Number;
                }
                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;

            }
        }
        public static void PopulateDropDownList(DropDownList DropDownName, string SQLString, string CnnString, string DefaultValue)
        {
            try
            {
                Utils dbLink = new Utils();
                dbLink.strProvider = CnnString;
                dbLink.CommandTimeOut = 0;
                dbLink.sqlText = SQLString.ToString();
                SqlDataReader PReader = dbLink.GetReader();
                DropDownName.DataSource = PReader;
                DropDownName.DataBind();
                ListItem li = new ListItem(DefaultValue, DefaultValue);
                DropDownName.Items.Insert(0, li);
                PReader.Close();
                dbLink.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void DealerNameDropDownList(DropDownList DropDownName, string SQLString, string CnnString, string DefaultValue)
        {
            try
            {
                Utils dbLink = new Utils();
                dbLink.strProvider = CnnString;
                dbLink.CommandTimeOut = 0;
                dbLink.sqlText = SQLString.ToString();
                SqlDataReader PReader = dbLink.GetReader();
                DropDownName.DataSource = PReader;
                DropDownName.DataBind();
                ListItem li = new ListItem(DefaultValue, DefaultValue);
                DropDownName.Items.Insert(0, li);
                PReader.Close();
                dbLink.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void PopulateListBox(ListBox ListBoxName, string SQLString, string CnnString, string DefaultValue)
        {
            try
            {
                Utils dbLink = new Utils();
                dbLink.strProvider = CnnString;
                dbLink.CommandTimeOut = 0;
                dbLink.sqlText = SQLString.ToString();
                SqlDataReader PReader = dbLink.GetReader();
                ListBoxName.DataSource = PReader;
                ListBoxName.DataBind();
                ListBoxName.Items.Add(DefaultValue);
                PReader.Close();
                dbLink.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet getDataSet(string SQLString, string CnnString)
        {
            SqlConnection Conn = new SqlConnection(CnnString);
            SqlDataAdapter DA = new SqlDataAdapter(SQLString, Conn);
            DA.SelectCommand.CommandTimeout = 0;
            Conn.Open();
            DataSet ReturnDs = new DataSet();
            DA.Fill(ReturnDs, "Table1");
            Conn.Close();
            return ReturnDs;
        }
        public static string getDataSingleValue(string SQLString, string CnnString, string colname)
        {
            string SingleValue = "";
            try
            {
                SqlConnection conn = new SqlConnection(CnnString);
                SqlCommand cmd = new SqlCommand(SQLString, conn);
                SqlDataAdapter returnVal = new SqlDataAdapter(SQLString, conn);
                conn.Close();
                conn.Open();
                DataTable dt = new DataTable("CharacterInfo");
                returnVal.Fill(dt);
                conn.Close();
                if (dt.Rows.Count > 0)
                {
                    SingleValue = dt.Rows[0][colname].ToString();
                }
                if (SingleValue == "")
                {
                    SingleValue = "0";
                }
                returnVal.Dispose();
                return SingleValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetDataTable(string SQLString, string CnnString)
        {
            Utils dbLink = new Utils();
            dbLink.strProvider = CnnString.ToString();
            dbLink.CommandTimeOut = 0;
            dbLink.sqlText = SQLString.ToString();
            SqlDataReader dr = dbLink.GetReader();
            DataTable tb = new DataTable();
            tb.Load(dr);
            dr.Close();
            dbLink.CloseConnection();
            return tb;

        }



        //for compression
        public static bool IsGZipSupported()
        {

            string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];

            if (!string.IsNullOrEmpty(AcceptEncoding) &&

                 AcceptEncoding.Contains("gzip") || AcceptEncoding.Contains("deflate"))

                return true;

            return false;

        }
        public static void GZipEncodePage()
        {

            if (IsGZipSupported())
            {

                HttpResponse Response = HttpContext.Current.Response;



                string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];

                if (AcceptEncoding.Contains("gzip"))
                {

                    Response.Filter = new System.IO.Compression.GZipStream(Response.Filter,

                                              System.IO.Compression.CompressionMode.Compress);

                    Response.AppendHeader("Content-Encoding", "gzip");

                }

                else
                {
                    Response.Filter = new System.IO.Compression.DeflateStream(Response.Filter, System.IO.Compression.CompressionMode.Compress);
                    Response.AppendHeader("Content-Encoding", "deflate");
                }

            }

        }

        public static void user_log(string userid, string formname, string ClientLocalIP, string eventname, string MACAddress, string BrowserName, string ClientOSName, string CnnString)
        {
            string sdate = DateTime.Now.ToString();
            string sql = "INSERT INTO [USERLOG]([UserID],[formname],[eventname],[clientip],[MACAddress],[BrowserName],[ClientOSName]) VALUES ('" + userid + "','" + formname + "','" + eventname + "','" + ClientLocalIP + "','" + MACAddress + "','" + BrowserName + "','" + ClientOSName + "')";
            Utils.ExecNonQuery(sql, CnnString);

        }


        public static void Exception_log(string userid, string Formname, string computername, string exeption, string CnnString)
        {
            string strDate = DateTime.Now.ToString();
            string strQuery = "insert into Exceptionlog(LoginId,FormName,UpdateDateTime,ComputerIP,ExceptionName)values('" + userid + "','" + Formname + "','" + strDate + "','" + computername + "','" + exeption + "')";
            Utils.ExecNonQuery(strQuery, CnnString);
        }
        public static bool IsInteger(string theValue)
        {
            try
            {
                Convert.ToInt32(theValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //>>>> Export to Excel tanuj 24/12/2011
        public static void ExportToSpreadsheet(DataTable table, string name)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            foreach (DataColumn column in table.Columns)
            {
                context.Response.Write(column.ColumnName + ",");
            }
            context.Response.Write(Environment.NewLine);
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    context.Response.Write(row[i].ToString().Replace(",", string.Empty) + ",");
                }
                context.Response.Write(Environment.NewLine);
            }
            context.Response.ContentType = "text/csv";
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + name + ".csv");
            context.Response.End();
        }

     

        public DataTable GetRecords(string query, SqlParameter[] sqlParameter, SqlConnection con)
        {
            SqlCommand sqlCommand = new SqlCommand(query, con);
            DataTable records = null;
            try
            {
                con.Open();
                records = new DataTable();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if (sqlParameter != null)
                    sqlCommand.Parameters.AddRange(sqlParameter);
                records.Load(sqlCommand.ExecuteReader());
                con.Close();
            }
            catch (SqlException ex)
            {
                con.Close();
                throw ex;
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return records;
        }

        public static void DealerRegistration(string htmlString,string Subject, string CustomerEmail,string    AttachmentFile)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient("mail.rosmertahsrp.com");
                message.From = new MailAddress("no-reply@rosmertahsrp.com");
                message.To.Add(new MailAddress(CustomerEmail));
                message.To.Add(new MailAddress("online@bookmyhsrp.com"));
                message.Bcc.Add(new MailAddress("amar.khokar@gmail.com"));
                message.Subject = Subject;

                if (AttachmentFile != "")
                {
                    Attachment data = new Attachment(HttpContext.Current.Server.MapPath(AttachmentFile));
                    message.Attachments.Add(data);
                }
                

                message.IsBodyHtml = true;
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential("no-reply@rosmertahsrp.com", "RtL_1234");
                smtp.Send(message);

            }
            catch (Exception ex)
            {

            }
        }


        public static void OrderCancellationEmail(string htmlString, string OrderNo, string CustomerEmail)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient("mail.rosmertahsrp.com");
                message.From = new MailAddress("no-reply@rosmertahsrp.com");
                message.To.Add(new MailAddress("online@bookmyhsrp.com"));
                message.Bcc.Add(new MailAddress("amar.khokar@gmail.com"));
                message.Subject = "Order : " + OrderNo + " Cancellation E-mail";
                message.IsBodyHtml = true;
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential("no-reply@rosmertahsrp.com", "RtL_1234");
                smtp.Send(message);

            }
            catch (Exception ex)
            {

            }
        }


        public static String SMSSend12(string mobile, string SMSText)
        {

            string sendURL = string.Empty;
            string result = string.Empty;
            String userid = "2000187338";
            String passwd = "QBvXe79FK";
            sendURL = "http://enterprise.smsgupshup.com/GatewayAPI/rest?method=sendMessage&send_to=" + mobile + "&msg=" + SMSText + "&userid=" + userid + "&password=" + passwd + "&v=1.1&msg_type=TEXT&auth_scheme=PLAIN&mask=DLHSRP";

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(sendURL);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            result = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();
            System.Threading.Thread.Sleep(350);

            return result;

        }



        public static String SMSSend(string mobile, string SMSText)
        {
            //txtAuthKey.Text="343817AaX3yb5BY4rI5f967427P1";
            //txtSenderId.Text ="BMHSRP;    // "dlhsrp";
            string result;
            //Your authentication key
            string authKey = "343817AaX3yb5BY4rI5f967427P1";
            //Multiple mobiles numbers separated by comma
            string mobileNumber = mobile;
            //Sender ID,While using route4 sender id should be 6 characters long.
            string senderId = "BMHSRP";
            //Your message to send, Add URL encoding here.
            string message = HttpUtility.UrlEncode(SMSText);
            string country = "91";
            //Prepare you post parameters
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("authkey={0}", authKey);
            sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
            sbPostData.AppendFormat("&message={0}", message);
            sbPostData.AppendFormat("&sender={0}", senderId);
            sbPostData.AppendFormat("&country={0}", country);
            sbPostData.AppendFormat("&route={0}", "4");

            try
            {
                //Call Send SMS API
                string sendSMSUri = "https://api.msg91.com/api/sendhttp.php";
                //Create HTTPWebrequest
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                //Prepare and Add URL Encoded data
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(sbPostData.ToString());
                //Specify post method
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;
                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                //Get the response
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string responseString = reader.ReadToEnd();

                //Close the response
                reader.Close();
                response.Close();
                result = responseString;
            }
            catch (SystemException ex)
            {
                result = ex.Message.ToString();
            }

            return result;
        }
    

    }
}