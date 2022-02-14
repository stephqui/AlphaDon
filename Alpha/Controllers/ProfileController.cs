using Alpha.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Controllers
{
   // [Authorize]
    public class ProfileController : Controller
    {
       // [AllowAnonymous]
        public IActionResult Index()
        {
            Dal dal = new Dal();
            List<Profile> profiles = dal.GetAllProfiles();
            return View(profiles);

        }
        public IActionResult ProfileChange(int id)
        {
            if (id != 0)
            {
                using (Dal dal = new Dal())
                {
                    Profile profile = dal.GetAllProfiles().Where(s => s.Id == id).FirstOrDefault();
                    if (profile == null)
                    {
                        return View("Error");
                    }
                    return View(profile);//retourne le formulaire
                }
            }
            return View("Error");
        }
        [HttpPost]
        public IActionResult ProfileChange(Profile profile)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(profile);
            //}
            if (profile.Id != 0)
            {
                using (Dal dal = new Dal())
                {
                    dal.ProfileChange(profile.Id, profile.LastName, profile.FirstName, profile.Nationality,
                     profile.Birthday, profile.Nick, profile.Phone, profile.PayMethod);
                    return RedirectToAction("ProfileChange", new { @id = profile.Id });
                }
            }
            else
            {
                return View("Error");
            }
        }
        public IActionResult Test()
        {
            List<UserAccount> userAccounts = new Dal().GetAllUserAccount();
            return View(userAccounts);
        }
    }

}
