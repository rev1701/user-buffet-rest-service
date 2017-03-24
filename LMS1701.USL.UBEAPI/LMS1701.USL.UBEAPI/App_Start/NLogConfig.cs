using System;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS1701.USL.UBEAPI.App_Start
{
    public static class NLogConfig
    {
        public static Logger nLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }
    }
}