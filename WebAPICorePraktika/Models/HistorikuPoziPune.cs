using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPICorePraktika.Models {
    public class HistorikuPoziPune {

        [Key]
        public int HistorikuId { get; set; }
        public string PozicioniPerpara { get; set; }
        public string PozicioniPas { get; set; }

        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
