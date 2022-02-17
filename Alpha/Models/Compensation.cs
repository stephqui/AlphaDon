using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class Compensation
    {
        public int Id { get; set; }

        [MaxLength(6)]
        public long Amount { get; set; }

        public string Object { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }

        //creation ForeignKey en 2 lignes
        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }

    }
}
