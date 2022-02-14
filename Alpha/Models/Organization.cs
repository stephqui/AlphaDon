using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class Organization
    {
        public int Id { get; set; }

        [MaxLength(25)]
        public string Name { get; set; }


        
        //[Required]
        //public int FKProjectCreator { get; set; }

		//[Required]
        //public int FKOrgType { get; set; }
    }
}
