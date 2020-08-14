using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _016zTree.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Department> Departments { get; set; }
    }

    public class Department
    {
        public int DepNo { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }

    public class Employee
    {
        public int EmpNo { get; set; }
        public string Name { get; set; }
    }
}