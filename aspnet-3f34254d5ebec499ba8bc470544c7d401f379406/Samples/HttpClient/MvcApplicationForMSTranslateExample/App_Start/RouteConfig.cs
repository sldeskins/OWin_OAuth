﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApplicationForMSTranslateExample
{
    public class RouteConfig
    {
        public static void RegisterRoutes ( RouteCollection routes )
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        
           // http://localhost:62634/aspx/Shared/Layout.Master
            routes.IgnoreRoute("{resource}.master/{*pathInfo}");
            routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");
            routes.IgnoreRoute("{resource}.html/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}