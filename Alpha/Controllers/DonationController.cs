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
            List<UnitDonation> unitDonations = dal.GetDonationsByCollectId(collectId);
            return View(unitDonations);
        }
        public ActionResult CreateUnitDonation(int collectId)
        {
            ViewBag.collectId = collectId;
            return View("DonationProject");
        }

        public IActionResult UpDateCollect(int collectId)
        { 
            Dal dal = new Dal();
            int test = dal.AmountCalculation(collectId);

            return RedirectToAction("Index", "Project");
            //return RedirectToAction("Index", "Project", new { value = test, test = "toto" });
        }

        // a compléter ************************************************************************************
        [HttpPost]
        public IActionResult CreateUnitDonation(UnitDonation unitDonation)
        {
            Dal dal = new Dal();
            dal.CreateUnitDonation(unitDonation.Id, unitDonation.PayMethod, unitDonation.Amount, DateTime.Now, unitDonation.CollectId.Value);
            dal.AmountCalculation(unitDonation.CollectId.Value);
            return Redirect("/Project/Index");
        }
    }
}