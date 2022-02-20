using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Models
{
    interface IDal: IDisposable
    {
        void DeleteCreateDatabase();

        //*********************************** PROJECT *************************************************
        bool ProjectExiste(string projectName);
        List<Project> GetAllProjects();
        Project GetThisProject(int projectId);
        Project GetMyProject(int profileId);
        List<UnitDonation> GetDonationsByCollectId(int collectId);
        int AmountCalculation(int collectId);
        void CreateProject(string projectName, string description, ProjectCategory category, DateTime startDate,
            DateTime endDate, string place, WorldAreas area, Int32 limit, int? profileId, int id, 
            string summary, string picture);
        void CreateUnitDonation(int id, string payMethod, int CurrentAmount, DateTime date, int collectId);


        //*********************************** PROFILE / USERACCOUNT *************************************************
        UserAccount AddUserAccount(string mail, string password);

        List<Profile> GetAllProfiles();
        void ProfileChange(int id, string lastName, string firstName, string nationality, Int32 birthday,
            string nick, string phone, string payMethod, string picture);
        void ProfileChangeNoImage(int id, string lastName, string firstName, string nationality, Int32 birthday,
           string nick, string phone, string payMethod);
        UserAccount Authentify(string mail, string password);
        UserAccount GetUserAccount(int id);
        List<UserAccount> GetAllUserAccount();
        UserAccount GetUserAccount(string idStr);
    }
}
