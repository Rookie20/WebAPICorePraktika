using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPICorePraktika.Models {
    public class Departamenti {

        [Key]
        public int DepartamentId { get; set; }

        [Required]
        public string DepartamentEmer { get; set; }

        [DataType(DataType.Date)]
        public DateTime DepDataKrijimit { get; set; }

        [Required]
        public string DepartamentPershkrimi { get; set; }

        [JsonIgnore]
        public  List<PozicioniPunes> PozicioniPune  { get; set; }
    }
}
