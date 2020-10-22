using System.Collections.Generic;
using System.Linq;
using WebAPICorePraktika.Models;
using Microsoft.EntityFrameworkCore;
using WebAPICorePraktika.Data.ApplicationUserData;

namespace WebAPICorePraktika.Data.PozicionPuneData {
    public class PozicionPuneRepository : IPozicionPuneRepository {
        private readonly ApplicationUserContext _context;

        public PozicionPuneRepository(ApplicationUserContext context) {
            _context = context;
        }

        public void CreatePozicionPune(PozicioniPunes pozicioniPunes) {
            _context.PozicioniPune.Add(pozicioniPunes);
        }

        public void DeletePozicionPune(PozicioniPunes pozicioniPunes) {
            _context.PozicioniPune.Remove(pozicioniPunes);
        }

        public bool DepartamentExist(int id) {
            if(_context.Departament.Where(d => d.DepartamentId == id).Any()) {
                return true;
            }
            return false;
        }

        public IEnumerable<PozicioniPunes> GetAllPozicioniPune() {
            return _context.PozicioniPune.Include(d => d.Departament).Include(d => d.applicationUsers).ToList();
        }

        public PozicioniPunes GetPozicioniPunesById(int id) {
            return _context.PozicioniPune.Include(d => d.Departament).Include(d => d.applicationUsers).FirstOrDefault(p => p.PozicionPuneId == id);
        }

        public bool SaveChanges() {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdatePozicioniPune(PozicioniPunes pozicioniPunes) {
            _context.Entry(pozicioniPunes).State = EntityState.Modified;
        }
    }
}
