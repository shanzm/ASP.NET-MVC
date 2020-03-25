using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _012_任务调度框架_在MVC中使用
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //使用Quartz.NET
            Task.Run(() => TimedTask.ExcuteTimedTask(DateTime.Now.ToString(), "/5 * * ? * *"));
        }
    }
}
