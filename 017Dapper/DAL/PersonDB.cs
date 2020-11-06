using _017Dapper.Model;
using Dapper;
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
    }
}
