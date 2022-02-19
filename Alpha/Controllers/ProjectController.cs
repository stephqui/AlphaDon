﻿using Alpha.Models;
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
        private Dal dal;
        public ProjectController()
        {
            dal = new Dal();
        }
        /*public IActionResult Index(int value, string test)*/
        public IActionResult Index()
        {
            List<Project> projects = dal.GetAllProjects();
            return View(projects);     
        }
        public IActionResult ManageProjects()
        {
            List<Project> projects = dal.GetAllProjects();
            return View(projects);
        }

        //le gestionnaire controle le projet en cours de création
        public IActionResult ThisProject(int projectId)
        {

            Project project = dal.GetThisProject(projectId);
            return View("MyProject", project);
        }


        //visiteur qui veut faire un don
        public IActionResult FullSingleProject(int projectId)
        {
            Project project = dal.GetThisProject(projectId);
            return View("FullSingleProject", project);
        }

        //le createur du projet va voir son projet
        public IActionResult MyProject()
        {
            //Project project = dal.GetMyProject(1);
            Project project = dal.GetMyProject(Convert.ToInt32(User.Identity.Name));
            
            return View(project);
        }

        //*****************************************************************************************


        public ActionResult CreateProject(int projectId)
        {
            if (projectId != 0)
            {

                Project project = dal.GetThisProject(projectId);
                return View(project);
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateProject(Project project, IFormFile image)
        {

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
    

    //commentaires
    [HttpPost]
    public IActionResult NewComment(Project project, ushort projectId, string comment)
    {

        string stUserId = HttpContext.User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Sid)?.Value;
        dal.CreateCommentProject(ushort.Parse(stUserId), projectId, comment);

        return RedirectToAction("FullSingleProject", new { id = projectId });
    }

    [HttpPost]
    public IActionResult AnswerComment(Project proj, ushort projectId, ushort replyerId, string comment)
    {

        string stUserId = HttpContext.User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Sid)?.Value;
        dal.AnswerCommentProject(ushort.Parse(stUserId), projectId, comment, replyerId);

        return RedirectToAction("FullSingleProject");
    }
}
}

