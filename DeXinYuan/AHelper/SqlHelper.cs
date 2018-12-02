using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using DapperExtensions;

namespace DeXinYuan
{
    public abstract class SqlHelper
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static readonly string _sqlConnectStr;

        static SqlHelper()
        {
            _sqlConnectStr = SqlConn.SqlContention;
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="sqlConnection">链接字符串</param>
        /// <returns>返回插入行数</returns>
        public static dynamic Insert<T>(T entity, string sqlConnection = null) where T : class
        {
            return Query(conn =>
            {
                dynamic result = conn.Insert(entity);
                return result;
            }, sqlConnection);
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        public static bool Update<T>(T entity, string sqlConnection = null) where T : class
        {
            return Query(conn =>
            {
                bool result = conn.Update(entity);
                return result;
            }, sqlConnection);
        }




        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        public static bool Delete<T>(T entity, string sqlConnection = null) where T : class
        {
            return Query(conn =>
            {
                bool result = conn.Delete(entity);
                return result;
            }, sqlConnection);
        }

        /// <summary>
        /// 调用数据库操作 返回指定结果T
        /// </summary>
        /// <param name="sql">需要执行的sql</param>
        /// <param name="parameter">参数</param>
        /// <param name="isTransaction">是否使用事务</param>
        /// <param name="sqlConnection">链接字符串(如果不使用默认链接字符串)</param>
        public static T ExecuteScalar<T>(StringBuilder sql, object parameter = null, bool isTransaction = false, string sqlConnection = null)
        {
            return ExecuteScalar<T>(sql.ToString(), parameter, isTransaction, sqlConnection);
        }


        /// <summary>
        /// 调用数据库操作 返回指定结果T
        /// </summary>
        /// <param name="sql">需要执行的sql</param>
        /// <param name="parameter">参数</param>
        /// <param name="isTransaction">是否使用事务</param>
        /// <param name="sqlConnection">链接字符串(如果不使用默认链接字符串)</param>
        public static T ExecuteScalar<T>(string sql, object parameter = null, bool isTransaction = false, string sqlConnection = null)
        {
            using (IDbConnection con = GetterDbConnection(sqlConnection))
            {
                if (isTransaction)
                {
                    con.Open();
                    using (IDbTransaction trans = con.BeginTransaction())
                    {
                        try
                        {
                            T res = con.ExecuteScalar<T>(sql, parameter);
                            trans.Commit();
                            return res;
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
                else
                {
                    T res = con.ExecuteScalar<T>(sql, parameter);
                    return res;
                }
            }
        }

        /// <summary>
        /// 调用数据库操作 返回影响行数
        /// </summary>
        /// <param name="sql">需要执行的sql</param>
        /// <param name="parameter">参数</param>
        /// <param name="isTransaction">是否使用事务</param>
        /// <param name="sqlConnection">链接字符串(如果不使用默认链接字符串)</param>
        public static int Execute(StringBuilder sql, object parameter = null, bool isTransaction = false, string sqlConnection = null)
        {
            return Execute(sql.ToString(), parameter, isTransaction, sqlConnection);
        }

        /// <summary>
        /// 调用数据库操作 返回影响行数
        /// </summary>
        /// <param name="sql">需要执行的sql</param>
        /// <param name="parameter">参数</param>
        /// <param name="isTransaction">是否使用事务</param>
        /// <param name="sqlConnection">链接字符串(如果不使用默认链接字符串)</param>
        public static int Execute(string sql, object parameter = null, bool isTransaction = false, string sqlConnection = null)
        {
            using (IDbConnection con = GetterDbConnection(sqlConnection))
            {
                if (isTransaction)
                {
                    con.Open();
                    using (IDbTransaction trans = con.BeginTransaction())
                    {
                        try
                        {
                            int res = con.Execute(sql, parameter, trans);
                            trans.Commit();
                            return res;
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
                else
                {
                    int res = con.Execute(sql, parameter);
                    return res;
                }
            }
        }

        /// <summary>
        /// 调用数据库操作 返回T
        /// </summary>
        /// <typeparam name="T">需要返回的对象</typeparam>
        /// <param name="sql">需要执行的sql</param>
        /// <param name="parameter">参数</param>
        /// <param name="sqlConnection">链接字符串(如果不使用默认链接字符串)</param>
        /// <returns>返回结果T</returns>
        public static IEnumerable<T> Query<T>(StringBuilder sql, object parameter = null, string sqlConnection = null)
        {
            return Query<T>(sql.ToString(), parameter, sqlConnection);
        }

        /// <summary>
        /// 调用数据库操作 返回T
        /// </summary>
        /// <typeparam name="T">需要返回的对象</typeparam>
        /// <param name="sql">需要执行的sql</param>
        /// <param name="parameter">参数</param>
        /// <param name="sqlConnection">链接字符串(如果不使用默认链接字符串)</param>
        /// <returns>返回结果T</returns>
        public static IEnumerable<T> Query<T>(string sql, object parameter = null, string sqlConnection = null)
        {
            using (IDbConnection con = GetterDbConnection(sqlConnection))
            {
                IEnumerable<T> result = con.Query<T>(sql, parameter);
                return result;
            }
        }

        public static DataTable ExecuteReader(string sql, object parameter = null, string sqlConnection = null)
        {
            using (IDbConnection conn = GetterDbConnection(sqlConnection))
            {
                var list = conn.ExecuteReader(sql, parameter);
                DataTable dt = new DataTable();
                dt.Load(list);
                return dt;
            }
        }


        /// <summary>
        /// 调用数据库操作 返回T
        /// </summary>
        /// <typeparam name="T">需要返回的对象</typeparam>
        /// <param name="sql">需要执行的sql</param>
        /// <param name="parameter">参数</param>
        /// <param name="sqlConnection">链接字符串(如果不使用默认链接字符串)</param>
        /// <returns>返回结果T</returns>
        public static IEnumerable<T> Query<T>(string sql, object parameter, CommandType? commandType, string sqlConnection = null)
        {
            using (IDbConnection con = GetterDbConnection(sqlConnection))
            {
                IEnumerable<T> result = con.Query<T>(sql, parameter, null, true, null, commandType);
                return result;
            }
        }

        /// <summary>
        /// 设置链接字符串并打开连接，可进行替换默认链接字符串
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        protected static IDbConnection GetterDbConnection(string sqlConnection)
        {
            sqlConnection = sqlConnection ?? _sqlConnectStr;
            if (sqlConnection == null)
            {
                throw new ArgumentException("链接字符串为空");
            }
            //IDbConnection conn = new OleDbConnection(sqlConnection);
            IDbConnection con = new SqlConnection(sqlConnection); //方便切换数据库
            return con;
        }


        /// <summary>
        /// 调用数据库操作 无参数返回
        /// </summary>
        /// <param name="action">需要执行的业务逻辑</param>
        /// <param name="sqlConnection">链接字符串(如果不使用默认链接字符串)</param>
        public static void Execute(Action<IDbConnection> action, string sqlConnection = null)
        {
            using (IDbConnection con = GetterDbConnection(sqlConnection))
            {
                action(con);
            }
        }


        /// <summary>
        /// 调用数据库操作 返回 T
        /// </summary>
        /// <typeparam name="T">需要返回的对象</typeparam>
        /// <param name="func">需要执行的业务逻辑</param>
        /// <param name="sqlConnection">链接字符串(如果不使用默认链接字符串)</param>
        /// <returns>返回结果T</returns>
        public static T Query<T>(Func<IDbConnection, T> func, string sqlConnection = null)
        {
            using (IDbConnection con = GetterDbConnection(sqlConnection))
            {
                T t = func(con);
                return t;
            }
        }


        /// <summary>
        /// 调用数据库操作 返回 T
        /// </summary>
        /// <typeparam name="T">需要返回的对象</typeparam>
        /// <param name="func">需要执行的业务逻辑</param>
        /// <param name="sqlConnection">链接字符串(如果不使用默认链接字符串)</param>
        /// <returns>返回结果T</returns>
        [Obsolete("和上边的重复了")]
        public static T QueryMultiple<T>(Func<IDbConnection, T> func, string sqlConnection = null)
        {
            using (IDbConnection con = GetterDbConnection(sqlConnection))
            {
                T t = func(con);
                return t;
            }
        }


        /// <summary>
        /// 事务调用数据库操作 无参数返回
        /// </summary>
        /// <param name="action">需要执行的业务逻辑</param>
        /// <param name="sqlConnection">链接字符串(如果不使用默认链接字符串)</param>
        public static void Execute(Action<IDbConnection, IDbTransaction> action, string sqlConnection = null)
        {
            using (IDbConnection con = GetterDbConnection(sqlConnection))
            {
                con.Open();
                using (IDbTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        action(con, trans);

                        if (trans != null && trans.Connection != null)
                        {
                            trans.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (trans != null && trans.Connection != null)
                        {
                            trans.Rollback();
                        }

                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 事务调用数据库操作返回T
        /// </summary>
        /// <typeparam name="T">需要返回的对象</typeparam>
        /// <param name="func">需要执行的业务逻辑</param>
        /// <param name="sqlConnection">链接字符串(如果不使用默认链接字符串)</param>
        /// <returns>返回结果T</returns>
        public static T Execute<T>(Func<IDbConnection, IDbTransaction, T> func, string sqlConnection = null)
        {
            using (IDbConnection con = GetterDbConnection(sqlConnection))
            {
                con.Open();
                using (IDbTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        T t = func(con, trans);
                        if (trans != null && trans.Connection != null)
                        {
                            trans.Commit();
                        }
                        return t;
                    }
                    catch (Exception ex)
                    {
                        if (trans != null && trans.Connection != null)
                        {
                            trans.Rollback();
                        }

                        throw;
                    }
                }
            }
        }
    }
}