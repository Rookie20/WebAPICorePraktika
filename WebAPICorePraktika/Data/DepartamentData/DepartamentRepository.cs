using System;
using System.Collections.Generic;
using System.Linq;
using WebAPICorePraktika.Models;

namespace WebAPICorePraktika.Data.DepartamentData {
    public class DepartamentRepository : IDepartamentRepository {
        private readonly DepartamentContext _context;

        public DepartamentRepository(DepartamentContext context) {
            _context = context;
        }

        public void CreateDepartament(Departamenti departamenti) {
            departamenti.DepDataKrijimit = DateTime.Now;
            _context.Departament.Add(departamenti);
        }

        public void DeleteDepartament(Departamenti departamenti) {
            _context.Departament.Remove(departamenti);
        }

        public IEnumerable<Departamenti> GetAllDepartamente() {
            return _context.Departament.ToList();
        }

        public Departamenti GetDepartamentiById(int id) {
            return _context.Departament.FirstOrDefault(d => d.DepartamentId == id);
        }

        public bool SaveChanges() {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateDepartament(Departamenti departamenti) {
            _context.Entry(departamenti).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
