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

        public List<Project> GetAllProjects()
        {
            return _bddContext.Projects.Include(p => p.Profile).ThenInclude(p => p.Adress).ToList();
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
