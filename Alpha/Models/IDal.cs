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
        void ProjectExiste(string projectName);
        void GetAllProjects();
        void GetMyProject();
        void GetDonationsByCollectId();
        void AmountCalculation();
        void CreateProject(string projectName, string description, ProjectCategory category, DateTime startDate,
            DateTime endDate, string place, WorldAreas area, Int32 limit, int? profileId, int id, int? collectId);
        void CreateUnitDonation(int id, string payMethod, int CurrentAmount, DateTime date);


        //*********************************** PROFILE / USERACCOUNT *************************************************
        int AddUserAccount(string mail, string password);

        void GetAllProfiles();
        void ProfileChange(int id, string lastName, string firstName, string nationality, Int32 birthday,
            string nick, string phone, string payMethod);
        UserAccount Authentify(string mail, string password);
        UserAccount GetUserAccount(int id);
        void GetAllUserAccount();
        UserAccount GetUserAccount(string idStr);
    }
}
