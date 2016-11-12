using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Giffy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Account",
                url: "account/{act}",
                defaults: new { controller = "Home", action = "Index", act = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Associate",
                url: "associate",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Facebook Auth Complete",
                url: "authcomplete",
                defaults: new { controller = "Home", action = "AuthComplete" }
            );

            routes.MapRoute(
                name: "Google Auth Complete",
                url: "signin-google",
                defaults: new { controller = "Home", action = "AuthComplete" }
            );

            routes.MapRoute(
                name: "Management",
                 url: "management/{act}",
                 defaults: new { controller = "Home", action = "Index", act = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Pages",
                url: "{attr1}/{attr2}/{attr3}/{attr4}",
                defaults: new { controller = "Home", action = "Index", attr1 = UrlParameter.Optional, attr2 = UrlParameter.Optional, attr3 = UrlParameter.Optional, attr4 = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
