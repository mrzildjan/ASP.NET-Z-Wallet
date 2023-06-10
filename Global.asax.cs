using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Z_Wallet
{
    public class Global : HttpApplication
    {
        public static TimeZoneInfo CustomTimeZone { get; set; }

        void Application_Start(object sender, EventArgs e)
        {
            // Set custom time zone
            string timeZoneId = ConfigurationManager.AppSettings["ApplicationTimeZone"];
            try
            {
                if (!string.IsNullOrEmpty(timeZoneId))
                {
                    CustomTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                }
                else
                {
                    CustomTimeZone = TimeZoneInfo.Utc; // default to UTC if not specified
                }
            }
            catch (TimeZoneNotFoundException)
            {
                // The specified time zone could not be found, so use UTC instead.
                CustomTimeZone = TimeZoneInfo.Utc;
            }

            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
