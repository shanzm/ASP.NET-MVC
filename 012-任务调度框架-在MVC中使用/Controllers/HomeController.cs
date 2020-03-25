using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace _012_任务调度框架_在MVC中使用.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //await Task.Run(() => TimedTask.ExcuteTimedTask(DateTime.Now.ToString(), "/5 * * ? * *"));
            return Content("hello world!");
        }
    }
}