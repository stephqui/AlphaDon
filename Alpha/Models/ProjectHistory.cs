using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Alpha.Models
{
    public class ProjectHistory
    {
        public int Id { get; set; }

        [Required]
        public string ListeProjet { get; set; }
    }
}
