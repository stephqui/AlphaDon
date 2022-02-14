using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Alpha.Models
{
    public class Administrator
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string BusinessAccount { get; set; }

        [MaxLength(20)]
        public string Pseudo { get; set; }
    }
}
