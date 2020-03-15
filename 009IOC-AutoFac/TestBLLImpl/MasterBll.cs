using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBLLImpl;
using TestIBLL;

namespace TestBLLImpl
{
    //宠物主人类，实现了IMasterBll接口和IUserBll接口
    public class MasterBll : IMasterBll,IUserBll
    {
        //注意这里，这是一个接口的实现类，但是这个类有一个一个接口类型的属性
        public IAnimalBll dogBll { get; set; }

        public void AddNew(string userName, string pwd)
        {
            Console.WriteLine($"新增了一个Master用户：{userName}");
        }

        public bool Login(string userName, string pwd)
        {
            Console.WriteLine($"登录用户是Master：{userName}");
            return true;
        }

        public void Walk()
        {
            Console.WriteLine("带着狗散步！");
            dogBll.Cry();//在调用中，使用.PropertiesAutowired()方法给dogBll注册其实现类
        }
    }
}