using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Models
{
    interface IDal: IDisposable
    {
        void DeleteCreateDatabase();

        int AddUserAccount(string mail, string password);
        UserAccount Authentify(string mail, string password);
        UserAccount GetUserAccount(int id);
        UserAccount GetUserAccount(string idStr);
    }
}
