using DropDownList1;
using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

//模糊查询
//在input的输入框中输入一个字符串，“天”或是“前天”则在数据库的T_News表中模糊查询
//通过$.Post()实现异步刷新，在输入框下面显示在数据库中模糊查询的前五条数据

namespace MVCDemo.Controllers
{
    public class SearchController : Controller
    {
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string title)
        {
            DataTable dtNews = SqlHelper.GetDataTable("select top 5 * from T_News where Title like @title",
                      System.Data.CommandType.Text,
                      new SqlParameter("@title", "%" + title + "%"));
            List<News> listNews = new List<News>();
            //for (int i = 0; i < dtNews.Rows.Count; i++)
            //{
            //    listNews.Add(dtNews.Rows[i].);
            //}
            foreach (DataRow row in dtNews.Rows)
            {
                listNews.Add(new News() { Id = Convert.ToInt32(row["Id"]), Title = row["Title"].ToString(), Content = row["Content"].ToString() });
            }

            ReturnData<News> rm = new ReturnData<News>() { Data = listNews };

            //JavaScriptSerializer jss = new JavaScriptSerializer();
            //return Content(jss.Serialize(rm));

            return Json(rm,JsonRequestBehavior.AllowGet);
            //注意使用Json()函数，返回的是一个JavaScript中的Json对象
            //所以在前端页面中就不需要再对该数据使用JSON.Parse()反序列化

        }
    }
}