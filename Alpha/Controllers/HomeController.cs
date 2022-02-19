using Alpha.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Controllers
{
    public class HomeController : Controller
    {
        private Dal dal;
        public HomeController()
        {
            dal = new Dal();
        }
        public IActionResult Index()
        {
            List<Profile> profiles = dal.GetAllProfiles();
            return View(profiles);

        }
    }
}
