using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace _017Dapper2.Dal
{
    /// <summary>
    /// DapperSqlHelper是对Dapper的API的封装，简化每次的使用连接对象的方式
    /// 该类和Dapper.Contrib.Extensions无关
    /// 该类依赖于ConnectionInstance提供的连接对象
    /// </summary>
    public static class DapperSqlHelper
    {
        private static IDbConnection Db
        {
            get
            {
                ConnectionInstance.GetInstance();
                return ConnectionInstance.OpenCurrentDbConnection();
            }
        }

        #region 执行Select语句
        public static T QueryFirstOrDefault<T>(string sql, object param = null)
        {
            return Db.QueryFirstOrDefault<T>(sql, param);
        }

        public static Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null)
        {
            return Db.QueryFirstOrDefaultAsync<T>(sql, param);
        }

        public static IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Db.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        public static Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Db.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }
        #endregion

        #region 执行Update，Delete，Insert语句

        public static int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Db.Execute(sql, param, transaction, commandTimeout, commandType);
        }

        public static Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Db.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }
        #endregion

        #region 获取查询结果集的第一个单元格

        public static object ExecuteScalar(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Db.ExecuteScalar(sql, param, transaction, commandTimeout, commandType);
        }

        public static object ExecuteScalarAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Db.ExecuteScalarAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public static T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Db.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public static Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Db.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }
        #endregion

        #region 执行存储过程

        //执行查询存储过程
        public static IEnumerable<T> ProQuery<T>(string procName, object param)
        {
            return Db.Query<T>(procName, param, null, true, null, CommandType.StoredProcedure);
        }

        //执行非查询存储过程
        public static int ProExecute(string procName, object param)
        {
            return Db.Execute(procName, param, null, null, CommandType.StoredProcedure);
        }
        #endregion

        #region 查询返回多个结果集
        /// <summary>
        /// 同时查询多张表数据（高级查询）
        /// "select *from table1;select *from table2";
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static GridReader QueryMultiple(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Db.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
        }
        public static Task<GridReader> QueryMultipleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Db.QueryMultipleAsync(sql, param, transaction, commandTimeout, commandType);
        }

        #endregion

        #region 事务中执行多个SQL语句
        /// <summary>
        /// 同一事务中执行多条SQL语句
        /// </summary>
        /// <param name="argSql">多条SQL</param>
        /// <param name="param">param</param>
        /// <returns></returns>
        public static int ExecuteTransaction(string[] argSql)
        {
            using (IDbConnection con = Db)
            {
                using (IDbTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        int result = 0;
                        foreach (string sql in argSql)
                        {
                            result += con.Execute(sql, null, transaction);
                        }

                        transaction.Commit();
                        return result;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// 同一事务中执行多条带参数SQL语句
        ///示例：
        ///dic.Add("Insert into Users values (@UserName, @Email, @Address)",
        ///        new { UserName = "dapperUser", Email = "email@xxx.com", Address = "贝克街221号" });
        /// </summary>
        /// <param name="Key">多条SQL</param>
        /// <param name="Value">param</param>
        /// <returns></returns>
        public static int ExecuteTransaction(Dictionary<string, object> dic)
        {
            using (IDbConnection con = Db)
            {
                using (IDbTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        int result = 0;
                        foreach (KeyValuePair<string, object> sql in dic)
                        {
                            result += con.Execute(sql.Key, sql.Value, transaction);
                        }

                        transaction.Commit();
                        return result;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        } 
        #endregion
    }
}
