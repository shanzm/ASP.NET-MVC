using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DropDownList1.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult List()
        {
            DataTable dt = SqlHelper.GetDataTable("select * from T_Person", CommandType.Text);
            return View(dt);//注意我们这里传递的数据是DataTable类型的Model，所以我们需要在List.cshtml页面的开头：@model System.Data.DataTable
        }

        [HttpGet]
        public ActionResult Add()//从List.cshtml页面的超链接跳转，调用的是这个Add（）
        {
            return View();
        }

        [HttpPost]

        //从Add.cshtml页面表单提交过往，调用的是这个Add()
        //表单中的input标签的value值作为这里的参数（标签的name属性和参数名一一对应，大小写不敏感）

        #region 把Add.cshtml文件中的表单的提交的数据当作是一般参数
        //public ActionResult Add(string name, int age, string email, int classId)
        //{
        //    SqlParameter[] param =
        //    {
        //       new  SqlParameter ("@Name",name),
        //       new SqlParameter ("@Age" ,age),
        //       new SqlParameter ("@Email",email),
        //       new SqlParameter ("@ClassId",classId)
        //    };
        //    SqlHelper.ExecuteNonquery("insert into T_Person (Name,Age,Email,ClassId) values(@Name,@Age,@Email,@ClassId);", CommandType.Text, param);

        //    return Redirect("~/Person/List");
        //}

        #endregion 
        public ActionResult Add(Person model, string BtnSubmit)
        {
            SqlParameter[] param =
              {
               new  SqlParameter ("@Name",model.Name ),
               new SqlParameter ("@Age" ,model.Age),
               new SqlParameter ("@Email",model.Email),
               new SqlParameter ("@ClassId",model.ClassId )
            };
            switch (BtnSubmit)
            {
                case "AddNew":
                    SqlHelper.ExecuteNonquery("insert into T_Person (Name,Age,Email,ClassId) values(@Name,@Age,@Email,@ClassId);", CommandType.Text, param);
                    break;

                case "Cancel":
                    break;
            }
            return Redirect("~/Person/List");



        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            SqlParameter param = new SqlParameter("@Id", id);
            DataTable dt = SqlHelper.GetDataTable("select * from T_Person where Id=@Id", CommandType.Text, param);
            Person p = new Person();
            p.Name = Convert.ToString(dt.Rows[0]["Name"]);
            p.Age = Convert.ToInt32(dt.Rows[0]["Age"]);
            p.Email = dt.Rows[0]["Email"].ToString();
            p.ClassId = Convert.ToInt32(dt.Rows[0]["ClassId"]);
            p.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
            //注意不要忘记把Id传递给Edit.cshtml页面，虽然没有显示在用户界面，
            //因为由Edit.cshtml跳转到具体的编辑界面使用的Id在超链接中，并没有给Model
            //所以在具体的编辑页面提交后，model中并没有Id值
            return View(p);

        }

        [HttpPost]
        public ActionResult Edit(Person model)//Edit.cshtml的页面的“@Model”传递到这里作为参数
        {
            SqlParameter[] param =
             {
               new SqlParameter ("@Name",model.Name ),
               new SqlParameter ("@Age" ,model.Age ),
               new SqlParameter ("@Email",model.Email ),
               new SqlParameter ("@Id",model.Id ),
               new SqlParameter ("@ClassId",model.ClassId )
            };
            SqlHelper.ExecuteNonquery("update T_Person set Name=@Name,Age=@Age,Email=@Email,ClassId=@ClassId where Id=@Id", CommandType.Text, param);
            return Redirect("~/Person/List");
        }

        public ActionResult Delete(long Id)
        {
            SqlParameter param = new SqlParameter("Id", Id);
            SqlHelper.ExecuteNonquery("delete from T_Person where Id=@Id", CommandType.Text, param);

            return Redirect("~/Person/List");

        }
    }
}