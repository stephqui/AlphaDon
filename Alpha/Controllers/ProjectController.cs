using Alpha.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            Dal dal = new Dal();
            List<Project> projects = dal.GetAllProjects();
            return View(projects);     
        }

        public IActionResult MyProject()
        {
            Dal dal = new Dal();
            Project project = dal.GetMyProject(Convert.ToInt32(User.Identity.Name));
            return View(project);
        }

        //*****************************************************************************************
        public ActionResult CreateProject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProject(Project project)
        {
            Dal dal = new Dal();
            if (dal.ProjectExiste(project.ProjectName))
            {
                ModelState.AddModelError("Nom", "Ce nom du project existe déjà");
                return View(project);
            }
            if (!ModelState.IsValid)
                return View(project);
            dal.CreateProject(project.ProjectName, project.Description, project.Category, project.StartDate, project.EndDate,
                project.Place, project.Area, project.Limit, project.ProfileId, project.Id, project.CollectId);
            
            return Redirect("/Project/Index");
        }
    }
}
