using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Alpha.Models
{
    public class ProjectManager
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string BusinessAccount { get; set; }

        [Required]
        [MaxLength(20)]
        public string Pseudo { get; set; }

        //creation ForeignKey en 2 lignes
        //public int? ProjectWalletId { get; set; }
        //public virtual ProjectWallet ProjectWallet { get; set; }

    }
}
