using _002CheckData.Models;
using CheckData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _002CheckData.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (!ModelState.IsValid)//检验是否符合特性约束
            {
                return View("Login");
            }
            SqlParameter[] param =
            {
                new SqlParameter ("@Name",user.Name ),
                new SqlParameter ("@Password",user.Password )
            };

            string sql = "select * from tb_userInfo where userName=@Name and userPass=@Password";
            int result = SqlHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text, param);
            if (result == 1)
            {
                Session["username"] = user.Name;
                return Content($"{user.Name }登录成功！");
            }
            else
            {
                return Redirect("~/Login/Login");
            }

        }
    }
}