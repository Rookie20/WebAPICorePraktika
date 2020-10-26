using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPICorePraktika.Models {
    public class PozicioniPunes {
        [Key]
        public int PozicionPuneId { get; set; }
        [Required]
        public string PozicionPuneEmri { get; set; }

        [ForeignKey("Departament")]
        [Required]
        public int DepartamentId { get; set; }
        public Departamenti Departament { get; set; }

        public List<ApplicationUser> ApplicationUsers { get; set; }
    }
}
