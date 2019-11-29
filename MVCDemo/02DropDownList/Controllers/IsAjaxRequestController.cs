using DropDownList1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _02DropDownList.Controllers
{
    public class IsAjaxRequestController : Controller
    {

        ///这个Demo是什么意思呢？
        ///首先调试这个/IsAjaxRequest/Index
        ///显示了一个只有一个按钮的页面，你点击按钮则是Ajax请求
        ///我们在Jquery代码中设置请求为/IsAjaxRequest/Index1
        ///此时Index1()函数判断为是Ajax请求，则返回Josn(p)
        ///最终显示的是p.Name
        ///而我们判断是否是Ajax请求的使用的就是Request.IsAjaxRequest()这个方法
        ///原理是主流的浏览器在发送Ajax请求的时候会在报文头中添加：X-Requested-With：IsAjaxRequest
        ///我们判断就是根据请求的报文头中是否有：X-Requested-With：IsAjaxRequest



        public ActionResult Index()
        {
            return View();
        }

        // GET: IsAjaxRequest
        public ActionResult Index1()
        {
            Person p = new Person() { Id = 1, Name = "shanzm", Age = 23 };
            if (Request.IsAjaxRequest())
            {
                return Json(p);
            }
            else
            {
                return Content(p.Name);
            }

        }
    }
}