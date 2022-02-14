using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public enum AccountStatus { Valid, IsProjectCreator, Banned}
    public class UserAccount
    {
        
        public int Id { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }

        [MaxLength(35)]
        public string Mail { get; set; }

    
        public AccountStatus Status { get; set; }
 

        //creation ForeignKey en 2 lignes

        public int? ProfilId { get; set; }
        public virtual Profile Profil { get; set; }

        public int? ProjectWalletId { get; set; }
        public virtual ProjectWallet ProjectWallet { get; set; }

    }
}
