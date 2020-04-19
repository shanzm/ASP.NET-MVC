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
        public ActionResult Index(IndexModel model)
        {
            model.Result = model.Num1 + model.Num2;
            ViewData["name"] = "sahnzm-testViewData";
            //ViewBag.name = "shanzm2";

            return View(model);
        }

        //[HttpPost]
        //public ActionResult Index(int num1, int num2)//直接使用input的name属性值作为参数名接收表单提交的数据
        //{
        //    ViewData["name"] = "sahnzm-testViewData";
        //    IndexModel model = new IndexModel() { Num1 = num1, Num2 = num2, Result = num1 + num2 };
        //    return View("Index", model);
        //}
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    ViewData["name"] = "sahnzm-testViewData";
        //    IndexModel model = new IndexModel() { Num1 = 0, Num2 = 0, Result = 0 };
        //    return View("Index", model);
        //}

        public ActionResult JsonTest()
        {
            Person p = new Person() { Id = 001, Name = "shanzm", Email = "shanzm@qq.com", ClassId = 001, Age = 25 };

            return Json(p, JsonRequestBehavior.AllowGet);
        }
    }
}