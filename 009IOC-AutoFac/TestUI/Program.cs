using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestBLLImpl;
using TestIBLL;

//TestUI是一个控制台项目，这里用来模拟UI层的实现
//整个分层，都是模拟实现的，所以也就不连接数据库了，不写DAL层了，
//只是为了简单的示范一个控制反转（Inversion of control ,IOC）容器AutoFac的使用方式

//该项目需要引用TestIBLL项目和TestBLLImpl项目

//该项目需要安装AutoFac，PM>Instal-Package Autofac

//使用 IOC 容器的时候，一般都是建议基于接口编程，也就是把方法定义到接口中，然后再编写实现类。
//在使用的时候声明接口类型的变量、属性，由容器负责赋值。 接口、实现
//类一般都是定义在单独的项目中，这样减少互相的耦合。

namespace TestUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //InterfaceOriented();

            //UseAutoFac();

            //UseAutoFac2();

            //UseAutoFac3();

            //UseAutoFac4();

            //UseAutoFac5();

            //UseAutoFac6();

            UseAutoFac7();

            Console.ReadKey();
        }

        //首先不使用AutoFac
        private static void InterfaceOriented()
        {

            IUserBll userbll = new UserBll();//其实这里就是面向接口编程了，我们定义个一个接口类型的变量，赋值一个实现该接口的类的实例（即里氏原则）
            userbll.Login("shanzm", "123456");

            IAnimalBll dogBll = new DogBll();
            dogBll.Cry();

            Console.ReadKey();
        }

        private static void UseAutoFac()
        {
            //创建容器构造者，用于注册组件和服务（组件：接口实现类，服务：接口）
            ContainerBuilder builder = new ContainerBuilder();
            //注册组件UserBll类，并把服务IUserBll接口暴露给该组件
            //把服务（IUserBll）暴露给组件（UserBll）
            builder.RegisterType<UserBll>().As<IUserBll>();
            // builder.RegisterType<DogBll>().As<IAnimalBll>();//把DogBll类注册给IAnimalBll接口，注意在TestBllImpl项目中有多个IAnimalBll的实现类
            builder.RegisterType<DogBll>().Named<IAnimalBll>("Dog");
            builder.RegisterType<CatBll>().Named<IAnimalBll>("Cat");
            //创建容器
            IContainer container = builder.Build();
            //使用容器解析服务（不推荐这样，可能造成内存的泄露）
            //IUserBll userBll = container.Resolve<IUserBll>();
            //IAnimalBll dogBll = container.Resolve<IAnimalBll>();
            //使用容器的生命周期解析服务（这样可以确保服务实例被妥善地释放和垃圾回收）
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IUserBll userBll = scope.Resolve<IUserBll>();
                IAnimalBll dogBll = scope.ResolveNamed<IAnimalBll>("Dog");
                IAnimalBll catBll = scope.ResolveNamed<IAnimalBll>("Cat");
                userBll.Login("shanzm", "123456");
                dogBll.Cry();
                catBll.Cry();
            }

        }

        //一个实现类实现了多个接口
        private static void UseAutoFac2()
        {
            ContainerBuilder builder = new ContainerBuilder();

            //MasterBll实现了多个接口，我们可以把该类注册给他所有实现的接口
            //换言之，只要是MasterBll实现的接口，我们都可以该他一个MasterBll类型的对象
            //但是注意，这个MasterBll对象只包含当前接口的方法


            builder.RegisterType<MasterBll>().AsImplementedInterfaces();//把MasterBll类注册给所有他实现的接口
            //即一个组件暴露了多个服务，这里就等价于：
            //builder.RegisterType<MasterBll>().As<IUserBll>().As<IMasterBll>();

            IContainer container = builder.Build();

            IUserBll userBll = container.Resolve<IUserBll>();//其实这里的userBll是MasterBll类型的对象,但是只具有IUserBll接口中的方法
            userBll.Login("shanzm", "11111");//打印：登录用户是Master:shanzm
            Console.WriteLine(userBll.GetType());//打印：TestBLLImpl.MasterBll

        }

        //把所有的接口实现类都注册
        private static void UseAutoFac3()
        {
            //从上面几个示例，你可以发现好像使用依赖反转，好像有点麻烦！
            //若是TestBLL中有许多类，需要注册，则就可以显示出使用控制反转的
            ContainerBuilder builder = new ContainerBuilder();

            Assembly asm = Assembly.Load(" TestBLLImpl");//把TestBLLImpl程序集中的所有类都是注册给他的接口
            builder.RegisterAssemblyTypes(asm).AsImplementedInterfaces();//注意RegisterAssemblyTypes()的参数可以是多个程序集
            IContainer container = builder.Build();
            //其实到这里你就会发现使用AutoFac的便利了，
            //你需要在TestIBLL项目中给定义每个接口，在TestBllImpl项目中去实现每个接口
            //在TestUI项目中就不需要像以前一样去实例每一个TestBLLImpl项目中的类，使用AutoFac把每个类注册给他的接口
            //然后直接调用接口对象就可以了，而且你都不用明白到底是哪个一个类实现该接口
            //从而实现了解耦


            IUserBll userBll = container.Resolve<IUserBll>();
            userBll.Login("shanzm", "123456");

            Console.WriteLine(userBll.GetType());//调试时查看uerBll接口对象中的具体实现对象:TestBllImpl.UserBll
        }

        //同一个接口多个实现类，同时注册
        private static void UseAutoFac4()
        {
            ContainerBuilder builder = new ContainerBuilder();

            Assembly asm = Assembly.Load(" TestBLLImpl");
            builder.RegisterAssemblyTypes(asm).AsImplementedInterfaces();
            IContainer container = builder.Build();

            //IAnimalBll animalBll = container.Resolve<IAnimalBll>();
            //animalBll.Bark ();
            //因为在TestBLLImpl项目中有多个IAnimalBll接口的的实现类，所以这里的animalBll中具体是哪个类型的对象你也不知道
            //如果不止一个组件暴露了相同的服务, Autofac将使用最后注册的组件作为服务的提供方

            //将所有实现了IAnimalBll接口的对象都注册在一个集合中，遍历该集合分别使用每个实现了IAnimalBll接口的对象
            IEnumerable<IAnimalBll> animalBlls = container.Resolve<IEnumerable<IAnimalBll>>();
            foreach (var bll in animalBlls)
            {
                Console.WriteLine(bll.GetType());
                bll.Cry();
            }
            //挑选指定的某个实现类
            IAnimalBll dogBll = animalBlls.Where(t => t.GetType() == typeof(DogBll)).First();
            dogBll.Cry();
        }



        //接口实现类中的接口类型的属性
        private static void UseAutoFac5()
        {
            ContainerBuilder builder = new ContainerBuilder();

            Assembly asm = Assembly.Load("TestBLLImpl");
            //在这里通过.PropertiesAutowired()，给接口实现类中的接口属性也注册一个该类型的接口的实现类
            //即属性自动装配

            //法1:使用PropertiesAutowired()
            //builder.RegisterAssemblyTypes(asm).AsImplementedInterfaces().PropertiesAutowired();
            //builder.RegisterType<DogBll>().As<IAnimalBll>();
            //这样在给程序集中所有类注册的时候，自动为类中接口类型的属性也注册，但是有一个问题
            //若是该属性的接口类型有多个实现类，则使用PropertiesAutowired()为我们注册的实现类，可能不是我想要的
            //比如这里，我希望MasterBll类中的IAnimalBll类型的属性，注册一个DogBll类型的对象，但是实际是自动注册的是一个CatBll类型的对象
            //所以我们要显示的把Dogbll实现类注册给IAnimalBll接口

            //法2：使用WithProperty()
            //针对具体的实现类中具体的某个接口类型的属性进行注册
            builder.RegisterType<MasterBll>().As<IMasterBll>().WithProperty("dogBll", new DogBll());

            IContainer container = builder.Build();

            IMasterBll masterBll = container.Resolve<IMasterBll>();
            masterBll.Walk();
        }

        //在使用AutoFac的时候，  建议所有的实现类都写成无状态的
        //无状态的就是实现类中只去实现接口，而不要添加一些属性和字段
        //这样在注册接口的时候，所有的实现类的对象可以设置为单例模式
        //即整个系统中实现类的对象只有一个，降低了内存的占用，而且不会存在数据修改形成的脏数据
        private static void UseAutoFac6()
        {
            ContainerBuilder builder = new ContainerBuilder();

            Assembly asm = Assembly.Load(" TestBLLImpl");
            //这里通过使用SingleInstance()实现注册给接口的实现类的对象以单例模式
            builder.RegisterAssemblyTypes(asm).AsImplementedInterfaces().SingleInstance();
            IContainer container = builder.Build();

            //IUserBll userBll1 = container.Resolve<IUserBll>();
            //userBll1.Login("shanzm", "1111");

            //IUserBll userBll2 = container.Resolve<IUserBll>();
            //userBll2.Login("shanzm", "2222");

            using (var scope = container.BeginLifetimeScope())
            {
                IUserBll userBll1 = scope.Resolve<IUserBll>();
                userBll1.Login("shanzm", "1111");

                IUserBll userBll2 = scope.Resolve<IUserBll>();
                userBll2.Login("shanzm", "2222");

                //对比你就会发现，其实userBll1和userBll2指向的是同一个对象
                //因为是单例模式，所以在该程序中所有创建的IUserBll对象都是同一个。
                //若是去掉.SingleInstance()则会打印为false，即默认的不是单例模式
                Console.WriteLine(ReferenceEquals(userBll1, userBll2));//打印结果:true
            }


        }

        private static void UseAutoFac7()
        {
            ContainerBuilder builder = new ContainerBuilder();

            Assembly asm = Assembly.Load(" TestBLLImpl");
            //这里通过使用SingleInstance()实现注册给接口的实现类的对象以单例模式
            builder.RegisterAssemblyTypes(asm).AsImplementedInterfaces().SingleInstance();
            IContainer container = builder.Build();

            using (var scope1 = container.BeginLifetimeScope())
            {
                IUserBll userBll1 = scope1.Resolve<IUserBll>();
                userBll1.Login("shanzm", "1111");

                using (var scope2 = container.BeginLifetimeScope())
                {
                    IUserBll userBll2 = scope2.Resolve<IUserBll>();
                    userBll2.Login("shanzm", "2222");
                    Console.WriteLine(ReferenceEquals(userBll1, userBll2));
                    //因为是单一实例，所以就是在不同的生命周期中，也是同一个实例，打印结果:true
                }
            }

        }
    }
}
