using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestIService;
using TestMVC.Common;

//PM> Install-Package AutoFac.Mvc5

namespace TestMVC.Controllers
{
    public class HomeController : Controller
    {

        public IUserService userService { get; set; }//通过AutoFac自动为我们赋值一个IUserService接口实现对象

        //测试：控制器（也就是IController接口的实现类）中的接口类型的属性的注入
        public ActionResult CheckLogin()
        {
            bool b = userService.CheckLogin("shanzm", "123456");
            return Content(b.ToString());//结果：页面显示true
        }


        //测试：接口实现类中的接口类型的属性的注入
        public ActionResult UserAddNews()
        {
            string result = userService.UserAction("shanzm");
            return Content(result);//结果：页面显示：shanzm 添加新闻 ：Title：2020年3月16日-新冠病毒,Content：中国境内的新冠病毒被有效遏制
        }

        //测试：未实现任何接口的静态类中的接口属性的注入
        public ActionResult TestHelper()
        {
            string result = Helper.Test();
            return Content(result);//结果：页面显示：shanzm 添加新闻 ：Title：2020年3月16日-新冠病毒,Content：中国境内的新冠病毒被有效遏制
        }

        //测试:未实现任何接口的非静态类的注册即其接口类型的属性的注入
        public ActionResult TestPerson()
        {
            Person p = DependencyResolver.Current.GetService<Person>();
            string result = p.Test();
            return Content(result);//结果： 页面显示：shanzm 添加新闻 ：Title：2020年3月16日-新冠病毒,Content：中国境内的新冠病毒被有效遏制
        }

        //Undone：关于Quartz定时任务使用AutoFac注入
    }
}