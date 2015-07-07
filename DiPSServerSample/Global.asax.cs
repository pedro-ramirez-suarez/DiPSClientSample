using DiPSServerSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DiPSServerSample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// for a web application it's recommended that you subscribe to events only once not on every request
        /// </summary>
        public static bool SubscriptionsMade { get; set; }

        protected void Application_BeginRequest(Object source, EventArgs e)
        {
            Broker.Initialize("ws://localhost:8888/dips");
            // for a web application it's recommended that you subscribe to events only once not on every request
            if (!SubscriptionsMade)
            {
                SubscriptionsMade = true;
                //subscribe to the user logged in event
                Broker.Subscribe("userlogin", (u) =>
                {
                    //log the user
                    string usr = u.UserName.ToString();
                    ChatHelper.UserLogin(usr);
                    //send to subscribers the new list of users
                    Broker.PublishMessage("reloaduserlist", ChatHelper.OnlineUsers);
                });
            }
        }
    }
}
