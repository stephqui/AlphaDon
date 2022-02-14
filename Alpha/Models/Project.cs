using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public enum ProjectStatus { created, toControl, beingControled, collectStarted, collectOver, beingExecuted, executed }
    public enum Range { international, national, regional, local }
    public enum WorldAreas { middleEast, farEast, africa, northAmerica, southAmerica, europe, oceania }
    public enum ProjectCategory { medical, sport, humanitarian, educational, infrastructural, fairTrade }
   
    
    public class Project
    {
        public int Id { get; set; }

        [MaxLength(40)]
        public string ProjectName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        [MaxLength(20)]
        public string Place { get; set; }

        [MaxLength(20)]
        public string Rib { get; set; }


        // enum
      
        public ProjectStatus Status { get; set; }

        public Range Zone { get; set; }

        public WorldAreas Area { get; set; }

        // pour category : possibilité de faire un choix multiple 
        public ProjectCategory Category { get; set; }


        //creation ForeignKey en 2 lignes
        public int? ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        public int? CollectId { get; set; }
        public virtual Collect Collect { get; set; }
        public int? ProjectHistoryId { get; set; }
        public virtual ProjectHistory ProjectHistory { get; set; }
    }

}
