using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _015BootstrapTable.Controllers
{
    public class Test1Controller : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string balance)
        {
            if (!string.IsNullOrEmpty(balance))
            {
                decimal bal = Convert.ToDecimal(balance);
                //调试：控制台打印前端传递的参数
                //Debug.WriteLine(bal.ToString());
                SqlParameter param = new SqlParameter("@Balance", bal);
                string sql = "select * from szmBank where Balance>@Balance";
                DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text, param);
                return new JsonNetResult() { Data = dt };
            }
            else
            {
                return Content("请输入单号");
            }
        }

        [HttpPost]
        public ActionResult Test(string row)
        {
            return View("Index",row);
        }
    }
}