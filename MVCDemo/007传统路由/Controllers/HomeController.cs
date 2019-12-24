using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _007传统路由.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()//http://localhost:65034  默认URL
        {
            return Content("OK");
        }
        public ActionResult About(int id)//http://localhost:65034/home/about/2   显示：2  
                                         //注意等价于我们使用querystring传参，http://localhost:65034/home/about?Id=2
                                         //只不过我们设置了默认格式，所以若是传递id值，就不用使用querystring传参
        {
            return Content(id.ToString());
        }

        public ActionResult CreateURL()
        {
            return View();
        }
    }
}