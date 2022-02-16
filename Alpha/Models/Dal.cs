using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class Dal : IDal
    {
        private BddContext _bddContext;
        public Dal()
        {
            _bddContext = new BddContext();
        }

        
        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

       
        //Renvoie la liste des projets, leur statut, leur montant collecté.
        public List<Project> GetAllProjects()
        {
            return _bddContext.Projects.Include(p => p.Collect).Include(p => p.Profile).ThenInclude(p => p.Adress).ToList();
        }

        //renvoie tous les dons par collecte
        public List<UnitDonation> GetDonationsByCollectId(int collectId)
        {
            return _bddContext.UnitDonations.Where(u => u.CollectId == collectId).ToList();
        }

        //calcul a mise a jour de la somme des collectes
        public int AmountCalculation(int collectId)
        {
            List<UnitDonation> donations = GetDonationsByCollectId(collectId);
            int amount = 0;
            foreach (UnitDonation donation in donations)
            {
                amount += donation.CurrentAmount;
            }
            return amount;
        }

        //affiche le projet du createur
        public Project GetMyProject(int profileId)
        {
            Project project = _bddContext.Projects.Include(p => p.Collect).Include(p => p.Profile).FirstOrDefault(p => p.ProfileId == profileId);
            return project;
        }

        // Creation de projet
        public void CreateProject(string projectName, string description, ProjectCategory category, DateTime startDate,
            DateTime endDate, string place, WorldAreas area, Int32 limit, int? profileId, int id, int? collectId)
        {
            Project projectToAdd = new Project { ProjectName = projectName, Description = description,
                Category = category, StartDate = startDate, EndDate = endDate, Area = area, Limit = limit,
                ProfileId = profileId, CollectId = collectId };
            if (id != 0)
            {
                projectToAdd.Id = id;
            }
           
            this._bddContext.Projects.Add(projectToAdd);
            this._bddContext.SaveChanges();
        }

        public bool ProjectExiste(string projectName)
        {
            return _bddContext.Projects.ToList().Any(Project => string.Compare(Project.ProjectName, projectName, StringComparison.CurrentCultureIgnoreCase) == 0);
        }

        public List<Profile> GetAllProfiles()
        {
            return _bddContext.Profiles.ToList();
        }

        public List<UserAccount> GetAllUserAccount()
        {
            return _bddContext.UserAccounts.Include(u => u.Profil).ThenInclude(p => p.Adress).ToList();
        }

        public void ProfileChange(int id,  string lastName, string firstName, string nationality, Int32 birthday,
            string nick, string phone, string payMethod)
        {
            Profile profile = _bddContext.Profiles.Find(id);

            if (profile != null)
            {
                profile.LastName = lastName;
                profile.FirstName = firstName;
                profile.Nationality = nationality;
                profile.Nick = nick;
                profile.Birthday = birthday;
                profile.Phone = phone;
                profile.PayMethod = payMethod;
                _bddContext.SaveChanges();
            }
        }

        //Creer une collecte
        public void CreateCollect()
        {
            Collect collect = new Collect { };
            this._bddContext.Add(collect);
            this._bddContext.SaveChanges();
        }

        // Authentification functions

        public int AddUserAccount(string mail, string password)
        {
            Profile profil = new Profile()
            {

            };

            this._bddContext.Profiles.Add(profil);
            this._bddContext.SaveChanges();

            string motDePasse = EncodeMD5(password);
            UserAccount userAccount = new UserAccount() { Mail = mail, Password = motDePasse, Status=AccountStatus.Valid, ProfilId = profil.Id };
            this._bddContext.UserAccounts.Add(userAccount);
            this._bddContext.SaveChanges();

            return userAccount.Id;
        }

        public UserAccount Authentify(string mail, string password)
        {
            string motDePasse = EncodeMD5(password);
            UserAccount userAccount = this._bddContext.UserAccounts.FirstOrDefault(u => u.Mail == mail && u.Password == motDePasse);
            return userAccount;
        }


        public UserAccount GetUserAccount(int id)
        {
            return this._bddContext.UserAccounts.Find(id);
        }

        public UserAccount GetUserAccount(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetUserAccount(id);
            }
            return null;
        }

        private string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "ChoixUtilisateur" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
