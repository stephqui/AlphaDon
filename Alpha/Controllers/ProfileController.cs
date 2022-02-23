using Alpha.Models;
using Alpha.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
                UserAccountViewModel viewModel = new UserAccountViewModel();
                
                    viewModel.Profile = dal.GetAllProfiles().Where(s => s.Id == id).FirstOrDefault();
                viewModel.Adress = dal.GetAdressFromProfile(viewModel.Profile.AdressId);
                    //Profile profile = dal.GetAllProfiles().Where(s => s.Id == id).FirstOrDefault();
                    if (viewModel.Profile == null)
                    {
                        return View("Error");
                    }
                    return View(viewModel);//retourne le formulaire
            }
            return View("Error");
        }
        [HttpPost]
        public IActionResult ProfileChange(int id, UserAccountViewModel viewModel, IFormFile image)
        {

            //UserAccountViewModel viewModel = new UserAccountViewModel();
            Profile profile = viewModel.Profile;
            profile.Adress = viewModel.Adress;
            //viewModel.Profile = profile;
            //viewModel.Profile.Id = id;
            //viewModel.Adress = dal.GetAdressFromProfile(viewModel.Profile.AdressId);
            if (image != null && image.Length > 0)
            {
                var fileName = Path.GetFileName(image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\img", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }

            if (profile.Id != 0 && image == null)
            {
                dal.ProfileChangeNoImage(profile.Id, profile.LastName, profile.FirstName, profile.Nationality,
                 profile.Birthday, profile.Nick, profile.Phone, profile.PayMethod, profile.Siret, profile.Adress.Street,
                 profile.Adress.Zip,profile.Adress.City, profile.Adress.Country, profile.profilePersonality);
                return Redirect("/Home/Index");
                //return Redirect("/Profile/ProfileChange"); ne marche pas: erreur page profilechange @Model.Picture ligne20
            }
            else if (profile.Id != 0)
            {
                dal.ProfileChange(profile.Id, profile.LastName, profile.FirstName, profile.Nationality,
                 profile.Birthday, profile.Nick, profile.Phone, profile.PayMethod, image.FileName, profile.Siret, profile.Adress.Street, profile.Adress.Zip, 
                 profile.Adress.City, profile.Adress.Country, profile.profilePersonality);
            
                return Redirect("/Home/Index");
                //return Redirect("/Profile/ProfileChange"); ne marche pas: erreur page profilechange @Model.Picture ligne20
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
