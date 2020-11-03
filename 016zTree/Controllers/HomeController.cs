using _016zTree.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

#region 数据库备份
//USE[ShanTest]
//GO
///****** Object:  Table [dbo].[Company]    Script Date: 2020-11-03 11:23:43 ******/
//SET ANSI_NULLS ON
//GO
//SET QUOTED_IDENTIFIER ON
//GO
//CREATE TABLE[dbo].[Company]
//(

//   [Id][nvarchar](50) NULL,
//	[Pid] [nvarchar] (50) NULL,
//	[Name] [nvarchar] (50) NULL
//) ON[PRIMARY]

//GO
//INSERT[dbo].[Company]
//([Id], [Pid], [Name]) VALUES(N'1', N'0', N'中国总公司')
//GO
//INSERT[dbo].[Company]
//([Id], [Pid], [Name]) VALUES(N'2', N'1', N'江苏分公司')
//GO
//INSERT[dbo].[Company]
//([Id], [Pid], [Name]) VALUES(N'3', N'1', N'浙江分公司')
//GO
//INSERT[dbo].[Company]
//([Id], [Pid], [Name]) VALUES(N'10', N'1', N'上海分公司')
//GO
//INSERT[dbo].[Company]
//([Id], [Pid], [Name]) VALUES(N'4', N'2', N'苏州分公司')
//GO
//INSERT[dbo].[Company]
//([Id], [Pid], [Name]) VALUES(N'5', N'2', N'无锡分公司')
//GO
//INSERT[dbo].[Company]
//([Id], [Pid], [Name]) VALUES(N'6', N'3', N'湖州分公司')
//GO
//INSERT[dbo].[Company]
//([Id], [Pid], [Name]) VALUES(N'7', N'3', N'杭州分公司')
//GO
//INSERT[dbo].[Company]
//([Id], [Pid], [Name]) VALUES(N'8', N'4', N'吴江区分公司')
//GO
//INSERT[dbo].[Company]
//([Id], [Pid], [Name]) VALUES(N'9', N'4', N'吴中区分公司')
//GO

#endregion

namespace _016zTree.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 数据写死，演示zTree
        /// 案例来源：在zTree官网下载文档，文档中的zTree/demo/cn/core中的SimpleData.html
        /// </summary>
        /// <returns></returns>
        public ActionResult Index1()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index2()
        {
            return View();
        }

        /// <summary>
        /// 这里其实是根据用户点击的父节点查找该节点下的所有子节点
        /// 但是开始初始化的时候，是无参的，所以查询总父节点下的所有二级子节点
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetZtreeNodeJson(int? Id)
        {
            string sql = "select * from Company where Pid=@Id";
            SqlParameter sqlParameter = new SqlParameter("@Id", Id ?? 1);//中国总公司的id=1
            DataTable dt = SqlHelper.GetDataTable(sql, System.Data.CommandType.Text, sqlParameter);
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
                if (IsParent(zNode.Id)||zNode.Id==10)
                {
                    zNode.IsParent = true;
                    zNode.Open = true;
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
        /// 简单的判断该Id的节点是否是父节点
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