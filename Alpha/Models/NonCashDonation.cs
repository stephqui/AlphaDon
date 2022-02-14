using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class NonCashDonation
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string DonationType { get; set; }


        //creation ForeignKey en 2 lignes
        public int? CollectId { get; set; }
        public virtual Collect Collect { get; set; }

        public int? AnonymUserId { get; set; }
        public virtual AnonymUser AnonymUser { get; set; }
    }
}
