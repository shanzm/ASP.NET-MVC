﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;

namespace _007传统路由
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.Add("Chrome", new ChromeRoute());//在这里添加我们自定义的路由过滤

            //注意传统路由和特性路由是可以混合使用的，建议把特性路由放在最前，因为特性路由相对于传统路由更加具体，传统路由更加宽泛自由
            routes.MapMvcAttributeRoutes();

            //设置 http://localhost:65034/welcome 指向Home中的Index（）
            #region 书写格式
            //routes.MapRoute(
            //    name: "static",
            //    url: "welcome", 
            //    defaults: new { controller = "Home", action = "Index" }
            //    );
            #endregion
            //简洁写的法
            //其实上面的写法就是把函数的形参名带着，在C#中的任何函数都是可以这样写的
            routes.MapRoute("static", "welcome", new { controller = "Home", action = "Index" });

            //路由的格式：控制器名/Action名/id值
            //注意我们这里设置的路由规则中controller和action和id都是加了个大括号，本质就是占位符，所以任何的controller和action都可以和这个路由规则匹配
            //而上面的路由规则中URL为welcome，匹配的就是welcome这个单词
            //设置控制器名默认为：Home，action默认为：Index，id为可选
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            #region 路由顺序
            //注意每一个routes.MapRoute(）的书写顺序是很讲究的，路由会按照先后顺序与之传入的URL进行匹配
            //比如说，我颠倒上面的static路由和Default路由
            //则当我输入“http://localhost:65034/welcome”就会出错，因为它也能与Default路由匹配，
            //但是只是符合Default路由的模式，并不能指向正确的action
            //按照Default路由匹配，它实际就是：http://localhost:65034/welcome/index  而实际是没有这个URL的
            //简而言之，路由匹配是自上而下的，匹配到第一个满足的即技术
            #endregion

            #region 路由约束
            //路由约束使用正则表达式
            //在特性路由中我们使用类似于{ id:int}的格式约束路由的某一个参数的类型
            //也可以使用正则表达式｛year:regex{^\d{4}$}
            //传统路由使用一个单独的参数,且传统路由的约束在使用正则表达式的时候，可以省略"^""$"的位置匹配

            //这里注意：直接调试输入路由：localhost://65043/2020/08/11 会报错，因为该url首先匹配的是上面的control/action/id，所以会报错，因为我们就没有名称为2020的控制器
            //所有需要调试的话，可以先把上面的Default路由注释掉
            //注意：这里MapRoute（）使用了四个参数，ctrl+shift+space查看智能提示理解了
            //第一个参数是路由名，第二个参数是路由规则，第三个参数就是默认路由，第四个参数就是路由规则的约束
            #endregion
            routes.MapRoute("blogs", "{year}/{month}/{day}", new { controller = "blog", action = "index" }, new { year = @"\d{4}", month = @"\d{2}", day = @"\d{2}" });


            routes.MapRoute("timeRoute", "{controller}/{action}_{year}_{month}_{day}", new { controller = "home", action = "time" }, new { year = @"\d{4}" });

            ///自定义路由约束
            ///约束一个路由只响应Get请求
            routes.MapRoute("onlyGet", "{controller}/{action}", null, new { httpMethod = new HttpMethodConstraint("GET") });


            //传统路由中约束类型，等价于特性路由中｛id：int｝
            //添加命名空间using System.Web.Mvc.Routing.Constraints;
            routes.MapRoute("onlyint", "{controller}/{action}/{id}", null, new { id = new IntRouteConstraint() });


            #region 选择传统路由还是特性路由
            //1.首先二者可以混用
            //2.传统路由的优点：可以集中配置所有的路由，传统路由比特性路由灵活
            //3.特性路由的优点：路由和操作代码保存在一起
            #endregion


            //routes.MapRoute(）的第一个参数是路由规则的名字
            //这个名字有什么用？我们可以在View页面使用Html辅助函数：@Html.RouteLink()来生成URL
            //也可以使用@Url.RouteUrl()来生成URL，具体见本项目中的CreateURL.cshtml
        }
    }

    
    /// <summary>
    /// 自定义一个路由配置类，限制用户访问可以使用的浏览器
    /// </summary>
    public class ChromeRoute : RouteBase
    {
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (httpContext.Request.UserAgent.Contains("Mozilla/5.0 (Windows NT 6.1; Win64; x64)"))
            //这串字符是从Chrome中Network中的user-agent，表示是使用Chrome浏览器访问
            //注意不同的电脑系统，这个字符串是不一样的
            //比如说win10系统中这里就是：Mozilla/5.0 (Windows NT 10.0; Win64; x64)
            {
                return null;//若是Chrome浏览器则正常访问
            }
            else
            {
                RouteData routeData = new RouteData(this, new MvcRouteHandler());
                routeData.Values.Add("controller", "Home");
                routeData.Values.Add("action", "refuse");//这里即跳转到：home/refuse页面
                return routeData;
            }
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
