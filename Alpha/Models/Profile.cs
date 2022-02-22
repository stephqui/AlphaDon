using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public enum ProfilePersonality { Single, Ngo, Association, Company }
    public class Profile
    {
        public int Id { get; set; }

        public ProfilePersonality profilePersonality { get; set; }

        [MaxLength(40)]
        public string FirstName{ get; set; }

        [MaxLength(40)]
        public string LastName { get; set; }

        [MaxLength(2)]
        public string Nationality { get; set; }

        public Int32 Birthday { get; set; }

        [MaxLength(15)]
        public string Nick { get; set; }

        [MaxLength(14)]
        public string Siret { get; set; }

        [MaxLength(13)]
        public string Phone { get; set; }
        public string Picture { get; set; }

        //public List<Project> MyFavoritesProjects { get; set; }
        public List<Newsletter> MyNewsletters { get; set; }

        [MaxLength(5)]
        public string PayMethod { get; set; }
        public int? AdressId { get; set; }
        public virtual Adress Adress { get; set; }
    }
}
