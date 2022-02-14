using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class Adress
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Street { get; set; }

        [MaxLength(30)]
        public string City { get; set; }

        public int Zip { get; set; }

        [MaxLength(2)]
        public string Country { get; set; }

        //creation ForeignKey
        //public int? ProfilId { get; set; }
        //public virtual Profile Profil { get; set; }
    }
}
