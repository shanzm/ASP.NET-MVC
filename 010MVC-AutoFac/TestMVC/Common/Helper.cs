using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestIService;

namespace TestMVC.Common
{
    ///这个类不是MVC中的类，这个类的对象不是使用AutoFac创建的，（其实这是一个静态的帮助类，不需要创建对象呀），
    ///但是在这个类中我们使用了接口对象，无法使用PropertiesAutowired()方法为为属性注入对象

    ///注意只有AutoFac组测的对象，才可能给该对象的属性赋值
    public static class Helper
    {
        public static string Test()
        {
            IUserService userService = DependencyResolver.Current.GetService<IUserService>();
            return userService.UserAction("shanzm");
        }
    }
}