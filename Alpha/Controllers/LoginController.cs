using Alpha.Models;
using Alpha.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Alpha.Controllers
{
    public class LoginController : Controller
    {
        private Dal dal;
        public LoginController()
        {
            dal = new Dal();
        }
        public IActionResult Index()
        {
            UserAccountViewModel viewModel = new UserAccountViewModel { Authentify = HttpContext.User.Identity.IsAuthenticated };
            if (viewModel.Authentify)
            {
                viewModel.UserAccount = dal.GetUserAccount(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
                return View(viewModel);
            }
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Index(UserAccountViewModel viewModel, string returnUrl)
        {
            if (viewModel.UserAccount.Mail != null && viewModel.UserAccount.Password != null)
            {
                UserAccount userAccount = dal.Authentify(viewModel.UserAccount.Mail, viewModel.UserAccount.Password);
                if (userAccount != null)
                {
                    var userClaims = new List<Claim>()
                    {
                        //new Claim(ClaimTypes.Name, userAccount.Profil.FirstName),
                        new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()),
                        new Claim(ClaimTypes.Role, userAccount.Role),
                    };
                    if (userAccount.Profil.FirstName != null)
                    { 
                        userClaims.Add(new Claim(ClaimTypes.Name, userAccount.Profil.FirstName));
                    }

                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });

                    HttpContext.SignInAsync(userPrincipal);

                    //Pour recuperer le role ou l'id
                    //string str = @User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                    //string str = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return Redirect("/home/index");
                }
                ModelState.AddModelError("Utilisateur.Mail", "Mail et/ou mot de passe incorrect(s)");
            }
            return View(viewModel);
        }

        public IActionResult CreateUserAccount()
        {
            string returnUrl = Request.Headers["Referer"].ToString();
            ViewBag.returnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        public IActionResult CreateUserAccount(UserAccount userAccount)
        //public IActionResult CreateUserAccount(UserAccount userAccount, int forDonation, string returnUrl)
        {
            
            if (userAccount.Mail !=null && userAccount.Password !=null)
            {
                userAccount = dal.AddUserAccount(userAccount.Mail, userAccount.Password);
                var userClaims = new List<Claim>()
                {
                    //new Claim(ClaimTypes.Name, id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()),
                        new Claim(ClaimTypes.Role, userAccount.Role),
                        new Claim(ClaimTypes.Name, userAccount.Profil.FirstName),
                };

                var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                HttpContext.SignInAsync(userPrincipal);
               // if (forDonation == 0)
                    return RedirectToAction("ProfileChange", "Profile", new { id = userAccount.Id });
                //else
                    //return Redirect(returnUrl);

                //return Redirect("/Profile/ProfileChange");
            }
            return View(userAccount);
        }

       

        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
