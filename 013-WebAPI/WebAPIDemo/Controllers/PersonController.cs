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
        // GET: api/Person
        public IEnumerable<string> Get()//TEST:http://localhost:62814/api/person
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Person/5
        public string Get(int id)
        {
            return "value";
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
