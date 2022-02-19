using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int User { get; set; }

        public string Contenu { get; set; }

        public DateTime Date { get; set; }


        //creation ForeignKey en 2 lignes
        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }
        //==> est ce qu'on met cette clé ?
    }
}
