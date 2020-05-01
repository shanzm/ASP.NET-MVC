using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _014使用newtonjson
{
    //这个过滤器就是把JosnResult换成JosnNetResult对象
    //把这个过滤器在Global.asax.cs中注册，从而实现将全局的return JsonResult对象转换为return JosnNetResult对象

    //但是我们为了演示返回的JosnResult对象，我们在这里就不全局注册该过滤器，而是添加一个FilterAttribute父类
    //在需要返回JsonNetResult的action上使用[JosnNetActionFilter]特性标签
    public class JsonNetActionFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //filterContext.Result 获取由操作方法返回的结果(即return的结果)
            //因为JsonNetResult是继承于JsonResult对象的，所以这里一定要添加一个判断不是JosnNetResult对象
            if (filterContext.Result is JsonResult && !(filterContext.Result is JsonNetResult))
            {
                //把action 返回的JsonResult对象完全换为JosnNetResult对象
                JsonResult jsonResult = (JsonResult)filterContext.Result;
                JsonNetResult jsonNetResult = new JsonNetResult();
                //将josnResult中的所有属性赋值给jsonNetResult相应的属性，可以根据VS智能提示查看jsonResult的所有属性
                jsonNetResult.ContentEncoding = jsonResult.ContentEncoding;
                jsonNetResult.ContentType = jsonResult.ContentType;
                jsonNetResult.Data = jsonResult.Data;
                jsonNetResult.JsonRequestBehavior = jsonResult.JsonRequestBehavior;
                jsonNetResult.MaxJsonLength = jsonResult.MaxJsonLength;
                jsonNetResult.RecursionLimit = jsonResult.RecursionLimit;

                filterContext.Result = jsonNetResult;
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}