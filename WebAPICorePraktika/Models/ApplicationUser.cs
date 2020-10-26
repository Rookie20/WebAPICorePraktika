using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPICorePraktika.Models {
    public class ApplicationUser : IdentityUser {

        public string Emer { get; set; }
        public string Mbiemer { get; set; }
        public string KartaId { get; set; }
        public bool Aktiv { get; set; }

        [ForeignKey("PozicioniPune")]
        public int PozicionPuneId { get; set; }

        [JsonIgnore]
        public PozicioniPunes PozicioniPune { get; set; }

        [JsonIgnore]
        public List<HistorikuPoziPune> HistorikuPoziPunes { get; set; }

        [JsonIgnore]
        public List<Files> Files { get; set; }
    }

    public static class UserRoles {

        public const string Admin = "Admin";
        public const string Manaxher = "Manaxher";
        public const string User = "User";
    }

    public class RegisterModel {

        [Required]
        public string Emer { get; set; }

        [Required]
        public string Mbiemer { get; set; }

        [Required]
        public string KartaId { get; set; }

        [Required]
        public string Username { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public int PozicioniPuneId { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class LoginModel {

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class Response {
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
