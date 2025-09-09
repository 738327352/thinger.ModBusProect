using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace thinger.Dal
{
     class SQLHelper
    {
        
        private static string connString =ConfigurationManager.ConnectionStrings["conString1"].ToString().Trim();

        /// <summary>
        /// 执行insert ，update，deleate类型的SQL语句
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="paramArray">sql语句数组</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int ExecuteNonQuery(string cmdText, SqlParameter[] paramArray = null) {

            SqlConnection conn = new SqlConnection (connString);
            SqlCommand cmd = new SqlCommand(cmdText,conn);
            //添加其他sql语句
            if (paramArray != null) {
                cmd.Parameters.AddRange (paramArray);

            }
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                String errorMsg = $"{DateTime.Now}:执行单一查询出现错误";

                throw new Exception(errorMsg);
            }
            finally { 
                conn.Close(); 
            }
        
        
        
        
        }

        /// <summary>
        /// 执行单一结果查询
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="paramArray"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static object ExecuteScalar(string cmdText, SqlParameter[] paramArray = null)
        {

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            //添加其他sql语句
            if (paramArray != null)
            {
                cmd.Parameters.AddRange(paramArray);

            }
            try
            {
                conn.Open();
                return cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
        

                throw new Exception("方法执行异常"+ex.Message);
            }
            finally
            {
                conn.Close();
            }




        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="paramArray"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static SqlDataReader ExecuteReader(string cmdText, SqlParameter[] paramArray = null)
        {

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            //添加其他sql语句
            if (paramArray != null)
            {
                cmd.Parameters.AddRange(paramArray);
                

            }
            try
            {
                conn.Open();

                return cmd.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {


                throw new Exception("方法执行异常" + ex.Message);
            }
          




        }
        /// <summary>
        /// 返回单一表
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static DataSet GetDataSet(string cmdText, String tableName = null)
        {

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            //添加其他sql语句
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            try
            {
                conn.Open();
                if (tableName == null)
                {

                    da.Fill(ds);
                }
                else { 
                
                    da.Fill(ds, tableName);
                
                }

                    
                return ds;

            }
            catch (Exception ex)
            {


                throw new Exception("方法执行异常" + ex.Message);
            }
            finally
            {
                conn.Close();
            }




        }


        /// <summary>
        /// 返回多表
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static DataSet GetDataSet(Dictionary<string,string> dicTableAndSql)
        {

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            //添加其他sql语句
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                foreach (string tbName in dicTableAndSql.Keys)
                {
                    cmd.CommandText = dicTableAndSql[tbName];
                    da.Fill(ds,tbName);
                }


                return ds;

            }
            catch (Exception ex)
            {


                throw new Exception("方法执行异常" + ex.Message);
            }
            finally
            {
                conn.Close();
            }




        }
    }
}
