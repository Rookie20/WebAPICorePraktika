using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPICorePraktika.Data.ApplicationUserData;
using WebAPICorePraktika.Models;

namespace WebAPICorePraktika.Data.DepartamentData {
    public class DepartamentRepository : IDepartamentRepository {
        private readonly ApplicationUserContext _context;

        public DepartamentRepository(ApplicationUserContext context) {
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
            return _context.Departament.Include(p => p.PozicioniPune).ThenInclude(p => p.ApplicationUsers).ToList();
        }

        public bool GetDepartamentEmer(string departamentEmer) {
            if(_context.Departament.Any(p => p.DepartamentEmer.Contains(departamentEmer))) {
                return true;
            }
            return false;
        }

        public Departamenti GetDepartamentiById(int id) {
            return _context.Departament.Include(p =>p.PozicioniPune).ThenInclude(p => p.ApplicationUsers).FirstOrDefault(d => d.DepartamentId == id);
        }

        public bool SaveChanges() {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateDepartament(Departamenti departamenti) {
            _context.Entry(departamenti).State = EntityState.Modified;
        }
    }
}
