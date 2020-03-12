using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestIService;

namespace TestMVC.Controllers
{
    public class HomeController : Controller
    {

        public IUserService userService { get; set; }//通过AutoFac自动为我们赋值一个IUserService接口实现对象
        // GET: Home
        public ActionResult Index()
        {
            bool b = userService.CheckLogin("shanzm", "123456");

            return Content(b.ToString());
        }

    }
}