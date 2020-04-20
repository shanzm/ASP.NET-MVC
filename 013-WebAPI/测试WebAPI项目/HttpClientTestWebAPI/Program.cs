using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientTestWebAPI
{
    class Program
    {

        //这里使用HttpClient对象发送请求
        //注意运行前，先要把WebAPI项目运行
        //调试方法：开多个VS，一个VS运行WebAPI项目，一个VS运行该测试项目
        static void Main(string[] args)
        {
            HttpClient hc = new HttpClient();
            string html = hc.GetStringAsync("http://localhost:62814/api/person/2").Result;
            Console.WriteLine(html); ;
            Console.ReadKey();
        }

    }
}
