using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestIBLL;

//TestBLLImpl是一个类库项目 ，
//在该项目中分别实现TestIBLL项目中的每一个实体类的接口
//该项目需要引用TestIBLL项目

//Impl就是Implement的缩写

namespace TestBLLImpl
{
    public class UserBll : IUserBll
    {
        public void AddNew(string userName, string pwd)
        {
            Console.WriteLine($"新增了一个普通用户：{userName}");
        }

        public bool Login(string userName, string pwd)
        {
            Console.WriteLine($"登录用户是普通用户：{userName}");
            return true;
        }
    }
}
