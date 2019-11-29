using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _04FilterTest1.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            string path = filterContext.HttpContext.Server.MapPath("~/error.log");

            string exceptionMsg = filterContext.Exception.Message;
            File.AppendAllText(path, DateTime.Now.ToString()+ exceptionMsg + "\r\n");

            //Undone:实现出现异常后记录在error.log,同时页面显示error！，而不是默认的黄色页面
            filterContext.Result = new ContentResult() { Content = "error!" };
        }
    }
}