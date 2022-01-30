using Dapper.Contrib.Extensions;
using System;
using System.Data;

namespace _017Dapper2
{
    public class UserService : BaseDAL<UserModel>
    {

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
    }
}
