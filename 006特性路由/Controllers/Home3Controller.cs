using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _006Route.Controllers
{
    [Route("home3/{action}")]
    public class Home3Controller : Controller
    {
        [Route("Index")]
        public ActionResult Index()//http://localhost:64315/index  //只能这一个，在action上写特性路由则完全的覆盖掉控制器上写的特性路由
                                   //http://localhost:64315/home3/index //无效
        {
            return Content("Index()");
        }
        public ActionResult About()//http://localhost:64315/home3/about
        {
            return Content("About()");
        }
        public ActionResult Delete()//http://localhost:64315/home/delete
        {
            return Content("Delete()");
        }


    }
}