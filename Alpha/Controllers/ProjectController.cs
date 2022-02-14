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
    }
}
