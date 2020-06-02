using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _02DropDownList.Models;

namespace _02DropDownList.Controllers
{
    public class TwoLevelController : Controller
    {   
        public ActionResult Index()
        {
            ViewBag.Date = DateTime.Now.ToString();
            List<namelist> llNameList = buildNameList();
            ViewData["SEX"] = llNameList.Select(i => new SelectListItem { Value = i.sex, Text = i.sex });
            ViewData["NAME"] = llNameList.Select(i => new SelectListItem { Value = i.name, Text = i.name });
            return View();

        }

        public JsonResult GetName(string id)   //view界面选择sex的，输出对应的name
        {
            List<namelist> llNameList = new List<namelist>();
            llNameList = buildNameList();
            namelist ccnamelist = new namelist();
            var varNameList = llNameList.Where(i => i.sex == id).Select(i => i.name);
            return Json(varNameList.ToList(), JsonRequestBehavior.AllowGet);
        }

        public List<namelist> buildNameList()   //因未连接数据库，为demo效果，临时建立一个list
        {
            List<namelist> llnamelist = new List<namelist>();
            for (int _count = 0; _count < 5; _count++)
            {
                namelist ccnamelist = new namelist();
                ccnamelist.index = _count;
                ccnamelist.sex = "male";
                ccnamelist.name = "Tommy " + _count.ToString();
                llnamelist.Add(ccnamelist);
            }
            for (int _count = 0; _count < 4; _count++)
            {
                namelist ccnamelist = new namelist();
                ccnamelist.index = _count;
                ccnamelist.sex = "female";
                ccnamelist.name = "Kitty " + _count.ToString();
                llnamelist.Add(ccnamelist);
            }
            return llnamelist;
        }
    }
}