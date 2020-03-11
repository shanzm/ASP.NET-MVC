using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBLLImpl;
using TestIBLL;

namespace TestBLLImpl
{
    public class MasterBll : IMasterBll
    {
        //注意这里，这是一个接口的实现类，但是这个类有一个一个接口类型的属性
        public IUserBll userBll { get; set; }
        public void Walk()
        {
            Console.WriteLine("散步！");
            userBll.Login("shanzm", "sss");//在调用中，使用.PropertiesAutowired()方法给userBll注册其实现类
        }
    }
}