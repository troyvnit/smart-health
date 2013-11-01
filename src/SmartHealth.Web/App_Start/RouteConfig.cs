using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartHealth.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Admin.Controllers" }
            );

            routes.MapRoute(
                name: "Area_Default",
                url: "{area}/{controller}/{action}/{id}",
                defaults: new { controller = "Article", action = "Index", area = "Admin", id = UrlParameter.Optional },
                namespaces: new[] { "Admin.Controllers" }
            );
        }
    }
}