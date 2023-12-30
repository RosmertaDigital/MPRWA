using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMHSRPv2.sticker
{
    public partial class ucStepBar : System.Web.UI.UserControl
    {
        public string BackPage
        {
            set { ltBack.Text = ltBack.Text.Replace("#page", value); }
        }

        public string PageStep
        {
            set { ltBack.Text = ltBack.Text.Replace("#dataact", "page" + value.Trim()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}