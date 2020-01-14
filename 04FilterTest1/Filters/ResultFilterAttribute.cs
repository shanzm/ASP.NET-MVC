using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _04FilterTest1.Filters
{
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