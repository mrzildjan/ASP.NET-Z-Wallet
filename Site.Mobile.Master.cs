using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Z_Wallet
{
    public partial class Site_Mobile : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string switchViewUrl = "~/__FriendlyUrls_SwitchView?ReturnUrl=%2f";
            Response.Redirect(switchViewUrl);
        }
    }
}