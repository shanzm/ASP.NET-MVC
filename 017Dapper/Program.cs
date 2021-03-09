using _017Dapper.DAL;
using _017Dapper.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


#region 说明
///0. 教程：https://dapper-tutorial.net/dapper
///0. 参考：https://github.com/StackExchange/Dapper
///0. 参考：https://www.cnblogs.com/huangxincheng/p/5832281.html
///0. 参考：https://www.cnblogs.com/flywong/p/9666963.html
///0. 参考：https://blog.csdn.net/qq_39360549/article/details/85291270
///0. 参考：https://www.cnblogs.com/pengze0902/p/6523458.html
///0. 参考：https://esofar.gitbooks.io/dapper-tutorial-cn/content/utilities/stored-procedure.html
///0. *参考：https://www.jianshu.com/p/e201e6a6870c
///1. 安装dapper:nuget>install-package dapper
///2. 定义一个Person类，有三个属性：Id,Name,Age,ClassId
///3. 定义一个Person表，有三个字段：Id,Name,Age,ClassId
///4. 定义一个Class类，有三个属性：ClassId,ClassName,ClassAddress，同时创建与之对应的table
///5. 定义一个PersonDB类，操作Person表
#endregion

namespace _017Dapper
{
    class Program
    {
        static void Main(string[] args)
        {
            //测试插入数据
            //InsertPerson();
            //InsertPersons();
            //InsertWithId();

            //更新数据
            //UpdateName();
            //UpdateAge();

            //删除数据
            //DeletePersonById();
            //DeletePersonsById();

            //查询
            //RetrievePersons();
            //RetrievePersonById();
            //RetrievePersonWithIn();
            //RetrievePersonWithLike();
            //RetrievePersonWithLikeAndIn();
            //RetrieveMultiQuery();
            RetrieveReturnAnonymousType();

            //QueryDataTable();
            //QueryDictionary();
            //QueryInt();
            //QueryWithJoin();

            //执行存成过程
            //ExecuteStoreProcedure();
            //ExecuteStoreProcedureWithParam();
            //ExecuteStoreProcedureWithDynamicParam();
            //ExecuteSqlWithDynamicParam();
            //ExecuteStoreProcedureInsert();
            //ExecuteStoreProcudureMoreThanOnce();
            Console.ReadKey();
        }



        //插入一条Person记录
        static void InsertPerson()
        {
            Person person = new Person() { Name = "Tom", Age = 20 };
            int effectNum = PersonDB.Insert(person);
            Console.WriteLine($"受影响行数{effectNum}");
        }

        //插入多条Person记录
        static void InsertPersons()
        {
            List<Person> persons = new List<Person>()
            {
                new Person() { Name = "Jerry", Age = 10 },
                new Person() { Name = "Bob", Age = 12 }
            };
            int effectNum = PersonDB.Insert(persons);
            Console.WriteLine($"受影响行数{effectNum}");
        }

        //插入返回自增iD
        static void InsertWithId()
        {
            Person person = new Person() { Name = "Bill", Age = 14 };
            int Id = PersonDB.InsertWithId(person);
            Console.WriteLine($"该条记录在数据库中的Id是{Id }");
        }

        //更新用户名
        static void UpdateName()
        {
            Person person = new Person() { Id = 1, Name = "汤姆" };
            int effectNum = PersonDB.Update(person);
            Console.WriteLine($"受影响行数{effectNum}");
        }

        //更新用户年龄
        static void UpdateAge()
        {
            List<Person> persons = new List<Person>()
           {
               new Person (){Id=2,Age=100},
               new Person (){Id=3,Age=100}
           };
            int effectNum = PersonDB.Update(persons);
            Console.WriteLine($"受影响行数{effectNum}");
        }

        //删除指定ID的person
        static void DeletePersonById()
        {
            Person person = new Person() { Id = 1 };
            int effectNum = PersonDB.Delete(person);
            Console.WriteLine($"删除了{effectNum}行数据！");
        }

        //根据ID批量删除Person记录
        static void DeletePersonsById()
        {
            List<Person> persons = new List<Person>()
            {
                new Person (){Id=2},
                new Person (){Id=3}
            };
            int effectNum = PersonDB.Delete(persons);
            Console.WriteLine($"删除了{effectNum}行数据！");
        }

        //无参查询
        static void RetrievePersons()
        {
            var persons = PersonDB.Retrieve();
            persons.ForEach(n => Console.WriteLine(n.Name));
        }

        //根据ID查询person
        static void RetrievePersonById()
        {
            Person person = new Person() { Id = 3 };
            Person personResult = PersonDB.Retrieve(person);
            Console.WriteLine(personResult.Name);
        }

        //sql中查询条件使用in
        static void RetrievePersonWithIn()
        {
            int[] argIds = { 4, 5 };
            List<Person> persons = PersonDB.RetrieveWithIn(argIds);
            persons.ForEach(n => Console.WriteLine(n.Name));
        }

        //sql中使用like模糊查询
        static void RetrievePersonWithLike()
        {
            string partName = "b";
            List<Person> persons = PersonDB.RetrieveWithLike(partName);
            persons.ForEach(n => Console.WriteLine(n.Name));
        }

        //sql中使用in和模糊查询
        static void RetrievePersonWithLikeAndIn()
        {
            List<Person> personList = PersonDB.RetrieveWithInAndLike(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, "b");
            personList.ForEach(n => Console.WriteLine(n.Name));
        }

        //sql多语句查询，即sql语句有多个返回
        static void RetrieveMultiQuery()
        {
            var queryResult = PersonDB.GetMultiQuery();
            Console.WriteLine("二元组的第一个分量");
            queryResult.Item1.ForEach(n => Console.WriteLine(n.Name + n.Age));
            Console.WriteLine("二元组的第二个分量");
            queryResult.Item2.ForEach(n => Console.WriteLine(n.Name + n.Age));
        }

        //查询结果为匿名类型
        static void RetrieveReturnAnonymousType()
        {
            string result = PersonDB.RetrieveReturnAnonymousType();
            Console.WriteLine(result);
        }

        //使用存储过程
        static void ExecuteStoreProcedure()
        {
            List<Person> person = PersonDB.ExecuteStoreProcedure();
            person.ForEach(n => Console.WriteLine(n.Name));
        }

        //使用带有参数的存储过程
        static void ExecuteStoreProcedureWithParam()
        {
            Person person = PersonDB.ExecuteStoreProcedureWithParam();
            Console.WriteLine(person.Name);
        }

        //使用动态参数执行存储过程
        static void ExecuteStoreProcedureWithDynamicParam()
        {
            Person person = PersonDB.ExecuteStoreProcedureWithDynamicParam();
            Console.WriteLine(person.Name + ":" + person.Age);
        }

        //使用动态参数执行sql
        static void ExecuteSqlWithDynamicParam()
        {
            Person person = PersonDB.ExecuteSqlWithDynamicParam();
            Console.WriteLine(person.Name+":"+person.Age);
        }

        //使用存储过程实现插入
        static void ExecuteStoreProcedureInsert()
        {
            int affectNum = PersonDB.ExecuteStoreProcedureInsert();
            Console.WriteLine($"插入:{affectNum == 1}");
        }

        //重复执行存储过程，插入多条数据
        static void ExecuteStoreProcudureMoreThanOnce()
        {
            List<Person> lists = new List<Person>() { new Person() { Name = "test", Age = 100, ClassId = "1" }, new Person() { Name = "test2", Age = 100, ClassId = "2" } };
            int affectNum = PersonDB.ExecuteStoreProcudureMoreThanOnce(lists);
            Console.WriteLine($"受影响行数：{affectNum}");
        }

        //使用Dapper查询返回Datatable
        static void QueryDataTable()
        {
            DataTable dtPerson = PersonDB.QueryReturnDataTable();
            foreach (DataRow dr in dtPerson.Rows)
            {
                Console.WriteLine(dr["Name"].ToString());
            }
        }

        //使用Dapper查询返回Dictionary
        static void QueryDictionary()
        {
            Dictionary<int, int> keyValuePairs = PersonDB.QueryReturnDictionary();
            Array.ForEach(keyValuePairs.ToArray(), n => Console.WriteLine($"Id:{n.Key},Age:{n.Value}"));
            //keyValuePairs.ToArray()的结果是KeyValuePair<int,int>[]
            //Dictionary可以看作是KeyValuePair类型的集合
        }

        //使用Dapper查询返回Int类型
        static void QueryInt()
        {
            Console.WriteLine($"table count={PersonDB.QueryReturnInt()}");
        }

        //连接查询
        static void QueryWithJoin()
        {
            List<PersonWithClass> listPersonWithClass = PersonDB.QuerywithJoin();
            listPersonWithClass.ForEach(item => Console.WriteLine(item.Name + " " + item.ClassName));
        }
    }
}
