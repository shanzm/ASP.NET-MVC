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

            UseAutoFac4();
            Console.ReadKey();
        }

        private static void InterfaceOriented()
        {
            //首先不使用AutoFac
            IUserBll userbll = new UserBll();//其实这里就是面向接口编程了，我们定义个一个接口类型的变量，赋值一个实现该接口的类的实例（即里氏原则）
            userbll.Login("shanzm", "123456");

            IAnimalBll dogBll = new DogBll();
            dogBll.Bark();

            Console.ReadKey();
        }

        private static void UseAutoFac()
        {
            ContainerBuilder builder = new ContainerBuilder();
            //把UserBll注册为IUserBll的实现类，即把实现了接口的类注册给他的接口！
            builder.RegisterType<UserBll>().As<IUserBll>();
            builder.RegisterType<DogBll>().As<IAnimalBll>();//把DogBll注册为IAnimalBll的实现类，注意在TestBllImpl项目中有多个IAnimalBll的实现类

            IContainer container = builder.Build();

            IUserBll userBll = container.Resolve<IUserBll>();
            IAnimalBll dogBll = container.Resolve<IAnimalBll>();
            userBll.Login("shanzm", "123456");
            dogBll.Bark();
        }

        private static void UseAutoFac2()
        {
            ContainerBuilder builder = new ContainerBuilder();
            //把UserBll注册为IUserBll的实现类，即
            //builder.RegisterType<UserBll>().As<IUserBll>();

            //有可能UersBll实现了多个接口，我们可以把该类注册给他所有实现的接口
            //换言之，只要是UserBll实现的接口，我们都可以该他一个UserBll对象

            builder.RegisterType<UserBll>().AsImplementedInterfaces();
            IContainer container = builder.Build();

            IUserBll userBll = container.Resolve<IUserBll>();//但是要注意的是若是TestBll项目中对IUserBll接口有多个实现类，则这里给接口注册的实现类具体是哪一个是不确定的

            userBll.Login("shanzm", "123456");

        }

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

        private static void UseAutoFac4()
        {
            ContainerBuilder builder = new ContainerBuilder();

            Assembly asm = Assembly.Load(" TestBLLImpl");
            builder.RegisterAssemblyTypes(asm).AsImplementedInterfaces();
            IContainer container = builder.Build();

            //IAnimalBll animalBll = container.Resolve<IAnimalBll>();
            //animalBll.Bark ();//因为在TestBLLImpl项目中有多个IAnimalBll接口的的实现类，所以这里的animalBll中具体是哪个类型的对象你也不知道

            //将所有实现了IAnimalBll接口的对象都注册在一个集合中，遍历该集合分别使用每个实现了IAnimalBll接口的对象
            IEnumerable<IAnimalBll> animalBlls = container.Resolve<IEnumerable<IAnimalBll>>();
            foreach (var bll in animalBlls)
            {
                Console.WriteLine(bll.GetType());
                bll.Bark();
            }

        }
    }
}
