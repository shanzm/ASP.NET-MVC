using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestIService;

//PM> Install-Package AutoFac.Mvc5

namespace TestMVC.Controllers
{
    public class HomeController : Controller
    {

        public IUserService userService { get; set; }//通过AutoFac自动为我们赋值一个IUserService接口实现对象
        // GET: Home
        public ActionResult CheckLogin()
        {
            bool b = userService.CheckLogin("shanzm", "123456");
            return Content(b.ToString());//结果：页面显示true
        }

        public ActionResult UserAddNews()
        {
            string result = userService.UserAction("shanzm");
            return Content(result);//结果：页面显示：shanzm 添加新闻 ：Title：2020年3月16日-新冠病毒,Content：中国境内的新冠病毒被有效遏制
        }
    }
}