using _017Dapper.Model;
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


        #region Dapper 中的Excuse

        ///Dapper.NET是一个简单的ORM，专门从SQL查询结果中快速生成对象
        ///Dapper.NET是通过对IDbConnection接口进行扩展。
        ///关于Excuse()函数，可以执行 1.存储过程，2.Insert语句，3.Update语句，4.Delete语句
        ///Excuse()函数有五个参数
        ///第一个参数：sql:要执行的sql语句
        ///第二个参数：param:sql语句中的参数，默认为null
        ///第三个参数：transaction：是否需要使用事务，默认为null
        ///第四个参数：commandTimeout:sql执行超时时间，默认为null
        ///第五个参数：commandType:sql类型，是sql语句还是存储过程，默认为null

        /// <summary>
        /// 插入一个Person记录
        /// </summary>
        /// <param name="person"></param>
        /// <returns>受影响行数</returns>
        public static int Insert(Person person)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                //connection.Open();//我看网上一些博客中是这样写的，但是其实是没有必要的，使用dapper不需要考虑conn是否连接，在执行dapper时自行判断 open状态，如果没有打开它会自己打开
                //详细可以看SqlMappe.cs中的2796行“ if (wasClosed) cnn.Open();”
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
                return connection.Execute("update Person set Age=@Age where Id=@Id", persons);
            }
        }

        #endregion


        #region Dapper 中的Query

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
        /// 使用like模糊查询
        /// </summary>
        /// <param name="partName"></param>
        /// <returns></returns>
        public static List<Person> RetrieveWithLike(string partName)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Person>("select * from person where name like @name", new { name = $"%{partName}%" }).ToList();
            }
        }


        /// <summary>
        /// 同时使用in和模糊查询
        /// </summary>
        /// <param name="argIds"></param>
        /// <param name="partName"></param>
        /// <returns></returns>
        public static List<Person> RetrieveWithInAndLike(int[] argIds, string partName)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string sql = "select * from person where Id in @Ids and Name like @Name";
                List<Person> personList = connection.Query<Person>(sql, new { Ids = argIds, Name = "%" + partName + "%" }).ToList();
                return personList;
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

        #endregion


        #region Dapper 中执行存储过程


        /// <summary>
        ///  执行无参数的存储过程
        ///  获取Person表中数据
        ///  存储过程：
        ///  ALTER PROCEDURE [dbo].[pro_GetPerson]
        ///  AS
        ///  BEGIN
        ///  SELECT* FROM Person
        ///  END
        /// </summary>
        /// <returns></returns>
        public static List<Person> ExecuteStoreProcedure()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string pro = "pro_GetPerson";
                return connection.Query<Person>(pro, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        /// <summary>
        /// 执行有参数的存储过程 获取Person表中指定Id的用户
        /// 存储过程如下：
        /// Create PROCEDURE pro_GetPersonWithId
        ///         @Id INT
        /// AS
        /// BEGIN
        ///     SELECT* FROM Person WHERE Id=@Id;
        /// END
        /// </summary>
        /// <returns></returns>
        public static Person ExecuteStoreProcedureWithParam()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                //传参方式1：使用DynamicParameters
                //string pro = "pro_GetPersonWithId";
                //DynamicParameters dynamicParameters = new DynamicParameters();//DynamicParameters是Dapper中定义的用于参数化查询的对象
                //dynamicParameters.Add("@Id", 2);
                //return connection.Query<Person>(pro, dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                //传参方式2：使用匿名对象
                //注意这里不可以使用Person对象作为参数 ,因为我们需要的参数就只需要Id，但是Person中还包含其他的属性
                //若是存储过程只是使用Person的一个属性做参数，则不能使用Person对象做参数
                //这一点和使用Dapper执行sql语句不一样，使用Dapper执行sql语句，语句中的参数可以可以使用Person对象的部分属性
                //可以使用匿名对象做参数

                string pro = "pro_GetPersonWithId";
                return connection.Query<Person>(pro, new { id = 2 }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        /// <summary>
        /// 执行有参存储过程，插入记录
        /// Create PROCEDURE pro_InsertPerson
        /// @Name nvarchar(50),
        /// @Age int,
        /// @ClassId nvarchar(50)=null
        /// AS
        /// BEGIN 
        /// 	insert into Person(Name, Age, ClassId) values(@Name, @Age, @ClassId)
        /// END
        /// </summary>
        /// <returns></returns>
        public static int ExecuteStoreProcedureInsert()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string pro = "pro_InsertPerson";
                return connection.Execute(pro, new { Name = "test", Age = 10 }, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// 重复执行存储过程，插入多条记录
        /// </summary>
        /// <returns></returns>
        public static int ExecuteStoreProcudureMoreThanOnce(List<Person> persons)
        {
            //这里我考虑过，使用匿名对象数组List<object>做为参数，但是在将匿名数组传入后，读取每一个匿名对象又比较麻烦，使用匿名对象做参数并不友好
            //其次，主要是想一想使用ORM的目的，就是应该为其定义一个类。

            //我定义的对象Person中有Id字段，但是这里执行存储过程插入记录中Id在数据库中是自增字段，所以不需要赋值
            //即在执行这个存储过程中我不需要Id字段，所以无法直接使用Person对象做参数，
            //这里我又定义了一个没有Id字段的Person2类，使用其作为参数类型
            //我认为这是有问题！但是还没想到其他的解决方法。

            //解决方法，依旧是使用Person类型做参数，在本函数中由List<Person>创建需要的匿名对象集合
            var query = persons.Select(person => new { Name = person.Name, Age = person.Age }).ToList();
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string pro = "pro_InsertPerson";
                return connection.Execute(pro, query, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion


        #region Dapper 查询返回其他一般类型

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
        //public static List<PersonWithClass2> QueryWithJoin()
        //{
        //    using (IDbConnection connection = new SqlConnection(connectionString))
        //    {
        //        string sql = "select * from Person left join Class on Person.ClassId=Class.ClassId";
        //       // return connection.Query<PersonWithClass2, SchoolClass, PersonWithClass2>(sql, (personWithClass2, schoolClass) => { personWithClass2.SchoolClass = schoolClass; return personWithClass2; });
        //    }
        //}
    }
}
