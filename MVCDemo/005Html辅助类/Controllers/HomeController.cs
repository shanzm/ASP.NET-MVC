using _005Html辅助类.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _005Html辅助类.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IsValid(Person model)
        {
            //注意使用Html.ValidationSummary()对提交的数据进行检验，
            //在action中使用ModelState.IsValid 来判断是否符合要求
            if (!ModelState.IsValid)
            {
                return View("IsValid");//一定要返回表单原始页面（数据提交的页面）
            }
            return Content(model.UserName);
        }
        [HttpGet]
        public ActionResult IsValid()
        {
            return View();
        }

    }
}