using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

///使用Log4Net日志框架
///NuGet:Install-Package log4net -Version 2.0.8
///自己在Web.config中添加配置
///每个Controller中声明一个ILog对象
///
///注意我们可以为log4net单独写一个配置文件log4net.config，而不是写在Web.config中
///然后在项目的Properties/AssemblyInfo.cs中添加一句：[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace _008Log4Net.Controllers
{
    public class HomeController : Controller
    {
        //定义一个静态制度字段，类型为ILog并初始化
        private static readonly ILog Log = LogManager.GetLogger(typeof(HomeController));

        // GET: Home
        public ActionResult Index()
        {
            try
            {
                Log.Debug("Hi I am log4net Debug Level");
                //在日志文件中：记录时间：2020-01-15 20:34:37,946 线程ID:[6] 日志级别：DEBUG 出错类：_008Log4Net.Controllers.HomeController property:[(null)] - 错误描述：Hi I am log4net Debug Level
                Log.Info("Hi I am log4net Info Level");
                Log.Warn("Hi I am log4net Warn Level");
                throw new NullReferenceException();
                return Content("测试OK！");

            }
            catch (Exception ex)
            {
                Log.Error("Hi I am log4net Error Level", ex);//ex即在日志中显示该异常的Message
                Log.Fatal("Hi I am log4net Fatal Level", ex);
                return Content("测试OK！");
            }
        

        }
    }
}