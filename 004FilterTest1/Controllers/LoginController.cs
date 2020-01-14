using _04FilterTest1.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace _04FilterTest1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "123456")
            {
                Session["username"] = username;
                return Content(username + "登录成功");
            }
            else
            {
                return Content("登录失败");
            }
        }


        ///注意我们调试的时候选择调试-开始执行（不调试）
        public ActionResult TestException()
        {
            //Exception ex = new Exception("自定义的一个异常");
            //throw ex;
            string a = null;
            a.ToString();

            return View();
        }


        ///注意这里使用的是自定义的过滤器特性
        ///因为我们没有在global.asax中添加，所以这个ResultFilter过滤器只是针对这个action
        [ResultFilter]
        public ActionResult TestResultFilter()
        {
            Thread.Sleep(2000);
            return View();
        }
    }
}