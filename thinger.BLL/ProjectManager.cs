using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thinger.BAL;
using thinger.Models;

namespace thinger.BLL
{
    public class ProjectManager
    {
        ProjectService projectService = new ProjectService();

        public bool IsRepeatForInster(Projects projects)
        {

            return projectService.IsRepeatForInster(projects);


        }

        public int Insert(Projects projects)
        {

            return projectService.Insert(projects);


        }


        public int Update(Projects projects)
        {

            return projectService.Update(projects);


        }


        public List<Projects> GetAllProjects()
        {
            return projectService.GetAllProjects();

        }
    }
}
