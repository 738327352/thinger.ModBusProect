using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using thinger.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
namespace thinger.Dal

{
    public class SysAdminsService
    {

        /// <summary>
        /// 查询数据库中用户输入的账号密码时候存在，如果存在返回账号ID和名称
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public SysAdmins AdminLogin(SysAdmins admin) {

            string sql = "select SysAdminId,AdminName from SysAdmins where SysAccount =@SysAccount and AdminPwd=@Password";
            SqlParameter[] sp = new SqlParameter[] {

                new SqlParameter("@SysAccount",admin.SysAccount),
                new SqlParameter("@Password",admin.AdminPwd)


            };
        
        SqlDataReader sqlDataReader=SQLHelper.ExecuteReader(sql, sp);

            if (sqlDataReader.Read())
            {

                admin.SysAdminnsId = Convert.ToInt32(sqlDataReader["SysAdminId"]);
                admin.AdminName = sqlDataReader["AdminName"].ToString();



            }
            else {


                admin = null;
            }


            sqlDataReader.Close();
            return admin;


        }

        /// <summary>
        /// 将用户保存在文件夹中
        /// </summary>
        /// <param name="admin"></param>
        public void SaveAdmin(SysAdmins admin) {

            FileStream fileStream = new FileStream("sysAdmins.obj",FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fileStream, admin);
            fileStream.Close();
        
        
        }

        /// <summary>
        /// 读取密码
        /// </summary>
        /// <returns></returns>
        public SysAdmins ReadPwd() {

            if (!File.Exists("sysAdmins.obj")) return null;

            FileStream fileStream = new FileStream("sysAdmins.obj", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
           SysAdmins admin = (SysAdmins)bf.Deserialize(fileStream);
            fileStream.Close();
            return admin;
           
        }
        /// <summary>
        /// 清除密码
        /// </summary>
        public void DeleatePwd() {
            if (File.Exists("sysAdmins.obj")) {
                File.Delete("sysAdmins.obj");
            
            }

        
        
        }

    }


   








}

