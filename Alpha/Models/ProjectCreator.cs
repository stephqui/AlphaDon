using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class ProjectCreator
    {   
        public int Id { get; set; }

        [MaxLength(70)]
        public string webSite { get; set; }

        //public int? UserAccountId { get; set; }
        //public virtual UserAccount UserAccount { get; set; }

        //creation ForeignKey en 2 lignes
        //public int? ProjectWalletId { get; set; }
        //public virtual ProjectWallet ProjectWallet { get; set; }

        public int? OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

    }
}
