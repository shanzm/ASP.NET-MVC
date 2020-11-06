using _017Dapper.DAL;
using _017Dapper.Model;
using System;
using System.Collections.Generic;


#region 说明
///0. 参考：https://mp.weixin.qq.com/s?__biz=MzI1NjEyODg0OQ==&mid=2247492961&idx=1&sn=d581eb31d47f231c5c6c338983912ae0&chksm=ea29c75fdd5e4e49b76891e8d8dd2669a939d249af3a31f8cf110d9d45fdb44d61d696c3e9b3&mpshare=1&scene=24&srcid=1011KwIW7YgGLzNMZH0iCm8a&sharer_sharetime=1602402592887&sharer_shareid=fa5b26e50d028927a27a58e92114971d&ascene=14&devicetype=android-28&version=27001355&nettype=WIFI&abtest_cookie=AAACAA%3D%3D&lang=zh_CN&exportkey=AUTxvE3%2F%2BuXaaXKmTSOSrAE%3D&pass_ticket=zvY0ksut6badPUenFfDiuHjo9WU6disyTJ%2FNROqgAZu%2FJucR3mVybBFTpwBEAoxe&wx_header=0
///1. 安装dapper:nuget>install-package dapper
///2. 定义一个Person类，有三个属性：Id,Name,Age
///3. 定义一个Person表，有三个字段：Id,Name,Age
///4. 定义一个PersonDB类，操作Person表
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
            RetrievePersonById();


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
               new Person (){Id=2},
               new Person (){Id=3}
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
            Person personResult= PersonDB.Retrieve(person);
            Console.WriteLine(personResult.Name);
        }
    }
}
