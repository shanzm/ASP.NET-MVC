using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _007传统路由
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //网站启动的时候，会从这里Application_Start开始
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);//注册路由
            //这个注册路由的方法就是我们在App_Start/RounteConfig.cs中定义的方法
            //网站第一次启动/重启，执行且只执行一次，只要是初始化的东西都可以放在这里
            //整个流程：网站启动-->注册路由-->请求来了-->路由解析-->controller/action
        }
    }
}
