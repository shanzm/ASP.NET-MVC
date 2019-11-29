using _04FilterTest1.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _04FilterTest1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //添加自定义的Filter
            GlobalFilters.Filters.Add(new CheckAuthorFilter());


           // test--使用VS，commit 某一个文件
        }
    }
}
