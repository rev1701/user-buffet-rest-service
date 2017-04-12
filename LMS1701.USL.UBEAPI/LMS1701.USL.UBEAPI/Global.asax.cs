using LMS1701.USL.UBEAPI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Mvc;

namespace LMS1701.USL.UBEAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var rhm = new RequestHeaderMapping("Accept", "text/html", StringComparison.InvariantCultureIgnoreCase, true, "application/json");
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(rhm);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfig.Configure();
            AreaRegistration.RegisterAllAreas();
            NLogConfig.nLogger().Log(new NLog.LogEventInfo(NLog.LogLevel.Info, "this", "webservice Started"));

        }
    }
}
