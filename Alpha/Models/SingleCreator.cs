using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class SingleCreator
    {
        public int Id { get; set; }

        [MaxLength(40)]
        public string FirstName { get; set; }

        [MaxLength(40)]
        public string LastName { get; set; }

        public int? ProjectCreatorId { get; set; }
        public virtual ProjectCreator ProjectCreator { get; set; }




    }
}
