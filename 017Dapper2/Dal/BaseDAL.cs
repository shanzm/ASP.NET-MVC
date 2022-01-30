using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace _017Dapper2
{
    /// <summary>
    /// 数据库访问基类
    /// </summary>
    /// <typeparam name="T">实体类类型</typeparam>
    public partial class BaseDAL<T> where T : class
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        protected IDbConnection Connection
        {
            get
            {
                //创建单一实例
                DapperHelper.GetInstance();
                return DapperHelper.OpenCurrentDbConnection();
            }
        }

        /// <summary>
        /// 对象的表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 主键属性对象
        /// </summary>
        public PropertyInfo PrimaryKey { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseDAL()
        {
            TableName = EntityHelper.GetTableName<T>();
            PrimaryKey = EntityHelper.GetPrimaryKey<T>();
        }


        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.GetAll<T>();
            }
        }

        /// <summary>
        /// 查询数据库,返回指定ID的对象
        /// </summary>
        /// <param name="id">主键的值</param>
        /// <returns></returns>
        public T FindByID(object id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Get<T>(id);
            }
        }

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public bool Insert(T info)
        {
            bool result = false;
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Insert(info);
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 插入指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        public bool Insert(IEnumerable<T> list)
        {
            bool result = false;
            using (IDbConnection dbConnection = Connection)
            {
                result = dbConnection.Insert(list) > 0;
            }
            return result;
        }

        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public bool Update(T info)
        {
            bool result = false;
            using (IDbConnection dbConnection = Connection)
            {
                result = dbConnection.Update(info);
            }
            return result;
        }

        /// <summary>
        /// 更新指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        public bool Update(IEnumerable<T> list)
        {
            bool result = false;
            using (IDbConnection dbConnection = Connection)
            {
                result = dbConnection.Update(list);
            }
            return result;
        }

        /// <summary>
        /// 从数据库中删除指定对象
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public bool Delete(T info)
        {
            bool result = false;
            using (IDbConnection dbConnection = Connection)
            {
                result = dbConnection.Delete(info);
            }
            return result;
        }

        /// <summary>
        /// 从数据库中删除指定对象集合
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        public bool Delete(IEnumerable<T> list)
        {
            bool result = false;
            using (IDbConnection dbConnection = Connection)
            {
                result = dbConnection.Delete(list);
            }
            return result;
        }

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象
        /// </summary>
        /// <param name="id">对象的ID</param>
        /// <returns></returns>
        public bool Delete(object id)
        {
            bool result = false;
            using (IDbConnection dbConnection = Connection)
            {
                string query = string.Format("DELETE FROM {0} WHERE {1} = @id", TableName, PrimaryKey.Name);
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);

                result = dbConnection.Execute(query, parameters) > 0;
            }
            return result;
        }

        /// <summary>
        /// 从数据库中删除所有对象
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll()
        {
            bool result = false;
            using (IDbConnection dbConnection = Connection)
            {
                result = dbConnection.DeleteAll<T>();
            }
            return result;
        }

    }

    /// <summary>
    /// 获取对象的特性标签
    /// </summary>
    public static class EntityHelper
    {
        /// <summary>
        /// 获取实体代表的表名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetTableName<T>() where T : class
        {
            object[] tablename = typeof(T).GetCustomAttributes(typeof(TableAttribute), true);
            return ((TableAttribute)tablename[0]).Name;
        }

        //public static string GetTableName(Type entityType)
        //{
        //    try
        //    {
        //        var tablename = entityType.GetCustomAttributes(typeof(TableAttribute), true);
        //        return ((TableAttribute)tablename[0]).Name;
        //    }
        //    catch
        //    {
        //        throw new Exception("没有配置Table特性！");
        //    }

        //}

        /// <summary>
        /// 获取实体主键名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PropertyInfo GetPrimaryKey<T>()
        {
            PropertyInfo[] pri = typeof(T).GetProperties();
            foreach (PropertyInfo item in pri)
            {
                object[] pris = item.GetCustomAttributes(typeof(KeyAttribute), true);
                if (pris.Any())
                {
                    return item;
                }
            }
            return null;
        }
    }

}
