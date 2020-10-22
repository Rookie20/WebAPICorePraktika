using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICorePraktika.Models {
    public class ApplicationUser : IdentityUser {

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
