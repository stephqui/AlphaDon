using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class PeriodicDonation
    {
        public int Id { get; set; }
        public DateTime Periodicity { get; set; }

        //creation ForeignKey en 2 lignes
        public int? UnitDonationId { get; set; }
        public virtual UnitDonation UnitDonation { get; set; }
    }
}
