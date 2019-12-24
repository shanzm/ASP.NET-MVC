using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _006Route.Controllers
{
    [RoutePrefix("home")]
    public class HomeController : Controller
    {
        // GET: Home
        [Route("index")]//http://localhost:64315/home/index 即可匹配到Index（）
        public ActionResult Index()
        {
            return Content("ok");
        }

        [Route ("about/{id=1}")]//http://localhost:64315/home/about 路由参数id默认是1，显示的是1
                                //http://localhost:64315/home/about/2  显示的是2
        public ActionResult About(int id)
        {
            return Content(id.ToString());
        }
    }
}