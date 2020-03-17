using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestIService;

namespace TestMVC.Common
{
    ///注意这个Person类虽然不是静态类，但是没有也没有实现TestIService中的任何接口
    ///而且也不是实现IController接口的类
    ///所以就目前的配置，这个类的对象不是AutoFac为其创建的，所以AutoFac也是无法为其接口属性userService赋值的
    ///
    ///所以我们在Global.asax.cs 文件中添加这样一句代码：
    ///  builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly).PropertiesAutowired();


    public class Person
    {
        public IUserService userService { get; set; }

        public string Test()
        {
            return userService.UserAction("shanzm");
        }
    }
}