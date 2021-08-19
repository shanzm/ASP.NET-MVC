using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _014使用newtonjson.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult ShowUserInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ShowUserInfoWithJsonResult(string name, int age)
        {
            //Json()创建的Josn对象是使用JavaScriptSerializer对像序列化的
            //其序列化的结果中DateTime类型会出现不是我们想要的格式
            //而且序列化后的属性在json对象中也是大写开头的，在C#代码中属性大写开头，而JavaScript代码中一般风格是小写开头（当然这个问题无关紧要，使用大写即可）
            //下面你可以测试以下代码：
            User user = new User() { Id = 001, Name = name, Age = age, CreateTime = DateTime.Now };
            return Json(user);
        }


        public ActionResult ShowUserInfoWithJosnNetResult(string name, int age)
        {
            //为了解决MVC中Json()序列化时间格式的问题，一般是使用newtonjs(也称为Josn.Net)框架解决
            //首先安装：PM>install-package newtonsoft.json.12.0.3(最新版本)
            //接着定义一个JosnNetResult继承于JsonResult多像（Json()创建的即JsonResult对象）
            //这就相当于自定义一个ActionResult对象（JosnResult对象是继承于ActionResult对象）
            //可以使用以下代码测试：
            User user = new User() { Id = 001, Name = name, Age = age, CreateTime = DateTime.Now };
            return new JsonNetResult() { Data = user };
        }

        [JsonNetActionFilter]
        public ActionResult ShowUserInfoWithJosnNetResultByFilter(string name, int age)
        {
            //按照AOP的思想，我们实现非“侵入式”的代码
            //定义一个过滤器，实现IActionFilter,
            //在过滤器中，将Acton要返回的JsonResult对象换为JsonNetResult对象
            User user = new User() { Id = 001, Name = name, Age = age, CreateTime = DateTime.Now };
            return Json(user);
        }

        //使用NewtonJson测试序列化DataTable
        public ActionResult TestDataTable()
        {

            //这里我是测试一下使用NewtonJson序列化DataTable
            DataTable dt = new DataTable("Student");
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            dt.Rows.Add(new object[] { 1, "张三" });
            dt.Rows.Add(new object[] { 2, "李四" });
            var jsonResult = new JsonNetResult() { Data = dt };
            return new JsonNetResult() { Data = dt };
        }

        //测试NewtonJson接受Get请求
        //undone:1.可能是封装的JsonNetResult类的关系，使用get请求返回josn就会出现“json get not allow”
        //2.new JsonNetResult() { Data = dt ,JsonRequestBehavior.AllowGet };这样写编译无法通过，显示：初始值设定项成员声明符无效
        public ActionResult TestGet()
        {
            DataTable dt = new DataTable("Student");
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            dt.Rows.Add(new object[] { 1, "张三" });
            dt.Rows.Add(new object[] { 2, "李四" });
            // return new JsonNetResult() { Data = dt ,JsonRequestBehavior.AllowGet };
            return new JsonNetResult() { Data = dt };
        }
    }
}