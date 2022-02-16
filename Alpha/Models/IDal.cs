using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Models
{
    interface IDal: IDisposable
    {
        void DeleteCreateDatabase();



        //void GetAllProjects();
        //void GetMyProject();
        //void GetDonationsByCollectId();
        //void AmountCalculation();
        void CreateProject(string projectName, string description, ProjectCategory category, DateTime startDate,
            DateTime endDate, string place, WorldAreas area, Int32 limit, int? profileId, int id, int? collectId);
        int AddUserAccount(string mail, string password);
        UserAccount Authentify(string mail, string password);
        UserAccount GetUserAccount(int id);
        UserAccount GetUserAccount(string idStr);
    }
}
