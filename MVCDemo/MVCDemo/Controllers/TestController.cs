using MVCDemo.Models;
using System;
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
            ViewData["name"] = "shanzm1";
            ViewBag.name = "shanzm2";

            return View(model);
        }
    }
}