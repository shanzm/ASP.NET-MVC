using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropDownList1.Models;
using System.Text;
using System.Collections;

namespace _02DropDownList.Controllers
{
    public class CheckDataController : Controller
    {
        // GET: CheckData
        //调试的时候爱浏览器中输入：
        //http://localhost:55063/CheckData/Index?Age=123， 则显示123
        //http://localhost:55063/CheckData/Index?Age=aa, 则显示“数据格式错误”
        //因为asp.net mvc 会自动检测数据类型，我们controller和view之间的数据传递是使用Model格式
        //所以我们可以在URL中加入Model属性作为参数进行传递，而 asp.net mvc会自动对数据的类型进行检测

        //同时若是数据类型是不符合的，asp.net mvc检测后不会抛出异常，而是程序员使用ModelSate.IsValid同过判断，来进行操作
        //其中的ModelSate是Controller类的一个属性

        //我们可是编写一个静态函数:private static string GetVlaidMsg(ModelStateDictionary modelState)，
        //我们可以新建议个MVCHelper类库，把这函数放在类库里，便于其他地方调用
        //返回值是错误信息列表(键值对形式)。
        //参数是ModelStateDictionary类型的，
        //ModelStateDictionary有许多同名类型分属不同的命名空间，注意这里命名空间是using System.Web.Mvc


        //ModelState从何而来？里面存放的是什么？
        //首先只要是.cshtml页面发送请求，则控制器的ModelState属性就会接收到值
        //接收的是Model属性类型错误的一个键值对数据，而且一个Model属性的错误可能是多个（也就是一个key对应多个vallue）

        public ActionResult Index(Person p)
        {
            if (ModelState.IsValid)
            {
                return View(p);
            }
            else
            {
                //return Content("数据格式错误！");
                string errorMsg = GetVlaidMsg(ModelState);
                return Content(errorMsg);
            }
        }

        private static string GetVlaidMsg(ModelStateDictionary modelState)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var propName in modelState.Keys)
            {
                if (modelState[propName].Errors.Count() <= 0)//相当于：If(!modelState[propName].Errors.Ang()，Any()函数：如果有数据则返回true ，否则返回false
                {
                    continue;
                }
                sb.AppendFormat("属性错误：", propName, ":");// sb.Append("属性错误：").Append(proName).Append(":");
                foreach (ModelError modelError in modelState[propName].Errors)
                {
                    sb.Append(modelError.ErrorMessage);
                }
            }
            return sb.ToString();
        }
    }
}