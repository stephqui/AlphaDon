using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class Newsletter
    {
        public int Id { get; set; }

        public string Contenu { get; set; }

        public DateTime Date { get; set; }


        //creation ForeignKey en 2 lignes
        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }

        //la table Adminitrator va etre modifiée. En attente.
        //public int? AdministratorId { get; set; }
        //public virtual Administrator Administrator { get; set; }
    }
}
