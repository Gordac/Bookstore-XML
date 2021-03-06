﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bookstore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Book", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "CreateNew",
                url: "{controller}/{action}",
                defaults: new { controller = "Book", action = "CreateNew" }
                );

            routes.MapRoute(
                name: "Add",
                url: "{controller}/{action}",
                defaults: new { controller = "Book", action = "Add" }
                );

            routes.MapRoute(
                name: "Edit",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Book", action = "Edit", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Update",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Book", action = "Update", id = UrlParameter.Optional}
                );
        }
    }
}
