using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class Collect
    {
        public int Id { get; set; }
    [MaxLength(30)]
        //[Required]
        //public int Limit { get; set; }

        public int CurrentAmount { get; set; }
    }
}
