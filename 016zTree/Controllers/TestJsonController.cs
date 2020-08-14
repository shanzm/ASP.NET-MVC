using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _016zTree.Models;

namespace _016zTree.Controllers
{
    public class TestJsonController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTree()
        {
            List<Employee> Employees1 = new List<Employee>()
            {
                new  Employee(){EmpNo=001,Name="开发张三"},
                new Employee (){EmpNo=002,Name="开发李四"},
                new Employee (){EmpNo=003,Name="开发王五"},
                new Employee (){EmpNo=004,Name="开发赵六"},
                new Employee (){EmpNo=005,Name="开发周七"}
            };

            List<Employee> Employees2 = new List<Employee>()
            {
                new  Employee(){EmpNo=006,Name="项目张三"},
                new Employee (){EmpNo=007,Name="项目李四"},
                new Employee (){EmpNo=008,Name="项目王五"},
                new Employee (){EmpNo=009,Name="项目赵六"}

            };

            Department Development1 = new Department() { DepNo = 0001, Name = "开发", Employees = Employees1 };
            Department Development2 = new Department() { DepNo = 0002, Name = "项目", Employees = Employees2 };

            List<Department> Departments = new List<Department>() { Development1, Development2 };

            Company Company = new Company() { Id = 00001, Name = "公司", Departments = Departments };

            return new JsonNetResult() { Data = Company };

        }
    }
}