using Alpha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.ViewModels
{
    public class UserAccountViewModel
    {
        public UserAccount UserAccount{ get; set; }
        public Profile Profile { get; set; }
        public Adress Adress { get; set; }
        public bool Authentify { get; set; }
    }
}
