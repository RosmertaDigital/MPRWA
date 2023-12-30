using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMHSRPv2.Models
{
    public class ConnString
    {
        public static string ConString()
        {
            String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            //connectionString = "Database=BookMyHSRP;Server=USS1798;UID=SA;PWD=Em5S!7@Z; pooling=true; Max Pool Size=200;Connect Timeout=0";
            //connectionString = "Database=BookMyHSRP;Server=103.197.122.32,56662;UID=hsrp1561;PWD=d!!W5%v}; pooling=true; Max Pool Size=200;Connect Timeout=0";
            //connectionString =   "Database=BookMyHSRP;Server=192.168.196.4;UID=hsrp1561;PWD=d!!W5%v}; pooling=true; Max Pool Size=200;Connect Timeout=0";

            return connectionString;
        }

    }
}