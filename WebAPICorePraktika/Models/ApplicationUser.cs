using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPICorePraktika.Models {
    public class ApplicationUser : IdentityUser {

        [ForeignKey("PozicioniPune")]
        public int PozicionPuneId { get; set; }
        public PozicioniPunes PozicioniPune { get; set; }
    }

    public static class UserRoles {

        public const string Admin = "Admin";
        public const string Manaxher = "Manaxher";
        public const string User = "User";
    }

    public class RegisterModel {

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
