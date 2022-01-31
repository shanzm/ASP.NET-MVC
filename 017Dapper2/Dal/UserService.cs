using Dapper.Contrib.Extensions;
using System;
using System.Data;

namespace _017Dapper2
{
    public class UserService : BaseDAL<UserModel>
    {

        //说明：BaseDAL中没有的方法可以在这里自行定义，可以依赖Dapper.Extensions中对对象的操作

        /// <summary>
        /// 在事务中同时插入User和Department表
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        public Tuple<bool, string> InsertUserAndDepartment(UserModel userModel, DepartmentModel department)
        {
            using (IDbConnection dbConnection = Connection)//这个Connection对象来自其父类BaseDAL中，而BaseDAL中的连接对象来自DapperHelper
            {
                using (IDbTransaction transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        dbConnection.Insert(userModel, transaction);
                        dbConnection.Insert(department, transaction);
                        transaction.Commit();
                        return new Tuple<bool, string>(true, "success");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return new Tuple<bool, string>(false, ex.Message);
                    }
                }
            }
        }


        //说明：复杂的SQL查询可以在这里使用DapperHelper中封装的方法
    }
}
