using System;
using System.Collections.Generic;
using System.Linq;



#region 说明
/// <summary>
/// 首先定义基础数据操作父类BaseDAL
///     * BaseDAL使用的Dapper.Contrib.Extensions;
///     * BaseDAL中只有最基本的增删改查操作
/// 所有的对象操作类XXService都继承于该类
///     * 基类中没有的操作，可以在XXService中自己实现:比如事务中同时执行操作，比如执行存储过程等
/// </summary>
#endregion


namespace _017Dapper2
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            //InsertUser();

            UpdateUser();

            //QueryUser();

            //InsertUserAndDepartment();


            Console.ReadKey();
        }


        //插入
        public static bool InsertUser()
        {
            UserService userService = new UserService();
            bool isSuccess = userService.Insert(new UserModel() { Name = "Tom", Age = 10, Gender = true, UpdateTime = DateTime.Now });
            return isSuccess;
        }


        //更新
        public static bool UpdateUser()
        {
            //注意更新是按照主键进行更新的，
            //若是没有给新对象的某些属性赋值则以此更新，会将没有赋值的属性字段更新为默认值

            UserService userService = new UserService();
            bool isSuccess = userService.Update(new UserModel { Id = 1, Name = "TomUpdate", UpdateTime = DateTime.Now });
            return isSuccess;
        }

        //查询
        public static void QueryUser()
        {
            UserService userService = new UserService();
            List<UserModel> listUser = userService.GetAll().ToList();
            listUser.ForEach(item => Console.WriteLine(item.Name + ":" + item.Age + ":" + item.GenderCH));
        }


        //同一个事务中插入User和Department
        public static void InsertUserAndDepartment()
        {
            UserService userService = new UserService();
            Tuple<bool, string> result = userService.InsertUserAndDepartment(
               new UserModel() { Name = "Jerry", Age = 10, Gender = false, UpdateTime = DateTime.Now },
               new DepartmentModel() { Name = "IT", Address = "1楼东" }
               );
            Console.WriteLine(result.Item2);
        }
    }
}
