using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTestWebAPI.Controllers
{
    public class PersonMVCController : Controller
    {
        // GET: PersonMVC
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show()
        {
            return Json(new { id = 1, name = "shanzm" },JsonRequestBehavior.AllowGet);
        }
    }
}