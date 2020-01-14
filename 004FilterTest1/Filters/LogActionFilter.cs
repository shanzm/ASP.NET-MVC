using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _04FilterTest1.Filters
{
    public class LogActionFilter : IActionFilter
    {
        //action执行完后的操作
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            string ctrName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            string path = filterContext.HttpContext.Server.MapPath("~/log.txt");
            File.AppendAllText(path, DateTime.Now.ToString() + "执行了" + ctrName + "." +actionName + "\r\n");
        }

        //action执行前的操作
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            string ctrName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            string path = filterContext.HttpContext.Server.MapPath("~/log.txt");
            File.AppendAllText(path, DateTime.Now.ToString() + "将要执行" + ctrName + "." + actionName + "\r\n");
        }
    }
}