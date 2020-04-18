using Bll;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//新建一个WebAPI项目
//添加一个PersonController WEPAPI控制器
//添加空视图则仅有一个PersonController类
//添加含有增删改查的WEPAPI视图，则含有四个Action，如下：

//注意在当前的项目，当前的Controllers文件夹中是可以添加MVC的Controller,只要定义继承于IController接口
//WebAPI的控制器继承于ApiController接口

namespace WebAPIDemo.Controllers
{
    public class PersonController : ApiController
    {
        PersonBll pBll = new PersonBll();

        // GET: api/Person
        //TEST:http://localhost:62814/api/person //请求PersonController控制器中的以get开头的无参请求
        //注意：默认请求是Get请求，所以URL是api/person,
       
        public IEnumerable<PersonDto> GetPersonAll()
        {
            return pBll.GetPersonAll();
            // return new string[] { "shanzm1", "shanzm2" };
        }

        //public PersonDto Get()
        //{
        //    return new PersonDto() { Id = 001, Name = "shanzm", Age = 25 };
        //}


        // GET: api/Person/5
        public string Get(int id)
        {
            return "value-shanzm";
        }


        //注意api的请求URL中是没有action名字的，Action 定义为什么名字都是无关紧要的。
        //注意其URL是：http://localhost:62814/api/person?num=123456
        //其组成是api/控制器名（不包括Controller）？参数=参数值
        public string GetPhoneNum(string Num)
        {
            return $"你的手机号是{Num}";
        }




        // POST: api/Person
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Person/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Person/5
        public void Delete(int id)
        {
        }
    }
}
