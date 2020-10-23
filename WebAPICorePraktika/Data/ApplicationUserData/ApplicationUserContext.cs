using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<HistorikuPoziPune> HistorikuPoziPunes { get; set; }
        public DbSet<Files> Files { get; set; }
    }
}
