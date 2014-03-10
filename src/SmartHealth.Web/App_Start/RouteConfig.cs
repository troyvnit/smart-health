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
            routes.IgnoreRoute("{resource}.txt"); 

            routes.MapRoute(
                "Admin Controller", // Route name
                "BackOffice/{controller}", // URL with parameters
                new { action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Home Controller", // Route name
                "{lang}/{action}", // URL with parameters
                new { controller = "Home" }, // Parameter defaults
                new { lang = "[a-z]{2}-[a-z]{2}" }
            );

            //routes.MapRoute(
            //    "Home Controller With Id", // Route name
            //    "{lang}/{action}/{id}", // URL with parameters
            //    new { controller = "Home", id = UrlParameter.Optional }, // Parameter defaults
            //    new { lang = "[a-z]{2}-[a-z]{2}", id = "([0-9]+)$" }
            //);

            routes.MapRoute(
                "Localization", // Route name
                "{lang}/{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new { lang = "[a-z]{2}-[a-z]{2}" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, lang = "vi-vn" },
                namespaces: new[] { "Admin.Controllers" }
            );
        }
    }
}