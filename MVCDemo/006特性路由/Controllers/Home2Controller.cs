using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _006Route.Controllers
{
    [RoutePrefix("home2")]
    [Route("{action=index}")]
    public class Home2Controller : Controller
    {
        // GET: Home2
        public ActionResult Index()//http://localhost:64315/home2
                                   //http://localhost:64315/home2/index 
                                   //http://localhost:64315/home2/Index //注意路由参数中的大小写可以忽略
        {
            return Content("ok:Index()");
        }
        public ActionResult About()//http://localhost:64315/home2/About
        {
            return Content("ok:About()");
        }
        [Route("del")]
        public ActionResult Delete()//http://localhost:64315/home2/del
        {
            return Content("ok:Delete()");
        }
    }
}