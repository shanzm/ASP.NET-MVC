using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TestMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            #region AutoFac配置
            ContainerBuilder builder = new ContainerBuilder();
            //using Autofac.Integration.Mvc;

            //把当前程序集中的所有Controllerr都注册到容器
            //换句话说就是把当前程序集中所有的IController接口都为其注册一个实例
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            //获取所有相关类库的程序集并把所有的接口实现类注册给相应的接口
            //Assembly[] assemblies = new Assembly[] { Assembly.Load("TestServiceImpl") };
            //builder.RegisterAssemblyTypes(assemblies)
            //    .Where(type => !type.IsAbstract)
            //    .AsImplementedInterfaces()
            //    .PropertiesAutowired();

            //此处只有TestServiceImpl程序集中的实现类注册给相应的接口
            Assembly asmSevice = Assembly.Load("TestServiceImpl");
            builder.RegisterAssemblyTypes(asmSevice)
                .Where(type => !type.IsAbstract)//除去抽象类，抽象类不可以实例化（其实这一句也可以不写）
                .AsImplementedInterfaces()
                .PropertiesAutowired();//接口实现类中接口类型的属性也注册

            //现在上面的那些配置，是可以为实现IController接口和TestIService中的接口的类注入对象
            //而那些没有实现这些接口的类，是无法使用使用AutoFac为期注入对象的
            //所以添加以下代码：
            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly).PropertiesAutowired();


            IContainer container = builder.Build();
            //注册系统级别的注入，即MVC中的所有Controller类都是由AutoFac帮我们创建对象
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion

        }
    }
}
