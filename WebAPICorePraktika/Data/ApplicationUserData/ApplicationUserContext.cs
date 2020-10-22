using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICorePraktika.Models;

namespace WebAPICorePraktika.Data.ApplicationUserData {
    public class ApplicationUserContext : IdentityDbContext<ApplicationUser> {
        public ApplicationUserContext( DbContextOptions<ApplicationUserContext> opt): base(opt) {

        }
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
        }

        public DbSet<Departamenti> Departament { get; set; }
        public DbSet<PozicioniPunes> PozicioniPune { get; set; }
    }
}
