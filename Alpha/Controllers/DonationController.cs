using Alpha.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Alpha.Controllers
{
    public class DonationController : Controller
    {
        private IDal dal;
        public DonationController()
        {
            dal = new Dal();
        }
        public IActionResult Index(int collectId)
        {
            Dal dal = new Dal();
            List<UnitDonation> unitDonations = dal.GetDonationsByCollectId(2);
            return View(unitDonations);
        }
        public ActionResult CreateUnitDonation()
        {
            return View();
        }

        public IActionResult UpDateCollect(int collectId)
        { 
            Dal dal = new Dal();
            int test = dal.AmountCalculation(2);

            return RedirectToAction("Index", "Project");
            //return RedirectToAction("Index", "Project", new { value = test, test = "toto" });
        }

        // a compléter ************************************************************************************
        [HttpPost]
        public IActionResult CreateUnitDonation(UnitDonation unitDonation)
        {
            Dal dal = new Dal();

            //if (dal.CreateUnitDonation(project.ProjectName))
            //{
            //    ModelState.AddModelError("Nom", "Ce nom du project existe déjà");
            //    return View(project);
            //}
            //if (!ModelState.IsValid)
            //    return View(project);
            //dal.CreateCollect();
            //dal.CreateProject(project.ProjectName, project.Description, project.Category, project.StartDate, project.EndDate,
            //    project.Place, project.Area, project.Limit, project.ProfileId, project.Id, project.CollectId);
            return Redirect("/Project/Index");
        }
    }
}