using Bll;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//该MVC项目是用于测试WebAPI项目的

namespace MVCTest.Controllers
{
    public class HomeController : Controller
    {
        PersonBll pBll = new PersonBll();
        // GET: Home
        public ActionResult Index()
        {
            PersonDto pDto = pBll.GetPerson(1);
            return Json(pDto,JsonRequestBehavior.AllowGet);
        }
    }
}