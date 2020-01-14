using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemo.Controllers
{
    public class LayoutTestController : Controller
    {
        // GET: LayoutTest
        public ActionResult Index()
        {
            return View();
        }
    }
}