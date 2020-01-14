using DropDownList1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _002CheckData.Controllers
{
    public class DropDownList3Controller : Controller
    {
        // GET: DropDownList3
        public ActionResult Index()
        {
            List<Person> list = new List<DropDownList1.Models.Person>();
            list.Add(new Person() { Id = 001, Name = "张三", Age = 12 });
            list.Add(new Person() { Id = 002, Name = "李四", Age = 13 });
            list.Add(new Person() { Id = 003, Name = "王五", Age = 14 });

            //使用SelectList类型的集合对list进行包装
            //SelectList()对象的构造函数有很多重载
            //第一个参数：是实现了IEnumberable接口的对象
            //第二个参数：string dataValueField是在option标签中的value属性值
            //第三个参数：string dataTextField是在option标签中的InterText值（即显示的值）
            //注意上面的两个参数的取值是Person类型的属性名
            //第四个参数：object selectedValue，表示被选中的那个option标签的value值
            SelectList selectList = new SelectList(list, "Id", "Name",2);
            return View(selectList );
        }
    }
}