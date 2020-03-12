using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestIService;

namespace TestServiceImpl
{
    public class UserService : IUserService
    {
        public INewsService newsService { get; set; }//注意接口的实现类是可以有接口类型的属性，该属性也会被注册一个实现对应类型接口的类的对象

        public bool CheckLogin(string userName, string pwd)
        {
            string result = newsService.AddNews("titlelala", "lalabody");
            return (result == "titlelala");//为了验证newsService属性被注册，所以这里其实是一定返回true;
        }

        public bool CheckUserNameExists(string userName)
        {
            return false;
        }

    }
}
