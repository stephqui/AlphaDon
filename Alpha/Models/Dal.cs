using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        //**************************************** PROJECT *******************************************************************

        //Renvoie la liste des projets, leur statut, leur montant collecté.
        public List<Project> GetAllProjects()
        {
            return _bddContext.Projects.Include(p => p.Collect).Include(p => p.Profile).ThenInclude(p => p.Adress).ToList();
        }

        //renvoie tous les dons par collecte
        public List<UnitDonation> GetDonationsByCollectId(int collectId)
        {
            return _bddContext.UnitDonations.Where(u => u.CollectId == collectId).ToList();
            //return _bddContext.UnitDonations.Count();
        }

 
        //affiche le projet du createur
        public Project GetMyProject(int profileId)
        {
            Project project = _bddContext.Projects.Include(p => p.Collect).Include(p => p.Profile).FirstOrDefault(p => p.ProfileId == profileId);
            return project;
        }

        public Project GetThisProject(int projectId)
        {

            Project targetProject =  _bddContext.Projects.Include(p => p.Profile).Include(c => c.Collect).FirstOrDefault(p => p.Id == projectId);
            return targetProject;
        }

        // Creation de projet
        public void CreateProject(string projectName, string description, ProjectCategory category, DateTime startDate,
            DateTime endDate, string place, WorldAreas area, Int32 limit, int? profileId, int id, 
            string summary, string picture)
        {
            //on crée une collecte en meme temps que le projet
            Collect collect = new Collect { };
            this._bddContext.Collects.Add(collect);
            this._bddContext.SaveChanges();

            Project projectToAdd = new Project { ProjectName = projectName, Description = description,
                Category = category, StartDate = startDate, EndDate = endDate, Area = area, Limit = limit,
                ProfileId = profileId, CollectId = collect.Id, Picture = picture };
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

        public void DeleteProject(int id)
        {
            Project projectToDelete = this._bddContext.Projects.Find(id);
            this._bddContext.Projects.Remove(projectToDelete);
            this._bddContext.SaveChanges();
        }

        public void DeleteProject(int id, string projectName, string description, string summary, string picture, string place, string rib, int limit)
        {
            Project projectToDelete = this._bddContext.Projects.Where(r => r.Id == id && r.ProjectName == projectName && r.Description == description && r.Summary == summary && r.Picture == picture && r.Place == place && r.Rib == rib && r.Limit == limit).FirstOrDefault();
            if (projectToDelete != null)
            {
                this._bddContext.Projects.Remove(projectToDelete);
                this._bddContext.SaveChanges();
            }
        }

        public void UpdateProject(int id, string projectName, string description, string summary, string picture, string place, string rib, int limit)
        {
            Project projectToUpdate = this._bddContext.Projects.Find(id);
            if (projectToUpdate != null)
            {
                projectToUpdate.ProjectName = projectName;
                projectToUpdate.Description = description;
                projectToUpdate.Summary = summary;
                projectToUpdate.Picture = picture;
                projectToUpdate.Place = place;
                projectToUpdate.Rib = rib;
                projectToUpdate.Limit = limit;
                this._bddContext.SaveChanges();
            }
        }

        //*************************************** COMMENTAIRES ********************************************
        //commentaires
        public void CreateCommentProject(ushort userId, ushort projectId, string comment)
        {
            //ajout lien project -> comment
            Comment comment1 = _bddContext.Comment.Include(p => p.Project).FirstOrDefault(p => p.ProjectId == projectId);

            Project project = _bddContext.Projects.Find(projectId);

            DateTime now = DateTime.Now;
            Comment newComment = new Comment { User = userId, ProjectId = projectId, Contenu = comment };
            //Comment newComment = new Comment { Contenu = comment, Date = now, User = user };

            _bddContext.Comment.Add(newComment);
            _bddContext.SaveChanges();
        }


        public void AnswerCommentProject(ushort userId, ushort projectId, string comment, ushort replyerId)
        {
            //ajout lien project -> comment
            Comment comment1 = _bddContext.Comment.Include(p => p.Project).FirstOrDefault(p => p.ProjectId == projectId);

            UserAccount user = _bddContext.UserAccounts.Find(userId);
            Project project = _bddContext.Projects.Find(projectId);

            DateTime now = DateTime.Now;
            Comment answerComment = _bddContext.Comment.Find(replyerId);

            Comment newComment = new Comment { Contenu = comment, Date = now, ProjectId = projectId, User = userId };
            //Comment newComment = new Comment { CommentText = comment, Date = now, Project = project, User = user, Replyer = answerComment };

            _bddContext.Comment.Add(newComment);
            _bddContext.SaveChanges();
        }

        //**************************************** DON *******************************************************************
        // Creation d'un don
        public void CreateUnitDonation(int id, string payMethod, int amount, DateTime date, int collectId)
        {
            UnitDonation unitDonationToAdd = new UnitDonation
            {
                Id = id,
                PayMethod = payMethod,
                Amount = amount,
                Date = date,
                CollectId = collectId
            };
            //if (id != 0)
            //{
            //    unitDonationToAdd.Id = id;
            //}
            this._bddContext.UnitDonations.Add(unitDonationToAdd);
            this._bddContext.SaveChanges();
        }

        //calcul a mise a jour de la somme des collectes
        public int AmountCalculation(int collectId)
        {
            Collect collectToUpdate = this._bddContext.Collects.Find(collectId);

            List<UnitDonation> donations = GetDonationsByCollectId(collectId);
            int amount = 0;
            foreach (UnitDonation donation in donations)
            {
                amount += donation.Amount;
            }

            collectToUpdate.CurrentAmount = amount;
            this._bddContext.SaveChanges();
            return amount;
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
            string nick, string phone, string payMethod, string picture)
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
                profile.Picture = picture;
                _bddContext.SaveChanges();
            }
        }

        //j'essaie de contourner si image est null
        public void ProfileChangeNoImage(int id, string lastName, string firstName, string nationality, Int32 birthday,
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
        public UserAccount GetUserAccountConnected(string uaId)
        {
            //recupere id du user connecté
            
            UserAccount ua = this.GetAllUserAccount().FirstOrDefault(u => u.Id == Convert.ToInt32(uaId));
            return ua;
        }




        // Authentification functions

        public UserAccount AddUserAccount(string mail, string password)
        {
            Profile profil = new Profile()
            {

            };

            this._bddContext.Profiles.Add(profil);
            this._bddContext.SaveChanges();

            string motDePasse = EncodeMD5(password);
            UserAccount userAccount = new UserAccount() { Mail = mail, Password = motDePasse, Status=AccountStatus.Valid, Role= "Basic", ProfilId = profil.Id };
            this._bddContext.UserAccounts.Add(userAccount);
            this._bddContext.SaveChanges();

            return userAccount;
        }



        public UserAccount Authentify(string mail, string password)
        {
            string motDePasse = EncodeMD5(password);
            UserAccount userAccount = this._bddContext.UserAccounts.Include(p => p.Profil).FirstOrDefault(u => u.Mail == mail && u.Password == motDePasse);
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
