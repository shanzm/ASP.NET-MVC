using DropDownList1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _02DropDownList.Controllers
{
    public class DropDownList2Controller : Controller
    {
        // GET: DropDownList2
        public ActionResult Index()
        {
            List<Person> list = new List<DropDownList1.Models.Person>();
            list.Add(new Person() { Id = 001, Name = "张三", Age = 12 });
            list.Add(new Person() { Id = 002, Name = "李四", Age = 13 });
            list.Add(new Person() { Id = 003, Name = "王五", Age = 14 });

            //List<SelectListItem> sliList = new List<SelectListItem>();
            //foreach (Person  p in list)
            //{
            //    SelectListItem listItem = new SelectListItem();//SelectListItem对象有以下三个属性，分别赋值
            //    listItem.Selected = (p.Id == 2);//是否选中状态，也就是是否生成 selected="selected"属性
            //    listItem.Text = p.Name;//显示的值，也就是<option>的 innerText 部分
            //    listItem.Value = p.Id.ToString();//生成的 value 属性，注意是 string 类型
            //    sliList.Add(listItem);//将SelectListItem类型的对象装入List<SelectListItem>中
            //}

            //以上遍历list集合，添加到sliList集合，可以使用Linq语句(或是使用List的Select()和Where()方法)
            //注意Linq查询后的结果是IEnumerable<SelectListItem>类型的变量，视图需要改为：@model IEnumberable<SelectListItem>
            var sliList = from item in list
                          select new SelectListItem { Selected = item.Id == 2, Text = item.Name, Value = item.Id.ToString() };
            return View(sliList);//注意我们在这里返回的Model是List<SelectListItem>类型的集合，所以我们在视图页要：@model List<SelectListItem>

        }
    }
}