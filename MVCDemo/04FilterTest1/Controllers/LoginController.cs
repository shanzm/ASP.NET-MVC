using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}