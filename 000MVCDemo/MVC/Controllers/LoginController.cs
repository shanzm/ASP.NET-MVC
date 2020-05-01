using DemoBll;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC.Controllers
{
    public class LoginController : Controller
    {
        UserBll userBll = new UserBll();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            //检验是否符合特性约束
            if (!ModelState.IsValid)
            {
                return View("Login");
            }
            else
            {
                if (userBll.Login(loginModel.Name, loginModel.Password))
                {
                    // 创建认证Cookie。可用于以后的认证请求过程中。
                    FormsAuthentication.SetAuthCookie(loginModel.Name, false);
                    //登录成功跳转到User列表页面
                    return RedirectToAction("List", "User");
                }
                else
                {
                    ModelState.AddModelError("LoginError", "用户名或密码错误！");
                    return View();
                }
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();//清空Session
            return Redirect("~/Home/Index");
        }
    }
}