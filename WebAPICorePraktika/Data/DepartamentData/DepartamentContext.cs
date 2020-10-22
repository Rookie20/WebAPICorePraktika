using Microsoft.EntityFrameworkCore;
using WebAPICorePraktika.Models;

namespace WebAPICorePraktika.Data.DepartamentData {
    public class DepartamentContext : DbContext{
        public DepartamentContext(DbContextOptions<DepartamentContext> dbContext) : base(dbContext) {

        }

        public DbSet<Departamenti> Departament { get; set; }
        public DbSet<PozicioniPunes> PozicioniPune { get; set; }
    }
}
