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
            return true;
        }

        public string UserAction(string userName)
        {
            string result = newsService.AddNews("2020年3月16日-新冠病毒", "中国境内的新冠病毒被有效遏制");
            return userName+"  添加新闻  ："+result;
        }
    }
}
