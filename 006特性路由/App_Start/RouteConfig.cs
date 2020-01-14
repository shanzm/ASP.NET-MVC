using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _006Route
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
            //以上是默                 认的传统路由的配置

            //URI 统一资源标识符，是标识了一个资源的字符串
            //URL 属于URI，URI是某种资源的标识符，而URL则为获取某种资源提供了信息

            //传统中如PHP，如ASP.NET 中的URL都是代表磁盘中的物理路径，比如哦我们之前写的一般处理程序，URL就是一个ashx文件的物理路径
            //而ASP.NET MVC 则把URL映射到指定的方法（Action）上，而不是磁盘的物理路径

            //ASP.NET MVC中路由的用途：
            //1.生成原始的URL
            //2.匹配传入的请求，并映射到指定的操作上

            //使用自定义的特性路由（ASP.NET MVC5中添加的特性路由）
            routes.MapMvcAttributeRoutes();
        }
    }
}
