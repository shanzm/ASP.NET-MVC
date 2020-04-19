using MVCDemo.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemo.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index(IndexModel model)//Action
        {
            model.Result = model.Num1 + model.Num2;
            ViewData["name"] = "sahnzm-testViewData";
            //ViewBag.name = "shanzm2";

            return View(model);
        }

        public  ActionResult JsonTest( )
        {
            Person p = new Person() { Id = 001, Name = "shanzm", Email = "shanzm@qq.com", ClassId = 001, Age = 25 };

            return Json(p, JsonRequestBehavior.AllowGet);
        }
    }
}