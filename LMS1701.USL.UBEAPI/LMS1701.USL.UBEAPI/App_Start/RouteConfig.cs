using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace LMS1701.USL.UBEAPI.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathinfo*}");

            routes.MapHttpRoute(
             name: "API Default",
              routeTemplate: "api/{controller}/{method}",
              defaults: new { method = RouteParameter.Optional }
            );
        }
    }
}