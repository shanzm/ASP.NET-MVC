﻿using _017Dapper.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace _017Dapper.DAL
{
    public class PersonDB
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ToString();


        /// <summary>
        /// 插入一个Person记录
        /// </summary>
        /// <param name="person"></param>
        /// <returns>受影响行数</returns>
        public static int Insert(Person person)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                //Dapper中的Execute执行的方式返回受影响的行数
                return connection.Execute("insert into Person(Name,Age) values (@Name,@Age)", person);
            }
        }


        /// <summary>
        /// 批量插入Person记录
        /// </summary>
        /// <param name="persons">Person集合</param>
        /// <returns>受影响行数</returns>
        public static int Insert(List<Person> persons)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Execute("insert into Person(Name,Age) values(@Name,@Age)", persons);
            }
        }


        /// <summary>
        /// 插入一条Person记录返回自增ID
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public static int InsertWithId(Person person)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                //Dapper中的QueryFirstOrDefault执行查询语句
                return connection.QueryFirstOrDefault<int>("insert into Person (Name,Age) values(@Name,@Age) select cast(@@IDENTITY as int)", person);
            }
        }


        /// <summary>
        /// 删除一条Person记录
        /// </summary>
        /// <param name="person"></param>
        /// <returns>受影响行数</returns>
        public static int Delete(Person person)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Execute("delete from Person where Id=@Id", person);
            }
        }


        /// <summary>
        /// 批量删除Person记录
        /// </summary>
        /// <param name="persons"></param>
        public static int Delete(List<Person> persons)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Execute("delete from Person where Id=@Id", persons);
            }
        }


        /// <summary>
        /// 根据ID更新Person的Name
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public static int Update(Person person)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Execute("update Person set Name=@Name where Id=@Id", person);
            }
        }


        /// <summary>
        /// 根据ID批量更新用户年龄
        /// </summary>
        /// <param name="persons"></param>
        /// <returns></returns>
        public static int Update(List<Person> persons)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Execute("update Person set Age=Age+1 where Id=@Id", persons);
            }
        }


        /// <summary>
        /// 无参数查询
        /// </summary>
        /// <returns></returns>
        public static List<Person> Retrieve()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Person>("select * from Person").ToList();
            }
        }


        /// <summary>
        /// 根据person id 查询person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public static Person Retrieve(Person person)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Person>("select * from Person where Id=@Id", person).FirstOrDefault();
            }
        }


        /// <summary>
        /// 查询条件中使用IN
        /// </summary>
        /// <returns></returns>
        public static List<Person> RetrieveWithIn(int[] argIds)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string sql = "select * from Person where Id in @Ids";
                return connection.Query<Person>(sql, new { Ids = argIds }).ToList();

            }
        }


        /// <summary>
        /// 多语句操作：sql中多个查询结果集
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Tuple<List<Person>, List<Person>> GetMultiQuery()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string sql = "select * from Person where Age<14;select * from Person where Age=15";
                var queryResult = connection.QueryMultiple(sql);
                List<Person> persons1 = queryResult.Read<Person>().ToList();//接收第一个select查询的结果
                List<Person> persons2 = queryResult.Read<Person>().ToList();//接收第二个select查询的结果


                //使用元祖（C#6.0语法）实现一个函数返回多个返回值
                Tuple<List<Person>, List<Person>> tupleListPerson = new Tuple<List<Person>, List<Person>>(persons1, persons2);
                return tupleListPerson;
            }
        }


        #region 使用Dapper查询返回其他一般类型
        /// <summary>
        /// 使用Dapper查询结果为DataTable类型
        /// </summary>
        /// <returns></returns>
        public static DataTable QueryReturnDataTable()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string sql = "select * from Person";
                DataTable dtPerson = new DataTable();
                IDataReader dataReader = connection.ExecuteReader(sql);
                dtPerson.Load(dataReader);

                return dtPerson;
            }
        }


        /// <summary>
        /// 使用Dapper查询结果为Dictionary类型
        /// </summary>
        public static Dictionary<int, int> QueryReturnDictionary()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string sql = "select Id,Age from Person";
                Dictionary<int, int> dicPerson = connection.Query(sql).ToDictionary(item => (int)item.Id, item => (int)item.Age);//注意这里使用id作为Dictionary的key,Dictionary的key不能重复
                return dicPerson;
            }
        }


        /// <summary>
        /// 标量查询：使用Dapper查询结果为Dictionary类型
        /// </summary>
        /// <returns></returns>
        public static int QueryReturnInt()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string sql = "select count(*) from Person";
                int count = connection.Query<int>(sql).FirstOrDefault();
                return count;
            }
        }
        #endregion

        /// <summary>
        /// 连接查询
        /// </summary>
        public static List<PersonWithClass> QuerywithJoin()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string sql = "select * from Person left join Class on Person.ClassId=Class.ClassId";
                //这个PersonWithClass类就是根据查询结果中的字段而创建的
                return connection.Query<PersonWithClass>(sql).ToList();
            }
        }


        /// <summary>
        /// 连接查询
        /// </summary>
        /// <returns></returns>
        public static List<PersonWithClass2> QueryWithJoin()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string sql = "select * from Person left join Class on Person.ClassId=Class.ClassId";
               // return connection.Query<PersonWithClass2, SchoolClass, PersonWithClass2>(sql, (personWithClass2, schoolClass) => { personWithClass2.SchoolClass = schoolClass; return personWithClass2; });
            }
        }
    }
}