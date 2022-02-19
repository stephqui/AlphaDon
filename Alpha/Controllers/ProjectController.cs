using Alpha.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Alpha.Controllers
{
    public class ProjectController : Controller
    {
        /*public IActionResult Index(int value, string test)*/
        public IActionResult Index()
        {
            Dal dal = new Dal();
            List<Project> projects = dal.GetAllProjects();
            return View(projects);     
        }
        public IActionResult ManageProjects()
        {
            Dal dal = new Dal();
            List<Project> projects = dal.GetAllProjects();
            return View(projects);
        }

        public IActionResult FullSingleProject(int projectId)
        {
            Dal dal = new Dal();
            Project project = dal.GetThisProject(projectId);
            return View("FullSingleProject", project);
        }
        public IActionResult MyProject()
        {
            Dal dal = new Dal();
            Project project = dal.GetMyProject(1);
            //Project project = dal.GetMyProject(Convert.ToInt32(User.Identity.Name));
            return View(project);
        }

        //*****************************************************************************************

        //Pour le gestionnaire de projet, il affiche le formulaire pour remplir/modifier les champs.
        public ActionResult CreateProject(int projectId)
        {
            if (projectId != 0)
            {
                Dal dal = new Dal();
                Project project = dal.GetThisProject(projectId);
                return View(project);
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateProject(Project project, IFormFile image)
        {
            Dal dal = new Dal();

            if (image != null && image.Length > 0)
            {
                var fileName = Path.GetFileName(image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\img", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }

            if (dal.ProjectExiste(project.ProjectName))
            {
                ModelState.AddModelError("Nom", "Ce nom du project existe déjà");
                return View(project);
            }
            if (!ModelState.IsValid)
                return View(project);

            //recupere id du user connecté
            string uaId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserAccount ua = dal.GetUserAccountConnected(uaId);

            dal.CreateProject(project.ProjectName, project.Description, project.Category, project.StartDate, project.EndDate,
                project.Place, project.Area, project.Limit, ua.ProfilId, project.Id, project.Summary, image.FileName);
            
            return Redirect("/Project/Index");
        }
    }
}
