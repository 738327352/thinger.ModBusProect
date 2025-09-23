using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thinger.Dal;
using thinger.Models;

namespace thinger.BAL
{
    public class ProjectService
    {



        public bool IsRepeatForInster(Projects projects) {

            
            String sql = "select count(1) from Projects where projectName=@projectName";

            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@projectName", projects.projectName)};



            if (SQLHelper.ExecuteScalar(sql, sp)!=null)
            {

                return false;

            }
            else { return true; }


        }
        
    }
}
