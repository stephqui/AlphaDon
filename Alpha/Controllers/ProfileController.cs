using Alpha.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Controllers
{
   // [Authorize]
    public class ProfileController : Controller
    {
        private Dal dal;
        public ProfileController()
        {
            dal = new Dal();
        }
        // [AllowAnonymous]
        public IActionResult Index()
        {
            List<Profile> profiles = dal.GetAllProfiles();
            return View(profiles);

        }
        public IActionResult ProfileChange(int id)
        {
            if (id != 0)
            {
               
                    Profile profile = dal.GetAllProfiles().Where(s => s.Id == id).FirstOrDefault();
                    if (profile == null)
                    {
                        return View("Error");
                    }
                    return View(profile);//retourne le formulaire
            }
            return View("Error");
        }
        [HttpPost]
        public IActionResult ProfileChange(Profile profile, IFormFile image)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(profile);
            //}

            if (image != null && image.Length > 0)
            {
                var fileName = Path.GetFileName(image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\img", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }
            if (profile.Id != 0)
            {
                    dal.ProfileChange(profile.Id, profile.LastName, profile.FirstName, profile.Nationality,
                     profile.Birthday, profile.Nick, profile.Phone, profile.PayMethod, image.FileName);
                    return Redirect("/Home/Index");
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
