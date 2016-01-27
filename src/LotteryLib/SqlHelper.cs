using System;
using System.Data;
using System.Data.SqlClient;

namespace LotteryLib
{
    public class SqlHelper
    {
        private string _conStr;

        public string conStr
        {
            get { return _conStr; }
            set { _conStr = value; }
        }

        /// <summary>
        ///     运行sql  返回受影响的行数
        /// </summary>
        /// <param name="sql">select * from User</param>
        /// <returns></returns>
        public int ExceSql(string sql)
        {
            var conn = new SqlConnection(_conStr);
            try
            {
                var cmd = new SqlCommand(sql, conn);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                var count = cmd.ExecuteNonQuery();
                return count;
            }
            catch (Exception exp)
            {
                throw exp;
            }

            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        ///     带参数的sql语句执行
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras">参数列表</param>
        /// <returns></returns>
        public int ExceSql(string sql, SqlParameter[] paras)
        {
            var conn = new SqlConnection(_conStr);
            try
            {
                var cmd = new SqlCommand(sql, conn);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                if (paras != null && paras.Length > 0)
                {
                    foreach (var para in paras)
                    {
                        cmd.Parameters.Add(para);
                    }
                }
                var count = cmd.ExecuteNonQuery();
                return count;
            }
            catch (Exception exp)
            {
                throw exp;
            }

            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        ///     用DataAdapter填充到一个数据集集合
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet ExecReturnDataSet(string sql)
        {
            var conn = new SqlConnection(_conStr);
            try
            {
                var adap = new SqlDataAdapter(sql, conn); //用一个DataAdapter
                var ds = new DataSet();
                adap.Fill(ds); //填充DataSet
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///     带参数的sql运行 返回一个数据集集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet ExecReturnDataSet(string sql, SqlParameter[] paras, CommandType type)
        {
            var conn = new SqlConnection(_conStr);
            try
            {
                var adap = new SqlDataAdapter(sql, conn); //用一个DataAdapter
                if (paras != null && paras.Length > 0)
                {
                    foreach (var p in paras)
                    {
                        adap.SelectCommand.Parameters.Add(p);
                    }
                }
                adap.SelectCommand.CommandType = type; //设定当前执行的sql类型

                var ds = new DataSet();
                adap.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///     基于sql命令行，返回阅读器的对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecSqlReturnDataReader(string sql)
        {
            var conn = new SqlConnection(_conStr);
            try
            {
                var cmd = new SqlCommand(sql, conn);

                if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                {
                    conn.Open(); //打开数据库
                }

                var sqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return sqlDataReader;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
           

        }

        /// <summary>
        ///     基于sql命令行，返回阅读器的对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecSqlReturnDataReader(string sql, SqlParameter[] paras, CommandType type)
        {
            var conn = new SqlConnection(_conStr);

            try
            {
                var cmd = new SqlCommand(sql, conn);

                if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                {
                    conn.Open(); //打开数据库
                }
                if (paras != null && paras.Length > 0)
                {
                    foreach (var p in paras)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                cmd.CommandType = type; //设定当前执行的sql类型
                var sqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection); //关闭reader的时候同时关闭的数据

                return sqlDataReader;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
           

        }
    }
}