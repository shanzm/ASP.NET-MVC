using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _04FilterTest1.Filters
{
    //注意定义过滤器就是实现MVC5个过滤器接口中的某一个
    //然后在Global.asax.cs文件中注意该过滤器，实现全局过滤

    //但是我们也可以在实现过滤器接口的同时，也继承FilterAttribute特性
    //这样我们就可以把该过滤器当作特性标签标注在特定的action或controller上，而不是全局过滤

    ///这个结果过滤器是计算OnResultExecuting（）和OnResultExecuted(）之间的触发间隔
    public class ResultFilterAttribute : FilterAttribute, IResultFilter
    {
        private Stopwatch timer;
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            timer.Stop();
            filterContext.HttpContext.Response.Write(string.Format("<div>Result elasped time:{0:F6}</dic>", timer.Elapsed.TotalSeconds));
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //timer = Stopwatch.StartNew();
            timer = new Stopwatch();
            timer.Start();
        }
    }
}