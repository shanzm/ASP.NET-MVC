using _016zTree.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _016zTree.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string id)
        {
            string sql = "select * from Company";
            DataTable dt = SqlHelper.GetDataTable(sql, System.Data.CommandType.Text);
            List<ZNode> listZNode = new List<ZNode>();

            foreach (DataRow row in dt.Rows)
            {

                ZNode zNode = new ZNode()
                {
                    Id = row["Id"].ToString(),//节点Id
                    Name = row["Name"].ToString(),//节点名字
                    Pid = row["Pid"].ToString(),//父节点
                    Url = "https://baike.baidu.com/item/" + row["Name"].ToString()//给节点添加超链接，可以在后台进行拼接，就像这里这样。
                };
                if (zNode.Id=="0")
                {
                    zNode.Pid = null;
                }
            else if (zNode.Pid == "0")
                {
                
                    {

                    }
                }
                listZNode.Add(zNode);
            }
            return new JsonNetResult() { Data = listZNode };
        }
    }
}