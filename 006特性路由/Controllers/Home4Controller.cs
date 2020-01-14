using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _006Route.Controllers
{
    [RoutePrefix("Home4")]
    [Route("{action=index}/{id?}")]
    //通过以上两个特性即可按照我们通常管理匹配到一下所有的action
    //http://localhost:64315/home4 默认是匹配到Index（）
    //路由参数｛id}为可选参数
    public class Home4Controller : Controller
    {
        public ActionResult Index()
        {
            return Content("Index()");
        }
        public ActionResult About(int id)
        {
            return Content("About()" + id.ToString());
        }
        public ActionResult Delete()
        {
            return Content("Delete()");
        }
    }
}