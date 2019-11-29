using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _04FilterTest1.Controllers
{
    public class MainController : Controller
    {

        ///调试该项目：首先先运行http://localhost:57920/Login/Index
        ///使用admin,12345登录
        ///显示登录成功后，就会把name把Seesion中
        ///之后我们在运行http://localhost:57920/Main/Index
        ///因为与服务器没有断开连接，所以Seesion["name"]!=null,即用户已经登录
        // GET: Mian
        public ActionResult Index()
        {
            #region 在每一个action中进行权限的验证
            //if (Session["username"] != null)
            //{
            //    return Content("当前用户以登录，请继续操作！");
            //}
            //else
            //{
            //    return Content("请先登录");
            //}
            #endregion

            #region 使用OnAuthorization（）
            //在Global.asax中添加：GlobalFilters.Filters.Add(new CheckAuthorFilter());
           
            ViewBag.content= "当前用户已登录，你可以继续进行其他操作";
            return View(); 
            #endregion

        }
    }
}