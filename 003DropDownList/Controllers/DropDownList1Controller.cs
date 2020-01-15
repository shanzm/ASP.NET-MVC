using _003DropDownList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _003DropDownList.Controllers
{
    public class DropDownList1Controller : Controller
    {
        // GET: DropDownList
        public ActionResult Index()
        {
            List<Person> list = new List<Person>();
            list.Add(new Person() { Id = 001, Name = "张三", Age = 12 });
            list.Add(new Person() { Id = 002, Name = "李四", Age = 13 });
            list.Add(new Person() { Id = 003, Name = "王五", Age = 14 });

            return View(list);
            //注意我们返回的而是一个List<Person>类型的对象，
            //所以我们在视图文件中：@model List<Person>

        }
    }
}