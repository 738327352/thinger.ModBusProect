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



        public bool IsRepeatForInster(Projects projects)
        {


            String sql = "select projectName from Projects where projectName=@projectName";

            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@projectName", projects.projectName)};



            if (SQLHelper.ExecuteScalar(sql, sp) != null)
            {
                return true;
            }
            else { return false; }


        }

        public int Insert(Projects projects)
        {
            String sql = "insert into Projects (projectName) values(@projectName);select @@Identity";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@projectName", projects.projectName)};
      
        return SQLHelper.ExecuteNonQuery(sql, sp);
        
        
        }



        public List<Projects> GetAllProjects() { 
        
        String sql ="select ProjectId,projectName from Projects";
            SqlParameter[] sp = null;
            SqlDataReader reader = SQLHelper.ExecuteReader(sql, sp);
            List<Projects> list = new List<Projects>();
            while (reader.Read())
            {
                Projects p = new Projects();
                p.ProjectId = (int)reader["ProjectId"];
                p.projectName = reader["projectName"].ToString();
                list.Add(p);

            }
            reader.Close();

            return list;


        }


        public int Update(Projects projects) {


            String slq = "update Projects set projectName=@ProjectName where ProjectId =@ProjectId";

            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@ProjectName",projects.projectName),
                new SqlParameter("@ProjectId",projects.ProjectId)

            };
            try
            {
               return  SQLHelper.ExecuteNonQuery(slq, sp);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        


        }

    }
}
