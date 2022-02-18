using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class UnitDonation
    {
        public int Id { get; set; }

        [MaxLength(5)]
        public string PayMethod { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }


        //creation ForeignKey en 2 lignes
        public int? CollectId { get; set; }
        public virtual Collect Collect { get; set; }

        public int? AnonymUserId { get; set; }
        public virtual AnonymUser AnonymUser { get; set; }
    }
}
