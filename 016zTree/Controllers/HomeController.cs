using _016zTree.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        [HttpGet]
        public ActionResult Index2()
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
                    Id = Convert.ToInt32(row["Id"]),//节点Id
                    Name = row["Name"].ToString(),//节点名字
                    Pid = Convert.ToInt32(row["Pid"]),//父节点
                    Url = "https://baike.baidu.com/item/" + row["Name"].ToString()//给节点添加超链接，可以在后台进行拼接，就像这里这样。
                };
                if (IsParent(zNode.Id))
                {
                    zNode.IsParent = true;
                    zNode.Open = false;
                }
                else
                {
                    zNode.IsParent = false;
                }
                listZNode.Add(zNode);
            }
            return new JsonNetResult() { Data = listZNode };
        }

        /// <summary>
        /// 判断该Id的节点是否是父节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [NonAction]
        public bool IsParent(int id)
        {
            SqlParameter param = new SqlParameter("@Id", id);
            string sql = "select count(*) from Company where Pid=@Id";
            int count = (int)SqlHelper.ExecuteScalar(sql, CommandType.Text, param);
            return count > 0 ? true : false;
        }
    }
}