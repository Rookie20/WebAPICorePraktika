using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPICorePraktika.Models {
    public class Departamenti {

        [Key]
        public int DepartamentId { get; set; }

        [Required]
        public string DepartamentEmer { get; set; }

        public DateTime DepDataKrijimit { get; set; }

        [Required]
        public string DepartamentPershkrimi { get; set; }
    }
}
